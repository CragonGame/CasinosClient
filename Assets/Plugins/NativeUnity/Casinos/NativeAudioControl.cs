using UnityEngine;
using System.Collections;

//-------------------------------------------------------------------------
public enum _eAndroidAudioControl
{
    setMusicSilence,
    cancelMusicSilence,
    getIsSilence,
    setMusicMax,
    cancelMusicMax,
    getIsMaxVolume,
    setCurrentMusicVolume,
    getCurrentMusicVolume,
}

public class NativeAudioControl
{

#if UNITY_EDITOR || UNITY_STANDALONE

    //-------------------------------------------------------------------------
    public NativeAudioControl()
    {
    }
    //-------------------------------------------------------------------------
    public void setMusicSilence()
    {
    }

    //-------------------------------------------------------------------------
    public void cancelMusicSilence()
    {

    }

    //-------------------------------------------------------------------------
    // 是否静音
    public int getIsSilence()
    {
        return 0;
    }

    //-------------------------------------------------------------------------
    public void setMusicMax()
    {
    }

    //-------------------------------------------------------------------------
    public void cancelMusicMax()
    {
    }

    //-------------------------------------------------------------------------
    public int getIsMaxVolume()
    {
        return 0;
    }

    //-------------------------------------------------------------------------
    public void setCurrentMusicVolume(float current_muiscvolume)
    {
    }

    //-------------------------------------------------------------------------
    public float getCurrentMusicVolume()
    {
        return 50;
    }

#elif UNITY_ANDROID
    public AndroidJavaClass mAndroidJavaClassAudioControl;
    public AndroidJavaObject mAndroidAudioControl;

    //-------------------------------------------------------------------------
    public NativeAudioControl()
    {
        mAndroidJavaClassAudioControl = new AndroidJavaClass("com.AudioController.AudioController.AudioController");
    }

    //-------------------------------------------------------------------------
    public void setMusicSilence()
    {
        _initMusicJavaObject();

        mAndroidAudioControl.Call(_eAndroidAudioControl.setMusicSilence.ToString());
    }

    //-------------------------------------------------------------------------
    public void cancelMusicSilence()
    {
        _initMusicJavaObject();

        mAndroidAudioControl.Call(_eAndroidAudioControl.cancelMusicSilence.ToString());
    }

    //-------------------------------------------------------------------------
    public int getIsSilence()
    {
        _initMusicJavaObject();

        return mAndroidAudioControl.Call<int>(_eAndroidAudioControl.getIsSilence.ToString());
    }

    //-------------------------------------------------------------------------
    public void setMusicMax()
    {
        _initMusicJavaObject();

        mAndroidAudioControl.Call(_eAndroidAudioControl.setMusicMax.ToString());
    }

    //-------------------------------------------------------------------------
    public void cancelMusicMax()
    {
        _initMusicJavaObject();

        mAndroidAudioControl.Call(_eAndroidAudioControl.cancelMusicMax.ToString());
    }

    //-------------------------------------------------------------------------
    public int getIsMaxVolume()
    {
        _initMusicJavaObject();

        return mAndroidAudioControl.Call<int>(_eAndroidAudioControl.getIsMaxVolume.ToString());
    }

    //-------------------------------------------------------------------------
    public void setCurrentMusicVolume(float current_muiscvolume)
    {
        _initMusicJavaObject();

        mAndroidAudioControl.Call(_eAndroidAudioControl.setCurrentMusicVolume.ToString(), current_muiscvolume);
    }

    //-------------------------------------------------------------------------
    public float getCurrentMusicVolume()
    {
        _initMusicJavaObject();

        return mAndroidAudioControl.Call<float>(_eAndroidAudioControl.getCurrentMusicVolume.ToString());
    }

    //-------------------------------------------------------------------------
    void _initMusicJavaObject()
    {
        if (mAndroidAudioControl == null)
        {
            mAndroidAudioControl = mAndroidJavaClassAudioControl.CallStatic<AndroidJavaObject>("Instantce");
        }
    }

#elif UNITY_IPHONE || UNITY_IOS
    //-------------------------------------------------------------------------
    public void setMusicSilence()
    {
        setMusicSilence_ios();
    }

    //-------------------------------------------------------------------------
    public void cancelMusicSilence()
    {
        cancelMusicSilence_ios();
    }

    //-------------------------------------------------------------------------
    public int getIsSilence()
    {
        return getIsSilence_ios();
    }

    //-------------------------------------------------------------------------
    public void setMusicMax()
    {
        setMusicMax_ios();
    }

    //-------------------------------------------------------------------------
    public void cancelMusicMax()
    {
        cancelMusicMax_ios();
    }

    //-------------------------------------------------------------------------
    public int getIsMaxVolume()
    {
        return getIsMaxVolume_ios();
    }

    //-------------------------------------------------------------------------
    public void setCurrentMusicVolume(float current_muiscvolume)
    {
        setCurrentMusicVolum_ios(current_muiscvolume);
    }

    //-------------------------------------------------------------------------
    public float getCurrentMusicVolume()
    {
        return getCurrentMusicVolume_ios();
    }

    //-------------------------------------------------------------------------
    #region DllImport
    [DllImport ("__Internal")]
    private static extern void setMusicSilence_ios();
    [DllImport ("__Internal")]
    private static extern void cancelMusicSilence_ios();
    [DllImport ("__Internal")]
    private static extern int getIsSilence_ios();
    [DllImport ("__Internal")]
    private static extern void setMusicMax_ios();
    [DllImport ("__Internal")]
    private static extern void cancelMusicMax_ios();
    [DllImport ("__Internal")]
    private static extern int getIsMaxVolume_ios();
    [DllImport ("__Internal")]
    private static extern void setCurrentMusicVolum_ios(float current_musicvolume);
    [DllImport ("__Internal")]
    private static extern float getCurrentMusicVolume_ios();
    #endregion

#endif

}