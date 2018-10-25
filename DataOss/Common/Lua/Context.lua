-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
-- TODO，待删除
PlayerIconDomain = 'http://cragon-king-oss.cragon.cn/images/'
BotIconDomain = 'http://cragon-king-oss.cragon.cn/ucenter/'
UCenterDomain = 'http://ucenter.cragon.cn'
GatewayIp = 'king-gateway.cragon.cn'
GatewayPort = 5882
ServerState = 0-- 服务器状态: 0正常,1维护
ServerStateInfo = ''-- 系统公告
ClientShowFirstRecharge = true-- 客户端显示首充按钮 false 不显示 true 显示
DesktopHSysBankShowDBValue = true-- 百人系统庄是否显示SQlite配置值
ShootingTextShowVIPLimit = 0-- 弹幕发送后是否真正发送弹幕VIP等级限制，0为无限制
DesktopHCanChatVIPLimit = 1-- 百人是否可聊天VIP等级限制，0为无限制
DesktopCanChatVIPLimit = 0-- 普通桌是否可聊天VIP等级限制，0为无限制
CanReportLog = false-- 是否开启上传日志到Bugly后台
CanReportLogDeviceId = ""-- 可以上传的机器码
CanReportLogPlayerId = ""-- 可以上传的玩家Id
UseWechatPay = true
UseAliPay = true
UseIAP = false
UseLan = true
UseDefaultLan = false
DefaultLan = 'Chinese'
ChipIconSolustion = 0
CurrentMoneyType = 0
UCenterAppId = 'King'
BuglyAppId = '0aed5e7e56'
PinggPPAppId = 'app_TCi58CGKCSaHGCuP'
WeChatAppId = 'wxff929d92c3997b5d'
WeChatAppSecret = '159e7a0f00dd15fd81fd63c4844b0dfc'
WeChatState = 'WeChat'
DataEyeId = 'E02253AEC6F95038833955AC4FED9D77'
PushAppId = 'TXYr3LD0se8JU8UOtg9cj3'
PushAppKey = 'F6i4mvPKr96KuYsNAXciw9'
PushAppSecret = 'DWlJdILTq77OG68J4jFcx3'
ShareSDKAppKey = '254dedc7a3730'
ShareSDKAppSecret = '53788920e17ffa1d9af4ef3540352172'
BeeCloudId = '9c24464e-c912-44aa-bfe8-ca3a384410d0'
BeeCloudLiveSecret = '71625ddd-5a3d-4b73-be74-1580c8912dda'
BeeCloudTestSecret = '7bbc79a8-f310-4d76-a582-2622242c23f5'

---------------------------------------
-- 配置，开发者选项
ConfigDevelopSettings = {}

function ConfigDevelopSettings:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.LuaMgr = CS.Casinos.CasinosContext.Instance.LuaMgr
    self.ShowDevelopSettings = false-- 是否显示开发者选项
    self.ClientShowFPS = true-- 客户端显示FPS信息 false 不显示 true 显示
    self.FPSLimit = 60-- 限帧
    return o
end

---------------------------------------
-- 配置
Config = {}

function Config:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.LuaMgr = CS.Casinos.CasinosContext.Instance.LuaMgr
    self.DevelopSettings = ConfigDevelopSettings:new(nil)
    self.Env = nil
    self.CommonVersion = nil
    self.CommonRootURL = nil
    self.DataVersion = nil
    self.DataRootURL = nil
    self.OssRootUrl = 'http://cragon-king-oss.cragon.cn'
    self.AutopatcherUrl = 'http://cragon-king-oss.cragon.cn/autopatcher/VersionInfo.xml'
    self.PlayerIconDomain = 'http://cragon-king-oss.cragon.cn/images/'
    self.BotIconDomain = 'http://cragon-king-oss.cragon.cn/ucenter/'
    self.SysNoticeInfoUrl = ''
    self.UCenterDomain = 'http://ucenter.cragon.cn'
    self.GatewayIp = 'king-gateway.cragon.cn'
    self.GatewayPort = 5882
    self.BundleUpdateStata = 0
    self.BundleUpdateVersion = '1.00.067'
    self.BundleUpdateURL = 'https://cragon-king-oss.cragon.cn/KingTexas.apk'
    self.CommonFileListFileName = 'CommonFileList.txt'
    self.DataFileListFileName = 'DataFileList.txt'
    self.TbFileList = { 'KingCommon', 'KingDesktop', 'KingDesktopH', 'KingClient' }
    self.ServerState = 0-- 服务器状态: 0正常,1维护
    self.ServerStateInfo = ''-- 系统公告
    self.LotteryTicketFactoryName = 'Texas'
    self.ClientWechatIsInstalled = true
    self.ClientShowWechat = true-- 客户端显示微信登录按钮 false 不显示 true 显示
    self.ClientShowFirstRecharge = true-- 客户端显示首充按钮 false 不显示 true 显示
    self.ClientShowGoldTree = false
    self.NeedHideClientUi = false-- 客户端排行等界面显示与隐藏
    self.DesktopHSysBankShowDBValue = true-- 百人系统庄是否显示SQlite配置值
    self.ShootingTextShowVIPLimit = 0-- 弹幕发送后是否真正发送弹幕VIP等级限制，0为无限制
    self.DesktopHCanChatVIPLimit = 1-- 百人是否可聊天VIP等级限制，0为无限制
    self.DesktopCanChatVIPLimit = 0-- 普通桌是否可聊天VIP等级限制，0为无限制
    self.CanReportLog = false-- 是否开启上传日志到Bugly后台
    self.CanReportLogDeviceId = ""-- 可以上传的机器码
    self.CanReportLogPlayerId = ""-- 可以上传的玩家Id
    self.UseWechatPay = true
    self.UseAliPay = true
    self.UseIAP = false
    self.UseLan = true
    self.UseDefaultLan = false
    self.DefaultLan = 'Chinese'
    self.ChipIconSolustion = 0
    self.CurrentMoneyType = 0
    self.UCenterAppId = 'King'
    self.BuglyAppId = '0aed5e7e56'
    self.PinggPPAppId = 'app_TCi58CGKCSaHGCuP'
    self.WeChatAppId = 'wxff929d92c3997b5d'
    self.WeChatAppSecret = '159e7a0f00dd15fd81fd63c4844b0dfc'
    self.WeChatState = 'WeChat'
    self.DataEyeId = 'E02253AEC6F95038833955AC4FED9D77'
    self.PushAppId = 'TXYr3LD0se8JU8UOtg9cj3'
    self.PushAppKey = 'F6i4mvPKr96KuYsNAXciw9'
    self.PushAppSecret = 'DWlJdILTq77OG68J4jFcx3'
    self.ShareSDKAppKey = '254dedc7a3730'
    self.ShareSDKAppSecret = '53788920e17ffa1d9af4ef3540352172'
    self.BeeCloudId = '9c24464e-c912-44aa-bfe8-ca3a384410d0'
    self.BeeCloudLiveSecret = '71625ddd-5a3d-4b73-be74-1580c8912dda'
    self.BeeCloudTestSecret = '7bbc79a8-f310-4d76-a582-2622242c23f5'
    self.PayUseTestMode = false
    self.PayUrlScheme = "com.Cragon.KingTexas2"
    return o
end

---------------------------------------
Context = {}

---------------------------------------
function Context:new(o, env)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.LuaMgr = CS.Casinos.CasinosContext.Instance.LuaMgr
    self.Launch = Launch
    self.LaunchStep = {}
    self.Cfg = Config:new(nil)
    self.Cfg.Env = env
    self.Cfg.CommonVersion = self.Launch.LaunchCfg.CommonVersion
    self.Cfg.CommonRootURL = string.format('https://cragon-king-oss.cragon.cn/Common/%s/', self.Launch.LaunchCfg.CommonVersion)
    self.Cfg.DataVersion = self.Launch.LaunchCfg.DataVersion
    self.Cfg.DataRootURL = string.format('https://cragon-king-oss.cragon.cn/%s/Data_%s/', self.CasinosContext.Config.Platform, self.Launch.LaunchCfg.DataVersion)
    self.Env = env
    self.TbDataMgr = nil
    self.Json = nil
    self.Rpc = nil
    self.LuaHelper = nil
    self.ControllerMgr = nil
    self.ViewMgr = nil
    return o
end

---------------------------------------
function Context:Init()
    print('Context:Init()')

    local show_fps_obj = self.CasinosContext.Config.GoMain:GetComponent("Casinos.MbShowFPS")
    show_fps_obj.enabled = self.Cfg.DevelopSettings.ClientShowFPS
    --if (self.Cfg.DevelopSettings.FPSLimit == -1) then
    --    CS.UnityEngine.QualitySettings.vSyncCount = 0
    --elseif (self.Cfg.DevelopSettings.FPSLimit == 30) then
    --    CS.UnityEngine.QualitySettings.vSyncCount = 2
    --elseif (self.Cfg.DevelopSettings.FPSLimit == 60) then
    --    CS.UnityEngine.QualitySettings.vSyncCount = 1
    --else
    --    CS.UnityEngine.QualitySettings.vSyncCount = 0
    --    CS.UnityEngine.Application.targetFrameRate = 60
    --end

    Context:_initLaunchStep()
    Context:_nextLaunchStep()
end

---------------------------------------
function Context:Release()
    if (self.TimerUpdateCopyStreamingAssetsToPersistentData ~= nil) then
        self.TimerUpdateCopyStreamingAssetsToPersistentData:Close()
        self.TimerUpdateCopyStreamingAssetsToPersistentData = nil
    end

    if (self.TimerUpdateRemoteCommonToPersistent ~= nil) then
        self.TimerUpdateRemoteCommonToPersistent:Close()
        self.TimerUpdateRemoteCommonToPersistent = nil
    end

    if (self.TimerUpdateRemoteDataToPersistent ~= nil) then
        self.TimerUpdateRemoteDataToPersistent:Close()
        self.TimerUpdateRemoteDataToPersistent = nil
    end

    self.CasinosContext = nil
    self.LuaMgr = nil
    self.Launch = nil

    print('Context:Release()')
end

---------------------------------------
function Context:DoString(name)
    self.LuaMgr:DoString(name)
end

---------------------------------------
-- 初始化LaunchStep
function Context:_initLaunchStep()
    -- 检测Bundle是否需要更新
    if (self.Cfg.BundleUpdateStata == 1 and self.Cfg.BundleUpdateVersion ~= nil and self.Cfg.BundleUpdateURL ~= nil and self.CasinosContext.Config.VersionBundle ~= self.Cfg.BundleUpdateVersion) then
        self.LaunchStep[1] = "UpdateBundle"
    end

    -- 检测是否需要首次运行解压
    --print('CasinosContext.Config.VersionDataPersistent=' .. self.CasinosContext.Config.VersionDataPersistent)
    --print('CasinosContext.Config.StreamingAssetsInfo.DataVersion=' .. self.CasinosContext.Config.StreamingAssetsInfo.DataVersion)
    --local r = self.CasinosContext.Config:VersionCompare(self.CasinosContext.Config.VersionDataPersistent, self.CasinosContext.Config.StreamingAssetsInfo.DataVersion)
    --if (r < 0) then
    --    self.LaunchStep[2] = "CopyStreamingAssetsToPersistentData"
    --end

    -- 检测是否需要更新Common
    if (self.CasinosContext.Config.VersionCommonPersistent ~= self.Cfg.CommonVersion) then
        self.LaunchStep[2] = "UpdateCommon"
    end

    -- 检测是否需要更新Data
    if (self.CasinosContext.Config.VersionDataPersistent ~= self.Cfg.DataVersion) then
        self.LaunchStep[3] = "UpdateData"
    end

    -- 进入Login界面
    self.LaunchStep[4] = "ShowLogin"
end

---------------------------------------
-- 执行下一步LaunchStep
function Context:_nextLaunchStep()
    -- 更新Bundle
    if (self.LaunchStep[1] ~= nil) then
        self.Launch:UpdateViewLoadingDescAndProgress("准备更新安装包", 0, 100)

        -- 弹框让玩家选择，更新Bundle
        -- TODO，调用Native Api安装Bundle
        local msg_info = string.format('有新的安装包需要更新，当前BundleVersion：%s，新的BundleVersion：%s',
                self.CasinosContext.Config.VersionBundle, self.Cfg.BundleUpdateVersion)
        local view_premsgbox = self.PreViewMgr.CreateView("PreMsgBox")
        view_premsgbox:showMsgBox(msg_info,
                function()
                    CS.UnityEngine.Application.Quit()
                end,
                function()
                    print('更新安装包')
                end
        )

        return
    end

    -- 首次运行解压
    --if (self.LaunchStep[2] ~= nil) then
    --    self.Launch:UpdateViewLoadingDescAndProgress("首次运行解压资源，不消耗流量", 0, 100)
    --    if (self.CopyStreamingAssetsToPersistentData == nil) then
    --        self.CopyStreamingAssetsToPersistentData = CS.Casinos.CopyStreamingAssetsToPersistentData2()
    --        self.CopyStreamingAssetsToPersistentData:CopyAsync('')
    --    end
    --    self.TimerUpdateCopyStreamingAssetsToPersistentData = self.CasinosContext.TimerShaft:RegisterTimer(30, self, self._timerUpdateCopyStreamingAssetsToPersistentData)
    --    return
    --end

    -- 更新Common
    if (self.LaunchStep[2] ~= nil) then
        --if (CS.UnityEngine.Application.internetReachability == CS.UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork) then
        --end
        self.Launch:UpdateViewLoadingDescAndProgress("更新游戏脚本", 0, 100)
        local http_url = self.Cfg.CommonRootURL .. self.Cfg.CommonFileListFileName
        --print(http_url)
        local async_asset_loadgroup = CS.Casinos.CasinosContext.Instance.AsyncAssetLoadGroup
        async_asset_loadgroup:LoadWWWAsync(http_url,
                function(url, www)
                    -- 比较Oss上的CommonFileList.txt和Persistent中的CommonFileList.txt差异集，获取需要更新的列表
                    local commonfilelist_persistent = self.CasinosContext.PathMgr:CombinePersistentDataPath(self.Cfg.CommonFileListFileName)
                    --print(commonfilelist_persistent)
                    self.RemoteCommonFileListContent = www.text
                    local persistent_commonfilelist_content = self.LuaMgr:ReadAllText(commonfilelist_persistent)
                    local commonrootdir_persistent = self.CasinosContext.PathMgr:CombinePersistentDataPath('/')
                    self.UpdateRemoteCommonToPersistent = CS.Casinos.UpdateRemoteToPersistentData()
                    self.UpdateRemoteCommonToPersistent:UpateAsync(self.RemoteCommonFileListContent, persistent_commonfilelist_content, self.Cfg.CommonRootURL, commonrootdir_persistent)
                    self.TimerUpdateRemoteCommonToPersistent = self.CasinosContext.TimerShaft:RegisterTimer(30, self, self._timerUpdateRemoteCommonToPersistent)
                end
        )
        return
    end

    -- 更新Data
    if (self.LaunchStep[3] ~= nil) then
        --if (CS.UnityEngine.Application.internetReachability == CS.UnityEngine.NetworkReachability.ReachableViaLocalAreaNetwork) then
        --end
        self.Launch:UpdateViewLoadingDescAndProgress("更新游戏数据", 0, 100)
        local http_url = self.Cfg.DataRootURL .. self.Cfg.DataFileListFileName
        --print(http_url)
        local async_asset_loadgroup = CS.Casinos.CasinosContext.Instance.AsyncAssetLoadGroup
        async_asset_loadgroup:LoadWWWAsync(http_url,
                function(url, www)
                    -- 比较Oss上的datafilelist.txt和Persistent中的datafilelist.txt差异集，获取需要更新的Data列表
                    local datafilelist_persistent = self.CasinosContext.PathMgr:CombinePersistentDataPath(self.Cfg.DataFileListFileName)
                    --print(datafilelist_persistent)
                    self.RemoteDataFileListContent = www.text
                    local persistent_datafilelist_content = self.LuaMgr:ReadAllText(datafilelist_persistent)
                    local datarootdir_persistent = self.CasinosContext.PathMgr:CombinePersistentDataPath('/')
                    self.UpdateRemoteDataToPersistent = CS.Casinos.UpdateRemoteToPersistentData()
                    self.UpdateRemoteDataToPersistent:UpateAsync(self.RemoteDataFileListContent, persistent_datafilelist_content, self.Cfg.DataRootURL, datarootdir_persistent)
                    self.TimerUpdateRemoteDataToPersistent = self.CasinosContext.TimerShaft:RegisterTimer(30, self, self._timerUpdateRemoteDataToPersistent)
                end
        )
        return
    end

    -- 卸载Launch，加载并显示Login
    if (self.LaunchStep[4] ~= nil) then
        self.LaunchStep[4] = nil

        self.Launch:UpdateViewLoadingDescAndProgress("准备登录中", 0, 100)

        self.LuaMgr:LoadLuaFromRawDir(self.CasinosContext.PathMgr.DirLuaRoot)

        self.CasinosContext.CanReportLog = self.Cfg.CanReportLog
        self.CasinosContext.CanReportLogDeviceId = self.Cfg.CanReportLogDeviceId
        self.CasinosContext.CanReportLogPlayerId = self.Cfg.CanReportLogPlayerId
        if (self.CasinosContext.UnityAndroid == true) then
        end

        self:DoString("MessagePack")
        self.Json = require("json")
        self:DoString("RPC")
        self.Rpc = RPC:new(nil)
        self:DoString("LuaHelper")
        self.LuaHelper = LuaHelper:new(nil)
        self:DoString("TexasHelper")
        self:DoString("TbDataBase")
        self:DoString("TbDataMgr")

        local t_db = {}
        for i, v in pairs(self.Cfg.TbFileList) do
            t_db[i] = self.CasinosContext.PathMgr.DirRawRoot .. "tbdata/" .. v .. ".db"
        end
        self.TbDataMgr = TbDataMgr:new(nil)
        self:_regTbData()
        self.TbDataMgr:Setup(t_db)
        self:DoString("TbDataHelper")
        TbDataHelper:new(nil, self.TbDataMgr)

        self:DoString("LanBase")
        self:DoString("LanEn")
        self:DoString("LanMgr")
        self:DoString("LanZh")
        self.LanMgr = LanMgr:new(nil)
        self.LanMgr:parseLanKeyValue()

        self:_regModel()

        self:DoString("EventSys")
        self.EventSys = EventSys:new(nil)
        self.EventSys:OnCreate()

        self:DoString("UiChipShowHelper")
        self.UiChipShowHelper = UiChipShowHelper:new(nil)

        self:DoString("ViewMgr")
        self.ViewMgr = ViewMgr:new(nil)
        self.ViewMgr.LanMgr = self.LanMgr
        self.ViewMgr.TbDataMgr = self.TbDataMgr
        self.ViewMgr:OnCreate()
        self:_RegView()
        self:DoString("ViewHelper")
        ViewHelper:new(nil)

        self:DoString("CasinoHelper")
        CasinoHelper:new(nil, self.Rpc.MessagePack, self.LanMgr)
        self:DoString("Native")
        Native:new(nil, self.ViewMgr, self)
        self:DoString("PicCapture")
        self.PicCapture = PicCapture:new(nil, self.ViewMgr, self)

        self:DoString("ControllerMgr")
        self.ControllerMgr = ControllerMgr:new(nil)
        self.ControllerMgr:OnCreate()
        self.ControllerMgr.TbDataMgr = self.TbDataMgr
        self.ControllerMgr.RPC = self.Rpc
        self.ControllerMgr.LanMgr = self.LanMgr
        self:_regController()
        self.ViewMgr.ControllerMgr = self.ControllerMgr
        self.ViewMgr.RPC = self.Rpc

        -- 加载AssetBundle列表
        local table_ab = {
            "About", "ActivityCenter", "ActivityPopup", "AgreeOrDisAddFriendRequest", "ApplySucceed",
            "Bag", "Bank", "BlindTable",
            "Chat", "ChatChooseTarget", "ChatExPression", "ChatFriend", "ChipOperate", "ChooseLan", "ClassicModel", "Club", "ClubHelp", "Common", "CreateDeskTop", "CreateMatch",
            "DailyReward", "Desktop", "DesktopChatParent", "DesktopHints", "DesktopPlayerInfo", "DesktopPlayerOperate", "DesktopMenu",
            "DesktopH", "DesktopHBetReward", "DesktopHTexas", "DesktopHBankPlayerList", "DesktopHCardType", "DesktopHHistory", "DesktopHRewardPot",
            "DesktopHMenu", "DesktopHHelp", "DesktopHResult", "DesktopHSetCardType", "DesktopHTongSha", "DesktopHTongPei",
            "Edit", "EditAddress", "EnterMatchNotify",
            "Friend", "FriendOnLine", "Feedback",
            "GetChipEffect", "GiftDetail", "GiftShop",
            "IdCardCheck", "InviteFriendPlay",
            "JoinMatch",
            "LanZh", "LanEn", "LanZhAndroid", "Loading", "LockChat", "Login", "LotteryTicket",
            "Mail", "MailDetail", "Main", "MatchInfo", "MatchLobby", "MTTGameResult", "MTTProcess",
            "Notice",
            "PayType", "PlayerInfo", "PlayerProfile", "Pool", "Purse",
            "QuitOrBack",
            "Ranking", "RechargeFirst", "ResetPwd",
            "Share", "ShareType", "ShootingText", "Shop", "SnowBallReward",
            "TakePhoto",
        }
        for i = 1, #(table_ab) do
            local full_name = self.CasinosContext.PathMgr.DirAbUi .. string.lower(table_ab[i]) .. ".ab"
            table_ab[i] = full_name
        end

        self.LuaMgr:LoadLocalBundleAsync(self, table_ab,
                function(this, list_ab)
                    local ui_package = CS.FairyGUI.UIPackage
                    for i, v in pairs(list_ab) do
                        ui_package.AddPackage(v)
                    end

                    -- 销毁Launch相关资源，加载登录界面
                    self.Launch:Finish()
                    self.ControllerMgr:CreateController("UCenter", nil, nil)
                    self.ControllerMgr:CreateController("Login", nil, nil)
                end
        )
    end
end

---------------------------------------
-- 定时器，首次运行解压
function Context:_timerUpdateCopyStreamingAssetsToPersistentData(tm)
    local is_done = self.CopyStreamingAssetsToPersistentData:IsDone()
    if (is_done) then
        self.TimerUpdateCopyStreamingAssetsToPersistentData:Close()
        self.TimerUpdateCopyStreamingAssetsToPersistentData = nil
        self.CopyStreamingAssetsToPersistentData = nil

        -- 用LaunchInfo.LaunchVersion覆盖Persistent中的；并更新VersionDataPersistent
        self.CasinosContext.Config:WriteVersionDataPersistent(self.CasinosContext.Config.LaunchInfo.LaunchVersion)

        -- 执行下一步LaunchStep
        self.LaunchStep[2] = nil
        self:_nextLaunchStep()
    else
        local value = self.CopyStreamingAssetsToPersistentData.LeftCount
        local max = self.CopyStreamingAssetsToPersistentData.TotalCount
        self.Launch:UpdateViewLoadingProgress(max - value, max)
    end
end

---------------------------------------
-- 定时器，更新Common
function Context:_timerUpdateRemoteCommonToPersistent(tm)
    local is_done = self.UpdateRemoteCommonToPersistent:IsDone()
    if (is_done) then
        self.TimerUpdateRemoteCommonToPersistent:Close()
        self.TimerUpdateRemoteCommonToPersistent = nil
        self.UpdateRemoteCommonToPersistent = nil

        -- 用Remote CommonFileList.txt覆盖Persistent中的；并更新VersionCommonPersistent
        local commonfilelist_persistent = self.CasinosContext.PathMgr:CombinePersistentDataPath(self.Cfg.CommonFileListFileName)
        CS.System.IO.File.WriteAllText(commonfilelist_persistent, self.RemoteCommonFileListContent)
        self.RemoteCommonFileListContent = nil
        self.CasinosContext.Config:WriteVersionCommonPersistent(self.Cfg.CommonVersion)

        -- 执行下一步LaunchStep
        self.LaunchStep[2] = nil
        self:_nextLaunchStep()
    else
        local value = self.UpdateRemoteCommonToPersistent.LeftCount
        local max = self.UpdateRemoteCommonToPersistent.TotalCount
        self.Launch:UpdateViewLoadingProgress(max - value, max)
    end
end

---------------------------------------
-- 定时器，更新Data
function Context:_timerUpdateRemoteDataToPersistent(tm)
    local is_done = self.UpdateRemoteDataToPersistent:IsDone()
    if (is_done) then
        self.TimerUpdateRemoteDataToPersistent:Close()
        self.TimerUpdateRemoteDataToPersistent = nil
        self.UpdateRemoteDataToPersistent = nil

        -- 用Remote DataFileList.txt覆盖Persistent中的；并更新VersionDataPersistent
        local datafilelist_persistent = self.CasinosContext.PathMgr:CombinePersistentDataPath(self.Cfg.DataFileListFileName)
        CS.System.IO.File.WriteAllText(datafilelist_persistent, self.RemoteDataFileListContent)
        self.RemoteDataFileListContent = nil
        self.CasinosContext.Config:WriteVersionDataPersistent(self.Cfg.DataVersion)

        -- 执行下一步LaunchStep
        self.LaunchStep[3] = nil
        self:_nextLaunchStep()
    else
        local value = self.UpdateRemoteDataToPersistent.LeftCount
        local max = self.UpdateRemoteDataToPersistent.TotalCount
        self.Launch:UpdateViewLoadingProgress(max - value, max)
    end
end

---------------------------------------
function Context:_regModel()
    self:DoString("ModelCommon")
    self:DoString("ModelAccount")
    self:DoString("ModelActor")
    self:DoString("ModelWallet")
    self:DoString("ModelUCenter")
    self:DoString("ModelTrade")
    self:DoString("ModelDesktop")
    self:DoString("ModelDesktopTexas")
    self:DoString("ModelDesktopTexasMatch")
    self:DoString("ModelActivity")
    self:DoString("ModelGrow")
    self:DoString("ModelPlayer")
    self:DoString("ModelRank")
    self:DoString("ModelMarquee")
    self:DoString("ModelWallet")
    self:DoString("ModelBag")
    self:DoString("ModelDesktopH")
    self:DoString("ModelIM")
    self:DoString("ModelIMClientEvent")
    self:DoString("ModelIMMailBox")
    self:DoString("ModelLotteryTicket")
    self:DoString("ModelPlayer")
    self:DoString("ModelPlayerInfo")
end

---------------------------------------
function Context:_regController()
    self:DoString("ControllerLogin")
    local con_login_fac = ControllerLoginFactory:new(nil)
    self.ControllerMgr:RegController("Login", con_login_fac)
    self:DoString("ControllerUCenter")
    local con_ucenter_fac = ControllerUCenterFactory:new(nil)
    self.ControllerMgr:RegController("UCenter", con_ucenter_fac)
    self:DoString("ControllerActivity")
    local con_activity_fac = ControllerActivityFactory:new(nil)
    self.ControllerMgr:RegController("Activity", con_activity_fac)
    self:DoString("ControllerActor")
    local con_actor_fac = ControllerActorFactory:new(nil)
    self.ControllerMgr:RegController("Actor", con_actor_fac)
    self:DoString("ControllerBag")
    local con_bag_fac = ControllerBagFactory:new(nil)
    self.ControllerMgr:RegController("Bag", con_bag_fac)
    self:DoString("ControllerDesktopTexas")
    local con_desk_fac = ControllerDesktopTexasFactory:new(nil)
    self.ControllerMgr:RegController("Desktop", con_desk_fac)
    self:DoString("ControllerDeskH")
    local con_deskh_fac = ControllerDeskHFactory:new(nil)
    self.ControllerMgr:RegController("DesktopH", con_deskh_fac)
    self:DoString("ControllerGrow")
    local con_grow_fac = ControllerGrowFactory:new(nil)
    self.ControllerMgr:RegController("Grow", con_grow_fac)
    self:DoString("ControllerIM")
    self:DoString("IMChat")
    self:DoString("IMFeedback")
    self:DoString("IMChatRecord")
    self:DoString("IMFriendList")
    self:DoString("IMMailBox")
    local con_im_fac = ControllerIMFactory:new(nil)
    self.ControllerMgr:RegController("IM", con_im_fac)
    self:DoString("ControllerLobby")
    local con_lobby_fac = ControllerLobbyFactory:new(nil)
    self.ControllerMgr:RegController("Lobby", con_lobby_fac)
    self:DoString("ControllerLotteryTicket")
    local con_lottery_fac = ControllerLotteryTicketFactory:new(nil)
    self.ControllerMgr:RegController("LotteryTicket", con_lottery_fac)
    self:DoString("ControllerMarquee")
    local con_marquee_fac = ControllerMarqueeFactory:new(nil)
    self.ControllerMgr:RegController("Marquee", con_marquee_fac)
    self:DoString("ControllerPlayer")
    local con_player_fac = ControllerPlayerFactory:new(nil)
    self.ControllerMgr:RegController("Player", con_player_fac)
    self:DoString("ControllerRanking")
    local con_ranking_fac = ControllerRankingFactory:new(nil)
    self.ControllerMgr:RegController("Ranking", con_ranking_fac)
    self:DoString("ControllerTrade")
    local con_trade_fac = ControllerTradeFactory:new(nil)
    self.ControllerMgr:RegController("Trade", con_trade_fac)
    self:DoString("ControllerMTT")
    local con_mtt = ControllerMTTFactory:new(nil)
    self.ControllerMgr:RegController("Mtt", con_mtt)
    self:DoString("Prop")
    self:DoString("Item")
    self:DoString("ItemData1")
    self:DoString("Unit")
    self:DoString("UnitBilling")
    self:DoString("UnitConsume")
    self:DoString("UnitGoodsVoucher")
    self:DoString("UnitGoldPackage")
    self:DoString("UnitGiftNormal")
    self:DoString("UnitRedEnvelopes")
    self:DoString("UnitGiftTmp")
    self:DoString("UnitMagicExpression")
    self:DoString("OnLineReward")
    self:DoString("TimingReward")
end

---------------------------------------
function Context:_RegView()
    self:DoString("ViewLoading")
    local view_loading_fac = ViewLoadingFactory:new(nil, "Loading", "Loading", "Loading", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Loading", view_loading_fac)
    self:DoString("ViewMsgBox")
    local view_msgbox_fac = ViewMsgBoxFactory:new(nil, "Common", "MsgBox", "Waiting", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("MsgBox", view_msgbox_fac)
    self:DoString("ViewLogin")
    self:DoString("UiResetPwd")
    self:DoString("UiRegister")
    self:DoString("UiChooseCountryCode")
    local view_login_fac = ViewLoginFactory:new(nil, "Login", "Login", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Login", view_login_fac)
    self:DoString("ViewAbout")
    local view_about_fac = ViewAboutFactory:new(nil, "About", "About", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("About", view_about_fac)
    self:DoString("ViewDesktopChatParent")
    local view_chatparent_fac = ViewDesktopChatParentFactory:new(nil, "DesktopChatParent", "DesktopChatParent", "DesktopChat", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopChatParent", view_chatparent_fac)
    self:DoString("ViewFloatMsg")
    local view_floatmsg_fac = ViewFloatMsgFactory:new(nil, "Common", "FloatMsg", "Waiting", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("FloatMsg", view_floatmsg_fac)
    self:DoString("ViewMailDetail")
    local view_maildetail_fac = ViewMailDetailFactory:new(nil, "MailDetail", "MailDetail", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("MailDetail", view_maildetail_fac)
    self:DoString("ViewNotice")
    local view_notice_fac = ViewNoticeFactory:new(nil, "Notice", "Notice", "Loading", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Notice", view_notice_fac)
    self:DoString("ViewPayType")
    local view_paytype_fac = ViewPayTypeFactory:new(nil, "PayType", "PayType", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("PayType", view_paytype_fac)
    self:DoString("ViewPermanentPosMsg")
    local view_permanent_fac = ViewPermanentPosMsgFactory:new(nil, "Common", "PermanentPosMsg", "Loading", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("PermanentPosMsg", view_permanent_fac)
    self:DoString("ViewPool")
    local view_pool_fac = ViewPoolFactory:new(nil, "Pool", "Pool", "None", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Pool", view_pool_fac)
    self:DoString("ViewShootingText")
    local view_shooting_fac = ViewShootingTextFactory:new(nil, "ShootingText", "ShootingText", "ShootingText", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ShootingText", view_shooting_fac)
    self:DoString("ViewTakePhoto")
    local view_takephoto_fac = ViewTakePhotoFactory:new(nil, "TakePhoto", "TakePhoto", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("TakePhoto", view_takephoto_fac)
    self:DoString("ViewWaiting")
    local view_waiting_fac = ViewWaitingFactory:new(nil, "Common", "Waiting", "Waiting", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Waiting", view_waiting_fac)
    self:DoString("ViewWaitingCountDown")
    local view_waiting1_fac = ViewWaitingCountDownFactory:new(nil, "Common", "WaitingCountDown", "Waiting", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("WaitingCountDown", view_waiting1_fac)
    self:DoString("ViewLotteryTicket")
    self:DoString("ViewLotteryTicketBase")
    self:DoString("ViewLotteryTicketRewardPot")
    self:DoString("ViewLotteryTicketTexas")
    self:DoString("ItemLotteryTicketBetOperate")
    self:DoString("ItemLotteryTicketBetPot")
    self:DoString("ItemLotteryTicketCard")
    self:DoString("ItemLotteryTicketHistory")
    self:DoString("ItemLotteryTicketRewardPotPlayerInfo")
    local view_lottery_fac = ViewLotteryTicketFactory:new(nil, "LotteryTicket", "LotteryTicket", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("LotteryTicket", view_lottery_fac)
    self:DoString("ViewDesktopTexas")
    self:DoString("PlayerSeatWidgetControllerTexas")
    self:DoString("PlayerOperateAni")
    self:DoString("PlayerShowTips")
    self:DoString("ViewHandCardTexas")
    self:DoString("ViewPotTexasPoker")
    self:DoString("ViewDesktopTypeBase")
    self:DoString("ViewTexasClassic")
    self:DoString("ViewTexasMTT")
    self:DoString("DealerEx")
    self:DoString("ViewMTTGameResult")
    self:DoString("ItemMttRewardItem")
    local view_mttresult_fac = ViewMTTGameResultFactory:new(nil, "MTTGameResult", "MTTGameResult", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("MTTGameResult", view_mttresult_fac)
    self:DoString("ViewMTTProcess")
    local view_mttprocess_fac = ViewMTTProcessFactory:new(nil, "MTTProcess", "MTTProcess", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("MTTProcess", view_mttprocess_fac)
    self:DoString("UiCardCommonEx")
    self:DoString("UiCardDealingEx")
    self:DoString("UiChipEx")
    self:DoString("UiChipMgrEx")
    self:DoString("UiDesktopTexasFlow")
    self:DoString("ViewPlayerGiftAndVIP")
    self:DoString("DesktopPlayerTexas")
    self:DoString("DesktopBase")
    self:DoString("DesktopTexas")
    self:DoString("DesktopHelperBase")
    self:DoString("DesktopHelperTexas")
    self:DoString("DesktopTypeBase")
    self:DoString("DesktopTexasClassic")
    self:DoString("DesktopTexasMTT")
    local view_desktoptexas_fac = ViewDesktopTexasFactory:new(nil, "Desktop", "Desktop", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopTexas", view_desktoptexas_fac)
    self:DoString("ViewDesktopPlayerInfoTexas")
    local view_desktopplayerinfotexas_fac = ViewDesktopPlayerInfoTexasFactory:new(nil, "DesktopPlayerInfo", "DesktopPlayerInfo", "SceneActor", false, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopPlayerInfoTexas", view_desktopplayerinfotexas_fac)
    self:DoString("ViewDesktopPlayerOperateTexas")
    self:DoString("DesktopFastBet")
    local view_desktopoperatetexas_fac = ViewDesktopPlayerOperateTexasFactory:new(nil, "DesktopPlayerOperate", "DesktopPlayerOperate", "PlayerOperateUi", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopPlayerOperateTexas", view_desktopoperatetexas_fac)
    self:DoString("ViewLockChatTexas")
    local view_lockchattexas_fac = ViewLockChatTexasFactory:new(nil, "LockChat", "LockChat", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("LockChatTexas", view_lockchattexas_fac)
    self:DoString("ViewDesktopHintsTexas")
    local view_desktophintstexas_fac = ViewDesktopHintsTexasFactory:new(nil, "DesktopHints", "DesktopHints", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopHintsTexas", view_desktophintstexas_fac)
    self:DoString("ViewDesktopMenuTexas")
    local view_desktopmenutexas_fac = ViewDesktopMenuTexasFactory:new(nil, "DesktopMenu", "DesktopMenu", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopMenuTexas", view_desktopmenutexas_fac)
    self:DoString("ViewDesktopChatExpression")
    local view_desktopchatexp_fac = ViewDesktopChatExpressionFactory:new(nil, "ChatExPression", "ChatExPression", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ChatExPression", view_desktopchatexp_fac)
    self:DoString("ViewDesktopH")
    self:DoString("ViewDesktopHBase")
    self:DoString("ViewDesktopHTexas")
    self:DoString("DesktopHBase")
    self:DoString("DesktopHTexas")
    self:DoString("DesktopHUiGold")
    self:DoString("DesktopHDealer")
    self:DoString("DesktopHBankPlayer")
    self:DoString("DesktopHCard")
    self:DoString("DesktopHCards")
    self:DoString("DesktopHCardTypeBase")
    self:DoString("DesktopHCardTypeTexas")
    self:DoString("DesktopHGoldPool")
    self:DoString("DesktopHRewardPot")
    self:DoString("DesktopHSelf")
    self:DoString("DesktopHStandPlayer")
    self:DoString("DesktopHChair")
    self:DoString("DesktopHChair")
    self:DoString("GoldController")
    self:DoString("DesktopHBetPot")
    local view_desktoph_fac = ViewDesktopHFactory:new(nil, "DesktopH", "DesktopH", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopH", view_desktoph_fac)
    self:DoString("ViewDesktopHBankList")
    local view_desktophbanklist_fac = ViewDesktopHBankListFactory:new(nil, "DesktopHBankPlayerList", "DesktopHBankPlayerList", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopHBankList", view_desktophbanklist_fac)
    self:DoString("ViewDesktopHBetReward")
    local view_desktophbetreward_fac = ViewDesktopHBetRewardFactory:new(nil, "DesktopHBetReward", "DesktopHBetReward", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopHBetReward", view_desktophbetreward_fac)
    self:DoString("ViewDesktopHCardType")
    local view_desktophcardtype_fac = ViewDesktopHCardTypeFactory:new(nil, "DesktopHCardType", "DesktopHCardType", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopHCardType", view_desktophcardtype_fac)
    self:DoString("ViewDesktopHHelp")
    local view_desktophhelp_fac = ViewDesktopHHelpFactory:new(nil, "DesktopHHelp", "DesktopHHelp", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopHHelp", view_desktophhelp_fac)
    self:DoString("ViewDesktopHHistory")
    local view_desktophhistory_fac = ViewDesktopHHistoryFactory:new(nil, "DesktopHHistory", "DesktopHHistory", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopHHistory", view_desktophhistory_fac)
    self:DoString("ViewDesktopHMenu")
    local view_desktophmenu_fac = ViewDesktopHMenuFactory:new(nil, "DesktopHMenu", "DesktopHMenu", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopHMenu", view_desktophmenu_fac)
    self:DoString("ViewDesktopHResult")
    local view_desktophresult_fac = ViewDesktopHResultFactory:new(nil, "DesktopHResult", "DesktopHResult", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopHResult", view_desktophresult_fac)
    self:DoString("ViewDesktopHSetCardType")
    local view_desktophsetcardtype_fac = ViewDesktopHSetCardTypeFactory:new(nil, "DesktopHSetCardType", "DesktopHSetCardType", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopHSetCardType", view_desktophsetcardtype_fac)
    self:DoString("ViewDesktopHRewardPot")
    local view_desktophrewardpot_fac = ViewDesktopHRewardPotFactory:new(nil, "DesktopHRewardPot", "DesktopHRewardPot", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DesktopHRewardPot", view_desktophrewardpot_fac)
    self:DoString("ViewAgreeOrDisAddFriendRequest")
    local view_agreefriend_fac = ViewAgreeOrDisAddFriendRequestFactory:new(nil, "AgreeOrDisAddFriendRequest", "AgreeOrDisAddFriendRequest", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("AgreeOrDisAddFriendRequest", view_agreefriend_fac)
    self:DoString("ViewBag")
    local view_bag_fac = ViewBagFactory:new(nil, "Bag", "Bag", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Bag", view_bag_fac)
    self:DoString("ViewBank")
    local view_bank_fac = ViewBankFactory:new(nil, "Bank", "Bank", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Bank", view_bank_fac)
    self:DoString("ViewChat")
    local view_chat_fac = ViewChatFactory:new(nil, "Chat", "Chat", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Chat", view_chat_fac)
    self:DoString("ViewChatChooseTarget")
    local view_chatchoose_fac = ViewChatChooseTargetFactory:new(nil, "ChatChooseTarget", "ChatChooseTarget", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ChatChooseTarget", view_chatchoose_fac)
    self:DoString("ViewChatFriend")
    local view_chatfriend_fac = ViewChatFriendFactory:new(nil, "ChatFriend", "ChatFriend", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ChatFriend", view_chatfriend_fac)
    self:DoString("ViewChipOperate")
    local view_chipoperate_fac = ViewChipOperateFactory:new(nil, "ChipOperate", "ChipOperate", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ChipOperate", view_chipoperate_fac)
    self:DoString("ViewChooseLan")
    local view_chooselan_fac = ViewChooseLanFactory:new(nil, "ChooseLan", "ChooseLan", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ChooseLan", view_chooselan_fac)
    self:DoString("ViewShare")
    local view_share_fac = ViewShareFactory:new(nil, "Share", "Share", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Share", view_share_fac)
    self:DoString("ViewShareType")
    local view_sharetype_fac = ViewShareTypeFactory:new(nil, "ShareType", "ShareType", "NomalUi", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ShareType", view_sharetype_fac)
    self:DoString("ViewCreateDesktop")
    local view_createdesktop_fac = ViewCreateDesktopFactory:new(nil, "CreateDeskTop", "CreateDeskTop", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("CreateDeskTop", view_createdesktop_fac)
    self:DoString("ViewDailyReward")
    local view_dailyreward_fac = ViewDailyRewardFactory:new(nil, "DailyReward", "DailyReward", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("DailyReward", view_dailyreward_fac)
    self:DoString("ViewEdit")
    local view_edit_fac = ViewEditFactory:new(nil, "Edit", "Edit", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Edit", view_edit_fac)
    self:DoString("ViewFriend")
    local view_friend_fac = ViewFriendFactory:new(nil, "Friend", "Friend", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Friend", view_friend_fac)
    self:DoString("ViewFriendOnLine")
    local view_friendonline_fac = ViewFriendOnLineFactory:new(nil, "FriendOnLine", "FriendOnLine", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("FriendOnLine", view_friendonline_fac)
    self:DoString("ViewGiftDetail")
    local view_giftdetail_fac = ViewGiftDetailFactory:new(nil, "GiftDetail", "GiftDetail", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("GiftDetail", view_giftdetail_fac)
    self:DoString("ViewGiftShop")
    local view_giftshop_fac = ViewGiftShopFactory:new(nil, "GiftShop", "GiftShop", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("GiftShop", view_giftshop_fac)
    self:DoString("ViewGoldTree")
    local view_goldtree_fac = ViewGoldTreeFactory:new(nil, "GoldTree", "GoldTree", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("GoldTree", view_goldtree_fac)
    self:DoString("ViewInviteFriendPlay")
    local view_invite_fac = ViewInviteFriendPlayFactory:new(nil, "InviteFriendPlay", "InviteFriendPlay", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("InviteFriendPlay", view_invite_fac)
    self:DoString("ViewIdCardCheck")
    local id_check_fac = ViewIdCardCheckFactory:new(nil, "IdCardCheck", "IdCardCheck", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("IdCardCheck", id_check_fac)
    self:DoString("ViewLobby")
    local view_classicmodel_fac = ViewLobbyFactory:new(nil, "ClassicModel", "ClassicModel", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ClassicModel", view_classicmodel_fac)
    self:DoString("ViewMail")
    local view_viewmail_fac = ViewMailFactory:new(nil, "Mail", "Mail", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Mail", view_viewmail_fac)
    self:DoString("ViewMain")
    self:DoString("ViewOnlineReward")
    self:DoString("ViewTimingReward")
    self:DoString("UiMainPlayerInfo")
    local view_viewmain_fac = ViewMainFactory:new(nil, "Main", "Main", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Main", view_viewmain_fac)
    self:DoString("ViewFeedback")
    local view_viewfeedback_fac = ViewFeedbackFactory:new(nil, "Feedback", "Feedback", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Feedback", view_viewfeedback_fac)
    self:DoString("ViewPlayerInfo")
    local view_playerinfo_fac = ViewPlayerInfoFactory:new(nil, "PlayerInfo", "PlayerInfo", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("PlayerInfo", view_playerinfo_fac)
    self:DoString("ViewPlayerProfile")
    local view_playerprofile_fac = ViewPlayerProfileFactory:new(nil, "PlayerProfile", "PlayerProfile", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("PlayerProfile", view_playerprofile_fac)
    self:DoString("ViewActivityCenter")
    local view_activitycenter_fac = ViewActivityCenterFactory:new(nil, "ActivityCenter", "ActivityCenter", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ActivityCenter", view_activitycenter_fac)
    self:DoString("ViewQuitOrBack")
    local view_back_fac = ViewQuitOrBackFactory:new(nil, "QuitOrBack", "QuitOrBack", "QuitGame", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("QuitOrBack", view_back_fac)
    self:DoString("ViewRanking")
    local view_ranking_fac = ViewRankingFactory:new(nil, "Ranking", "Ranking", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Ranking", view_ranking_fac)
    self:DoString("ViewRechargeFirst")
    local view_recharge_fac = ViewRechargeFirstFactory:new(nil, "RechargeFirst", "RechargeFirst", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("RechargeFirst", view_recharge_fac)
    self:DoString("ViewResetPwd")
    local view_resetpwd_fac = ViewResetPwdFactory:new(nil, "ResetPwd", "ResetPwd", "NomalUi", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ResetPwd", view_resetpwd_fac)
    self:DoString("ViewShop")
    local view_shop_fac = ViewShopFactory:new(nil, "Shop", "Shop", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Shop", view_shop_fac)
    self:DoString("ViewPurse")
    local view_purse_fac = ViewPurseFactory:new(nil, "Purse", "Purse", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Purse", view_purse_fac)
    self:DoString("ViewHeadIcon")
    self:DoString("ViewVIPSign")
    self:DoString("ViewHeadIconBig")
    local view_headionbig_fac = ViewHeadIconBigFactory:new(nil, "Common", "CoHeadIconBig", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("HeadIconBig", view_headionbig_fac)
    self:DoString("ViewActivityPopup")
    local view_activity_popup_fac = ViewActivityPopupFactory:new(nil, "ActivityPopup", "ActivityPopup", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ActivityPopup", view_activity_popup_fac)
    self:DoString("ViewMatchLobby")
    local view_matchlobby_fac = ViewMatchLobbyFactory:new(nil, "MatchLobby", "MatchLobby", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("MatchLobby", view_matchlobby_fac)
    self:DoString("ViewApplySucceed")
    local view_applysucceed_fac = ViewApplySucceedFactory:new(nil, "ApplySucceed", "ApplySucceed", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ApplySucceed", view_applysucceed_fac)
    self:DoString("ViewMatchInfo")
    local view_matchinfo_fac = ViewMatchInfoFactory:new(nil, "MatchInfo", "MatchInfo", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("MatchInfo", view_matchinfo_fac)
    self:DoString("ViewClub")
    local view_club_fac = ViewClubFactory:new(nil, "Club", "Club", "Background", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("Club", view_club_fac)
    self:DoString("ViewCreateMatch")
    local view_creatematch = ViewCreateMatchFactory:new(nil, "CreateMatch", "CreateMatch", "NomalUiMain", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("CreateMatch", view_creatematch)
    self:DoString("ViewBlindTable")
    local view_blindtable = ViewBlindTableFactory:new(nil, "BlindTable", "BlindTable", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("BlindTable", view_blindtable)
    self:DoString("ViewClubHelp")
    local view_clubhelp = ViewClubHelpFactory:new(nil, "ClubHelp", "ClubHelp", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("ClubHelp", view_clubhelp)
    self:DoString("ViewJoinMatch")
    local view_joinmatch = ViewJoinMatchFactory:new(nil, "JoinMatch", "JoinMatch", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("JoinMatch", view_joinmatch)
    self:DoString("ViewGetChipEffect")
    local view_getchipeffect = ViewGetChipEffectFactory:new(nil, "GetChipEffect", "GetChipEffect", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("GetChipEffect", view_getchipeffect)
    self:DoString("ViewEnterMatchNotify")
    local view_enterMatchNotify = ViewEnterMatchNotifyFactory:new(nil, "EnterMatchNotify", "EnterMatchNotify", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("EnterMatchNotify", view_enterMatchNotify)
    self:DoString("ViewSnowBallReward")
    local view_snowBallRewardFactory = ViewSnowBallRewardFactory:new(nil, "SnowBallReward", "SnowBallReward", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("SnowBallReward", view_snowBallRewardFactory)
    self:DoString("ViewEditAddress")
    local view_editAddressFactory = ViewEditAddressFactory:new(nil, "EditAddress", "EditAddress", "MessgeBox", true, CS.FairyGUI.FitScreen.FitSize)
    self.ViewMgr:RegView("EditAddress", view_editAddressFactory)
    --Item
    self:DoString("ItemDesktopHBeBankPlayerInfo")
    self:DoString("ItemDesktopHBetOperate")
    self:DoString("ItemDesktopHBetPot")
    self:DoString("ItemDesktopHBetReward")
    self:DoString("ItemDesktopHGameEndPotResult")
    self:DoString("ItemDesktopHGameEndWinPlayer")
    self:DoString("ItemDesktopHHistroy")
    self:DoString("ItemDesktopHSeat")
    self:DoString("ItemDesktopHSetCardType")
    self:DoString("ItemUiDesktopHTongPei")
    self:DoString("ItemUiDesktopHTongSha")
    self:DoString("ItemLotteryTicketBetOperate")
    self:DoString("ItemLotteryTicketBetPot")
    self:DoString("ItemLotteryTicketCard")
    self:DoString("ItemLotteryTicketHistory")
    self:DoString("ItemLotteryTicketRewardPotPlayerInfo")
    self:DoString("ItemAttachment")
    self:DoString("ItemBetChipRange")
    self:DoString("ItemChatEx")
    self:DoString("ItemChatPresetMsg")
    self:DoString("ItemDailyReward")
    self:DoString("ItemDesktopHintsInfo")
    self:DoString("ItemDesktopRaiseOperateGFlower")
    self:DoString("ItemHeadIcon")
    self:DoString("ItemHeadIconWithNickName")
    self:DoString("ItemMagicExpIcon")
    self:DoString("ItemMagicExpSender")
    self:DoString("ItemMail")
    self:DoString("ItemNotice")
    self:DoString("ItemNumOperate")
    self:DoString("ItemPlayerChatLock")
    self:DoString("ItemPlayerReport")
    self:DoString("ItemChatDesktop")
    self:DoString("ItemCountryCode")
    self:DoString("ItemChatTargetInfo")
    self:DoString("ItemChooseChatTargetInfo")
    self:DoString("ItemDesktopHRewardPotPlayerInfo")
    self:DoString("ItemAnte")
    self:DoString("ItemGift")
    self:DoString("ItemGiftType")
    self:DoString("ItemInviteFriendPlay")
    self:DoString("ItemLan")
    self:DoString("ItemLobbyDesk")
    self:DoString("ItemLobbyDeskPlayInfo")
    self:DoString("ItemMainOperate")
    self:DoString("ItemRank")
    self:DoString("ItemReportPlayerOperate")
    self:DoString("ItemShowFriendSimple")
    self:DoString("ItemUiShopConsume")
    self:DoString("ItemUiShopDiamond")
    self:DoString("ItemUiShopGold")
    self:DoString("ItemUiShopVIPInfo")
    self:DoString("ItemUiShopVIPInfo")
    self:DoString("ItemVIPBuyInfo")
    self:DoString("ItemActivity")
    self:DoString("ItemActivityTitle")
    self:DoString("ItemUiShopGoods")
    self:DoString("ItemUiPurseGold")
    self:DoString("ItemMatchType")
    self:DoString("ItemMatchInfo")
    self:DoString("ItemBlind")
    self:DoString("ItemMatchInfoBlind")
    self:DoString("ItemMatchInfoReward")
    self:DoString("ItemSnowBallRewardPlayerNum")
    self:DoString("ItemSnowBallRewardInfo")
    self:DoString("ItemMatchInfoRank")
end

---------------------------------------
function Context:_regTbData()
    self:DoString("TbDataHintsInfoTexas")
    self.TbDataMgr:RegTbDataFac("HintsInfoTexas", TbDataFactoryHintsInfoTexas:new(nil))
    self:DoString("TbDataActorLevel")
    self.TbDataMgr:RegTbDataFac("ActorLevel", TbDataFactoryActorLevel:new(nil))
    self:DoString("TbDataCommon")
    self.TbDataMgr:RegTbDataFac("Common", TbDataFactoryCommon:new(nil))
    self:DoString("TbDataDailyReward")
    self.TbDataMgr:RegTbDataFac("DailyReward", TbDataFactoryDailyReward:new(nil))
    self:DoString("TbDataExpression")
    self.TbDataMgr:RegTbDataFac("Expression", TbDataFactoryExpression:new(nil))
    self:DoString("TbDataItem")
    self.TbDataMgr:RegTbDataFac("Item", TbDataFactoryItem:new(nil))
    self:DoString("TbDataItemType")
    self.TbDataMgr:RegTbDataFac("ItemType", TbDataFactoryItemType:new(nil))
    self:DoString("TbDataLanEn")
    self.TbDataMgr:RegTbDataFac("LanEn", TbDataFactoryLanEn:new(nil))
    self:DoString("TbDataLans")
    self.TbDataMgr:RegTbDataFac("Lans", TbDataFactoryLans:new(nil))
    self:DoString("TbDataLanZh")
    self.TbDataMgr:RegTbDataFac("LanZh", TbDataFactoryLanZh:new(nil))
    self:DoString("TbDataLotteryTicket")
    self.TbDataMgr:RegTbDataFac("LotteryTicket", TbDataFactoryLotteryTicket:new(nil))
    self:DoString("TbDataLotteryTicketBetOperate")
    self.TbDataMgr:RegTbDataFac("LotteryTicketBetOperate", TbDataFactoryLotteryTicketBetOperate:new(nil))
    self:DoString("TbDataLotteryTicketGoldPercent")
    self.TbDataMgr:RegTbDataFac("LotteryTicketGoldPercent", TbDataFactoryLotteryTicketGoldPercent:new(nil))
    self:DoString("TbDataOnlineReward")
    self.TbDataMgr:RegTbDataFac("OnlineReward", TbDataFactoryOnlineReward:new(nil))
    self:DoString("TbDataPresetMsg")
    self.TbDataMgr:RegTbDataFac("PresetMsg", TbDataFactoryPresetMsg:new(nil))
    self:DoString("TbDataUnitBilling")
    self.TbDataMgr:RegTbDataFac("UnitBilling", TbDataFactoryUnitBilling:new(nil))
    self:DoString("TbDataUnitConsume")
    self.TbDataMgr:RegTbDataFac("UnitConsume", TbDataFactoryUnitConsume:new(nil))
    self:DoString("TbDataUnitGoodsVoucher")
    self.TbDataMgr:RegTbDataFac("UnitGoodsVoucher", TbDataFactoryUnitGoodsVoucher:new(nil))
    self:DoString("TbDataUnitGoldPackage")
    self.TbDataMgr:RegTbDataFac("UnitGoldPackage", TbDataFactoryUnitGoldPackage:new(nil))
    self:DoString("TbDataUnitGiftNormal")
    self.TbDataMgr:RegTbDataFac("UnitGiftNormal", TbDataFactoryUnitGiftNormal:new(nil))
    self:DoString("TbDataUnitRedEnvelopes")
    self.TbDataMgr:RegTbDataFac("UnitRedEnvelopes", TbDataFactoryUnitRedEnvelopes:new(nil))
    self:DoString("TbDataUnitGiftTmp")
    self.TbDataMgr:RegTbDataFac("UnitGiftTmp", TbDataFactoryUnitGiftTmp:new(nil))
    self:DoString("TbDataUnitMagicExpression")
    self.TbDataMgr:RegTbDataFac("UnitMagicExpression", TbDataFactoryUnitMagicExpression:new(nil))
    self:DoString("TbDataVIPLevel")
    self.TbDataMgr:RegTbDataFac("VIPLevel", TbDataFactoryVIPLevel:new(nil))
    self:DoString("TbDataConfigTexasDesktop")
    self.TbDataMgr:RegTbDataFac("ConfigTexasDesktop", TbDataFactoryConfigTexasDesktop:new(nil))
    self:DoString("TbDataDesktopInfoTexas")
    self.TbDataMgr:RegTbDataFac("DesktopInfoTexas", TbDataFactoryDesktopInfoTexas:new(nil))
    self:DoString("TbDataCfigTexasDesktopH")
    self.TbDataMgr:RegTbDataFac("CfigTexasDesktopH", TbDataFactoryCfigTexasDesktopH:new(nil))
    self:DoString("TbDataCfigTexasDesktopHGoldPercent")
    self.TbDataMgr:RegTbDataFac("CfigTexasDesktopHGoldPercent", TbDataFactoryCfigTexasDesktopHGoldPercent:new(nil))
    self:DoString("TbDataCfigTexasDesktopHSysBank")
    self.TbDataMgr:RegTbDataFac("CfigTexasDesktopHSysBank", TbDataFactoryCfigTexasDesktopHSysBank:new(nil))
    self:DoString("TbDataDesktopHBetOperateTexas")
    self.TbDataMgr:RegTbDataFac("DesktopHBetOperateTexas", TbDataFactoryDesktopHBetOperateTexas:new(nil))
    self:DoString("TbDataDesktopHBetPotTexas")
    self.TbDataMgr:RegTbDataFac("DesktopHBetPotTexas", TbDataFactoryDesktopHBetPotTexas:new(nil))
    self:DoString("TbDataDesktopHInfoTexas")
    self.TbDataMgr:RegTbDataFac("DesktopHInfoTexas", TbDataFactoryDesktopHInfoTexas:new(nil))
    self:DoString("TbDataDesktopHBetReward")
    self.TbDataMgr:RegTbDataFac("DesktopHBetReward", TbDataFactoryDesktopHBetReward:new(nil))
    self:DoString("TbDataTexasRaiseBlinds")
    self.TbDataMgr:RegTbDataFac("TexasRaiseBlinds", TbDataFactoryTexasRaiseBlinds:new(nil))
    self:DoString("TbDataDesktopFastBet")
    self.TbDataMgr:RegTbDataFac("DesktopFastBet", TbDataFactoryDesktopFastBet:new(nil))
    self:DoString("TbDataTexasSnowBallRewardPlayerNum")
    self.TbDataMgr:RegTbDataFac("TexasSnowBallRewardPlayerNum", TbDataFactoryTexasSnowBallRewardPlayerNum:new(nil))
    self:DoString("TbDataTexasSnowBallRewardInfo")
    self.TbDataMgr:RegTbDataFac("TexasSnowBallRewardInfo", TbDataFactoryTexasSnowBallRewardInfo:new(nil))
end

---------------------------------------
-- 计算BotIcon Url
function Context:CalcBotIconUrl(is_small, icon)
    if (is_small == true) then
        return BotIconDomain .. 'boticonsmall/' .. icon .. '.jpg'
    else
        return BotIconDomain .. 'boticon/' .. icon .. '.jpg'
    end
end