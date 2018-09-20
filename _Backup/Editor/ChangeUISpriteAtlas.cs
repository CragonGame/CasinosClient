using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChangeUISpriteAtlas : EditorWindow
{
    //-------------------------------------------------------------------------
    static GameObject NeedReplaceObj;
    //static UIAtlas NewAtlas;

    //-------------------------------------------------------------------------
    [MenuItem("SelfTool/ChangeUISpriteAtlas")]
    static void AddWindow()
    {
        EditorWindow.GetWindow<ChangeUISpriteAtlas>();
    }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        //ComponentSelector.Draw<GameObject>("Prefab", NeedReplaceObj, prefabChanged, true, GUILayout.MinWidth(80f));
        //ComponentSelector.Draw<UIAtlas>("NewAtlas", NewAtlas, newAtlasChanged, true, GUILayout.MinWidth(80f));
        bool check_and_replace = GUILayout.Button("查找替换", GUILayout.Width(200));
        if (check_and_replace)
        {
            _checkAndReplaceAtlas();
        }
    }

    //-------------------------------------------------------------------------
    void _checkAndReplaceAtlas()
    {
        //if (NeedReplaceObj != null && NewAtlas != null)
        //{
        //    UISprite[] sprites = NeedReplaceObj.GetComponentsInChildren<UISprite>();
        //    foreach (var i in sprites)
        //    {
        //        Debug.Log("i   " + i.gameObject.name);
        //        i.atlas = NewAtlas;
        //    }

        //    EditorUtility.SetDirty(NeedReplaceObj);
        //}
        //else
        //{
        //    Debug.LogError("NeedReplaceObj or NewAtlas is Null!");
        //}
    }

    //-------------------------------------------------------------------------
    void prefabChanged(Object obj)
    {
        NeedReplaceObj = (GameObject)obj;
    }

    //-------------------------------------------------------------------------
    void newAtlasChanged(Object obj)
    {
        //NewAtlas = (UIAtlas)obj;
    }
}
