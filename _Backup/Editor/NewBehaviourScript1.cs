using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class FindReference : EditorWindow
{

    static public FindReference instance;
    Vector2 mScroll = Vector2.zero;
    public Dictionary<string, List<string>> dict;

    void OnEnable() { instance = this; }
    void OnDisable() { instance = null; }

    void OnGUI()
    {

        if (dict == null)
        {
            return;
        }
        mScroll = GUILayout.BeginScrollView(mScroll);

        List<string> list = dict["prefab"];
        if (list != null && list.Count > 0)
        {
            //if (NGUIEditorTools.DrawHeader("Prefab"))
            {
                foreach (string item in list)
                {
                    GameObject go = AssetDatabase.LoadAssetAtPath(item, typeof(GameObject)) as GameObject;
                    EditorGUILayout.ObjectField("Prefab", go, typeof(GameObject), false);

                }
            }
            list = null;
        }
        
        list = dict["cs"];
        if (list != null && list.Count > 0)
        {
            //if (NGUIEditorTools.DrawHeader("Script"))
            {
                foreach (string item in list)
                {
                    MonoScript go = AssetDatabase.LoadAssetAtPath(item, typeof(MonoScript)) as MonoScript;
                    EditorGUILayout.ObjectField("Script", go, typeof(MonoScript), false);

                }
            }
            list = null;
        }

        GUILayout.EndScrollView();
        //NGUIEditorTools.DrawList("Objects", list.ToArray(), "");
    }

    /// <summary>
    /// 根据脚本查找引用的对象
    /// </summary>
    [MenuItem("Assets/Wiker/Find Script Reference", false, 0)]
    static public void FindScriptReference()
    {
        //EditorWindow.GetWindow<UIAtlasMaker>(false, "Atlas Maker", true).Show();
        //Debug.Log("Selected Transform is on " + Selection.activeObject.name + ".");
        //foreach(string guid in Selection.assetGUIDs){

        // Debug.Log("GUID " + guid);

        //}
        ShowProgress(0, 0, 0);
        string curPathName = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());

        Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
        List<string> prefabList = new List<string>();
        List<string> fbxList = new List<string>();
        List<string> scriptList = new List<string>();
        List<string> textureList = new List<string>();
        List<string> matList = new List<string>();
        List<string> shaderList = new List<string>();
        List<string> fontList = new List<string>();
        List<string> levelList = new List<string>();

        string[] allGuids = AssetDatabase.FindAssets("t:Prefab t:Scene", new string[] { "Assets" });
        int i = 0;
        foreach (string guid in allGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            string[] names = AssetDatabase.GetDependencies(new string[] { assetPath });  //依赖的东东
            foreach (string name in names)
            {
                if (name.Equals(curPathName))
                {
                    //Debug.Log("Refer:" + assetPath);
                    if (assetPath.EndsWith(".prefab"))
                    {
                        prefabList.Add(assetPath);
                        break;
                    }
                    else if (assetPath.ToLower().EndsWith(".fbx"))
                    {
                        fbxList.Add(assetPath);
                        break;
                    }
                    else if (assetPath.ToLower().EndsWith(".unity"))
                    {
                        levelList.Add(assetPath);
                        break;
                    }
                    else if (assetPath.EndsWith(".cs"))
                    {
                        scriptList.Add(assetPath);
                        break;
                    }
                    else if (assetPath.EndsWith(".png"))
                    {
                        textureList.Add(assetPath);
                        break;
                    }
                    else if (assetPath.EndsWith(".mat"))
                    {
                        matList.Add(assetPath);
                        break;
                    }
                    else if (assetPath.EndsWith(".shader"))
                    {
                        shaderList.Add(assetPath);
                        break;
                    }
                    else if (assetPath.EndsWith(".ttf"))
                    {
                        fontList.Add(assetPath);
                        break;
                    }
                }
            }
            ShowProgress((float)i / (float)allGuids.Length, allGuids.Length, i);
            i++;
        }

        dic.Add("prefab", prefabList);
        dic.Add("fbx", fbxList);
        dic.Add("cs", scriptList);
        dic.Add("texture", textureList);
        dic.Add("mat", matList);
        dic.Add("shader", shaderList);
        dic.Add("font", fontList);
        dic.Add("level", levelList);
        dic.Add("anim", null);
        dic.Add("animTor", null);
        EditorUtility.ClearProgressBar();
        EditorWindow.GetWindow<FindReference>(false, "Object Reference", true).Show();

        //foreach (KeyValuePair<string, BetterList<string>> d in dic)
        //{
        // foreach (string s in d.Value)
        // {
        // Debug.Log(d.Key + "=" + s);
        // }
        //}

        if (FindReference.instance.dict != null) FindReference.instance.dict.Clear();
        FindReference.instance.dict = dic;

        //string[] path = new string[1];
        //path[0] = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
        //string[] names = AssetDatabase.GetDependencies(path); //依赖的东东
        //foreach (string name in names)
        //{
        // Debug.Log("Name:"+name);
        //}


    }

    public static void ShowProgress(float val, int total, int cur)
    {
        EditorUtility.DisplayProgressBar("Searching", string.Format("Finding ({0}/{1}), please wait...", cur, total), val);
    }

    /// <summary>
    /// 查找对象引用的类型
    /// </summary>
    [MenuItem("Assets/Wiker/Find Object Dependencies", false, 0)]
    public static void FindObjectDependencies()
    {

        ShowProgress(0, 0, 0);
        Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
        List<string> prefabList = new List<string>();
        List<string> fbxList = new List<string>();
        List<string> scriptList = new List<string>();
        List<string> textureList = new List<string>();
        List<string> matList = new List<string>();
        List<string> shaderList = new List<string>();
        List<string> fontList = new List<string>();
        List<string> animList = new List<string>();
        List<string> animTorList = new List<string>();
        string curPathName = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
        string[] names = AssetDatabase.GetDependencies(new string[] { curPathName });  //依赖的东东
        int i = 0;
        foreach (string name in names)
        {
            if (name.EndsWith(".prefab"))
            {
                prefabList.Add(name);
            }
            else if (name.ToLower().EndsWith(".fbx"))
            {
                fbxList.Add(name);
            }
            else if (name.EndsWith(".cs"))
            {
                scriptList.Add(name);
            }
            else if (name.EndsWith(".png"))
            {
                textureList.Add(name);
            }
            else if (name.EndsWith(".mat"))
            {
                matList.Add(name);
            }
            else if (name.EndsWith(".shader"))
            {
                shaderList.Add(name);
            }
            else if (name.EndsWith(".ttf"))
            {
                fontList.Add(name);
            }
            else if (name.EndsWith(".anim"))
            {
                animList.Add(name);
            }
            else if (name.EndsWith(".controller"))
            {
                animTorList.Add(name);
            }
            Debug.Log("Dependence:" + name);
            ShowProgress((float)i / (float)names.Length, names.Length, i);
            i++;
        }

        dic.Add("prefab", prefabList);
        dic.Add("fbx", fbxList);
        dic.Add("cs", scriptList);
        dic.Add("texture", textureList);
        dic.Add("mat", matList);
        dic.Add("shader", shaderList);
        dic.Add("font", fontList);
        dic.Add("level", null);
        dic.Add("animTor", animTorList);
        dic.Add("anim", animList);
        //deps.Sort(Compare);
        EditorWindow.GetWindow<FindReference>(false, "Object Dependencies", true).Show();
        if (FindReference.instance.dict != null) FindReference.instance.dict.Clear();
        FindReference.instance.dict = dic;
        EditorUtility.ClearProgressBar();
    }

}