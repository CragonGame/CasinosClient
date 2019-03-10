// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public class Context
    {
        //-------------------------------------------------------------------------
        public static Context Instance { get; private set; }
        public StringBuilder Sb { get; set; } = new StringBuilder(512);
        public GameObject GoLaunch { get; private set; }
        public Casinos.MbAsyncLoadAssets MbAsyncLoadAssets { get; private set; }
        public Casinos.MbShowFPS MbShowFPS { get; private set; }
        public Casinos.TimerShaft TimerShaft { get; private set; }
        public Config Config { get; private set; }
        public PathMgr PathMgr { get; private set; }
        public EventMgr EventMgr { get; private set; }
        public ResourceMgr ResourceMgr { get; private set; }
        public SoundMgr SoundMgr { get; private set; }
        public ParticleMgr ParticleMgr { get; private set; }
        public SpineMgr SpineMgr { get; private set; }
        public ControllerMgr ControllerMgr { get; private set; }
        public ViewMgr ViewMgr { get; private set; }
        System.Diagnostics.Stopwatch Stopwatch { get; set; }

        //-------------------------------------------------------------------------
        public Context()
        {
            Instance = this;
        }

        //-------------------------------------------------------------------------
        public void Create(string platform, bool is_editor, bool is_editor_debug)
        {
            string s = string.Format("Context.Create()");
            Debug.Log(s);

            Stopwatch = new System.Diagnostics.Stopwatch();
            Stopwatch.Start();
            TimerShaft = new Casinos.TimerShaft();

            GoLaunch = GameObject.Find("Launch");
            MbAsyncLoadAssets = (Casinos.MbAsyncLoadAssets)GoLaunch.GetComponent("Casinos.MbAsyncLoadAssets");
            MbShowFPS = (Casinos.MbShowFPS)GoLaunch.GetComponent("Casinos.MbShowFPS");

            Config = new Config(platform, is_editor, is_editor_debug);
            Config.Load();
            PathMgr = new PathMgr();
            EventMgr = new EventMgr();
            ResourceMgr = new ResourceMgr();
            SoundMgr = new SoundMgr();
            ParticleMgr = new ParticleMgr();
            SpineMgr = new SpineMgr();

            ControllerMgr = new ControllerMgr();
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerActivity>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerActor>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerBag>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerDesktopTexasList>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerGrow>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerLaunch>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerLogin>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerLotteryTicket>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerMarquee>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerMatchTexasList>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerMusicPlayer>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerPlayer>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerRanking>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerReward>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerTrade>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerUCenter>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerWallet>());

            ViewMgr = new ViewMgr();
            ViewMgr.RegViewFactory(new ViewFactoryLaunch());
            ViewMgr.RegViewFactory(new ViewFactoryLogin());

            ControllerMgr.Create();
            ViewMgr.Create();

            // 初始化系统参数
            if (Config.IsEditor)
            {
                Application.runInBackground = false;
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = -1;
            }
            else
            {
                Application.runInBackground = true;
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
                QualitySettings.vSyncCount = 1;
                Application.targetFrameRate = 60;
            }

            // 是否显示FPS
            if (Config.CfgSettings.ClientShowFPS)
            {
                MbShowFPS.enabled = true;
            }
            else
            {
                MbShowFPS.enabled = false;
            }

            // 先创建ControllerLaunch
            ControllerLaunch controller_launch = ControllerMgr.CreateController<ControllerLaunch>();
            controller_launch.Launch();
        }

        //-------------------------------------------------------------------------
        public void Destroy()
        {
            if (ViewMgr != null)
            {
                ViewMgr.Destroy();
                ViewMgr = null;
            }

            if (ControllerMgr != null)
            {
                ControllerMgr.Destroy();
                ControllerMgr = null;
            }

            if (TimerShaft != null)
            {
                TimerShaft.Destroy();
                TimerShaft = null;
            }

            Debug.Log("Context.Destroy()");
        }

        //-------------------------------------------------------------------------
        public void Update(float elapsed_tm)
        {
            if (TimerShaft != null)
            {
                TimerShaft.ProcessTimer((ulong)Stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
