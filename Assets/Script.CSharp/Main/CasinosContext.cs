// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine;
    using XLua;
    using FairyGUI;
    using GameCloud.Unity.Common;

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
        public string UCenterDomain { get; private set; }
        public string UCenterAppId { get; private set; }
        public TimerShaft TimerShaft { get; private set; }
        public System.Diagnostics.Stopwatch Stopwatch { get; private set; }
        public MemoryStream MemoryStream { get; private set; }
        public StringBuilder SB { get; private set; }
        public AsyncAssetLoaderMgr AsyncAssetLoaderMgr { get; private set; }
        public PathMgr PathMgr { get; private set; }
        public CasinosPlayerPrefs PlayerPrefs { get; private set; }
        public CasinosConfig Config { get; private set; }
        public TextureMgr TextureMgr { get; private set; }
        public CasinosLua CasinosLua { get; private set; }
        public NetBridge NetBridge { get; private set; }
        public NativeMgr NativeMgr { get; set; }
        public bool Pause { get; set; }
        public bool LoadDataDone { get; set; }
        public string ABResourcePathTitle { get; set; }
        public string UiPathRoot { get; set; }
        public string ResourcesRowPathRoot { get; set; }
        public _eLoginType LoginType { get; set; }
        public bool WeiChatIsInstalled { get; set; }
        public bool ShowWeiChat { get; set; }
        public bool ServerIsInvalid { get; set; }
        public string ServerStateInfo { get; set; }
        public bool UseHttps { get; set; }
        public bool UseBindPhoneAndWeiChat { get; private set; }
        public bool NeedHideClientUi { get; set; }
        public bool ClientShowFirstRecharge { get; set; }
        public bool DesktopHSysBankShowDBValue { get; set; }
        public bool CanReportLog { get; set; }
        public string CanReportLogDeviceId { get; set; }
        public string CanReportLogPlayerId { get; set; }
        public int ShootingTextShowVIPLimit { get; set; }
        public int DesktopHCanChatVIPLimit { get; set; }
        public int DesktopCanChatVIPLimit { get; set; }
        public bool ShowGoldTree { get; set; }
        public bool UseWeiChatPay { get; set; }
        public bool UseALiPay { get; set; }
        public string BuglyAppId { get; set; }
        public string PinggPPAppId { get; set; }
        public string WeChatAppId { get; set; }
        public string WeChatState { get; set; }
        public string DataEyeId { get; set; }
        public string PushAppId { get; set; }
        public string PushAppKey { get; set; }
        public string PushAppSecret { get; set; }
        public string ShareSDKAppKey { get; set; }
        public string ShareSDKAppSecret { get; set; }
        public bool UseLan { get; set; }
        public bool UseDefaultLan { get; set; }
        public string DefaultLan { get; set; }
        public string CurrentLan { get; set; }
        public bool UnityAndroid { get; set; }
        public bool UnityIOS { get; set; }
        public bool IsEditor { get; set; }
        public string LotteryTicketFactoryName { get; set; }
        public string AccountId { get; set; }
        public bool IsSqliteUnity { get; set; }
        public ushort CSharpLastMethodId { get; set; }
        public AsyncAssetLoadGroup AsyncAssetLoadGroup { get; private set; }
        public NativeAPIMsgReceiverListener NativeAPIMsgReceiverListner { get; set; }
        FTMgr FTMgr { get; set; }
        HeadIconMgr HeadIconMgr { get; set; }
        CSoundMgr SoundMgr { get; set; }
        string LuaProjectListenerName { get; set; }
        string PlatformName { get; set; }
        public LuaTable TbDataMgrLua { get; set; }

        public const string LocalDataVersionKey = "LocalVersionInfo";
        public const string PreDataVersionKey = "PreDataVersion";
        public const string LanKey = "LanKey";

        //---------------------------------------------------------------------
        public CasinosContext(bool use_persistent,
            string lua_project_listener_name,
            string ui_pathroot,
            string resourcesrow_pathroot,
            string ab_resource_title,
            string lotteryticket_factoryname)
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
            WeiChatIsInstalled = true;
            ShowWeiChat = true;
            UseBindPhoneAndWeiChat = false;
            LuaProjectListenerName = lua_project_listener_name;
            LotteryTicketFactoryName = lotteryticket_factoryname;
            UseLan = true;
            _checkLan();

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
            PlatformName = "IOS";
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
            PlatformName = "ANDROID";
            UseHttps = false;
            UnityAndroid = true;
            UnityIOS = false;
#elif UNITY_STANDALONE_WIN
            PlatformName = "PC";
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
        public void FixedUpdate()
        {
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
            if (string.IsNullOrEmpty(Config.VersionLaunchPersistent))
            {
                need_copy = true;
            }

            // 将Launch从StreamingAssets拷贝到Persistent
            if (need_copy)
            {
                var copy_dir = new CopyStreamingAssetsToPersistentData1();
                copy_dir.CopySync(Config.StreamingAssetsInfo.ListLaunchFile);
            }

            CasinosLua.Launch();
        }

        //---------------------------------------------------------------------
        public string GetRandomTips()
        {
            return "确认退出吗";

            //var arr_tip = UserConfig.Current.Tips;
            //return arr_tip[UnityEngine.Random.Range(0, arr_tip.Length)];
        }

        //---------------------------------------------------------------------
        public string GetPlatformName(bool to_lower)
        {
            string platform = PlatformName;
            if (to_lower)
            {
                platform = PlatformName.ToLower();
            }
            return platform;
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
        public LuaTable GetLuaTable(string name)
        {
            return CasinosLua.GetLuaTable(name);
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
        // 需要调整为可并发调用的接口
        public void LuaAsyncLoadLocalUiBundle(Action loaddone_callback, params string[] need_load_ab_path)
        {
            List<string> list_ab = new List<string>();
            foreach (var i in need_load_ab_path)
            {
                list_ab.Add(i);
            }

            AsyncAssetLoadGroup.asyncLoadLocalBundle(
                list_ab, _eAsyncAssetLoadType.LocalBundle, (List<AssetBundle> list_abex) =>
            {
                foreach (var i in list_abex)
                {
                    UIPackage.AddPackage(i);
                }

                loaddone_callback();
            });
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

        //---------------------------------------------------------------------
        void _checkLan()
        {
            if (UnityEngine.PlayerPrefs.HasKey(LanKey))
            {
                CurrentLan = UnityEngine.PlayerPrefs.GetString(LanKey);
            }
            else
            {
                CurrentLan = Application.systemLanguage.ToString();
            }
        }
    }
}

//---------------------------------------------------------------------
//public void PlayBackgroundSound(string snd_name, bool is_loop = false)
//{
//    //if (string.IsNullOrEmpty(snd_name))
//    //{
//    //    EbLog.Note("ClientSound.playBackgroundSound() 声音名称为空");
//    //}

//    //string full_snd_name = "Audio/" + snd_name;
//    //AudioClip audio_clip = (AudioClip)Resources.Load(full_snd_name, typeof(AudioClip));

//    //SoundManager.Play(audio_clip, is_loop);
//}

//---------------------------------------------------------------------
//public void SetLoadDataDone()
//{
//    LoadDataDone = true;

//    //var t_lanmgr = CasinosLua.LuaEnv.Global.Get<LuaTable>("LanMgr");
//    //var refresh_lan = t_lanmgr.Get<Action>("refreshLan");
//    //refresh_lan();

//    CasinosLua.LuaEnv.Global.Get<LuaTable>(LuaProjectListenerName);
//    ViewMgr = CasinosLua.LuaEnv.Global.Get<LuaTable>("ViewMgr");
//    ViewHelper = CasinosLua.LuaEnv.Global.Get<LuaTable>("ViewHelper");
//    UiEndWaiting = ViewHelper.Get<Action<LuaTable>>("UiEndWaiting");
//    CreateView = ViewMgr.Get<GetView>("createView");
//    GetView = ViewMgr.Get<GetView>("getView");
//    DestroyView = ViewMgr.Get<Action<LuaTable>>("destroyView");
//    if (ProjectListener != null)
//    {
//        ActionOnApplicationPause = ProjectListener.Get<Action<bool>>("OnApplicationPause");
//    }

//    //ControllerMgr = CasinosLua.LuaEnv.Global.Get<LuaTable>("ControllerMgr");
//    //CreateController = ControllerMgr.Get<CreateController>("CreateController");
//    //GetController = ControllerMgr.Get<GetController>("GetController");
//}

//---------------------------------------------------------------------
//public string GetChannelName()
//{
//    string bundle_id = Application.identifier;
//    string channel_name = bundle_id.Replace(Config.AppCommonIdentifier, "");
//    return channel_name;
//}