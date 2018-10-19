using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class GameCloudEditor : EditorWindow
{
    //-------------------------------------------------------------------------        
    static string mTargetPlatformRootPath;
    static string mAssetBundleResourcesPath;
    static string mAssetBundleResourcesPkgSinglePath;
    static string mAssetBundleResourcesPkgFoldPath;
    static string mRowAssetPath;
    static string mABTargetPathRoot;
    static string mAssetPath;
    static BuildTarget mInitBuildTargetPlatform;
    static BuildTargetGroup mInitBuildTargetGroupPlatform;
    static BuildTarget mCurrentBuildTargetPlatform;
    static BuildTargetGroup mCurrentBuildTargetGroupPlatform;
    static string mPatchInfoPath;
    static List<string> mDoNotPackFileExtention = new List<string> { ".meta", ".DS_Store" };
    static List<string> mDoNotCopyFileName = new List<string> { "ConfigC.lua", "ConfigS.lua", "DBConfig.lua" };
    static List<string> mDoNotCopyDir = new List<string> { ".idea" };

    MD5 MD5 { get; set; }
    List<ProjectPlatformInfo> ListNeedBuildPlatform { get; set; }
    List<ProjectPlatformInfo> ListNeedCopyRawPlatform { get; set; }
    Queue<ProjectPlatformInfo> QueueNeedBuildPlatform { get; set; }
    List<ProjectPlatformInfo> ListNeedCopyPlatform { get; set; }
    Queue<ProjectPlatformInfo> QueueNeedCopyPlatform { get; set; }
    List<_InitProjectInfo> ListInitProjectInfo { get; set; }
    static Dictionary<string, ProjectPlatformInfo> MapProjectPlatformInfo { get; set; }
    static Dictionary<string, ProjectPlatformInfo> MapProjectPlatformInfoChanged { get; set; }
    Dictionary<int, int> MapProjectIndexCombineWithSelectIndex { get; set; }//key projectindex,value selectindex
    string[] ArrayProjectBundleIdentity { get; set; }
    _InitProjectInfo CurrentProject { get; set; }
    ProjectPlatformInfo CurrentBuildProjectPlatformInfo { get; set; }
    ProjectPlatformInfo CurrentPlatformProjectPlatformInfo { get; set; }
    static string CurrentProjectABTargetPath { get; set; }
    int CurrentSelectIndex { get; set; }
    public const string AssetBundleTargetDirectory = "ABPatch";
    public const string ABPathInfoResourceDirectory = "GameCloud.Unity.Client/AutoPatcherInfo";
    public const string AssetRoot = "Assets/";
    public const string PackInfoTextName = "DataFileList.txt";
    const string PatchInfoName = "ABPatchInfo.xml";
    const string AssetBundlePkgSingleFoldName = "PkgSingle";
    const string AssetBundlePkgFoldFoldName = "PkgFold";
    string mDataTargetPath;
    bool mCopyAndroid = false;
    bool mCopyIOS = false;
    bool mCopyPC = false;
    bool mIsPackSelectItem = true;
    List<string> mListAllPkgSingleABFile = new List<string>();
    List<string> mListAllPkgFoldABFold = new List<string>();

    //-------------------------------------------------------------------------
    [MenuItem("CasinosPublish/GenDataRes", false, 2)]
    static void AutoPatcher()
    {
        CreateInstance<GameCloudEditor>();

        if (!Directory.Exists(mABTargetPathRoot) || Directory.GetDirectories(mABTargetPathRoot).Length == 0)
        {
            GameCloudEditorInitProjectInfo test = GetWindow<GameCloudEditorInitProjectInfo>("初始化项目信息");
            test.copyPatchInfo(mABTargetPathRoot, mAssetPath + ABPathInfoResourceDirectory);
            return;
        }

        EditorWindow.GetWindow<GameCloudEditor>();
    }

    //-------------------------------------------------------------------------
    void OnEnable()
    {
        MD5 = new MD5CryptoServiceProvider();
        ListNeedBuildPlatform = new List<ProjectPlatformInfo>();
        ListNeedCopyRawPlatform = new List<ProjectPlatformInfo>();
        QueueNeedBuildPlatform = new Queue<ProjectPlatformInfo>();
        ListNeedCopyPlatform = new List<ProjectPlatformInfo>();
        QueueNeedCopyPlatform = new Queue<ProjectPlatformInfo>();
        MapProjectPlatformInfo = new Dictionary<string, ProjectPlatformInfo>();
        MapProjectPlatformInfoChanged = new Dictionary<string, ProjectPlatformInfo>();
        _checkTargetPath();
        _getAllProjectAndCurrentProject();
        _initCurrentProject();
        _translatePatchXml(mPatchInfoPath);
        _initCurrentBuildTarget();
        _checkResourcesPath();
        _getCurrentTargetPath();
        //_checkPatchData();
    }

    //-------------------------------------------------------------------------
    void _initCurrentBuildTarget()
    {
        string current_platformkey = "";
#if UNITY_IPHONE || UNITY_IOS
        mInitBuildTargetPlatform = BuildTarget.iOS;
        mInitBuildTargetGroupPlatform = BuildTargetGroup.iOS;
        current_platformkey = "IOS";
#elif UNITY_ANDROID
        mInitBuildTargetPlatform = BuildTarget.Android;
        mInitBuildTargetGroupPlatform = BuildTargetGroup.Android;
        current_platformkey = "ANDROID";
#elif UNITY_STANDALONE_WIN
        mInitBuildTargetPlatform = BuildTarget.StandaloneWindows64;
        mInitBuildTargetGroupPlatform = BuildTargetGroup.Standalone;
        current_platformkey = "PC";
#endif
        mCurrentBuildTargetPlatform = mInitBuildTargetPlatform;
        mCurrentBuildTargetGroupPlatform = mInitBuildTargetGroupPlatform;
        CurrentPlatformProjectPlatformInfo = MapProjectPlatformInfo[current_platformkey];
    }

    //-------------------------------------------------------------------------
    void _checkTargetPath()
    {
        string current_dir = System.Environment.CurrentDirectory;
        current_dir = current_dir.Replace(@"\", "/");
        mAssetPath = current_dir + "/" + AssetRoot;

        mABTargetPathRoot = Path.Combine(current_dir, AssetBundleTargetDirectory);
        mABTargetPathRoot = mABTargetPathRoot.Replace(@"\", "/");
    }

    //-------------------------------------------------------------------------
    void _getAllProjectAndCurrentProject()
    {
        ListInitProjectInfo = new List<_InitProjectInfo>();
        MapProjectIndexCombineWithSelectIndex = new Dictionary<int, int>();
        var project_infoderectories = Directory.GetDirectories(mABTargetPathRoot);

        foreach (var i in project_infoderectories)
        {
            if (i.Contains("CommonLua"))
            {
                continue;
            }

            var project_info_lines = File.ReadAllText(i + "/" + GameCloudEditorInitProjectInfo.PROJECT_INFO_FILE_NAME);
            _InitProjectInfo project_info = EbTool.jsonDeserialize<_InitProjectInfo>(project_info_lines);
            if (project_info.IsDefault)
            {
                CurrentProject = project_info;
            }

            ListInitProjectInfo.Add(project_info);
        }

        ListInitProjectInfo.Sort((x, y) => x.ProjectIndex.CompareTo(y.ProjectIndex));
        if (CurrentProject == null)
        {
            CurrentProject = ListInitProjectInfo[0];
            _changeDefaultProject(true);
        }
        _combineProjectIndexWithSelectIndex();
        ArrayProjectBundleIdentity = ListInitProjectInfo.Select(x => x.BundleIdentify).ToArray();
    }

    //-------------------------------------------------------------------------
    void _decideCurrentProject(int project_index)
    {
        var project_i = MapProjectIndexCombineWithSelectIndex.First(x => x.Value.Equals(project_index));

        _InitProjectInfo project_info = ListInitProjectInfo.Find(x => x.ProjectIndex == project_i.Key);
        if (project_info != null)
        {
            _changeDefaultProject(false);
            CurrentProject = project_info;
            _changeDefaultProject(true);
        }
    }

    //-------------------------------------------------------------------------
    void _changeDefaultProject(bool is_default)
    {
        CurrentProject.IsDefault = is_default;
        using (StreamWriter sw = new StreamWriter(mABTargetPathRoot + "/" + CurrentProject.BundleIdentify + "/" + GameCloudEditorInitProjectInfo.PROJECT_INFO_FILE_NAME))
        {
            sw.Write(GameCloudEditorInitProjectInfo.getFormatProjectInfo(CurrentProject));
        }
    }

    //-------------------------------------------------------------------------
    void _initCurrentProject()
    {
        if (CurrentProject == null)
        {
            return;
        }

        //PlayerSettings.companyName = CurrentProject.CompanyName;
        //PlayerSettings.productName = CurrentProject.AppName;
        //PlayerSettings.applicationIdentifier = CurrentProject.BundleIdentify;
        mPatchInfoPath = Path.Combine(mABTargetPathRoot, CurrentProject.BundleIdentify);
        mPatchInfoPath = Path.Combine(mPatchInfoPath, PatchInfoName);
        mPatchInfoPath = mPatchInfoPath.Replace(@"\", "/");
        CurrentProjectABTargetPath = Path.Combine(mABTargetPathRoot, CurrentProject.BundleIdentify);
        CurrentProjectABTargetPath = CurrentProjectABTargetPath.Replace(@"\", "/");
    }

    //-------------------------------------------------------------------------
    void _combineProjectIndexWithSelectIndex()
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
    void _checkResourcesPath()
    {
        string folder_suffix = CurrentProject.ProjectSourceFolderName;
        mAssetBundleResourcesPath = mAssetPath + "/Resources." + folder_suffix;
        mAssetBundleResourcesPkgSinglePath = mAssetBundleResourcesPath + "/" + AssetBundlePkgSingleFoldName;
        mAssetBundleResourcesPkgFoldPath = mAssetBundleResourcesPath + "/" + AssetBundlePkgFoldFoldName;
        if (!Directory.Exists(mAssetBundleResourcesPath))
        {
            Directory.CreateDirectory(mAssetBundleResourcesPath);
        }
        if (!Directory.Exists(mAssetBundleResourcesPkgSinglePath))
        {
            Directory.CreateDirectory(mAssetBundleResourcesPkgSinglePath);
        }
        if (!Directory.Exists(mAssetBundleResourcesPkgFoldPath))
        {
            Directory.CreateDirectory(mAssetBundleResourcesPkgFoldPath);
        }

        mAssetBundleResourcesPath = "Assets/Resources." + folder_suffix;
        mAssetBundleResourcesPkgSinglePath = mAssetBundleResourcesPath + "/" + AssetBundlePkgSingleFoldName;
        mAssetBundleResourcesPkgFoldPath = mAssetBundleResourcesPath + "/" + AssetBundlePkgFoldFoldName;
        mRowAssetPath = mAssetPath + "/Resources." + folder_suffix + "Raw";
        if (!Directory.Exists(mRowAssetPath))
        {
            Directory.CreateDirectory(mRowAssetPath);
        }
        mRowAssetPath = "Assets/Resources." + folder_suffix + "Raw";

        //mConfigPath = mAssetPath + "Resources." + folder_suffix + "Config";
        //if (!Directory.Exists(mConfigPath))
        //{
        //    Directory.CreateDirectory(mConfigPath);
        //}
        //mConfigPath = "Assets/Resources." + folder_suffix + "Config";
    }

    //-------------------------------------------------------------------------
    void _getCurrentTargetPath()
    {
        if (mCurrentBuildTargetPlatform == BuildTarget.Android)
        {
            mTargetPlatformRootPath = CurrentProjectABTargetPath + "/ANDROID/";
            CurrentBuildProjectPlatformInfo = MapProjectPlatformInfo["ANDROID"];
        }
        else if (mCurrentBuildTargetPlatform == BuildTarget.iOS)
        {
            mTargetPlatformRootPath = CurrentProjectABTargetPath + "/IOS/";
            CurrentBuildProjectPlatformInfo = MapProjectPlatformInfo["IOS"];
        }
        else if (mCurrentBuildTargetPlatform == BuildTarget.StandaloneWindows64)
        {
            mTargetPlatformRootPath = CurrentProjectABTargetPath + "/PC/";
            CurrentBuildProjectPlatformInfo = MapProjectPlatformInfo["PC"];
        }

        //PlayerSettings.bundleVersion = CurrentBuildProjectPlatformInfo.BundleVersion;
    }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        int select_index = 0;
        if (CurrentProject != null)
        {
            if (!MapProjectIndexCombineWithSelectIndex.TryGetValue(CurrentProject.ProjectIndex, out select_index))
            {
                return;
            }

            select_index = EditorGUILayout.Popup("当前项目：", select_index, ArrayProjectBundleIdentity);
            if (CurrentSelectIndex != select_index)
            {
                _decideCurrentProject(select_index);
                _initCurrentProject();
                _checkResourcesPath();
                _getCurrentTargetPath();
                _translatePatchXml(mPatchInfoPath);
                CurrentSelectIndex = select_index;
                ListNeedBuildPlatform.Clear();
                ListNeedCopyRawPlatform.Clear();
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        mIsPackSelectItem = EditorGUILayout.Toggle("BuildSelectItems", mIsPackSelectItem);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("AB资源所在路径:", mAssetBundleResourcesPath);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("裸资源所在路径:", mRowAssetPath);
        EditorGUILayout.EndHorizontal();

        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("Config所在路径:", mConfigPath);
        //EditorGUILayout.EndHorizontal();

        _drawSplitLine();

        MapProjectPlatformInfoChanged.Clear();
        foreach (var item in MapProjectPlatformInfo)
        {
            var platform_info = _drwaProjectPlatformInfo(item.Value);
            MapProjectPlatformInfoChanged[item.Key] = platform_info;
        }

        foreach (var i in MapProjectPlatformInfoChanged)
        {
            ProjectPlatformInfo project_info = null;
            MapProjectPlatformInfo.TryGetValue(i.Key, out project_info);
            if (project_info != null)
            {
                project_info.cloneData(i.Value);
            }
        }

        EditorGUILayout.BeginHorizontal();
        bool click_build_asset = GUILayout.Button("打AssetBundle包(压缩)", GUILayout.Width(200));
        if (click_build_asset)
        {
            _startBuild();
        }

        bool click_build_copy_raw = GUILayout.Button("CopyAssetsRawFoldToABFold", GUILayout.Width(200));
        if (click_build_copy_raw)
        {
            _startCopyRawAndConfig();
        }
        EditorGUILayout.EndHorizontal();

        _drawSplitLine();

        EditorGUILayout.BeginHorizontal();
        bool copy_android = mCopyAndroid;
        copy_android = EditorGUILayout.Toggle("Copy&DeleteAndroidAB", copy_android);
        if (mCopyAndroid != copy_android)
        {
            mCopyAndroid = copy_android;
            _checkIfNeedCopyPlatform(mCopyAndroid, MapProjectPlatformInfo["ANDROID"]);
        }

        bool copy_ios = mCopyIOS;
        copy_ios = EditorGUILayout.Toggle("Copy&DeleteIOSAB", copy_ios);
        if (mCopyIOS != copy_ios)
        {
            mCopyIOS = copy_ios;
            _checkIfNeedCopyPlatform(mCopyIOS, MapProjectPlatformInfo["IOS"]);
        }

        bool copy_pc = mCopyPC;
        copy_pc = EditorGUILayout.Toggle("Copy&DeletePCAB", copy_pc);
        if (mCopyPC != copy_pc)
        {
            mCopyPC = copy_pc;
            _checkIfNeedCopyPlatform(mCopyPC, MapProjectPlatformInfo["PC"]);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        bool copy_asset = GUILayout.Button("复制选中平台AB资源到persistentPath");
        if (copy_asset)
        {
            foreach (var i in ListNeedCopyPlatform)
            {
                _copyOrDeleteToPersistentDataPath(i, false);
                _copyOrDeleteToPersistentDataPath(i, true);
            }
        }
        bool delete_asset = GUILayout.Button("删除选中平台persistentPath中的AB资源");
        if (delete_asset)
        {
            foreach (var i in ListNeedCopyPlatform)
            {
                _copyOrDeleteToPersistentDataPath(i, false);
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    //-------------------------------------------------------------------------
    ProjectPlatformInfo _drwaProjectPlatformInfo(ProjectPlatformInfo platform_info)
    {
        ProjectPlatformInfo platform_infoex = new ProjectPlatformInfo();
        platform_infoex.cloneData(platform_info);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("平台:", platform_infoex.PlatformName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("平台程序包版本号:", platform_infoex.BundleVersion);
        bool add_bundleversion = GUILayout.Button("程序包版本号加一");
        if (add_bundleversion)
        {
            _changeBundleData(platform_infoex, true);
        }
        bool minus_bundleversion = GUILayout.Button("程序包版本号减一");
        if (minus_bundleversion)
        {
            _changeBundleData(platform_infoex, false);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("平台资源版本号:", platform_infoex.DataVersion);
        bool add_dataeversion = GUILayout.Button("资源版本号加一");
        if (add_dataeversion)
        {
            _changeDataData(platform_infoex, true);
        }
        bool minus_dataeversion = GUILayout.Button("资源版本号减一");
        if (minus_dataeversion)
        {
            _changeDataData(platform_infoex, false);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("平台目标路径:", platform_infoex.PlatformTargetRootPath + platform_infoex.DataVersion);

        EditorGUILayout.BeginHorizontal();
        bool buid_andorid = platform_infoex.IsBuildPlatform;
        buid_andorid = EditorGUILayout.Toggle("BuidPlatform", buid_andorid);
        if (buid_andorid != platform_infoex.IsBuildPlatform)
        {
            platform_infoex.IsBuildPlatform = buid_andorid;
            _checkIfNeedPackOrCopyRawPlatform(platform_infoex.IsBuildPlatform, platform_infoex);
        }
        EditorGUILayout.EndHorizontal();

        _drawSplitLine();

        return platform_infoex;
    }

    //-------------------------------------------------------------------------
    void _drawSplitLine()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("==================华丽的分割线==================");
        EditorGUILayout.EndHorizontal();
    }

    ////-------------------------------------------------------------------------
    //string _checkDragText(Rect rect)
    //{
    //    string path = "";
    //    if ((Event.current.type == EventType.DragUpdated ||
    //        Event.current.type == EventType.DragExited) &&
    //        rect.Contains(Event.current.mousePosition))
    //    {
    //        path = DragAndDrop.paths[0];
    //        if (!string.IsNullOrEmpty(path))
    //        {
    //            DragAndDrop.AcceptDrag();
    //        }
    //    }

    //    return path;
    //}

    //-------------------------------------------------------------------------
    void _checkIfNeedPackOrCopyRawPlatform(bool is_currentneed, ProjectPlatformInfo build_target)
    {
        if (is_currentneed)
        {
            _setNeedPackOrCopyRawPlatform(build_target);
        }
        else
        {
            _removeNeedPackOrCopyRawPlatform(build_target);
        }
    }

    //-------------------------------------------------------------------------
    void _setNeedPackOrCopyRawPlatform(ProjectPlatformInfo build_target)
    {
        if (!ListNeedBuildPlatform.Contains(build_target))
        {
            ListNeedBuildPlatform.Add(build_target);
        }

        if (!ListNeedCopyRawPlatform.Contains(build_target))
        {
            ListNeedCopyRawPlatform.Add(build_target);
        }
    }

    //-------------------------------------------------------------------------
    void _removeNeedPackOrCopyRawPlatform(ProjectPlatformInfo build_target)
    {
        ListNeedBuildPlatform.Remove(build_target);
        ListNeedCopyRawPlatform.Remove(build_target);
    }

    //-------------------------------------------------------------------------
    void _checkIfNeedCopyPlatform(bool is_currentneed, ProjectPlatformInfo build_target)
    {
        if (is_currentneed)
        {
            _setNeedCopyPlatform(build_target);
        }
        else
        {
            _removeNeedCopyPlatform(build_target);
        }
    }

    //-------------------------------------------------------------------------
    void _setNeedCopyPlatform(ProjectPlatformInfo build_target)
    {
        if (!ListNeedCopyPlatform.Contains(build_target))
        {
            ListNeedCopyPlatform.Add(build_target);
        }
    }

    //-------------------------------------------------------------------------
    void _removeNeedCopyPlatform(ProjectPlatformInfo build_target)
    {
        if (ListNeedCopyPlatform.Contains(build_target))
        {
            ListNeedCopyPlatform.Remove(build_target);
        }
    }

    //-------------------------------------------------------------------------
    void _startCopyRawAndConfig()
    {
        foreach (var i in ListNeedCopyRawPlatform)
        {
            string target_path = i.PlatformTargetRootPath + i.DataVersion;
            if (!Directory.Exists(target_path))
            {
                Directory.CreateDirectory(target_path);
            }

            if (Directory.Exists(mRowAssetPath))
            {
                copyFile(mRowAssetPath, target_path, "Assets/", mDoNotCopyDir);
            }

            //if (Directory.Exists(mConfigPath))
            //{
            //    copyFile(mConfigPath, target_path, "Assets/");
            //}
        }

        Debug.Log("裸资源复制完毕!");
    }

    //-------------------------------------------------------------------------
    void _startBuild()
    {
        foreach (var i in ListNeedBuildPlatform)
        {
            QueueNeedBuildPlatform.Enqueue(i);
        }

        _startCurrentBuild();
    }

    //-------------------------------------------------------------------------
    void _startCurrentBuild()
    {
        if (QueueNeedBuildPlatform.Count > 0)
        {
            mCurrentBuildTargetPlatform = QueueNeedBuildPlatform.Dequeue().BuildTarget;
            _getCurrentTargetPath();
            string target_path = CurrentBuildProjectPlatformInfo.PlatformTargetRootPath + CurrentBuildProjectPlatformInfo.DataVersion;
            if (!Directory.Exists(target_path))
            {
                Directory.CreateDirectory(target_path);
            }

            if (mIsPackSelectItem)
            {
                _packAssetBundleCompressSelect();
            }
            else
            {
                _packAssetBundleCompressAll(target_path);
            }

            _packResources(target_path);
        }
        else
        {
            ShowNotification(new GUIContent("打包完成!"));

            EditorUserBuildSettings.SwitchActiveBuildTarget(mInitBuildTargetGroupPlatform, mInitBuildTargetPlatform);
        }
    }

    //-------------------------------------------------------------------------
    void _packResources(string pack_infopath)
    {
        StreamWriter sw;
        string info = pack_infopath + "/" + PackInfoTextName;

        if (File.Exists(info))
        {
            File.Delete(info);
        }

        sw = File.CreateText(info);

        using (sw)
        {
            _checkPackInfo(sw, pack_infopath);
        }

        _startCurrentBuild();
    }

    //-------------------------------------------------------------------------
    void _checkPackInfo(StreamWriter sw, string path)
    {
        string[] files = Directory.GetFiles(path);
        foreach (var i in files)
        {
            string directory_name = Path.GetDirectoryName(i);
            directory_name = directory_name.Replace(@"\", "/");
            directory_name = directory_name.Substring(directory_name.LastIndexOf("/") + 1);
            string file_name = Path.GetFileName(i);
            if (file_name.Equals(PackInfoTextName))
            {
                continue;
            }

            if (mDoNotCopyFileName.Contains(file_name))
            {
                continue;
            }

            string file_extension = Path.GetExtension(i);
            if (mDoNotPackFileExtention.Contains(file_extension))
            {
                continue;
            }

            string file_directory = Path.GetDirectoryName(i);
            file_directory = file_directory.Replace(@"\", "/");
            string target_path = file_directory.Replace(CurrentBuildProjectPlatformInfo.PlatformTargetRootPath + CurrentBuildProjectPlatformInfo.DataVersion + "/", "");
            string file_path = i;
            sw.WriteLine(_getFilePackInfo(target_path, file_name, file_path));
        }

        string[] directorys = Directory.GetDirectories(path);
        foreach (var i in directorys)
        {
            _checkPackInfo(sw, i);
        }
    }

    //-------------------------------------------------------------------------
    string _getFilePackInfo(string target_path, string file_name, string file_path)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(target_path + "/" + file_name + " ");

        using (FileStream sr = File.OpenRead(file_path))
        {
            byte[] new_bytes = MD5.ComputeHash(sr);
            foreach (var bytes in new_bytes)
            {
                sb.Append(bytes.ToString("X2"));
            }
        }

        return sb.ToString();
    }

    //-------------------------------------------------------------------------
    void _copyOrDeleteToPersistentDataPath(ProjectPlatformInfo build_target, bool is_copy)
    {
        string persistent_data_path = "";
        string resource_path = "";
        if (build_target.BuildTarget == BuildTarget.iOS)
        {
            persistent_data_path = Application.persistentDataPath + "/IOS/";
            resource_path = build_target.PlatformTargetRootPath + build_target.DataVersion;
        }
        else if (build_target.BuildTarget == BuildTarget.Android)
        {
            persistent_data_path = Application.persistentDataPath + "/ANDROID/";
            resource_path = build_target.PlatformTargetRootPath + build_target.DataVersion;
        }
        else if (build_target.BuildTarget == BuildTarget.StandaloneWindows64)
        {
            persistent_data_path = Application.persistentDataPath + "/PC/";
            resource_path = build_target.PlatformTargetRootPath + build_target.DataVersion;
        }

        persistent_data_path = persistent_data_path.Replace(@"\", "/");

        if (is_copy)
        {
            if (!Directory.Exists(persistent_data_path))
            {
                Directory.CreateDirectory(persistent_data_path);
            }

            try
            {
                copyFile(resource_path, persistent_data_path, resource_path, mDoNotCopyDir);
                ShowNotification(new GUIContent("复制AB到本地成功!"));
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
        else
        {
            PlayerPrefs.DeleteAll();
            try
            {
                deleteFile(persistent_data_path);
                ShowNotification(new GUIContent("删除本地AB成功!"));
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    //-------------------------------------------------------------------------
    public static void deleteFile(string directory_path)
    {
        if (Directory.Exists(directory_path))
        {
            string[] files = Directory.GetFiles(directory_path);
            foreach (var i in files)
            {
                File.Delete(i);
            }

            string[] directorys = Directory.GetDirectories(directory_path);

            foreach (var i in directorys)
            {
                deleteFile(i);
            }

            Directory.Delete(directory_path);
        }
    }

    //-------------------------------------------------------------------------
    void _packAssetBundleCompressAll(string target_path)
    {
        _setCurrentBuildTargetGroupPlatform();
        EditorUserBuildSettings.SwitchActiveBuildTarget(mCurrentBuildTargetGroupPlatform, mCurrentBuildTargetPlatform);
        deleteFile(mTargetPlatformRootPath);

        Caching.ClearCache();

        mListAllPkgSingleABFile.Clear();
        _getAllPkgSingleFiles(mAssetBundleResourcesPkgSinglePath);
        mListAllPkgFoldABFold.Clear();
        _getAllPkgFoldFold(mAssetBundleResourcesPkgFoldPath);

        _pakABSingle();
        _pakABFold();

        if (Directory.Exists(mRowAssetPath))
        {
            copyFile(mRowAssetPath, target_path, "Assets/", mDoNotCopyDir);
        }

        //if (Directory.Exists(mConfigPath))
        //{
        //    copyFile(mConfigPath, target_path, "Assets/");
        //}

        Debug.Log("裸资源复制完毕!");
    }

    //-------------------------------------------------------------------------
    void _packAssetBundleCompressSelect()
    {
        _setCurrentBuildTargetGroupPlatform();
        EditorUserBuildSettings.SwitchActiveBuildTarget(mCurrentBuildTargetGroupPlatform, mCurrentBuildTargetPlatform);

        UnityEngine.Object[] select_objs = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);

        List<string> list_single_pkg = new List<string>();
        Dictionary<string, List<string>> map_fold_pkg = new Dictionary<string, List<string>>();

        foreach (var i in select_objs)
        {
            string path_asset = AssetDatabase.GetAssetPath(i);
            bool is_directory = Directory.Exists(path_asset);
            if (is_directory)
            {
                continue;
            }

            //string path_full = Path.GetFullPath(path_asset);
            string extension = Path.GetExtension(path_asset);
            if (mDoNotPackFileExtention.Contains(extension))
            {
                continue;
            }

            string file_name = Path.GetFileName(path_asset);

            if (path_asset.Contains(AssetBundlePkgSingleFoldName))
            {
                list_single_pkg.Add(path_asset);
            }
            else if (path_asset.Contains(AssetBundlePkgFoldFoldName))
            {
                string fold = path_asset;
                fold = fold.Replace(AssetRoot, "");
                fold = fold.Replace(AssetBundlePkgFoldFoldName + "/", "");
                fold = CurrentBuildProjectPlatformInfo.PlatformTargetRootPath + CurrentBuildProjectPlatformInfo.DataVersion + "/" + fold;
                fold = fold.Replace(file_name, "");
                List<string> list_directory = null;
                map_fold_pkg.TryGetValue(fold, out list_directory);
                if (list_directory == null)
                {
                    list_directory = new List<string>();
                    map_fold_pkg[fold] = list_directory;
                }

                list_directory.Add(path_asset);
            }
        }

        string path_assetex = "";
        foreach (var i in list_single_pkg)
        {
            path_assetex = i;
            path_assetex = path_assetex.Replace(AssetRoot, "");
            path_assetex = path_assetex.Replace(AssetBundlePkgSingleFoldName + "/", "");
            path_assetex = CurrentBuildProjectPlatformInfo.PlatformTargetRootPath + CurrentBuildProjectPlatformInfo.DataVersion + "/" + path_assetex;
            string file_name = Path.GetFileName(path_assetex);
            string obj_dir = path_assetex.Replace(file_name, "");
            var names = AssetDatabase.GetDependencies(i);
            string ab_name = Path.GetFileNameWithoutExtension(path_assetex);
            _pakABSingleEx(ab_name, names, obj_dir);
        }

        foreach (var i in map_fold_pkg)
        {
            List<string> list_samefold_file = i.Value;
            _pakABFoldEx(i.Key, list_samefold_file);
        }
    }

    //-------------------------------------------------------------------------
    void _buildAssetBundleCompressed(AssetBundleBuild[] arr_abb, string path, BuildTarget target)
    {
        BuildPipeline.BuildAssetBundles(path, arr_abb, BuildAssetBundleOptions.ForceRebuildAssetBundle, target);
    }

    //-------------------------------------------------------------------------
    void _getAllPkgSingleFiles(string directory_path)
    {
        string[] ab_file = Directory.GetFiles(directory_path);
        foreach (var i in ab_file)
        {
            string extension = Path.GetExtension(i);
            if (mDoNotPackFileExtention.Contains(extension))
            {
                continue;
            }

            mListAllPkgSingleABFile.Add(i);
        }

        string[] directorys = Directory.GetDirectories(directory_path);
        foreach (var i in directorys)
        {
            _getAllPkgSingleFiles(i);
        }
    }

    //-------------------------------------------------------------------------
    void _getAllPkgFoldFold(string directory_path)
    {
        string[] directorys = Directory.GetDirectories(directory_path);
        mListAllPkgFoldABFold.AddRange(directorys);

        foreach (var i in directorys)
        {
            _getAllPkgFoldFold(i);
        }
    }

    //-------------------------------------------------------------------------
    void _pakABSingle()
    {
        foreach (var obj in mListAllPkgSingleABFile)
        {
            if (File.Exists(obj))
            {
                string path = Path.GetFullPath(obj);
                path = path.Replace(@"\", "/");
                path = path.Replace(mAssetPath, "");
                path = path.Replace(AssetBundlePkgSingleFoldName + "/", "");
                path = CurrentBuildProjectPlatformInfo.PlatformTargetRootPath + CurrentBuildProjectPlatformInfo.DataVersion + "/" + path;
                string file_name = Path.GetFileName(obj);
                string obj_dir = path.Replace(file_name, "");
                var names = AssetDatabase.GetDependencies(obj);
                string ab_name = Path.GetFileNameWithoutExtension(obj);
                _pakABSingleEx(ab_name, names, obj_dir);
            }
        }
    }

    //-------------------------------------------------------------------------
    void _pakABSingleEx(string ab_name, string[] dependence_name, string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        AssetBundleBuild abb;
        abb.assetBundleName = ab_name + ".ab";
        abb.assetBundleVariant = "";
        abb.addressableNames = null;
        int asset_index = 0;
        List<string> list_needbuildassetname = new List<string>();
        foreach (var j in dependence_name)
        {
            if (j.EndsWith(".cs") || j.EndsWith(".ttf")) continue;
            if (list_needbuildassetname.Contains(j))
            {
                continue;
            }
            list_needbuildassetname.Add(j);
        }
        abb.assetNames = new string[list_needbuildassetname.Count];

        foreach (var i in list_needbuildassetname)
        {
            abb.assetNames[asset_index++] = i;
        }

        AssetBundleBuild[] arr_abb = new AssetBundleBuild[1];
        arr_abb[0] = abb;

        _buildAssetBundleCompressed(arr_abb, path, mCurrentBuildTargetPlatform);
    }

    //-------------------------------------------------------------------------
    void _pakABFold()
    {
        foreach (var i in mListAllPkgFoldABFold)
        {
            if (Directory.Exists(i))
            {
                string path = Path.GetFullPath(i);
                path = path.Replace(@"\", "/");
                path = path.Replace(mAssetPath, "");
                path = path.Replace(AssetBundlePkgFoldFoldName + "/", "");
                path = CurrentBuildProjectPlatformInfo.PlatformTargetRootPath + CurrentBuildProjectPlatformInfo.DataVersion + "/" + path;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                List<string> list_samefold_file = new List<string>();
                string[] ab_file = Directory.GetFiles(i);
                foreach (var obj in ab_file)
                {
                    string extension = Path.GetExtension(i);
                    if (mDoNotPackFileExtention.Contains(extension))
                    {
                        continue;
                    }

                    list_samefold_file.Add(obj);
                }

                _pakABFoldEx(path, list_samefold_file);
            }
        }
    }

    //-------------------------------------------------------------------------
    void _pakABFoldEx(string path, List<string> list_samefold_file)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        AssetBundleBuild[] arr_abb = new AssetBundleBuild[list_samefold_file.Count];
        int index = 0;
        string fold_name = "";
        foreach (var file in list_samefold_file)
        {
            if (File.Exists(file))
            {
                if (string.IsNullOrEmpty(fold_name))
                {
                    fold_name = Directory.GetParent(file).Name;
                }

                var names = AssetDatabase.GetDependencies(file);

                AssetBundleBuild abb;
                abb.assetBundleName = fold_name + ".ab";
                abb.assetBundleVariant = "";
                abb.addressableNames = null;
                int asset_index = 0;
                List<string> list_needbuildassetname = new List<string>();
                foreach (var j in names)
                {
                    if (j.EndsWith(".cs") || j.EndsWith(".ttf")) continue;
                    if (list_needbuildassetname.Contains(j))
                    {
                        continue;
                    }
                    list_needbuildassetname.Add(j);
                }
                abb.assetNames = new string[list_needbuildassetname.Count];

                foreach (var asset in list_needbuildassetname)
                {
                    abb.assetNames[asset_index++] = asset;
                }

                arr_abb[index] = abb;
                index++;
            }
        }

        _buildAssetBundleCompressed(arr_abb, path, mCurrentBuildTargetPlatform);
    }

    //-------------------------------------------------------------------------
    public static void copyFile(string path, string target_rootpath, string need_replacepath, List<string> list_except_dir = null)
    {
        string[] files = Directory.GetFiles(path);
        foreach (var i in files)
        {
            if (!File.Exists(i))
            {
                continue;
            }

            string file_extension = Path.GetExtension(i);
            if (mDoNotPackFileExtention.Contains(file_extension))
            {
                continue;
            }

            string file_name = Path.GetFileName(i);

            string file_directory = Path.GetDirectoryName(i);
            file_directory = file_directory.Replace(@"\", "/");
            string target_path = file_directory.Replace(need_replacepath, "");
            string file_path = i;
            string target_p = target_rootpath + "/" + target_path;
            if (!Directory.Exists(target_p))
            {
                Directory.CreateDirectory(target_p);
            }
            File.Copy(file_path, target_p + "/" + file_name, true);
        }

        string[] directorys = Directory.GetDirectories(path);
        foreach (var i in directorys)
        {
            bool need_continue = false;
            if (list_except_dir != null)
            {
                foreach (var j in list_except_dir)
                {
                    if (i.Contains(j))
                    {
                        need_continue = true;
                        break;
                    }
                }

                if (need_continue) continue;
            }
            copyFile(i, target_rootpath, need_replacepath, list_except_dir);
        }
    }

    //-------------------------------------------------------------------------
    void _translatePatchXml(string path)
    {
        XmlDocument abpath_xml = new XmlDocument();
        abpath_xml.Load(path);

        XmlElement root = null;
        root = abpath_xml.DocumentElement;

        foreach (XmlNode i in root.ChildNodes)
        {
            if (i is XmlComment)
            {
                continue;
            }

            var xml_element = (XmlElement)i;
            ProjectPlatformInfo platform_info = new ProjectPlatformInfo();
            platform_info.PlatformKey = (_ePlatform)(int.Parse(xml_element.GetAttribute("PlatformKey")));
            platform_info.PlatformName = xml_element.GetAttribute("PlatformName");
            platform_info.BundleVersion = xml_element.GetAttribute("BundleVersion");
            platform_info.DataVersion = xml_element.GetAttribute("DataVersion");
            platform_info.PlatformTargetRootPath = CurrentProjectABTargetPath + "/" +
                platform_info.PlatformName + "/" + platform_info.BundleVersion + "/" + "DataVersion_";
            platform_info.BuildTarget = (BuildTarget)(int.Parse(xml_element.GetAttribute("BuildTarget")));
            platform_info.IsBuildPlatform = false;
            MapProjectPlatformInfo[platform_info.PlatformName] = platform_info;
        }
    }

    //-------------------------------------------------------------------------
    public static void changeBundleData(_ePlatform platform, string target_path, string new_bundle, bool change_allplatform = false)
    {
        string ab_pathinfo = target_path + "/" + PatchInfoName;
        var infos = File.ReadAllLines(ab_pathinfo);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < infos.Length; i++)
        {
            var line = infos[i];
            if (change_allplatform)
            {
                string bundle = "BundleVersion=\"";
                if (line.Contains(bundle))
                {
                    string replace_oldvalue = line.Substring(line.IndexOf(bundle), 24);
                    string replace_newvalue = "BundleVersion=\"" + new_bundle + "\"";
                    line = line.Replace(replace_oldvalue, replace_newvalue);
                }
            }
            else
            {
                if (line.Contains(platform.ToString()))
                {
                    string replace_oldvalue = line.Substring(line.IndexOf("BundleVersion=\""), 24);
                    string replace_newvalue = "BundleVersion=\"" + new_bundle + "\"";
                    line = line.Replace(replace_oldvalue, replace_newvalue);
                }
            }

            sb.AppendLine(line);
        }

        using (StreamWriter sw = new StreamWriter(ab_pathinfo))
        {
            sw.Write(sb.ToString());
        }
    }

    //-------------------------------------------------------------------------
    public static void changeDataData(_ePlatform platform, string target_path, string new_data, bool change_allplatform = false)
    {
        string ab_pathinfo = target_path + "/" + PatchInfoName;
        var infos = File.ReadAllLines(ab_pathinfo);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < infos.Length; i++)
        {
            var line = infos[i];
            if (change_allplatform)
            {
                string data = "DataVersion=\"";
                if (line.Contains(data))
                {
                    string replace_oldvalue = line.Substring(line.IndexOf(data), 22);
                    string replace_newvalue = "DataVersion=\"" + new_data + "\"";
                    line = line.Replace(replace_oldvalue, replace_newvalue);
                }
            }
            else
            {
                if (line.Contains(platform.ToString()))
                {
                    string replace_oldvalue = line.Substring(line.IndexOf("DataVersion=\""), 22);
                    string replace_newvalue = "DataVersion=\"" + new_data + "\"";
                    line = line.Replace(replace_oldvalue, replace_newvalue);
                }
            }

            sb.AppendLine(line);
        }

        using (StreamWriter sw = new StreamWriter(ab_pathinfo))
        {
            sw.Write(sb.ToString());
        }
    }

    //-------------------------------------------------------------------------
    void _changeBundleData(ProjectPlatformInfo platform_info, bool add)
    {
        int bundle = int.Parse(platform_info.BundleVersion.Replace(".", ""));
        if (add)
        {
            bundle += 1;
        }
        else
        {
            bundle -= 1;
        }
        string bundle_version = bundle.ToString();
        bundle_version = bundle_version.Insert(1, ".").Insert(4, ".");
        platform_info.BundleVersion = bundle_version;

        changeBundleData(platform_info.PlatformKey, CurrentProjectABTargetPath, bundle_version);

        if (CurrentPlatformProjectPlatformInfo.PlatformName.Equals(platform_info.PlatformName))
        {
            //PlayerSettings.bundleVersion = bundle_version;
        }
    }

    //-------------------------------------------------------------------------
    void _changeDataData(ProjectPlatformInfo platform_info, bool add)
    {
        int data = int.Parse(platform_info.DataVersion.Replace(".", ""));
        if (add)
        {
            data += 1;
        }
        else
        {
            data -= 1;
        }
        string data_version = data.ToString();
        data_version = data_version.Insert(1, ".").Insert(4, ".");
        platform_info.DataVersion = data_version;
        if (PlayerPrefs.HasKey(AutoPatcherStringDef.PlayerPrefsKeyLocalVersionInfo))
        {
            string version_info = PlayerPrefs.GetString(AutoPatcherStringDef.PlayerPrefsKeyLocalVersionInfo);
            if (!string.IsNullOrEmpty(version_info))
            {
                var version = EbTool.jsonDeserialize<LocalVersionInfo>(version_info);
                version.data_version = data_version;
                PlayerPrefs.SetString(AutoPatcherStringDef.PlayerPrefsKeyLocalVersionInfo, EbTool.jsonSerialize(version));
            }
        }

        changeDataData(platform_info.PlatformKey, CurrentProjectABTargetPath, data_version);
    }

    //-------------------------------------------------------------------------
    void _setCurrentBuildTargetGroupPlatform()
    {
        if (mCurrentBuildTargetPlatform == BuildTarget.iOS)
        {
            mCurrentBuildTargetGroupPlatform = BuildTargetGroup.iOS;
        }
        else if (mCurrentBuildTargetPlatform == BuildTarget.Android)
        {
            mCurrentBuildTargetGroupPlatform = BuildTargetGroup.Android;
        }
        else if (mCurrentBuildTargetPlatform == BuildTarget.StandaloneWindows64)
        {
            mCurrentBuildTargetGroupPlatform = BuildTargetGroup.Standalone;
        }
    }
}

//-------------------------------------------------------------------------
public class ProjectPlatformInfo
{
    public _ePlatform PlatformKey;
    public string PlatformName;
    public string BundleVersion;
    public string DataVersion;
    public string PlatformTargetRootPath;
    public BuildTarget BuildTarget;
    public bool IsBuildPlatform;

    //-------------------------------------------------------------------------
    public void cloneData(ProjectPlatformInfo project_info)
    {
        this.PlatformKey = project_info.PlatformKey;
        this.PlatformName = project_info.PlatformName;
        this.BundleVersion = project_info.BundleVersion;
        this.DataVersion = project_info.DataVersion;
        this.PlatformTargetRootPath = project_info.PlatformTargetRootPath;
        this.BuildTarget = project_info.BuildTarget;
        this.IsBuildPlatform = project_info.IsBuildPlatform;
    }
}

//-----------------------------------------------------------------------------
[Serializable]
public struct LocalVersionInfo
{
    public string data_version;
    public string remote_version_url;
}

//-----------------------------------------------------------------------------
public class AutoPatcherStringDef
{
    public static string PlayerPrefsKeyLocalVersionInfo
    {
        get
        {
            string key = "LocalVersionInfo";
#if UNITY_ANDROID
            key += "_Android";
#elif UNITY_IPHONE
            key+="_iOS";
#else
            key += "_Pc";
#endif
            return key;
        }
    }
}

//-------------------------------------------------------------------------
public enum _ePlatform
{
    None = 0,
    ANDROID,
    IOS,
    PC,
}