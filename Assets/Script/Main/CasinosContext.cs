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

    //public enum _eFriendStateClient
    //{
    //    Offline,
    //    Fishing,
    //    GFlowerDesktopH,
    //    GFlowerDesktopPrivate,
    //    GFlowerDesktopNormal,
    //    TexasDesktopClassicPrivate,
    //    TexasDesktopClassic,
    //    TexasDesktopMustBet,
    //    TexasDesktopMustBetPrivate,
    //    TexasDesktopH,
    //    NiuNiuDesktopH,
    //    ZhongFBDesktopH,
    //    NotInDesktop,
    //}

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
        public DelayMgr DelayMgr { get; private set; }
        public LuaMgr LuaMgr { get; private set; }
        public NetMgr NetMgr { get; private set; }
        public SpineMgr SpineMgr { get; private set; }
        public NativeMgr NativeMgr { get; set; }
        public NativeAPIMsgReceiverListener NativeAPIMsgReceiverListner { get; set; }
        public _eLoginType LoginType { get; set; }
        public bool UseHttps { get; set; }
        public bool CanReportLog { get; set; }
        public string CanReportLogDeviceId { get; set; }
        public string CanReportLogPlayerId { get; set; }
        public bool UnityAndroid { get; set; }
        public bool UnityIOS { get; set; }
        public bool IsEditor { get; set; }
        public bool IsSqliteUnity { get; set; }
        public LuaTable TbDataMgrLua { get; set; }
        FTMgr FTMgr { get; set; }
        HeadIconMgr HeadIconMgr { get; set; }
        SoundMgr SoundMgr { get; set; }

        //---------------------------------------------------------------------
        public CasinosContext(bool force_use_resouceslaunch, bool force_use_dataoss)
        {
            Instance = this;

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
            TimerShaft = new TimerShaft();
            MemoryStream = new MemoryStream();
            SB = new StringBuilder();
            FTMgr = new FTMgr();
            PathMgr = new PathMgr(editor_runsorce, force_use_resouceslaunch, force_use_dataoss);// 初始化PathMgr
            Config = new CasinosConfig(editor_runsorce);
            NativeMgr = new NativeMgr();
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

            if (AsyncAssetLoaderMgr == null)
            {
                AsyncAssetLoaderMgr = new AsyncAssetLoaderMgr();
            }
            AsyncAssetLoadGroup = AsyncAssetLoaderMgr.createAsyncAssetLoadGroup();
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

            if (FTMgr != null)
            {
                FTMgr.Update(elapsed_tm);
            }

            if (DelayMgr != null)
            {
                DelayMgr.Update(elapsed_tm);
            }

            if (NetMgr != null)
            {
                NetMgr.Update(elapsed_tm);
            }

            if (AsyncAssetLoaderMgr != null)
            {
                AsyncAssetLoaderMgr.Update(Time.deltaTime);
            }

            if (LuaMgr != null)
            {
                LuaMgr.Update(elapsed_tm);
            }

            if (TimerShaft != null)
            {
                TimerShaft.ProcessTimer((ulong)Stopwatch.ElapsedMilliseconds);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LuaMgr._CSharpCallOnAndroidQuitConfirm();
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

            if (SpineMgr != null)
            {
                SpineMgr.Destroy();
                SpineMgr = null;
            }

            AsyncAssetLoaderMgr = null;
            if (AsyncAssetLoadGroup != null)
            {
                AsyncAssetLoadGroup.destroy();
                AsyncAssetLoadGroup = null;
            }

            if (NetMgr != null)
            {
                NetMgr.Close();
                NetMgr = null;
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
            if (!version_persistent || PathMgr.ForceUseDirResourcesLaunch)
            {
                PathMgr.DirLaunchLua = "Lua/Launch/";
                PathMgr.DirLaunchLuaType = DirType.Resources;
                PathMgr.DirLaunchAb = "Resources.KingTexasLaunch/";
                PathMgr.DirLaunchAbType = DirType.Resources;
            }
            else
            {
                PathMgr.DirLaunchLua = PathMgr.CombinePersistentDataPath("Lua/Launch/");
                PathMgr.DirLaunchLuaType = DirType.Raw;
                PathMgr.DirLaunchAb = PathMgr.CombinePersistentDataPath("Resources.KingTexasLaunch/");
                PathMgr.DirLaunchAbType = DirType.Raw;
            }

            if (PathMgr.ForceUseDirDataOss)
            {
                string p = Path.Combine(Environment.CurrentDirectory, "./DataOss/");
                var di = new DirectoryInfo(p);
                string p1 = di.FullName.Replace('\\', '/');
                PathMgr.DirLuaRoot = p1 + "Common/Lua/";
                PathMgr.DirRawRoot = p1 + "Common/Raw/";
                PathMgr.DirAbRoot = p1 + Config.Platform + "/Resources.KingTexas/";
            }
            else
            {
                PathMgr.DirLuaRoot = PathMgr.CombinePersistentDataPath("Lua/");
                PathMgr.DirRawRoot = PathMgr.CombinePersistentDataPath("Raw/");
                PathMgr.DirAbRoot = PathMgr.CombinePersistentDataPath("Resources.KingTexas/");
            }

#if UNITY_EDITOR
            string info1 = string.Format("DirLaunchLua={0}, DirLaunchLuaType={1}",
                PathMgr.DirLaunchLua, PathMgr.DirLaunchLuaType);
            Debug.Log(info1);
            string info2 = string.Format("DirLaunchAb={0}, DirLaunchAbType={1}",
                PathMgr.DirLaunchAb, PathMgr.DirLaunchAbType);
            Debug.Log(info2);
            Debug.Log("DirLuaRoot=" + PathMgr.DirLuaRoot);
            Debug.Log("DirRawRoot=" + PathMgr.DirRawRoot);
            Debug.Log("DirAbRoot=" + PathMgr.DirAbRoot);
#endif

            PathMgr.DirAbUi = PathMgr.DirAbRoot + "Ui/";// "Resources.KingTexas/Ui/"，需动态计算
            PathMgr.DirAbCard = PathMgr.DirAbRoot + "Cards/";// "Resources.KingTexas/Cards/"，需动态计算
            PathMgr.DirAbAudio = PathMgr.DirAbRoot + "Audio/";// "Resources.KingTexas/Audio/"，需动态计算
            PathMgr.DirAbItem = PathMgr.DirAbRoot + "Item/";// "Resources.KingTexas/Item/"，需动态计算
            PathMgr.DirAbParticle = PathMgr.DirAbRoot + "Particle/";// "Resources.KingTexas/Particle/"，需动态计算

            LuaMgr.Launch();
        }

        //-------------------------------------------------------------------------
        public void OnApplicationPause(bool pause)
        {
            LuaMgr._CSharpCallOnApplicationPause(pause);
        }

        //-------------------------------------------------------------------------
        public void OnApplicationFocus(bool focus_status)
        {
            LuaMgr._CSharpCallOnApplicationFocus(focus_status);
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
#if UNITY_IOS
            NativeOperateType operate = (NativeOperateType)native_operate;
            NativeMgr.nativeOperate(operate);
#endif
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

// 读取VersionLaunchStreamingAssets
//var mb_helper = Config.GoMain.GetComponent<MbHelper>();
//var datafilelist_path = PathMgr.combineWWWStreamingAssetsPath("StreamingAssetsInfo.json");
//Debug.Log("4444444444");
//mb_helper.WWWDownload(datafilelist_path,
//    (w) =>
//    {
//        Debug.Log("555555555555");

//        //Config.StreamingAssetsInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<StreamingAssetsInfo>(w.text);
//        Config.StreamingAssetsInfo = JsonUtility.FromJson<StreamingAssetsInfo>(w.text);

//        // 比较Launch版本，决定是否将Launch从StreamingAssets拷贝到Persistent
//        bool need_copy = false;
//        if (string.IsNullOrEmpty(Config.VersionLaunchPersistent)
//            || Config.VersionLaunchPersistent != Config.StreamingAssetsInfo.DataVersion)
//        {
//            need_copy = true;
//        }

//        // 将Launch从StreamingAssets拷贝到Persistent
//        if (need_copy)
//        {
//            var l = Config.StreamingAssetsInfo.ListLaunchFile;
//            Dictionary<string, string> map_copyfile = new Dictionary<string, string>(l.Count);
//            foreach (var i in l)
//            {
//                var path_www_streamingassets_file = PathMgr.combineWWWStreamingAssetsPath(i);
//                map_copyfile[path_www_streamingassets_file] = i;
//            }
//            Debug.Log("6666666666666");
//            mb_helper.WWWDownloadList(new List<string>(map_copyfile.Keys),
//                (arr) =>
//                {
//                    Debug.Log("777777777777");
//                    foreach (var www in arr)
//                    {
//                        string path_www_streamingassets_file = map_copyfile[www.url];
//                        var str = PathMgr.combinePersistentDataPath(path_www_streamingassets_file);
//                        string d = Path.GetDirectoryName(str);
//                        if (!Directory.Exists(d))
//                        {
//                            Directory.CreateDirectory(d);
//                        }

//                        using (FileStream fs = new FileStream(str, FileMode.Create))
//                        {
//                            fs.Write(www.bytes, 0, www.bytes.Length);
//                        }
//                    }

//                    Debug.Log("8888888888888");

//                    Config.WriteVersionLaunchPersistent(Config.StreamingAssetsInfo.DataVersion);

//                    CasinosLua.Launch();
//                });
//        }
//        else
//        {
//            CasinosLua.Launch();
//        }
//    });

//---------------------------------------------------------------------
//public static string GetMaJiangCardResName(CardData card_data)
//{
//    string card_res_name = "";
//    MaJiangSuit majiang_suit = (MaJiangSuit)card_data.suit;
//    if (majiang_suit != MaJiangSuit.Wan && majiang_suit != MaJiangSuit.Tong
//        && majiang_suit != MaJiangSuit.Tiao)
//    {
//        card_res_name = majiang_suit.ToString();
//    }
//    else
//    {
//        MaJiangType majiang_type = (MaJiangType)card_data.type;
//        card_res_name = majiang_type.ToString() + majiang_suit.ToString();
//    }

//    return card_res_name;
//}

//---------------------------------------------------------------------
//public static void setParticle(GComponent g, string particle_resouce_path)
//{
//    var particle_parent = g.GetChild("ParticleParent").asGraph;
//    GameObject particle = (GameObject)GameObject.Instantiate(Resources.Load(particle_resouce_path));
//    particle_parent.SetNativeObject(new GoWrapper(particle));
//}

//---------------------------------------------------------------------
//public static void SetCommonBgParticle(GComponent co_commonbg)
//{
//    var particleblue_parent = co_commonbg.GetChild("ParticleBlue").asGraph;
//    SetParticle(particleblue_parent, "Particle/StarBlue");
//    //var particleyellow_parent = co_commonbg.GetChild("ParticleYellow").asGraph;
//    //GameObject yellow = (GameObject)GameObject.Instantiate(Resources.Load("Particle/StarYellow"));
//    //particleyellow_parent.SetNativeObject(new GoWrapper(yellow));
//    var particleyellowright_parent = co_commonbg.GetChild("ParticleYellowRight").asGraph;
//    SetParticle(particleyellowright_parent, "Particle/StarYellowRight");
//}

//---------------------------------------------------------------------
//public static void SetLoadingBgParticle(GComponent co_commonbg)
//{
//    var particleblue_parent = co_commonbg.GetChild("ParticleTop").asGraph;
//    SetParticle(particleblue_parent, "Particle/Star");
//    var particleyellow_parent = co_commonbg.GetChild("ParticleLeft").asGraph;
//    SetParticle(particleyellow_parent, "Particle/Star");
//    var particleyellowright_parent = co_commonbg.GetChild("ParticleRight").asGraph;
//    SetParticle(particleyellowright_parent, "Particle/Star");
//}

//---------------------------------------------------------------------
//public static void SetParticle(GGraph graph, string particle_name)
//{
//    GameObject particle = (GameObject)GameObject.Instantiate(Resources.Load(particle_name));
//    graph.SetNativeObject(new GoWrapper(particle));
//}