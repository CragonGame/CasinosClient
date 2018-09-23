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
        public MemoryStream MemoryStream { get; private set; }
        public StringBuilder SB { get; private set; }
        public AsyncAssetLoaderMgr AsyncAssetLoaderMgr { get; private set; }
        public PathMgr PathMgr { get; private set; }
        public CasinosPlayerPrefs PlayerPrefs { get; private set; }
        public CasinosConfig Config { get; private set; }
        public CasinosListener Listener { get; private set; }
        public TextureMgr TextureMgr { get; private set; }
        public CasinosLua CasinosLua { get; private set; }
        public NetBridge NetBridge { get; private set; }
        public NativeMgr NativeMgr { get; set; }
        public bool IsDev { get; set; }
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
        public LuaTable MainCLua { get; set; }
        public LuaTable ProjectListener { get; set; }
        public LuaTable TbDataMgrLua { get; set; }
        public LuaTable PreViewMgr { get; set; }
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
        public LuaTable ViewMgr { get; set; }
        public LuaTable ViewHelper { get; set; }
        public Action<LuaTable> UiEndWaiting { get; set; }
        public Action<bool> ActionOnApplicationPause { get; set; }
        public AsyncAssetLoadGroup AsyncAssetLoadGroup { get; private set; }
        public NativeAPIMsgReceiverListener NativeAPIMsgReceiverListner { get; set; }
        GetView CreateView { get; set; }
        GetView GetView { get; set; }
        Action<LuaTable> DestroyView { get; set; }
        GetView CreatePreView { get; set; }
        GetView GetPreView { get; set; }
        Action<LuaTable> DestroyPreView { get; set; }
        FTMgr FTMgr { get; set; }
        HeadIconMgr HeadIconMgr { get; set; }
        CSoundMgr SoundMgr { get; set; }
        string LuaProjectListenerName { get; set; }
        string PlatformName { get; set; }

        public const string LocalDataVersionKey = "LocalVersionInfo";
        public const string PreDataVersionKey = "PreDataVersion";
        public const string LanKey = "LanKey";

        //public CopyStreamingAssetsToPersistentDataPath CopyStreamingAssetsToPersistentDataPath { get; set; }
        //public Launch Launch { get; set; }

        //---------------------------------------------------------------------
        public CasinosContext(CasinosListener listener,
            bool use_persistent,
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

#if IsDev
            IsDev = true;
#else
            IsDev = false;
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
            TimerShaft = new TimerShaft();

            Listener = listener;
            MemoryStream = new MemoryStream();
            SB = new StringBuilder();
            Config = new CasinosConfig(editor_runsorce);

            //Config = GameObject.FindObjectOfType<MbCasinosConfig>();
            //Config.FormatConfigUrl();
            //UserConfig = GameObject.FindObjectOfType<MbCasinosUserConfig>();

            // 初始化系统参数
            {
#if UNITY_EDITOR
                Application.runInBackground = false;
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
#else
                Application.runInBackground = true;
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif
                Time.fixedDeltaTime = 0.03f;
                QualitySettings.vSyncCount = 1;
                //Application.targetFrameRate = 30;
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

            //CopyStreamingAssetsToPersistentDataPath = new CopyStreamingAssetsToPersistentDataPath();
            //Launch = new Launch();

            HeadIconMgr = new HeadIconMgr();
        }

        //---------------------------------------------------------------------
        public void Update(float elapsed_tm)
        {
            if (SoundMgr != null)
            {
                SoundMgr.update();
            }

            if (FTMgr != null)
            {
                FTMgr.update(elapsed_tm);
            }

            if (NetBridge != null)
            {
                NetBridge.update(elapsed_tm);
            }

            if (AsyncAssetLoaderMgr != null)
            {
                AsyncAssetLoaderMgr.update(Time.deltaTime);
            }

            if (CasinosLua != null)
            {
                CasinosLua.Update(elapsed_tm);
            }

            if (TimerShaft != null)
            {
                TimerShaft.ProcessTimer((ulong)(Time.deltaTime * 1000));
            }

            //if (CopyStreamingAssetsToPersistentDataPath != null)
            //{
            //    CopyStreamingAssetsToPersistentDataPath.update(elapsed_tm);
            //}
        }

        //---------------------------------------------------------------------
        public void FixedUpdate()
        {
            Listener.OnFixedUpdate(Time.fixedDeltaTime);
        }

        //---------------------------------------------------------------------
        public void Close()
        {
            if (SoundMgr != null)
            {
                SoundMgr.destroy();
                SoundMgr = null;
            }

            if (MemoryStream != null)
            {
                MemoryStream.Close();
                MemoryStream = null;
            }

            if (CasinosLua != null)
            {
                CasinosLua.Release();
                CasinosLua = null;
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

            Screen.sleepTimeout = SleepTimeout.SystemSetting;

            EbLog.Note("Casinos Close()");
        }

        //---------------------------------------------------------------------
        public void Launch()
        {
            var launch = new CasinosLaunch();

            // 解析全局函数
            //FuncLuaInit = CasinosLua.LuaEnv.Global.Get<Action>("MainCInit");
            //FuncLuaUpdate = LuaEnv.Global.Get<DelegateLuaUpdate>("MainCUpdate");
            //FuncLuaRelease = LuaEnv.Global.Get<Action>("MainCRelease");
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
        //public void copyStreamingAssetsToPersistentDataPath()
        //{
        //    Launch.copyStreamingAssetsToPersistentDataPath();
        //}

        //---------------------------------------------------------------------
        public void LoadConfigDone()
        {
        }

        //---------------------------------------------------------------------
        public void SetLoadDataDone()
        {
            LoadDataDone = true;

            //var t_lanmgr = CasinosLua.LuaEnv.Global.Get<LuaTable>("LanMgr");
            //var refresh_lan = t_lanmgr.Get<Action>("refreshLan");
            //refresh_lan();

            CasinosLua.LuaEnv.Global.Get<LuaTable>(LuaProjectListenerName);
            ViewMgr = CasinosLua.LuaEnv.Global.Get<LuaTable>("ViewMgr");
            ViewHelper = CasinosLua.LuaEnv.Global.Get<LuaTable>("ViewHelper");
            UiEndWaiting = ViewHelper.Get<Action<LuaTable>>("UiEndWaiting");
            CreateView = ViewMgr.Get<GetView>("createView");
            GetView = ViewMgr.Get<GetView>("getView");
            DestroyView = ViewMgr.Get<Action<LuaTable>>("destroyView");
            if (ProjectListener != null)
            {
                ActionOnApplicationPause = ProjectListener.Get<Action<bool>>("OnApplicationPause");
            }

            //ControllerMgr = CasinosLua.LuaEnv.Global.Get<LuaTable>("ControllerMgr");
            //CreateController = ControllerMgr.Get<CreateController>("CreateController");
            //GetController = ControllerMgr.Get<GetController>("GetController");
        }

        //---------------------------------------------------------------------
        public void SetNativeOperate(int native_operate)
        {
#if UNITY_IOS
            NativeOperateType operate = (NativeOperateType)native_operate;
            CasinosContext.Instance.NativeMgr.nativeOperate(operate);
#endif
        }

        //---------------------------------------------------------------------
        public void SetPreViewMgr()
        {
            PreViewMgr = CasinosLua.LuaEnv.Global.Get<LuaTable>("PreViewMgr");
            var action_new = PreViewMgr.Get<Action<LuaTable>>("new");
            action_new(CasinosLua.LuaEnv.NewTable());
            var action_oncreate = PreViewMgr.Get<Action>("onCreate");
            action_oncreate();
            CreatePreView = PreViewMgr.Get<GetView>("createView");
            GetPreView = PreViewMgr.Get<GetView>("getView");
            DestroyPreView = PreViewMgr.Get<Action<LuaTable>>("destroyView");
        }

        //---------------------------------------------------------------------
        public LuaTable GetLuaTable(string name)
        {
            return CasinosLua.GetLuaTable(name);
        }

        //---------------------------------------------------------------------
        public LuaTable createView(string view_name)
        {
            return CreateView(view_name);
        }

        //---------------------------------------------------------------------
        public LuaTable getView(string view_name)
        {
            return GetView(view_name);
        }

        //---------------------------------------------------------------------
        public void destroyView(LuaTable lua_table)
        {
            DestroyView(lua_table);
        }

        //---------------------------------------------------------------------
        public LuaTable createPreView(string view_name)
        {
            return CreatePreView(view_name);
        }

        //---------------------------------------------------------------------
        public LuaTable getPreView(string view_name)
        {
            return GetPreView(view_name);
        }

        //---------------------------------------------------------------------
        public void destroyPreView(LuaTable lua_table)
        {
            DestroyPreView(lua_table);
        }

        //---------------------------------------------------------------------
        //public string GetChannelName()
        //{
        //    string bundle_id = Application.identifier;
        //    string channel_name = bundle_id.Replace(Config.AppCommonIdentifier, "");
        //    return channel_name;
        //}

        //---------------------------------------------------------------------
        public string GetMainCUrl()
        {
            //ClearSB();
            SB.Clear();
            SB.Append("https://cragon-king-oss.cragon.cn/");

#if UNITY_ANDROID
            SB.Append("ANDROID");
#elif UNITY_IPHONE
            SB.Append("IOS");
#endif
            SB.Append("/Bundle/");
            SB.Append(Application.version);
            SB.Append("/MainC.lua");

            return SB.ToString();
        }

        //---------------------------------------------------------------------
        public void ClearSB()
        {
            SB.Clear();
            //SB.Length = 0;
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
        //public void RegLuaFilePath(string luafile_relativepath, params string[] luafile_name)
        //{
        //    CasinosLua.RegLuaFilePath(luafile_relativepath, luafile_name);
        //}

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
        public void PlayBackgroundSound(string snd_name, bool is_loop = false)
        {
            //if (string.IsNullOrEmpty(snd_name))
            //{
            //    EbLog.Note("ClientSound.playBackgroundSound() 声音名称为空");
            //}

            //string full_snd_name = "Audio/" + snd_name;
            //AudioClip audio_clip = (AudioClip)Resources.Load(full_snd_name, typeof(AudioClip));

            //SoundManager.Play(audio_clip, is_loop);
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