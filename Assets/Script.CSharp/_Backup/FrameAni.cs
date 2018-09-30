//// Copyright(c) Cragon. All rights reserved.

//namespace Casinos
//{
//    using System;
//    using System.IO;
//    using System.Collections.Generic;
//    using FairyGUI;

//    public class FrameAni
//    {
//        //---------------------------------------------------------------------
//        Queue<string> QueResPath { get; set; }
//        List<string> ListAllFileName { get; set; }
//        Dictionary<string, NTexture> MapResTexture { get; set; }
//        GLoader GLoader { get; set; }
//        float PerFrameTm { get; set; }
//        float CurrentFrameTm { get; set; }
//        bool LoadAllDone { get; set; }
//        Action LoaderAllDoneCallBack { get; set; }

//        //---------------------------------------------------------------------
//        public FrameAni(float per_frametm, string path, string file_title, GLoader loader, Action loaderall_done_callback)
//        {
//            PerFrameTm = per_frametm;
//            GLoader = loader;
//            LoaderAllDoneCallBack = loaderall_done_callback;
//            if (Directory.Exists(path))
//            {
//                QueResPath = new Queue<string>();
//                ListAllFileName = new List<string>();
//                MapResTexture = new Dictionary<string, NTexture>();
//                var files = Directory.GetFiles(path);
//                if (files.Length > 0)
//                {
//                    List<string> list_files = new List<string>();
//                    foreach (var i in files)
//                    {
//                        var ext = Path.GetExtension(i);
//                        if (ext != ".ab")
//                        {
//                            continue;
//                        }

//                        list_files.Add(i);
//                        var file_name = Path.GetFileNameWithoutExtension(i);
//                        ListAllFileName.Add(file_name);
//                    }

//                    list_files.Sort((x1, x2) =>
//                    {
//                        var file_name1 = Path.GetFileNameWithoutExtension(x1);
//                        var file_name2 = Path.GetFileNameWithoutExtension(x2);
//                        file_name1 = file_name1.Replace(file_title, "");
//                        file_name2 = file_name2.Replace(file_title, "");
//                        var file1_index = int.Parse(file_name1);
//                        var file2_index = int.Parse(file_name2);
//                        return file1_index.CompareTo(file2_index);
//                    });

//                    foreach (var i in list_files)
//                    {
//                        QueResPath.Enqueue(i);
//                    }
//                }
//            }

//            _setTexture();
//        }

//        //---------------------------------------------------------------------
//        public void update(float tm)
//        {
//            CurrentFrameTm += tm;
//            if (CurrentFrameTm >= PerFrameTm)
//            {
//                CurrentFrameTm = 0f;
//                _setTexture();
//            }

//            if (!LoadAllDone)
//            {
//                bool load_alldone = true;
//                foreach (var i in ListAllFileName)
//                {
//                    if (!TextureMgr.Instant.haveTexture(i))
//                    {
//                        load_alldone = false;
//                        break;
//                    }
//                }

//                if (load_alldone)
//                {
//                    LoadAllDone = true;
//                    LoaderAllDoneCallBack();
//                }
//            }
//        }

//        //---------------------------------------------------------------------
//        void _setTexture()
//        {
//            if (QueResPath.Count > 0)
//            {
//                var current_path = QueResPath.Dequeue();
//                if (MapResTexture.ContainsKey(current_path))
//                {
//                    GLoader.texture = MapResTexture[current_path];
//                }
//                else
//                {
//                    GLoader.icon = current_path;
//                }
//                QueResPath.Enqueue(current_path);
//            }
//        }
//    }
//}