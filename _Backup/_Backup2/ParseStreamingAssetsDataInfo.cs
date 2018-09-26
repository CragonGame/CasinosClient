//// Copyright (c) Cragon. All rights reserved.

//namespace Casinos
//{
//    using System;
//    using System.Collections.Generic;
//    using System.IO;
//    using UnityEngine;

//    public struct StreamingAssetsFileInfo
//    {
//        public string datafile_streamingasset_path;
//        public string datafile_persistentdata_path;
//    }

//    public class ParseStreamingAssetsDataInfo
//    {
//        //---------------------------------------------------------------------      
//        public List<StreamingAssetsFileInfo> ListPreData { get; private set; }
//        public List<StreamingAssetsFileInfo> ListData { get; private set; }

//        WWW WWWLoadDataFileList;
//        byte[] DataFileListTextBytes;
//        Action ParseDown;
//        bool IsParseDown;

//        //---------------------------------------------------------------------
//        public ParseStreamingAssetsDataInfo(Action parse_down)
//        {
//            ParseDown = parse_down;
//            IsParseDown = false;
//            ListPreData = new List<StreamingAssetsFileInfo>();
//            ListData = new List<StreamingAssetsFileInfo>();
//        }

//        //---------------------------------------------------------------------
//        public bool startPaseData()
//        {
//            if (IsParseDown)
//            {
//                return IsParseDown;
//            }

//            try
//            {
//                var datafilelist_path = CasinosContext.Instance.PathMgr.combineWWWStreamingAssetsPath("DataFileList.txt");
//                WWWLoadDataFileList = new WWW(datafilelist_path);
//            }
//            catch (Exception e)
//            {
//                //UiHelperCasinos.UiShowPreLoading("ParseStreamingAssetsDataInfo Init " + e.Message, 0);
//            }

//            return false;
//        }

//        //---------------------------------------------------------------------
//        public void writeStreamingAssetsDataFileList2Persistent()
//        {
//            if (DataFileListTextBytes == null || DataFileListTextBytes.Length == 0)
//            {
//                return;
//            }

//            try
//            {
//                using (FileStream fs = new FileStream(CasinosContext.Instance.PathMgr.combinePersistentDataPath("DataFileList.txt"),
//                     FileMode.Create))
//                {
//                    fs.Write(DataFileListTextBytes, 0, DataFileListTextBytes.Length);
//                }
//            }
//            catch (Exception e)
//            {
//                //UiHelperCasinos.UiShowPreLoading("ParseStreamingAssetsDataInfo WriteDataFileList " + e.Message, 0);
//                return;
//            }
//        }

//        //---------------------------------------------------------------------
//        public void update(float tm)
//        {
//            if (WWWLoadDataFileList != null)
//            {
//                if (WWWLoadDataFileList.isDone)
//                {
//                    if (!string.IsNullOrEmpty(WWWLoadDataFileList.error))
//                    {
//                        //UiHelperCasinos.UiShowPreLoading("ParseStreamingAssetsDataInfo WWWLoadDataFileList " + WWWLoadDataFileList.error, 0);
//                        return;
//                    }

//                    DataFileListTextBytes = WWWLoadDataFileList.bytes;

//                    // 读取本地DataFileList.txt文件成功
//                    _parseDataFileList(WWWLoadDataFileList.text);

//                    WWWLoadDataFileList = null;
//                    IsParseDown = true;
//                    if (ParseDown != null)
//                    {
//                        ParseDown();
//                        ParseDown = null;
//                    }
//                }
//            }
//        }

//        //---------------------------------------------------------------------
//        void _parseDataFileList(string data_filelist_text)
//        {
//            try
//            {
//                string[] infos = data_filelist_text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
//                foreach (var i in infos)
//                {
//                    if (string.IsNullOrEmpty(i))
//                    {
//                        continue;
//                    }

//                    string[] info = i.Split(' ');
//                    if (info.Length != 2)
//                    {
//                        //FloatMsgInfo msg_info;
//                        //msg_info.msg = "本地资源文件损坏,请修复文件!";
//                        //msg_info.color = Color.red;
//                        //UiMgr.Instance.FloatMsgMgr.createFloatMsg(msg_info);
//                    }
//                    else
//                    {
//                        StreamingAssetsFileInfo file_info;
//                        file_info.datafile_persistentdata_path = CasinosContext.Instance.PathMgr.combinePersistentDataPath(info[0]);
//                        file_info.datafile_streamingasset_path = CasinosContext.Instance.PathMgr.combineWWWStreamingAssetsPath(info[0]);
                        
//                        if (!i.Contains("PreData"))
//                        {
//                            ListData.Add(file_info);
//                        }
//                        else
//                        {
//                            ListPreData.Add(file_info);
//                        }
//                    }
//                }
//            }
//            catch (Exception e)
//            {
//                //UiHelperCasinos.UiShowPreLoading("ParseStreamingAssetsDataInfo _parseDataFileList " + e.Message, 0);
//                return;
//            }
//        }
//    }
//}