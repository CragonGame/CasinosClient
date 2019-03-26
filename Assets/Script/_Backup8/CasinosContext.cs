// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using UnityEngine;
    //using XLua;
    using FairyGUI;
    
    public enum _eProjectItemDisplayNameKey
    {
        Gold,
        Diamond
    }

    public enum _eChatItemType
    {
        NormalChat,
        ShootingText,
        Marquee,
    }

    //[LuaCallCSharp]
    public class CasinosContext
    {
        //---------------------------------------------------------------------
        public static CasinosContext Instance { get; private set; }
        //public TimerShaft TimerShaft { get; private set; }
        public System.Diagnostics.Stopwatch Stopwatch { get; private set; }
        public MemoryStream MemoryStream { get; private set; }
        public StringBuilder SB { get; private set; }
        public PathMgr PathMgr { get; private set; }
        public CasinosPlayerPrefs PlayerPrefs { get; private set; }
        public CasinosConfig Config { get; private set; }
        public TextureMgr TextureMgr { get; private set; }
        public DelayMgr DelayMgr { get; private set; }
        public LuaMgr LuaMgr { get; private set; }
        public NetMgr NetMgr { get; private set; }
        public SpineMgr SpineMgr { get; private set; }
        //public NativeMgr NativeMgr { get; set; }
        public NativeAPIMsgReceiverListener NativeAPIMsgReceiverListner { get; set; }
        public bool UseHttps { get; set; }
        public bool CanReportLog { get; set; }
        public string CanReportLogDeviceId { get; set; }
        public string CanReportLogPlayerId { get; set; }
        public bool UnityAndroid { get; set; }
        public bool UnityIOS { get; set; }
        public bool IsEditor { get; set; }
        public bool IsEditorDebug { get; set; }// 是否处于编辑器调试模式，可以调试lua，使用本地ab，raw
        public bool IsSqliteUnity { get; set; }
        //public LuaTable TbDataMgrLua { get; set; }
        //FTMgr FTMgr { get; set; }
        HeadIconMgr HeadIconMgr { get; set; }
        SoundMgr SoundMgr { get; set; }
        CasinosILRuntime CsRuntime { get; set; }

        //---------------------------------------------------------------------
        public CasinosContext(bool is_editor_debug)
        {
            Instance = this;
            IsEditorDebug = is_editor_debug;

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

            Stopwatch = new System.Diagnostics.Stopwatch();
            Stopwatch.Start();
            //TimerShaft = new TimerShaft();
            MemoryStream = new MemoryStream();
            SB = new StringBuilder();
            //FTMgr = new FTMgr();
            PathMgr = new PathMgr(editor_runsorce);// 初始化PathMgr
            Config = new CasinosConfig(editor_runsorce);
            //NativeMgr = new NativeMgr();
            SpineMgr = new SpineMgr();

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
                QualitySettings.vSyncCount = 1;
                Application.targetFrameRate = 60;
#endif
            }

            if (NativeAPIMsgReceiverListner == null)
            {
                NativeAPIMsgReceiverListner = new NativeAPIMsgReceiverListener();
            }

            NetMgr = new NetMgr();
            PlayerPrefs = new CasinosPlayerPrefs();
            TextureMgr = new TextureMgr();
            UIObjectFactory.SetLoaderExtension(typeof(GLoaderEx));
            DelayMgr = new DelayMgr();
            LuaMgr = new LuaMgr();
            SoundMgr = new SoundMgr();
            HeadIconMgr = new HeadIconMgr();
        }

        //---------------------------------------------------------------------
        public void Update(float elapsed_tm)
        {
            if (SoundMgr != null)
            {
                SoundMgr.Update();
            }

            //if (FTMgr != null)
            //{
            //    FTMgr.Update(elapsed_tm);
            //}

            if (DelayMgr != null)
            {
                DelayMgr.Update(elapsed_tm);
            }

            if (NetMgr != null)
            {
                NetMgr.Update(elapsed_tm);
            }

            if (LuaMgr != null)
            {
                LuaMgr.Update(elapsed_tm);
            }

            if (CsRuntime != null)
            {
                CsRuntime.Update();
            }

            //if (TimerShaft != null)
            //{
            //    TimerShaft.ProcessTimer((ulong)Stopwatch.ElapsedMilliseconds);
            //}

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //LuaMgr._CSharpCallOnAndroidQuitConfirm();
            }
        }

        //---------------------------------------------------------------------
        public void Close()
        {
            //if (TimerShaft != null)
            //{
            //    TimerShaft.Destroy();
            //    TimerShaft = null;
            //}

            if (SoundMgr != null)
            {
                SoundMgr.Destroy();
                SoundMgr = null;
            }

            if (HeadIconMgr != null)
            {
                HeadIconMgr.Destroy();
                HeadIconMgr = null;
            }

            if (SpineMgr != null)
            {
                SpineMgr.Destroy();
                SpineMgr = null;
            }

            if (NetMgr != null)
            {
                NetMgr.Close();
                NetMgr = null;
            }

            if (NativeAPIMsgReceiverListner != null)
            {
                NativeAPIMsgReceiverListner.Close();
                NativeAPIMsgReceiverListner = null;
            }

            if (CsRuntime != null)
            {
                CsRuntime.Destroy();
                CsRuntime = null;
            }

            if (LuaMgr != null)
            {
                LuaMgr.Release();
                LuaMgr = null;
            }

            if (DelayMgr != null)
            {
                DelayMgr.Close();
                DelayMgr = null;
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
            // 计算PathMgr中的动态目录
            bool version_persistent = false;
            if (!string.IsNullOrEmpty(Config.VersionCommonPersistent) && !string.IsNullOrEmpty(Config.VersionDataPersistent))
            {
                version_persistent = true;
            }
            if (!version_persistent)
            {
                PathMgr.DirLaunchAb = "Resources.KingTexasLaunch/";
                PathMgr.DirLaunchAbType = DirType.Resources;
            }
            else
            {
                PathMgr.DirLaunchAb = PathMgr.CombinePersistentDataPath("Resources.KingTexasLaunch/");
                PathMgr.DirLaunchAbType = DirType.Raw;
            }

            if (IsEditorDebug)
            {
                string p = Path.Combine(System.Environment.CurrentDirectory, "./DataOss/");
                var di = new DirectoryInfo(p);
                string p1 = di.FullName.Replace('\\', '/');

                string lua_root = System.Environment.CurrentDirectory + "/Assets/Script.Lua/";
                PathMgr.DirLuaRoot = lua_root.Replace('\\', '/');
                PathMgr.DirRawRoot = p1 + "Common/Raw/";
                PathMgr.DirAbRoot = p1 + Config.Platform + "/Resources.KingTexas/";
            }
            else
            {
                PathMgr.DirLuaRoot = PathMgr.CombinePersistentDataPath("Lua/");
                PathMgr.DirRawRoot = PathMgr.CombinePersistentDataPath("Raw/");
                PathMgr.DirAbRoot = PathMgr.CombinePersistentDataPath("Resources.KingTexas/");
            }

            PathMgr.DirCsRoot = PathMgr.CombinePersistentDataPath("Cs/");

            PathMgr.DirAbUi = PathMgr.DirAbRoot + "Ui/";// "Resources.KingTexas/Ui/"，需动态计算
            PathMgr.DirAbCard = PathMgr.DirAbRoot + "Cards/";// "Resources.KingTexas/Cards/"，需动态计算
            PathMgr.DirAbAudio = PathMgr.DirAbRoot + "Audio/";// "Resources.KingTexas/Audio/"，需动态计算
            PathMgr.DirAbItem = PathMgr.DirAbRoot + "Item/";// "Resources.KingTexas/Item/"，需动态计算
            PathMgr.DirAbParticle = PathMgr.DirAbRoot + "Particle/";// "Resources.KingTexas/Particle/"，需动态计算

            LuaMgr.Launch(!version_persistent);

            CsRuntime = new CasinosILRuntime();
            CsRuntime.Create();
        }

        //-------------------------------------------------------------------------
        public void OnApplicationPause(bool pause)
        {
           // LuaMgr._CSharpCallOnApplicationPause(pause);
        }

        //-------------------------------------------------------------------------
        public void OnApplicationFocus(bool focus_status)
        {
           // LuaMgr._CSharpCallOnApplicationFocus(focus_status);
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
            if (CanReportLog
                && !string.IsNullOrEmpty(CanReportLogPlayerId)
                && CanReportLogPlayerId.Equals(player_id))
            {
                BuglyAgent.ReportException(name, message, stackTrace);
            }
        }

        //---------------------------------------------------------------------
        public void SetNativeOperate(int native_operate)
        {
//#if UNITY_IOS
//            //NativeOperateType operate = (NativeOperateType)native_operate;
//            //NativeMgr.nativeOperate(operate);
//#endif
        }

        //---------------------------------------------------------------------
        public void ClearSB()
        {
            SB.Length = 0;
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
        public void Play(string file_name, _eSoundLayer sound_layer)
        {
            SoundMgr.Play(file_name, sound_layer);
        }

        //---------------------------------------------------------------------
        public void BgVolumeChange(float volume)
        {
            SoundMgr.BgVolumeChange(volume);
        }

        //---------------------------------------------------------------------
        public List<AudioSource> GetAudioSource()
        {
            return SoundMgr.GetAudioSource();
        }

        //---------------------------------------------------------------------
        public void DestroyAllSceneSound()
        {
            SoundMgr.DestroyAllSceneSound();
        }

        //---------------------------------------------------------------------
        public void StopAllSceneSound()
        {
            SoundMgr.StopAllSceneSound();
        }

        //---------------------------------------------------------------------
        public void FreeAudioSource(AudioSource audio_src)
        {
            SoundMgr.FreeAudioSource(audio_src);
        }
    }
}