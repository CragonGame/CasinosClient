// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System.Collections.Generic;
    using UnityEngine;
    using FairyGUI;
    using Spine.Unity;

    public class ViewLogin : View
    {
        //---------------------------------------------------------------------
        public override void Create()
        {
            Debug.Log("ViewLaunch.ViewLogin()");
        }

        //---------------------------------------------------------------------
        public override void Destory()
        {
        }

        //---------------------------------------------------------------------
        public override void HandleEvent(Event ev)
        {
        }
    }

    public class ViewFactoryLogin : ViewFactory
    {
        //-------------------------------------------------------------------------
        public override string GetName()
        {
            return "ViewLogin";
        }

        //-------------------------------------------------------------------------
        public override List<string> GetAbUiDependencies()
        {
            return new List<string>() { "Common" };
        }

        //-------------------------------------------------------------------------
        public override string GetAbUiAndPackageName()
        {
            return "Login";
        }

        //-------------------------------------------------------------------------
        public override string GetComponentName()
        {
            return "Login";
        }

        //-------------------------------------------------------------------------
        public override View CreateView()
        {
            return new ViewLogin();
        }
    }
}
