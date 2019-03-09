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
        public EventMgr EventMgr { get; private set; }
        public ControllerMgr ControllerMgr { get; private set; }
        Dictionary<string, ViewFactory> MapViewFactory { get; set; } = new Dictionary<string, ViewFactory>();
        Dictionary<string, View> MapView { get; set; } = new Dictionary<string, View>();
        GameObject GoGRoot { get; set; }
        StringBuilder Sb { get; set; }

        const int STANDARD_WIDTH = 1066;// UI设计宽度
        const int STANDARD_HEIGHT = 640;// UI设计高度

        //-------------------------------------------------------------------------
        public ViewMgr()
        {
            Sb = Context.Instance.Sb;
        }

        //-------------------------------------------------------------------------
        public void RegViewFactory(ViewFactory factory)
        {
            MapViewFactory[factory.GetName()] = factory;
        }

        //-------------------------------------------------------------------------
        public void Create()
        {
            EventMgr = Context.Instance.EventMgr;
            ControllerMgr = Context.Instance.ControllerMgr;

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

            //Debug.Log("ViewName=" + name);

            string ab_dir = factory.GetAbUiDir();

            List<string> list_ab_dependencies = factory.GetAbUiDependencies();
            if (list_ab_dependencies != null)
            {
                foreach (var i in list_ab_dependencies)
                {
                    AddUiPackage(ab_dir, i);
                }
            }

            AddUiPackage(ab_dir, factory.GetAbUiAndPackageName());

            var go = new GameObject();
            go.name = name;
            var layer = LayerMask.NameToLayer(FairyGUI.StageCamera.LayerName);
            go.layer = layer;

            FairyGUI.UIPanel ui_panel = (FairyGUI.UIPanel)go.AddComponent(typeof(FairyGUI.UIPanel));
            ui_panel.packageName = factory.GetAbUiAndPackageName();
            ui_panel.componentName = factory.GetComponentName();
            ui_panel.fitScreen = FairyGUI.FitScreen.None;// view_factory.FitScreen
            ui_panel.ApplyModifiedProperties(true, true);

            var view = factory.CreateView();
            view.EventMgr = EventMgr;
            view.ControllerMgr = ControllerMgr;
            view.GCom = ui_panel.ui;
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
        // 添加UIPackage。ab_filename不带路径，不带后缀名。
        public void AddUiPackage(string ab_dir, string ab_filename)
        {
            var res_mgr = Context.Instance.ResourceMgr;

            Sb.Clear();
            Sb.Append(ab_dir);
            Sb.Append(ab_filename.ToLower());
            Sb.Append(".ab");
            string path_ab = Sb.ToString();

            AssetBundle ab = res_mgr.QueryAssetBundle(path_ab);
            if (ab == null)
            {
                ab = AssetBundle.LoadFromFile(path_ab);
                FairyGUI.UIPackage.AddPackage(ab);
                res_mgr.AddAssetBundle(path_ab, ab);
            }
        }

        //---------------------------------------------------------------------
        public void ListenEvent<T>(EventListener listener) where T : Event
        {
            EventMgr.ListenEvent<T>(listener);
        }

        //---------------------------------------------------------------------
        public void UnListenAllEvent(EventListener listener)
        {
            EventMgr.UnListenAllEvent(listener);
        }

        //---------------------------------------------------------------------
        public T GenEvent<T>() where T : Event, new()
        {
            return EventMgr.GenEvent<T>();
        }
    }
}
