// Copyright (c) Cragon. All rights reserved.

namespace Cs
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    // 配置，开发者选项
    public class ConfigDevelopSettings
    {
        //-------------------------------------------------------------------------
        public bool ShowDevelopSettings { get; set; } = false;// 是否显示开发者选项
        public bool ClientShowFPS { get; set; } = true;// 客户端显示FPS信息 false 不显示 true 显示
        public int FPSLimit { get; set; } = 60;// 限帧

        //-------------------------------------------------------------------------
        public void Load()
        {
            if (PlayerPrefs.HasKey("ClientShowFPS"))
            {
                ClientShowFPS = bool.Parse(UnityEngine.PlayerPrefs.GetString("ClientShowFPS"));
            }
        }
    }

    // 配置，模块配置
    public class ConfigModule
    {
        //-------------------------------------------------------------------------
        //bool WalletEnable = false;// 钱包模块
        public bool GrowEnable { get; set; } = false;// 摇钱树模块

        //-------------------------------------------------------------------------
        public void Load()
        {
        }
    }

    // 配置
    public class Config
    {
        //-------------------------------------------------------------------------
        public string Platform { get; set; }
        public bool IsEditor { get; set; }
        public bool IsEditorDebug { get; set; }
        public string Channel { get; set; }// 渠道
        public ConfigDevelopSettings CfgSettings { get; private set; } = new ConfigDevelopSettings();
        public ConfigModule CfgModule { get; private set; } = new ConfigModule();
        public string Env { get; set; } = "Pro";
        public bool UseLan { get; set; } = true;
        public bool UseDefaultLan { get; set; } = false;
        public string DefaultLan { get; set; } = "Chinese";
        public string CurrentLan { get; set; } = "ChineseSimplified";
        public string BundleVersion { get; set; } = "";// 本地Bundle版本号
        public string LaunchVersion { get; set; } = "";// 本地Launch版本号
        public string LaunchRootURL { get; set; } = "https://cragon-king.oss-cn-shanghai.aliyuncs.com/Launch/";
        public string CommonVersion { get; set; } = "";// 本地Common版本号
        public string CommonRootURL { get; set; } = "https://cragon-king.oss-cn-shanghai.aliyuncs.com/Common/{0}/";// {0}=Version
        public string DataVersion { get; set; } = "";// 本地Data版本号
        public string DataRootURL { get; set; } = "https://cragon-king.oss-cn-shanghai.aliyuncs.com/{0}/Data_{1}/";// {0}=Platform {1}=Version
        public string OssRootUrl { get; set; } = "https://cragon-king-oss.cragon.cn";
        public string AutopatcherUrl { get; set; } = "https://cragon-king-oss.cragon.cn/autopatcher/VersionInfo.xml";
        public string PlayerIconDomain { get; set; } = "https://cragon-king-oss.cragon.cn/images/";
        public string BotIconDomain { get; set; } = "https://cragon-king-oss.cragon.cn/ucenter/";
        public string SysNoticeInfoUrl { get; set; } = "";
        public string UCenterDomain { get; set; } = "https://ucenter-dev.cragon.cn";
        public string GatewayIp { get; set; } = "king-gateway-dev.cragon.cn";
        public int GatewayPort { get; set; } = 5882;
        public int BundleUpdateState { get; set; } = 1;
        public string BundleUpdateVersion { get; set; } = "1.30.001";
        public string BundleUpdateUrlANDROID { get; set; } = "https://cragon-king-oss.cragon.cn/ANDROID/KingTexas_1.30.001.apk";
        public string BundleUpdateUrlIOS { get; set; } = "itms-services:///?action=download-manifest&url=https://cragon-king-oss.cragon.cn/KingTexas.plist";
        public string LaunchFileListFileName { get; set; } = "LaunchFileList.txt";
        public string CommonFileListFileName { get; set; } = "CommonFileList.txt";
        public string DataFileListFileName { get; set; } = "DataFileList.txt";
        public List<string> TbFileList { get; set; } = new List<string>() { "KingCommon", "KingDesktop", "KingDesktopH", "KingClient" };
        public int ServerState { get; set; } = 0;// 服务器状态: 0正常,1维护
        public string ServerStateInfo { get; set; } = "";// 系统公告
        public string LotteryTicketFactoryName { get; set; } = "Texas";
        public bool ClientWechatIsInstalled { get; set; } = true;
        public bool ClientShowWechat { get; set; } = true;// 客户端显示微信登录按钮 false 不显示 true 显示
        public bool ClientShowFirstRecharge { get; set; } = true;// 客户端显示首充按钮 false 不显示 true 显示
        public bool ClientShowGoldTree { get; set; } = false;
        public bool NeedHideClientUi { get; set; } = false;// 客户端排行等界面显示与隐藏
        public bool DesktopHSysBankShowDBValue { get; set; } = true;// 百人系统庄是否显示SQlite配置值
        public int ShootingTextShowVIPLimit { get; set; } = 0;// 弹幕发送后是否真正发送弹幕VIP等级限制，0为无限制
        public int DesktopHCanChatVIPLimit { get; set; } = 1;// 百人是否可聊天VIP等级限制，0为无限制
        public int DesktopCanChatVIPLimit { get; set; } = 0;// 普通桌是否可聊天VIP等级限制，0为无限制
        public bool CanReportLog { get; set; } = false;// 是否开启上传日志到Bugly后台
        public string CanReportLogDeviceId { get; set; } = "";// 可以上传的机器码
        public string CanReportLogPlayerId { get; set; } = "";// 可以上传的玩家Id
        public bool UseWechatPay { get; set; } = true;
        public bool UseAliPay { get; set; } = true;
        public bool UseIAP { get; set; } = false;
        //public int ChipIconSolustion { get; set; } = 0;
        //public int CurrentMoneyType { get; set; } = 0;
        public string UCenterAppId { get; set; } = "King";
        public string PinggPPAppId { get; set; } = "app_TCi58CGKCSaHGCuP";
        public string WeChatAppId { get; set; } = "wxff929d92c3997b5d";
        public string WeChatAppSecret { get; set; } = "159e7a0f00dd15fd81fd63c4844b0dfc";
        public string WeChatState { get; set; } = "Wechat";
        public string DataEyeId { get; set; } = "E02253AEC6F95038833955AC4FED9D77";
        public string PushAppId { get; set; } = "TXYr3LD0se8JU8UOtg9cj3";
        public string PushAppKey { get; set; } = "F6i4mvPKr96KuYsNAXciw9";
        public string PushAppSecret { get; set; } = "DWlJdILTq77OG68J4jFcx3";
        public string ShareSDKAppKey { get; set; } = "254dedc7a3730";
        public string ShareSDKAppSecret { get; set; } = "53788920e17ffa1d9af4ef3540352172";
        public string BeeCloudId { get; set; } = "9c24464e-c912-44aa-bfe8-ca3a384410d0";
        public string BeeCloudLiveSecret { get; set; } = "71625ddd-5a3d-4b73-be74-1580c8912dda";
        public string BeeCloudTestSecret { get; set; } = "7bbc79a8-f310-4d76-a582-2622242c23f5";
        public bool PayUseTestMode { get; set; } = false;
        public string PayUrlScheme { get; set; } = "com.Cragon.KingTexas2";

        //-------------------------------------------------------------------------
        public Config(string platform, bool is_editor, bool is_editor_debug)
        {
            Platform = platform;
            IsEditor = is_editor;
            IsEditorDebug = is_editor_debug;

            // 获取Env值
            string env_key = "Env";
            if (PlayerPrefs.HasKey(env_key))
            {
                Env = PlayerPrefs.GetString(env_key);
            }

            // 获取CurrentLan值
            string lan_key = "LanKey";
            if (PlayerPrefs.HasKey(lan_key))
            {
                CurrentLan = PlayerPrefs.GetString(lan_key);
            }
            else
            {
                CurrentLan = Application.systemLanguage.ToString();
            }

            BundleVersion = Application.version;

            if (PlayerPrefs.HasKey("VersionLaunchPersistent"))
            {
                LaunchVersion = PlayerPrefs.GetString("VersionLaunchPersistent");
            }

            if (PlayerPrefs.HasKey("VersionCommonPersistent"))
            {
                CommonVersion = PlayerPrefs.GetString("VersionCommonPersistent");
            }

            if (PlayerPrefs.HasKey("VersionDataPersistent"))
            {
                DataVersion = PlayerPrefs.GetString("VersionDataPersistent");
            }

            //Debug.Log("Platform=" + Platform);
            //Debug.Log("IsEditorDebug=" + IsEditorDebug);
            //Debug.Log("BundleVersion=" + BundleVersion);
            //Debug.Log("LaunchVersion=" + LaunchVersion);
            //Debug.Log("CommonVersion=" + CommonVersion);
            //Debug.Log("DataVersion=" + DataVersion);
        }

        //-------------------------------------------------------------------------
        public void Load()
        {
            CfgSettings.Load();
            CfgModule.Load();
        }

        //---------------------------------------------------------------------
        public void WriteVersionLaunchPersistent(string version_launch_persistent_new)
        {
            LaunchVersion = version_launch_persistent_new;
            PlayerPrefs.SetString("VersionLaunchPersistent", LaunchVersion);
        }

        //---------------------------------------------------------------------
        public void WriteVersionCommonPersistent(string version_common_persistent_new)
        {
            CommonVersion = version_common_persistent_new;
            PlayerPrefs.SetString("VersionCommonPersistent", CommonVersion);
        }

        //---------------------------------------------------------------------
        public void WriteVersionDataPersistent(string version_data_persistent_new)
        {
            DataVersion = version_data_persistent_new;
            PlayerPrefs.SetString("VersionDataPersistent", DataVersion);
        }

        //---------------------------------------------------------------------
        // v1>v2, return 1; v1=v2, return 0; v1<v2, return -1;
        public int VersionCompare(string v1, string v2)
        {
            if (string.IsNullOrEmpty(v1)) return -1;
            if (string.IsNullOrEmpty(v2)) return 1;

            string v11 = v1.Replace(".", "");
            string v22 = v2.Replace(".", "");
            long i1 = long.Parse(v11);
            long i2 = long.Parse(v22);

            if (i1 > i2) return 1;
            else if (i1 == i2) return 0;
            else return -1;
        }
    }
}
