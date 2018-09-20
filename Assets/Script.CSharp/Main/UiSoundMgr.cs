// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System.Collections.Generic;
    using UnityEngine;

    public class UiSoundMgr
    {
        //---------------------------------------------------------------------
        static UiSoundMgr mUiSoundMgr;
        Dictionary<string, AudioClip> MapAudioClip;
        const string mAudioPathRoot = "Audio/";

        //---------------------------------------------------------------------
        private UiSoundMgr()
        {
            MapAudioClip = new Dictionary<string, AudioClip>();
        }

        //---------------------------------------------------------------------
        public static UiSoundMgr Instant
        {
            get
            {
                if (mUiSoundMgr == null)
                {
                    mUiSoundMgr = new UiSoundMgr();
                }
                return mUiSoundMgr;
            }
        }

        //---------------------------------------------------------------------
        public void playSound(string sound_path, GameObject obj, bool is_loop = false)
        {
            bool is_silence = false;
            if (PlayerPrefs.HasKey(CSoundMgr.SoundIsSilenceKey))
            {
                bool.TryParse(PlayerPrefs.GetString(CSoundMgr.SoundIsSilenceKey), out is_silence);
            }

            if (is_silence)
            {
                return;
            }

            float volume = 1;
            if (PlayerPrefs.HasKey(CSoundMgr.SoundKey))
            {
                volume = PlayerPrefs.GetFloat(CSoundMgr.SoundKey);
            }

            AudioClip clip = null;
            sound_path = mAudioPathRoot + sound_path;
            if (MapAudioClip.ContainsKey(sound_path))
            {
                clip = MapAudioClip[sound_path];
            }
            else
            {
                clip = Resources.Load<AudioClip>(sound_path);
                MapAudioClip.Add(sound_path, clip);
            }

            AudioSource[] audio_sources = obj.GetComponents<AudioSource>();
            AudioSource current_audio = null;
            if (audio_sources == null || audio_sources.Length == 0)
            {
                current_audio = obj.AddComponent<AudioSource>();
            }
            else
            {
                foreach (var i in audio_sources)
                {
                    if (!i.isPlaying)
                    {
                        current_audio = i;
                        break;
                    }
                }

                if (current_audio == null)
                {
                    current_audio = obj.AddComponent<AudioSource>();
                }
            }

            current_audio.playOnAwake = false;
            current_audio.clip = clip;
            current_audio.volume = volume;
            current_audio.loop = is_loop;
            current_audio.Play();
        }
    }
}