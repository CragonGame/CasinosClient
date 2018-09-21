// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    // 将StreamingAssets中的指定目录拷贝到PersistentData的相同目录中
    public class CopyStreamingAssetsToPersistentData
    {
        //---------------------------------------------------------------------   

        //---------------------------------------------------------------------
        public CopyStreamingAssetsToPersistentData(string copy_dir)
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
            List<string> list_copyfile = new List<string>();

            FileInfo[] file_list = dir.GetFiles("*.*", SearchOption.AllDirectories);
            foreach (FileInfo i in file_list)
            {
                if (i.Name.EndsWith(".meta")) continue;

                var s = i.FullName.ToLower();
                var s1 = s.Replace('/', '\\');
                var s2 = s1.Replace(sa_path2, "");

                list_copyfile.Add(s2);
            }

            foreach (var i in list_copyfile)
            {
                var ss = CasinosContext.Instance.PathMgr.combineWWWStreamingAssetsPath(i);
                WWW www = new WWW(ss);
                while (!www.isDone) { }

                var str = CasinosContext.Instance.PathMgr.combinePersistentDataPath(i);
                string d = Path.GetDirectoryName(str);
                if (!Directory.Exists(d))
                {
                    Directory.CreateDirectory(d);
                }

                using (FileStream fs = new FileStream(str, FileMode.Create))
                {
                    fs.Write(www.bytes, 0, www.bytes.Length);
                }
            }
        }
    }
}