using UnityEngine;
using System.Collections;

public class NativeAudioControl
{
    //-------------------------------------------------------------------------
    static INativeAudioControl mINativeAudioControl;

    //-------------------------------------------------------------------------
    static NativeAudioControl()
    {
#if UNITY_ANDROID
        mINativeAudioControl = new AndroidAudioControl();
        Debug.Log("AndroidAudioControl::");
#elif UNITY_IOS
        mINativeAudioControl = new IOSAudioControl();
        Debug.Log("IOSAudioControl::");
#else
        Debug.LogError("Do not supported on this platform. ");
#endif
    }

    //-------------------------------------------------------------------------
    public static void setMusicSilence()
    {
        mINativeAudioControl.setMusicSilence();
    }

    //-------------------------------------------------------------------------
    public static void cancelMusicSilence()
    {
        mINativeAudioControl.cancelMusicSilence();
    }

    //-------------------------------------------------------------------------
    /// <summary>
    /// 0 false 1 true
    /// </summary>
    /// <returns></returns>
    public static int getIsSilence()
    {
        return mINativeAudioControl.getIsSilence();
    }

    //-------------------------------------------------------------------------
    public static void setMusicMax()
    {
        mINativeAudioControl.setMusicMax();
    }

    //-------------------------------------------------------------------------
    public static void cancelMusicMax()
    {
        mINativeAudioControl.cancelMusicMax();
    }

    //-------------------------------------------------------------------------
    /// <summary>
    /// 0 false 1 true
    /// </summary>
    /// <returns></returns>
    public static int getIsMaxVolume()
    {
        return mINativeAudioControl.getIsMaxVolume();
    }

    //-------------------------------------------------------------------------
    public static void setCurrentMusicVolume(float current_muiscvolume)
    {
        mINativeAudioControl.setCurrentMusicVolume(current_muiscvolume);
    }

    //-------------------------------------------------------------------------
    public static float getCurrentMusicVolume()
    {
        return mINativeAudioControl.getCurrentMusicVolume();
    }
}
