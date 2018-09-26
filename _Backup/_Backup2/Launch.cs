//// Copyright (c) Cragon. All rights reserved.

//namespace Casinos
//{
//    using System;
//    using UnityEngine;
//    using XLua;

//    // 该类去创建Launch.lua
//    public class Launch
//    {
//        //---------------------------------------------------------------------
//        public ParseStreamingAssetsDataInfo ParseStreamingAssetsDataInfo { get; set; }
//        LuaTable ControllerLaunch { get; set; }
//        DelegateLuaUpdate FuncLuaUpdate { get; set; }
//        Action CopyStreamingAssetsToPersistentDataPath { get; set; }

//        //---------------------------------------------------------------------
//        public Launch()
//        {
//            ParseStreamingAssetsDataInfo = new ParseStreamingAssetsDataInfo(_parseStreamingAssetsDataDown);
//            ParseStreamingAssetsDataInfo.startPaseData();
//        }

//        //---------------------------------------------------------------------
//        public void update(float tm)
//        {
//            if (ParseStreamingAssetsDataInfo != null)
//            {
//                ParseStreamingAssetsDataInfo.update(tm);
//            }

//            if (FuncLuaUpdate != null)
//            {
//                FuncLuaUpdate(tm);
//            }
//        }

//        //---------------------------------------------------------------------
//        public void copyStreamingAssetsToPersistentDataPath()
//        {
//            CopyStreamingAssetsToPersistentDataPath();
//        }

//        //---------------------------------------------------------------------
//        void _parseStreamingAssetsDataDown()
//        {
//            bool need_copy_predata = false;
//            if (!PlayerPrefs.HasKey(CasinosContext.PreDataVersionKey))
//            {
//                need_copy_predata = true;
//            }
//            else
//            {
//                var predata_version = PlayerPrefs.GetString(CasinosContext.PreDataVersionKey);
//                //if (!CasinosContext.Instance.Config.InitDataVersion.Equals(predata_version))
//                {
//                    need_copy_predata = true;
//                }
//            }

//            if (need_copy_predata)
//            {
//                CasinosContext.Instance.CopyStreamingAssetsToPersistentDataPath.startCopy(ParseStreamingAssetsDataInfo.ListPreData, null, _firstCopyPreDataDown);
//            }
//            else
//            {
//                _startControllerLaunchLua();
//            }
//        }

//        //---------------------------------------------------------------------
//        void _firstCopyPreDataDown()
//        {
//            //PlayerPrefs.SetString(CasinosContext.PreDataVersionKey, CasinosContext.Instance.Config.InitDataVersion);
//            _startControllerLaunchLua();
//        }

//        //---------------------------------------------------------------------
//        void _startControllerLaunchLua()
//        {
//            CasinosContext c_c = CasinosContext.Instance;
//            c_c.CasinosLua.DoString("Launch");
//            ControllerLaunch = c_c.CasinosLua.LuaEnv.Global.Get<LuaTable>("Launch");
//            var action_new = ControllerLaunch.Get<Action<LuaTable>>("new");
//            action_new(c_c.CasinosLua.LuaEnv.NewTable());
//            var action_oncreate = ControllerLaunch.Get<Action>("onCreate");
//            action_oncreate();
//            FuncLuaUpdate = ControllerLaunch.Get<DelegateLuaUpdate>("onUpdate");
//            CopyStreamingAssetsToPersistentDataPath = ControllerLaunch.Get<Action>("copyStreamingAssetsToPersistentDataPath");
//        }
//    }
//}