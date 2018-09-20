using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using GameCloud.Unity.Common;

public class UiWndAutoPatcher : EditorWindow
{
    //-------------------------------------------------------------------------
    string SceneName { get; set; }
    int PopupIndex { get; set; }

    //-------------------------------------------------------------------------
    void OnGUI()
    {
        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("asdfasdfasdf", EditorStyles.boldLabel);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        {
            string[] list_txt = new string[] { "aaaaa", "bbbbbbbb", "ccccccc", "dddddd", "eeeeeee", "fffff" };
            PopupIndex = EditorGUI.Popup(GUILayoutUtility.GetRect(100, 16), "可选列表", PopupIndex, list_txt);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        {
            SceneName = EditorGUI.TextField(GUILayoutUtility.GetRect(100, 16), "Export Scene Name", SceneName);
        }
        GUILayout.EndHorizontal();

        //GUILayout.Space(10f);
        //GUILayout.BeginHorizontal();
        //{
        //    string[] list_txt = new string[] { "aaaaa", "bbbbbbbb", "ccccccc","dddddd","eeeeeee","fffff" };
        //    GUILayout.SelectionGrid(0, list_txt, 3, GUILayout.Width(300f));
        //}
        //GUILayout.EndHorizontal();

        GUILayout.Space(10f);
        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Export", "LargeButton", GUILayout.Width(150f)))
            {
                _onBtnExport(SceneName);
            }

            if (GUILayout.Button("Export2", "LargeButton", GUILayout.Width(150f)))
            {
                _onBtnExport(SceneName);
            }
        }
        GUILayout.EndHorizontal();
    }

    //-------------------------------------------------------------------------
    void _onBtnExport(string scene_name)
    {
        EbLog.Note("----------------------------------");
        EbLog.Note("Begin Export Entity Scene");
        EbLog.Note("End Export Entity Scene");
        EbLog.Note("----------------------------------");
    }
}