// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using XLua;
    using GameCloud.Unity.Common;

    [CSharpCallLua]
    public delegate void DelegateLua1(LuaTable lua_table);

    [CSharpCallLua]
    public delegate void DelegateLua2(LuaTable lua_table, float tm);

    [CSharpCallLua]
    public delegate void DelegateLua3(LuaTable lua_table, bool b);

    [CSharpCallLua]
    public delegate void DelegateLua4(LuaTable lua_table, List<AssetBundle> list_ab);

    public static class LuaCustomSettings
    {
        //---------------------------------------------------------------------
        [LuaCallCSharp]
        public static List<Type> CustomType = new List<Type>()
        {
            // Unity3D
            typeof(UnityEngine.ApplicationInstallMode),
            typeof(UnityEngine.ApplicationSandboxType),
            typeof(UnityEngine.CameraType),
            typeof(UnityEngine.CameraClearFlags),
            typeof(UnityEngine.NetworkReachability),
            typeof(UnityEngine.RuntimePlatform),
            typeof(UnityEngine.SystemLanguage),
            typeof(UnityEngine.ThreadPriority),
            typeof(UnityEngine.Application),
            typeof(UnityEngine.AssetBundle),
            typeof(UnityEngine.AudioClip),
            typeof(UnityEngine.AudioSource),
            typeof(UnityEngine.Camera),
            typeof(UnityEngine.Color),
            typeof(UnityEngine.Color32),
            typeof(UnityEngine.Coroutine),
            typeof(UnityEngine.Font),
            typeof(UnityEngine.GameObject),
            typeof(UnityEngine.Hash128),
            typeof(UnityEngine.Material),
            typeof(UnityEngine.Mathf),
            typeof(UnityEngine.Matrix4x4),
            typeof(UnityEngine.Mesh),
            typeof(UnityEngine.ParticleSystem),
            typeof(UnityEngine.Ping),
            typeof(UnityEngine.PlayerPrefs),
            typeof(UnityEngine.Quaternion),
            typeof(UnityEngine.Random),
            typeof(UnityEngine.Rect),
            typeof(UnityEngine.Renderer),
            typeof(UnityEngine.Resources),
            typeof(UnityEngine.Screen),
            typeof(UnityEngine.ScreenCapture),
            typeof(UnityEngine.SystemInfo),
            typeof(UnityEngine.Texture3D),
            typeof(UnityEngine.Time),
            typeof(UnityEngine.Networking.UnityWebRequest),
            typeof(UnityEngine.Vector3),
            typeof(UnityEngine.Vector2),
            typeof(UnityEngine.Vector4),
            typeof(UnityEngine.WWW),
            typeof(UnityEngine.WWWForm),
            //typeof(UnityEngine.Texture),
            //typeof(UnityEngine.Texture2D),
            //typeof(UnityEngine.MovieTexture),
            //typeof(UnityEngine.AudioSettings),
            //typeof(UnityEngine.Input),
            
            // System            
            typeof(Action),
            typeof(Action<string, WWW>),
            typeof(BitConverter),
            typeof(Array),
            typeof(Hashtable),
            typeof(List<string>),
            typeof(List<AssetBundle>),
            typeof(System.Text.RegularExpressions.Regex),
            typeof(System.Text.RegularExpressions.Match),
            typeof(System.Text.RegularExpressions.MatchCollection),
            typeof(System.IO.Directory),
            typeof(System.IO.File),
            typeof(System.IO.FileStream),
            typeof(System.IO.MemoryStream),
            typeof(System.IO.Path),
            
            // FairyGUI
            typeof(FairyGUI.EaseType),
            typeof(FairyGUI.RelationType),
            typeof(FairyGUI.TweenPropType),
            typeof(FairyGUI.Container),
            typeof(FairyGUI.Controller),
            typeof(FairyGUI.DisplayObject),
            typeof(FairyGUI.EventCallback0),
            typeof(FairyGUI.EventCallback1),
            typeof(FairyGUI.EventContext),
            typeof(FairyGUI.EventDispatcher),
            typeof(FairyGUI.EventListener),
            typeof(FairyGUI.InputEvent),
            typeof(FairyGUI.GObject),
            typeof(FairyGUI.GGraph),
            typeof(FairyGUI.GGroup),
            typeof(FairyGUI.GImage),
            typeof(FairyGUI.GLoader),
            typeof(FairyGUI.GMovieClip),
            typeof(FairyGUI.GTextField),
            typeof(FairyGUI.GRichTextField),
            typeof(FairyGUI.GTextInput),
            typeof(FairyGUI.GComponent),
            typeof(FairyGUI.GList),
            typeof(FairyGUI.GRoot),
            typeof(FairyGUI.GLabel),
            typeof(FairyGUI.GButton),
            typeof(FairyGUI.GComboBox),
            typeof(FairyGUI.GObjectPool),
            typeof(FairyGUI.GProgressBar),
            typeof(FairyGUI.GSlider),
            typeof(FairyGUI.GTween),
            typeof(FairyGUI.GTweener),
            typeof(FairyGUI.GTweenCallback),
            typeof(FairyGUI.GTweenCallback1),
            typeof(FairyGUI.ListItemRenderer),
            typeof(FairyGUI.Stage),
            typeof(FairyGUI.PlayCompleteCallback),
            typeof(FairyGUI.PopupMenu),
            typeof(FairyGUI.Relations),
            typeof(FairyGUI.ScrollPane),
            typeof(FairyGUI.TextFormat),
            typeof(FairyGUI.TimerCallback),
            typeof(FairyGUI.Transition),
            typeof(FairyGUI.TransitionHook),
            typeof(FairyGUI.TweenValue),
            typeof(FairyGUI.UIPackage),
            typeof(FairyGUI.Window),

            // Casinos
            typeof(_eProjectItemDisplayNameKey),
            typeof(_eAsyncAssetLoadType),
            typeof(_ePayType),
            typeof(AsyncAssetLoaderMgr),
            typeof(AsyncAssetLoadGroup),
            typeof(Card),
            typeof(ChatParser),
            typeof(CopyStreamingAssetsToPersistentData2),
            typeof(DataEye),
            typeof(EbTool),
            typeof(EbTimeEvent),
            typeof(EbTimer),
            typeof(EbDoubleLinkNode<EbTimeEvent>),
            typeof(EbDoubleLinkList<EbTimeEvent>),
            typeof(EbTimeWheel),
            typeof(LoaderTicket),
            typeof(MbHelper),
            typeof(NativeFun),
            typeof(OpenInstall),
            typeof(OpenInstallReceiver),
            typeof(Push),
            typeof(PushReceiver),
            typeof(QRCodeMaker),
            typeof(ShareSDKReceiver),
            typeof(TcpClient),
            typeof(ThirdPartyLogin),
            typeof(TimerShaft),
            typeof(UiHelper),
            typeof(UpdateRemoteToPersistentData),
            //typeof(UiSoundMgr),
            //typeof(DevInfoSet),
            //typeof(MobLink),
            //typeof(MobLinkScene),
            //typeof(MobLinkReceiver),
            //typeof(UniWebView),
            //typeof(UniWebViewMessage),

            // SDKs
            typeof(ZXing.BarcodeFormat),
            typeof(BuglyAgent),
            typeof(OnePF.Purchase),
            typeof(cn.sharesdk.unity3d.ShareContent),
            typeof(cn.sharesdk.unity3d.ShareSDK),
            typeof(ZXing.BarcodeWriter),
            typeof(ZXing.QrCode.QrCodeEncodingOptions),
        };

        //---------------------------------------------------------------------
        [CSharpCallLua]
        public static List<Type> CustomType2 = new List<Type>()
        {
            typeof(Action),
            typeof(Action<string>),
            typeof(Action<string[]>),
            typeof(Action<byte[]>),
            typeof(Action<LuaTable>),
            typeof(Action<LuaTable,string>),
            typeof(Action<LuaTable,string,float>),
            typeof(Action<float>),
            typeof(Action<string,Action>),
            typeof(Action<string,Action,Action>),
            typeof(Action<string,WWW>),
            typeof(Action<string,string>),
            typeof(Action<string,int>),
            typeof(Action<string,float>),
            typeof(Action<string,bool>),
            typeof(Action<string,string,string>),
            typeof(Action<int, int>),
            typeof(Action<int>),
            typeof(Action<long>),
            typeof(Action<bool>),
            typeof(Action<float, string, Dictionary<byte, string>>),
            typeof(Action<float, long>),
            typeof(Action<Dictionary<string, long>>),
            typeof(Action<string, long>),
            typeof(Action<Dictionary<byte, long>>),
            typeof(Action<Dictionary<byte, long>, Dictionary<byte, long>>),
            typeof(Action<LoaderTicket, string, UnityEngine.Object>),
            typeof(Action<Dictionary<string,Dictionary<string,string>>>),
            typeof(Action<ushort,byte[]>),
            typeof(Action<OnePF.Purchase>),
            typeof(List<string>),
            typeof(List<AssetBundle>),
            typeof(OnSocketReceive),
            typeof(OnSocketConnected),
            typeof(OnSocketClosed),
            typeof(OnSocketError),
            typeof(Action<UnityEngine.Object, LoaderTicket>),
            typeof(Action<LoaderTicket, Texture>),
            typeof(AllTaskDoneCallBack),
            typeof(EventHandler),

            // FairyGUI
            typeof(FairyGUI.EventCallback0),
            typeof(FairyGUI.EventCallback1),
            typeof(FairyGUI.EventContext),
            typeof(FairyGUI.EventListener),
            typeof(FairyGUI.GTweenCallback),
            typeof(FairyGUI.GTweenCallback1),
            typeof(FairyGUI.ListItemRenderer),

            // SDKs
            typeof(cn.sharesdk.unity3d.ShareSDK.EventHandler),

            //typeof(MobLink.GetMobIdHandler),
            //typeof(MobLink.RestoreSceneHandler),
            //typeof(UniWebView.PageStartedDelegate),
            //typeof(UniWebView.PageFinishedDelegate),
            //typeof(UniWebView.PageErrorReceivedDelegate),
            //typeof(UniWebView.MessageReceivedDelegate),
            //typeof(UniWebView.ShouldCloseDelegate),
            //typeof(UniWebView.KeyCodeReceivedDelegate),
            //typeof(UniWebView.OreintationChangedDelegate),
            //typeof(UniWebView.OnWebContentProcessTerminatedDelegate),
        };
    }

    [LuaCallCSharp]
    public class CasinosLua
    {
        //---------------------------------------------------------------------
        public LuaEnv LuaEnv { get; private set; }
        Dictionary<string, byte[]> MapLuaFiles { get; set; }
        DelegateLua1 FuncLaunchClose { get; set; }
        DelegateLua3 FuncLaunchOnApplicationPause { get; set; }
        DelegateLua3 FuncLaunchOnApplicationFocus { get; set; }
        CasinosContext Context { get; set; }

        float TickTimeSpanLast = 0;
        const float TickTimeSpanMax = 1f;

        //---------------------------------------------------------------------
        public CasinosLua()
        {
            Context = CasinosContext.Instance;
            MapLuaFiles = new Dictionary<string, byte[]>();
            LuaEnv = new LuaEnv();
            LuaEnv.AddLoader(LuaLoaderCustom);
        }

        //---------------------------------------------------------------------
        public void Release()
        {
            var lua_launch = LuaEnv.Global.Get<LuaTable>("Launch");
            if (FuncLaunchClose != null) FuncLaunchClose.Invoke(lua_launch);
            FuncLaunchClose = null;
            FuncLaunchOnApplicationPause = null;
            FuncLaunchOnApplicationFocus = null;

            if (LuaEnv != null)
            {
                LuaEnv.Dispose();
                LuaEnv = null;
            }
        }

        //---------------------------------------------------------------------
        public void Update(float elapsed_tm)
        {
            TickTimeSpanLast += elapsed_tm;
            if (LuaEnv != null && TickTimeSpanLast > TickTimeSpanMax)
            {
                TickTimeSpanLast = 0f;
                LuaEnv.Tick();
            }
        }

        //---------------------------------------------------------------------
        public void Launch()
        {
            Debug.Log("99999999999");

            // 预加载Script.Lua/Launch中的所有lua文件，显示加载界面
            var path_mgr = CasinosContext.Instance.PathMgr;
            var path_launch = path_mgr.combinePersistentDataPath("Script.Lua/Launch/");
            string[] list_path = new string[] { path_launch };
            LoadLuaFromDir2(list_path);
            Debug.Log("aaaaaaaaaa");
            DoString("Launch");
            Debug.Log("bbbbbbbbb");
            var lua_launch = LuaEnv.Global.Get<LuaTable>("Launch");
            FuncLaunchClose = lua_launch.Get<DelegateLua1>("Close");
            FuncLaunchOnApplicationPause = lua_launch.Get<DelegateLua3>("OnApplicationPause");
            FuncLaunchOnApplicationFocus = lua_launch.Get<DelegateLua3>("OnApplicationFocus");
            var func_setup = lua_launch.Get<DelegateLua1>("Setup");
            func_setup(lua_launch);
            Debug.Log("ccccccccc");
        }

        //---------------------------------------------------------------------
        public void LoadLuaFromBytes(string file_name, string text)
        {
            string name = file_name.ToLower();
            string name1 = name.Replace(".lua", "");

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
            MapLuaFiles[name1] = bytes;
        }

        //---------------------------------------------------------------------
        public void LoadLuaFromDir(string path)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            System.IO.FileInfo[] file_list = dir.GetFiles("*.lua", System.IO.SearchOption.AllDirectories);
            foreach (System.IO.FileInfo i in file_list)
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(i.FullName, System.IO.FileMode.Open))
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(fs))
                    {
                        string s = sr.ReadToEnd();
                        byte[] data = System.Text.Encoding.UTF8.GetBytes(s);

                        string name = i.Name.ToLower();
                        string name1 = name.Replace(".lua", "");
                        MapLuaFiles[name1] = data;
                    }
                }
            }

            Debug.Log("LoadLuaFromDir() 文件数量=" + file_list.Length);
        }

        //---------------------------------------------------------------------
        public void LoadLuaFromDir2(string[] list_path)
        {
            foreach (var k in list_path)
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(k);
                System.IO.FileInfo[] file_list = dir.GetFiles("*.lua", System.IO.SearchOption.AllDirectories);
                foreach (System.IO.FileInfo i in file_list)
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(i.FullName, System.IO.FileMode.Open))
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(fs))
                        {
                            string s = sr.ReadToEnd();
                            byte[] data = System.Text.Encoding.UTF8.GetBytes(s);

                            string name = i.Name.ToLower();
                            string name1 = name.Replace(".lua", "");
                            MapLuaFiles[name1] = data;
                        }
                    }
                }
            }
        }

        //---------------------------------------------------------------------
        public void DoString(string luafile_name)
        {
            string do_string = string.Format("require '{0}'", luafile_name);
            LuaEnv.DoString(do_string);
        }

        //---------------------------------------------------------------------
        public LuaTable GetLuaTable(string name)
        {
            return CasinosContext.Instance.CasinosLua.LuaEnv.Global.Get<LuaTable>(name);
        }

        //---------------------------------------------------------------------
        // 异步加载本地的AssetBundle
        public LoaderTicket LoadLocalBundleAsync(LuaTable lua_table, LuaTable need_load_ab_path, DelegateLua4 loaded_callback)
        {
            List<string> list_ab_name = new List<string>();

            need_load_ab_path.ForEach<int, string>(
                (i, v) =>
                {
                    list_ab_name.Add(v);
                });

            var ticket = Context.AsyncAssetLoadGroup.asyncLoadLocalBundle(
                 list_ab_name, _eAsyncAssetLoadType.LocalBundle, (List<AssetBundle> list_ab1) =>
                 {
                     if (loaded_callback != null)
                     {
                         loaded_callback.Invoke(lua_table, list_ab1);
                         loaded_callback = null;
                     }
                 });

            return ticket;
        }

        //---------------------------------------------------------------------
        public LuaTable TransListToLuaTable<T>(List<T> list_t)
        {
            LuaTable l_t = LuaEnv.NewTable();
            for (int i = 0; i < list_t.Count; i++)
            {
                l_t.Set(i, list_t[i]);
            }

            return l_t;
        }

        //---------------------------------------------------------------------
        public LuaTable TransDicToLuaTable<TKey, TValue>(Dictionary<TKey, TValue> map)
        {
            LuaTable l_t = LuaEnv.NewTable();
            foreach (var i in map)
            {
                l_t.Set(i.Key, i.Value);
            }

            return l_t;
        }

        //---------------------------------------------------------------------
        // 读取整个指定的文本文件
        public string ReadAllText(string full_filename)
        {
            string all_text = string.Empty;
            if (System.IO.File.Exists(full_filename))
            {
                all_text = System.IO.File.ReadAllText(full_filename);
            }
            return all_text;
        }

        //---------------------------------------------------------------------
        // 分割字符串
        public LuaTable SpliteStr(string str, string splite_s)
        {
            var splite_str = str.Split(new string[] { splite_s }, StringSplitOptions.RemoveEmptyEntries);
            var t = LuaEnv.NewTable();
            int index = 1;
            foreach (var i in splite_str)
            {
                t.Set(index, i);
                index++;
            }

            return t;
        }

        //---------------------------------------------------------------------
        public void DestroyGameObject(GameObject o)
        {
            UnityEngine.Object.Destroy(o);
        }

        //---------------------------------------------------------------------
        public void Vibrate()
        {
            Handheld.Vibrate();
        }

        //---------------------------------------------------------------------
        public string GetSystemLanguageAsString()
        {
            return Application.systemLanguage.ToString();
        }

        //---------------------------------------------------------------------
        public void _CSharpCallOnApplicationPause(bool pause)
        {
            var lua_launch = LuaEnv.Global.Get<LuaTable>("Launch");
            if (FuncLaunchOnApplicationPause != null) FuncLaunchOnApplicationPause.Invoke(lua_launch, pause);
        }

        //---------------------------------------------------------------------
        public void _CSharpCallOnApplicationFocus(bool focus_status)
        {
            var lua_launch = LuaEnv.Global.Get<LuaTable>("Launch");
            if (FuncLaunchOnApplicationFocus != null) FuncLaunchOnApplicationFocus.Invoke(lua_launch, focus_status);
        }

        //---------------------------------------------------------------------
        // 自定义Lua文件加载函数
        byte[] LuaLoaderCustom(ref string file_name)
        {
            var key = file_name.ToLower();

            byte[] array_file = null;
            MapLuaFiles.TryGetValue(key, out array_file);
            if (array_file != null)
            {
                return array_file;
            }

            return null;
        }
    }
}