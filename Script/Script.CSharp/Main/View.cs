// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using FairyGUI;

    public abstract class View
    {
        //-------------------------------------------------------------------------
        public GComponent GCom { get; set; }

        //-------------------------------------------------------------------------
        public abstract void Create();

        //-------------------------------------------------------------------------
        public abstract void Destory();

        //-------------------------------------------------------------------------
        public abstract void HandleEvent(Event ev);
    }

    public abstract class ViewFactory
    {
        //-------------------------------------------------------------------------
        public abstract string GetName();

        //-------------------------------------------------------------------------
        public abstract View CreateView();
    }

    public class ViewFactory<T> : ViewFactory where T : View, new()
    {
        //-------------------------------------------------------------------------
        public override string GetName()
        {
            return typeof(T).Name;
        }

        //-------------------------------------------------------------------------
        public override View CreateView()
        {
            return new T();
        }
    }
}
