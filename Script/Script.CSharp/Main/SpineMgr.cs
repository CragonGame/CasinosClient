// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using Spine.Unity;

    public class SpineMgr
    {
        //---------------------------------------------------------------------
        public SpineMgr()
        {
        }

        //---------------------------------------------------------------------
        public SkeletonAnimation CreateSpineObjFromAb(string ab_path_prefix, string ab_name,
            string atlas_name, string texture_name, string json_name, string shader_name)
        {
            var res_mgr = Context.Instance.ResourceMgr;

            string s = ab_name.ToLower();
            string ab_fullname = ab_path_prefix + s + ".ab";

            AssetBundle ab = res_mgr.QueryAssetBundle(ab_fullname);
            if (ab == null)
            {
                ab = AssetBundle.LoadFromFile(ab_fullname);
                res_mgr.AddAssetBundle(ab_fullname, ab);
            }

            var atlas = ab.LoadAsset<TextAsset>(atlas_name);
            var texture = ab.LoadAsset<Texture>(texture_name);
            var json = ab.LoadAsset<TextAsset>(json_name);

            return _createSpineGameObject(atlas, texture, json, shader_name);
        }

        //---------------------------------------------------------------------
        public SkeletonAnimation CreateSpineObjFromRes(string res_prefix,
            string atlas_name, string texture_name, string json_name, string shader_name)
        {
            var atlas = Resources.Load<TextAsset>(res_prefix + atlas_name);
            var texture = Resources.Load<Texture>(res_prefix + texture_name);
            var json = Resources.Load<TextAsset>(res_prefix + json_name);

            return _createSpineGameObject(atlas, texture, json, shader_name);
        }

        //---------------------------------------------------------------------
        SkeletonAnimation _createSpineGameObject(TextAsset atlas, Texture png, TextAsset json, string shader_name)
        {
            Shader shader = Shader.Find(shader_name);

            Material[] materials = new Material[1];
            materials[0] = new Material(shader);
            materials[0].mainTexture = png;

            AtlasAsset atlasdata = ScriptableObject.CreateInstance<AtlasAsset>();
            atlasdata.atlasFile = atlas;
            atlasdata.materials = materials;

            AtlasAsset[] atlasdata1 = new AtlasAsset[1];
            atlasdata1[0] = atlasdata;

            SkeletonDataAsset skeleton_dataasset = ScriptableObject.CreateInstance<SkeletonDataAsset>();
            skeleton_dataasset.atlasAssets = atlasdata1;
            skeleton_dataasset.skeletonJSON = json;

            GameObject go = new GameObject();
            SkeletonAnimation skeleton_anim = go.AddComponent<SkeletonAnimation>();
            skeleton_anim.skeletonDataAsset = skeleton_dataasset;

            return skeleton_anim;
        }
    }
}
