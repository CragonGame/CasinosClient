// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.IO;
    using System.Text;
    using UnityEngine;
    //using XLua;

    //[LuaCallCSharp]
    public enum _eEditorRunSourcePlatform
    {
        Android,
        IOS,
        PC,
    }

    //[LuaCallCSharp]
    public enum DirType
    {
        Raw = 0,
        Resources,
    }

    //[LuaCallCSharp]
    public class PathMgr
    {
        //---------------------------------------------------------------------
        public string PathAssets { get; private set; }// Unity3D的Assets目录
        public string PathSettings { get; private set; }// Unity3D的Settings目录
        public string PathSettingsUser { get; private set; }// Unity3D的SettingsUser目录
        public string DirLaunchAb { get; set; }// Resources.KingTexasLaunch目录，需动态计算
        public DirType DirLaunchAbType { get; set; }// Resources.KingTexasLaunch目录类型，需动态计算
        public string DirLuaRoot { get; set; }// Lua/目录，需动态计算
        public string DirRawRoot { get; set; }// Raw/，需动态计算
        public string DirCsRoot { get; set; }// Cs/，需动态计算
        public string DirAbRoot { get; set; }// "Resources.KingTexas/"，需动态计算
        public string DirAbUi { get; set; }// "Resources.KingTexas/Ui/"，需动态计算
        public string DirAbCard { get; set; }// "Resources.KingTexas/Cards/"，需动态计算
        public string DirAbAudio { get; set; }// "Resources.KingTexas/Audio/"，需动态计算
        public string DirAbItem { get; set; }// "Resources.KingTexas/Item/"，需动态计算
        public string DirAbParticle { get; set; }// "Resources.KingTexas/Particle/"，需动态计算
        _eEditorRunSourcePlatform EditorModeRunsourcesPlatform { get; set; }
        string CombinePlatform { get; set; }
        StringBuilder Sb { get; set; }
        string WWWPersistentDataPath { get; set; }
        string PersistentDataPath { get; set; }
        string StreamingAssetsPath { get; set; }
        string WWWStreamingAssetsPath { get; set; }

        //---------------------------------------------------------------------
        public PathMgr(_eEditorRunSourcePlatform editor_mode_runsources_platform)
        {
            Sb = new StringBuilder(256);

            EditorModeRunsourcesPlatform = editor_mode_runsources_platform;
            switch (EditorModeRunsourcesPlatform)
            {
                case _eEditorRunSourcePlatform.Android:
                    CombinePlatform = "ANDROID";
                    break;
                case _eEditorRunSourcePlatform.IOS:
                    CombinePlatform = "IOS";
                    break;
                case _eEditorRunSourcePlatform.PC:
                    CombinePlatform = "PC";
                    break;
                default:
                    break;
            }

            // WWWPersistentDataPath
            {
                WWWPersistentDataPath =
#if UNITY_STANDALONE_WIN && UNITY_EDITOR
                "file:///" + Application.persistentDataPath + "/"+ CombinePlatform;
#elif UNITY_ANDROID && UNITY_EDITOR
                "file:///" + Application.persistentDataPath + "/" + CombinePlatform;
#elif UNITY_IPHONE && UNITY_EDITOR
                "file:///" + Application.persistentDataPath + "/" + CombinePlatform;
#elif UNITY_ANDROID
                "file:///" + Application.persistentDataPath;
#elif UNITY_IPHONE
                "file:///" + Application.persistentDataPath;
#else
                string.Empty;
#endif
            }

            // PersistentDataPath
            {
                PersistentDataPath =
#if UNITY_STANDALONE_WIN && UNITY_EDITOR
                Application.persistentDataPath +"/"+ CombinePlatform;
#elif UNITY_ANDROID && UNITY_EDITOR
                Application.persistentDataPath + "/" + CombinePlatform;
#elif UNITY_IPHONE && UNITY_EDITOR
                Application.persistentDataPath + "/" + CombinePlatform;
#elif UNITY_ANDROID
                Application.persistentDataPath;
#elif UNITY_IPHONE
                Application.persistentDataPath;
#else
                string.Empty;
#endif
            }

            // StreamingAssetsPath
            {
                StreamingAssetsPath =

#if UNITY_EDITOR
                Application.streamingAssetsPath;
#elif UNITY_IPHONE
                Application.streamingAssetsPath;
#elif UNITY_ANDROID
                Application.streamingAssetsPath;
#endif
                StreamingAssetsPath += "/" + EditorModeRunsourcesPlatform.ToString();
            }

            // WWWStreamingAssetsPath
            {
                WWWStreamingAssetsPath =
#if UNITY_EDITOR
                "file:///" + Application.streamingAssetsPath;
#elif UNITY_IPHONE
                "file:///" + Application.streamingAssetsPath;
#elif UNITY_ANDROID
                "jar:file://" + Application.dataPath + "!/assets";
#endif
                WWWStreamingAssetsPath += "/" + EditorModeRunsourcesPlatform.ToString();
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
        }

        //---------------------------------------------------------------------
        public string GetWWWPersistentDataPath()
        {
            return WWWPersistentDataPath;
        }

        //---------------------------------------------------------------------
        public string CombineWWWPersistentDataPath(string path)
        {
            Sb.Length = 0;
            Sb.Append(WWWPersistentDataPath);
            Sb.Append("/");
            Sb.Append(path);
            return Sb.ToString();
        }

        //---------------------------------------------------------------------
        public string GetPersistentDataPath()
        {
            return PersistentDataPath;
        }

        //---------------------------------------------------------------------
        public string CombinePersistentDataPath(string path, bool for_lua = false)
        {
            Sb.Length = 0;
            Sb.Append(PersistentDataPath);
            Sb.Append("/");
            Sb.Append(path);
            return Sb.ToString();
        }

        //---------------------------------------------------------------------
        public string GetStreamingAssetsPath()
        {
            return StreamingAssetsPath;
        }

        //---------------------------------------------------------------------
        public string CombineStreamingAssetsPath(string path)
        {
            Sb.Length = 0;
            Sb.Append(StreamingAssetsPath);
            Sb.Append("/");
            Sb.Append(path);
            return Sb.ToString();
        }

        //---------------------------------------------------------------------
        public string GetWWWStreamingAssetsPath()
        {
            return WWWStreamingAssetsPath;
        }

        //---------------------------------------------------------------------
        public string CombineWWWStreamingAssetsPath(string path)
        {
            Sb.Length = 0;
            Sb.Append(WWWStreamingAssetsPath);
            Sb.Append("/");
            Sb.Append(path);
            return Sb.ToString();
        }
    }
}