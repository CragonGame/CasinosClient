// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;

    public struct EbAngle
    {
        //---------------------------------------------------------------------
        public static readonly float INF = -EbMath.PI;
        public static readonly float SUP = EbMath.PI;
        public float _value;
        public float Value { get { return _value; } set { _value = value; Normalize(); } }
        public void SetValue(float value) { _value = value; Normalize(); }

        //---------------------------------------------------------------------
        public EbAngle(EbAngle rAngle)
        {
            _value = rAngle.Value;
        }

        //---------------------------------------------------------------------
        public EbAngle(float angle)
        {
            _value = angle;
            Normalize();
        }

        //---------------------------------------------------------------------
        public static void Normalize(ref float angle)
        {
            if (angle >= EbMath.PI)
            {
                if (angle > EbMath.PI_X3)
                {
                    angle = angle - (int)(angle / EbMath.PI_X2) * EbMath.PI_X2;
                }
                else
                {
                    angle = angle - EbMath.PI_X2;
                }
            }
            else if (angle < -EbMath.PI)
            {
                if (angle < -EbMath.PI_X3)
                {
                    angle = angle - (int)(angle / EbMath.PI_X2 - 1) * EbMath.PI_X2;
                }
                else
                {
                    angle = angle + EbMath.PI_X2;
                }
            }
        }

        //---------------------------------------------------------------------
        public static float Diff(EbAngle from, EbAngle to)
        {
            float fDiff = from.Value - to.Value;
            if (fDiff < -EbMath.PI)
            {
                return fDiff + EbMath.PI_X2;
            }
            else if (fDiff >= EbMath.PI)
            {
                return fDiff - EbMath.PI_X2;
            }
            else
            {
                return fDiff;
            }
        }

        //---------------------------------------------------------------------
        public static float SameSignAngle(EbAngle angle, EbAngle record)
        {
            if (angle.Value > record.Value + EbMath.PI)
            {
                return angle.Value - EbMath.PI_X2;
            }
            else if (angle.Value < record.Value - EbMath.PI)
            {
                return angle.Value + EbMath.PI_X2;
            }
            else
            {
                return angle.Value;
            }
        }

        //---------------------------------------------------------------------
        public static EbAngle operator +(EbAngle kAngle1, EbAngle kAngle2)
        {
            var v = kAngle1.Value + kAngle2.Value;
            EbAngle a;
            a._value = v;
            a.Normalize();
            return a;
        }

        //---------------------------------------------------------------------
        public static EbAngle operator -(EbAngle kAngle1, EbAngle kAngle2)
        {
            var v = kAngle1.Value - kAngle2.Value;
            EbAngle a;
            a._value = v;
            a.Normalize();
            return a;
        }

        //---------------------------------------------------------------------
        public bool IsBetween(EbAngle kLeft, EbAngle kRight)
        {
            if (kLeft.Value <= kRight.Value)
            {
                return (kLeft.Value <= Value && Value <= kRight.Value);
            }
            else
            {
                return (kLeft.Value <= Value || Value <= kRight.Value);
            }
        }

        //---------------------------------------------------------------------
        public void Lerp(EbAngle a, EbAngle b, float t)
        {
            _value = a.Value * t + SameSignAngle(a, b) * (1 - t);
            Normalize();
        }

        //---------------------------------------------------------------------
        void Normalize()
        {
            Normalize(ref _value);
        }
    }
}
