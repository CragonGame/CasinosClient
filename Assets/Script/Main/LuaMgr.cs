// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using XLua;
    using GameCloud.Unity.Common;
    using System.IO;

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
            typeof(UnityEngine.Object),
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
            typeof(UnityEngine.Texture2D),
            typeof(UnityEngine.Texture3D),
            typeof(UnityEngine.Time),
            typeof(UnityEngine.Networking.UnityWebRequest),
            typeof(UnityEngine.Vector3),
            typeof(UnityEngine.Vector2),
            typeof(UnityEngine.Vector4),
            
            // System            
            typeof(Action),
            typeof(Action<int, string>),
            typeof(Action<string, UnityEngine.Networking.UnityWebRequest>),
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
            typeof(FairyGUI.NTexture),
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
            typeof(_eChatItemType),
            typeof(_ePayType),
            typeof(_eProjectItemDisplayNameKey),
            //typeof(Card),
            typeof(ChatParser),
            typeof(EbTool),
            typeof(EbTimeEvent),
            typeof(EbTimer),
            typeof(EbDoubleLinkNode<EbTimeEvent>),
            typeof(EbDoubleLinkList<EbTimeEvent>),
            typeof(EbTimeWheel),
            typeof(MbHelper),
            typeof(NativeFun),
            //typeof(OpenInstall),
            //typeof(OpenInstallReceiver),
            //typeof(Push),
            //typeof(PushReceiver),
            //typeof(ShareSDKReceiver),
            typeof(TcpClient),
            typeof(ThirdPartyLogin),
            typeof(TimerShaft),
            typeof(UiHelper),
            typeof(UpdateRemoteToPersistentData),
            //typeof(DevInfoSet),
            
            // SDKs
            typeof(ZXing.BarcodeFormat),
            typeof(BuglyAgent),
            //typeof(OnePF.Purchase),
            typeof(cn.sharesdk.unity3d.ShareContent),
            typeof(cn.sharesdk.unity3d.ShareSDK),
            typeof(ZXing.BarcodeWriter),
            typeof(ZXing.QrCode.QrCodeEncodingOptions),
            //typeof(UniWebView),
            //typeof(UniWebViewMessage),
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
            typeof(Action<LuaTable, string>),
            typeof(Action<LuaTable, string, float>),
            typeof(Action<float>),
            typeof(Action<int, string>),
            typeof(Action<string, Action>),
            typeof(Action<string, Action, Action>),
            typeof(Action<string, UnityEngine.Networking.UnityWebRequest>),
            typeof(Action<string, string>),
            typeof(Action<string, int>),
            typeof(Action<string, float>),
            typeof(Action<string, bool>),
            typeof(Action<string, string, string>),
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
            typeof(Action<Dictionary<string,Dictionary<string, string>>>),
            typeof(Action<ushort, byte[]>),
            typeof(List<string>),
            typeof(List<AssetBundle>),
            typeof(OnSocketReceive),
            typeof(OnSocketConnected),
            typeof(OnSocketClosed),
            typeof(OnSocketError),
            typeof(AllTaskDoneCallBack),
            typeof(DelayEndCallBack),
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
            //typeof(UniWebView.PageStartedDelegate),
            //typeof(UniWebView.PageFinishedDelegate),
            //typeof(UniWebView.PageErrorReceivedDelegate),
            //typeof(UniWebView.MessageReceivedDelegate),
            //typeof(UniWebView.ShouldCloseDelegate),
            //typeof(UniWebView.KeyCodeReceivedDelegate),
            //typeof(UniWebView.OnWebContentProcessTerminatedDelegate),
        };
    }

    [LuaCallCSharp]
    public class LuaMgr
    {
        //---------------------------------------------------------------------
        public LuaEnv LuaEnv { get; private set; }
        Dictionary<string, byte[]> MapLuaFiles { get; set; }
        DelegateLua1 FuncLaunchClose { get; set; }
        DelegateLua1 FuncLaunchOnAndroidQuitConfirm { get; set; }
        DelegateLua3 FuncLaunchOnApplicationPause { get; set; }
        DelegateLua3 FuncLaunchOnApplicationFocus { get; set; }
        DelegateLua1 FuncLaunchOnSocketClose { get; set; }
        CasinosContext Context { get; set; }
        MbAsyncLoadAssets MbAsyncLoadAssets { get; set; }

        float TickTimeSpanLast = 0;
        const float TickTimeSpanMax = 1f;

        //---------------------------------------------------------------------
        public LuaMgr()
        {
            Context = CasinosContext.Instance;
            MapLuaFiles = new Dictionary<string, byte[]>();
            LuaEnv = new LuaEnv();
            LuaEnv.AddLoader(_luaLoaderCustom);

            var go_main = GameObject.Find(StringDef.GoMainObj);
            MbAsyncLoadAssets = go_main.GetComponent<MbAsyncLoadAssets>();
        }

        //---------------------------------------------------------------------
        public void Release()
        {
            var lua_launch = LuaEnv.Global.Get<LuaTable>("Launch");
            if (FuncLaunchClose != null) FuncLaunchClose.Invoke(lua_launch);
            FuncLaunchClose = null;
            FuncLaunchOnAndroidQuitConfirm = null;
            FuncLaunchOnApplicationPause = null;
            FuncLaunchOnApplicationFocus = null;
            FuncLaunchOnSocketClose = null;
            LuaEnv.Tick();

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
        public void Launch(bool is_load_launchlua_from_resources)
        {
            // 预加载Script.Lua/Launch中的所有txt文件，显示加载界面
            var path_mgr = CasinosContext.Instance.PathMgr;
            var cfg = CasinosContext.Instance.Config;

            if (is_load_launchlua_from_resources)
            {
                foreach (var i in cfg.LaunchInfoResources.ListLua)
                {
                    LoadLuaFromResources(i);
                }
            }
            else if (CasinosContext.Instance.IsEditorDebug)
            {
                string dir_launchlua = Environment.CurrentDirectory + "/Assets/Script.Lua/Launch/";
                LoadLuaFromRawDir(dir_launchlua.Replace('\\', '/'));
            }
            else
            {
                AssetBundle ab = AssetBundle.LoadFromFile(path_mgr.DirLuaRoot + "lua_launch_android.ab");
                LoadLuaFromAssetBundle(ab);
            }

            DoString("Launch");

            var lua_launch = LuaEnv.Global.Get<LuaTable>("Launch");
            FuncLaunchClose = lua_launch.Get<DelegateLua1>("Close");
            FuncLaunchOnAndroidQuitConfirm = lua_launch.Get<DelegateLua1>("OnAndroidQuitConfirm");
            FuncLaunchOnApplicationPause = lua_launch.Get<DelegateLua3>("OnApplicationPause");
            FuncLaunchOnApplicationFocus = lua_launch.Get<DelegateLua3>("OnApplicationFocus");
            FuncLaunchOnSocketClose = lua_launch.Get<DelegateLua1>("OnSocketClose");
            var func_setup = lua_launch.Get<DelegateLua1>("Setup");
            func_setup(lua_launch);
        }

        //---------------------------------------------------------------------
        public void LocalLoadTextureFromAbAsync(string url, string name, Action<Texture> cb)
        {
            MbAsyncLoadAssets.LocalLoadTextureFromAbAsync(url, name, cb);
        }

        //---------------------------------------------------------------------
        public void WWWLoadTextAsync(string url, Action<string> cb)
        {
            MbAsyncLoadAssets.WWWLoadTextAsync(url, cb);
        }

        //---------------------------------------------------------------------
        public void WWWLoadTextureAsync(string url, Action<Texture> cb)
        {
            MbAsyncLoadAssets.WWWLoadTextureAsync(url, cb);
        }

        //---------------------------------------------------------------------
        public void WWWLoadAssetBundleAsync(string url, Action<AssetBundle> cb)
        {
            MbAsyncLoadAssets.WWWLoadAssetBundleAsync(url, cb);
        }

        //---------------------------------------------------------------------
        public void LoadLuaFromAssetBundle(AssetBundle ab)
        {
            string[] list_assets = ab.GetAllAssetNames();
            foreach (var i in list_assets)
            {
                string file_name = Path.GetFileNameWithoutExtension(i);
                TextAsset ta = ab.LoadAsset<TextAsset>(i);
                MapLuaFiles[file_name] = ta.bytes;
            }
        }

        //---------------------------------------------------------------------
        public void LoadLuaFromBytes(string file_name, string text)
        {
            string name = file_name.ToLower();
            string name1 = name.Replace(".txt", "");

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
            MapLuaFiles[name1] = bytes;
        }

        //---------------------------------------------------------------------
        public void LoadLuaFromResources(string filepath)
        {
            string name = filepath;
            if (name.EndsWith(".txt"))
            {
                name = name.Remove(name.Length - 4, 4);
            }
            name = name.Replace('\\', '/');

            var txt = Resources.Load<TextAsset>(name);

            int index = name.LastIndexOf('/');
            string name1 = name.Substring(index + 1, name.Length - index - 1);
            string name2 = name1.ToLower();
            MapLuaFiles[name2] = txt.bytes;
        }

        //---------------------------------------------------------------------
        public void LoadLuaFromRawDir(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] file_list = dir.GetFiles("*.txt", SearchOption.AllDirectories);
            foreach (FileInfo i in file_list)
            {
                using (FileStream fs = new FileStream(i.FullName, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string s = sr.ReadToEnd();
                        byte[] data = System.Text.Encoding.UTF8.GetBytes(s);

                        string name = i.Name.ToLower();
                        string name1 = name.Replace(".txt", "");
                        MapLuaFiles[name1] = data;
                    }
                }
            }

            //Debug.Log("LoadLuaFromRawDir() 文件数量=" + file_list.Length);
        }

        //---------------------------------------------------------------------
        public void LoadLuaFromRawDir2(string[] list_path)
        {
            foreach (var k in list_path)
            {
                DirectoryInfo dir = new DirectoryInfo(k);
                FileInfo[] file_list = dir.GetFiles("*.txt", SearchOption.AllDirectories);
                foreach (FileInfo i in file_list)
                {
                    using (FileStream fs = new FileStream(i.FullName, FileMode.Open))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            string s = sr.ReadToEnd();
                            byte[] data = System.Text.Encoding.UTF8.GetBytes(s);

                            string name = i.Name.ToLower();
                            string name1 = name.Replace(".txt", "");
                            MapLuaFiles[name1] = data;
                        }
                    }
                }
            }

            //Debug.Log("LoadLuaFromRawDir2() 文件数量=" + file_list.Length);
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
            return LuaEnv.Global.Get<LuaTable>(name);
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
            if (File.Exists(full_filename))
            {
                all_text = File.ReadAllText(full_filename);
            }
            return all_text;
        }

        //---------------------------------------------------------------------
        public void WriteFileFromBytes(string path, byte[] bytes)
        {
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    ms.WriteTo(fs);
                }
            }
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
        // 拷贝text到剪切板
        public void SetClipBoard(string text)
        {
            BlankOperationClipboard.SetValue(text);
        }

        //---------------------------------------------------------------------
        // 从剪切板中获取拷贝的text
        public string GetClipBoard()
        {
            return BlankOperationClipboard.GetValue();
        }

        //---------------------------------------------------------------------
        // 创建二维码
        public Color32[] CreateQRCode(string encoding_text, int width, int height)
        {
            var writer = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                {
                    CharacterSet = "UTF-8",
                    Height = height,
                    Width = width,
                    Margin = 1// 设置二维码的边距,单位不是固定像素
                }
            };
            return writer.Write(encoding_text);
        }

        //---------------------------------------------------------------------
        public void SaveTextureToFile(Texture2D texture, string file_name)
        {
            var bytes = texture.EncodeToPNG();
            using (var fs = File.Create(file_name))
            {
                var binary = new BinaryWriter(fs);
                binary.Write(bytes);
            }
        }

        //---------------------------------------------------------------------
        // 创建内嵌浏览器
        //     public UniWebView CreateWebView(string go_name)
        //     {
        //         var go = new GameObject(go_name);
        //         UniWebView web_view = go.AddComponent<UniWebView>();
        //web_view.Frame = new Rect(0, 0, Screen.width, Screen.height);
        ////web_view.Load("http://docs.uniwebview.com/game.html");
        ////web_view.Show();
        //return web_view;
        //     }

        //     //---------------------------------------------------------------------
        //     // 创建内嵌浏览器
        //     public UniWebView CreateWebView2(string go_name)
        //     {
        //         var go = new GameObject(go_name);
        //         UniWebView web_view = go.AddComponent<UniWebView>();
        ////web_view.Frame = new Rect(0, 0, Screen.width, Screen.height);
        ////web_view.Load("http://docs.uniwebview.com/game.html");
        ////web_view.Show();
        //return web_view;
        //     }

        //---------------------------------------------------------------------
        public string ToBase64String(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        //---------------------------------------------------------------------
        public byte[] FromBase64String(string data)
        {
            return Convert.FromBase64String(data);
        }

        //---------------------------------------------------------------------
        public void _CSharpCallOnAndroidQuitConfirm()
        {
            var lua_launch = LuaEnv.Global.Get<LuaTable>("Launch");
            if (FuncLaunchOnAndroidQuitConfirm != null) FuncLaunchOnAndroidQuitConfirm.Invoke(lua_launch);
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
        public void _CSharpCallOnSocketClose()
        {
            var lua_launch = LuaEnv.Global.Get<LuaTable>("Launch");
            if (FuncLaunchOnSocketClose != null) FuncLaunchOnSocketClose.Invoke(lua_launch);
        }

        //---------------------------------------------------------------------
        // 自定义Lua文件加载函数
        byte[] _luaLoaderCustom(ref string file_name)
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