// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;
    using UnityEngine.Networking;
    using ILRuntime.Runtime.Enviorment;

    public class CommonInfo
    {
        public string CommonVersion { get; set; }
        public List<string> CommonFileList { get; set; }
    }

    public class MbLaunch : MonoBehaviour
    {
        //---------------------------------------------------------------------
        //string VersionCommonPersistent { get; set; }
        CommonInfo CommonInfo { get; set; }
        ILRuntime.Runtime.Enviorment.AppDomain AppDomain { get; set; } = new ILRuntime.Runtime.Enviorment.AppDomain();

        //---------------------------------------------------------------------
        void Start()
        {
            // 读取VersionCommonPersistent
            string common_ver = string.Empty;
            if (PlayerPrefs.HasKey("VersionCommonPersistent"))
            {
                common_ver = PlayerPrefs.GetString("VersionCommonPersistent");
            }

            if (string.IsNullOrEmpty(common_ver))
            {
                StartCoroutine(_copyStreamingAssets2PersistentAsync(_launch));
            }
            else
            {
                _launch();
            }
        }

        //---------------------------------------------------------------------
        IEnumerator _copyStreamingAssets2PersistentAsync(Action cb)
        {
            string s = Application.streamingAssetsPath;

            using (UnityWebRequest www_request = UnityWebRequest.Get(s + "/CommonInfo.json"))
            {
                yield return www_request.SendWebRequest();

                CommonInfo = LitJson.JsonMapper.ToObject<CommonInfo>(www_request.downloadHandler.text);
            }

            Dictionary<string, UnityWebRequest> map_www = new Dictionary<string, UnityWebRequest>();
            foreach (var i in CommonInfo.CommonFileList)
            {
                UnityWebRequest www_request = UnityWebRequest.Get(s + "/" + i);
                www_request.SendWebRequest();
                map_www[i] = www_request;
            }

            List<string> list_key = new List<string>();
            while (true)
            {
                list_key.Clear();
                list_key.AddRange(map_www.Keys);
                foreach (var i in list_key)
                {
                    var www = map_www[i];
                    if (www.isDone)
                    {
                        string path = Application.persistentDataPath + "/" + i;

                        string p = Path.GetDirectoryName(path);
                        if (!Directory.Exists(p))
                        {
                            Debug.Log(p);
                            Directory.CreateDirectory(p);
                        }

                        File.WriteAllBytes(path, www.downloadHandler.data);

                        map_www.Remove(i);
                    }
                }

                if (map_www.Count == 0) break;
                yield return 0;
            }

            PlayerPrefs.SetString("VersionCommonPersistent", CommonInfo.CommonVersion);

            cb();
        }

        //---------------------------------------------------------------------
        void _launch()
        {
            string s = Application.persistentDataPath + "/Common/Cs/";
            //s = s.Replace('\\', '/');
            //s = s.Replace("Assets/StreamingAssets", "");
            //s += "Script/Script.CSharp/bin/";
            //Debug.Log(s);
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
                            AppDomain.DelegateManager.RegisterMethodDelegate<string>();
                            AppDomain.DelegateManager.RegisterMethodDelegate<AssetBundle>();
                            AppDomain.DelegateManager.RegisterMethodDelegate<Texture>();
                            //AppDomain.DelegateManager.RegisterFunctionDelegate<int, float, bool>();

                            ILRuntime.Runtime.Generated.CLRBindings.Initialize(AppDomain);
                            LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(AppDomain);

                            string platform = "Android";
                            bool is_editor = false;
#if UNITY_STANDALONE_WIN
                            platform = "PC";
#elif UNITY_ANDROID && UNITY_EDITOR
                            platform = "Android";
#elif UNITY_ANDROID
                            platform = "Android";
#elif UNITY_IPHONE
                            platform = "iOS";
#endif

#if UNITY_EDITOR
                            is_editor = true;
#else
                            is_editor = false;
#endif

                            bool is_editor_debug = false;
                            AppDomain.Invoke("Cs.Main", "Create", null, new object[] { platform, is_editor, is_editor_debug });
                        }
                    }
                }
            }
        }
    }
}
