// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System.Collections.Generic;
    using UnityEngine;

    public enum _eSoundLayer
    {
        Background = 0,// 背景层，loop，同名替换，不同名替换
        LayerIgnore,
        LayerReplace,
        LayerReplaceFromBegin,
        LayerNormal
    }

    public interface ISoundLayer
    {
        //---------------------------------------------------------------------
        _eSoundLayer getLayerId();

        //---------------------------------------------------------------------
        void play(AudioSource audio_src);

        //---------------------------------------------------------------------
        void volumeChange(float volume);

        //---------------------------------------------------------------------
        void update();

        //---------------------------------------------------------------------
        void destroy();
    }

    // 背景层，loop，同名替换，不同名替换
    public class CSoundLayerBackground : ISoundLayer
    {
        //---------------------------------------------------------------------
        SoundMgr mSoundMgr = null;
        List<AudioSource> mListAudioPlaying = new List<AudioSource>();

        //---------------------------------------------------------------------
        public CSoundLayerBackground(SoundMgr sound_mgr)
        {
            mSoundMgr = sound_mgr;
        }

        //---------------------------------------------------------------------
        public _eSoundLayer getLayerId()
        {
            return _eSoundLayer.Background;
        }

        //---------------------------------------------------------------------
        public void play(AudioSource audio_src)
        {
            bool is_silence = false;
            if (PlayerPrefs.HasKey(SoundMgr.MusicIsSilenceKey))
            {
                bool.TryParse(PlayerPrefs.GetString(SoundMgr.MusicIsSilenceKey), out is_silence);
            }

            destroy();

            float music_value = 0f;
            if (!PlayerPrefs.HasKey(SoundMgr.BGMusicKey))
            {
                music_value = SoundMgr.DefaultBgMusicVolume;
            }
            else
            {
                music_value = PlayerPrefs.GetFloat(SoundMgr.BGMusicKey);
            }

            if (is_silence)
            {
                music_value = 0;
            }

            audio_src.volume = music_value;
            audio_src.loop = true;
            audio_src.Play();
            mListAudioPlaying.Add(audio_src);
        }

        //---------------------------------------------------------------------
        public void volumeChange(float volume)
        {
            foreach (var i in mListAudioPlaying)
            {
                i.volume = volume;
            }
        }

        //---------------------------------------------------------------------
        public void update()
        {
        }

        //---------------------------------------------------------------------
        public void destroy()
        {
            foreach (var i in mListAudioPlaying)
            {
                if (i == null)
                {
                    continue;
                }

                i.Stop();
                mSoundMgr.freeAudioSource(i);
            }
            mListAudioPlaying.Clear();
        }
    }

    // 同名忽略，不同名也忽略
    public class CSoundLayerIgnore : ISoundLayer
    {
        //---------------------------------------------------------------------
        SoundMgr mSoundMgr = null;
        AudioSource mAudioPlaying = null;

        //---------------------------------------------------------------------
        public CSoundLayerIgnore(SoundMgr sound_mgr)
        {
            mSoundMgr = sound_mgr;
        }

        //---------------------------------------------------------------------
        public _eSoundLayer getLayerId()
        {
            return _eSoundLayer.LayerIgnore;
        }

        //---------------------------------------------------------------------
        public void play(AudioSource audio_src)
        {
            bool is_silence = false;
            if (PlayerPrefs.HasKey(SoundMgr.SoundIsSilenceKey))
            {
                bool.TryParse(PlayerPrefs.GetString(SoundMgr.SoundIsSilenceKey), out is_silence);
            }

            if (mAudioPlaying == null)
            {
                float music_value = 0f;
                if (!PlayerPrefs.HasKey(SoundMgr.SoundKey))
                {
                    music_value = SoundMgr.DefaultSoundVolume;
                }
                else
                {
                    music_value = PlayerPrefs.GetFloat(SoundMgr.SoundKey);
                }

                if (is_silence)
                {
                    music_value = 0;
                }

                audio_src.volume = music_value;
                audio_src.loop = false;
                audio_src.Play();
                mAudioPlaying = audio_src;
            }
            else
            {
                mSoundMgr.freeAudioSource(audio_src);
            }
        }

        //---------------------------------------------------------------------
        public void volumeChange(float volume)
        {
            if (mAudioPlaying != null)
            {
                mAudioPlaying.volume = volume;
            }
        }

        //---------------------------------------------------------------------
        public void update()
        {
            if (mAudioPlaying == null) return;

            if (!mAudioPlaying.isPlaying)
            {
                mAudioPlaying.Stop();
                mSoundMgr.freeAudioSource(mAudioPlaying);
                mAudioPlaying = null;
            }
        }

        //---------------------------------------------------------------------
        public void destroy()
        {
            if (mAudioPlaying != null)
            {
                mAudioPlaying.Stop();
                mSoundMgr.freeAudioSource(mAudioPlaying);
                mAudioPlaying = null;
            }
        }
    }

    // 同名替换，不同名重叠(重新播放)
    public class CSoundLayerReplaceFromBegin : ISoundLayer
    {
        //---------------------------------------------------------------------
        SoundMgr mSoundMgr = null;
        Dictionary<string, AudioSource> mMapAudioPlaying = new Dictionary<string, AudioSource>();
        List<string> mListDestroy = new List<string>();

        //---------------------------------------------------------------------
        public CSoundLayerReplaceFromBegin(SoundMgr sound_mgr)
        {
            mSoundMgr = sound_mgr;
        }

        //---------------------------------------------------------------------
        public _eSoundLayer getLayerId()
        {
            return _eSoundLayer.LayerReplaceFromBegin;
        }

        //---------------------------------------------------------------------
        public void play(AudioSource audio_src)
        {
            bool is_silence = false;
            if (PlayerPrefs.HasKey(SoundMgr.SoundIsSilenceKey))
            {
                bool.TryParse(PlayerPrefs.GetString(SoundMgr.SoundIsSilenceKey), out is_silence);
            }

            if (mMapAudioPlaying.ContainsKey(audio_src.name))
            {
                // 同名替换
                mMapAudioPlaying[audio_src.name].Stop();
                mMapAudioPlaying[audio_src.name].Play();

                audio_src.Stop();
                mSoundMgr.freeAudioSource(audio_src);
            }
            else
            {
                // 不同名重叠
                float music_value = 0f;
                if (!PlayerPrefs.HasKey(SoundMgr.SoundKey))
                {
                    music_value = SoundMgr.DefaultSoundVolume;
                }
                else
                {
                    music_value = PlayerPrefs.GetFloat(SoundMgr.SoundKey);
                }

                if (is_silence)
                {
                    music_value = 0;
                }

                audio_src.volume = music_value;
                audio_src.loop = false;
                audio_src.Play();
                mMapAudioPlaying[audio_src.name] = audio_src;
            }
        }

        //---------------------------------------------------------------------
        public void volumeChange(float volume)
        {
            foreach (var i in mMapAudioPlaying)
            {
                i.Value.volume = volume;
            }
        }

        //---------------------------------------------------------------------
        public void update()
        {
            foreach (var i in mMapAudioPlaying)
            {
                if (!i.Value.isPlaying)
                {
                    i.Value.Stop();
                    mSoundMgr.freeAudioSource(i.Value);
                    mListDestroy.Add(i.Key);
                }
            }

            foreach (var i in mListDestroy)
            {
                mMapAudioPlaying.Remove(i);
            }
            mListDestroy.Clear();
        }

        //---------------------------------------------------------------------
        public void destroy()
        {
            foreach (var i in mMapAudioPlaying)
            {
                if (i.Value != null)
                {
                    i.Value.Stop();
                    mSoundMgr.freeAudioSource(i.Value);
                }
            }
            mMapAudioPlaying.Clear();
        }
    }

    // 同名替换，不同名重叠（不重新播放）
    public class CSoundLayerReplace : ISoundLayer
    {
        //---------------------------------------------------------------------
        SoundMgr mSoundMgr = null;
        Dictionary<string, AudioSource> mMapAudioPlaying = new Dictionary<string, AudioSource>();
        List<string> mListDestroy = new List<string>();

        //---------------------------------------------------------------------
        public CSoundLayerReplace(SoundMgr sound_mgr)
        {
            mSoundMgr = sound_mgr;
        }

        //---------------------------------------------------------------------
        public _eSoundLayer getLayerId()
        {
            return _eSoundLayer.LayerReplace;
        }

        //---------------------------------------------------------------------
        public void play(AudioSource audio_src)
        {
            bool is_silence = false;
            if (PlayerPrefs.HasKey(SoundMgr.SoundIsSilenceKey))
            {
                bool.TryParse(PlayerPrefs.GetString(SoundMgr.SoundIsSilenceKey), out is_silence);
            }

            if (mMapAudioPlaying.ContainsKey(audio_src.name))
            {
                audio_src.Stop();
                mSoundMgr.freeAudioSource(audio_src);
            }
            else
            {
                // 不同名重叠
                float music_value = 0f;
                if (!PlayerPrefs.HasKey(SoundMgr.SoundKey))
                {
                    music_value = SoundMgr.DefaultSoundVolume;
                }
                else
                {
                    music_value = PlayerPrefs.GetFloat(SoundMgr.SoundKey);
                }

                if (is_silence)
                {
                    music_value = 0;
                }

                audio_src.volume = music_value;
                audio_src.loop = false;
                audio_src.Play();
                mMapAudioPlaying[audio_src.name] = audio_src;
            }
        }

        //---------------------------------------------------------------------
        public void volumeChange(float volume)
        {
            foreach (var i in mMapAudioPlaying)
            {
                i.Value.volume = volume;
            }
        }

        //---------------------------------------------------------------------
        public void update()
        {
            foreach (var i in mMapAudioPlaying)
            {
                if (!i.Value.isPlaying)
                {
                    i.Value.Stop();
                    mSoundMgr.freeAudioSource(i.Value);
                    mListDestroy.Add(i.Key);
                }
            }

            foreach (var i in mListDestroy)
            {
                mMapAudioPlaying.Remove(i);
            }
            mListDestroy.Clear();
        }

        //---------------------------------------------------------------------
        public void destroy()
        {
            foreach (var i in mMapAudioPlaying)
            {
                if (i.Value != null)
                {
                    i.Value.Stop();
                    mSoundMgr.freeAudioSource(i.Value);
                }
            }
            mMapAudioPlaying.Clear();
        }
    }

    // 常规
    public class CSoundLayerNormal : ISoundLayer
    {
        //---------------------------------------------------------------------
        SoundMgr mSoundMgr = null;
        List<AudioSource> mListAudioPlaying = new List<AudioSource>();
        List<AudioSource> mListAudioDestroy = new List<AudioSource>();

        //---------------------------------------------------------------------
        public CSoundLayerNormal(SoundMgr sound_mgr)
        {
            mSoundMgr = sound_mgr;
        }

        //---------------------------------------------------------------------
        public _eSoundLayer getLayerId()
        {
            return _eSoundLayer.LayerNormal;
        }

        //---------------------------------------------------------------------
        public void play(AudioSource audio_src)
        {
            bool is_silence = false;
            if (PlayerPrefs.HasKey(SoundMgr.SoundIsSilenceKey))
            {
                bool.TryParse(PlayerPrefs.GetString(SoundMgr.SoundIsSilenceKey), out is_silence);
            }

            // 同名最多10个
            int count = 0;
            foreach (var i in mListAudioPlaying)
            {
                if (audio_src.name == i.name)
                {
                    count++;
                    if (count >= 10) break;
                }
            }

            float music_value = 0f;
            if (!PlayerPrefs.HasKey(SoundMgr.SoundKey))
            {
                music_value = SoundMgr.DefaultSoundVolume;
            }
            else
            {
                music_value = PlayerPrefs.GetFloat(SoundMgr.SoundKey);
            }

            if (count < 10)
            {
                if (is_silence)
                {
                    music_value = 0;
                }

                audio_src.volume = music_value;
                audio_src.loop = false;
                audio_src.Play();
                mListAudioPlaying.Add(audio_src);
            }
            else
            {
                audio_src.Stop();
                mSoundMgr.freeAudioSource(audio_src);
            }
        }

        //---------------------------------------------------------------------
        public void volumeChange(float volume)
        {
            foreach (var i in mListAudioPlaying)
            {
                i.volume = volume;
            }
        }

        //---------------------------------------------------------------------
        public void update()
        {
            foreach (var i in mListAudioPlaying)
            {
                if (!i.isPlaying)
                {
                    mListAudioDestroy.Add(i);
                }
            }

            foreach (var i in mListAudioDestroy)
            {
                i.Stop();
                mSoundMgr.freeAudioSource(i);
                mListAudioPlaying.Remove(i);
            }
            mListAudioDestroy.Clear();
        }

        //---------------------------------------------------------------------
        public void destroy()
        {
            foreach (var i in mListAudioPlaying)
            {
                i.Stop();
                mSoundMgr.freeAudioSource(i);
            }
            mListAudioPlaying.Clear();
            mListAudioDestroy.Clear();
        }
    }

    public class SoundMgr
    {
        //---------------------------------------------------------------------
        GameObject mObjAudio = null;
        Dictionary<_eSoundLayer, ISoundLayer> mMapSoundLayer = new Dictionary<_eSoundLayer, ISoundLayer>();
        Queue<AudioSource> mQueAudioFree = new Queue<AudioSource>();
        List<AudioSource> mListAudioFree = new List<AudioSource>();
        Dictionary<string, AudioClip> mMapAudioClip = new Dictionary<string, AudioClip>();
        Dictionary<string, AssetBundle> MapAssetBundleAudioClip = new Dictionary<string, AssetBundle>();
        public const string BGMusicKey = "BGMusic";
        public const string SoundKey = "Sound";
        public const string MusicIsSilenceKey = "MusicSilence";
        public const string SoundIsSilenceKey = "SoundMusicSilence";
        public const string UseZhenDongKey = "UseZhenDong";
        public const float DefaultBgMusicVolume = 0.3f;
        public const float DefaultSoundVolume = 1f;

        //---------------------------------------------------------------------
        public SoundMgr()
        {
            mObjAudio = new GameObject("Audio Object");

            if (!PlayerPrefs.HasKey(BGMusicKey))
            {
                PlayerPrefs.SetFloat(BGMusicKey, DefaultBgMusicVolume);
            }
            if (!PlayerPrefs.HasKey(SoundKey))
            {
                PlayerPrefs.SetFloat(SoundKey, DefaultSoundVolume);
            }

            mListAudioFree.Clear();
            for (int i = 0; i < 8; i++)
            {
                AudioSource obj_audio = mObjAudio.AddComponent<AudioSource>();
                mListAudioFree.Add(obj_audio);
                mQueAudioFree.Enqueue(obj_audio);
            }

            {
                CSoundLayerBackground s = new CSoundLayerBackground(this);
                mMapSoundLayer[s.getLayerId()] = s;
            }

            {
                CSoundLayerIgnore s = new CSoundLayerIgnore(this);
                mMapSoundLayer[s.getLayerId()] = s;
            }

            {
                CSoundLayerReplaceFromBegin s = new CSoundLayerReplaceFromBegin(this);
                mMapSoundLayer[s.getLayerId()] = s;
            }

            {
                CSoundLayerReplace s = new CSoundLayerReplace(this);
                mMapSoundLayer[s.getLayerId()] = s;
            }

            {
                CSoundLayerNormal s = new CSoundLayerNormal(this);
                mMapSoundLayer[s.getLayerId()] = s;
            }
        }

        //---------------------------------------------------------------------
        public void play(string file_name, _eSoundLayer sound_layer)
        {
            if (string.IsNullOrEmpty(file_name)) return;
            var audio_clip = _loadAudioClip(file_name);
            if (audio_clip != null)
            {
                AudioSource audio_src = _genAudioSource();
                audio_src.clip = audio_clip;
                mMapSoundLayer[sound_layer].play(audio_src);
            }
        }

        //---------------------------------------------------------------------
        public void bgVolumeChange(float volume)
        {
            mMapSoundLayer[_eSoundLayer.Background].volumeChange(volume);
            //foreach (var i in mMapSoundLayer)
            //{
            //    i.Value.volumeChange(volume);
            //}
        }

        //---------------------------------------------------------------------
        public List<AudioSource> getAudioSource()
        {
            return mListAudioFree;
        }

        //---------------------------------------------------------------------
        public void Update()
        {
            foreach (var i in mMapSoundLayer)
            {
                i.Value.update();
            }
        }

        //---------------------------------------------------------------------
        public void destroy()
        {
            mMapAudioClip.Clear();

            //foreach (var i in mMapSoundLayer)
            //{
            //    i.Value.destroy();
            //}
            mMapSoundLayer.Clear();

            mQueAudioFree.Clear();

            mListAudioFree.Clear();

            if (mObjAudio != null)
            {
                GameObject.Destroy(mObjAudio);
                mObjAudio = null;
            }
        }

        //---------------------------------------------------------------------
        public void destroyAllSceneSound()
        {
            mMapAudioClip.Clear();

            foreach (var i in mMapSoundLayer)
            {
                i.Value.destroy();
            }
            mMapSoundLayer.Clear();
        }

        //---------------------------------------------------------------------
        public void stopAllSceneSound()
        {
            foreach (var i in mMapSoundLayer)
            {
                i.Value.destroy();
            }
        }

        //---------------------------------------------------------------------
        public void freeAudioSource(AudioSource audio_src)
        {
            mQueAudioFree.Enqueue(audio_src);
        }

        //---------------------------------------------------------------------
        AudioSource _genAudioSource()
        {
            if (mQueAudioFree.Count == 0)
            {
                AudioSource audio_src = mObjAudio.AddComponent<AudioSource>();
                mListAudioFree.Add(audio_src);
                return audio_src;
            }
            else
            {
                return mQueAudioFree.Dequeue();
            }
        }

        //---------------------------------------------------------------------
        AudioClip _loadAudioClip(string audio_name)
        {
            audio_name = audio_name.ToLower();
            AudioClip audio_clip = null;
            mMapAudioClip.TryGetValue(audio_name, out audio_clip);
            if (audio_clip == null)
            {
                string s = CasinosContext.Instance.PathMgr.DirAbAudio + audio_name + ".ab";

                AssetBundle ab = null;
                MapAssetBundleAudioClip.TryGetValue(s, out ab);
                if (ab == null)
                {
                    ab = AssetBundle.LoadFromFile(s);
                    MapAssetBundleAudioClip[s] = ab;
                }

                audio_clip = ab.LoadAsset<AudioClip>(audio_name);

                mMapAudioClip[audio_name] = audio_clip;
            }

            return audio_clip;
        }
    }
}