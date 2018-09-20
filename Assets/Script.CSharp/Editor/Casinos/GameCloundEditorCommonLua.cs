using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class GameCloundEditorCommonLua : EditorWindow
{
    //-------------------------------------------------------------------------
    MD5 MD5 { get; set; }
    string CurrentVersion { get; set; }
    static string mABTargetPathRoot;
    static string mCommonLuaDateFileListPath;
    static string mCommonLuaTargetPath;
    static string mRowAssetPath;
    static string mVersionInfoPath;
    static List<string> mDoNotPackFileExtention = new List<string> { ".meta", ".DS_Store" };
    static List<string> mDoNotCopyFileName = new List<string> { "ConfigC.lua", "ConfigS.lua", "DBConfig.lua" };
    static List<string> mDoNotCopyDir = new List<string> { ".idea" };
    const string mVersionInfoName = "CommonLuaVersionInfo.xml";
    const string PackInfoTextName = "CommonLuaFileList.txt";

    //-------------------------------------------------------------------------
    [MenuItem("CasinosPublish/GenCommonLuaRes", false, 1)]
    static void CreateCommonLuaFileList()
    {
        ScriptableObject.CreateInstance<GameCloundEditorCommonLua>();

        EditorWindow.GetWindow<GameCloundEditorCommonLua>();
    }

    //-------------------------------------------------------------------------
    void OnEnable()
    {
        MD5 = new MD5CryptoServiceProvider();

        string current_dir = System.Environment.CurrentDirectory;
        current_dir = current_dir.Replace(@"\", "/");
        string asset_path = current_dir + "/" + GameCloudEditor.AssetRoot;
        mRowAssetPath = asset_path + "CommonLua";

        mABTargetPathRoot = Path.Combine(current_dir, GameCloudEditor.AssetBundleTargetDirectory);
        mABTargetPathRoot = mABTargetPathRoot.Replace(@"\", "/");
        mCommonLuaDateFileListPath = mABTargetPathRoot + "/CommonLua";

        mVersionInfoPath = mCommonLuaDateFileListPath + "/" + mVersionInfoName;
        XmlDocument x_d = new XmlDocument();
        x_d.Load(mVersionInfoPath);
        XmlElement root = null;
        root = x_d.DocumentElement;

        foreach (XmlNode i in root.ChildNodes)
        {
            if (i is XmlComment)
            {
                continue;
            }

            var xml_element = (XmlElement)i;
            CurrentVersion = xml_element.GetAttribute("Version");
        }
    }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        EditorGUILayout.LabelField("版本号:", CurrentVersion);
        EditorGUILayout.BeginHorizontal();
        bool add_dataeversion = GUILayout.Button("版本号加一");
        if (add_dataeversion)
        {
            _changeDataData(true);
        }
        bool minus_dataeversion = GUILayout.Button("版本号减一");
        if (minus_dataeversion)
        {
            _changeDataData(false);
        }
        EditorGUILayout.EndHorizontal();

        bool click_build_asset = GUILayout.Button("生成DateFileList文件", GUILayout.Width(200));
        if (click_build_asset)
        {
            _startBuild();
        }
    }

    //-------------------------------------------------------------------------
    void _startBuild()
    {
        mCommonLuaTargetPath = mCommonLuaDateFileListPath + "/" + CurrentVersion;

        if (Directory.Exists(mRowAssetPath))
        {
            GameCloudEditor.copyFile(mRowAssetPath, mCommonLuaTargetPath, mRowAssetPath, mDoNotCopyDir);
        }

        if (!Directory.Exists(mCommonLuaTargetPath))
        {
            Directory.CreateDirectory(mCommonLuaTargetPath);
        }

        StreamWriter sw;
        string info = mCommonLuaTargetPath + "/" + PackInfoTextName;

        if (File.Exists(info))
        {
            File.Delete(info);
        }

        sw = File.CreateText(info);

        using (sw)
        {
            _checkPackInfo(sw, mCommonLuaTargetPath);
        }
        ShowNotification(new GUIContent("打包完成!"));
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
            bool need_continue = false;
            foreach (var j in mDoNotCopyDir)
            {
                if(directory_name.Contains(j))
                {
                    need_continue = true;
                    break;
                }
            }

            if(need_continue)
            {
                continue;
            }

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
            string target_path = file_directory.Replace(mCommonLuaTargetPath + "/", "");
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
    void _changeDataData(bool add)
    {
        int data = int.Parse(CurrentVersion.Replace(".", ""));
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
        CurrentVersion = data_version;

        XmlDocument x_d = new XmlDocument();
        x_d.Load(mVersionInfoPath);
        XmlElement root = null;
        root = x_d.DocumentElement;

        foreach (XmlNode i in root.ChildNodes)
        {
            if (i is XmlComment)
            {
                continue;
            }

            var xml_element = (XmlElement)i;
            xml_element.SetAttribute("Version", CurrentVersion);
        }

        x_d.Save(mVersionInfoPath);
    }
}
