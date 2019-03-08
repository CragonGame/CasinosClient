// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Context
    {
        //-------------------------------------------------------------------------
        public static Context Instance { get; private set; }
        public ControllerMgr ControllerMgr { get; private set; }
        public ViewMgr ViewMgr { get; private set; }

        //-------------------------------------------------------------------------
        public Context()
        {
            Instance = this;
        }

        //-------------------------------------------------------------------------
        public void Create()
        {
            Debug.Log("CsContext.Create()");

            ControllerMgr = new ControllerMgr();

            ViewMgr = new ViewMgr();
            ViewMgr.RegViewFactory(new ViewFactory<ViewLaunch>());
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
