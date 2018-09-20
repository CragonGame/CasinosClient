using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class EditorViewProjectConfig : EditorWindow
{
    //-------------------------------------------------------------------------
    void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("Casinos项目配置");

        GUILayout.Space(20);
        GUILayout.Label("---------------------------------");
        EditorGUILayout.LabelField("运行时使用的资源目录:", EditorContext.Instance.PathAssets);
        bool use_tmpdir_res = EditorGUILayout.Toggle("运行时是否使用临时目录中的资源（不勾选表示使用本地目录中的资源）",
            EditorContext.Instance.Config.CfgUserSettings.UseTmpDirRes);
        if (use_tmpdir_res != EditorContext.Instance.Config.CfgUserSettings.UseTmpDirRes)
        {
            EditorContext.Instance.Config.SaveValueUseTmpDirRes(use_tmpdir_res);
        }

        GUILayout.Space(10);
        EditorGUILayout.LabelField("DataVersion:", "1.01.113");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("++", GUILayout.Width(200)))
        {
        }
        if (GUILayout.Button("--", GUILayout.Width(200)))
        {
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("关闭", GUILayout.Width(200)))
        {
            Close();
        }
        EditorGUILayout.EndHorizontal();
    }
}