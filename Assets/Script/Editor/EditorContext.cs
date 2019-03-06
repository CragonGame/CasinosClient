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

        types.Add(typeof(UnityEngine.Vector2));
        types.Add(typeof(UnityEngine.Vector3));
        types.Add(typeof(UnityEngine.Quaternion));
        types.Add(typeof(UnityEngine.GameObject));
        types.Add(typeof(UnityEngine.Object));
        types.Add(typeof(UnityEngine.Transform));
        types.Add(typeof(UnityEngine.RectTransform));
        types.Add(typeof(UnityEngine.Time));
        types.Add(typeof(UnityEngine.Debug));

        types.Add(typeof(MessagePack.Resolvers.StandardResolver));
        types.Add(typeof(MessagePack.Resolvers.AttributeFormatterResolver));
        types.Add(typeof(MessagePack.Resolvers.BuiltinResolver));
        types.Add(typeof(MessagePack.Resolvers.PrimitiveObjectResolver));
        types.Add(typeof(MessagePack.Formatters.BitArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.BooleanArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.BooleanFormatter));
        types.Add(typeof(MessagePack.Formatters.ByteArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.ByteArraySegmentFormatter));
        types.Add(typeof(MessagePack.Formatters.ByteFormatter));
        types.Add(typeof(MessagePack.Formatters.CharArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.CharFormatter));
        types.Add(typeof(MessagePack.Formatters.DateTimeArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.DateTimeFormatter));
        types.Add(typeof(MessagePack.Formatters.DecimalFormatter));
        types.Add(typeof(MessagePack.Formatters.DoubleArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.DoubleFormatter));
        types.Add(typeof(MessagePack.Formatters.Int16ArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.Int16Formatter));
        types.Add(typeof(MessagePack.Formatters.Int32ArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.Int32Formatter));
        types.Add(typeof(MessagePack.Formatters.Int64ArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.Int64Formatter));
        types.Add(typeof(MessagePack.Formatters.NilFormatter));
        types.Add(typeof(MessagePack.Formatters.NullableBooleanFormatter));
        types.Add(typeof(MessagePack.Formatters.PrimitiveObjectFormatter));
        types.Add(typeof(MessagePack.Formatters.SByteArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.SByteFormatter));
        types.Add(typeof(MessagePack.Formatters.SingleArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.SingleFormatter));
        types.Add(typeof(MessagePack.Formatters.StringBuilderFormatter));
        types.Add(typeof(MessagePack.Formatters.TimeSpanFormatter));
        types.Add(typeof(MessagePack.Formatters.UInt16ArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.UInt16Formatter));
        types.Add(typeof(MessagePack.Formatters.UInt32ArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.UInt32Formatter));
        types.Add(typeof(MessagePack.Formatters.UInt64ArrayFormatter));
        types.Add(typeof(MessagePack.Formatters.UInt64Formatter));
        types.Add(typeof(MessagePack.Formatters.UriFormatter));
        types.Add(typeof(MessagePack.Formatters.VersionFormatter));
        types.Add(typeof(MessagePack.IFormatterResolver));
        types.Add(typeof(MessagePack.IgnoreMemberAttribute));
        types.Add(typeof(MessagePack.KeyAttribute));
        types.Add(typeof(MessagePack.MessagePackObjectAttribute));
        types.Add(typeof(MessagePack.MessagePackSerializer));

        types.Add(typeof(MsgPack));

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