using UnityEngine;
using System.Collections;

public interface INativeAudioControl
{
    //-------------------------------------------------------------------------
    void setMusicSilence();
    void cancelMusicSilence();
    int getIsSilence();//0false 1true
    void setMusicMax();
    void cancelMusicMax();
    int getIsMaxVolume();//0false 1true
    void setCurrentMusicVolume(float current_muiscvolume);
    float getCurrentMusicVolume();
}
