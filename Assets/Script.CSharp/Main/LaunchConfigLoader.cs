//// Copyright(c) Cragon. All rights reserved.

//namespace Casinos
//{
//    using System;
//    using UnityEngine;

//    public class LaunchConfigLoader
//    {
//        //---------------------------------------------------------------------
//        WWW WWWConfig { get; set; }
//        string ConfigUrl { get; set; }
//        Action<byte[]> LoaderDone { get; set; }

//        int mRetryCount = 0;
//        float mWWWTimeOut = 0f;

//        //---------------------------------------------------------------------
//        public LaunchConfigLoader()
//        {
//        }

//        //---------------------------------------------------------------------
//        public void loadConfig(string config_url, Action<byte[]> loader_done)
//        {
//            ConfigUrl = CasinoHelper.FormalUrlWithRandomVersion(config_url);
//            LoaderDone = loader_done;
//            WWWConfig = new WWW(ConfigUrl);
//        }

//        //---------------------------------------------------------------------
//        public void update(float tm)
//        {
//            if (WWWConfig == null) return;

//            if (WWWConfig.isDone)
//            {
//                if (string.IsNullOrEmpty(WWWConfig.error))
//                {
//                    if (LoaderDone != null)
//                    {
//                        LoaderDone(WWWConfig.bytes);
//                    }

//                    mRetryCount = 0;
//                    WWWConfig.Dispose();
//                    WWWConfig = null;
//                }
//                else
//                {
//                    UiHelperCasinos.UiShowPreLoading("LaunchConfigLoader WWWConfig " + WWWConfig.error, 0);

//                    mRetryCount++;
//                    if (mRetryCount > 3)
//                    {
//                        mRetryCount = 0;
//                        WWWConfig = null;
//                    }
//                    else
//                    {
//                        WWWConfig = new WWW(CasinoHelper.FormalUrlWithRandomVersion(ConfigUrl));
//                    }
//                }
//            }
//            else
//            {
//                // WWW有时会不下载，此处记录超时并重新下载
//                if (WWWConfig.progress == 0)
//                {
//                    mWWWTimeOut += tm;
//                    if (mWWWTimeOut > 5f)
//                    {
//                        mWWWTimeOut = 0f;
//                        WWWConfig = new WWW(CasinoHelper.FormalUrlWithRandomVersion(ConfigUrl));
//                    }
//                }
//                else
//                {
//                    mWWWTimeOut = 0f;
//                }
//            }
//        }
//    }
//}