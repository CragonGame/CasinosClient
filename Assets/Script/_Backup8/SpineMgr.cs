namespace Casinos
{
    using System.Collections.Generic;
    using UnityEngine;
    using Spine.Unity;

    public class SpineMgr
    {
        //---------------------------------------------------------------------
        Dictionary<string, AssetBundle> MapAssetBundleCache = new Dictionary<string, AssetBundle>();

        //---------------------------------------------------------------------
        public SpineMgr()
        {
        }

        //---------------------------------------------------------------------
        public void Destroy()
        {
        }

        //---------------------------------------------------------------------
        public SkeletonAnimation CreateSpineObjFromAb(string ab_path_prefix, string ab_name,
            string atlas_name, string texture_name, string json_name, string shader_name)
        {
            string s = ab_name.ToLower();
            AssetBundle ab = null;
            MapAssetBundleCache.TryGetValue(s, out ab);
            if (ab == null)
            {
                string spine_path = ab_path_prefix + s + ".ab";
                ab = AssetBundle.LoadFromFile(spine_path);
                MapAssetBundleCache[s] = ab;
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