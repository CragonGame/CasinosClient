using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EditorContext
{
    //-------------------------------------------------------------------------
    public static EditorContext Instance { get; internal set; }
    public string PathDataOss { get; private set; }// DataOss目录
    public string PathAssets { get; private set; }// Unity3D的Assets目录
    public string PathDataSrc { get; private set; }// 源数据资源根目录（未打包的）
    public string PathStreamingAssets { get; private set; }// 源数据资源根目录（未打包的）
    public string PathSettings { get; private set; }// Settings目录
    public string PathSettingsUser { get; private set; }// SettingsUser目录
    public string PathPersistent { get; private set; }// Persistent根目录
    public EditorConfig Config { get; private set; }

    //-------------------------------------------------------------------------
    public EditorContext()
    {
        Instance = this;

        // PathDataOss
        {
            string p = Path.Combine(Environment.CurrentDirectory, "./DataOss/");
            var di = new DirectoryInfo(p);
            PathDataOss = di.FullName;
        }

        // PathAssets
        {
            string p = Path.Combine(Environment.CurrentDirectory, "./Assets/");
            var di = new DirectoryInfo(p);
            PathAssets = di.FullName;
        }

        // PathDataSrc
        {
            string p = Path.Combine(Environment.CurrentDirectory, "./Assets/Resources.KingTexas/");
            var di = new DirectoryInfo(p);
            PathDataSrc = di.FullName;
        }

        // PathStreamingAssets
        {
            string p = Path.Combine(Environment.CurrentDirectory, "./Assets/StreamingAssets/");
            var di = new DirectoryInfo(p);
            PathStreamingAssets = di.FullName;
        }

        // PathSettings
        {
            string p = Path.Combine(Environment.CurrentDirectory, "./Assets/Settings/");
            var di = new DirectoryInfo(p);
            PathSettings = di.FullName;
        }

        // PathSettingsUser
        {
            string p = Path.Combine(Environment.CurrentDirectory, "./Assets/SettingsUser/");
            var di = new DirectoryInfo(p);
            PathSettingsUser = di.FullName;
        }

        // PersistentDataPath
        {
            PathPersistent =
#if UNITY_STANDALONE_WIN && UNITY_EDITOR
            Application.persistentDataPath +"/PC";
#elif UNITY_ANDROID && UNITY_EDITOR
            Application.persistentDataPath + "/ANDROID";
#elif UNITY_IPHONE && UNITY_EDITOR
            Application.persistentDataPath + "/IOS";
#elif UNITY_ANDROID
            Application.persistentDataPath;
#elif UNITY_IPHONE
            Application.persistentDataPath;
#else
            string.Empty;
#endif
        }

        Config = new EditorConfig();
    }

    //-------------------------------------------------------------------------
    [MenuItem("Casinos/DataPublish", false, 1)]
    static void MenuItemCasinos()
    {
        if (Instance == null)
        {
            new EditorContext();
        }

        EditorViewDataPublish editor = ScriptableObject.CreateInstance<EditorViewDataPublish>();
        editor.titleContent = new GUIContent("资源版本发布", "用于发布资源版本到指定的发布目录");
        editor.minSize = new Vector2(1024, 768);
        editor.maxSize = new Vector2(1024, 768);
        editor.Show();
    }

    //-------------------------------------------------------------------------
    [MenuItem("Casinos/ILRuntime/Generate CLR Binding Code", false, 103)]
    static void MenuItemGenerateCLRBinding()
    {
        if (Instance == null)
        {
            new EditorContext();
        }

        List<Type> types = new List<Type>();
        types.Add(typeof(short));
        types.Add(typeof(ushort));
        types.Add(typeof(int));
        types.Add(typeof(uint));
        types.Add(typeof(float));
        types.Add(typeof(double));
        types.Add(typeof(long));
        types.Add(typeof(ulong));
        types.Add(typeof(object));
        types.Add(typeof(string));
        types.Add(typeof(Array));

        types.Add(typeof(System.Environment));
        types.Add(typeof(System.IO.Directory));
        types.Add(typeof(System.IO.DirectoryInfo));
        types.Add(typeof(System.IO.File));
        types.Add(typeof(System.IO.MemoryStream));
        types.Add(typeof(System.IO.FileStream));
        types.Add(typeof(System.IO.Path));
        types.Add(typeof(System.Text.StringBuilder));

        types.Add(typeof(UnityEngine.Object));
        types.Add(typeof(UnityEngine.Component));
        types.Add(typeof(UnityEngine.Behaviour));
        types.Add(typeof(UnityEngine.MonoBehaviour));
        types.Add(typeof(UnityEngine.Application));
        types.Add(typeof(UnityEngine.AssetBundle));
        types.Add(typeof(UnityEngine.AudioClip));
        types.Add(typeof(UnityEngine.AudioSource));
        types.Add(typeof(UnityEngine.Camera));
        types.Add(typeof(UnityEngine.Color));
        types.Add(typeof(UnityEngine.Color32));
        types.Add(typeof(UnityEngine.Coroutine));
        types.Add(typeof(UnityEngine.Debug));
        types.Add(typeof(UnityEngine.Font));
        types.Add(typeof(UnityEngine.GameObject));
        types.Add(typeof(UnityEngine.Hash128));
        types.Add(typeof(UnityEngine.LayerMask));
        types.Add(typeof(UnityEngine.Material));
        types.Add(typeof(UnityEngine.Mathf));
        types.Add(typeof(UnityEngine.Matrix4x4));
        //types.Add(typeof(UnityEngine.Mesh));
        types.Add(typeof(UnityEngine.MeshRenderer));
        //types.Add(typeof(UnityEngine.ParticleSystem));
        types.Add(typeof(UnityEngine.Ping));
        types.Add(typeof(UnityEngine.PlayerPrefs));
        types.Add(typeof(UnityEngine.Quaternion));
        types.Add(typeof(UnityEngine.QualitySettings));
        types.Add(typeof(UnityEngine.Random));
        types.Add(typeof(UnityEngine.Rect));
        types.Add(typeof(UnityEngine.RectTransform));
        types.Add(typeof(UnityEngine.Renderer));
        types.Add(typeof(UnityEngine.Resources));
        types.Add(typeof(UnityEngine.Screen));
        types.Add(typeof(UnityEngine.ScreenCapture));
        types.Add(typeof(UnityEngine.SystemInfo));
        //types.Add(typeof(UnityEngine.Texture));
        //types.Add(typeof(UnityEngine.Texture2D));
        //types.Add(typeof(UnityEngine.Texture3D));
        types.Add(typeof(UnityEngine.TextAsset));
        types.Add(typeof(UnityEngine.Time));
        types.Add(typeof(UnityEngine.Transform));
        types.Add(typeof(UnityEngine.Networking.UnityWebRequest));
        types.Add(typeof(UnityEngine.Vector2));
        types.Add(typeof(UnityEngine.Vector3));
        types.Add(typeof(UnityEngine.Vector4));

        // FairyGUI
        //types.Add(typeof(FairyGUI.EaseType));
        //types.Add(typeof(FairyGUI.RelationType));
        //types.Add(typeof(FairyGUI.TweenPropType));
        types.Add(typeof(FairyGUI.Container));
        types.Add(typeof(FairyGUI.Controller));
        types.Add(typeof(FairyGUI.DisplayObject));
        //types.Add(typeof(FairyGUI.EventCallback0));
        //types.Add(typeof(FairyGUI.EventCallback1));
        types.Add(typeof(FairyGUI.EventContext));
        types.Add(typeof(FairyGUI.EventDispatcher));
        types.Add(typeof(FairyGUI.EventListener));
        types.Add(typeof(FairyGUI.InputEvent));
        types.Add(typeof(FairyGUI.GoWrapper));
        types.Add(typeof(FairyGUI.GObject));
        types.Add(typeof(FairyGUI.GGraph));
        types.Add(typeof(FairyGUI.GGroup));
        types.Add(typeof(FairyGUI.GImage));
        types.Add(typeof(FairyGUI.GLoader));
        types.Add(typeof(FairyGUI.GMovieClip));
        types.Add(typeof(FairyGUI.GTextField));
        types.Add(typeof(FairyGUI.GRichTextField));
        types.Add(typeof(FairyGUI.GTextInput));
        types.Add(typeof(FairyGUI.GComponent));
        types.Add(typeof(FairyGUI.GList));
        types.Add(typeof(FairyGUI.GRoot));
        types.Add(typeof(FairyGUI.GLabel));
        types.Add(typeof(FairyGUI.GButton));
        types.Add(typeof(FairyGUI.GComboBox));
        types.Add(typeof(FairyGUI.GObjectPool));
        types.Add(typeof(FairyGUI.GProgressBar));
        types.Add(typeof(FairyGUI.GSlider));
        types.Add(typeof(FairyGUI.GTween));
        types.Add(typeof(FairyGUI.GTweener));
        //types.Add(typeof(FairyGUI.GTweenCallback));
        //types.Add(typeof(FairyGUI.GTweenCallback1));
        //types.Add(typeof(FairyGUI.ListItemRenderer));
        types.Add(typeof(FairyGUI.NTexture));
        types.Add(typeof(FairyGUI.Stage));
        //types.Add(typeof(FairyGUI.PlayCompleteCallback));
        types.Add(typeof(FairyGUI.PopupMenu));
        types.Add(typeof(FairyGUI.Relations));
        types.Add(typeof(FairyGUI.ScrollPane));
        types.Add(typeof(FairyGUI.TextFormat));
        //types.Add(typeof(FairyGUI.TimerCallback));
        types.Add(typeof(FairyGUI.Transition));
        //types.Add(typeof(FairyGUI.TransitionHook));
        types.Add(typeof(FairyGUI.TweenValue));
        types.Add(typeof(FairyGUI.UIPackage));
        types.Add(typeof(FairyGUI.Window));

        // Spine
        types.Add(typeof(Spine.Unity.SkeletonAnimation));

        // Casinos
        types.Add(typeof(Casinos.MbAsyncLoadAssets));

        // 所有DLL内的类型的真实C#类型都是ILTypeInstance
        types.Add(typeof(List<ILRuntime.Runtime.Intepreter.ILTypeInstance>));

        ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(types, "Assets/Script.ILRuntimeGenerated");

        AssetDatabase.Refresh();
    }

    //-------------------------------------------------------------------------
    [MenuItem("Casinos/ILRuntime/Generate CLR Binding Code by Analysis", false, 104)]
    static void GenerateCLRBindingByAnalysis()
    {
        // 用新的分析热更dll调用引用来生成绑定代码
        //ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();
        //using (System.IO.FileStream fs = new System.IO.FileStream("Assets/StreamingAssets/HotFix_Project.dll", System.IO.FileMode.Open, System.IO.FileAccess.Read))
        //{
        //    domain.LoadAssembly(fs);
        //}

        ////Crossbind Adapter is needed to generate the correct binding code
        //InitILRuntime(domain);
        //ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, "Assets/ILRuntime/Generated");
    }

    //-------------------------------------------------------------------------
    static void InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain domain)
    {
        //这里需要注册所有热更DLL中用到的跨域继承Adapter，否则无法正确抓取引用
        //domain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
        //domain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        //domain.RegisterCrossBindingAdaptor(new InheritanceAdapter());
    }

    //-------------------------------------------------------------------------
    static bool ExecuteProgram(string exe_filename, string args)
    {
        System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
        info.FileName = exe_filename;
        info.WorkingDirectory = Environment.CurrentDirectory;
        info.UseShellExecute = true;
        info.Arguments = args;
        info.CreateNoWindow = true;
        info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

        System.Diagnostics.Process task = null;
        bool rt = true;
        try
        {
            task = System.Diagnostics.Process.Start(info);
            if (task != null)
            {
                //task.WaitForExit();
                //task.WaitForExit(100000);
                //task.StandardOutput.ReadToEnd();
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("执行批处理: " + e.ToString());
            return false;
        }
        finally
        {
            if (task != null && task.HasExited)
            {
                rt = (task.ExitCode == 0);
            }
        }
        return rt;
    }
}