// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System.Collections.Generic;
    using UnityEngine;

    public class StreamingAssetsInfo
    {
        public string DataVersion { get; set; }
        public List<string> ListLaunchDir { get; set; }
    }

    public class CasinosConfig
    {
        //---------------------------------------------------------------------
        public GameObject GoConfig { get; private set; }
        public GameObject GoMain { get; private set; }
        public string Platform { get; private set; }// Android, iOS, PC
        public string Channel { get; private set; } = "";
        public StreamingAssetsInfo StreamingAssetsInfo { get; private set; }
        public string VersionBundle { get; private set; }
        public string VersionDataPersistent { get; private set; }
        public string VersionLaunchPersistent { get; private set; }

        //---------------------------------------------------------------------
        public CasinosConfig(_eEditorRunSourcePlatform editor_mode_runsources_platform)
        {
            switch (editor_mode_runsources_platform)
            {
                case _eEditorRunSourcePlatform.Android:
                    Platform = "ANDROID";
                    break;
                case _eEditorRunSourcePlatform.IOS:
                    Platform = "IOS";
                    break;
                case _eEditorRunSourcePlatform.PC:
                    Platform = "PC";
                    break;
                default:
                    break;
            }

            GoMain = GameObject.Find("Main Object");
            GoConfig = GameObject.Find("Main Config");
            VersionBundle = Application.version;

            // 读取VersionLaunchStreamingAssets
            var datafilelist_path = CasinosContext.Instance.PathMgr.combineWWWStreamingAssetsPath("StreamingAssetsInfo.json");
            WWW www = new WWW(datafilelist_path);
            while (!www.isDone)
            {
            }
            StreamingAssetsInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<StreamingAssetsInfo>(www.text);

            // 读取VersionLaunchPersistent
            if (PlayerPrefs.HasKey("VersionLaunchPersistent"))
            {
                VersionLaunchPersistent = PlayerPrefs.GetString("VersionLaunchPersistent");
            }

            // 读取VersionDataPersistent
            if (PlayerPrefs.HasKey("VersionDataPersistent"))
            {
                VersionDataPersistent = PlayerPrefs.GetString("VersionDataPersistent");
            }
        }

        //---------------------------------------------------------------------
        public void WriteVersionDataPersistent(string version_data_persistent_new)
        {
            VersionDataPersistent = version_data_persistent_new;
            PlayerPrefs.SetString("VersionDataPersistent", VersionDataPersistent);
        }
    }
}