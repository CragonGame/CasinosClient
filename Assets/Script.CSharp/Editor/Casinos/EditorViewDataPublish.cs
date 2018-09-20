using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;

public class EditorViewDataPublish : EditorWindow
{
    //-------------------------------------------------------------------------
    HashSet<string> DoNotPackFileExtention { get; set; } = new HashSet<string> { ".meta", ".DS_Store" };
    HashSet<string> DoNotCopyFileName { get; set; } = new HashSet<string> { "DataFileList.txt" };
    HashSet<string> DoNotCopyDir { get; set; } = new HashSet<string> { ".idea" };
    string AssetBundlePkgSingleFoldName { get; set; } = "BuildAssetBundleFromSingle";
    string AssetBundlePkgFoldFoldName { get; set; } = "BuildAssetBundleFromFold";
    MD5 MD5 { get; set; } = new MD5CryptoServiceProvider();
    StringBuilder Sb { get; set; } = new StringBuilder();

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        var settings = EditorContext.Instance.Config.CfgProjectSettings;

        GUILayout.Space(10);
        GUILayout.Label("Casinos资源发布");

        GUILayout.Space(20);
        GUILayout.Label("------------------------------------------------------");
        EditorGUILayout.LabelField("AB资源所在路径:", EditorContext.Instance.PathAssets);
        EditorGUILayout.LabelField("BundleVersion:", Application.version);

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
        }
        if (GUILayout.Button("生成AssetBundle（全部）", GUILayout.Width(200)))
        {
            _buildAssetBundle(Casinos._eEditorRunSourcePlatform.Android, false);
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("ClearRawDataAndLua", GUILayout.Width(200)))
        {
            _clearRawDataAndLua(Casinos._eEditorRunSourcePlatform.Android);
        }
        if (GUILayout.Button("CopyRawData", GUILayout.Width(200)))
        {
            _copyRawData(Casinos._eEditorRunSourcePlatform.Android);
        }
        if (GUILayout.Button("CopyLua", GUILayout.Width(200)))
        {
            _copyLua(Casinos._eEditorRunSourcePlatform.Android);
        }
        if (GUILayout.Button("生成DataFileList.txt", GUILayout.Width(200)))
        {
            _genDataFileList(Casinos._eEditorRunSourcePlatform.Android);
        }

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
        }
        if (GUILayout.Button("生成AssetBundle（全部）", GUILayout.Width(200)))
        {
            _buildAssetBundle(Casinos._eEditorRunSourcePlatform.IOS, false);
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("ClearRawDataAndLua", GUILayout.Width(200)))
        {
            _clearRawDataAndLua(Casinos._eEditorRunSourcePlatform.Android);
        }
        if (GUILayout.Button("CopyRawData", GUILayout.Width(200)))
        {
            _copyRawData(Casinos._eEditorRunSourcePlatform.IOS);
        }
        if (GUILayout.Button("CopyLua", GUILayout.Width(200)))
        {
            _copyLua(Casinos._eEditorRunSourcePlatform.IOS);
        }
        if (GUILayout.Button("生成DataFileList.txt", GUILayout.Width(200)))
        {
            _genDataFileList(Casinos._eEditorRunSourcePlatform.IOS);
        }

        GUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        //if (GUILayout.Button("发布", GUILayout.Width(200)))
        //{
        //    //SaveBug();
        //}
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

        Object[] select_objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        List<string> list_single_pkg = new List<string>();
        Dictionary<string, List<string>> map_fold_pkg = new Dictionary<string, List<string>>();

        foreach (var i in select_objs)
        {
            string path_asset = AssetDatabase.GetAssetPath(i);
            bool is_directory = Directory.Exists(path_asset);
            if (is_directory) continue;

            string extension = Path.GetExtension(path_asset);
            if (DoNotPackFileExtention.Contains(extension)) continue;

            string file_name = Path.GetFileName(path_asset);

            if (path_asset.Contains(AssetBundlePkgSingleFoldName))
            {
                list_single_pkg.Add(path_asset);
            }
            else if (path_asset.Contains(AssetBundlePkgFoldFoldName))
            {
                string fold = path_asset;
                fold = fold.Replace("Assets/", "");
                fold = fold.Replace(AssetBundlePkgFoldFoldName + "/", "");
                fold = EditorContext.Instance.PathStreamingAssets + platform.ToString() + "/" + fold;
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
            path_assetex = path_assetex.Replace("Assets/", "");
            path_assetex = path_assetex.Replace(AssetBundlePkgSingleFoldName + "/", "");
            path_assetex = EditorContext.Instance.PathStreamingAssets + platform.ToString() + "/" + path_assetex;

            string file_name = Path.GetFileName(path_assetex);
            string obj_dir = path_assetex.Replace(file_name, "");
            var names = AssetDatabase.GetDependencies(i);
            string ab_name = Path.GetFileNameWithoutExtension(path_assetex);
            _pakABSingleEx(ab_name, names, obj_dir, build_target);
        }

        foreach (var i in map_fold_pkg)
        {
            List<string> list_samefold_file = i.Value;
            _pakABFoldEx(i.Key, list_samefold_file, build_target);
        }

        ShowNotification(new GUIContent("BuildAssetBundle Finished!"));
    }

    //-------------------------------------------------------------------------
    void _pakABSingleEx(string ab_name, string[] dependence_name, string path, BuildTarget build_target)
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

        BuildPipeline.BuildAssetBundles(path, arr_abb,
            BuildAssetBundleOptions.ForceRebuildAssetBundle,
            build_target);
    }

    //-------------------------------------------------------------------------
    void _pakABFoldEx(string path, List<string> list_samefold_file, BuildTarget build_target)
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

        BuildPipeline.BuildAssetBundles(path, arr_abb,
            BuildAssetBundleOptions.ForceRebuildAssetBundle,
            build_target);
    }

    //-------------------------------------------------------------------------
    void _clearRawDataAndLua(Casinos._eEditorRunSourcePlatform platform)
    {
        string path_rawdata = string.Empty;
        string path_lua = string.Empty;
        switch (platform)
        {
            case Casinos._eEditorRunSourcePlatform.Android:
                path_rawdata = EditorContext.Instance.PathStreamingAssets + "ANDROID\\Resources.KingTexas\\";
                path_lua = EditorContext.Instance.PathStreamingAssets + "ANDROID\\Script.Lua\\";
                break;
            case Casinos._eEditorRunSourcePlatform.IOS:
                path_rawdata = EditorContext.Instance.PathStreamingAssets + "IOS\\Resources.KingTexas\\";
                path_lua = EditorContext.Instance.PathStreamingAssets + "IOS\\Script.Lua\\";
                break;
            case Casinos._eEditorRunSourcePlatform.PC:
                path_rawdata = EditorContext.Instance.PathStreamingAssets + "PC\\Resources.KingTexas\\";
                path_lua = EditorContext.Instance.PathStreamingAssets + "PC\\Script.Lua\\";
                break;
        }

        List<string> list_rawdata = new List<string>();
        DirectoryInfo dir = new DirectoryInfo(path_rawdata);
        DirectoryInfo[] dir_list = dir.GetDirectories();// 获取目录下（不包含子目录）的子目录
        foreach (DirectoryInfo i in dir_list)
        {
            if (i is DirectoryInfo)
            {
                if (i.FullName.Contains("Raw"))
                {
                    list_rawdata.Add(i.FullName);
                }
            }
        }

        foreach (var i in list_rawdata)
        {
            if (Directory.Exists(i)) Directory.Delete(i, true);
        }

        if (Directory.Exists(path_lua)) Directory.Delete(path_lua, true);

        ShowNotification(new GUIContent("ClearRawDataAndLua Finished!"));
    }

    //-------------------------------------------------------------------------
    void _copyRawData(Casinos._eEditorRunSourcePlatform platform)
    {
        string path_src = EditorContext.Instance.PathAssets + "Resources.KingTexas\\Raw\\";
        string path_dst = string.Empty;
        switch (platform)
        {
            case Casinos._eEditorRunSourcePlatform.Android:
                path_dst = EditorContext.Instance.PathStreamingAssets + "ANDROID\\Resources.KingTexas\\";
                break;
            case Casinos._eEditorRunSourcePlatform.IOS:
                path_dst = EditorContext.Instance.PathStreamingAssets + "IOS\\Resources.KingTexas\\";
                break;
            case Casinos._eEditorRunSourcePlatform.PC:
                path_dst = EditorContext.Instance.PathStreamingAssets + "PC\\Resources.KingTexas\\";
                break;
        }

        _copyDir(platform, path_src, path_dst);

        ShowNotification(new GUIContent("CopyRawData Finished!"));
    }

    //-------------------------------------------------------------------------
    void _copyLua(Casinos._eEditorRunSourcePlatform platform)
    {
        string path_src = EditorContext.Instance.PathAssets + "Script.Lua\\";
        string path_dst = string.Empty;
        switch (platform)
        {
            case Casinos._eEditorRunSourcePlatform.Android:
                path_dst = EditorContext.Instance.PathStreamingAssets + "ANDROID\\Script.Lua\\";
                break;
            case Casinos._eEditorRunSourcePlatform.IOS:
                path_dst = EditorContext.Instance.PathStreamingAssets + "IOS\\Script.Lua\\";
                break;
            case Casinos._eEditorRunSourcePlatform.PC:
                path_dst = EditorContext.Instance.PathStreamingAssets + "PC\\Script.Lua\\";
                break;
        }

        _copyDir(platform, path_src, path_dst);

        ShowNotification(new GUIContent("CopyLua Finished!"));
    }

    //-------------------------------------------------------------------------
    void _copyDir(Casinos._eEditorRunSourcePlatform platform, string path_src, string path_dst)
    {
        DirectoryInfo dir = new DirectoryInfo(path_src);
        FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();// 获取目录下（不包含子目录）的文件和子目录
        foreach (FileSystemInfo i in fileinfo)
        {
            if (i is DirectoryInfo)// 判断是否文件夹
            {
                if (!Directory.Exists(path_dst + "\\" + i.Name))
                {
                    Directory.CreateDirectory(path_dst + "\\" + i.Name);
                }
                _copyDir(platform, i.FullName, path_dst + "\\" + i.Name);// 递归调用复制子文件夹
            }
            else
            {
                string file_extension = Path.GetExtension(i.FullName);
                if (DoNotPackFileExtention.Contains(file_extension)) continue;

                File.Copy(i.FullName, path_dst + "\\" + i.Name, true);// 不是文件夹即复制文件，true表示可以覆盖同名文件
            }
        }
    }

    //-------------------------------------------------------------------------
    void _genDataFileList(Casinos._eEditorRunSourcePlatform platform)
    {
        string path_dst = string.Empty;
        switch (platform)
        {
            case Casinos._eEditorRunSourcePlatform.Android:
                path_dst = EditorContext.Instance.PathStreamingAssets + "ANDROID\\";
                break;
            case Casinos._eEditorRunSourcePlatform.IOS:
                path_dst = EditorContext.Instance.PathStreamingAssets + "IOS\\";
                break;
            case Casinos._eEditorRunSourcePlatform.PC:
                path_dst = EditorContext.Instance.PathStreamingAssets + "PC\\";
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
            _genDataFileList2(sw, path_dst, path_dst);
        }

        ShowNotification(new GUIContent("GenDataFileList Finished!"));
    }

    //-------------------------------------------------------------------------
    void _genDataFileList2(StreamWriter sw, string cur_path, string path_dst)
    {
        string[] files = Directory.GetFiles(cur_path);
        foreach (var i in files)
        {
            string directory_name = Path.GetDirectoryName(i);
            directory_name = directory_name.Replace(@"\", "/");
            directory_name = directory_name.Substring(directory_name.LastIndexOf("/") + 1);
            string file_name = Path.GetFileName(i);

            if (DoNotCopyFileName.Contains(file_name)) continue;

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
            _genDataFileList2(sw, i, path_dst);
        }
    }

    //-------------------------------------------------------------------------
    // 计算并返回单个文件的MD5值
    string _calcOneFileMD5(string target_path, string file_name, string file_path)
    {
        Sb.Clear();
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