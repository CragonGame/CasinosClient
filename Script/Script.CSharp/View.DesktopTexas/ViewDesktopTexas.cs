// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System.Collections.Generic;
    using UnityEngine;
    using FairyGUI;

    public class ViewDesktopTexas : View
    {
        //---------------------------------------------------------------------
        DesktopTexasFlow Flow { get; set; }

        //---------------------------------------------------------------------
        public override void Create()
        {
            Flow = new DesktopTexasFlow();
            Flow.Create();
        }

        //---------------------------------------------------------------------
        public override void Destory()
        {
            if (Flow != null)
            {
                Flow.Destory();
                Flow = null;
            }
        }

        //---------------------------------------------------------------------
        public override void HandleEvent(Event ev)
        {
        }
    }
}
