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
        public string VersionLaunchStreamingAssets { get; set; }
        public string VersionLaunchPersistent { get; set; }
        DelegateLua1 FuncLuaLaunchInit { get; set; }
        DelegateLuaUpdate FuncLuaLaunchUpdate { get; set; }
        Action FuncLuaLaunchRelease { get; set; }

        //---------------------------------------------------------------------
        public CasinosLaunch()
        {
            // 读取VersionLaunchStreamingAssets
            var datafilelist_path = CasinosContext.Instance.PathMgr.combineWWWStreamingAssetsPath("StreamingAssetsInfo.json");
            WWW www = new WWW(datafilelist_path);
            while (!www.isDone)
            {
            }

            StreamingAssetsInfo streaming_assets_info = Newtonsoft.Json.JsonConvert.DeserializeObject<StreamingAssetsInfo>(www.text);

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
                foreach (var i in streaming_assets_info.ListLaunchDir)
                {
                    var copy_dir = new CopyStreamingAssetsToPersistentData(i);
                }
            }

            // 预加载Script.Lua/Launch中的所有lua文件，显示加载界面
            CasinosContext.Instance.CasinosLua.LoadLuaLaunch();
            CasinosContext.Instance.CasinosLua.DoString("Launch");
            var lua_launch = CasinosContext.Instance.CasinosLua.LuaEnv.Global.Get<LuaTable>("Launch");
            FuncLuaLaunchInit = lua_launch.Get<DelegateLua1>("Init");
            FuncLuaLaunchUpdate = lua_launch.Get<DelegateLuaUpdate>("Update");
            FuncLuaLaunchRelease = lua_launch.Get<Action>("Release");
            FuncLuaLaunchInit(lua_launch);
        }

        //---------------------------------------------------------------------
        //public async Task Fire()
        //{
        //    Debug.Log("CasinosLaunch.Fire() 1");
        //    await Task.Delay(5000);
        //    Debug.Log("CasinosLaunch.Fire()2");
        //}
    }
}