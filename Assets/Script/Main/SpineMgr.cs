namespace Casinos
{
    using UnityEngine;
    using Spine.Unity;

    public class SpineMgr
    {
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
            string spine_path = ab_path_prefix + ab_name.ToLower() + ".ab";
            AssetBundle spine_ab = AssetBundle.LoadFromFile(spine_path);
            var atlas = spine_ab.LoadAsset<TextAsset>(atlas_name);
            var texture = spine_ab.LoadAsset<Texture>(texture_name);
            var json = spine_ab.LoadAsset<TextAsset>(json_name);

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

//var atlas = spine_ab.LoadAsset<TextAsset>(atlas_name);
//self.AbLoadingMarry = p_helper:GetPreSpine("LoadingMarry")
//local atlas = self.AbLoadingMarry:LoadAsset("Mary_Loading.atlas")
//local texture = self.AbLoadingMarry:LoadAsset("Mary_Loading")
//local json = self.AbLoadingMarry:LoadAsset("Mary_LoadingJson")
//self.PlayerAnim = CS.Casinos.SpineHelper.CreateSpineGameObject(atlas, texture, json, "Spine/Skeleton")

//self.PlayerAnim.transform.localScale = CS.Casinos.LuaHelper.GetVector3(70, 70, 1000)
//self.PlayerAnim:Initialize(false)
//self.PlayerAnim.loop = true
//self.PlayerAnim.AnimationName = "animation"
//self.PlayerAnim.transform.gameObject.name = "LoadingMote"
//self.MoteRender = self.PlayerAnim.transform.gameObject:GetComponent("MeshRenderer")
//self.MoteRender.sortingOrder = 4
//self.HolderMote = self.ComUi:GetChild("HolderMote").asGraph
//self.HolderMote:SetNativeObject(CS.FairyGUI.GoWrapper(self.PlayerAnim.transform.gameObject))