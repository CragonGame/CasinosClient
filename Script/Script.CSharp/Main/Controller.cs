// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;

    public abstract class Controller : EventListener
    {
        //---------------------------------------------------------------------
        public EventMgr EventMgr { get; set; }
        public ViewMgr ViewMgr { get; set; }
        public ControllerMgr ControllerMgr { get; set; }

        //---------------------------------------------------------------------
        public abstract void Create();

        //---------------------------------------------------------------------
        public abstract void Destory();

        //---------------------------------------------------------------------
        public void ListenEvent<T>() where T : Event
        {
            EventMgr.ListenEvent<T>(this);
        }

        //---------------------------------------------------------------------
        public void UnListenAllEvent()
        {
            EventMgr.UnListenAllEvent(this);
        }

        //---------------------------------------------------------------------
        public T GenEvent<T>() where T : Event, new()
        {
            return EventMgr.GenEvent<T>();
        }
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
