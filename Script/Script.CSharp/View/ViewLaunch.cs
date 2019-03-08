// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class ViewLaunch : View
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
        public override void HandleEvent(Event ev)
        {
            Debug.Log("ViewLaunch.HandleEvent()");
        }
    }
}
