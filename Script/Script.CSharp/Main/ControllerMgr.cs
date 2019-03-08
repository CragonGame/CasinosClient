// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ControllerMgr
    {
        //-------------------------------------------------------------------------
        Dictionary<string, ViewFactory> MapViewFactory { get; set; } = new Dictionary<string, ViewFactory>();
        Dictionary<string, View> MapView { get; set; } = new Dictionary<string, View>();

        //-------------------------------------------------------------------------
        public void RegViewFactory(ViewFactory factory)
        {
            MapViewFactory[factory.GetName()] = factory;
        }

        //-------------------------------------------------------------------------
        public void Create()
        {
        }

        //-------------------------------------------------------------------------
        public void Destroy()
        {
        }
    }
}
