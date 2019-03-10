using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;

public class EditorViewDataPublish : EditorWindow
{
    //-------------------------------------------------------------------------
    HashSet<string> DoNotPackFileExtention = new HashSet<string> { ".meta", ".DS_Store" };
    string AssetBundleSingleFoldName = "BuildAssetBundleFromSingle";
    string AssetBundlePkgFoldFoldName = "BuildAssetBundleFromFold";
    string AssetBundleLaunchFoldName = "Resources.KingTexasLaunch";
    MD5 MD5 = new MD5CryptoServiceProvider();
    StringBuilder Sb = new StringBuilder();

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        if (EditorContext.Instance == null)
        {
            new EditorContext();
        }

        var settings = EditorContext.Instance.Config.CfgProjectSettings;

        GUILayout.Space(10);
        GUILayout.Label("Casinos资源发布");

        GUILayout.Space(10);
        GUILayout.Label("---------------------------------");
        EditorGUILayout.LabelField("运行时使用的资源目录");

        bool is_editor_debug = EditorGUILayout.Toggle("是否处于编辑器调试模式",
            EditorContext.Instance.Config.CfgUserSettings.IsEditorDebug, GUILayout.Width(600));
        if (is_editor_debug != EditorContext.Instance.Config.CfgUserSettings.IsEditorDebug)
        {
            EditorContext.Instance.Config.SaveValueIsEditorDebug(is_editor_debug);
        }

        GUILayout.Space(10);
        GUILayout.Label("------------------------------------------------------");
        EditorGUILayout.LabelField("BundleVersion:", Application.version);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("清除所有VersionPersistent", GUILayout.Width(200)))
        {
            PlayerPrefs.DeleteKey("VersionLaunchPersistent");
            PlayerPrefs.DeleteKey("VersionCommonPersistent");
            PlayerPrefs.DeleteKey("VersionDataPersistent");
            ShowNotification(new GUIContent("删除所有VersionPersistent成功!"));
        }
        if (GUILayout.Button("清除VersionLaunchPersistent", GUILayout.Width(200)))
        {
            PlayerPrefs.DeleteKey("VersionLaunchPersistent");
            ShowNotification(new GUIContent("删除VersionLaunchPersistent成功!"));
        }
        if (GUILayout.Button("清除VersionCommonPersistent", GUILayout.Width(200)))
        {
            PlayerPrefs.DeleteKey("VersionCommonPersistent");
            ShowNotification(new GUIContent("删除VersionCommonPersistent成功!"));
        }
        if (GUILayout.Button("清除VersionDataPersistent", GUILayout.Width(200)))
        {
            PlayerPrefs.DeleteKey("VersionDataPersistent");
            ShowNotification(new GUIContent("删除VersionDataPersistent成功!"));
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("清空Persistent目录", GUILayout.Width(200)))
        {
            string dir_launch = Application.persistentDataPath + "/Launch";
            string dir_common = Application.persistentDataPath + "/Common";
            string dir_android = Application.persistentDataPath + "/Android";
            string dir_ios = Application.persistentDataPath + "/iOS";
            if (Directory.Exists(dir_launch))
            {
                Directory.Delete(dir_launch, true);
            }
            if (Directory.Exists(dir_common))
            {
                Directory.Delete(dir_common, true);
            }
            if (Directory.Exists(dir_android))
            {
                Directory.Delete(dir_android, true);
            }
            if (Directory.Exists(dir_ios))
            {
                Directory.Delete(dir_ios, true);
            }
            ShowNotification(new GUIContent("清空Persistent目录成功!"));
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("清空Persistent目录并清除所有VersionPersistent", GUILayout.Width(400)))
        {
            PlayerPrefs.DeleteKey("VersionLaunchPersistent");
            PlayerPrefs.DeleteKey("VersionCommonPersistent");
            PlayerPrefs.DeleteKey("VersionDataPersistent");
            string dir_launch = Application.persistentDataPath + "/Launch";
            string dir_common = Application.persistentDataPath + "/Common";
            string dir_android = Application.persistentDataPath + "/Android";
            string dir_ios = Application.persistentDataPath + "/iOS";
            if (Directory.Exists(dir_launch))
            {
                Directory.Delete(dir_launch, true);
            }
            if (Directory.Exists(dir_common))
            {
                Directory.Delete(dir_common, true);
            }
            if (Directory.Exists(dir_android))
            {
                Directory.Delete(dir_android, true);
            }
            if (Directory.Exists(dir_ios))
            {
                Directory.Delete(dir_ios, true);
            }
            ShowNotification(new GUIContent("清空Persistent目录并清除所有VersionPersistent成功!"));
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.Label("------------------------------------------------------");
        EditorGUILayout.LabelField("Launch资源处理");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("生成LaunchFileList.txt", GUILayout.Width(200)))
        {
            AssetDatabase.Refresh();
            var ignore_files = new HashSet<string> { "LaunchFileList.txt" };
            _genLaunchFileList(ignore_files);
            AssetDatabase.Refresh();
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.Label("------------------------------------------------------");
        EditorGUILayout.LabelField("Common资源处理");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("打包Lua脚本（Android）", GUILayout.Width(200)))
        {
            AssetDatabase.Refresh();
            _buildAssetBundleLua(Casinos._eEditorRunSourcePlatform.Android);
            var ignore_files = new HashSet<string> { "CommonFileList.txt", "Bundle.txt", "Context.txt" };
            _genCommonFileList(ignore_files);
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("打包Lua脚本（iOS）", GUILayout.Width(200)))
        {
            AssetDatabase.Refresh();
            _buildAssetBundleLua(Casinos._eEditorRunSourcePlatform.IOS);
            var ignore_files = new HashSet<string> { "CommonFileList.txt", "Bundle.txt", "Context.txt" };
            _genCommonFileList(ignore_files);
            AssetDatabase.Refresh();
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.Label("------------------------------------------------------");
        GUILayout.Label("ANDROID");
        EditorGUILayout.LabelField("DataVersion:", settings.CurrentDataVersionANDROID.ToString());
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("++", GUILayout.Width(200)))
        {
            _changeDataVersion(Casinos._eEditorRunSourcePlatform.Android, true);
        }
        if (GUILayout.Button("--", GUILayout.Width(200)))
        {
            _changeDataVersion(Casinos._eEditorRunSourcePlatform.Android, false);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("生成AssetBundle（选中）", GUILayout.Width(200)))
        {
            _buildAssetBundle(Casinos._eEditorRunSourcePlatform.Android, true);
            _genDataFileList(Casinos._eEditorRunSourcePlatform.Android);
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("生成AssetBundle（全部）", GUILayout.Width(200)))
        {
            _buildAssetBundle(Casinos._eEditorRunSourcePlatform.Android, false);
            _genDataFileList(Casinos._eEditorRunSourcePlatform.Android);
            AssetDatabase.Refresh();
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.Label("------------------------------------------------------");
        GUILayout.Label("IOS");
        EditorGUILayout.LabelField("DataVersion:", settings.CurrentDataVersionIOS.ToString());
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("++", GUILayout.Width(200)))
        {
            _changeDataVersion(Casinos._eEditorRunSourcePlatform.IOS, true);
        }
        if (GUILayout.Button("--", GUILayout.Width(200)))
        {
            _changeDataVersion(Casinos._eEditorRunSourcePlatform.IOS, false);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("生成AssetBundle（选中）", GUILayout.Width(200)))
        {
            _buildAssetBundle(Casinos._eEditorRunSourcePlatform.IOS, true);
            _genDataFileList(Casinos._eEditorRunSourcePlatform.IOS);
            AssetDatabase.Refresh();
        }
        if (GUILayout.Button("生成AssetBundle（全部）", GUILayout.Width(200)))
        {
            _buildAssetBundle(Casinos._eEditorRunSourcePlatform.IOS, false);
            _genDataFileList(Casinos._eEditorRunSourcePlatform.IOS);
            AssetDatabase.Refresh();
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("关闭", GUILayout.Width(200)))
        {
            Close();
        }
        EditorGUILayout.EndHorizontal();
    }

    //-------------------------------------------------------------------------
    void _changeDataVersion(Casinos._eEditorRunSourcePlatform platform, bool add)
    {
        var settings = EditorContext.Instance.Config.CfgProjectSettings;

        string current_data_version = settings.InitDataVersion;
        switch (platform)
        {
            case Casinos._eEditorRunSourcePlatform.Android:
                current_data_version = settings.CurrentDataVersionANDROID;
                break;
            case Casinos._eEditorRunSourcePlatform.IOS:
                current_data_version = settings.CurrentDataVersionIOS;
                break;
            case Casinos._eEditorRunSourcePlatform.PC:
                current_data_version = settings.CurrentDataVersionANDROID;
                break;
        }

        int data = int.Parse(current_data_version.Replace(".", ""));
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
        current_data_version = data_version;

        switch (platform)
        {
            case Casinos._eEditorRunSourcePlatform.Android:
                settings.CurrentDataVersionANDROID = current_data_version;
                break;
            case Casinos._eEditorRunSourcePlatform.IOS:
                settings.CurrentDataVersionIOS = current_data_version;
                break;
            case Casinos._eEditorRunSourcePlatform.PC:
                settings.CurrentDataVersionANDROID = current_data_version;
                break;
        }

        EditorContext.Instance.Config.SaveProjectSettings();
    }

    //-------------------------------------------------------------------------
    void _buildAssetBundleLua(Casinos._eEditorRunSourcePlatform platform)
    {
        BuildTarget build_target = BuildTarget.Android;
        BuildTargetGroup build_target_group = BuildTargetGroup.Android;

        switch (platform)
        {
            case Casinos._eEditorRunSourcePlatform.Android:
                build_target = BuildTarget.Android;
                build_target_group = BuildTargetGroup.Android;
                break;
            case Casinos._eEditorRunSourcePlatform.IOS:
                build_target = BuildTarget.iOS;
                build_target_group = BuildTargetGroup.iOS;
                break;
            case Casinos._eEditorRunSourcePlatform.PC:
                build_target = BuildTarget.StandaloneWindows64;
                build_target_group = BuildTargetGroup.Standalone;
                break;
        }
        EditorUserBuildSettings.SwitchActiveBuildTarget(build_target_group, build_target);

        string dir_assets = EditorContext.Instance.PathAssets;

        List<string> list_file = new List<string>();
        var ignore_files = new HashSet<string> { "CommonFileList.txt", "Bundle.txt", "Context.txt" };
        _getDataFileList2(list_file, ignore_files, dir_assets + "Script.Lua\\");

        string ab_name1 = "lua_android.ab";
        bool b = _pakAbLuaEx(list_file, ab_name1, platform, build_target);
        if (!b)
        {
            Debug.LogError("BuildAssetBundle失败！");
            return;
        }

        list_file.Clear();
        _getDataFileList2(list_file, ignore_files, dir_assets + "Script.Lua\\Launch\\");
        ab_name1 = "lua_launch_android.ab";
        b = _pakAbLuaEx(list_file, ab_name1, platform, build_target);
        if (!b)
        {
            Debug.LogError("BuildAssetBundle失败！");
            return;
        }

        ShowNotification(new GUIContent("BuildAssetBundle Finished!"));
    }

    //-------------------------------------------------------------------------
    void _buildAssetBundle(Casinos._eEditorRunSourcePlatform platform, bool selected_or_all)
    {
        BuildTarget build_target = BuildTarget.Android;
        BuildTargetGroup build_target_group = BuildTargetGroup.Android;

        switch (platform)
        {
            case Casinos._eEditorRunSourcePlatform.Android:
                build_target = BuildTarget.Android;
                build_target_group = BuildTargetGroup.Android;
                break;
            case Casinos._eEditorRunSourcePlatform.IOS:
                build_target = BuildTarget.iOS;
                build_target_group = BuildTargetGroup.iOS;
                break;
            case Casinos._eEditorRunSourcePlatform.PC:
                build_target = BuildTarget.StandaloneWindows64;
                build_target_group = BuildTargetGroup.Standalone;
                break;
        }
        EditorUserBuildSettings.SwitchActiveBuildTarget(build_target_group, build_target);

        UnityEngine.Object[] select_objs = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);

        Dictionary<string, List<string>> map_single_pkg = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> map_fold_pkg = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> map_fold_launch = new Dictionary<string, List<string>>();

        foreach (var i in select_objs)
        {
            string path_asset = AssetDatabase.GetAssetPath(i);

            string extension = Path.GetExtension(path_asset);
            if (DoNotPackFileExtention.Contains(extension) || string.IsNullOrEmpty(extension)) continue;

            string file_name = Path.GetFileName(path_asset);

            if (path_asset.Contains(AssetBundleSingleFoldName))
            {
                string key = Directory.GetParent(path_asset).Name;

                List<string> list_directory = null;
                map_single_pkg.TryGetValue(key, out list_directory);
                if (list_directory == null)
                {
                    list_directory = new List<string>();
                    map_single_pkg[key] = list_directory;
                }

                list_directory.Add(path_asset);
            }
            else if (path_asset.Contains(AssetBundlePkgFoldFoldName))
            {
                string fold = path_asset;
                fold = fold.Replace("Assets/", "");
                fold = fold.Replace(AssetBundlePkgFoldFoldName + "/", "");
                fold = EditorContext.Instance.PathDataOss + platform.ToString() + "/" + fold;
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
            else if (path_asset.Contains(AssetBundleLaunchFoldName))
            {
                string fold = path_asset;
                fold = fold.Replace("Assets/", "");
                fold = EditorContext.Instance.PathDataOss + platform.ToString() + "/" + fold;
                fold = fold.Replace(file_name, "");

                List<string> list_directory = null;
                map_fold_launch.TryGetValue(fold, out list_directory);
                if (list_directory == null)
                {
                    list_directory = new List<string>();
                    map_fold_launch[fold] = list_directory;
                }

                list_directory.Add(path_asset);
            }
        }

        bool b = _pakABSingleEx(map_single_pkg, platform, build_target);
        if (!b)
        {
            Debug.LogError("BuildAssetBundle失败！");
            return;
        }

        foreach (var i in map_fold_pkg)
        {
            List<string> list_samefold_file = i.Value;
            AssetBundleManifest ab_manifest = _pakABFoldEx(i.Key, list_samefold_file, build_target);

            if (ab_manifest == null)
            {
                Debug.LogError("BuildAssetBundle失败！");
                return;
            }
        }

        foreach (var i in map_fold_launch)
        {
            List<string> list_samefold_file = i.Value;
            AssetBundleManifest ab_manifest = _pakABFoldEx(i.Key, list_samefold_file, build_target);

            if (ab_manifest == null)
            {
                Debug.LogError("BuildAssetBundle失败！");
                return;
            }
        }

        ShowNotification(new GUIContent("BuildAssetBundle Finished!"));
    }

    //-------------------------------------------------------------------------
    bool _pakAbLuaEx(List<string> list_file, string ab_name, Casinos._eEditorRunSourcePlatform platform, BuildTarget build_target)
    {
        AssetBundleBuild abb = new AssetBundleBuild();
        abb.assetBundleName = ab_name;
        abb.assetBundleVariant = "";
        abb.addressableNames = null;

        int asset_index = 0;
        abb.assetNames = new string[list_file.Count];
        foreach (var j in list_file)
        {
            string j1 = j.Replace('\\', '/');
            int n = j1.IndexOf("Assets");
            string j2 = j1.Substring(n);// AssetBundle中的资源路径必须起始于Assets
            abb.assetNames[asset_index++] = j2;
        }

        AssetBundleBuild[] arr_abb = new AssetBundleBuild[1];
        arr_abb[0] = abb;

        string dir_dataoss = EditorContext.Instance.PathDataOss;
        AssetBundleManifest ab_manifest = BuildPipeline.BuildAssetBundles(dir_dataoss + "Common\\Lua",
            arr_abb,
            BuildAssetBundleOptions.ForceRebuildAssetBundle, build_target);
        if (ab_manifest == null)
        {
            return false;
        }

        return true;
    }

    //-------------------------------------------------------------------------
    bool _pakABSingleEx(Dictionary<string, List<string>> map_single_pkg,
        Casinos._eEditorRunSourcePlatform platform, BuildTarget build_target)
    {
        string path_assetex = "";
        foreach (var i in map_single_pkg)
        {
            path_assetex = i.Value[0];
            path_assetex = path_assetex.Replace("Assets/", "");
            path_assetex = path_assetex.Replace(AssetBundleSingleFoldName + "/", "");
            path_assetex = EditorContext.Instance.PathDataOss + platform.ToString() + "/" + path_assetex;
            string file_name = Path.GetFileName(path_assetex);
            string file_extension = Path.GetExtension(path_assetex);
            string obj_dir = path_assetex.Replace(file_name, "");

            if (!Directory.Exists(obj_dir))
            {
                Directory.CreateDirectory(obj_dir);
            }

            AssetBundleBuild[] arr_abb = new AssetBundleBuild[i.Value.Count];
            for (int n = 0; n < i.Value.Count; n++)
            {
                string f = i.Value[n];
                string ab_name = Path.GetFileNameWithoutExtension(f);

                AssetBundleBuild abb;
                abb.assetBundleName = ab_name + ".ab";
                abb.assetBundleVariant = "";
                abb.addressableNames = null;

                var dependences = AssetDatabase.GetDependencies(f);
                List<string> list_needbuildassetname = new List<string>(dependences.Length);
                foreach (var j in dependences)
                {
                    if (j.EndsWith(".cs") || j.EndsWith(".ttf")) continue;
                    list_needbuildassetname.Add(j);
                }

                int asset_index = 0;
                abb.assetNames = new string[list_needbuildassetname.Count];
                foreach (var j in list_needbuildassetname)
                {
                    abb.assetNames[asset_index++] = j;
                }

                arr_abb[n] = abb;
            }

            if (file_extension == ".prefab")
            {
                foreach (var k in arr_abb)
                {
                    AssetBundleBuild[] arr_abb1 = new AssetBundleBuild[1] { k };
                    AssetBundleManifest ab_manifest = BuildPipeline.BuildAssetBundles(obj_dir, arr_abb1,
                        BuildAssetBundleOptions.ForceRebuildAssetBundle, build_target);
                    if (ab_manifest == null)
                    {
                        Debug.LogError("BuildAssetBundle失败！");
                        return false;
                    }
                }
            }
            else
            {
                AssetBundleManifest ab_manifest = BuildPipeline.BuildAssetBundles(obj_dir, arr_abb,
                    BuildAssetBundleOptions.ForceRebuildAssetBundle, build_target);
                if (ab_manifest == null)
                {
                    Debug.LogError("BuildAssetBundle失败！");
                    return false;
                }
            }
        }

        return true;
    }

    //-------------------------------------------------------------------------
    AssetBundleManifest _pakABFoldEx(string path, List<string> list_samefold_file, BuildTarget build_target)
    {
        AssetBundleBuild[] arr_abb = new AssetBundleBuild[1];

        string fold_name = Directory.GetParent(path).Name;
        AssetBundleBuild abb;
        abb.assetBundleName = fold_name + ".ab";
        abb.assetNames = null;
        abb.assetBundleVariant = "";
        abb.addressableNames = null;
        int asset_index = 0;
        List<string> list_needbuildassetname = new List<string>();

        foreach (var file in list_samefold_file)
        {
            var names = AssetDatabase.GetDependencies(file);
            foreach (var j in names)
            {
                if (j.EndsWith(".cs") || j.EndsWith(".ttf")) continue;
                if (!list_needbuildassetname.Contains(j)) list_needbuildassetname.Add(j);
            }
        }

        abb.assetNames = new string[list_needbuildassetname.Count];
        foreach (var asset in list_needbuildassetname)
        {
            abb.assetNames[asset_index++] = asset;
        }

        arr_abb[0] = abb;

        var obj_dir = Directory.GetParent(path).Parent.FullName;
        if (!Directory.Exists(obj_dir))
        {
            Directory.CreateDirectory(obj_dir);
        }
        AssetBundleManifest ab_manifest = BuildPipeline.BuildAssetBundles(obj_dir, arr_abb,
            BuildAssetBundleOptions.ForceRebuildAssetBundle, build_target);

        return ab_manifest;
    }

    //-------------------------------------------------------------------------
    void _copyDir(Casinos._eEditorRunSourcePlatform platform, string path_src, string path_dst)
    {
        var path_dst1 = path_dst.Replace("\\", "/");
        var path_src1 = path_src.Replace("\\", "/");

        DirectoryInfo dir = new DirectoryInfo(path_src1);
        FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();// 获取目录下（不包含子目录）的文件和子目录
        foreach (FileSystemInfo i in fileinfo)
        {
            if (i is DirectoryInfo)// 判断是否文件夹
            {
                if (i.Name == ".idea" || i.Name == ".ideadictionaries") continue;

                if (!Directory.Exists(path_dst1 + i.Name))
                {
                    Directory.CreateDirectory(path_dst1 + i.Name);
                }
                _copyDir(platform, i.FullName, path_dst1 + i.Name);// 递归调用复制子文件夹
            }
            else
            {
                string file_extension = Path.GetExtension(i.FullName);
                if (DoNotPackFileExtention.Contains(file_extension)
                    || i.Name == "Context.txt"
                    || i.Name == "Bundle.txt") continue;

                File.Copy(i.FullName, path_dst1 + "/" + i.Name, true);// 不是文件夹即复制文件，true表示可以覆盖同名文件
            }
        }
    }

    //-------------------------------------------------------------------------
    void _genLaunchFileList(HashSet<string> ignore_files)
    {
        string path_dst = EditorContext.Instance.PathDataOss + "Launch/";
        path_dst = path_dst.Replace(@"\", "/");

        string file_launchfilelist = path_dst + EditorStringDef.FileLaunchFileList;
        if (File.Exists(file_launchfilelist))
        {
            File.Delete(file_launchfilelist);
        }

        using (StreamWriter sw = File.CreateText(file_launchfilelist))
        {
            _genDataFileList2(sw, ignore_files, path_dst, path_dst);
        }

        ShowNotification(new GUIContent("GenLaunchFileList Finished!"));
    }

    //-------------------------------------------------------------------------
    void _genCommonFileList(HashSet<string> ignore_files)
    {
        string path_dst = EditorContext.Instance.PathDataOss + "Common/";
        path_dst = path_dst.Replace(@"\", "/");

        string file_commonfilelist = path_dst + EditorStringDef.FileCommonFileList;
        if (File.Exists(file_commonfilelist))
        {
            File.Delete(file_commonfilelist);
        }

        using (StreamWriter sw = File.CreateText(file_commonfilelist))
        {
            _genDataFileList2(sw, ignore_files, path_dst, path_dst);
        }

        ShowNotification(new GUIContent("GenCommonFileList Finished!"));
    }

    //-------------------------------------------------------------------------
    void _genDataFileList(Casinos._eEditorRunSourcePlatform platform)
    {
        string path_dst = string.Empty;
        switch (platform)
        {
            case Casinos._eEditorRunSourcePlatform.Android:
                path_dst = EditorContext.Instance.PathDataOss + "ANDROID\\";
                break;
            case Casinos._eEditorRunSourcePlatform.IOS:
                path_dst = EditorContext.Instance.PathDataOss + "IOS\\";
                break;
            case Casinos._eEditorRunSourcePlatform.PC:
                path_dst = EditorContext.Instance.PathDataOss + "PC\\";
                break;
        }
        path_dst = path_dst.Replace(@"\", "/");

        string file_datafilelist = path_dst + EditorStringDef.FileDataFileList;
        if (File.Exists(file_datafilelist))
        {
            File.Delete(file_datafilelist);
        }

        using (StreamWriter sw = File.CreateText(file_datafilelist))
        {
            var ignore_files = new HashSet<string> { "DataFileList.txt", "LaunchInfo.json" };
            _genDataFileList2(sw, ignore_files, path_dst, path_dst);
        }

        ShowNotification(new GUIContent("GenDataFileList Finished!"));
    }

    //-------------------------------------------------------------------------
    void _getDataFileList2(List<string> list_file, HashSet<string> ignore_files, string cur_path)
    {
        string[] files = Directory.GetFiles(cur_path);
        foreach (var i in files)
        {
            //string directory_name = Path.GetDirectoryName(i);
            //directory_name = directory_name.Replace(@"\", "/");
            //directory_name = directory_name.Substring(directory_name.LastIndexOf("/") + 1);

            string file_name = Path.GetFileName(i);
            if (ignore_files.Contains(file_name)) continue;

            string file_extension = Path.GetExtension(i);
            if (DoNotPackFileExtention.Contains(file_extension)) continue;

            //string file_directory = Path.GetDirectoryName(i);
            //file_directory = file_directory.Replace(@"\", "/");
            //string target_path = file_directory.Replace(path_dst, "");
            //string file_path = i;

            list_file.Add(i);
        }

        string[] directorys = Directory.GetDirectories(cur_path);
        foreach (var i in directorys)
        {
            if (i.Contains(".idea")) continue;

            _getDataFileList2(list_file, ignore_files, i);
        }
    }

    //-------------------------------------------------------------------------
    void _genDataFileList2(StreamWriter sw, HashSet<string> ignore_files, string cur_path, string path_dst)
    {
        string[] files = Directory.GetFiles(cur_path);
        foreach (var i in files)
        {
            string directory_name = Path.GetDirectoryName(i);
            directory_name = directory_name.Replace(@"\", "/");
            directory_name = directory_name.Substring(directory_name.LastIndexOf("/") + 1);
            string file_name = Path.GetFileName(i);

            if (ignore_files.Contains(file_name)) continue;

            string file_extension = Path.GetExtension(i);
            if (DoNotPackFileExtention.Contains(file_extension)) continue;

            string file_directory = Path.GetDirectoryName(i);
            file_directory = file_directory.Replace(@"\", "/");
            string target_path = file_directory.Replace(path_dst, "");
            string file_path = i;

            string file_md5 = _calcOneFileMD5(target_path, file_name, file_path);
            sw.WriteLine(file_md5);
        }

        string[] directorys = Directory.GetDirectories(cur_path);
        foreach (var i in directorys)
        {
            if (i.Contains(".idea")) continue;

            _genDataFileList2(sw, ignore_files, i, path_dst);
        }
    }

    //-------------------------------------------------------------------------
    // 计算并返回单个文件的MD5值
    string _calcOneFileMD5(string target_path, string file_name, string file_path)
    {
        Sb.Length = 0;
        Sb.Append(target_path + "/" + file_name + " ");

        using (FileStream sr = File.OpenRead(file_path))
        {
            byte[] new_bytes = MD5.ComputeHash(sr);
            foreach (var bytes in new_bytes)
            {
                Sb.Append(bytes.ToString("X2"));
            }
        }

        return Sb.ToString();
    }
}