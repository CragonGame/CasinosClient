// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    // 头像加载的全部是远程图片裸资源
    public class HeadIconMgr
    {
        //---------------------------------------------------------------------
        public static HeadIconMgr Instant { get; private set; }
        Dictionary<string, Texture> MapHeadIconResources { get; set; }

        const string mDefaultIcon = "playershadow";
        const string mDefaultIconStr = "profile_";
        const string mSmallIcon = "thumbnail_";
        const string mLargeIcon = "image_";

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
                //string s = string.Format("HeadIconMgr.LoadIconAsync() path={0} name={1}", resource_path, resource_name);
                //Debug.Log(s);

                CasinosContext.Instance.LuaMgr.WWWLoadTextureAsync(resource_path, load_callback);
            }
        }

        //---------------------------------------------------------------------
        public static string getIconName(bool is_small, string icon_name, ref string icon_resource_name)
        {
            string icon_str = mSmallIcon;
            if (!is_small)
            {
                icon_str = mLargeIcon;
            }
            CasinosContext.Instance.ClearSB();

            var sb = CasinosContext.Instance.SB;
            sb.Append(mDefaultIconStr);
            sb.Append(icon_str);
            sb.Append(icon_name);
            icon_resource_name = sb.ToString();

            CasinosContext.Instance.ClearSB();
            sb.Append(icon_resource_name);
            sb.Append(".jpg");
            icon_name = CasinoHelper.FormalUrlWithRandomVersion(sb.ToString());

            return icon_name;
        }

        //---------------------------------------------------------------------
        public void destroyCurrentResources()
        {
            foreach (var i in MapHeadIconResources)
            {
                if (i.Value != null)
                {
                    GameObject.DestroyImmediate(i.Value, true);
                }
            }

            MapHeadIconResources.Clear();
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
