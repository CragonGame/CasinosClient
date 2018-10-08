//// Copyright (c) Cragon. All rights reserved.

//namespace Casinos
//{
//    using System;
//    using System.Collections;
//    using System.Collections.Generic;
//    using System.IO;
//    using UnityEngine;

//    // 将StreamingAssets中的指定目录拷贝到PersistentData的相同目录中，同步
//    public class CopyStreamingAssetsToPersistentData1
//    {
//        //---------------------------------------------------------------------
//        public void CopySync(List<string> list_copyfile)
//        {
//            List<WWW> list_www = new List<WWW>(list_copyfile.Count);
//            foreach (var i in list_copyfile)
//            {
//                var path_www_streamingassets_file = CasinosContext.Instance.PathMgr.combineWWWStreamingAssetsPath(i);

//                WWW www = new WWW(path_www_streamingassets_file);
//                list_www.Add(www);
//            }

//            bool all_done = false;
//            while (!all_done)
//            {
//                bool has_undone = false;
//                for (int i = 0; i < list_www.Count; i++)
//                {
//                    WWW www = list_www[i];
//                    if (www == null) continue;

//                    if (www.isDone)
//                    {
//                        var str = CasinosContext.Instance.PathMgr.combinePersistentDataPath(list_copyfile[i]);
//                        string d = Path.GetDirectoryName(str);
//                        if (!Directory.Exists(d))
//                        {
//                            Directory.CreateDirectory(d);
//                        }

//                        using (FileStream fs = new FileStream(str, FileMode.Create))
//                        {
//                            fs.Write(www.bytes, 0, www.bytes.Length);
//                        }

//                        list_www[i] = null;
//                        www.Dispose();
//                    }
//                    else
//                    {
//                        has_undone = true;
//                    }
//                }
//                if (!has_undone) all_done = true;
//            }
//        }
//    }
//}