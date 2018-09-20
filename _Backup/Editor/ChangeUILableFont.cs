using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ChangeUILableFont : EditorWindow
{
    //-------------------------------------------------------------------------
    static string NeedReplacePath;
    static GameObject NeedReplaceObj;
    static Font NewFont;

    //-------------------------------------------------------------------------
    [MenuItem("SelfTool/ChangeUILableFont")]
    static void AddWindow()
    {
        EditorWindow.GetWindow<ChangeUILableFont>();
        NeedReplacePath = System.Environment.CurrentDirectory + "/Assets/Resources/Ui/Prefab/";
    }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        NeedReplacePath = EditorGUILayout.TextField("替换目标文件夹路径:", NeedReplacePath);
        //ComponentSelector.Draw<GameObject>("NeedReplaceObj", NeedReplaceObj, newNeedReplaceObjChanged, true, GUILayout.MinWidth(80f));
        //ComponentSelector.Draw<Font>("NewFont", NewFont, newFontChanged, true, GUILayout.MinWidth(80f));
        bool check_and_replace = GUILayout.Button("查找替换", GUILayout.Width(200));
        if (check_and_replace)
        {
            _checkAndReplaceFont();
        }
    }

    //-------------------------------------------------------------------------
    void _checkAndReplaceFont()
    {
        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        List<Object> list_prefab = new List<Object>();
        foreach (var i in selection)
        {
            string asset_path = AssetDatabase.GetAssetPath(i);
            if (Path.GetExtension(asset_path) != ".prefab")
            {
                continue;
            }

            list_prefab.Add(i);
        }

        if (NewFont != null)
        {
            foreach (var i in list_prefab)
            {
                _replaceFont(i);
            }
        }

        //if (Directory.Exists(NeedReplacePath))
        //{
        //    string[] files = Directory.GetFiles(NeedReplacePath, "*.prefab", SearchOption.AllDirectories);

        //    foreach (var i in files)
        //    {
        //        Debug.LogError(i);
        //    }
        //}

        //if (NeedReplaceObj != null && NewFont != null)
        //{
        //    UILabel[] lables = NeedReplaceObj.GetComponentsInChildren<UILabel>();
        //    foreach (var i in lables)
        //    {
        //        Debug.Log("i   " + i.gameObject.name);
        //        i.trueTypeFont = NewFont;
        //    }
        //}
        //else
        //{
        //    Debug.LogError("NeedReplaceObj or NewAtlas is Null!");
        //}
    }

    //-------------------------------------------------------------------------
    void _replaceFont(Object obj)
    {
        GameObject need_replaceobj = (GameObject)obj;
        //UILabel[] lables = need_replaceobj.GetComponentsInChildren<UILabel>();
        //foreach (var i in lables)
        //{
        //    Debug.Log("i   " + i.gameObject.name);
        //    i.trueTypeFont = NewFont;
        //}

        EditorUtility.SetDirty(obj);
    }

    //-------------------------------------------------------------------------
    void newFontChanged(Object obj)
    {
        NewFont = (Font)obj;
    }

    //-------------------------------------------------------------------------
    void newNeedReplaceObjChanged(Object obj)
    {
        NeedReplaceObj = (GameObject)obj;
    }
}
