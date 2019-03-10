// Copyright (c) Cragon. All rights reserved.
// 一个UI Package对应一个AssetBundle

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using FairyGUI;

    public abstract class View : EventListener
    {
        //---------------------------------------------------------------------
        public EventMgr EventMgr { get; set; }
        public ViewMgr ViewMgr { get; set; }
        public ControllerMgr ControllerMgr { get; set; }
        public GameObject Go { get; set; }
        public GComponent GCom { get; set; }

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

    public abstract class ViewFactory
    {
        //---------------------------------------------------------------------
        public abstract string GetName();

        //---------------------------------------------------------------------
        public virtual string GetAbUiDir()
        {
            return Context.Instance.PathMgr.DirAbUi;
        }

        //---------------------------------------------------------------------
        public virtual List<string> GetAbUiDependencies()
        {
            return null;
        }

        //---------------------------------------------------------------------
        public abstract string GetAbUiAndPackageName();

        //---------------------------------------------------------------------
        public abstract string GetComponentName();

        //---------------------------------------------------------------------
        public abstract View CreateView();
    }
}
