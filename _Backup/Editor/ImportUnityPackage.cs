using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class ImportUnityPackage : EditorWindow
{
    //-------------------------------------------------------------------------
    static string mPackagePath;
    static string mManifestBackUpPath;
    static string mCurrentDir;
    const string mThirdPackageDirectoryName = "ThirdPackage";
    const string mManifestBackUp = "ManifestBackUp";
    const string mPluginsName = "Plugins";
    const string mAssetName = "Assets";
    const string mUnityNativeThirdPackageFilePath = @"Plugins\GameCloud.Native\TheThirdSDK\";
    const string mPluginsAndoridManifestPath = @"Plugins\Android\AndroidManifest.xml";
    const string mMainManifestBakUpName = @"\MainManifest\";
    string[] mThirdPackageName = { "DataEye" };

    //-------------------------------------------------------------------------
    [MenuItem("GameCloud.Unity/ImportThirdUnityPackage")]
    static void AddWindow()
    {
        EditorWindow.GetWindow(typeof(ImportUnityPackage));

        mCurrentDir = System.Environment.CurrentDirectory;
        Debug.LogError(mCurrentDir);
        string current_dir = mCurrentDir;
        current_dir = current_dir.Substring(0, current_dir.LastIndexOf(@"\"));
        mPackagePath = string.Format(current_dir + "{0}", @"\" + mThirdPackageDirectoryName);
        mManifestBackUpPath = string.Format(current_dir + "{0}", @"\" + mManifestBackUp);
    }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        EditorGUILayout.LabelField("UnityPackagePath:", mPackagePath);
        EditorGUILayout.LabelField("ManifestBackUpPath:", mManifestBackUpPath);
        bool import = GUILayout.Button("ImportPackage", GUILayout.Width(200));
        if (import)
        {
            //_importPackage();
        }
    }

    //-------------------------------------------------------------------------
    void _importPackage()
    {
        if (!Directory.Exists(mPackagePath))
        {
            ShowNotification(new GUIContent("无该目录!"));
        }
        else
        {
            string[] files = Directory.GetFiles(mPackagePath);
            if (files.Length == 0)
            {
                return;
            }

            _copyFile(_getManifestPath(), mManifestBackUpPath + mMainManifestBakUpName + "AndroidManifest.xml");

            foreach (var i in files)
            {
                AssetDatabase.ImportPackage(i, false);

                foreach (var item in mThirdPackageName)
                {
                    if (i.Contains(item))
                    {
                        _copyFile(_getManifestPath(), mManifestBackUpPath + @"\" + item + @"\AndroidManifest.xml");
                        break;
                    }
                }

                _copyToNativePath();
            }
        }
    }

    //-------------------------------------------------------------------------
    void _copyFile(string source_path, string target_path)
    {
        File.Copy(source_path, target_path, true);
    }

    //-------------------------------------------------------------------------
    void _copyToNativePath()
    {
        string current_dir = mCurrentDir + "/" + mAssetName;
        string[] dirs = Directory.GetDirectories(current_dir);
        foreach (var i in dirs)
        {
            Debug.LogError(i);
            foreach (var item in mThirdPackageName)
            {
                if (i.Contains(item))
                {
                    string last_dir = i.Substring(i.LastIndexOf(@"\") + 1);
                    string[] files = Directory.GetFiles(i);
                    foreach (var file in files)
                    {
                        string file_name = Path.GetFileName(file);
                        _copyFile(file, _getThirdPackagePath() + last_dir + "/" + file_name);
                    }
                    Directory.Delete(i);
                    break;
                }
            }
        }
    }

    //-------------------------------------------------------------------------
    string _getManifestPath()
    {
        string streaming_assets = Application.streamingAssetsPath;
        streaming_assets = streaming_assets.Substring(0, streaming_assets.LastIndexOf("/"));

        return streaming_assets + "/" + mPluginsAndoridManifestPath;
    }

    //-------------------------------------------------------------------------
    string _getThirdPackagePath()
    {
        string streaming_assets = Application.streamingAssetsPath;
        streaming_assets = streaming_assets.Substring(0, streaming_assets.LastIndexOf("/"));

        return streaming_assets + "/" + mUnityNativeThirdPackageFilePath;
    }
}
