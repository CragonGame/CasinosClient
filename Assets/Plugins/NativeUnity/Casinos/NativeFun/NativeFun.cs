using UnityEngine;
using System.Collections;

public class NativeFun
{
    //-------------------------------------------------------------------------
    static INativeFun mINativeFun;

    //-------------------------------------------------------------------------
    static NativeFun()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        mINativeFun = new AndroidFun();        
#elif UNITY_IOS  
		mINativeFun = new IOSFun(); 
#else
        Debug.LogError("Do not supported on this platform. ");
#endif
    }

    //-------------------------------------------------------------------------
    public static string getCountryCode()
    {
        return mINativeFun.getCountryCode();
    }

    //-------------------------------------------------------------------------
    public static void installAPK(string file_path)
    {
        mINativeFun.installAPK(file_path);
    }
}