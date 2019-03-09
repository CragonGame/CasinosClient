// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using UnityEngine;

    public class VersionInfoRemote
    {
        public string BundleSelectPro;
        public string BundleSelectDev;
        public string LaunchSelectPro;
        public string LaunchSelectDev;
        public string CommonSelectPro;
        public string CommonSelectDev;
        public string DataSelectPro;
        public string DataSelectDev;
    }

    public class ControllerLaunch : Controller
    {
        //---------------------------------------------------------------------
        VersionInfoRemote VersionInfoRemote { get; set; }

        //---------------------------------------------------------------------
        public override void Create()
        {
            Debug.Log("ControllerLaunch.Create()");

            ListenEvent<EventControllerLaunch>();
        }

        //---------------------------------------------------------------------
        public override void Destory()
        {
            UnListenAllEvent();

            Debug.Log("ControllerLaunch.Destory()");
        }

        //---------------------------------------------------------------------
        public override void HandleEvent(Event ev)
        {
            if (ev is EventControllerLaunch)
            {
                EventControllerLaunch evt = (EventControllerLaunch)ev;
                Debug.Log("Event.Info=" + evt.Info);
            }
        }

        //---------------------------------------------------------------------
        public void Launch()
        {
            // 先创建ViewLaunch
            ViewLaunch view_launch = ViewMgr.CreateView<ViewLaunch>();

            string tips = "正在努力检测更新，请耐心等待...";
            //local lan = self.LaunchCfg.CurrentLan
            //if (lan == "English") then
            //tips = "Try to loading the config,please wait..."
            //else
            //if (lan == "Chinese" or lan == "ChineseSimplified") then
            //tips = "正在努力加载配置，请耐心等待..."
            view_launch.UpdateDesc(tips);

            Casinos.MbAsyncLoadAssets async_loader = Context.Instance.MbAsyncLoadAssets;
            Config cfg = Context.Instance.Config;

            // 下载并加载bundle_xxx.txt
            string str_rd = UnityEngine.Random.Range(100000, 999999).ToString();
            string bundle_cfg_remote = string.Format("Bundle_{0}", cfg.BundleVersion);
            string bundle_cfg_remote_url = string.Format("https://cragon-king-oss.cragon.cn/{0}/{1}.txt?{2}",
                Context.Instance.Config.Platform.ToUpper(), bundle_cfg_remote, str_rd);
            Debug.Log(bundle_cfg_remote_url);
            async_loader.WWWLoadTextAsync(bundle_cfg_remote_url, _onDownloadBundleCfg);
        }

        //---------------------------------------------------------------------
        void _onDownloadBundleCfg(string text)
        {
            var ev = GenEvent<EventControllerLaunch>();
            ev.Info = "asdasdfasfd";
            ev.Broadcast();

            VersionInfoRemote = LitJson.JsonMapper.ToObject<VersionInfoRemote>(text);
            Debug.Log("VersionInfoRemote.LaunchSelectDev=" + VersionInfoRemote.LaunchSelectDev);
            Debug.Log("VersionInfoRemote.LaunchSelectPro=" + VersionInfoRemote.LaunchSelectPro);
            Debug.Log("VersionInfoRemote.CommonSelectDev=" + VersionInfoRemote.CommonSelectDev);
            Debug.Log("VersionInfoRemote.CommonSelectPro=" + VersionInfoRemote.CommonSelectPro);
            Debug.Log("VersionInfoRemote.DataSelectDev=" + VersionInfoRemote.DataSelectDev);
            Debug.Log("VersionInfoRemote.DataSelectPro=" + VersionInfoRemote.DataSelectPro);
        }
    }
}
