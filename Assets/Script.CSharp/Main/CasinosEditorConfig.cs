namespace Casinos
{
    using System;
    using System.IO;

    public class CasinosEditorCfgProjectSettings
    {
        //---------------------------------------------------------------------
        public string CompanyName { get; set; }
        public string AppName { get; set; }
        public string BundleIdentify { get; set; }
        public string InitDataVersion { get; set; }

        //"BundleIdentify" : "com.Cragon.KingTexas",
        //"InitBundleVersion" : "1.00.000",
        //"InitDataVersion" : "1.00.000",
        //"ProjectSourceFolderName" : "KingTexas",
    }

    public class CasinosEditorCfgUserSettings
    {
        //---------------------------------------------------------------------
        public bool UseTmpDirRes { get; set; }// 是否使用临时目录中的资源，true=临时目录，false=本地目录
    }

    public class CasinosEditorConfig
    {
        //---------------------------------------------------------------------
        public CasinosEditorCfgProjectSettings CfgProjectSettings { get; private set; }
        public CasinosEditorCfgUserSettings CfgUserSettings { get; private set; }
        public string PathAssets { get; private set; }// Unity3D的Assets目录
        public string PathSettings { get; private set; }// Settings目录
        public string PathSettingsUser { get; private set; }// SettingsUser目录

        //---------------------------------------------------------------------
        public CasinosEditorConfig()
        {
            // PathAssets
            {
                string p = Path.Combine(Environment.CurrentDirectory, "./Assets/");
                var di = new DirectoryInfo(p);
                PathAssets = di.FullName;
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

            // Load EditorUserSettings.json
            {
                string full_filename = Path.Combine(PathSettingsUser, StringDef.FileEditorUserSettings);
                if (!File.Exists(full_filename))
                {
                    CfgUserSettings = new CasinosEditorCfgUserSettings()
                    {
                        UseTmpDirRes = false
                    };
                }
                else
                {
                    using (StreamReader sr = File.OpenText(full_filename))
                    {
                        string s = sr.ReadToEnd();
                        CfgUserSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<CasinosEditorCfgUserSettings>(s);
                    }
                }
            }

            // Load
            {
                string full_filename = Path.Combine(PathSettings, StringDef.FileEditorProjectSettings);
                using (StreamReader sr = File.OpenText(full_filename))
                {
                    string s = sr.ReadToEnd();
                    CfgProjectSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<CasinosEditorCfgProjectSettings>(s);
                }
            }
        }
    }
}