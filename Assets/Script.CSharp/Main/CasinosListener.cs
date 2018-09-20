// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    
    public abstract class CasinosListener
    {
        //---------------------------------------------------------------------
        public string BackSound { get; set; }
        public List<string> ListUiPackage { get; private set; }

        //---------------------------------------------------------------------
        public CasinosListener()
        {
            ListUiPackage = new List<string>();
        }

        //---------------------------------------------------------------------
        public void AddUiPackage(string ui_package_path)
        {
            ListUiPackage.Add(ui_package_path);
        }

        //---------------------------------------------------------------------
        public abstract string[] OnInitializeBegin(Action init_done_callback);

        //---------------------------------------------------------------------
        public abstract void OnInitializeUpdate(float elapsed_tm);

        //---------------------------------------------------------------------
        public abstract void OnInitializeEnd();

        //---------------------------------------------------------------------
        public abstract string GetCanNotChatTipsDesktopH();

        //---------------------------------------------------------------------
        public abstract string GetCanNotChatTipsDesktop();

        //---------------------------------------------------------------------
        public abstract void OnFixedUpdate(float elapsed_tm);
    }
}