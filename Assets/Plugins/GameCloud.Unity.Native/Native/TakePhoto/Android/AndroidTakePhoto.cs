using UnityEngine;
using System.Collections;

public class AndroidTakePhoto : INativeTakePhoto
{
    public AndroidJavaClass mAndoridJavaClassTakePhoto;
    public AndroidJavaObject mAndroidTakePhoto;

    public AndroidTakePhoto()
    {
        mAndoridJavaClassTakePhoto = new AndroidJavaClass("com.TakePhoto.TakePhoto.TakePhoto");
    }

    //-------------------------------------------------------------------------
    public void takeNewPhoto(int photo_width, int photo_height, string photo_name, string photo_finalpath)
    {
        if (mAndroidTakePhoto == null)
        {
            mAndroidTakePhoto = mAndoridJavaClassTakePhoto.CallStatic<AndroidJavaObject>("Instantce",
                photo_width, photo_height, "getPicSuccess", "getPicFail", photo_name, photo_finalpath, "NativeReceiver");
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
            mAndroidTakePhoto = mAndoridJavaClassTakePhoto.CallStatic<AndroidJavaObject>("Instantce",
                photo_width, photo_height, "getPicSuccess", "getPicFail", photo_name, photo_finalpath, "NativeReceiver");
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
}

//-------------------------------------------------------------------------
public enum _eAndroidTakePhoto
{
    takeNewPhoto,
    takeExistPhoto,
}
