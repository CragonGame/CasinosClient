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
            string s = string.Format("CsContext.Create() PlatForm={0}, IsEditorDebug={1}", platform, is_editor_debug);
            Debug.Log(s);

            Platform = platform;
            PathMgr = new PathMgr(platform, is_editor_debug);
            ResourceMgr = new ResourceMgr();
            SpineMgr = new SpineMgr();
            ControllerMgr = new ControllerMgr();

            ViewMgr = new ViewMgr();
            ViewMgr.RegViewFactory(new ViewFactoryLaunch());
            ViewMgr.Create();

            // 先显示Loading界面
            ViewMgr.CreateView<ViewLaunch>();

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

            Debug.Log("CsContext.Destroy()");
        }

        //-------------------------------------------------------------------------
        public void Update()
        {
        }
    }
}
