// Copyright (c) Cragon. All rights reserved.

namespace GameCloud.Unity.Common
{
    using System;

    // The bounding box is a 2d or 3d shape.
    public struct EbBoundingBox : IEquatable<EbBoundingBox>
    {
        //---------------------------------------------------------------------
        public EbVector3 Max { get; set; }// Gets or sets the Max coordinate.
        public EbVector3 Min { get; set; }// Gets or sets the Min coordinate.

        //---------------------------------------------------------------------
        // Compares this instance to another bounding box.
        public bool Equals(EbBoundingBox other)
        {
            return (this.Min == other.Min) && (this.Max == other.Max);
        }

        //---------------------------------------------------------------------
        public override bool Equals(object obj)
        {
            return (obj is EbBoundingBox) ? this.Equals((EbBoundingBox)obj) : false;
        }

        public override string ToString()
        {
            return string.Format("[min:({0},{1}),max:({2},{3})]", Min.x, Min.z, Max.x, Max.z);
        }

        //---------------------------------------------------------------------
        public override int GetHashCode()
        {
            return this.Min.GetHashCode() + this.Max.GetHashCode();
        }

        //---------------------------------------------------------------------
        public static bool operator ==(EbBoundingBox a, EbBoundingBox b)
        {
            return a.Equals(b);
        }

        //---------------------------------------------------------------------
        public static bool operator !=(EbBoundingBox a, EbBoundingBox b)
        {
            return !a.Equals(b);
        }

        //---------------------------------------------------------------------
        public static EbBoundingBox createFromPoints(params EbVector3[] points)
        {
            if (points == null)
            {
                throw new ArgumentNullException("points");
            }

            if (points.Length == 0)
            {
                throw new ArgumentException("points");
            }

            EbVector3 min = points[0];
            EbVector3 max = points[0];
            for (int index = 1; index < points.Length; index++)
            {
                EbVector3 tmp = points[index];
                if (tmp.x < min.x)
                {
                    min.x = tmp.x;
                }

                if (tmp.y < min.y)
                {
                    min.y = tmp.y;
                }

                if (tmp.z < min.z)
                {
                    min.z = tmp.z;
                }

                if (tmp.x > max.x)
                {
                    max.x = tmp.x;
                }

                if (tmp.y > max.y)
                {
                    max.y = tmp.y;
                }

                if (tmp.z > max.z)
                {
                    max.z = tmp.z;
                }
            }

            return new EbBoundingBox { Min = min, Max = max };
        }

        //---------------------------------------------------------------------
        public bool contains(EbVector3 point)
        {
            // not outside of box?
            return (point.x < this.Min.x || point.x > this.Max.x || point.y < this.Min.y || point.y > this.Max.y
                || point.z < this.Min.z || point.z > this.Max.z) == false;
        }

        //---------------------------------------------------------------------
        // Gets all 4 corners of a 2D bounding box with the minimum Z.
        public EbVector3[] getCorners2D()
        {
            return new[]
                {
                    new EbVector3 { x = this.Min.x, y = this.Min.y, z = this.Min.z }, new EbVector3 { x = this.Max.x, y = this.Min.y, z = this.Min.z },
                    new EbVector3 { x = this.Min.x, y = this.Max.y, z = this.Min.z }, new EbVector3 { x = this.Max.x, y = this.Max.y, z = this.Min.z }
                };
        }

        //---------------------------------------------------------------------
        // Gets all 8 corners of a 3D bounding box.
        public EbVector3[] getCorners3D()
        {
            return new[]
                {
                    new EbVector3 { x = this.Min.x, y = this.Min.y, z = this.Min.z }, new EbVector3 { x = this.Max.x, y = this.Min.y, z = this.Min.z },
                    new EbVector3 { x = this.Min.x, y = this.Max.y, z = this.Min.z }, new EbVector3 { x = this.Max.x, y = this.Max.y, z = this.Min.z },
                    new EbVector3 { x = this.Min.x, y = this.Min.y, z = this.Max.z }, new EbVector3 { x = this.Max.x, y = this.Min.y, z = this.Max.z },
                    new EbVector3 { x = this.Min.x, y = this.Max.y, z = this.Max.z }, new EbVector3 { x = this.Max.x, y = this.Max.y, z = this.Max.z },
                };
        }

        //---------------------------------------------------------------------
        // Intersects this instance with another bounding box.
        public EbBoundingBox intersectWith(EbBoundingBox other)
        {
            return new EbBoundingBox { Min = EbVector3.max(this.Min, other.Min), Max = EbVector3.min(this.Max, other.Max) };
        }

        //---------------------------------------------------------------------
        // Checks whether <see cref = "Max" /> and <see cref = "Min" /> span a valid (positive) area.
        public bool isValid()
        {
            return (this.Max.x < this.Min.x || this.Max.y < this.Min.y || this.Max.z < this.Min.z) == false;
        }

        //---------------------------------------------------------------------
        // Unites two bounding boxes.
        public EbBoundingBox unionWith(EbBoundingBox other)
        {
            return new EbBoundingBox { Min = EbVector3.min(this.Min, other.Min), Max = EbVector3.max(this.Max, other.Max) };
        }
    }
}
