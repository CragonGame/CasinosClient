// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    // 将StreamingAssets中的指定目录拷贝到PersistentData的相同目录中，异步
    public class CopyStreamingAssetsToPersistentData2
    {
        //---------------------------------------------------------------------
        public int TotalCount { get; private set; }// 需要Copy的文件总数
        public int LeftCount { get; private set; }// 剩余未Copy的文件总数
        Queue<string> QueCopyFile { get; set; } = new Queue<string>(4096);
        Dictionary<string, WWW> MapWWW { get; set; } = new Dictionary<string, WWW>();
        List<string> ListFinished { get; set; } = new List<string>(5);

        //---------------------------------------------------------------------
        public void CopyAsync(string copy_dir)
        {
            string pd_path = CasinosContext.Instance.PathMgr.getPersistentDataPath();
            var pd_path1 = pd_path.ToLower();
            var pd_path2 = pd_path1.Replace('/', '\\');

            // 遍历copy_dir中的所有文件
            string sa_path = CasinosContext.Instance.PathMgr.getStreamingAssetsPath();
            var sa_path1 = sa_path.ToLower();
            var sa_path2 = sa_path1.Replace('/', '\\');
            var p = CasinosContext.Instance.PathMgr.combineStreamingAssetsPath(copy_dir);

            DirectoryInfo dir = new DirectoryInfo(p);
            QueCopyFile.Clear();

            FileInfo[] file_list = dir.GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo i in file_list)
            {
                if (i.Name.EndsWith(".meta")) continue;

                var s = i.FullName.ToLower();
                var s1 = s.Replace('/', '\\');
                var s2 = s1.Replace(sa_path2, "");

                QueCopyFile.Enqueue(s2);
            }

            TotalCount = QueCopyFile.Count;
            LeftCount = QueCopyFile.Count;
        }

        //---------------------------------------------------------------------
        public bool IsDone()
        {
            while (QueCopyFile.Count > 0 && MapWWW.Count < 5)
            {
                string s1 = QueCopyFile.Dequeue();
                var s2 = CasinosContext.Instance.PathMgr.combineWWWStreamingAssetsPath(s1);
                MapWWW[s1] = new WWW(s2);
            }

            foreach (var i in MapWWW)
            {
                if (!i.Value.isDone) continue;
                ListFinished.Add(i.Key);

                var str = CasinosContext.Instance.PathMgr.combinePersistentDataPath(i.Key);
                string d = Path.GetDirectoryName(str);
                if (!Directory.Exists(d))
                {
                    Directory.CreateDirectory(d);
                }

                using (FileStream fs = new FileStream(str, FileMode.Create))
                {
                    fs.Write(i.Value.bytes, 0, i.Value.bytes.Length);
                }
            }

            foreach (var i in ListFinished)
            {
                MapWWW.Remove(i);
            }
            ListFinished.Clear();

            LeftCount = QueCopyFile.Count;

            return QueCopyFile.Count > 0 || MapWWW.Count > 0 ? false : true;
        }
    }
}