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
        ViewLaunch ViewLaunch { get; set; }
        VersionInfoRemote VersionInfoRemote { get; set; }
        string BundleVersionRemote { get; set; } = string.Empty;
        string LaunchVersionRemote { get; set; } = string.Empty;
        string CommonVersionRemote { get; set; } = string.Empty;
        string DataVersionRemote { get; set; } = string.Empty;
        string[] LaunchStep { get; set; } = new string[4];
        Casinos.EbTimer TimerUpdate { get; set; }
        UpdateRemoteToPersistentData UpdateRemoteToPersistentData4Launch { get; set; }
        UpdateRemoteToPersistentData UpdateRemoteToPersistentData4CommonAndData { get; set; }
        string StrCache1 { get; set; }
        string StrCache2 { get; set; }

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

            // 销毁定时器
            if (TimerUpdate != null)
            {
                TimerUpdate.Close();
                TimerUpdate = null;
            }

            Debug.Log("ControllerLaunch.Destory()");
        }

        //---------------------------------------------------------------------
        public override void HandleEvent(Event ev)
        {
            if (ev is EventControllerLaunch evt)
            {
                Debug.Log("Event.Info=" + evt.Info);
            }
        }

        //---------------------------------------------------------------------
        public void Launch()
        {
            // 先创建ViewLaunch
            ViewLaunch = ViewMgr.CreateView<ViewLaunch>();

            string tips = "正在努力检测更新，请耐心等待...";
            //local lan = self.LaunchCfg.CurrentLan
            //if (lan == "English") then
            //tips = "Try to loading the config,please wait..."
            //else
            //if (lan == "Chinese" or lan == "ChineseSimplified") then
            //tips = "正在努力加载配置，请耐心等待..."
            ViewLaunch.UpdateDesc(tips);

            Casinos.MbAsyncLoadAssets async_loader = Context.Instance.MbAsyncLoadAssets;
            Config cfg = Context.Instance.Config;

            // 下载并加载bundle_xxx.txt
            string bundle_cfg_remote = string.Format("B_{0}", cfg.BundleVersion);
            //cragon-king-oss.cragon.cn
            //cragon-king.oss-cn-shanghai.aliyuncs.com
            string bundle_cfg_remote_url = string.Format("https://cragon-king.oss-cn-shanghai.aliyuncs.com/{0}/{1}.txt",
                Context.Instance.Config.Platform.ToUpper(), bundle_cfg_remote);
            Debug.Log(bundle_cfg_remote_url);
            async_loader.WWWLoadTextAsync(bundle_cfg_remote_url, _onDownloadBundleCfg);
        }

        //---------------------------------------------------------------------
        // Bundle_x.xx.xxx.txt下载完毕
        void _onDownloadBundleCfg(string text)
        {
            var ev = GenEvent<EventControllerLaunch>();
            ev.Info = "asdasdfasfd";
            ev.Broadcast();

            VersionInfoRemote = LitJson.JsonMapper.ToObject<VersionInfoRemote>(text);

            //Debug.Log("VersionInfoRemote.BundleSelectDev=" + VersionInfoRemote.BundleSelectDev);
            //Debug.Log("VersionInfoRemote.BundleSelectPro=" + VersionInfoRemote.BundleSelectPro);
            //Debug.Log("VersionInfoRemote.LaunchSelectDev=" + VersionInfoRemote.LaunchSelectDev);
            //Debug.Log("VersionInfoRemote.LaunchSelectPro=" + VersionInfoRemote.LaunchSelectPro);
            //Debug.Log("VersionInfoRemote.CommonSelectDev=" + VersionInfoRemote.CommonSelectDev);
            //Debug.Log("VersionInfoRemote.CommonSelectPro=" + VersionInfoRemote.CommonSelectPro);
            //Debug.Log("VersionInfoRemote.DataSelectDev=" + VersionInfoRemote.DataSelectDev);
            //Debug.Log("VersionInfoRemote.DataSelectPro=" + VersionInfoRemote.DataSelectPro);

            _initLaunchStep();
            _nextLaunchStep();
        }

        //---------------------------------------------------------------------
        // 初始化LaunchStep
        void _initLaunchStep()
        {
            Config cfg = Context.Instance.Config;
            if (cfg.Env == "Pro")
            {
                BundleVersionRemote = VersionInfoRemote.BundleSelectPro;
                LaunchVersionRemote = VersionInfoRemote.LaunchSelectPro;
                CommonVersionRemote = VersionInfoRemote.CommonSelectPro;
                DataVersionRemote = VersionInfoRemote.DataSelectPro;
            }
            else
            {
                BundleVersionRemote = VersionInfoRemote.BundleSelectDev;
                LaunchVersionRemote = VersionInfoRemote.LaunchSelectDev;
                CommonVersionRemote = VersionInfoRemote.CommonSelectDev;
                DataVersionRemote = VersionInfoRemote.DataSelectDev;
            }

            // 检测是否需要更新Bundle
            if (!string.IsNullOrEmpty(BundleVersionRemote) && cfg.BundleVersion != BundleVersionRemote)
            {
                LaunchStep[0] = "UpdateBundle";
                Debug.Log("需要更新Bundle");
            }

            // 检测是否需要更新Launch
            if (cfg.LaunchVersion != LaunchVersionRemote)
            {
                LaunchStep[1] = "UpdateLaunch";
                Debug.Log("需要更新Launch");
            }

            // 检测是否需要更新Common&Data
            if (cfg.CommonVersion != CommonVersionRemote || cfg.DataVersion != DataVersionRemote)
            {
                LaunchStep[2] = "UpdateCommon&Data";
                Debug.Log("需要更新Common&Data");
            }

            // 进入Login界面
            LaunchStep[3] = "ShowLogin";
        }

        //---------------------------------------------------------------------
        // 执行下一步LaunchStep
        void _nextLaunchStep()
        {
            //StrCache1 = string.Empty;
            //StrCache2 = string.Empty;

            // 销毁定时器
            //if (TimerUpdate != null)
            //{
            //    TimerUpdate.Close();
            //    TimerUpdate = null;
            //}

            //UpdateViewLoadingDescAndProgress("准备登录中", 0, 100);

            //ViewMgr.DestroyView<ViewLaunch>();
            //ControllerMgr.CreateController<ControllerUCenter>();
            //ControllerMgr.CreateController<ControllerLogin>();

            //return;

            Casinos.MbAsyncLoadAssets async_loader = Context.Instance.MbAsyncLoadAssets;
            Config cfg = Context.Instance.Config;

            // 更新Bundle
            if (!string.IsNullOrEmpty(LaunchStep[0]))
            {
                UpdateViewLoadingDescAndProgress("准备更新安装包", 0, 100);
                //if (self.CasinosContext.UnityAndroid == true)
                //{
                //// 弹框让玩家选择，更新Bundle Apk
                //var msg_info = string.Format('有新的安装包需要更新\n当前BundleVersion：{0}\n新的BundleVersion：{1}',
                //    self.CasinosContext.Config.VersionBundle, self.Cfg.BundleUpdateVersion);
                //var view_premsgbox = self.PreViewMgr:CreateView("PreMsgBox");
                //view_premsgbox.showMsgBox(msg_info,
                //     function()
                //         self.Launch:UpdateViewLoadingDescAndProgress("正在更新安装包", 0, 100)
                //         self.WWWUpdateBundleApk = CS.UnityEngine.UnityWebRequest(self.Cfg.BundleUpdateUrlANDROID)
                //         self.WWWUpdateBundleApk:SendWebRequest()
                //         self.TimerUpdateBundleApk = self.CasinosContext.TimerShaft:RegisterTimer(30, self, self._timerUpdateBundleApk)
                //         self.PreViewMgr:DestroyView(view_premsgbox)
                //     end,
                //     function()
                //         self.PreViewMgr:DestroyView(view_premsgbox)
                //         UnityEngine.Application.Quit()
                //     end
                //);
                //}
                //else if (self.CasinosContext.UnityIOS == true)
                //{ 
                //    // 弹框让玩家选择，更新Bundle Ipa
                //    local msg_info = string.Format("有新的安装包需要更新\n当前BundleVersion：{0}\n新的BundleVersion：{1}",
                //        self.CasinosContext.Config.VersionBundle, self.Cfg.BundleUpdateVersion);
                //    local view_premsgbox = PreViewMgr:CreateView("PreMsgBox");
                //    view_premsgbox.showMsgBox(msg_info,
                //        function()
                //            Launch:UpdateViewLoadingDescAndProgress("正在更新安装包", 0, 100);
                //            // TODO，调用打开ios下载链接api
                //            UnityEngine.Application.OpenURL(self.Cfg.BundleUpdateUrlIOS);
                //            PreViewMgr:DestroyView(view_premsgbox);
                //        end,
                //        function()
                //            PreViewMgr: DestroyView(view_premsgbox);
                //            UnityEngine.Application.Quit();
                //        end
                //    );
                //}

                return;
            }

            // 更新Launch
            if (!string.IsNullOrEmpty(LaunchStep[1]))
            {
                Debug.Log("准备更新Luanch");

                //if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                //{
                //}

                UpdateViewLoadingDescAndProgress("更新Launch...", 0, 100);
                string http_url = cfg.LaunchRootURL + LaunchVersionRemote + "/" + cfg.LaunchFileListFileName;
                Debug.Log(http_url);
                async_loader.WWWLoadTextAsync(http_url, _onDownloadLaunchFileList);

                return;
            }

            // 更新Common
            if (!string.IsNullOrEmpty(LaunchStep[2]))
            {
                Debug.Log("准备更新Common&Data");
                //    --if (CS.UnityEngine.Application.internetReachability == CS.UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork) then
                //    --end
                //    UpdateViewLoadingDescAndProgress("更新游戏脚本", 0, 100)
                //    string http_url = self.Cfg.CommonRootURL..self.Cfg.CommonFileListFileName
                //    --print(http_url)
                //    self.LuaMgr:WWWLoadTextAsync(http_url,
                //    function(text)
                //        -- 比较Oss上的CommonFileList.txt和Persistent中的CommonFileList.txt差异集，获取需要更新的列表
                //        string commonfilelist_persistent = self.CasinosContext.PathMgr:CombinePersistentDataPath(self.Cfg.CommonFileListFileName)
                //        --print(commonfilelist_persistent)
                //        self.RemoteCommonFileListContent = text
                //        string persistent_commonfilelist_content = self.LuaMgr:ReadAllText(commonfilelist_persistent)
                //        string commonrootdir_persistent = self.CasinosContext.PathMgr:CombinePersistentDataPath('/')
                //        self.UpdateRemoteCommonToPersistent = CS.Casinos.UpdateRemoteToPersistentData()
                //        self.UpdateRemoteCommonToPersistent:UpateAsync(self.RemoteCommonFileListContent, persistent_commonfilelist_content, self.Cfg.CommonRootURL, commonrootdir_persistent)
                //        self.TimerUpdateRemoteCommonToPersistent = self.CasinosContext.TimerShaft:RegisterTimer(30, self, self._timerUpdateRemoteCommonToPersistent)
                //    end
                //    );
                return;
            }

            // 更新Data
            if (!string.IsNullOrEmpty(LaunchStep[2]))
            {
                //if (UnityEngine.Application.internetReachability == UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork) then
                //end
                //    UpdateViewLoadingDescAndProgress("更新游戏数据", 0, 100)
                //    string http_url = self.Cfg.DataRootURL..self.Cfg.DataFileListFileName
                //    //print(http_url)
                //    self.LuaMgr:WWWLoadTextAsync(http_url,
                //        function(text)
                //            -- 比较Oss上的datafilelist.txt和Persistent中的datafilelist.txt差异集，获取需要更新的Data列表
                //            string datafilelist_persistent = PathMgr:CombinePersistentDataPath(self.Cfg.DataFileListFileName)
                //            --print(datafilelist_persistent)
                //            self.RemoteDataFileListContent = text
                //            string persistent_datafilelist_content = self.LuaMgr:ReadAllText(datafilelist_persistent)
                //            string datarootdir_persistent = self.CasinosContext.PathMgr:CombinePersistentDataPath('/')
                //            self.UpdateRemoteDataToPersistent = CS.Casinos.UpdateRemoteToPersistentData()
                //            self.UpdateRemoteDataToPersistent:UpateAsync(self.RemoteDataFileListContent, persistent_datafilelist_content, self.Cfg.DataRootURL, datarootdir_persistent)
                //            self.TimerUpdateRemoteDataToPersistent = self.CasinosContext.TimerShaft:RegisterTimer(30, self, self._timerUpdateRemoteDataToPersistent)
                //        end);
                return;
            }

            // 销毁ViewLaunch，加载并显示Login
            if (!string.IsNullOrEmpty(LaunchStep[3]))
            {
            }
        }

        //---------------------------------------------------------------------
        // LaunchFileList.txt下载完毕
        void _onDownloadLaunchFileList(string remote_launchfilelist_content)
        {
            StrCache1 = remote_launchfilelist_content;

            Debug.Log("Launch文件列表下载完毕");

            Casinos.MbAsyncLoadAssets async_loader = Context.Instance.MbAsyncLoadAssets;
            Config cfg = Context.Instance.Config;
            PathMgr path_mgr = Context.Instance.PathMgr;

            // 比较Oss上的LaunchFileList.txt和Persistent中的LaunchFileList.txt差异集，获取需要更新的列表
            string persistent_launchfilelist_content = string.Empty;
            string launchfilelist_persistent = path_mgr.DirLaunchRoot + cfg.LaunchFileListFileName;
            if (File.Exists(launchfilelist_persistent))
            {
                persistent_launchfilelist_content = File.ReadAllText(launchfilelist_persistent);
            }

            if (TimerUpdate == null)
            {
                TimerUpdate = Context.Instance.TimerShaft.RegisterTimer(30, _timerUpdate);
            }

            Debug.Log(cfg.LaunchRootURL + LaunchVersionRemote + "/");
            Debug.Log(path_mgr.DirLaunchRoot);

            UpdateRemoteToPersistentData4Launch = new UpdateRemoteToPersistentData();
            UpdateRemoteToPersistentData4Launch.UpateAsync(
                remote_launchfilelist_content,
                persistent_launchfilelist_content,
                cfg.LaunchRootURL + LaunchVersionRemote + "/",
                path_mgr.DirLaunchRoot);
        }

        //---------------------------------------------------------------------
        // 定时器
        void _timerUpdate(float tm)
        {
            if (UpdateRemoteToPersistentData4Launch != null)
            {
                if (UpdateRemoteToPersistentData4Launch.IsDone())
                {
                    UpdateRemoteToPersistentData4Launch = null;
                    UpdateViewLoadingProgress(100, 100);

                    Debug.Log("Launch更新完成！！！！！！！！！！！！！！！！！！！！");

                    // 用Remote LaunchFileList.txt中的内容覆盖Persistent中的；并更新VersionLaunchPersistent
                    var cfg = Context.Instance.Config;
                    var path_mgr = Context.Instance.PathMgr;
                    string launchfilelist_persistent = Context.Instance.PathMgr.DirLaunchRoot + cfg.LaunchFileListFileName;
                    File.WriteAllText(launchfilelist_persistent, StrCache1);
                    Context.Instance.Config.WriteVersionLaunchPersistent(LaunchVersionRemote);
                    StrCache1 = string.Empty;

                    // 重启AppDomain
                    Context.Instance.GoLaunch.AddComponent(typeof(Casinos.MbLaunchRestart));

                    // 执行下一步LaunchStep
                    //LaunchStep[2] = string.Empty;
                    //_nextLaunchStep();
                }
                else
                {
                    int value = UpdateRemoteToPersistentData4Launch.LeftCount;
                    int max = UpdateRemoteToPersistentData4Launch.TotalCount;
                    UpdateViewLoadingProgress(max - value, max);
                }
            }
        }

        //---------------------------------------------------------------------
        // 更新加载界面进度条进度
        void UpdateViewLoadingProgress(int cur, int max)
        {
            ViewLaunch.UpdateLoadingProgress(cur, max);
        }

        //---------------------------------------------------------------------
        // 更新加载界面进度条描述
        void UpdateViewLoadingDesc(string desc)
        {
            ViewLaunch.UpdateDesc(desc);
        }

        //---------------------------------------------------------------------
        // 更新加载界面进度条描述和进度
        void UpdateViewLoadingDescAndProgress(string desc, int cur, int max)
        {
            ViewLaunch.UpdateDesc(desc);
            ViewLaunch.UpdateLoadingProgress(cur, max);
        }
    }
}
