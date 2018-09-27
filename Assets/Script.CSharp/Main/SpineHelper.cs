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
            Shader shader = Shader.Find(shaderName);

            Material[] materials = new Material[1];
            materials[0] = new Material(shader);
            materials[0].mainTexture = (Texture)png;

            AtlasAsset atlasdata = ScriptableObject.CreateInstance<AtlasAsset>();
            atlasdata.atlasFile = (TextAsset)atlas;
            atlasdata.materials = materials;

            AtlasAsset[] atlasdata1 = new AtlasAsset[1];
            atlasdata1[0] = atlasdata;

            SkeletonDataAsset playerData = ScriptableObject.CreateInstance<SkeletonDataAsset>();
            playerData.atlasAssets = atlasdata1;
            playerData.skeletonJSON = (TextAsset)json;

            GameObject go = new GameObject();
            SkeletonAnimation playerAnim = go.AddComponent<SkeletonAnimation>();
            playerAnim.skeletonDataAsset = playerData;

            return playerAnim;
        }
    }
}