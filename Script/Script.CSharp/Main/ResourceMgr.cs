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
        Dictionary<string, AssetBundle> MapAssetBundle { get; set; } = new Dictionary<string, AssetBundle>();

        //---------------------------------------------------------------------
        // 是否加载过指定名字的AssetBundle
        public AssetBundle QueryAssetBundle(string ab_name)
        {
            MapAssetBundle.TryGetValue(ab_name, out AssetBundle ab);
            if (ab == null)
            {
                string s = string.Format("ResourceMgr.QueryAssetBundle()失败！ AssetBundleName={0}", ab_name);
                Debug.LogError(s);
            }
            return ab;
        }


        //---------------------------------------------------------------------
        // 确认加载过指定名字的AssetBundle
        public void AddAssetBundle(string ab_name, AssetBundle ab)
        {
            MapAssetBundle[ab_name] = ab;
        }
    }
}
