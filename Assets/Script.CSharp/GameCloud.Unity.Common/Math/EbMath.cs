// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;

    public static class EbMath
    {
        //---------------------------------------------------------------------
        public static readonly float PI = 3.141593f;
        public static readonly float PI_X2 = PI * 2;
        public static readonly float PI_X3 = PI * 3;
        public static readonly float PI_X4 = PI * 4;
        public static readonly float PI_HALF = PI * 0.5f;
        public static readonly float Infinity = float.PositiveInfinity;
        public static readonly float NegativeInfinity = float.NegativeInfinity;
        public static readonly float Deg2Rad = 0.01745329f;
        public static readonly float Rad2Deg = 57.29578f;
        public static readonly float Epsilon = float.Epsilon;

        //---------------------------------------------------------------------
        public static float Sin(float f)
        {
            return (float)Math.Sin((double)f);
        }

        //---------------------------------------------------------------------
        public static float Cos(float f)
        {
            return (float)Math.Cos((double)f);
        }

        //---------------------------------------------------------------------
        public static float Tan(float f)
        {
            return (float)Math.Tan((double)f);
        }

        //---------------------------------------------------------------------
        public static float Asin(float f)
        {
            return (float)Math.Asin((double)f);
        }

        //---------------------------------------------------------------------
        public static float Acos(float f)
        {
            return (float)Math.Acos((double)f);
        }

        //---------------------------------------------------------------------
        public static float Atan(float f)
        {
            return (float)Math.Atan((double)f);
        }

        //---------------------------------------------------------------------
        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2((double)y, (double)x);
        }

        //---------------------------------------------------------------------
        public static float Sqrt(float f)
        {
            return (float)Math.Sqrt((double)f);
        }

        //---------------------------------------------------------------------
        public static int IntInf(float value)
        {
            if (value >= 0)
            {
                return (int)value;
            }
            else
            {
                return (int)(value - 0.999f);
            }
        }

        //---------------------------------------------------------------------
        public static int IntSup(float value)
        {
            if (value >= 0)
            {
                return (int)(value + 0.999f);
            }
            else
            {
                return (int)value;
            }
        }

        //---------------------------------------------------------------------
        public static int IntNear(float value)
        {
            return (int)(value + 0.5f);
        }

        //---------------------------------------------------------------------
        public static float Abs(float value)
        {
            return Math.Abs(value);
        }

        //---------------------------------------------------------------------
        //public static Int8 Abs(Int8 value)
        //{
        //    return Math.Abs(value);
        //}

        //---------------------------------------------------------------------
        public static short Abs(short value)
        {
            return Math.Abs(value);
        }

        //---------------------------------------------------------------------
        public static int Abs(int value)
        {
            return Math.Abs(value);
        }

        //---------------------------------------------------------------------
        public static long Abs(long value)
        {
            return Math.Abs(value);
        }

        //---------------------------------------------------------------------
        public static float Min(float a, float b)
        {
            return ((a >= b) ? b : a);
        }

        //---------------------------------------------------------------------
        //public static Int8 Min(Int8 a, Int8 b)
        //{
        //    return ((a >= b) ? b : a);
        //}

        //---------------------------------------------------------------------
        //public static UInt8 Min(UInt8 a, UInt8 b)
        //{
        //    return ((a >= b) ? b : a);
        //}

        //---------------------------------------------------------------------
        public static short Min(short a, short b)
        {
            return ((a >= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static ushort Min(ushort a, ushort b)
        {
            return ((a >= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static int Min(int a, int b)
        {
            return ((a >= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static uint Min(uint a, uint b)
        {
            return ((a >= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static long Min(long a, long b)
        {
            return ((a >= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static ulong Min(ulong a, ulong b)
        {
            return ((a >= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static float Min(params float[] values)
        {
            int length = values.Length;
            if (length == 0)
            {
                return 0f;
            }
            float num2 = values[0];
            for (int i = 1; i < length; i++)
            {
                if (values[i] < num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        //---------------------------------------------------------------------
        public static int Min(params int[] values)
        {
            int length = values.Length;
            if (length == 0)
            {
                return 0;
            }
            int num2 = values[0];
            for (int i = 1; i < length; i++)
            {
                if (values[i] < num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        //---------------------------------------------------------------------
        public static float Max(float a, float b)
        {
            return ((a <= b) ? b : a);
        }

        //---------------------------------------------------------------------
        //public static Int8 Max(Int8 a, Int8 b)
        //{
        //    return ((a <= b) ? b : a);
        //}

        //---------------------------------------------------------------------
        //public static UInt8 Max(UInt8 a, UInt8 b)
        //{
        //    return ((a <= b) ? b : a);
        //}

        //---------------------------------------------------------------------
        public static short Max(short a, short b)
        {
            return ((a <= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static ushort Max(ushort a, ushort b)
        {
            return ((a <= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static int Max(int a, int b)
        {
            return ((a <= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static uint Max(uint a, uint b)
        {
            return ((a <= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static long Max(long a, long b)
        {
            return ((a <= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static ulong Max(ulong a, ulong b)
        {
            return ((a <= b) ? b : a);
        }

        //---------------------------------------------------------------------
        public static float Max(params float[] values)
        {
            int length = values.Length;
            if (length == 0)
            {
                return 0f;
            }
            float num2 = values[0];
            for (int i = 1; i < length; i++)
            {
                if (values[i] > num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        //---------------------------------------------------------------------
        public static int Max(params int[] values)
        {
            int length = values.Length;
            if (length == 0)
            {
                return 0;
            }
            int num2 = values[0];
            for (int i = 1; i < length; i++)
            {
                if (values[i] > num2)
                {
                    num2 = values[i];
                }
            }
            return num2;
        }

        //---------------------------------------------------------------------
        public static float Pow(float f, float p)
        {
            return (float)Math.Pow((double)f, (double)p);
        }

        //---------------------------------------------------------------------
        public static float Exp(float power)
        {
            return (float)Math.Exp((double)power);
        }

        //---------------------------------------------------------------------
        public static float Log(float f, float p)
        {
            return (float)Math.Log((double)f, (double)p);
        }

        //---------------------------------------------------------------------
        public static float Log(float f)
        {
            return (float)Math.Log((double)f);
        }

        //---------------------------------------------------------------------
        public static float Log10(float f)
        {
            return (float)Math.Log10((double)f);
        }

        //---------------------------------------------------------------------
        public static float Ceil(float f)
        {
            return (float)Math.Ceiling((double)f);
        }

        //---------------------------------------------------------------------
        public static float Floor(float f)
        {
            return (float)Math.Floor((double)f);
        }

        //---------------------------------------------------------------------
        public static float Round(float f)
        {
            return (float)Math.Round((double)f);
        }

        //---------------------------------------------------------------------
        public static int CeilToInt(float f)
        {
            return (int)Math.Ceiling((double)f);
        }

        //---------------------------------------------------------------------
        public static int FloorToInt(float f)
        {
            return (int)Math.Floor((double)f);
        }

        //---------------------------------------------------------------------
        public static int RoundToInt(float f)
        {
            return (int)Math.Round((double)f);
        }

        //---------------------------------------------------------------------
        public static float Sign(float f)
        {
            return ((f < 0f) ? -1f : 1f);
        }

        //---------------------------------------------------------------------
        public static float Radius2Degree(float radius)
        {
            return 180.0f - radius * Rad2Deg;
        }

        //---------------------------------------------------------------------
        public static float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                value = min;
                return value;
            }
            if (value > max)
            {
                value = max;
            }
            return value;
        }

        //---------------------------------------------------------------------
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                value = min;
                return value;
            }
            if (value > max)
            {
                value = max;
            }
            return value;
        }

        //---------------------------------------------------------------------
        public static float Clamp01(float value)
        {
            if (value < 0f)
            {
                return 0f;
            }
            if (value > 1f)
            {
                return 1f;
            }
            return value;
        }

        //---------------------------------------------------------------------
        public static float Lerp(float from, float to, float t)
        {
            return (from + ((to - from) * Clamp01(t)));
        }

        //---------------------------------------------------------------------
        public static float Wrap(float val, float low, float high)// 取值范围[low, high)
        {
            float ret = (val);
            float rang = (high - low);

            while (ret >= high)
            {
                ret -= rang;
            }
            while (ret < low)
            {
                ret += rang;
            }
            return ret;
        }

        //---------------------------------------------------------------------
        public static int Wrap(int val, int low, int high)// 取值范围[low, high)
        {
            int ret = (val);
            int rang = (high - low);

            while (ret >= high)
            {
                ret -= rang;
            }
            while (ret < low)
            {
                ret += rang;
            }
            return ret;
        }

        //---------------------------------------------------------------------
        public static float MoveTowards(float current, float target, float maxDelta)
        {
            if (Abs((float)(target - current)) <= maxDelta)
            {
                return target;
            }
            return (current + (Sign(target - current) * maxDelta));
        }

        //---------------------------------------------------------------------
        //public static float MoveTowardsAngle(float current, float target, float maxDelta)
        //{
        //    target = current + DeltaAngle(current, target);
        //    return MoveTowards(current, target, maxDelta);
        //}

        //---------------------------------------------------------------------
        public static float SmoothStep(float from, float to, float t)
        {
            t = Clamp01(t);
            t = (((-2f * t) * t) * t) + ((3f * t) * t);
            return ((to * t) + (from * (1f - t)));
        }


        //---------------------------------------------------------------------
        public static float Gamma(float value, float absmax, float gamma)
        {
            bool flag = false;
            if (value < 0f)
            {
                flag = true;
            }
            float num = Abs(value);
            if (num > absmax)
            {
                return (!flag ? num : -num);
            }
            float num2 = Pow(num / absmax, gamma) * absmax;
            return (!flag ? num2 : -num2);
        }

        //---------------------------------------------------------------------
        public static bool Approximately(float a, float b)
        {
            return (Abs((float)(b - a)) < Max((float)(1E-06f * Max(Abs(a), Abs(b))), (float)1.121039E-44f));
        }

        //---------------------------------------------------------------------
        public static float Repeat(float t, float length)
        {
            return (t - (Floor(t / length) * length));
        }

        //---------------------------------------------------------------------
        public static float PingPong(float t, float length)
        {
            t = Repeat(t, length * 2f);
            return (length - Abs((float)(t - length)));
        }

        //---------------------------------------------------------------------
        public static float InverseLerp(float from, float to, float value)
        {
            if (from < to)
            {
                if (value < from)
                {
                    return 0f;
                }
                if (value > to)
                {
                    return 1f;
                }
                value -= from;
                value /= to - from;
                return value;
            }
            if (from <= to)
            {
                return 0f;
            }
            if (value < to)
            {
                return 1f;
            }
            if (value > from)
            {
                return 0f;
            }
            return (1f - ((value - to) / (from - to)));
        }
    }

    public class EbMath2D
    {
        //---------------------------------------------------------------------
        public static readonly float FRONT_X = 0.0f;
        public static readonly float FRONT_Y = -1.0f;

        //---------------------------------------------------------------------
        public static float Length(float x, float y)
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        //---------------------------------------------------------------------
        public static float Length2(float x, float y)
        {
            return (x * x + y * y);
        }

        //---------------------------------------------------------------------
        public static float Length(float fSrcX, float fSrcY, float fDesX, float fDesY)
        {
            float deltaX = fDesX - fSrcX;
            float deltaY = fDesY - fSrcY;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        //---------------------------------------------------------------------
        public static float Length2(float fSrcX, float fSrcY, float fDesX, float fDesY)
        {
            float deltaX = fDesX - fSrcX;
            float deltaY = fDesY - fSrcY;
            return (deltaX * deltaX + deltaY * deltaY);
        }

        //---------------------------------------------------------------------
        public static float GetAngle(float fX, float fY)
        {
            float angle = (float)Math.Atan2(fX, -fY);
            return angle;
        }

        //---------------------------------------------------------------------
        public static void CaculateAngle(float fAngle, ref float fX, ref float fY)
        {
            fY = -(float)Math.Cos(fAngle);
            fX = (float)Math.Sin(fAngle);
        }

        //---------------------------------------------------------------------
        public static float GetRotateAngle(float fSrcX, float fSrcy, float fDesX, float fDesY)
        {
            float fDesAngle = GetAngle(fDesX, fDesY);
            float fSrcAngle = GetAngle(fSrcX, fSrcy);
            float fRotateAngle = fDesAngle - fSrcAngle;
            // 映射到(-M_PI, M_PI)区间上来
            if (fRotateAngle > EbMath.PI) fRotateAngle -= EbMath.PI_X2;
            else if (fRotateAngle < -EbMath.PI) fRotateAngle += EbMath.PI_X2;
            return fRotateAngle;
        }

        //---------------------------------------------------------------------
        public static void Rotate(float fSrcX, float fSrcy, float fRotateAngle, ref float fDesX, ref float fDesY)
        {
            // 逆时针旋转
            float fSin = (float)Math.Sin(fRotateAngle);
            float fCon = (float)Math.Cos(fRotateAngle);
            // 顺时针旋转
            //float fSin = sin(-fRotateAngle);
            //float fCon = cos(-fRotateAngle);
            fDesX = fCon * fSrcX - fSin * fSrcy;
            fDesY = fSin * fSrcX + fCon * fSrcy;
        }

        //---------------------------------------------------------------------
        public static void Rotate(ref float fX, ref float fY, float fRotateAngle)
        {
            float fSin = (float)Math.Sin(fRotateAngle);
            float fCon = (float)Math.Cos(fRotateAngle);
            // 顺时针旋转
            //float fSin = sin(-fRotateAngle);
            //float fCon = cos(-fRotateAngle);
            float fDesX = fCon * fX - fSin * fY;
            float fDesY = fSin * fX + fCon * fY;
            fX = fDesX;
            fY = fDesY;
        }

        //---------------------------------------------------------------------
        public static void RotateRight90(ref float x, ref float y)
        {
            float temp = x;
            x = y;
            y = -temp;
        }

        //---------------------------------------------------------------------
        public static void RotateLeft90(ref float x, ref float y)
        {
            float temp = x;
            x = -y;
            y = temp;
        }

        //---------------------------------------------------------------------
        public static int GetSide(float fromX, float fromY, float toX, float toY, float x, float y)
        {
            float s = (fromX - x) * (toY - y) - (fromY - y) * (toX - x);
            if (s == 0)
            {
                return 0;
            }
            else if (s < 0)// 右侧
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}