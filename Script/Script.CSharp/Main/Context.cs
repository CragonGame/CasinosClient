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
        public PathMgr PathMgr { get; private set; }
        public ResourceMgr ResourceMgr { get; private set; }
        public SoundMgr SoundMgr { get; private set; }
        public ParticleMgr ParticleMgr { get; private set; }
        public SpineMgr SpineMgr { get; private set; }
        public ControllerMgr ControllerMgr { get; private set; }
        public ViewMgr ViewMgr { get; private set; }
        public string Platform { get; private set; }

        //-------------------------------------------------------------------------
        public Context()
        {
            Instance = this;
        }

        //-------------------------------------------------------------------------
        public void Create(string platform, bool is_editor_debug)
        {
            string s = string.Format("Context.Create() PlatForm={0}, IsEditorDebug={1}", platform, is_editor_debug);
            Debug.Log(s);

            Platform = platform;
            PathMgr = new PathMgr(platform, is_editor_debug);
            ResourceMgr = new ResourceMgr();
            SoundMgr = new SoundMgr();
            ParticleMgr = new ParticleMgr();
            SpineMgr = new SpineMgr();

            ControllerMgr = new ControllerMgr();
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerActivity>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerActor>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerBag>());
            ControllerMgr.RegControllerFactory(new ControllerFactory<ControllerDesktopListTexas>());
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

            ControllerMgr.Create();
            ViewMgr.Create();

            // 先创建ControllerLaunch
            ControllerLaunch controller_launch = ControllerMgr.CreateController<ControllerLaunch>();
            controller_launch.Launch();

            //TestJson.Run();
            //TestMsgPack.Run();
            //TestProtobuf.Run();
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

            Debug.Log("Context.Destroy()");
        }

        //-------------------------------------------------------------------------
        public void Update()
        {
        }
    }
}
