// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.IO;
    using UnityEngine;
    using XLua;

    [LuaCallCSharp]
    public enum _eEditorRunSourcePlatform
    {
        Android,
        IOS,
        PC,
    }

    [LuaCallCSharp]
    public class PathMgr
    {
        //---------------------------------------------------------------------
        public bool UsePersistent { get; private set; }// true=使用临时目录中的资源，false=使用编辑器目录中的资源
        public string PathAssets { get; private set; }// Unity3D的Assets目录
        public string PathSettings { get; private set; }// Unity3D的Settings目录
        public string PathSettingsUser { get; private set; }// Unity3D的SettingsUser目录
        public string PathLuaRoot { get; private set; }// Lua文件所在根目录
        _eEditorRunSourcePlatform EditorModeRunsourcesPlatform { get; set; }
        string NeedCombinePath { get; set; }

        //---------------------------------------------------------------------
        public PathMgr(_eEditorRunSourcePlatform editor_mode_runsources_platform, bool use_persistent)
        {
            UsePersistent = use_persistent;
#if !UNITY_EDITOR
            UsePersistent = true;
#endif
            EditorModeRunsourcesPlatform = editor_mode_runsources_platform;
            switch (EditorModeRunsourcesPlatform)
            {
                case _eEditorRunSourcePlatform.Android:
                    NeedCombinePath = "ANDROID";
                    break;
                case _eEditorRunSourcePlatform.IOS:
                    NeedCombinePath = "IOS";
                    break;
                case _eEditorRunSourcePlatform.PC:
                    NeedCombinePath = "PC";
                    break;
                default:
                    break;
            }

            // PathAssets
            {
                string p = Path.Combine(Environment.CurrentDirectory, "./Assets/");
                var di = new DirectoryInfo(p);
                PathAssets = di.FullName;
            }

            // PathSettings
            {
                string p = Path.Combine(Environment.CurrentDirectory, "./Assets/Settings/");
                var di = new DirectoryInfo(p);
                PathSettings = di.FullName;
            }

            // PathSettingsUser
            {
                string p = Path.Combine(Environment.CurrentDirectory, "./Assets/SettingsUser/");
                var di = new DirectoryInfo(p);
                PathSettingsUser = di.FullName;
            }

            if (UsePersistent)
            {
                // PathLuaRoot
                PathLuaRoot = combinePersistentDataPath("Script.Lua/");
            }
            else
            {
                // PathLuaRoot
                {
                    string p = Path.Combine(Environment.CurrentDirectory, "./Assets/Script.Lua/");
                    var di = new DirectoryInfo(p);
                    PathLuaRoot = di.FullName;
                }
            }
        }

        //---------------------------------------------------------------------
        public string getWWWPersistentDataPath()
        {
            string persistent_path =
#if UNITY_STANDALONE_WIN && UNITY_EDITOR
            "file:///" + Application.persistentDataPath + "/"+ NeedCombinePath;
#elif UNITY_ANDROID && UNITY_EDITOR
            "file:///" + Application.persistentDataPath + "/" + NeedCombinePath;
#elif UNITY_IPHONE && UNITY_EDITOR
            "file:///" + Application.persistentDataPath+ "/" +NeedCombinePath;
#elif UNITY_ANDROID
            "file:///" + Application.persistentDataPath;
#elif UNITY_IPHONE
            "file:///" + Application.persistentDataPath;
#else
            string.Empty;
#endif
            return persistent_path;
        }

        //---------------------------------------------------------------------
        public string combineWWWPersistentDataPath(string path)
        {
            return getWWWPersistentDataPath() + "/" + path;
        }

        //---------------------------------------------------------------------
        public string getPersistentDataPath()
        {
            string persistent_path =
#if UNITY_STANDALONE_WIN && UNITY_EDITOR
            Application.persistentDataPath +"/"+ NeedCombinePath;
#elif UNITY_ANDROID && UNITY_EDITOR
            Application.persistentDataPath + "/" + NeedCombinePath;
#elif UNITY_IPHONE && UNITY_EDITOR
            Application.persistentDataPath+"/"+ NeedCombinePath;
#elif UNITY_ANDROID
            Application.persistentDataPath;
#elif UNITY_IPHONE
            Application.persistentDataPath;
#else
            string.Empty;
#endif

            return persistent_path;
        }

        //---------------------------------------------------------------------
        public string combinePersistentDataPath(string path, bool for_lua = false)
        {
            string p = getPersistentDataPath() + "/" + path;

            return p;
        }

        //---------------------------------------------------------------------
        public string getStreamingAssetsPath()
        {
            string assetbundlePath =
#if UNITY_EDITOR
            Application.streamingAssetsPath;
#elif UNITY_IPHONE
            Application.streamingAssetsPath;
#elif UNITY_ANDROID
            Application.streamingAssetsPath;
#endif

            assetbundlePath += "/" + EditorModeRunsourcesPlatform.ToString();

            return assetbundlePath;
        }

        //---------------------------------------------------------------------
        public string combineStreamingAssetsPath(string path)
        {
            return getStreamingAssetsPath() + "/" + path;
        }

        //---------------------------------------------------------------------
        public string getWWWStreamingAssetsPath()
        {
            string assetbundlePath =
#if UNITY_EDITOR
            "file:///" + Application.streamingAssetsPath;
#elif UNITY_IPHONE
            "file:///" + Application.streamingAssetsPath;
#elif UNITY_ANDROID
            "jar:file://" + Application.dataPath + "!/assets";
#endif
            assetbundlePath += "/" + EditorModeRunsourcesPlatform.ToString();

            return assetbundlePath;
        }

        //---------------------------------------------------------------------
        public string combineWWWStreamingAssetsPath(string path)
        {
            var path_combine = getWWWStreamingAssetsPath() + "/" + path;
            return path_combine;
        }
    }
}