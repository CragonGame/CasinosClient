// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using UnityEngine;

    public class CasinosConfig
    {
        //---------------------------------------------------------------------
        public string Platform { get; private set; }// Android, iOS, PC
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
    }
}