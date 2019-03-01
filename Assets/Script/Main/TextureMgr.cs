// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class TextureMgr
    {
        //---------------------------------------------------------------------
        public Dictionary<string, Texture> MapTexture { get; private set; }
        public static TextureMgr Instant { get; private set; }

        //---------------------------------------------------------------------
        public TextureMgr()
        {
            Instant = this;
            MapTexture = new Dictionary<string, Texture>();
        }

        //---------------------------------------------------------------------
        public void destroy()
        {
        }

        //---------------------------------------------------------------------
        public void getTexture(string name, string path, Action<Texture> call_back)
        {
            Texture texture = null;
            MapTexture.TryGetValue(name, out texture);

            //LoaderTicket tick = null;
            if (texture == null)
            {
                string s = string.Format("TextureMgr.getTexture() path={0} name={1}", path, name);
                Debug.Log(s);
                CasinosContext.Instance.LuaMgr.WWWLoadTextureAsync(path + name, call_back);

                //tick = CasinosContext.Instance.AsyncAssetLoadGroup.asyncLoadAsset(
                //    path, name, _eAsyncAssetLoadType.LocalBundleAsset,
                //    (LoaderTicket ticket, string path_ex, UnityEngine.Object obj) =>
                //    {
                //        texture = (Texture)obj;
                //        MapTexture[name] = texture;
                //        call_back(ticket, texture);
                //    });

                //AssetBundle
                //CasinosContext.Instance.LuaMgr.
            }
            else
            {
                call_back(texture);
            }

            //return tick;
        }

        //---------------------------------------------------------------------
        public bool haveTexture(string name)
        {
            return MapTexture.ContainsKey(name);
        }

        //---------------------------------------------------------------------
        public void destroyTexture(string name, bool destory_asset)
        {
            Texture texture = null;
            MapTexture.TryGetValue(name, out texture);

            if (texture != null)
            {
                GameObject.DestroyImmediate(texture, destory_asset);
            }
        }
    }
}