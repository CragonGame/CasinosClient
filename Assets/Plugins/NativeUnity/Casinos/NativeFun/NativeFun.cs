using UnityEngine;
using System.Collections;

//定义模板
//#if UNITY_EDITOR || UNITY_STANDALONE
//#elif UNITY_ANDROID
//#elif UNITY_IPHONE || UNITY_IOS
//#endif

public class NativeFun
{
#if UNITY_EDITOR || UNITY_STANDALONE
    //-------------------------------------------------------------------------
    public NativeFun()
    {
    }

    //-------------------------------------------------------------------------
    public string getCountryCode()
    {
        return "CN";
    }

    //-------------------------------------------------------------------------
    public void installAPK(string file_path)
    {
    }

#elif UNITY_ANDROID
    //-------------------------------------------------------------------------
    public AndroidJavaClass mAndoridJavaClassNativeFun;
    public AndroidJavaObject mAndroidFun;

    //-------------------------------------------------------------------------
    public NativeFun()
    {
        mAndoridJavaClassNativeFun = new AndroidJavaClass("com.Native.Native.NativeFun");
        if (mAndroidFun == null)
        {
            mAndroidFun = mAndoridJavaClassNativeFun.CallStatic<AndroidJavaObject>("Instantce");
        }
    }

    //-------------------------------------------------------------------------
    public string getCountryCode()
    {
        string code = null;
        if (mAndroidFun != null)
        {
            code = mAndroidFun.CallStatic<string>("getCountryZipCode");
        }
        return code;
    }

    //-------------------------------------------------------------------------
    public void installAPK(string file_path)
    {
        if (mAndroidFun != null)
        {
            mAndroidFun.CallStatic("installAPK", file_path);
        }
    }
#elif UNITY_IPHONE || UNITY_IOS
    
    //-------------------------------------------------------------------------
    public NativeFun()
    {
    }

    //-------------------------------------------------------------------------
    public string getCountryCode()
    {        
        return getCountryCodeIos();
    }

    //-------------------------------------------------------------------------
    public void installAPK(string file_path)
    {      
    }

    #region DllImport
        //-------------------------------------------------------------------------
        [DllImport("__Internal")]
        private static extern string getCountryCodeIos();
    #endregion

#endif
}