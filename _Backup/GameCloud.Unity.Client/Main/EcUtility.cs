using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using GameCloud.Unity.Common;

public static class EcUtility
{
    //---------------------------------------------------------------------
    public static Vector3 toVector3(this EbVector3 vector)
    {
        return new Vector3(vector.x, vector.y, vector.z);
    }

    //---------------------------------------------------------------------
    public static EbVector3 toEbVector3(this Vector3 vector)
    {
        return new EbVector3(vector.x, vector.y, vector.z);
    }

    //---------------------------------------------------------------------
    public static Vector2 toVector2(this EbVector2 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    //---------------------------------------------------------------------
    public static EbVector2 toEbVector2(this Vector2 vector)
    {
        return new EbVector2(vector.x, vector.y);
    }
}
