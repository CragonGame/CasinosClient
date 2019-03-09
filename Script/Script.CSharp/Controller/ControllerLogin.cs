// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public class ControllerLogin : Controller
    {
        //---------------------------------------------------------------------
        LoginPlayerPrefs LoginPlayerPrefs { get; set; }

        //---------------------------------------------------------------------
        public override void Create()
        {
            LoginPlayerPrefs = new LoginPlayerPrefs();
        }

        //---------------------------------------------------------------------
        public override void Destory()
        {
        }

        //---------------------------------------------------------------------
        public override void HandleEvent(Event ev)
        {
        }
    }
}