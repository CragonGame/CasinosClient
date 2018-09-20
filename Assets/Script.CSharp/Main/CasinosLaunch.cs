// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Threading.Tasks;
    using UnityEngine;

    public class DataInfo
    {
        public string DataVersion { get; set; }
    }

    public class CasinosLaunch
    {
        //---------------------------------------------------------------------
        public string VersionLaunchStreamingAssets { get; set; }
        public string VersionLaunchPersistent { get; set; }
        
        //public ParseStreamingAssetsDataInfo ParseStreamingAssetsDataInfo { get; set; }

        //---------------------------------------------------------------------
        public CasinosLaunch()
        {
            // 读取VersionLaunchStreamingAssets
            var datafilelist_path = CasinosContext.Instance.PathMgr.combineWWWStreamingAssetsPath("Android/DataVersion.json");
            WWW www = new WWW(datafilelist_path);
            while (!www.isDone)
            {
            }

            DataInfo data_info = Newtonsoft.Json.JsonConvert.DeserializeObject<DataInfo>(www.text);

            // 读取VersionLaunchPersistent
            string version_launch_persistent = string.Empty;
            if (PlayerPrefs.HasKey("VersionLaunchPersistent"))
            {
                version_launch_persistent = PlayerPrefs.GetString("VersionLaunchPersistent");
            }

            // 比较Launch版本，决定是否将Launch从StreamingAssets拷贝到Persistent
            bool need_copy = false;
            if (string.IsNullOrEmpty(version_launch_persistent))
            {
                need_copy = true;
            }

            // 将Launch从StreamingAssets拷贝到Persistent
            if (need_copy)
            {

            }

            //ParseStreamingAssetsDataInfo = new ParseStreamingAssetsDataInfo(_parseStreamingAssetsDataDown);
            //ParseStreamingAssetsDataInfo.startPaseData();
        }

        //---------------------------------------------------------------------
        public async Task Fire()
        {
            //WWW www = new WWW("asdf");
            //await www;

            await Task.Delay(5000);

            Debug.Log("CasinosLaunch.Fire()");
        }

        //---------------------------------------------------------------------
        //public void update(float tm)
        //{
        //    if (ParseStreamingAssetsDataInfo != null)
        //    {
        //        ParseStreamingAssetsDataInfo.update(tm);
        //    }

        //    //if (FuncLuaUpdate != null)
        //    //{
        //    //    FuncLuaUpdate(tm);
        //    //}
        //}

        //---------------------------------------------------------------------
        public void copyStreamingAssetsToPersistentDataPath()
        {
            //CopyStreamingAssetsToPersistentDataPath();
        }

        //---------------------------------------------------------------------
        void _parseStreamingAssetsDataDown()
        {
            bool need_copy_predata = false;
            if (!PlayerPrefs.HasKey(CasinosContext.PreDataVersionKey))
            {
                need_copy_predata = true;
            }
            else
            {
                var predata_version = PlayerPrefs.GetString(CasinosContext.PreDataVersionKey);
                //if (!CasinosContext.Instance.Config.InitDataVersion.Equals(predata_version))
                {
                    need_copy_predata = true;
                }
            }

            //if (need_copy_predata)
            //{
            //    CasinosContext.Instance.CopyStreamingAssetsToPersistentDataPath.startCopy(ParseStreamingAssetsDataInfo.ListPreData, null, _firstCopyPreDataDown);
            //}
            //else
            //{
            //    _startControllerLaunchLua();
            //}
        }

        //---------------------------------------------------------------------
        void _firstCopyPreDataDown()
        {
            //PlayerPrefs.SetString(CasinosContext.PreDataVersionKey, CasinosContext.Instance.Config.InitDataVersion);

            _startControllerLaunchLua();
        }

        //---------------------------------------------------------------------
        void _startControllerLaunchLua()
        {
            //CasinosContext c_c = CasinosContext.Instance;
            //c_c.CasinosLua.DoString("Launch");
            //ControllerLaunch = c_c.CasinosLua.LuaEnv.Global.Get<LuaTable>("Launch");
            //var action_new = ControllerLaunch.Get<Action<LuaTable>>("new");
            //action_new(c_c.CasinosLua.LuaEnv.NewTable());
            //var action_oncreate = ControllerLaunch.Get<Action>("onCreate");
            //action_oncreate();
            //FuncLuaUpdate = ControllerLaunch.Get<DelegateLuaUpdate>("onUpdate");
            //CopyStreamingAssetsToPersistentDataPath = ControllerLaunch.Get<Action>("copyStreamingAssetsToPersistentDataPath");
        }
    }
}