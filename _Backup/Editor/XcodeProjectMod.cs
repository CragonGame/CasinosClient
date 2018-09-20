#if UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class XcodeProjectMod : MonoBehaviour
{
    static List<FrameWorkInfo> mListFramWork = new List<FrameWorkInfo>();
    static List<string> mListLibs = new List<string>();

    //-------------------------------------------------------------------------
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
        string projPath = PBXProject.GetPBXProjectPath(path);


        string[] files = Directory.GetFiles(Application.dataPath, "*.xcodemod", SearchOption.AllDirectories);
        foreach (string i in files)
        {
            _getModInfo(i);
        }

        PBXProject proj = new PBXProject();
        proj.ReadFromString(File.ReadAllText(projPath));
        string target = proj.TargetGuidByName("Unity-iPhone");

        _addFrameWork(proj, target);
        _addLibs(proj, target);

        //proj.AddFrameworkToProject(target, "AssetsLibrary.framework", false);

        //CopyAndReplaceDirectory("Assets/Lib/mylib.framework", Path.Combine(path, "Frameworks/mylib.framework"));
        //proj.AddFileToBuild(target, proj.AddFile("Frameworks/mylib.framework", "Frameworks/mylib.framework", PBXSourceTree.Source));

        //var fileName = "my_file.xml";
        //var filePath = Path.Combine("Assets/Lib", fileName);
        //File.Copy(filePath, Path.Combine(path, fileName));
        //proj.AddFileToBuild(target, proj.AddFile(fileName, fileName, PBXSourceTree.Source));

        //proj.SetBuildProperty(target, "CODE_SIGN_RESOURCE_RULES_PATH", "$(SDKROOT)/ResourceRules.plist");

        //proj.SetBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
        //proj.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(PROJECT_DIR)/Frameworks");

        File.WriteAllText(projPath, proj.WriteToString());
        }
    }

    //-------------------------------------------------------------------------
    static void _getModInfo(string path)
    {
        var exists = File.Exists(path);
        //FileInfo projectFileInfo = new FileInfo(path);
        if (!exists)
        {
            Debug.LogWarning("File does not exist.");
        }

        //string name = System.IO.Path.GetFileNameWithoutExtension(path);
        //string path = System.IO.Path.GetDirectoryName(path);

        Dictionary<string, string> map_modinfo = new Dictionary<string, string>();
        foreach (var i in File.ReadAllLines(path))
        {
            var index = i.IndexOf(":");
            var mode_key = i.Substring(0, index);
            var mode_value = i.Substring(index + 1);
            map_modinfo[mode_key.Trim().Replace("\"", "")] = mode_value.Trim().Replace("[", "").Replace("]", "");
        }
        string frameworks = "";
        if (map_modinfo.TryGetValue("Frameworks", out frameworks))
        {
            if (!string.IsNullOrEmpty(frameworks))
            {
                var list_frameworkinfo = frameworks.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var i in list_frameworkinfo)
                {
                    var framework_info = i.Trim().Replace("\"", "");
                    FrameWorkInfo framework = new FrameWorkInfo();
                    var f_info = framework_info.Split(new char[] { ':' }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (f_info.Length != 2)
                    {
                        Debug.LogWarning("Frameworks format Error.");
                    }
                    else
                    {
                        framework.name = f_info[0].Trim();
                        framework.is_weak = f_info[1].Trim() == "IsWeak";
                    }

                    mListFramWork.Add(framework);
                }
            }
        }
        string libs = "";
        if (map_modinfo.TryGetValue("Libs", out libs))
        {
            if (!string.IsNullOrEmpty(libs))
            {
                var list_libsinfo = libs.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                foreach (var i in list_libsinfo)
                {
                    mListLibs.Add(i.Trim().Replace("\"", ""));
                }
            }
        }
    }

    //-------------------------------------------------------------------------
    static void _addFrameWork(PBXProject proj, string target)
    {
        foreach (var i in mListFramWork)
        {
            proj.AddFrameworkToProject(target, i.name, i.is_weak);
        }
    }

    //-------------------------------------------------------------------------
    static void _addLibs(PBXProject proj, string target)
    {
        foreach (var i in mListLibs)
        {
            proj.AddFileToBuild(target, proj.AddFile(i, i, PBXSourceTree.Source));
        }
    }

    //-------------------------------------------------------------------------
    internal static void CopyAndReplaceDirectory(string srcPath, string dstPath)
    {
        if (Directory.Exists(dstPath))
            Directory.Delete(dstPath);
        if (File.Exists(dstPath))
            File.Delete(dstPath);

        Directory.CreateDirectory(dstPath);

        foreach (var file in Directory.GetFiles(srcPath))
            File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));

        foreach (var dir in Directory.GetDirectories(srcPath))
            CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
    }
}

//-------------------------------------------------------------------------
public class FrameWorkInfo
{
    public string name;
    public bool is_weak;
}
#endif
