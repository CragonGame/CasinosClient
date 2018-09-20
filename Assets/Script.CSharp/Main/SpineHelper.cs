namespace Casinos
{
    using UnityEngine;
    using Spine.Unity;
    using XLua;

    [LuaCallCSharp]
    public class SpineHelper
    {
        //---------------------------------------------------------------------
        public static SkeletonAnimation LoadResourcesPrefab(object atlas, object png, object json, string shaderName)
        {
            SkeletonAnimation playerAnim;
            SkeletonDataAsset playerData;
            AtlasAsset[] atlasdata1 = new AtlasAsset[1];
            AtlasAsset atlasdata;
            atlasdata = ScriptableObject.CreateInstance<AtlasAsset>();
            playerData = ScriptableObject.CreateInstance<SkeletonDataAsset>();

            atlasdata.atlasFile = (TextAsset)atlas;

            Material[] materials = new Material[1];
            materials[0] = new Material(Shader.Find(shaderName));
            materials[0].mainTexture = (Texture)png;
            atlasdata.materials = materials;

            atlasdata1[0] = atlasdata;
            playerData.atlasAssets = atlasdata1;
            playerData.skeletonJSON = (TextAsset)json;

            GameObject go = new GameObject();

            playerAnim = go.AddComponent<SkeletonAnimation>();
            playerAnim.skeletonDataAsset = playerData;
            //var m_Mr = go.GetComponent<MeshRenderer>();
            return playerAnim;
        }
    }
}