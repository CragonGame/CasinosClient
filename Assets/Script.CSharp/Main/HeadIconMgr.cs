// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class HeadIconMgr
    {
        //----------------------------------------------------------------------
        Dictionary<string, Texture> MapHeadIconResources { get; set; }
        Dictionary<string, string> MapLoadingIcon { get; set; }

        const string mDefaultIcon = "playershadow";
        const string mDefaultIconStr = "profile_";
        const string mSmallIcon = "thumbnail_";
        const string mLargeIcon = "image_";
        //public const string mDefaultIconLocalPath = "Ui/Texture/PlayerShadow";

        //----------------------------------------------------------------------  
        public static HeadIconMgr Instant { get; private set; }

        //----------------------------------------------------------------------
        public HeadIconMgr()
        {
            Instant = this;
            MapHeadIconResources = new Dictionary<string, Texture>();
            MapLoadingIcon = new Dictionary<string, string>();
        }

        //----------------------------------------------------------------------
        public LoaderTicket asyncLoadIcon(string id, string resource_path,
            string resource_name, GameObject head_icon,
            Action<UnityEngine.Object, LoaderTicket> load_callback = null)
        {
            LoaderTicket loader_ticket = null;
            Texture head_resource = null;
            if (MapHeadIconResources.TryGetValue(id, out head_resource))
            {
                if (head_icon != null)
                {
                    UiHelper.setActiveState(true, head_icon);
                    //UiHelper.setTexture(head_icon, head_resource);
                }

                if (load_callback != null)
                {
                    load_callback(head_resource, loader_ticket);
                }
            }
            else
            {
                if (MapLoadingIcon.ContainsKey(id))
                {
                    return loader_ticket;
                }

                MapLoadingIcon[id] = id;
                loader_ticket = CasinosContext.Instance.AsyncAssetLoadGroup.asyncLoadAsset(
                    resource_path, resource_name, _eAsyncAssetLoadType.WWWRawAsset,
                    (LoaderTicket ticket, string path, UnityEngine.Object obj) =>
                    {
                        MapLoadingIcon.Remove(id);
                        if (obj != null)
                        {
                            MapHeadIconResources[id] = (Texture)obj;
                            if (head_icon != null)
                            {
                                UiHelper.setActiveState(true, head_icon);
                                //UiHelper.setTexture(head_icon, (Texture)obj);
                            }
                        }

                        if (load_callback != null)
                        {
                            load_callback(obj, loader_ticket);
                        }
                    });
            }

            return loader_ticket;
        }

        //----------------------------------------------------------------------
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

        //----------------------------------------------------------------------
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

        //----------------------------------------------------------------------
        public void destroy()
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