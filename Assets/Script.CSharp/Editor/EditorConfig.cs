using System.IO;

[System.Serializable]
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

[System.Serializable]
public class EditorCfgUserSettings
{
    //-------------------------------------------------------------------------
    public bool UseTmpDirRes;// 是否使用临时目录中的资源，true=临时目录，false=本地目录
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
            if (!File.Exists(full_filename))
            {
                CfgUserSettings = new EditorCfgUserSettings()
                {
                    UseTmpDirRes = true
                };

                using (StreamWriter sw = File.CreateText(full_filename))
                {
                    string s = UnityEngine.JsonUtility.ToJson(CfgUserSettings);
                    sw.Write(s);
                }
            }
            else
            {
                using (StreamReader sr = File.OpenText(full_filename))
                {
                    string s = sr.ReadToEnd();
                    CfgUserSettings = UnityEngine.JsonUtility.FromJson<EditorCfgUserSettings>(s);
                }
            }
        }

        // Load
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
    public void SaveValueUseTmpDirRes(bool use_tmpdir_res)
    {
        CfgUserSettings.UseTmpDirRes = use_tmpdir_res;

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