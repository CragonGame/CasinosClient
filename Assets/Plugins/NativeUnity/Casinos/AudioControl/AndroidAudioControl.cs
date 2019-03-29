using UnityEngine;
using System.Collections;
using System;

//-------------------------------------------------------------------------
//public enum _eAndroidAudioControl
//{
//    setMusicSilence,
//    cancelMusicSilence,
//    getIsSilence,
//    setMusicMax,
//    cancelMusicMax,
//    getIsMaxVolume,
//    setCurrentMusicVolume,
//    getCurrentMusicVolume,
//}

public class AndroidAudioControl : INativeAudioControl
{
    public AndroidJavaClass mAndroidJavaClassAudioControl;
    public AndroidJavaObject mAndroidAudioControl;

    //-------------------------------------------------------------------------
    public AndroidAudioControl()
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
            mAndroidAudioControl = mAndroidJavaClassAudioControl.CallStatic<AndroidJavaObject>("Instantce",
                "NativeReceiver", "audioChanged");
        }
    }
}