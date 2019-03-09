// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public class ResourceMgr
    {
        //---------------------------------------------------------------------
        Dictionary<string, AssetBundle> MapAssetBundle { get; set; } = new Dictionary<string, AssetBundle>();// Key=Ab的完整路径

        //---------------------------------------------------------------------
        // 是否加载过指定名字的AssetBundle
        public AssetBundle QueryAssetBundle(string ab_fullname)
        {
            MapAssetBundle.TryGetValue(ab_fullname, out AssetBundle ab);
            //if (ab == null)
            //{
            //    string s = string.Format("ResourceMgr.QueryAssetBundle() 查询结果为不存在！ AssetBundleName={0}", ab_fullname);
            //    Debug.Log(s);
            //}
            return ab;
        }

        //---------------------------------------------------------------------
        // 确认加载过指定名字的AssetBundle
        public void AddAssetBundle(string ab_fullname, AssetBundle ab)
        {
            MapAssetBundle[ab_fullname] = ab;
        }
    }
}
