using System.Collections;
using System.Collections.Generic;
using System.IO;
using ILRuntime.Runtime.Enviorment;
using UnityEngine;
using UnityEngine.Networking;

public class MbILRuntime : MonoBehaviour
{
    //-------------------------------------------------------------------------
    // AppDomain是ILRuntime的入口，最好是在一个单例类中保存，整个游戏全局就一个，这里为了示例方便，每个例子里面都单独做了一个
    // 大家在正式项目中请全局只创建一个AppDomain
    // 首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
    AppDomain AppDomain { get; set; } = new ILRuntime.Runtime.Enviorment.AppDomain();

    //-------------------------------------------------------------------------
    void Start()
    {
        //StartCoroutine(LoadHotFixAssembly());
        LoadHotFixAssembly();
    }

    //-------------------------------------------------------------------------
    void LoadHotFixAssembly()
    {
        string s = Application.streamingAssetsPath;
        s = s.Replace('\\', '/');
        s = s.Replace("Assets/StreamingAssets", "");
        s += "Temp/bin/Debug/";
        Debug.Log(s);

        byte[] dll = File.ReadAllBytes(s + "Script.CSharp.dll");
        byte[] pdb = File.ReadAllBytes(s + "Script.CSharp.pdb");

        //        // 正常项目中应该是自行从其他地方下载dll，或者打包在AssetBundle中读取，平时开发以及为了演示方便直接从StreammingAssets中读取，
        //        // 正式发布的时候需要大家自行从其他地方读取dll
        //        // 这个DLL文件是直接编译HotFix_Project.sln生成的，已经在项目中设置好输出目录为StreamingAssets，在VS里直接编译即可生成到对应目录，无需手动拷贝
        //#if UNITY_ANDROID
        //        UnityWebRequest www = new UnityWebRequest(Application.streamingAssetsPath + "/Script.CSharp.dll");
        //#else
        //        UnityWebRequest www = new UnityWebRequest("file:///" + Application.streamingAssetsPath + "/Script.CSharp.dll");
        //#endif
        //        www.SendWebRequest();
        //        while (!www.isDone)
        //        {
        //            yield return 1;
        //        }

        //        if (!string.IsNullOrEmpty(www.error))
        //        {
        //            UnityEngine.Debug.LogError(www.error);
        //        }

        //        byte[] dll = www.downloadHandler.data;
        //        www.Dispose();

        //        // PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
        //#if UNITY_ANDROID
        //        www = new UnityWebRequest(Application.streamingAssetsPath + "/Script.CSharp.pdb");
        //#else
        //        www = new UnityWebRequest("file:///" + Application.streamingAssetsPath + "/Script.CSharp.pdb");
        //#endif
        //        www.SendWebRequest();
        //        while (!www.isDone)
        //        {
        //            yield return 1;
        //        }

        //        if (!string.IsNullOrEmpty(www.error))
        //        {
        //            UnityEngine.Debug.LogError(www.error);
        //        }

        //        byte[] pdb = www.downloadHandler.data;

        using (MemoryStream fs = new MemoryStream(dll))
        {
            using (MemoryStream p = new MemoryStream(pdb))
            {
                AppDomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());

                AppDomain.Invoke("CsMain", "Launch", null, null);
            }
        }

        //InitializeILRuntime();

        //OnHotFixLoaded();
    }

    //-------------------------------------------------------------------------
    void InitializeILRuntime()
    {
        // 这里做一些ILRuntime的注册
    }

    //-------------------------------------------------------------------------
    void OnHotFixLoaded()
    {
        AppDomain.Invoke("CsMain", "Launch", null, null);
    }
}
