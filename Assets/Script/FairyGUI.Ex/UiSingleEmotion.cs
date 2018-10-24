// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using FairyGUI;

    public class UiSingleEmotion
    {
        //---------------------------------------------------------------------
        public GComponent Com { get; set; }
        public GLoader GloaderEmotion { get; set; }
        public Transition TransitionEmotion { get; set; }

        //---------------------------------------------------------------------
        public UiSingleEmotion(GComponent com)
        {
            Com = com;
            Com.touchable = false;
            GloaderEmotion = Com.GetChild("LoaderEmotion").asLoader;
            TransitionEmotion = Com.GetTransition("TransitionEmotion");
        }

        //---------------------------------------------------------------------
        public void setEmotion(string emotion_name, bool with_ani)
        {
            TransitionEmotion.Stop(true, false);
            GloaderEmotion.alpha = 1;
            GloaderEmotion.SetPosition(0, 0, 0);
            GloaderEmotion.icon = UIPackage.GetItemURL("Common", emotion_name);
            CasinosContext.Instance.Play(emotion_name, _eSoundLayer.LayerNormal);
            if (with_ani)
            {
                TransitionEmotion.Play();
            }
        }

        //---------------------------------------------------------------------
        public void resetEmotion()
        {
            GloaderEmotion.icon = null;
            GloaderEmotion.alpha = 1;
            GloaderEmotion.SetPosition(0, 0, 0);
            TransitionEmotion.Stop(true, false);
        }
    }
}