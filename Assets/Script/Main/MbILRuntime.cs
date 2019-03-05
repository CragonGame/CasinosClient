using System.Collections;
using System.Collections.Generic;
using System.IO;
using ILRuntime.Runtime.Enviorment;
using UnityEngine;

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
        s += "Script.CSharp/bin/Release/";

#if UNITY_EDITOR
        // 检测Script.CSharp.dll是否存在，如不存在则给出提示
#endif

        byte[] dll = File.ReadAllBytes(s + "Script.CSharp.dll");
        byte[] pdb = File.ReadAllBytes(s + "Script.CSharp.pdb");

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
        LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(AppDomain);
    }

    //-------------------------------------------------------------------------
    void OnHotFixLoaded()
    {
        AppDomain.Invoke("CsMain", "Launch", null, null);
    }
}
