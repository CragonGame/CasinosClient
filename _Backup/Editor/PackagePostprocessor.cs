using UnityEngine;
using UnityEditor;
using System.Collections;

public class PackagePostprocessor : AssetPostprocessor
{

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        //foreach (var str in importedAssets)
        //{
        //    Debug.Log("Reimported Asset: " + str);
        //}
        //foreach (var str in deletedAssets)
        //{
        //    Debug.Log("Deleted Asset: " + str);
        //}

        //for (var i = 0; i < movedAssets.Length; i++)
        //{
        //    Debug.Log("Moved Asset: " + movedAssets[i] + " from: " + movedFromAssetPaths[i]);
        //}
    }
}
