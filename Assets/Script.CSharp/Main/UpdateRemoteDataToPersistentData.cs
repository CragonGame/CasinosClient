// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    // 将Oss中的Data差异集更新到PersistentData的相同目录中，异步
    public class UpdateRemoteToPersistentData
    {
        //---------------------------------------------------------------------
        public int TotalCount { get; private set; }// 需要Update的文件总数
        public int LeftCount { get; private set; }// 剩余未Update的文件总数
        Queue<string> QueUpdateFile { get; set; } = new Queue<string>(4096);
        Dictionary<string, WWW> MapWWW { get; set; } = new Dictionary<string, WWW>();
        List<string> ListFinished { get; set; } = new List<string>(5);
        string RemoteDataRootUrl { get; set; }

        //---------------------------------------------------------------------
        public int UpateAsync(string datafilelist_remote, string datafilelist_persistent, string remotedata_root_url)
        {
            RemoteDataRootUrl = remotedata_root_url;

            Dictionary<string, string> map_remote = new Dictionary<string, string>();
            Dictionary<string, string> map_persistent = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(datafilelist_remote))
            {
                string[] arr_remote = datafilelist_remote.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var i in arr_remote)
                {
                    var arr = i.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    map_remote[arr[0]] = arr[1];
                }
            }

            if (!string.IsNullOrEmpty(datafilelist_persistent))
            {
                string[] arr_persistent = datafilelist_persistent.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var i in arr_persistent)
                {
                    var arr = i.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    map_persistent[arr[0]] = arr[1];
                }
            }

            foreach (var i in map_remote)
            {
                string value_persistent = string.Empty;
                if (map_persistent.TryGetValue(i.Key, out value_persistent))
                {
                    if (i.Value != value_persistent) QueUpdateFile.Enqueue(i.Key);
                }
                else
                {
                    QueUpdateFile.Enqueue(i.Key);
                }
            }

            TotalCount = QueUpdateFile.Count;
            LeftCount = QueUpdateFile.Count;

            return QueUpdateFile.Count;
        }

        //---------------------------------------------------------------------
        public bool IsDone()
        {
            while (QueUpdateFile.Count > 0 && MapWWW.Count < 5)
            {
                string s1 = QueUpdateFile.Dequeue();
                var s2 = Path.Combine(RemoteDataRootUrl, s1);
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

                i.Value.Dispose();
            }

            foreach (var i in ListFinished)
            {
                MapWWW.Remove(i);
            }
            ListFinished.Clear();

            LeftCount = QueUpdateFile.Count;

            return QueUpdateFile.Count > 0 || MapWWW.Count > 0 ? false : true;
        }
    }
}