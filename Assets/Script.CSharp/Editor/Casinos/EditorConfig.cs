using System.IO;

public class EditorCfgProjectSettings
{
    //-------------------------------------------------------------------------
    public string CompanyName { get; set; }
    public string AppName { get; set; }
    public string BundleIdentify { get; set; }
    public string InitDataVersion { get; set; }
    public string CurrentDataVersionANDROID { get; set; }
    public string CurrentDataVersionIOS { get; set; }
}

public class EditorCfgUserSettings
{
    //-------------------------------------------------------------------------
    public bool UseTmpDirRes { get; set; }// 是否使用临时目录中的资源，true=临时目录，false=本地目录
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
            string full_filename = Path.Combine(EditorContext.Instance.PathSettingsUser, EditorStringDef.FileEditorUserSettings);
            if (!File.Exists(full_filename))
            {
                CfgUserSettings = new EditorCfgUserSettings()
                {
                    UseTmpDirRes = false
                };

                using (StreamWriter sw = File.CreateText(full_filename))
                {
                    string s = Newtonsoft.Json.JsonConvert.SerializeObject(CfgUserSettings);
                    sw.Write(s);
                }
            }
            else
            {
                using (StreamReader sr = File.OpenText(full_filename))
                {
                    string s = sr.ReadToEnd();
                    CfgUserSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<EditorCfgUserSettings>(s);
                }
            }
        }

        // Load
        {
            string full_filename = Path.Combine(EditorContext.Instance.PathSettings, EditorStringDef.FileEditorProjectSettings);
            using (StreamReader sr = File.OpenText(full_filename))
            {
                string s = sr.ReadToEnd();
                CfgProjectSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<EditorCfgProjectSettings>(s);
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
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(CfgUserSettings);
            sw.Write(s);
        }
    }

    //-------------------------------------------------------------------------
    public void SaveProjectSettings()
    {
        string full_filename = Path.Combine(EditorContext.Instance.PathSettings, EditorStringDef.FileEditorProjectSettings);

        using (StreamWriter sw = File.CreateText(full_filename))
        {
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(CfgProjectSettings);
            sw.Write(s);
        }
    }
}