// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public class ControllerLaunch : Controller
    {
        //---------------------------------------------------------------------
        public override void Create()
        {
            Debug.Log("ControllerLaunch.Create()");
        }

        //---------------------------------------------------------------------
        public override void Destory()
        {
            Debug.Log("ControllerLaunch.Destory()");
        }

        //---------------------------------------------------------------------
        public override void HandleEvent(Event ev)
        {
        }

        //---------------------------------------------------------------------
        public void Launch()
        {
            // 先创建ViewLaunch
            ViewMgr.CreateView<ViewLaunch>();
        }
    }
}
