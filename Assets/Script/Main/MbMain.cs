// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.IO;
    using UnityEngine;

    public class EditorCfgUserSettingsCopy
    {
        //-------------------------------------------------------------------------
        public bool ForceUseDirResourcesLaunch;// 是否强制使用目录ResourcesLaunch
        public bool ForceUseDirDataOss;// 是否强制使用目录DataOss
    }

    public class MbMain : MonoBehaviour
    {
        //---------------------------------------------------------------------
        CasinosContext Context { get; set; }

        //---------------------------------------------------------------------
        void Awake()
        {
            bool force_use_resouceslaunch = false;
            bool force_use_dataoss = false;

#if UNITY_EDITOR
            EditorCfgUserSettingsCopy cfg_usersettings = null;
            string p = Path.Combine(Environment.CurrentDirectory, "./Assets/SettingsUser/");
            var di = new DirectoryInfo(p);
            string path_settingsuser = di.FullName;

            string full_filename = Path.Combine(path_settingsuser, StringDef.FileEditorUserSettings);
            if (File.Exists(full_filename))
            {
                using (StreamReader sr = File.OpenText(full_filename))
                {
                    string s = sr.ReadToEnd();
                    cfg_usersettings = JsonUtility.FromJson<EditorCfgUserSettingsCopy>(s);
                }
            }

            if (cfg_usersettings != null)
            {
                force_use_resouceslaunch = cfg_usersettings.ForceUseDirResourcesLaunch;
                force_use_dataoss = cfg_usersettings.ForceUseDirDataOss;
            }

            string info = string.Format("ForceUseDirResourcesLaunch={0}, ForceUseDirDataOss={1}",
                force_use_resouceslaunch, force_use_dataoss);
            Debug.Log(info);
            Debug.Log("PersistentDataPath=" + Application.persistentDataPath);
#endif

            Context = new CasinosContext(force_use_resouceslaunch, force_use_dataoss);
            Context.Launch();
        }

        //---------------------------------------------------------------------
        void Update()
        {
            Context.Update(Time.deltaTime);
        }

        //---------------------------------------------------------------------
        void OnDestroy()
        {
            Context.Close();
        }

        //-------------------------------------------------------------------------
        void OnApplicationPause(bool pause)
        {
            Context.OnApplicationPause(pause);
        }

        //-------------------------------------------------------------------------
        void OnApplicationFocus(bool focusStatus)
        {
            Context.OnApplicationFocus(focusStatus);
        }
    }
}