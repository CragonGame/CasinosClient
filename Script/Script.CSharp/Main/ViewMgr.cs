// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public class ViewMgr
    {
        //-------------------------------------------------------------------------
        Dictionary<string, ViewFactory> MapViewFactory { get; set; } = new Dictionary<string, ViewFactory>();
        Dictionary<string, View> MapView { get; set; } = new Dictionary<string, View>();
        GameObject GoGRoot { get; set; }
        StringBuilder Sb { get; set; } = new StringBuilder(256);

        const int STANDARD_WIDTH = 1066;// UI设计宽度
        const int STANDARD_HEIGHT = 640;// UI设计高度

        //-------------------------------------------------------------------------
        public void RegViewFactory(ViewFactory factory)
        {
            MapViewFactory[factory.GetName()] = factory;
        }

        //-------------------------------------------------------------------------
        public void Create()
        {
            FairyGUI.GRoot.inst.SetContentScaleFactor(STANDARD_WIDTH, STANDARD_HEIGHT, FairyGUI.UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
            FairyGUI.UIConfig.defaultFont = "FontXi";

            string auto_rotation = string.Empty;
            if (PlayerPrefs.HasKey("ScreenAutoRotation"))
            {
                auto_rotation = PlayerPrefs.GetString("ScreenAutoRotation");
            }

            if (auto_rotation == "true")
            {
                Screen.orientation = ScreenOrientation.AutoRotation;
                Screen.autorotateToLandscapeRight = true;
                Screen.autorotateToLandscapeLeft = true;
                Screen.autorotateToPortraitUpsideDown = false;
                Screen.autorotateToPortrait = false;
            }

            GoGRoot = GameObject.Find("Stage/GRoot");
        }

        //-------------------------------------------------------------------------
        public void Destroy()
        {
        }

        //-------------------------------------------------------------------------
        public T CreateView<T>() where T : View
        {
            var name = typeof(T).Name;
            MapViewFactory.TryGetValue(name, out ViewFactory factory);
            if (factory == null)
            {
                string s = string.Format("ViewMgr.CreateView()失败！ 指定ViewName={0}没有注册！", name);
                Debug.LogError(s);
                return null;
            }

            Debug.Log("ViewName=" + name);

            string ab_name = "";
            bool r = AddUiPackage(ab_name);
            if (!r)
            {
                string s = string.Format("ViewMgr.CreateView()失败！ViewName={0}, AssetBundleName={1}", name, ab_name);
                Debug.LogError(s);
                return null;
            }

            var go = new GameObject();
            go.name = name;
            var layer = LayerMask.NameToLayer(FairyGUI.StageCamera.LayerName);
            go.layer = layer;
            go.transform.parent = GoGRoot.transform;

            FairyGUI.UIPanel ui_panel = (FairyGUI.UIPanel)go.AddComponent(typeof(FairyGUI.UIPanel));
            ui_panel.packageName = "";// view_factory.PackageName
            ui_panel.componentName = "";// view_factory.ComponentName
            ui_panel.fitScreen = FairyGUI.FitScreen.None;// view_factory.FitScreen
            ui_panel.ApplyModifiedProperties(true, true);

            var view = factory.CreateView();
            view.Create();

            return (T)view;
        }

        //-------------------------------------------------------------------------
        public void Destroy<T>() where T : View
        {
            var name = typeof(T).Name;
            MapView.TryGetValue(name, out View view);

            if (view == null)
            {
                return;
            }

            view.Destory();
            MapView.Remove(name);
        }

        //-------------------------------------------------------------------------
        // 添加UIPackage。ab_name不带路径，不带后缀名。
        public bool AddUiPackage(string ab_name)
        {
            var res_mgr = Context.Instance.ResourceMgr;
            var path_mgr = Context.Instance.PathMgr;

            AssetBundle ab = res_mgr.QueryAssetBundle(ab_name);
            if (ab == null) return false;

            string path_ab = path_mgr.DirAbUi + ab_name.ToLower() + ".ab";
            ab = AssetBundle.LoadFromFile(path_ab);
            FairyGUI.UIPackage.AddPackage(ab);
            res_mgr.AddAssetBundle(ab_name, ab);

            return true;
        }
    }
}
