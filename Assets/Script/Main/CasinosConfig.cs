// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class LaunchInfo
    {
        public List<string> ListLua;
    }

    public class CasinosConfig
    {
        //---------------------------------------------------------------------
        public GameObject GoMain { get; private set; }
        public string Platform { get; private set; }// Android, iOS, PC
        public string Channel { get; private set; }// 渠道
        public string VersionBundle { get; private set; }
        public string VersionCommonPersistent { get; private set; }
        public string VersionDataPersistent { get; private set; }
        public LaunchInfo LaunchInfoResources { get; set; }// Launch配置信息

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

            Channel = "";
            GoMain = GameObject.Find(StringDef.GoMainObj);
            VersionBundle = Application.version;

            // 读取VersionCommonPersistent
            VersionCommonPersistent = string.Empty;
            if (PlayerPrefs.HasKey("VersionCommonPersistent"))
            {
                VersionCommonPersistent = PlayerPrefs.GetString("VersionCommonPersistent");
            }

            // 读取VersionDataPersistent
            VersionDataPersistent = string.Empty;
            if (PlayerPrefs.HasKey("VersionDataPersistent"))
            {
                VersionDataPersistent = PlayerPrefs.GetString("VersionDataPersistent");
            }

            var launch_info = Resources.Load("LaunchInfo") as TextAsset;
            LaunchInfoResources = JsonUtility.FromJson<LaunchInfo>(launch_info.text);
        }

        //---------------------------------------------------------------------
        public void WriteVersionCommonPersistent(string version_common_persistent_new)
        {
            VersionCommonPersistent = version_common_persistent_new;
            PlayerPrefs.SetString("VersionCommonPersistent", VersionCommonPersistent);
        }

        //---------------------------------------------------------------------
        public void WriteVersionDataPersistent(string version_data_persistent_new)
        {
            VersionDataPersistent = version_data_persistent_new;
            PlayerPrefs.SetString("VersionDataPersistent", VersionDataPersistent);
        }

        //---------------------------------------------------------------------
        // v1>v2, return 1; v1=v2, return 0; v1<v2, return -1;
        public int VersionCompare(string v1, string v2)
        {
            if (string.IsNullOrEmpty(v1)) return -1;
            if (string.IsNullOrEmpty(v2)) return 1;

            string v11 = v1.Replace(".", "");
            string v22 = v2.Replace(".", "");
            long i1 = long.Parse(v11);
            long i2 = long.Parse(v22);

            if (i1 > i2) return 1;
            else if (i1 == i2) return 0;
            else return -1;
        }
    }
}