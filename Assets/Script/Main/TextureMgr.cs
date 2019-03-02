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
        public void Destroy()
        {
        }

        //---------------------------------------------------------------------
        public void LoadTextureAsync(string name, string path, Action<Texture> call_back)
        {
            MapTexture.TryGetValue(name, out Texture texture);

            if (texture == null)
            {
                string s = string.Format("TextureMgr.getTexture() path={0} name={1}", path, name);
                Debug.Log(s);

                CasinosContext.Instance.LuaMgr.LocalLoadTextureFromAbAsync(path, name, call_back);
            }
            else
            {
                call_back(texture);
            }
        }

        //---------------------------------------------------------------------
        public bool ExistTexture(string name)
        {
            return MapTexture.ContainsKey(name);
        }

        //---------------------------------------------------------------------
        public void DestroyTexture(string name, bool destory_asset)
        {
            MapTexture.TryGetValue(name, out Texture texture);
            if (texture != null)
            {
                GameObject.DestroyImmediate(texture, destory_asset);
            }
        }
    }
}