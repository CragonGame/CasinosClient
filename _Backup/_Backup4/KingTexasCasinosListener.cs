//// Copyright (c) Cragon. All rights reserved.

//namespace Casinos
//{
//#if UNITY_EDITOR || KINGTEXAS_VIEW
//    using System;
//    using System.Collections.Generic;
//    using UnityEngine;
//    using FairyGUI;

//    public class KingTexasCasinosListener : CasinosListener
//    {
//        //---------------------------------------------------------------------        
//        bool FairyGUILoadDone { get; set; }

//        //---------------------------------------------------------------------
//        public KingTexasCasinosListener() : base()
//        {
//            BackSound = "background";
//        }

//        //---------------------------------------------------------------------
//        public override string[] OnInitializeBegin(Action init_done_callback)
//        {
//            // 播放背景音乐
//            CasinosContext.Instance.Play(BackSound, _eSoundLayer.Background);

//            // 初始化单位模块
//            CasinosContext.Instance.AsyncAssetLoadGroup.asyncLoadLocalBundle(
//                ListUiPackage, _eAsyncAssetLoadType.LocalBundle,
//                (List<AssetBundle> list_ab) =>
//                {
//                    foreach (var i in list_ab)
//                    {
//                        UIPackage.AddPackage(i);
//                    }

//                    FairyGUILoadDone = true;
//                    if (FairyGUILoadDone && init_done_callback != null)
//                    {
//                        init_done_callback();
//                    }
//                });

//            return new string[] { "确认退出吗" };
//        }

//        //---------------------------------------------------------------------
//        public override string GetCanNotChatTipsDesktopH()
//        {
//            int vip_limit = CasinosContext.Instance.DesktopHCanChatVIPLimit;
//            return string.Format("只有VIP{0}才可以聊天", vip_limit);
//        }

//        //---------------------------------------------------------------------
//        public override string GetCanNotChatTipsDesktop()
//        {
//            int vip_limit = CasinosContext.Instance.DesktopCanChatVIPLimit;
//            return string.Format("只有VIP{0}才可以聊天", vip_limit);
//        }
//    }
//#endif
//}