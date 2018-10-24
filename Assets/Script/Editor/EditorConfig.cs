using System.IO;

public class EditorCfgProjectSettings
{
    //-------------------------------------------------------------------------
    public string CompanyName;
    public string AppName;
    public string BundleIdentify;
    public string InitDataVersion;
    public string CurrentDataVersionANDROID;
    public string CurrentDataVersionIOS;
}

public class EditorCfgUserSettings
{
    //-------------------------------------------------------------------------
    public bool ForceUseDirResourcesLaunch;// 是否强制使用目录ResourcesLaunch
    public bool ForceUseDirDataOss;// 是否强制使用目录DataOss
}

public class EditorConfig
{
    //-------------------------------------------------------------------------
    public EditorCfgProjectSettings CfgProjectSettings { get; private set; }
    public EditorCfgUserSettings CfgUserSettings { get; private set; }

    //-------------------------------------------------------------------------
    public EditorConfig()
    {
        // Load or Gen EditorUserSettings.json
        {
            if (!Directory.Exists(EditorContext.Instance.PathSettingsUser))
            {
                Directory.CreateDirectory(EditorContext.Instance.PathSettingsUser);
            }

            string full_filename = Path.Combine(EditorContext.Instance.PathSettingsUser, EditorStringDef.FileEditorUserSettings);
            if (File.Exists(full_filename))
            {
                using (StreamReader sr = File.OpenText(full_filename))
                {
                    string s = sr.ReadToEnd();
                    CfgUserSettings = UnityEngine.JsonUtility.FromJson<EditorCfgUserSettings>(s);
                }
            }

            if (CfgUserSettings == null)
            {
                CfgUserSettings = new EditorCfgUserSettings()
                {
                    ForceUseDirResourcesLaunch = true,
                    ForceUseDirDataOss = true
                };

                using (StreamWriter sw = File.CreateText(full_filename))
                {
                    string s = UnityEngine.JsonUtility.ToJson(CfgUserSettings);
                    sw.Write(s);
                }
            }
        }

        // Load EditorSettings.json
        {
            string full_filename = Path.Combine(EditorContext.Instance.PathSettings, EditorStringDef.FileEditorProjectSettings);
            using (StreamReader sr = File.OpenText(full_filename))
            {
                string s = sr.ReadToEnd();
                CfgProjectSettings = UnityEngine.JsonUtility.FromJson<EditorCfgProjectSettings>(s);
            }
        }
    }

    //-------------------------------------------------------------------------
    public void SaveValueForceUseDirResourcesLaunch(bool v)
    {
        CfgUserSettings.ForceUseDirResourcesLaunch = v;

        // EditorUserSettings.json
        string full_filename = Path.Combine(EditorContext.Instance.PathSettingsUser, EditorStringDef.FileEditorUserSettings);
        using (StreamWriter sw = File.CreateText(full_filename))
        {
            string s = UnityEngine.JsonUtility.ToJson(CfgUserSettings);
            sw.Write(s);
        }
    }

    //-------------------------------------------------------------------------
    public void SaveValueForceUseDirDataOss(bool v)
    {
        CfgUserSettings.ForceUseDirDataOss = v;

        // EditorUserSettings.json
        string full_filename = Path.Combine(EditorContext.Instance.PathSettingsUser, EditorStringDef.FileEditorUserSettings);
        using (StreamWriter sw = File.CreateText(full_filename))
        {
            string s = UnityEngine.JsonUtility.ToJson(CfgUserSettings);
            sw.Write(s);
        }
    }

    //-------------------------------------------------------------------------
    public void SaveProjectSettings()
    {
        string full_filename = Path.Combine(EditorContext.Instance.PathSettings, EditorStringDef.FileEditorProjectSettings);

        using (StreamWriter sw = File.CreateText(full_filename))
        {
            string s = UnityEngine.JsonUtility.ToJson(CfgProjectSettings);
            sw.Write(s);
        }
    }
}