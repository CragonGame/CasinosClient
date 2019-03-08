using System.Collections;
using System.Collections.Generic;
using System.IO;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Generated;

public class CasinosILRuntime
{
    //-------------------------------------------------------------------------
    // AppDomain是ILRuntime的入口，最好是在一个单例类中保存，整个游戏全局就一个，这里为了示例方便，每个例子里面都单独做了一个
    // 全局只创建一个AppDomain
    // 首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
    AppDomain AppDomain { get; set; } = new ILRuntime.Runtime.Enviorment.AppDomain();

    //-------------------------------------------------------------------------
    public void Create()
    {
        string s = UnityEngine.Application.streamingAssetsPath;
        s = s.Replace('\\', '/');
        s = s.Replace("Assets/StreamingAssets", "");
        s += "Script/Script.CSharp/bin/";

        //string s = Casinos.CasinosContext.Instance.PathMgr.DirCsRoot;

#if UNITY_EDITOR
        // 检测Script.CSharp.dll是否存在，如不存在则给出提示
#endif

        byte[] dll1 = File.ReadAllBytes(s + "Script.Common.dll");
        byte[] pdb1 = File.ReadAllBytes(s + "Script.Common.pdb");

        byte[] dll = File.ReadAllBytes(s + "Script.CSharp.dll");
        byte[] pdb = File.ReadAllBytes(s + "Script.CSharp.pdb");

        using (MemoryStream fs1 = new MemoryStream(dll1))
        {
            using (MemoryStream p1 = new MemoryStream(pdb1))
            {
                AppDomain.LoadAssembly(fs1, p1, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());

                using (MemoryStream fs = new MemoryStream(dll))
                {
                    using (MemoryStream p = new MemoryStream(pdb))
                    {
                        AppDomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());

                        // 这里做一些ILRuntime的注册
                        LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(AppDomain);
                        CLRBindings.Initialize(AppDomain);

                        AppDomain.Invoke("CsMain", "Create", null, null);
                    }
                }
            }
        }
    }

    //-------------------------------------------------------------------------
    public void Destroy()
    {
        AppDomain.Invoke("CsMain", "Destroy", null, null);
        CLRBindings.Shutdown(AppDomain);
    }

    //-------------------------------------------------------------------------
    public void Update()
    {
        AppDomain.Invoke("CsMain", "Update", null, null);
    }
}
