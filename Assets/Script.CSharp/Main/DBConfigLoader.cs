// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;
    using UnityEngine;

    public class DBConfigLoader
    {
        //---------------------------------------------------------------------
        WWW WWWConfig { get; set; }
        Dictionary<string, WWW> MapWWWLoadDB { get; set; }
        Dictionary<string, WWW> MapWWWLoadDBDone { get; set; }
        Action<bool, Action> LoaderDone { get; set; }
        string DBConfigUrl { get; set; }
        string DBConfigUrlFinal { get; set; }
        string DBUrl { get; set; }
        string DBConfigName { get; set; }
        string RemoteDbVersion { get; set; }
        StringBuilder SB { get; set; }
        bool NeedReloadDb { get; set; }
        Action LocalTbLoadDone { get; set; }
        int mRetryCount = 0;
        float mWWWTimeOut = 0f;
        const string DBVersion = "DBVersion";

        //---------------------------------------------------------------------
        public void loadDB(string config_url, string db_configname, bool need_reloaddb,
            Action local_tbloaddone = null, params Action<bool, Action>[] loader_done)
        {
            NeedReloadDb = need_reloaddb;
            LocalTbLoadDone = local_tbloaddone;
            DBConfigUrl = config_url;
            DBConfigName = db_configname;
            foreach (var i in loader_done)
            {
                LoaderDone += i;
            }

            SB = new StringBuilder();
            SB.Append(DBConfigUrl);
            SB.Append(Application.version);
            SB.Append("/");
            SB.Append(DBConfigName);
            var url = UiHelper.formateAndoridIOSUrl(SB.ToString());
            DBConfigUrlFinal = url;
            WWWConfig = new WWW(CasinoHelper.FormalUrlWithRandomVersion(DBConfigUrlFinal));
            MapWWWLoadDBDone = new Dictionary<string, WWW>();
        }

        //---------------------------------------------------------------------
        public void update(float tm)
        {
            if (WWWConfig != null)
            {
                if (WWWConfig.isDone)
                {
                    if (string.IsNullOrEmpty(WWWConfig.error))
                    {
                        _loadLua(WWWConfig.bytes);

                        mRetryCount = 0;
                        mWWWTimeOut = 0;
                        WWWConfig = null;
                    }
                    else
                    {
                        UiHelperCasinos.UiShowPreLoading("DBConfigLoader WWWConfig " + WWWConfig.error, 0);

                        mRetryCount++;
                        if (mRetryCount > 3)
                        {
                            mRetryCount = 0;
                            WWWConfig = null;
                        }
                        else
                        {
                            WWWConfig = new WWW(DBConfigUrlFinal);
                        }
                    }
                }
                else
                {
                    // WWW有时会不下载，此处记录超时并重新下载
                    if (WWWConfig.progress == 0)
                    {
                        mWWWTimeOut += tm;
                        if (mWWWTimeOut > 5f)
                        {
                            mWWWTimeOut = 0f;
                            WWWConfig = new WWW(DBConfigUrlFinal);
                        }
                    }
                    else
                    {
                        mWWWTimeOut = 0f;
                    }
                }
            }

            if (MapWWWLoadDB != null)
            {
                foreach (var i in MapWWWLoadDB)
                {
                    if (i.Value.isDone)
                    {
                        MapWWWLoadDBDone[i.Key] = i.Value;
                    }
                }

                foreach (var i in MapWWWLoadDBDone)
                {
                    MapWWWLoadDB.Remove(i.Key);
                    if (string.IsNullOrEmpty(i.Value.error))
                    {
                        string db_name = i.Key.Substring(i.Key.LastIndexOf('/'));
                        string local_path = CasinosContext.Instance.PathMgr.combinePersistentDataPath(db_name);
                        using (MemoryStream ms = new MemoryStream(i.Value.bytes))
                        {
                            using (FileStream fs = new FileStream(local_path, FileMode.Create))
                            {
                                ms.WriteTo(fs);
                            }
                        }
                    }
                }
                MapWWWLoadDBDone.Clear();

                if (MapWWWLoadDB.Count == 0)
                {
                    PlayerPrefs.SetString(Application.identifier + DBVersion, RemoteDbVersion);
                    if (LoaderDone != null)
                    {
                        LoaderDone(NeedReloadDb, LocalTbLoadDone);
                    }

                    MapWWWLoadDB = null;
                }
            }
        }

        //---------------------------------------------------------------------
        void _loadLua(byte[] config)
        {
            CasinosLua casinos_lua = CasinosContext.Instance.CasinosLua;
            casinos_lua.AddLuaFile(DBConfigName, config);
            casinos_lua.DoString(DBConfigName);

            bool version_issame = false;
            if (PlayerPrefs.HasKey(Application.identifier + DBVersion))
            {
                string local_version = PlayerPrefs.GetString(DBVersion);
                version_issame = local_version.Equals(RemoteDbVersion);
            }

            //string list_dbcommon = casinos_lua.LuaEnv.Global.Get<string>("DBListCommon");
            //var db_names_common = list_dbcommon.Split('|');
            //CasinosContext.Instance.UserConfig.addDBName(db_names_common);
            //string list_dbclient = casinos_lua.LuaEnv.Global.Get<string>("DBListClient");
            //var db_names_client = list_dbclient.Split('|');
            //CasinosContext.Instance.UserConfig.addDBName(db_names_client);
            //RemoteDbVersion = casinos_lua.LuaEnv.Global.Get<string>("DBVersion");
            //DBUrl = casinos_lua.LuaEnv.Global.Get<string>("DBUrl");

            //if (!version_issame)
            //{
            //    MapWWWLoadDB = new Dictionary<string, WWW>();
            //    foreach (var i in CasinosContext.Instance.UserConfig.getDBName())
            //    {
            //        SB.Length = 0;
            //        SB.Append(DBUrl);
            //        SB.Append("/");
            //        SB.Append(i);
            //        SB.Append(".db");
            //        string loaddb_url = UiHelper.formateAndoridIOSUrl(SB.ToString());
            //        WWW loaddb_www = new WWW(CasinoHelper.FormalUrlWithRandomVersion(loaddb_url));
            //        MapWWWLoadDB[loaddb_url] = loaddb_www;
            //    }
            //}
            //else
            //{
            //    if (LoaderDone != null)
            //    {
            //        LoaderDone(NeedReloadDb, LocalTbLoadDone);
            //    }
            //}
        }
    }
}