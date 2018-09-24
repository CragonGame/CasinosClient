// Copyright (c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using XLua;

    public class StreamingAssetsInfo
    {
        public string DataVersion { get; set; }
        public List<string> ListLaunchDir { get; set; }
    }

    public class CasinosLaunch
    {
        //---------------------------------------------------------------------
        //public string VersionLaunchStreamingAssets { get; set; }
        //public string VersionLaunchPersistent { get; set; }
        //DelegateLua1 FuncLuaLaunchInit { get; set; }
        //DelegateLuaUpdate FuncLuaLaunchUpdate { get; set; }
        //Action FuncLuaLaunchRelease { get; set; }
        //GameCloud.Unity.Common.EbTimer Timer { get; set; }

        //---------------------------------------------------------------------
        public CasinosLaunch()
        {
            var config = CasinosContext.Instance.Config;

            // 比较Launch版本，决定是否将Launch从StreamingAssets拷贝到Persistent
            bool need_copy = false;
            if (string.IsNullOrEmpty(config.VersionLaunchPersistent))
            {
                need_copy = true;
            }

            // 将Launch从StreamingAssets拷贝到Persistent
            if (need_copy)
            {
                var copy_dir = new CopyStreamingAssetsToPersistentData1();
                copy_dir.CopySync(config.StreamingAssetsInfo.ListLaunchDir);
            }

            // 预加载Script.Lua/Launch中的所有lua文件，显示加载界面
            var casinos_lua = CasinosContext.Instance.CasinosLua;
            var path_launch = CasinosContext.Instance.PathMgr.combinePersistentDataPath("Script.Lua/Launch/");
            string[] list_path = new string[] { path_launch };
            casinos_lua.LoadLuaFromDir2(list_path);
            casinos_lua.DoString("Launch");
            var lua_launch = casinos_lua.LuaEnv.Global.Get<LuaTable>("Launch");
            var func_setup = lua_launch.Get<DelegateLua1>("Setup");
            func_setup(lua_launch);

            //Timer = CasinosContext.Instance.TimerShaft.RegisterTimer(1000, _onTimer);
        }

        //---------------------------------------------------------------------
        //void _onTimer()
        //{
        //    Debug.Log("TimeJeffies=" + CasinosContext.Instance.TimerShaft.GetTimeJeffies());
        //}

        //---------------------------------------------------------------------
        //public async Task Fire()
        //{
        //    Debug.Log("CasinosLaunch.Fire() 1");
        //    await Task.Delay(5000);
        //    Debug.Log("CasinosLaunch.Fire()2");
        //}
    }
}