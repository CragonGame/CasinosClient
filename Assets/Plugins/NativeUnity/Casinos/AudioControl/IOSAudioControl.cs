using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

#if UNITY_IPHONE
public class IOSAudioControl : INativeAudioControl
{
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
    [DllImport("__Internal")]
    private static extern void setMusicSilence_ios();
    [DllImport("__Internal")]
    private static extern void cancelMusicSilence_ios();
    [DllImport("__Internal")]
    private static extern int getIsSilence_ios();
    [DllImport("__Internal")]
    private static extern void setMusicMax_ios();
    [DllImport("__Internal")]
    private static extern void cancelMusicMax_ios();
    [DllImport("__Internal")]
    private static extern int getIsMaxVolume_ios();
    [DllImport("__Internal")]
    private static extern void setCurrentMusicVolum_ios(float current_musicvolume);
    [DllImport("__Internal")]
    private static extern float getCurrentMusicVolume_ios();
#endregion
}
#endif