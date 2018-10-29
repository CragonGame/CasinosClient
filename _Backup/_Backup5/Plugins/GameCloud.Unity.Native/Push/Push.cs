using UnityEngine;
using System.Collections;
using OnePF;
using System.Collections.Generic;
using System;

public class Push
{
    //-------------------------------------------------------------------------
    static Push mPush;
    static IPush mIPush;

    //-------------------------------------------------------------------------
    public Push()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        mIPush = new AndroidPush();        
#elif UNITY_IOS
        mIPush = new IOSPush();        
#else
        //Debug.LogError("Do not supported on this platform. ");
#endif
    }

    //-------------------------------------------------------------------------
    public static Push Instant()
    {
        if (mPush == null)
        {
            mPush = new Push();
        }

        return mPush;
    }

    //-------------------------------------------------------------------------
    public void initPush(string appId, string appKey, string appSecret)
    {
        mIPush.initPush(appId, appKey, appSecret);
    }
}