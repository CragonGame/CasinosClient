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

    public class LaunchInfo
    {
        public string LaunchVersion { get; set; }
        public List<string> LaunchFileList { get; set; }
    }

    public class MbLaunch : MonoBehaviour
    {
        //---------------------------------------------------------------------
        LaunchInfo LaunchInfo { get; set; }
        ILRuntime.Runtime.Enviorment.AppDomain AppDomain { get; set; } = null;
        MemoryStream MsCommonDll { get; set; }
        MemoryStream MsCommonPdb { get; set; }
        MemoryStream MsScriptDll { get; set; }
        MemoryStream MsScriptPdb { get; set; }

        //---------------------------------------------------------------------
        public void Close()
        {
            OnDestroy();
        }

        //---------------------------------------------------------------------
        public void Init()
        {
            Start();
        }

        //---------------------------------------------------------------------
        void Start()
        {
            LaunchInfo = null;
            AppDomain = new ILRuntime.Runtime.Enviorment.AppDomain();

            // 读取VersionLaunchPersistent
            string launch_ver = string.Empty;
            if (PlayerPrefs.HasKey("VersionLaunchPersistent"))
            {
                launch_ver = PlayerPrefs.GetString("VersionLaunchPersistent");
            }

            if (string.IsNullOrEmpty(launch_ver))
            {
                StartCoroutine(_copyStreamingAssets2PersistentAsync(_launch));
            }
            else
            {
                _launch();
            }
        }

        //---------------------------------------------------------------------
        private void Update()
        {
            if (AppDomain != null)
            {
                AppDomain.Invoke("Cs.Main", "Update", null, new object[] { Time.deltaTime });
            }
        }

        //---------------------------------------------------------------------
        private void OnDestroy()
        {
            if (AppDomain != null)
            {
                AppDomain.Invoke("Cs.Main", "Destroy", null, new object[] { });
                AppDomain = null;
            }

            if (MsCommonDll != null)
            {
                MsCommonDll.Close();
                MsCommonDll = null;
            }

            if (MsCommonPdb != null)
            {
                MsCommonPdb.Close();
                MsCommonPdb = null;
            }

            if (MsScriptDll != null)
            {
                MsScriptDll.Close();
                MsScriptDll = null;
            }

            if (MsScriptPdb != null)
            {
                MsScriptPdb.Close();
                MsScriptPdb = null;
            }

            Debug.Log("MbLaunch.OnDestroy()");
        }

        //---------------------------------------------------------------------
        IEnumerator _copyStreamingAssets2PersistentAsync(Action cb)
        {
            string s = Application.streamingAssetsPath;

            using (UnityWebRequest www_request = UnityWebRequest.Get(s + "/LaunchInfo.json"))
            {
                yield return www_request.SendWebRequest();

                LaunchInfo = LitJson.JsonMapper.ToObject<LaunchInfo>(www_request.downloadHandler.text);
            }

            Dictionary<string, UnityWebRequest> map_www = new Dictionary<string, UnityWebRequest>();
            foreach (var i in LaunchInfo.LaunchFileList)
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
                            Directory.CreateDirectory(p);
                        }

                        File.WriteAllBytes(path, www.downloadHandler.data);

                        map_www.Remove(i);
                    }
                }

                if (map_www.Count == 0) break;
                yield return 0;
            }

            PlayerPrefs.SetString("VersionLaunchPersistent", LaunchInfo.LaunchVersion);

            cb();
        }

        //---------------------------------------------------------------------
        void _launch()
        {
            string s = Application.persistentDataPath + "/Launch/Cs/";
            //s = s.Replace('\\', '/');
            //s = s.Replace("Assets/StreamingAssets", "");
            //s += "Script/Script.CSharp/bin/";
            //Debug.Log(s);

#if UNITY_EDITOR
            // 检测Script.CSharp.dll是否存在，如不存在则给出提示
#endif

            byte[] dll1 = File.ReadAllBytes(s + "Script.Common.dll");
            byte[] pdb1 = File.ReadAllBytes(s + "Script.Common.pdb");

            byte[] dll = File.ReadAllBytes(s + "Script.CSharp.dll");
            byte[] pdb = File.ReadAllBytes(s + "Script.CSharp.pdb");

            MsCommonDll = new MemoryStream(dll1);
            MsCommonPdb = new MemoryStream(pdb1);
            MsScriptDll = new MemoryStream(dll);
            MsScriptPdb = new MemoryStream(pdb);

            AppDomain.LoadAssembly(MsCommonDll, MsCommonPdb, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            AppDomain.LoadAssembly(MsScriptDll, MsScriptPdb, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());

            // 这里做一些ILRuntime的注册
            AppDomain.DelegateManager.RegisterMethodDelegate<string>();
            AppDomain.DelegateManager.RegisterMethodDelegate<List<string>>();
            AppDomain.DelegateManager.RegisterMethodDelegate<AssetBundle>();
            AppDomain.DelegateManager.RegisterMethodDelegate<Texture>();

            LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(AppDomain);
            ILRuntime.Runtime.Generated.CLRBindings.Initialize(AppDomain);

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
            AppDomain.DebugService.StartDebugService(56000);
#else
            is_editor = false;
#endif

            bool is_editor_debug = false;
            AppDomain.Invoke("Cs.Main", "Create", null, new object[] { platform, is_editor, is_editor_debug });
        }
    }
}
