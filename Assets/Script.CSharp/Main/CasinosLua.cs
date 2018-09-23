// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    using ZXing;
    using ZXing.QrCode;
    using XLua;
    using cn.sharesdk.unity3d;
    using GameCloud.Unity.Common;

    //---------------------------------------------------------------------
    [CSharpCallLua]
    public delegate void DelegateLua1(LuaTable lua_table);

    [CSharpCallLua]
    public delegate void DelegateLuaUpdate(float tm);

    [CSharpCallLua]
    public delegate LuaTable GetView(string view_key);

    [CSharpCallLua]
    public delegate LuaTable CreateController(LuaTable lua_table, string controller_name, Dictionary<string, string> controller_data);

    [CSharpCallLua]
    public delegate LuaTable GetController(string controller_name);

    //---------------------------------------------------------------------
    public static class LuaCustomSettings
    {
        //---------------------------------------------------------------------
        [LuaCallCSharp]
        public static List<Type> CustomType = new List<Type>()
        {
            // Unity3D
            typeof(UnityEngine.AssetBundle),
            typeof(UnityEngine.Application),
            typeof(UnityEngine.ScreenCapture),
            typeof(UnityEngine.GameObject),
            typeof(UnityEngine.PlayerPrefs),
            typeof(UnityEngine.Rect),
            typeof(UnityEngine.ParticleSystem),
            typeof(UnityEngine.Renderer),
            typeof(UnityEngine.Resources),
            typeof(UnityEngine.AudioClip),
            typeof(UnityEngine.AudioSource),
            typeof(UnityEngine.Camera),
            typeof(UnityEngine.Color),
            typeof(UnityEngine.Color32),
            typeof(UnityEngine.Coroutine),
            typeof(UnityEngine.Font),
            typeof(UnityEngine.Handheld),
            typeof(UnityEngine.Hash128),
            typeof(UnityEngine.Material),
            typeof(UnityEngine.Mesh),
            typeof(UnityEngine.Mathf),
            typeof(UnityEngine.Matrix4x4),
            typeof(UnityEngine.WWW),
            typeof(UnityEngine.WWWForm),
            typeof(UnityEngine.Ping),
            typeof(UnityEngine.Quaternion),
            typeof(UnityEngine.Screen),
            typeof(UnityEngine.SystemInfo),
            typeof(UnityEngine.Random),
            typeof(UnityEngine.Vector3),
            typeof(UnityEngine.Vector2),
            typeof(UnityEngine.Vector4),
            typeof(UnityEngine.Time),
            //typeof(UnityEngine.QualitySettings),
            //typeof(UnityEngine.Texture),
            //typeof(UnityEngine.Texture2D),
            //typeof(UnityEngine.Texture3D),
            //typeof(UnityEngine.MovieTexture),
            //typeof(UnityEngine.AudioSettings),
            //typeof(UnityEngine.Input),
            
            // Sys            
            typeof(Action),
            typeof(Action<string, WWW>),
            typeof(BitConverter),
            typeof(Array),
            typeof(Hashtable),
            typeof(System.Text.RegularExpressions.Regex),
            typeof(System.Text.RegularExpressions.Match),
            typeof(System.Text.RegularExpressions.MatchCollection),           
            //typeof(System.IO.Directory),
            //typeof(System.IO.File),
            //typeof(System.IO.FileStream),
            //typeof(System.IO.Path),
            //typeof(System.IO.MemoryStream),
            //typeof(System.Xml.XmlDocument),
            //typeof(System.Xml.XmlNode),

            // DoTween
            typeof(DG.Tweening.DOTween),
            typeof(DG.Tweening.Tween),
            typeof(DG.Tweening.Sequence),
            typeof(DG.Tweening.Tweener),
            typeof(DG.Tweening.TweenCallback),
            typeof(DG.Tweening.Ease),
            typeof(DG.Tweening.LoopType),
            typeof(DG.Tweening.PathMode),
            typeof(DG.Tweening.PathType),
            typeof(DG.Tweening.RotateMode),
            typeof(DG.Tweening.ScrambleMode),
            typeof(DG.Tweening.TweenExtensions),
            typeof(DG.Tweening.TweenSettingsExtensions),
            typeof(DG.Tweening.ShortcutExtensions),
            //typeof(DG.Tweening.ShortcutExtensions43),
            //typeof(DG.Tweening.ShortcutExtensions46),
            //typeof(DG.Tweening.ShortcutExtensions50),

            // FairyGui
            typeof(FairyGUI.EventContext),
            typeof(FairyGUI.EventDispatcher),
            typeof(FairyGUI.EventListener),
            typeof(FairyGUI.InputEvent),
            typeof(FairyGUI.DisplayObject),
            typeof(FairyGUI.Container),
            typeof(FairyGUI.Stage),
            typeof(FairyGUI.Controller),
            typeof(FairyGUI.GObject),
            typeof(FairyGUI.GGraph),
            typeof(FairyGUI.GGroup),
            typeof(FairyGUI.GImage),
            typeof(FairyGUI.GLoader),
            typeof(FairyGUI.GMovieClip),
            typeof(FairyGUI.TextFormat),
            typeof(FairyGUI.GTextField),
            typeof(FairyGUI.GRichTextField),
            typeof(FairyGUI.GTextInput),
            typeof(FairyGUI.GComponent),
            typeof(FairyGUI.GList),
            typeof(FairyGUI.GRoot),
            typeof(FairyGUI.GLabel),
            typeof(FairyGUI.GButton),
            typeof(FairyGUI.GComboBox),
            typeof(FairyGUI.GProgressBar),
            typeof(FairyGUI.GSlider),
            typeof(FairyGUI.PopupMenu),
            typeof(FairyGUI.ScrollPane),
            typeof(FairyGUI.Transition),
            typeof(FairyGUI.UIPackage),
            typeof(FairyGUI.Window),
            typeof(FairyGUI.GObjectPool),
            typeof(FairyGUI.Relations),
            typeof(FairyGUI.RelationType),
            typeof(FairyGUI.GTween),
            typeof(FairyGUI.GTweener),
            typeof(FairyGUI.EaseType),
            typeof(FairyGUI.TweenValue),

            typeof(EbTool),
            typeof(TcpClient),
            typeof(EbTimeEvent),
            typeof(EbTimer),
            typeof(EbDoubleLinkNode<EbTimeEvent>),
            typeof(EbDoubleLinkList<EbTimeEvent>),
            typeof(EbTimeWheel),
            typeof(TimerShaft),
            typeof(LoaderTicket),
            typeof(AsyncAssetLoaderMgr),
            typeof(AsyncAssetLoadGroup),

            typeof(Casinos._eProjectItemDisplayNameKey),
            typeof(_eAsyncAssetLoadType),
            typeof(_ePayType),

            typeof(Casinos.ParseStreamingAssetsDataInfo),
            typeof(Casinos.CopyStreamingAssetsToPersistentDataPath),
            //typeof(Casinos.LaunchConfigLoader),
            typeof(Casinos.UiSoundMgr),
            typeof(ChatParser),
            typeof(UiHelper),
            typeof(Card),

            typeof(QRCodeMaker),
            typeof(OnePF.Purchase),
            typeof(BuglyAgent),
            typeof(ThirdPartyLogin),
            typeof(DataEye),
            typeof(Push),
            typeof(ShareSDK),
            typeof(ShareContent),
            typeof(ShareSDKReceiver),
            typeof(BarcodeWriter),
            typeof(QrCodeEncodingOptions),
            typeof(PushReceiver),
            typeof(OpenInstallReceiver),
            typeof(OpenInstall),
            typeof(NativeFun),

            //typeof(Casinos.ConfigSection),
            //typeof(DevInfoSet),
            //typeof(MobLink),
            //typeof(MobLinkScene),
            //typeof(MobLinkReceiver),
            //typeof(UniWebView),
            //typeof(UniWebViewMessage),
        };

        //---------------------------------------------------------------------
        [CSharpCallLua]
        public static List<Type> CustomType2 = new List<Type>()
        {
            typeof(Action<string>),
            typeof(Action<string[]>),
            typeof(Action<System.Byte[]>),
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
            typeof(OnSocketReceive),
            typeof(OnSocketConnected),
            typeof(OnSocketClosed),
            typeof(OnSocketError),

            typeof(FairyGUI.TimerCallback),
            typeof(FairyGUI.EventCallback0),
            typeof(FairyGUI.EventCallback1),
            typeof(FairyGUI.PlayCompleteCallback),
            typeof(FairyGUI.EventContext),
            typeof(FairyGUI.ListItemRenderer),
            typeof(FairyGUI.TransitionHook),
            typeof(DG.Tweening.TweenCallback),
            typeof(DG.Tweening.Core.DOGetter<int>),
            typeof(DG.Tweening.Core.DOSetter<int>),
            typeof(DG.Tweening.Core.DOGetter<float>),
            typeof(DG.Tweening.Core.DOSetter<float>),

            typeof(Action<UnityEngine.Object, LoaderTicket>),
            typeof(Action<LoaderTicket, Texture>),
            typeof(AllTaskDoneCallBack),

            typeof(EventHandler),
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
        string CurrentLuaPath { get; set; }
        Action FuncLuaInit { get; set; }
        DelegateLuaUpdate FuncLuaUpdate { get; set; }
        Action FuncLuaRelease { get; set; }
        Dictionary<string, byte[]> MapLuaFiles { get; set; }
        Dictionary<string, string> MapLuaFilePath { get; set; }

        //---------------------------------------------------------------------
        public CasinosLua()
        {
            MapLuaFiles = new Dictionary<string, byte[]>();
            MapLuaFilePath = new Dictionary<string, string>();
            LuaEnv = new LuaEnv();
            LuaEnv.AddLoader(LuaLoaderCustom);
        }

        //---------------------------------------------------------------------
        public void Release()
        {
            //var lua_context = LuaEnv.Global.Get<LuaTable>("Context");
            //var fun_release = lua_context.Get<Action>("Release");
            //fun_release();

            if (LuaEnv != null)
            {
                FuncLuaRelease?.Invoke();

                LuaEnv = null;
            }
        }

        //---------------------------------------------------------------------
        public void Update(float elapsed_tm)
        {
            if (LuaEnv != null)
            {
                FuncLuaUpdate?.Invoke(elapsed_tm);

                LuaEnv.Tick();
            }
        }

        //---------------------------------------------------------------------
        public void LoadLuaLaunch()
        {
            var path_launch = CasinosContext.Instance.PathMgr.combinePersistentDataPath("Script.Lua/Launch/");
            string[] list_path = new string[] { path_launch };
            LoadLuaFromDir(list_path);

            //var lua_context = LuaEnv.Global.Get<LuaTable>("Context");
            //var fun_new = lua_context.Get<DelegateLuaNew>("new");
            //fun_new(LuaEnv.NewTable());
            //var fun_init = lua_context.Get<Action>("Init");
            //fun_init();

            //var fun_release = lua_context.Get<Action>("Release");
            //fun_release();
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
        public void LoadLuaFromDir(string[] list_path)
        {
            foreach (var k in list_path)
            {
                DirectoryInfo dir = new DirectoryInfo(k);
                FileInfo[] file_list = dir.GetFiles("*.lua", SearchOption.AllDirectories);
                foreach (FileInfo i in file_list)
                {
                    using (FileStream fs = new FileStream(i.FullName, FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(fs))
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
        //public void RegLuaFilePath(string luafile_relativepath, params string[] luafile_name)
        //{
        //    foreach (var i in luafile_name)
        //    {
        //        string path = Path.Combine(
        //            CasinosContext.Instance.PathMgr.PathLuaRoot,
        //            string.Format("{0}{1}.lua", luafile_relativepath, i));
        //        MapLuaFilePath[i] = path;
        //    }
        //}

        //---------------------------------------------------------------------
        public void DoString(string luafile_name)
        {
            //string current_luapath = string.Empty;
            //MapLuaFilePath.TryGetValue(config_name, out current_luapath);
            //CurrentLuaPath = current_luapath;

            string do_string = string.Format("require '{0}'", luafile_name);
            LuaEnv.DoString(do_string);
        }

        //---------------------------------------------------------------------
        public void DoMainCLua(string config_name)
        {
            DoString(config_name);

            // 解析全局函数
            FuncLuaInit = LuaEnv.Global.Get<Action>("MainCInit");
            FuncLuaUpdate = LuaEnv.Global.Get<DelegateLuaUpdate>("MainCUpdate");
            FuncLuaRelease = LuaEnv.Global.Get<Action>("MainCRelease");

            FuncLuaInit();
        }

        //---------------------------------------------------------------------
        public LuaTable GetLuaTable(string name)
        {
            return CasinosContext.Instance.CasinosLua.LuaEnv.Global.Get<LuaTable>(name);
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