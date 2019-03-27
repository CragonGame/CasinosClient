using UnityEngine;
using System.Collections;

public enum _eAndroidTakePhoto
{
    takeNewPhoto,
    takeExistPhoto,
}

public class NativeTakePhoto
{

#if UNITY_EDITOR || UNITY_STANDALONE
    //-------------------------------------------------------------------------
    public void takeNewPhoto(int photo_width, int photo_height, string photo_name, string photo_finalpath)
    {

    }

    //-------------------------------------------------------------------------
    public void takeExistPhoto(int photo_width, int photo_height, string photo_name, string photo_finalpath)
    {
    }

#elif UNITY_ANDROID
    //-------------------------------------------------------------------------
    public AndroidJavaClass mAndoridJavaClassTakePhoto;
    public AndroidJavaObject mAndroidTakePhoto;

    //-------------------------------------------------------------------------
    public NativeTakePhoto()
    {
        mAndoridJavaClassTakePhoto = new AndroidJavaClass("com.TakePhoto.TakePhoto.TakePhoto");
    }

    //-------------------------------------------------------------------------
    public void takeNewPhoto(int photo_width, int photo_height, string photo_name, string photo_finalpath)
    {
        if (mAndroidTakePhoto == null)
        {
            mAndroidTakePhoto = mAndoridJavaClassTakePhoto.CallStatic<AndroidJavaObject>("Instantce", photo_width, photo_height,photo_name, photo_finalpath);
        }
        if (mAndroidTakePhoto != null)
        {
            mAndroidTakePhoto.Call(_eAndroidTakePhoto.takeNewPhoto.ToString());
        }
        else
        {
            Debug.LogError("TakePhoto Is Null");
        }
    }

    //-------------------------------------------------------------------------
    public void takeExistPhoto(int photo_width, int photo_height, string photo_name, string photo_finalpath)
    {
        if (mAndroidTakePhoto == null)
        {
            mAndroidTakePhoto = mAndoridJavaClassTakePhoto.CallStatic<AndroidJavaObject>("Instantce", photo_width, photo_height, photo_name, photo_finalpath);
        }
        if (mAndroidTakePhoto != null)
        {
            mAndroidTakePhoto.Call(_eAndroidTakePhoto.takeExistPhoto.ToString());
        }
        else
        {
            Debug.LogError("TakePhoto Is Null");
        }
    }

#elif UNITY_IPHONE || UNITY_IOS

    //-------------------------------------------------------------------------
    public void takeNewPhoto(int photo_width, int photo_height, string photo_name, string photo_finalpath)
    {
        takeNewPhoto_ios("NativeReceiver", "getPicSuccess",
            "getPicFail", photo_width, photo_height, photo_name);
    }

    //-------------------------------------------------------------------------
    public void takeExistPhoto(int photo_width, int photo_height, string photo_name, string photo_finalpath)
    {
        takeExistPhoto_ios("NativeReceiver", "getPicSuccess",
            "getPicFail", photo_width, photo_height, photo_name);
    }

    //-------------------------------------------------------------------------
#region DllImport
    [DllImport("__Internal")]
    private static extern void takeNewPhoto_ios(string msg_recivername, string take_photosuccessmsgname,
       string take_photofailmsgname, int photo_width, int photo_height, string store_photopath);
    [DllImport("__Internal")]
    private static extern void takeExistPhoto_ios(string msg_recivername, string take_photosuccessmsgname,
        string take_photofailmsgname, int photo_width, int photo_height, string store_photopath);
#endregion

#endif
}