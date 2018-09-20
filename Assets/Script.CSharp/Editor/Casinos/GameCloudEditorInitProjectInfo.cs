using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public class GameCloudEditorInitProjectInfo : EditorWindow
{
    //-------------------------------------------------------------------------        
    string mPatchInfoTargetDirectory;
    string mPatchInfoResouceDirectory;
    int mInitProjectNum;
    bool mCanStartInitProjectDetail;
    Dictionary<int, _InitProjectInfo> MapInitProjectInfo { get; set; }
    Dictionary<int, _InitProjectInfo> MapChangeInitProjectInfo { get; set; }
    public const string PROJECT_INFO_FILE_NAME = "ProjectInfo.txt";

    //-------------------------------------------------------------------------
    public void copyPatchInfo(string patch_infotargetdirectory, string patch_inforesoucedirectory)
    {
        mCanStartInitProjectDetail = false;
        MapInitProjectInfo = new Dictionary<int, _InitProjectInfo>();
        MapChangeInitProjectInfo = new Dictionary<int, _InitProjectInfo>();
        mPatchInfoTargetDirectory = patch_infotargetdirectory;
        mPatchInfoResouceDirectory = patch_inforesoucedirectory;
    }

    //-------------------------------------------------------------------------
    void _deleteDirectory(string path)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path);
        }
    }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        if (mCanStartInitProjectDetail)
        {
            MapChangeInitProjectInfo.Clear();
            foreach (var i in MapInitProjectInfo)
            {
                var project_info = _drowInitProjectWindow(i.Value);
                MapChangeInitProjectInfo[project_info.ProjectIndex] = project_info;
            }

            foreach (var i in MapChangeInitProjectInfo)
            {
                _InitProjectInfo project_info = null;
                MapInitProjectInfo.TryGetValue(i.Key, out project_info);
                if (project_info != null)
                {
                    project_info.cloneData(i.Value);
                }
            }

            bool init = GUILayout.Button("设置", GUILayout.Width(200));
            if (init)
            {
                bool is_initall = true;
                foreach (var i in MapInitProjectInfo)
                {
                    if (!i.Value.isAllInit())
                    {
                        is_initall = false;
                    }
                }

                if (is_initall)
                {
                    _initProject();
                    ShowNotification(new GUIContent("初始化成功!"));
                }
            }
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("初始化项目个数:");
            int.TryParse(EditorGUILayout.TextField(mInitProjectNum.ToString()), out mInitProjectNum);
            EditorGUILayout.EndHorizontal();

            bool confirm_num = GUILayout.Button("确定", GUILayout.Width(200));
            if (confirm_num)
            {
                if (mInitProjectNum != 0)
                {
                    for (int i = 0; i < mInitProjectNum; i++)
                    {
                        bool is_default = false;
                        if (i == 0)
                        {
                            is_default = true;
                        }

                        _InitProjectInfo init_info = new _InitProjectInfo();
                        init_info.ProjectIndex = i;
                        init_info.IsDefault = is_default;
                        MapInitProjectInfo[init_info.ProjectIndex] = init_info;
                    }
                    mCanStartInitProjectDetail = true;
                }
            }
        }
    }

    //-------------------------------------------------------------------------
    _InitProjectInfo _drowInitProjectWindow(_InitProjectInfo project_info)
    {
        _InitProjectInfo project_infoex = new _InitProjectInfo();
        project_infoex.cloneData(project_info);

        string company_name_title = "公司名:";
        string app_name_title = "App名:";
        string bundle_identify_title = "BundleIdentify:";
        if (project_infoex.IsDefault)
        {
            company_name_title = "默认公司名:";
            app_name_title = "默认App名:";
            bundle_identify_title = "默认BundleIdentify:";
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(company_name_title);
        project_infoex.CompanyName = EditorGUILayout.TextField(project_infoex.CompanyName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(app_name_title);
        project_infoex.AppName = EditorGUILayout.TextField(project_infoex.AppName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(bundle_identify_title);
        project_infoex.BundleIdentify = EditorGUILayout.TextField(project_infoex.BundleIdentify);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("初始程序版本号(请以*.**.***，其中*为数字来设置):");
        project_infoex.InitBundleVersion = EditorGUILayout.TextField(project_infoex.InitBundleVersion);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("初始资源版本号(请以*.**.***，其中*为数字来设置):");
        project_infoex.InitDataVersion = EditorGUILayout.TextField(project_infoex.InitDataVersion);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("项目名:");
        project_infoex.ProjectSourceFolderName = EditorGUILayout.TextField(project_infoex.ProjectSourceFolderName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("==================华丽的分割线==================");
        EditorGUILayout.EndHorizontal();

        return project_infoex;
    }

    //-------------------------------------------------------------------------
    void _initProject()
    {
        foreach (var i in MapInitProjectInfo)
        {
            _InitProjectInfo project_info = i.Value;

            string target_directory = mPatchInfoTargetDirectory + "/" + project_info.BundleIdentify;
            try
            {
                Directory.CreateDirectory(target_directory);
            }
            catch (Exception e)
            {
                _deleteDirectory(target_directory);
                Debug.LogError("GameCloudEditorInitProjectInfo CreateDirectory Error::" + e.Message);
            }

            try
            {
                GameCloudEditor.copyFile(mPatchInfoResouceDirectory, target_directory, mPatchInfoResouceDirectory);
            }
            catch (Exception e)
            {
                _deleteDirectory(target_directory);
                Debug.LogError("GameCloudEditorInitProjectInfo copyFile Error::" + e.Message);
            }

            if (project_info.IsDefault)
            {
                PlayerSettings.companyName = project_info.CompanyName;
                PlayerSettings.productName = project_info.AppName;
                PlayerSettings.applicationIdentifier = project_info.BundleIdentify;
            }

            var project_info_str = getFormatProjectInfo(project_info);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(project_info_str);

            string path = target_directory + "/" + PROJECT_INFO_FILE_NAME;
            using (FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
            {
                fs.Write(bytes, 0, bytes.Length);
            }

            GameCloudEditor.changeBundleData(_ePlatform.None, target_directory, project_info.InitBundleVersion, true);
            GameCloudEditor.changeDataData(_ePlatform.None, target_directory, project_info.InitDataVersion, true);
        }
    }

    //-------------------------------------------------------------------------
    public static string getFormatProjectInfo(_InitProjectInfo project_info)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("{");
        sb.Append("    \"ProjectIndex\" : ");
        sb.Append(project_info.ProjectIndex);
        sb.Append(",");
        sb.AppendLine();
        sb.Append("    \"IsDefault\" : \"");
        sb.Append(project_info.IsDefault);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"CompanyName\" : \"");
        sb.Append(project_info.CompanyName);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"AppName\" : \"");
        sb.Append(project_info.AppName);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"BundleIdentify\" : \"");
        sb.Append(project_info.BundleIdentify);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"InitBundleVersion\" : \"");
        sb.Append(project_info.InitBundleVersion);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"InitDataVersion\" : \"");
        sb.Append(project_info.InitDataVersion);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"ProjectSourceFolderName\" : \"");
        sb.Append(project_info.ProjectSourceFolderName);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("}");

        return sb.ToString();
    }
}

//-------------------------------------------------------------------------
public class _InitProjectInfo
{
    public int ProjectIndex;
    public bool IsDefault;
    public string CompanyName;
    public string AppName;
    public string ProjectSourceFolderName;
    public string BundleIdentify;
    public string InitBundleVersion;
    public string InitDataVersion;

    //-------------------------------------------------------------------------
    public void cloneData(_InitProjectInfo project_info)
    {
        this.AppName = project_info.AppName;
        this.ProjectSourceFolderName = project_info.ProjectSourceFolderName;
        this.BundleIdentify = project_info.BundleIdentify;
        this.InitBundleVersion = project_info.InitBundleVersion;
        this.CompanyName = project_info.CompanyName;
        this.InitDataVersion = project_info.InitDataVersion;
        this.IsDefault = project_info.IsDefault;
        this.ProjectIndex = project_info.ProjectIndex;
    }

    //-------------------------------------------------------------------------
    public bool isAllInit()
    {
        return !string.IsNullOrEmpty(CompanyName) && !string.IsNullOrEmpty(AppName)
            && !string.IsNullOrEmpty(BundleIdentify) && !string.IsNullOrEmpty(InitBundleVersion)
            && !string.IsNullOrEmpty(InitDataVersion);
    }
}
