// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using UnityEngine;

    // 头像加载的全部是远程图片裸资源
    public class HeadIconMgr
    {
        //---------------------------------------------------------------------
        public static HeadIconMgr Instant { get; private set; }
        Dictionary<string, Texture> MapHeadIconResources { get; set; }
        MbAsyncLoadAssets MbAsyncLoadAssets { get; set; }

        //const string mDefaultIcon = "playershadow";
        //const string mDefaultIconStr = "profile_";
        //const string mSmallIcon = "thumbnail_";
        //const string mLargeIcon = "image_";

        //---------------------------------------------------------------------
        public HeadIconMgr()
        {
            Instant = this;
            MapHeadIconResources = new Dictionary<string, Texture>();
        }

        //---------------------------------------------------------------------
        public void LoadIconAsync(string id, string resource_path, string resource_name, GameObject head_icon, Action<Texture> load_callback = null)
        {
            if (MapHeadIconResources.TryGetValue(id, out Texture head_resource))
            {
                if (head_icon != null)
                {
                    head_icon.SetActive(true);
                }

                if (load_callback != null)
                {
                    load_callback(head_resource);
                }
            }
            else
            {
                if (MbAsyncLoadAssets == null)
                {
                    var go = GameObject.Find("Launch");
                    MbAsyncLoadAssets = go.GetComponent<MbAsyncLoadAssets>();
                }

                MbAsyncLoadAssets.WWWLoadTextureAsync(resource_path, load_callback);
            }
        }

        //---------------------------------------------------------------------
        public void Destroy()
        {
            foreach (var i in MapHeadIconResources)
            {
                if (i.Value != null)
                {
                    GameObject.DestroyImmediate(i.Value, true);
                }
            }

            MapHeadIconResources.Clear();
            MapHeadIconResources = null;
        }
    }
}
