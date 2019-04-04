using UnityEngine;
using System.Collections;

//定义模板
//#if UNITY_EDITOR || UNITY_STANDALONE
//#elif UNITY_ANDROID
//#elif UNITY_IPHONE || UNITY_IOS
//#endif

public enum NativeOperateType
{
    Pay,
    Login,
}

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

    //-------------------------------------------------------------------------
    // 获取粘贴板的值
    public string GetValue()
    {
        return "";
    }

    //-------------------------------------------------------------------------
    // 设置粘贴板的值
    public void SetValue(string text)
    {
        
    }

    //-------------------------------------------------------------------------
    public void nativeOperate(NativeOperateType operate_type)
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

    //-------------------------------------------------------------------------
    // 获取粘贴板的值
    public string GetValue()
    {
        return mAndroidFun.CallStatic<string>("GetClipBoard");
    }

    //-------------------------------------------------------------------------
    // 设置粘贴板的值
    public void SetValue(string text)
    {
        mAndroidFun.CallStatic("SetClipBoard", text);
    }

    //-------------------------------------------------------------------------
    public void nativeOperate(NativeOperateType operate_type)
    {
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

    //-------------------------------------------------------------------------
    public void nativeOperate(NativeOperateType operate_type)
    {
        nativeOperate(operate_type.ToString());
    }

    #region DllImport
        //-------------------------------------------------------------------------
        [DllImport ("__Internal")]
        private static extern string getCountryCodeIos();

        //-------------------------------------------------------------------------
        [DllImport ("__Internal")]
        private static extern void nativeOperate(string operate_type);
    #endregion

#endif
}