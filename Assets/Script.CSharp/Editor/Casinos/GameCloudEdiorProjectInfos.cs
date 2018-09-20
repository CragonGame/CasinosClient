using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class GameCloudEdiorProjectInfos : EditorWindow
{
    //-------------------------------------------------------------------------
    static List<_InitProjectInfo> ListInitProjectInfo { get; set; }
    static string[] ArrayProjectBundleIdentity { get; set; }
    static Dictionary<int, int> MapProjectIndexCombineWithSelectIndex { get; set; }//key projectindex,value selectindex
    static _InitProjectInfo CurrentProject { get; set; }
    static string mTargetPath;
    static string mABTargetPathRoot;
    static string mABTargetPathCurrent;
    static string mAssetPath;
    static int CurrentSelectIndex { get; set; }
    _InitProjectInfo AddProject { get; set; }
    bool AddingProject { get; set; }

    //-------------------------------------------------------------------------
    [MenuItem("CasinosPublish/AddProjectInfo", false, 3)]
    static void AutoRun()
    {
        _checkTargetPath();
        _getAllProjectAndCurrentProject();

        EditorWindow.GetWindow<GameCloudEdiorProjectInfos>();
    }

    //-------------------------------------------------------------------------
    static void _checkTargetPath()
    {
        string current_dir = System.Environment.CurrentDirectory;
        current_dir = current_dir.Replace(@"\", "/");
        mAssetPath = current_dir + "/Assets/";

        mABTargetPathRoot = Path.Combine(current_dir, GameCloudEditor.AssetBundleTargetDirectory);
        mABTargetPathRoot = mABTargetPathRoot.Replace(@"\", "/");
    }

    //-------------------------------------------------------------------------
    static void _getAllProjectAndCurrentProject()
    {
        ListInitProjectInfo = new List<_InitProjectInfo>();
        MapProjectIndexCombineWithSelectIndex = new Dictionary<int, int>();
        if (!Directory.Exists(mABTargetPathRoot))
        {
            Directory.CreateDirectory(mABTargetPathRoot);
        }
        var project_infoderectories = Directory.GetDirectories(mABTargetPathRoot);

        foreach (var i in project_infoderectories)
        {
            if (i.Contains("CommonLua"))
            {
                continue;
            }

            var project_info_lines = File.ReadAllText(i + "/" + GameCloudEditorInitProjectInfo.PROJECT_INFO_FILE_NAME);
            _InitProjectInfo project_info = EbTool.jsonDeserialize<_InitProjectInfo>(project_info_lines);
            if (CurrentProject == null)
            {
                CurrentProject = project_info;
            }
            ListInitProjectInfo.Add(project_info);
        }

        ListInitProjectInfo.Sort((x, y) => x.ProjectIndex.CompareTo(y.ProjectIndex));
        _combineProjectIndexWithSelectIndex();
        ArrayProjectBundleIdentity = ListInitProjectInfo.Select(x => x.BundleIdentify).ToArray();
    }

    //-------------------------------------------------------------------------
    static void _decideCurrentProject(int project_index)
    {
        var project_i = MapProjectIndexCombineWithSelectIndex.First(x => x.Value.Equals(project_index));

        _InitProjectInfo project_info = ListInitProjectInfo.Find(x => x.ProjectIndex == project_i.Key);
        if (project_info != null)
        {
            CurrentProject = project_info;
        }
    }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        if (!AddingProject)
        {
            if (CurrentProject != null)
            {
                EditorGUILayout.BeginHorizontal();
                int select_index = 0;
                if (!MapProjectIndexCombineWithSelectIndex.TryGetValue(CurrentProject.ProjectIndex, out select_index))
                {
                    return;
                }
                select_index = MapProjectIndexCombineWithSelectIndex[CurrentProject.ProjectIndex];
                select_index = EditorGUILayout.Popup("当前项目：", select_index, ArrayProjectBundleIdentity);
                if (CurrentSelectIndex != select_index)
                {
                    _decideCurrentProject(select_index);
                    CurrentSelectIndex = select_index;
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("当前项目公司名:", CurrentProject.CompanyName);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("当前项目名:", CurrentProject.ProjectSourceFolderName);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("当前项目App名:", CurrentProject.AppName);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("当前项目BundleIdentify:", CurrentProject.BundleIdentify);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                bool click_delete_project = GUILayout.Button("删除选中项目信息", GUILayout.Width(200));
                if (click_delete_project)
                {
                    _deleteProject();
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("==================华丽的分割线==================");
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            bool click_add_project = GUILayout.Button("添加项目信息", GUILayout.Width(200));
            if (click_add_project)
            {
                AddProject = new _InitProjectInfo();
                AddProject.IsDefault = ListInitProjectInfo.Count == 0 ? true : false;
                AddingProject = true;
            }
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            _drowAddProjectWindow();

            EditorGUILayout.BeginHorizontal();
            bool click_add_project = GUILayout.Button("确定", GUILayout.Width(200));
            if (click_add_project)
            {
                int add_projectindex = 0;
                if (ListInitProjectInfo.Count != 0)
                {
                    add_projectindex = ListInitProjectInfo[ListInitProjectInfo.Count - 1].ProjectIndex + 1;
                }
                AddProject.ProjectIndex = add_projectindex;

                if (AddProject != null && AddProject.isAllInit())
                {
                    _addProject();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    //-------------------------------------------------------------------------
    void _deleteProject()
    {
        string delete_project = mABTargetPathRoot + "/" + CurrentProject.BundleIdentify;
        GameCloudEditor.deleteFile(delete_project);

        ListInitProjectInfo.Remove(CurrentProject);
        _combineProjectIndexWithSelectIndex();
        ArrayProjectBundleIdentity = ListInitProjectInfo.Select(x => x.BundleIdentify).ToArray();
        if (ListInitProjectInfo.Count > 0)
        {
            CurrentProject = ListInitProjectInfo[0];
            CurrentSelectIndex = CurrentProject.ProjectIndex;
        }
        else
        {
            MapProjectIndexCombineWithSelectIndex.Clear();
            GameCloudEditor.deleteFile(mABTargetPathRoot);
            CurrentProject = null;
        }
    }

    //-------------------------------------------------------------------------
    void _addProject()
    {
        string target_directory = mABTargetPathRoot + "/" + AddProject.BundleIdentify;
        try
        {
            Directory.CreateDirectory(target_directory);
        }
        catch (Exception e)
        {
            Debug.LogError("GameCloudEditorInitProjectInfo CreateDirectory Error::" + e.Message);
        }

        try
        {
            string resource_path = mAssetPath + GameCloudEditor.ABPathInfoResourceDirectory;
            GameCloudEditor.copyFile(resource_path, target_directory, resource_path);
        }
        catch (Exception e)
        {
            Debug.LogError("GameCloudEditorInitProjectInfo copyFile Error::" + e.Message);
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("{");
        sb.Append("    \"ProjectIndex\" : ");
        sb.Append(AddProject.ProjectIndex);
        sb.Append(",");
        sb.AppendLine();
        sb.Append("    \"IsDefault\" : \"");
        sb.Append(AddProject.IsDefault);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"CompanyName\" : \"");
        sb.Append(AddProject.CompanyName);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"AppName\" : \"");
        sb.Append(AddProject.AppName);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"BundleIdentify\" : \"");
        sb.Append(AddProject.BundleIdentify);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"InitBundleVersion\" : \"");
        sb.Append(AddProject.InitBundleVersion);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"InitDataVersion\" : \"");
        sb.Append(AddProject.InitDataVersion);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("    \"ProjectSourceFolderName\" : \"");
        sb.Append(AddProject.ProjectSourceFolderName);
        sb.Append("\",");
        sb.AppendLine();
        sb.Append("}");
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(sb.ToString());

        string path = target_directory + "/" + GameCloudEditorInitProjectInfo.PROJECT_INFO_FILE_NAME;
        using (FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
        {
            fs.Write(bytes, 0, bytes.Length);
        }

        GameCloudEditor.changeBundleData(_ePlatform.None, target_directory, AddProject.InitBundleVersion, true);
        GameCloudEditor.changeDataData(_ePlatform.None, target_directory, AddProject.InitDataVersion, true);

        ListInitProjectInfo.Add(AddProject);
        _combineProjectIndexWithSelectIndex();
        ArrayProjectBundleIdentity = ListInitProjectInfo.Select(x => x.BundleIdentify).ToArray();
        if (CurrentProject == null)
        {
            CurrentProject = AddProject;
        }

        AddingProject = false;
        AddProject = null;
    }

    //-------------------------------------------------------------------------
    static void _combineProjectIndexWithSelectIndex()
    {
        MapProjectIndexCombineWithSelectIndex.Clear();
        int select_index = 0;
        foreach (var i in ListInitProjectInfo)
        {
            MapProjectIndexCombineWithSelectIndex[i.ProjectIndex] = select_index;
            select_index++;
        }
    }

    //-------------------------------------------------------------------------
    void _drowAddProjectWindow()
    {
        string company_name_title = "公司名:";
        string app_name_title = "App名:";
        string bundle_identify_title = "BundleIdentify:";

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(company_name_title);
        AddProject.CompanyName = EditorGUILayout.TextField(AddProject.CompanyName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(app_name_title);
        AddProject.AppName = EditorGUILayout.TextField(AddProject.AppName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(bundle_identify_title);
        AddProject.BundleIdentify = EditorGUILayout.TextField(AddProject.BundleIdentify);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("初始程序版本号(请以*.**.***，其中*为数字来设置):");
        AddProject.InitBundleVersion = EditorGUILayout.TextField(AddProject.InitBundleVersion);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("初始资源版本号(请以*.**.***，其中*为数字来设置):");
        AddProject.InitDataVersion = EditorGUILayout.TextField(AddProject.InitDataVersion);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("项目名:");
        AddProject.ProjectSourceFolderName = EditorGUILayout.TextField(AddProject.ProjectSourceFolderName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("==================华丽的分割线==================");
        EditorGUILayout.EndHorizontal();
    }
}
