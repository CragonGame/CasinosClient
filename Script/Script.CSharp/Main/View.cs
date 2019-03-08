// Copyright (c) Cragon. All rights reserved.
// 一个UI Package对应一个AssetBundle

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
        public virtual string GetAbUiDir()
        {
            return Context.Instance.PathMgr.DirAbUi;
        }

        //-------------------------------------------------------------------------
        public virtual List<string> GetAbUiDependencies()
        {
            return null;
        }

        //-------------------------------------------------------------------------
        public abstract string GetAbUiAndPackageName();

        //-------------------------------------------------------------------------
        public abstract string GetComponentName();

        //-------------------------------------------------------------------------
        public abstract View CreateView();
    }
}
