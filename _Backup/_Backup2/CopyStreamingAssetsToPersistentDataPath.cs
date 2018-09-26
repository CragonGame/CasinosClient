//// Copyright (c) Cragon. All rights reserved.

//namespace Casinos
//{
//    using System;
//    using System.Collections;
//    using System.Collections.Generic;
//    using System.IO;
//    using System.Threading;
//    using UnityEngine;

//    public class CopyStreamingAssetsToPersistentDataPath
//    {
//        //---------------------------------------------------------------------   
//        public string CurrentDataFile { get; private set; }
//        public int TotalCount { get; private set; }
//        public int CurrentIndex { get; private set; }

//        const int MaxLoadCount = 5;

//        //---------------------------------------------------------------------        
//        Dictionary<StreamingAssetsFileInfo, WWW> MapWWW;
//        Dictionary<StreamingAssetsFileInfo, WWW> MapWWWDone;
//        Queue<StreamingAssetsFileInfo> QueFileNames;
//        Action<int, int> ActionPro;
//        Action ActionCopyDown;

//        //---------------------------------------------------------------------
//        public CopyStreamingAssetsToPersistentDataPath()
//        {
//            QueFileNames = new Queue<StreamingAssetsFileInfo>();
//            MapWWW = new Dictionary<StreamingAssetsFileInfo, WWW>();
//            MapWWWDone = new Dictionary<StreamingAssetsFileInfo, WWW>();
//            CurrentDataFile = "";
//            CurrentIndex = 0;
//            TotalCount = 0;
//        }

//        //---------------------------------------------------------------------
//        public void startCopy(List<StreamingAssetsFileInfo> list_need_copyfile,
//            Action<int, int> action_pro, Action action_copydown)
//        {
//            ActionPro = action_pro;
//            ActionCopyDown = action_copydown;
//            foreach (var i in list_need_copyfile)
//            {
//                QueFileNames.Enqueue(i);
//            }
//            TotalCount = QueFileNames.Count;
//        }

//        //---------------------------------------------------------------------
//        public void update(float tm)
//        {
//            if (MapWWW.Count < MaxLoadCount && QueFileNames.Count > 0)
//            {
//                StreamingAssetsFileInfo data_file = QueFileNames.Dequeue();
//                WWW www = new WWW(data_file.datafile_streamingasset_path);
//                MapWWW[data_file] = www;
//                try
//                {
//                    CurrentDataFile = Path.GetFileName(data_file.datafile_streamingasset_path);
//                }
//                catch (Exception e)
//                {
//                    //UiHelperCasinos.UiShowPreLoading("CopyStreamingAssetsToPersistentDataPath GetFileName " + e.Message, 0);
//                    return;
//                }

//                CurrentIndex = TotalCount - QueFileNames.Count;
//                if (ActionPro != null)
//                {
//                    ActionPro(CurrentIndex, TotalCount);
//                }
//            }

//            if (MapWWW.Count == 0) return;

//            foreach (var i in MapWWW)
//            {
//                WWW www = i.Value;
//                if (www.isDone)
//                {
//                    if (string.IsNullOrEmpty(www.error))
//                    {
//                        MapWWWDone[i.Key] = www;
//                    }
//                    else
//                    {
//                        Debug.Log("Key  " + i.Key.datafile_streamingasset_path + "   Key2  " + i.Key.datafile_persistentdata_path + " url   " + www.url);
//                        //UiHelperCasinos.UiShowPreLoading("CopyStreamingAssetsToPersistentDataPath WWWError " + www.error + "  url " + www.url, 0);
//                        return;
//                    }
//                }
//            }

//            foreach (var i in MapWWWDone)
//            {
//                MapWWW.Remove(i.Key);
//                WWW www = i.Value;
//                if (www.isDone)
//                {
//                    try
//                    {
//                        string dir = Path.GetDirectoryName(i.Key.datafile_persistentdata_path);
//                        if (!Directory.Exists(dir))
//                        {
//                            Directory.CreateDirectory(dir);
//                        }

//                        using (MemoryStream ms = new MemoryStream(www.bytes))
//                        {
//                            using (FileStream fs = new FileStream(i.Key.datafile_persistentdata_path, FileMode.Create))
//                            {
//                                ms.WriteTo(fs);
//                            }
//                        }
//                    }
//                    catch (Exception e)
//                    {
//                        //UiHelperCasinos.UiShowPreLoading("CopyStreamingAssetsToPersistentDataPath WriteFileError " + e.Message, 0);
//                        return;
//                    }
//                }
//            }
//            MapWWWDone.Clear();

//            if (MapWWW.Count == 0 && QueFileNames.Count == 0)
//            {
//                if (ActionCopyDown != null)
//                {
//                    ActionCopyDown();
//                }
//            }
//        }
//    }
//}