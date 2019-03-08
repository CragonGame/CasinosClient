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

    public class ViewFactoryLaunch : ViewFactory
    {
        //-------------------------------------------------------------------------
        public override string GetName()
        {
            return "ViewLaunch";
        }

        //-------------------------------------------------------------------------
        public override string GetAbUiDir()
        {
            return Context.Instance.PathMgr.DirAbLaunch;
        }

        //-------------------------------------------------------------------------
        public override List<string> GetAbUiDependencies()
        {
            // "DenLong", "LoadingMarry", 
            //return new List<string>() { "PreMsgbox" };
            return null;
        }

        //-------------------------------------------------------------------------
        public override string GetAbUiAndPackageName()
        {
            return "PreLoading";
        }

        //-------------------------------------------------------------------------
        public override string GetComponentName()
        {
            return "PreLoading";
        }

        //-------------------------------------------------------------------------
        public override View CreateView()
        {
            return new ViewLaunch();
        }
    }
}
