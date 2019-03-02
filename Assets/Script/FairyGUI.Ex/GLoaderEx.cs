// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using UnityEngine;
    using FairyGUI;

    public class GLoaderEx : GLoader
    {
        //---------------------------------------------------------------------
        public Action<bool> LoaderDoneCallBack { get; set; }

        internal static string HttpPrefix = "http://";
        internal static string HttpsPrefix = "https://";
        internal static string ABPostfix = ".ab";

        //---------------------------------------------------------------------
        protected override void LoadExternal()
        {
            Texture2D tex = null;
            string url = this.url.Replace('\\', '/');
            int index = url.LastIndexOf('/');
            string resource_name = url.Substring(index + 1);
            if (this.url.StartsWith(HttpPrefix) || this.url.StartsWith(HttpsPrefix))
            {
                HeadIconMgr.Instant.LoadIconAsync(resource_name, this.url, resource_name, null, _wwwCallBack);
            }
            else
            {
                if (this.url.EndsWith(ABPostfix))
                {
                    TextureMgr.Instant.LoadTextureAsync(resource_name.Replace(ABPostfix, ""), this.url, _loadTextureCallBackEx);
                }
                else
                {
                    tex = Resources.Load<Texture2D>(this.url);
                    _loadTextureCallBack(tex);
                }
            }
        }

        //---------------------------------------------------------------------
        void _loadTextureCallBackEx(Texture t)
        {
            _loadTextureCallBack(t);
        }

        //---------------------------------------------------------------------
        void _loadTextureCallBack(Texture t)
        {
            if (this.displayObject.gameObject == null)
            {
                return;
            }

            bool load_success = false;
            if (t != null)
            {
                load_success = true;
                onExternalLoadSuccess(new NTexture(t));
            }
            else
            {
                onExternalLoadFailed();
            }

            if (LoaderDoneCallBack != null)
            {
                LoaderDoneCallBack(load_success);
            }
        }

        //---------------------------------------------------------------------
        void _wwwCallBack(Texture obj)
        {
            if (this.displayObject.gameObject == null)
            {
                return;
            }

            bool load_success = false;
            Texture2D t = (Texture2D)obj;
            if (t != null)
            {
                load_success = true;
                onExternalLoadSuccess(new NTexture(t));
            }
            else
            {
                onExternalLoadFailed();
            }

            if (LoaderDoneCallBack != null)
            {
                LoaderDoneCallBack(load_success);
            }
        }
    }
}