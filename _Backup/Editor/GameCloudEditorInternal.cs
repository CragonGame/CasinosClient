using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

public class GameCloudEditorInternal : EditorWindow
{
    static string mUnityPackagePath = "./";

    //-------------------------------------------------------------------------
    [MenuItem("GameCloud.Unity/Native/GameCloud.Unity.NativeAll")]
    static void exportGameCloudUnityNativePackageAll()
    {
        string[] arr_assetpathname = new string[5];
        arr_assetpathname[0] = "Assets/GameCloud.Unity.Native";
        arr_assetpathname[1] = "Assets/GameCloud.Unity.Client/Editor/XcodeProjectMod.cs";
        arr_assetpathname[2] = "Assets/Plugins/Android";
        arr_assetpathname[3] = "Assets/Plugins/GameCloud.Unity.Native";        
        arr_assetpathname[4] = "Assets/GameCloud.Unity.Sample/Native";

        AssetDatabase.ExportPackage(arr_assetpathname, mUnityPackagePath + "GameCloud.Unity.NativeAll.unitypackage", ExportPackageOptions.Recurse);

        Debug.Log("Export GameCloud.Unity.NativeAll.unitypackage Finished!");
    }

    //-------------------------------------------------------------------------
    [MenuItem("GameCloud.Unity/Native/导出GameCloud.Unity.NativeDataEye")]
    static void exportGameCloudUnityNativeDataEye()
    {
        string[] arr_assetpathname = new string[5];
        arr_assetpathname[0] = "Assets/GameCloud.Unity.Native";
        arr_assetpathname[1] = "Assets/GameCloud.Unity.Client/Editor/XcodeProjectMod.cs";
        arr_assetpathname[2] = "Assets/Plugins/Android";
        arr_assetpathname[3] = "Assets/Plugins/GameCloud.Unity.Native/DataEye";
        arr_assetpathname[4] = "Assets/Plugins/GameCloud.Unity.Native/SDKReceiveMono";
        AssetDatabase.ExportPackage(arr_assetpathname, mUnityPackagePath + "GameCloud.Unity.NativeDataEye.unitypackage", ExportPackageOptions.Recurse);

        Debug.Log("Export GameCloud.Unity.NativeDataEye.unitypackage Finished!");
    }

    //-------------------------------------------------------------------------
    [MenuItem("GameCloud.Unity/Native/导出GameCloud.Unity.NativePay")]
    static void exportUnityNativePay()
    {
        string[] arr_assetpathname = new string[5];
        arr_assetpathname[0] = "Assets/GameCloud.Unity.Native";
        arr_assetpathname[1] = "Assets/GameCloud.Unity.Client/Editor/XcodeProjectMod.cs";
        arr_assetpathname[2] = "Assets/Plugins/Android";
        arr_assetpathname[3] = "Assets/Plugins/GameCloud.Unity.Native/Pay";
        arr_assetpathname[4] = "Assets/Plugins/GameCloud.Unity.Native/SDKReceiveMono";
        AssetDatabase.ExportPackage(arr_assetpathname, mUnityPackagePath + "GameCloud.Unity.NativePay.unitypackage", ExportPackageOptions.Recurse);

        Debug.Log("Export GameCloud.Unity.NativePay.unitypackage Finished!");
    }

    //-------------------------------------------------------------------------
    [MenuItem("GameCloud.Unity/Native/导出GameCloud.Unity.Native")]
    static void exportGameCloudNativeBySelf()
    {
        string[] arr_assetpathname = new string[5];
        arr_assetpathname[0] = "Assets/GameCloud.Unity.Native";
        arr_assetpathname[1] = "Assets/GameCloud.Unity.Client/Editor/XcodeProjectMod.cs";
        arr_assetpathname[2] = "Assets/Plugins/Android";
        arr_assetpathname[3] = "Assets/Plugins/GameCloud.Unity.Native/Native";
        arr_assetpathname[4] = "Assets/Plugins/GameCloud.Unity.Native/SDKReceiveMono";
        AssetDatabase.ExportPackage(arr_assetpathname, mUnityPackagePath + "GameCloud.Unity.Native.unitypackage", ExportPackageOptions.Recurse);

        Debug.Log("Export GameCloud.Unity.Native.unitypackage Finished!");
    }

    //-------------------------------------------------------------------------
    [MenuItem("GameCloud.Unity/Native/导出GameCloud.Unity.Speech")]
    static void exportBaiduSpeech()
    {
        string[] arr_assetpathname = new string[5];
        arr_assetpathname[0] = "Assets/GameCloud.Unity.Native";
        arr_assetpathname[1] = "Assets/GameCloud.Unity.Client/Editor/XcodeProjectMod.cs";
        arr_assetpathname[2] = "Assets/Plugins/Android";
        arr_assetpathname[3] = "Assets/Plugins/GameCloud.Unity.Native/Speech";
        arr_assetpathname[4] = "Assets/Plugins/GameCloud.Unity.Native/SDKReceiveMono";
        AssetDatabase.ExportPackage(arr_assetpathname, mUnityPackagePath + "GameCloud.Unity.Speech.unitypackage", ExportPackageOptions.Recurse);

        Debug.Log("Export GameCloud.Unity.Speech.unitypackage Finished!");
    }

    //-------------------------------------------------------------------------
    [MenuItem("GameCloud.Unity/导出GameCloud.Unity.Client.unitypackage")]
    static void exportGameCloudUnityPackage()
    {
        string[] arr_assetpathname = new string[4];
        arr_assetpathname[0] = "Assets/GameCloud.Unity.Client";
        arr_assetpathname[1] = "Assets/Plugins/GameCloud.Unity.Common";
        arr_assetpathname[2] = "Assets/Plugins/GameCloud.Unity.Sqlite";
        arr_assetpathname[3] = "Assets/Plugins/GameCloud.UnityPlugins";

        AssetDatabase.ExportPackage(arr_assetpathname, mUnityPackagePath + "GameCloud.Unity.Client.unitypackage", ExportPackageOptions.Recurse);

        Debug.Log("Export GameCloud.Unity.Client.unitypackage Finished!");
    }

    //-------------------------------------------------------------------------
    [MenuItem("GameCloud.Unity/导出GameCloud.Unity.Json.unitypackage")]
    static void exportGameCloudJsonPackage()
    {
        string[] arr_assetpathname = new string[1];
        arr_assetpathname[0] = "Assets/Plugins/GameCloud.Unity.Json";
        AssetDatabase.ExportPackage(arr_assetpathname, mUnityPackagePath + "GameCloud.Unity.Json.unitypackage", ExportPackageOptions.Recurse);

        Debug.Log("Export GameCloud.Unity.Json.unitypackage Finished!");
    }
}
