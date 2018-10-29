using UnityEngine;
using System.Collections;

public class AndroidFun : INativeFun
{
    public AndroidJavaClass mAndoridJavaClassTakePhoto;
    public AndroidJavaObject mAndroidFun;

    //-------------------------------------------------------------------------
    public AndroidFun()
    {
        mAndoridJavaClassTakePhoto = new AndroidJavaClass("com.Native.Native.NativeFun");
        if (mAndroidFun == null)
        {
            mAndroidFun = mAndoridJavaClassTakePhoto.CallStatic<AndroidJavaObject>("Instantce");
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
}