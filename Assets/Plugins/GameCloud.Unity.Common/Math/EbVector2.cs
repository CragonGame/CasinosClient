// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;
    
    [Serializable]
    public struct EbVector2 : IEquatable<EbVector2>
    {
        //---------------------------------------------------------------------
        public float x;// { get; set; }// Gets or sets the X value.
        public float y;// { get; set; }// Gets or sets Y value.
        public float Length { get { return (float)Math.Sqrt((x * x) + (y * y)); } }
        public float Length2 { get { return ((x * x) + (y * y)); } }

        //---------------------------------------------------------------------
        public EbVector2(float x, float y)
            : this()
        {
            this.x = x;
            this.y = y;
        }

        //---------------------------------------------------------------------
        public static EbVector2 Zero
        {
            get
            {
                EbVector2 v;// = new EbVector2();
                v.x = 0.0f;
                v.y = 0.0f;
                return v;
            }
        }

        //---------------------------------------------------------------------
        public static EbVector2 UnitX
        {
            get
            {
                EbVector2 v;// = new EbVector2();
                v.x = 1.0f;
                v.y = 0.0f;
                return v;
            }
        }

        //---------------------------------------------------------------------
        public static EbVector2 UnitY
        {
            get
            {
                EbVector2 v;// = new EbVector2();
                v.x = 0.0f;
                v.y = 1.0f;
                return v;
            }
        }

        //---------------------------------------------------------------------
        public static EbVector2 Unit
        {
            get
            {
                EbVector2 v;// = new EbVector2();
                v.x = 1.0f;
                v.y = 1.0f;
                return v;
            }
        }

        //---------------------------------------------------------------------
        public bool Equals(EbVector2 other)
        {
            return this.x.Equals(other.x) && this.y.Equals(other.y);
        }

        //---------------------------------------------------------------------
        public override bool Equals(object obj)
        {
            if (obj is EbVector2)
            {
                var other = (EbVector2)obj;
                return this.Equals(other);
            }

            return false;
        }

        //---------------------------------------------------------------------
        public override int GetHashCode()
        {
            int result = this.x.GetHashCode();
            result ^= this.y.GetHashCode();
            return result;
        }

        //---------------------------------------------------------------------
        public override string ToString()
        {
            return string.Format("{0}({1},{2})", base.ToString(), this.x, this.y);
        }

        //---------------------------------------------------------------------
        public static bool operator ==(EbVector2 coordinate1, EbVector2 coordinate2)
        {
            return coordinate1.Equals(coordinate2);
        }

        //---------------------------------------------------------------------
        public static bool operator !=(EbVector2 coordinate1, EbVector2 coordinate2)
        {
            return coordinate1.Equals(coordinate2) == false;
        }

        //---------------------------------------------------------------------
        public static EbVector2 operator +(EbVector2 a, EbVector2 b)
        {
            EbVector2 v;
            v.x = a.x + b.x;
            v.y = a.y + b.y;
            return v;

            //return new EbVector2 { x = a.x + b.x, y = a.y + b.y };
        }

        //---------------------------------------------------------------------
        public static EbVector2 operator -(EbVector2 a, EbVector2 b)
        {
            EbVector2 v;
            v.x = a.x - b.x;
            v.y = a.y - b.y;
            return v;

            //return new EbVector2 { x = a.x - b.x, y = a.y - b.y };
        }

        //---------------------------------------------------------------------
        public static EbVector2 operator -(EbVector2 a)
        {
            EbVector2 v;
            v.x = -a.x;
            v.y = -a.y;
            return v;

            //return new EbVector2 { x = -a.x, y = -a.y };
        }

        //---------------------------------------------------------------------
        public static EbVector2 operator *(EbVector2 a, int b)
        {
            EbVector2 v;
            v.x = a.x * b;
            v.y = a.y * b;
            return v;

            //return new EbVector2 { x = a.x * b, y = a.y * b };
        }

        //---------------------------------------------------------------------
        public static EbVector2 operator *(EbVector2 a, float b)
        {
            EbVector2 v;
            v.x = a.x * b;
            v.y = a.y * b;
            return v;

            //return new EbVector2 { x = a.x * b, y = a.y * b };
        }

        //---------------------------------------------------------------------
        public static EbVector2 operator /(EbVector2 a, int b)
        {
            EbVector2 v;
            v.x = a.x / b;
            v.y = a.y / b;
            return v;

            //return new EbVector2 { x = a.x / b, y = a.y / b };
        }

        //---------------------------------------------------------------------
        public static EbVector2 operator /(EbVector2 a, float b)
        {
            EbVector2 v;
            v.x = a.x / b;
            v.y = a.y / b;
            return v;

            //return new EbVector2 { x = a.x / b, y = a.y / b };
        }

        //---------------------------------------------------------------------
        public static EbVector2 max(EbVector2 value1, EbVector2 value2)
        {
            EbVector2 v;
            v.x = Math.Max(value1.x, value2.x);
            v.y = Math.Max(value1.y, value2.y);
            return v;

            //return new EbVector2 { x = Math.Max(value1.x, value2.x), y = Math.Max(value1.y, value2.y) };
        }

        //---------------------------------------------------------------------
        public static EbVector2 min(EbVector2 value1, EbVector2 value2)
        {
            EbVector2 v;
            v.x = Math.Min(value1.x, value2.x);
            v.y = Math.Min(value1.y, value2.y);
            return v;

            //return new EbVector2 { x = Math.Min(value1.x, value2.x), y = Math.Min(value1.y, value2.y) };
        }

        //---------------------------------------------------------------------
        public static EbVector2 lerp(EbVector2 from, EbVector2 to, float t)
        {
            EbVector2 v = from;
            v += (to - from) * t;
            return v;
        }

        //---------------------------------------------------------------------
        public static float dot(EbVector2 lhs, EbVector2 rhs)
        {
            return ((lhs.x * rhs.x) + (lhs.y * rhs.y));
        }

        //---------------------------------------------------------------------
        public float getDistance(EbVector2 vector)
        {
            return (float)Math.Sqrt(Math.Pow(vector.x - this.x, 2) + Math.Pow(vector.y - this.y, 2));
        }

        //---------------------------------------------------------------------
        public static float magnitude(EbVector2 a)
        {
            return (float)Math.Sqrt(((a.x * a.x) + (a.y * a.y)));
        }

        //---------------------------------------------------------------------
        public void normalize()
        {
            float num = magnitude(this);
            if (num > 1E-05f)
            {
                this = (EbVector2)(this / num);
            }
            else
            {
                this = Zero;
            }
        }

        //---------------------------------------------------------------------
        public static EbVector2 normalize(EbVector2 value)
        {
            float num = magnitude(value);
            if (num > 1E-05f)
            {
                return (EbVector2)(value / num);
            }
            return Zero;
        }

        //---------------------------------------------------------------------
        public EbVector2 normalized
        {
            get
            {
                return normalize(this);
            }
        }
    }
}
