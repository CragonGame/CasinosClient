// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System.Collections.Generic;
    using UnityEngine;
    using FairyGUI;
    using Spine.Unity;

    public class ViewLaunch : View
    {
        //---------------------------------------------------------------------
        GTextField GTextTips { get; set; }
        GProgressBar GProgress { get; set; }

        //---------------------------------------------------------------------
        public override void Create()
        {
            Debug.Log("ViewLaunch.Create()");

            GProgress = GCom.GetChild("Progress").asProgress;
            GProgress.max = 100;
            GProgress.value = 0;

            GTextTips = GCom.GetChild("Tips").asTextField;

            var com_bg = GCom.GetChild("ComBg").asCom;
            var image_mote = com_bg.GetChild("ImageMote").asImage;
            image_mote.visible = false;

            // 隐藏图片模特
            var spine_mgr = Context.Instance.SpineMgr;
            var path_mgr = Context.Instance.PathMgr;
            string ab_path_prefix = path_mgr.DirLaunchAb;

            // 加载Spine，灯笼和Marry
            SkeletonAnimation spine_loadingmarry = spine_mgr.CreateSpineObjFromAb(
                ab_path_prefix, "LoadingMarry", "Mary_Loading.atlas", "Mary_Loading", "Mary_LoadingJson", "Spine/Skeleton");
            spine_loadingmarry.transform.localScale = new Vector3(70, 70, 1000);
            spine_loadingmarry.Initialize(false);
            spine_loadingmarry.loop = true;
            spine_loadingmarry.AnimationName = "animation";
            spine_loadingmarry.transform.gameObject.name = "LoadingMote";
            MeshRenderer loadingmarry_render = (MeshRenderer)spine_loadingmarry.transform.gameObject.GetComponent("MeshRenderer");
            loadingmarry_render.sortingOrder = 4;

            var loadingmarry_holder = GCom.GetChild("HolderMote").asGraph;
            loadingmarry_holder.SetNativeObject(new GoWrapper(spine_loadingmarry.transform.gameObject));

            SkeletonAnimation spine_denglong = spine_mgr.CreateSpineObjFromAb(
                ab_path_prefix, "DengLong", "denglong.atlas", "denglong", "denglongJson", "Spine/Skeleton");

            var denglong_parent = GCom.GetChild("DengLongParent").asCom;
            spine_denglong.transform.parent = denglong_parent.displayObject.gameObject.transform;
            spine_denglong.transform.localPosition = new Vector3(-10, -90, -318);
            spine_denglong.transform.localScale = new Vector3(90, 90, 90);
            spine_denglong.transform.gameObject.layer = denglong_parent.displayObject.gameObject.layer;
            spine_denglong.Initialize(false);
            spine_denglong.loop = true;
            spine_denglong.transform.gameObject.name = "DengLong";
            spine_denglong.AnimationName = "animation";
            MeshRenderer denglong_render = (MeshRenderer)spine_denglong.transform.gameObject.GetComponent("MeshRenderer");
            denglong_render.sortingOrder = 4;

            // 显示左上角显示版本信息
            RefreshVersionInfo();
        }

        //---------------------------------------------------------------------
        public override void Destory()
        {
            Debug.Log("ViewLaunch.Destory()");
        }

        //---------------------------------------------------------------------
        public override void HandleEvent(Event ev)
        {
            Debug.Log("ViewLaunch.HandleEvent()");
        }

        //---------------------------------------------------------------------
        // 显示左上角显示版本信息
        public void RefreshVersionInfo()
        {
            var version_text = GCom.GetChild("Version").asTextField;

            var cfg = Context.Instance.Config;
            string app_version_label = "应用版本";
            string launch_version_label = "脚本版本";
            string data_version_label = "数据版本";

            //local lan = CurrentLan
            //if (lan == "English") then
            //app_version = "AppVersion"
            //data_versionex = "DataVersion"
            //else
            //if (lan == "Chinese" or lan == "ChineseSimplified") then
            //app_version = "应用版本"
            //data_versionex = "数据版本"

            version_text.text = string.Format("{0}: {1},  {2}: {3},  {4}: {5}&{6} {7}",
                app_version_label, cfg.BundleVersion, launch_version_label, cfg.LaunchVersion,
                data_version_label, cfg.CommonVersion, cfg.DataVersion, cfg.Env);
        }

        //---------------------------------------------------------------------
        // 更新进度条上方提示文字
        public void UpdateDesc(string desc)
        {
            GTextTips.text = desc;
        }

        //---------------------------------------------------------------------
        // 更新进度条
        public void UpdateLoadingProgress(int value, int max)
        {
            GProgress.visible = true;
            GProgress.value = value;
            GProgress.max = max;
        }
    }

    public class ViewFactoryLaunch : ViewFactory
    {
        //-------------------------------------------------------------------------
        public override string GetName()
        {
            return "ViewLaunch";
        }

        //-------------------------------------------------------------------------
        public override string GetAbUiDir()
        {
            return Context.Instance.PathMgr.DirLaunchAb;
        }

        //-------------------------------------------------------------------------
        public override List<string> GetAbUiDependencies()
        {
            //return new List<string>() { "PreMsgbox" };
            return null;
        }

        //-------------------------------------------------------------------------
        public override string GetAbUiAndPackageName()
        {
            return "PreLoading";
        }

        //-------------------------------------------------------------------------
        public override string GetComponentName()
        {
            return "PreLoading";
        }

        //-------------------------------------------------------------------------
        public override View CreateView()
        {
            return new ViewLaunch();
        }
    }
}
