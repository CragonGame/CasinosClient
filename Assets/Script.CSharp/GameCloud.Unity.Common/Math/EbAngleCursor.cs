// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;

    public class EbAngleCursor1
    {
        //---------------------------------------------------------------------
        float _curDiretion;
        float _rotSpd = 10.0f;
        public float Direction { get { return _curDiretion; } set { _curDiretion = value; EbAngle.Normalize(ref _curDiretion); } }
        public float RotSpd { get { return _rotSpd; } set { _rotSpd = EbMath.Max(1.0f, Math.Abs(value)); } }

        //---------------------------------------------------------------------
        public bool Update(float deltaTime, float newDir)
        {
            if (newDir == _curDiretion)
            {
                return false;
            }
            float maxRot = deltaTime * _rotSpd;
            float deltaAngle = newDir - _curDiretion;
            EbAngle.Normalize(ref deltaAngle);
            if (Math.Abs(deltaAngle) < maxRot)
            {
                _curDiretion = newDir;
            }
            else
            {
                _curDiretion += maxRot * EbMath.Sign(deltaAngle);
            }
            EbAngle.Normalize(ref _curDiretion);
            return true;
        }
    }

    public class EbAngleCursor2
    {
        //---------------------------------------------------------------------
        float _curDiretion;
        float _accelerate = 2.0f;
        float _currentSpeed = 0.0f;
        float _maxSpeed = 4.0f;
        public float CurrentSpeed { get { return _currentSpeed; } }
        public float Direction
        {
            get { return _curDiretion; }
            set
            {
                _curDiretion = value;
                EbAngle.Normalize(ref _curDiretion);
                _currentSpeed = 0.0f;
            }
        }
        public float Accelerate { get { return _accelerate; } set { _accelerate = Math.Abs(value); } }
        public float MaxSpeed { get { return _maxSpeed; } set { _maxSpeed = Math.Abs(value); } }

        //---------------------------------------------------------------------
        public EbAngleCursor2()
        {
            SetSample(3.14f, 0.23f);
        }

        //---------------------------------------------------------------------
        public bool Update(float deltaTime, float newDir)
        {
            float deltaAngle = newDir - _curDiretion;
            if (deltaAngle == 0.0f)
            {
                return false;
            }
            EbAngle.Normalize(ref deltaAngle);
            float maxRot = 0.0f;
            if (deltaAngle > 0.0f)
            {
                _currentSpeed = Math.Abs(_currentSpeed);
                float newSpd = _currentSpeed + _accelerate * deltaTime;
                float maxSpd = EbMath.Sqrt(deltaAngle * _accelerate * 2.0f);
                _currentSpeed = EbMath.Min(EbMath.Min(newSpd, maxSpd), _maxSpeed);
                maxRot = _currentSpeed * deltaTime;
            }
            else
            {
                _currentSpeed = -Math.Abs(_currentSpeed);
                float newSpd = _currentSpeed - _accelerate * deltaTime;
                float maxSpd = -EbMath.Sqrt(-deltaAngle * _accelerate * 2.0f);
                _currentSpeed = EbMath.Max(EbMath.Max(newSpd, maxSpd), -_maxSpeed);
                maxRot = _currentSpeed * deltaTime;
            }
            if (Math.Abs(deltaAngle) < Math.Abs(maxRot))
            {
                _curDiretion = newDir;
                _currentSpeed = 0.0f;
            }
            else
            {
                _curDiretion += maxRot;
            }
            EbAngle.Normalize(ref _curDiretion);
            return true;
        }

        //---------------------------------------------------------------------
        public void SetSample(float distance, float duration)
        {
            _accelerate = 2.0f * distance / (duration * duration);
            _maxSpeed = _accelerate * duration;
        }
    }
}
