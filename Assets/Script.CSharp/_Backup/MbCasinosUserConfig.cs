//// Copyright (c) Cragon. All rights reserved.

//namespace Casinos
//{
//    using System;
//    using System.Collections.Generic;
//    using UnityEngine;

//    public enum ConfigType
//    {
//        Dev = 0,
//        Production
//    }

//    [Serializable]
//    public class ConfigSection
//    {
//        public string UCenterDomain = "ucenterdev.cragon.cn";
//        public string AutopatcherUrl = "http://www.cragon.cn/download/cragon/fishing/versioninfo.xml";
//        public string PlayerIconDomain = string.Empty;
//        public string BotIconDomain = string.Empty;
//        public string SysNoticeInfoUrl = string.Empty;
//        public string GatewayIp = string.Empty;
//        public int GatewayPort = 5882;
//        public string[] Tips;
//    }

//    public class MbCasinosUserConfig : MonoBehaviour
//    {
//        //---------------------------------------------------------------------
//        public ConfigType ConfigType = ConfigType.Production;
//        public string ProjectName = "Fishing";
//        public string ChannelName = string.Empty;
//        public ConfigSection ConfigDev;
//        public ConfigSection ConfigProduction;
//        public string DBRemoteConfigUrl;
//        public string DBRemoteConfigName;
//        public List<string> ListDBName;

//        //---------------------------------------------------------------------
//        public ConfigSection Current { get { return _getCurrentConfigSection(); } }

//        //---------------------------------------------------------------------
//        public void addDBName(string[] db_name)
//        {
//            foreach (var i in db_name)
//            {
//                if (!ListDBName.Contains(i))
//                {
//                    ListDBName.Add(i);
//                }
//            }
//        }

//        //---------------------------------------------------------------------
//        public void addDBName2(params string[] db_name)
//        {
//            foreach (var i in db_name)
//            {
//                if (!ListDBName.Contains(i))
//                {
//                    ListDBName.Add(i);
//                }
//            }
//        }

//        //---------------------------------------------------------------------
//        public List<string> getDBName()
//        {
//            return ListDBName;
//        }

//        ////---------------------------------------------------------------------
//        //void Start()
//        //{
//        //    //_translateConfig(ConfigDev);
//        //    //_translateConfig(ConfigProduction);
//        //}

//        //---------------------------------------------------------------------
//        //void Update()
//        //{
//        //}

//        //---------------------------------------------------------------------
//        ConfigSection _getCurrentConfigSection()
//        {
//            if (ConfigType == ConfigType.Dev)
//            {
//                return ConfigDev;
//            }
//            else
//            {
//                return ConfigProduction;
//            }
//        }

//        //---------------------------------------------------------------------
//        void _translateConfig(ConfigSection conifg)
//        {
//            conifg.AutopatcherUrl = UiHelper.formateAndoridIOSUrl(conifg.AutopatcherUrl);
//            conifg.PlayerIconDomain = UiHelper.formateAndoridIOSUrl(conifg.PlayerIconDomain);
//            conifg.BotIconDomain = UiHelper.formateAndoridIOSUrl(conifg.BotIconDomain);
//            conifg.SysNoticeInfoUrl = UiHelper.formateAndoridIOSUrl(conifg.SysNoticeInfoUrl);
//            conifg.UCenterDomain = UiHelper.formateAndoridIOSUrl(conifg.UCenterDomain);
//        }
//    }
//}