//// Copyright (c) Cragon. All rights reserved.

//namespace Casinos
//{
//    using UnityEngine;

//    public static class StreamingAssetsPathHelper
//    {
//        //---------------------------------------------------------------------
//        public static string wwwPath(string name)
//        {
//            if (string.IsNullOrEmpty(name)) { name = ""; }
//            if (_isPathSeparator(name[0])) name = "/" + name;

//            string filepath = "";

//#if UNITY_EDITOR
//            filepath = @"file://" + Application.streamingAssetsPath + name;
//#elif UNITY_IPHONE
//        filepath = @"file://" + Application.streamingAssetsPath + name;
//#elif UNITY_ANDROID
//        filepath = @"file://" + Application.persistentDataPath + "/assets" + name;
//#endif

//            return filepath;
//        }

//        //---------------------------------------------------------------------
//        public static string filePath(string file_name)
//        {
//            if (string.IsNullOrEmpty(file_name)) { file_name = ""; }
//            if (_isPathSeparator(file_name[0])) file_name = "/" + file_name;

//            string filepath = "";

//#if UNITY_ANDROID && !UNITY_EDITOR
//        filepath = Application.persistentDataPath + "/assets" + file_name;
//#else
//            filepath = Application.streamingAssetsPath + file_name;
//#endif

//            return filepath;
//        }

//        //---------------------------------------------------------------------
//        static bool _isPathSeparator(char c)
//        {
//            return c != '/' && c != '\\';
//        }
//    }
//}