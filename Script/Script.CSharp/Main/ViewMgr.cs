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

        const int STANDARD_WIDTH = 1066;
        const int STANDARD_HEIGHT = 640;

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

        //function AddUiPackage(ab_name)
        //    local ab = self.ModelMgr:QueryAssetBundle(ab_name)
        //    if ab == nil then
        //        local path_ab = CasinosContext.PathMgr.DirAbUi.. string.lower(ab_name) .. ".ab"
        //        ab = AssetBundle.LoadFromFile(path_ab)
        //        FairyGUI.UIPackage.AddPackage(ab)
        //        self.ModelMgr:AddAssetBundle(ab_name, ab)
        //    end
        //end

        //-------------------------------------------------------------------------
        public T CreateView<T>() where T : View
        {
            var name = typeof(T).Name;
            MapViewFactory.TryGetValue(name, out ViewFactory factory);
            if (factory == null)
            {
                Debug.LogError("CsViewMgr.CreateView() 指定ViewName=" + name + "没有注册！");
                return null;
            }

            Debug.Log("ViewName=" + name);

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
            var name = nameof(T);
            MapViewFactory.TryGetValue(name, out ViewFactory factory);
            if (factory == null)
            {
                Debug.LogError("CsViewMgr.Destroy() 指定ViewName=" + name + "没有注册！");
                return;
            }

            MapView.TryGetValue(name, out View view);

            if (view == null)
            {
                return;
            }

            view.Destory();
            MapView.Remove(name);
        }
    }
}
