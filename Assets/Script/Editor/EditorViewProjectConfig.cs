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
        if (EditorContext.Instance == null)
        {
            new EditorContext();
        }

        GUILayout.Space(10);
        GUILayout.Label("Casinos项目配置");

        GUILayout.Space(10);
        GUILayout.Label("---------------------------------");
        EditorGUILayout.LabelField("运行时使用的资源目录");
        bool force_use_resourceslaunch = EditorGUILayout.Toggle("是否强制使用Resources/Launch（不勾选表示使用PersistentData）",
            EditorContext.Instance.Config.CfgUserSettings.ForceUseDirResourcesLaunch, GUILayout.ExpandWidth(false));
        if (force_use_resourceslaunch != EditorContext.Instance.Config.CfgUserSettings.ForceUseDirResourcesLaunch)
        {
            EditorContext.Instance.Config.SaveValueForceUseDirResourcesLaunch(force_use_resourceslaunch);
        }

        bool force_use_dataoss = EditorGUILayout.Toggle("是否强制使用DataOss（不勾选表示使用PersistentData）",
            EditorContext.Instance.Config.CfgUserSettings.ForceUseDirDataOss, GUILayout.Width(600));
        if (force_use_dataoss != EditorContext.Instance.Config.CfgUserSettings.ForceUseDirDataOss)
        {
            EditorContext.Instance.Config.SaveValueForceUseDirDataOss(force_use_dataoss);
        }

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("关闭", GUILayout.Width(200)))
        {
            Close();
        }
        EditorGUILayout.EndHorizontal();
    }
}