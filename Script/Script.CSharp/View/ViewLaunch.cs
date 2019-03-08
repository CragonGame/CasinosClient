// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ViewLaunch : CsView
    {
        //---------------------------------------------------------------------
        public override void Create()
        {
            Debug.Log("ViewLaunch.Create()");
        }

        //---------------------------------------------------------------------
        public override void Destory()
        {
            Debug.Log("ViewLaunch.Destory()");
        }

        //---------------------------------------------------------------------
        public override void HandleEvent(CsEvent ev)
        {
            Debug.Log("ViewLaunch.HandleEvent()");
        }
    }
}
