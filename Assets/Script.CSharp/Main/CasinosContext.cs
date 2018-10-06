// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using UnityEngine;
    using XLua;
    using FairyGUI;

    public enum _eProjectItemDisplayNameKey
    {
        Gold,
        Diamond
    }

    public enum _eFriendStateClient
    {
        Offline,
        Fishing,
        GFlowerDesktopH,
        GFlowerDesktopPrivate,
        GFlowerDesktopNormal,
        TexasDesktopClassicPrivate,
        TexasDesktopClassic,
        TexasDesktopMustBet,
        TexasDesktopMustBetPrivate,
        TexasDesktopH,
        NiuNiuDesktopH,
        ZhongFBDesktopH,
        NotInDesktop,
    }

    public enum _eChatItemType
    {
        NormalChat,
        ShootingText,
        Marquee,
    }

    public enum _eLoginType
    {
        Acc,
        Guest,
        WeiXin,
    }

    [LuaCallCSharp]
    public class CasinosContext
    {
        //---------------------------------------------------------------------
        public static CasinosContext Instance { get; private set; }
        public TimerShaft TimerShaft { get; private set; }
        public System.Diagnostics.Stopwatch Stopwatch { get; private set; }
        public MemoryStream MemoryStream { get; private set; }
        public StringBuilder SB { get; private set; }
        public AsyncAssetLoaderMgr AsyncAssetLoaderMgr { get; private set; }
        public AsyncAssetLoadGroup AsyncAssetLoadGroup { get; private set; }
        public PathMgr PathMgr { get; private set; }
        public CasinosPlayerPrefs PlayerPrefs { get; private set; }
        public CasinosConfig Config { get; private set; }
        public TextureMgr TextureMgr { get; private set; }
        public CasinosLua CasinosLua { get; private set; }
        public NetBridge NetBridge { get; private set; }
        public NativeMgr NativeMgr { get; set; }
        public NativeAPIMsgReceiverListener NativeAPIMsgReceiverListner { get; set; }
        public string ABResourcePathTitle { get; set; }
        public string UiPathRoot { get; set; }
        public string ResourcesRowPathRoot { get; set; }
        public _eLoginType LoginType { get; set; }
        public bool UseHttps { get; set; }
        public bool CanReportLog { get; set; }
        public string CanReportLogDeviceId { get; set; }
        public string CanReportLogPlayerId { get; set; }
        public bool UnityAndroid { get; set; }
        public bool UnityIOS { get; set; }
        public bool IsEditor { get; set; }
        public bool IsSqliteUnity { get; set; }
        FTMgr FTMgr { get; set; }
        HeadIconMgr HeadIconMgr { get; set; }
        CSoundMgr SoundMgr { get; set; }
        public LuaTable TbDataMgrLua { get; set; }

        public const string LocalDataVersionKey = "LocalVersionInfo";
        public const string PreDataVersionKey = "PreDataVersion";
        public const string LanKey = "LanKey";

        //---------------------------------------------------------------------
        public CasinosContext(bool use_persistent,
            string ui_pathroot,
            string resourcesrow_pathroot,
            string ab_resource_title)
        {
            _eEditorRunSourcePlatform editor_runsorce = _eEditorRunSourcePlatform.Android;
#if UNITY_STANDALONE_WIN
            editor_runsorce = _eEditorRunSourcePlatform.PC;
#elif UNITY_ANDROID && UNITY_EDITOR
            editor_runsorce = _eEditorRunSourcePlatform.Android;
#elif UNITY_ANDROID
            editor_runsorce = _eEditorRunSourcePlatform.Android;
#elif UNITY_IPHONE
            editor_runsorce = _eEditorRunSourcePlatform.IOS;
#endif

            Instance = this;
            ABResourcePathTitle = ab_resource_title;
            ResourcesRowPathRoot = resourcesrow_pathroot;
            UiPathRoot = ui_pathroot;

#if UNITY_EDITOR
            IsEditor = true;
#else
            IsEditor = false;
#endif

#if UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_DASHBOARD_WIDGET || UNITY_STANDALONE_LINUX || UNITY_WEBPLAYER
            IsSqliteUnity = true;
#else
            IsSqliteUnity = false;
#endif

#if UNITY_IOS
            string version = UnityEngine.iOS.Device.systemVersion;
            var arr_version = version.Split('.');
            int first_version_code = 0;
            if (arr_version.Length > 0)
            {
                int.TryParse(arr_version[0].ToString(), out first_version_code);
            }
            if (first_version_code < 9)
            {
                UseHttps = false;
            }
            else
            {
                UseHttps = true;
            }
            UnityIOS = true;
            UnityAndroid = false;
#elif UNITY_ANDROID
            UseHttps = false;
            UnityAndroid = true;
            UnityIOS = false;
#elif UNITY_STANDALONE_WIN
#endif
            NativeMgr = new NativeMgr();
            PathMgr = new PathMgr(editor_runsorce, use_persistent);// 初始化PathMgr
            FTMgr = new FTMgr();
            Stopwatch = new System.Diagnostics.Stopwatch();
            Stopwatch.Start();
            TimerShaft = new TimerShaft();
            MemoryStream = new MemoryStream();
            SB = new StringBuilder();
            Config = new CasinosConfig(editor_runsorce);

            // 初始化系统参数
            {
#if UNITY_EDITOR
                Application.runInBackground = false;
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = -1;
#else
                Application.runInBackground = true;
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                //Time.fixedDeltaTime = 0.033f;
                QualitySettings.vSyncCount = 1;
                Application.targetFrameRate = 60;
#endif
            }

            // 初始化日志
            {
                EbLog.NoteCallback = Debug.Log;
                EbLog.WarningCallback = Debug.LogWarning;
                EbLog.ErrorCallback = Debug.LogError;
            }

            if (NativeAPIMsgReceiverListner == null)
            {
                NativeAPIMsgReceiverListner = new NativeAPIMsgReceiverListener();
            }

            if (AsyncAssetLoaderMgr == null)
            {
                AsyncAssetLoaderMgr = new AsyncAssetLoaderMgr();
            }

            AsyncAssetLoadGroup = AsyncAssetLoaderMgr.createAsyncAssetLoadGroup();

            NetBridge = new NetBridge();

            PlayerPrefs = new CasinosPlayerPrefs();

            TextureMgr = new TextureMgr();

            UIObjectFactory.SetLoaderExtension(typeof(GLoaderEx));

            CasinosLua = new CasinosLua();
            SoundMgr = new CSoundMgr();
            HeadIconMgr = new HeadIconMgr();
        }

        //---------------------------------------------------------------------
        public void Update(float elapsed_tm)
        {
            if (SoundMgr != null)
            {
                SoundMgr.Update();
            }

            if (FTMgr != null)
            {
                FTMgr.Update(elapsed_tm);
            }

            if (NetBridge != null)
            {
                NetBridge.Update(elapsed_tm);
            }

            if (AsyncAssetLoaderMgr != null)
            {
                AsyncAssetLoaderMgr.Update(Time.deltaTime);
            }

            if (CasinosLua != null)
            {
                CasinosLua.Update(elapsed_tm);
            }

            if (TimerShaft != null)
            {
                TimerShaft.ProcessTimer((ulong)Stopwatch.ElapsedMilliseconds);
            }
        }

        //---------------------------------------------------------------------
        public void Close()
        {
            if (TimerShaft != null)
            {
                TimerShaft.Destroy();
                TimerShaft = null;
            }

            if (SoundMgr != null)
            {
                SoundMgr.destroy();
                SoundMgr = null;
            }

            if (HeadIconMgr != null)
            {
                HeadIconMgr.destroy();
                HeadIconMgr = null;
            }

            AsyncAssetLoaderMgr = null;
            if (AsyncAssetLoadGroup != null)
            {
                AsyncAssetLoadGroup.destroy();
                AsyncAssetLoadGroup = null;
            }

            if (CasinosLua != null)
            {
                CasinosLua.Release();
                CasinosLua = null;
            }

            if (MemoryStream != null)
            {
                MemoryStream.Close();
                MemoryStream = null;
            }

            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }

        //---------------------------------------------------------------------
        public void Launch()
        {
            // 比较Launch版本，决定是否将Launch从StreamingAssets拷贝到Persistent
            bool need_copy = false;
            if (string.IsNullOrEmpty(Config.VersionLaunchPersistent)
                || Config.VersionLaunchPersistent != Config.StreamingAssetsInfo.DataVersion)
            {
                need_copy = true;
            }

            // 将Launch从StreamingAssets拷贝到Persistent
            if (need_copy)
            {
                var copy_dir = new CopyStreamingAssetsToPersistentData1();
                copy_dir.CopySync(Config.StreamingAssetsInfo.ListLaunchFile);
                Config.WriteVersionLaunchPersistent(Config.StreamingAssetsInfo.DataVersion);
            }

            CasinosLua.Launch();
        }

        //-------------------------------------------------------------------------
        public void OnApplicationPause(bool pause)
        {
            CasinosLua._CSharpCallOnApplicationPause(pause);
        }

        //-------------------------------------------------------------------------
        public void OnApplicationFocus(bool focus_status)
        {
            CasinosLua._CSharpCallOnApplicationFocus(focus_status);
        }

        //---------------------------------------------------------------------
        public void ReportLogWithDeviceId(string name, string message, string stackTrace)
        {
            string device_id = SystemInfo.deviceUniqueIdentifier;
            if (CanReportLog && !string.IsNullOrEmpty(CanReportLogDeviceId) && CanReportLogDeviceId.Equals(device_id))
            {
                BuglyAgent.ReportException(name, message, stackTrace);
            }
        }

        //---------------------------------------------------------------------
        public void ReportLogWithPlayerId(string name, string player_id, string message, string stackTrace)
        {
            if (CanReportLog && !string.IsNullOrEmpty(CanReportLogPlayerId) &&
                CanReportLogPlayerId.Equals(player_id))
            {
                BuglyAgent.ReportException(name, message, stackTrace);
            }
        }

        //---------------------------------------------------------------------
        public void SetNativeOperate(int native_operate)
        {
#if UNITY_IOS
            NativeOperateType operate = (NativeOperateType)native_operate;
            NativeMgr.nativeOperate(operate);
#endif
        }

        //---------------------------------------------------------------------
        public void ClearSB()
        {
            SB.Clear();
        }

        //---------------------------------------------------------------------
        public string AppendStrWithSB(params string[] strs)
        {
            ClearSB();

            foreach (var i in strs)
            {
                SB.Append(i);
            }

            return SB.ToString();
        }

        //---------------------------------------------------------------------
        public void Disconnect()
        {
            NetBridge.disconnect();
        }

        //---------------------------------------------------------------------
        public void Play(string file_name, _eSoundLayer sound_layer)
        {
            SoundMgr.play(file_name, sound_layer);
        }

        //---------------------------------------------------------------------
        public void BgVolumeChange(float volume)
        {
            SoundMgr.bgVolumeChange(volume);
        }

        //---------------------------------------------------------------------
        public List<AudioSource> GetAudioSource()
        {
            return SoundMgr.getAudioSource();
        }

        //---------------------------------------------------------------------
        public void DestroyAllSceneSound()
        {
            SoundMgr.destroyAllSceneSound();
        }

        //---------------------------------------------------------------------
        public void StopAllSceneSound()
        {
            SoundMgr.stopAllSceneSound();
        }

        //---------------------------------------------------------------------
        public void FreeAudioSource(AudioSource audio_src)
        {
            SoundMgr.freeAudioSource(audio_src);
        }
    }
}