// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;

    public abstract class Controller
    {
        //---------------------------------------------------------------------
        public ViewMgr ViewMgr { get; set; }

        //---------------------------------------------------------------------
        public abstract void Create();

        //---------------------------------------------------------------------
        public abstract void Destory();

        //---------------------------------------------------------------------
        public abstract void HandleEvent(Event ev);
    }

    public abstract class ControllerFactory
    {
        //---------------------------------------------------------------------
        public abstract string GetName();

        //---------------------------------------------------------------------
        public abstract Controller CreateController();
    }

    public class ControllerFactory<T> : ControllerFactory where T : Controller, new()
    {
        //---------------------------------------------------------------------
        public override string GetName()
        {
            return typeof(T).Name;
        }

        //---------------------------------------------------------------------
        public override Controller CreateController()
        {
            return new T();
        }
    }
}
