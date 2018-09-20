using UnityEngine;
using System.Collections;
using OnePF;
using System.Collections.Generic;
using System;

public class OpenInstall
{
    //-------------------------------------------------------------------------
    static OpenInstall mOpenInstall;
    static IOpenInstall mIOpenInstall;

    //-------------------------------------------------------------------------
    public OpenInstall()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        mIOpenInstall = new AndroidOpenInstall();        
#elif UNITY_IOS
        mIOpenInstall = new IOSOpenInstall();
#else
        //Debug.LogError("Do not supported on this platform. ");
#endif
    }

    //-------------------------------------------------------------------------
    public static OpenInstall Instant()
    {
        if (mOpenInstall == null)
        {
            mOpenInstall = new OpenInstall();
        }

        return mOpenInstall;
    }

    //-------------------------------------------------------------------------
    public static void GetWakeUp()
    {
        if (mIOpenInstall != null)
        {
            mIOpenInstall.GetWakeUp();
        }        
    }
}