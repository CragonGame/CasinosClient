//// Copyright (c) Cragon. All rights reserved.

//namespace Casinos
//{
//    using System;
//    using System.Collections.Generic;

//    public abstract class CasinosListener
//    {
//        //---------------------------------------------------------------------
//        public string BackSound { get; set; }
//        public List<string> ListUiPackage { get; private set; }

//        //---------------------------------------------------------------------
//        public CasinosListener()
//        {
//            ListUiPackage = new List<string>();
//        }

//        //---------------------------------------------------------------------
//        public void AddUiPackage(string ui_package_path)
//        {
//            ListUiPackage.Add(ui_package_path);
//        }

//        //---------------------------------------------------------------------
//        public abstract string[] OnInitializeBegin(Action init_done_callback);

//        //---------------------------------------------------------------------
//        public abstract string GetCanNotChatTipsDesktopH();

//        //---------------------------------------------------------------------
//        public abstract string GetCanNotChatTipsDesktop();
//    }
//}