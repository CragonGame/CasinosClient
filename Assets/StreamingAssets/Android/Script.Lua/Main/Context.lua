-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UCenterDomain = 'ucenter.cragon.cn'
AutopatcherUrl = 'cragon-king-oss.cragon.cn/autopatcher/VersionInfo.xml'
PlayerIconDomain = 'cragon-king-oss.cragon.cn/images/'
BotIconDomain = 'cragon-king-oss.cragon.cn/ucenter/'
SysNoticeInfoUrl = ''
GatewayIp = 'king-gateway.cragon.cn'
GatewayPort = 5882
BundleVersion = '1.00.067'
BundleURL = 'http://cragon-king.oss-cn-shanghai.aliyuncs.com/ANDROID/1.05.032/King.apk'
BundleUpdateURL = ''
BundleVersionState = ''
CommonLuaVersion = '1.00.652'
CommonLuaRootURL = 'cragon-lua-oss.cragon.cn/1.00.652/'
CommonLuaHelper = 'Helper/LuaHelper.lua'
CommonWWWLoader = 'Helper/WWWLoader.lua'
CommonLuaLoaderPath = 'Helper/CommonLuaLoader.lua'
ProjectDataLoaderPath = 'Helper/ProjectDataLoader.lua'
CommonLuaFileListTxtName = 'CommonLuaFileList.txt'
DataVersion = '1.00.601'
ProjectDataRootURL = 'cragon-king-oss.cragon.cn/ANDROID/Data/DataVersion_1.00.601/'
DataFileListTextName = 'DataFileList.txt'
DBListCommon = 'KingCommon|KingDesktop|KingDesktopH'
DBListServer = 'KingServer'
DBListClient = 'KingClient'
ServerState = 0 --服务器状态:0正常；1维护
SysNotice = '' --系统公告
ClientShowFPS = false -- 客户端显示FPS信息 false 不显示 true 显示
FPSLimit = 61
ClientShowWeiChat = true -- 客户端显示微信登录按钮 false 不显示 true 显示
ClientShowFirstRecharge = true -- 客户端显示首充按钮 false 不显示 true 显示
NeedHideClientUi = false -- 客户端排行等界面显示与隐藏
DesktopHSysBankShowDBValue = true  --百人系统庄是否显示SQlite配置值
ShootingTextShowVIPLimit = 0 --弹幕发送后是否真正发送弹幕VIP等级限制，0为无限制
DesktopHCanChatVIPLimit = 1 -- 百人是否可聊天VIP等级限制，0为无限制
DesktopCanChatVIPLimit = 0 -- 普通桌是否可聊天VIP等级限制，0为无限制
CanReportLog = false -- 是否开启上传日志到Bugly后台
CanReportLogDeviceId = "" -- 可以上传的机器码
CanReportLogPlayerId = "" -- 可以上传的玩家Id
ShowGoldTree = false
UseWeiChatPay = true
UseALiPay = true
UseIAP = true
UseLan = true
UseDefaultLan = false
DefaultLan = 'Chinese'
ChipIconSolustion = 0
CurrentMoneyType = 0
CSharpLastMethodId = 0
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
BeeCloudTestSecret= '7bbc79a8-f310-4d76-a582-2622242c23f5'
PayUseTestMode = false
PayUrlScheme = "com.Cragon.KingTexas2"

---------------------------------------
Context = {}

---------------------------------------
function Context:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.CasinosLua = CS.Casinos.CasinosContext.Instance.CasinosLua
    self.Launch = Launch
    return o
end

---------------------------------------
function Context:Init()
    Context:new(nil)
    print('Context:Init()')

    local desc = "首次运行解压资源，不消耗流量"
    self.Launch.PreLoading:UpdateDesc(desc)
    if(self.CopyStreamingAssetsToPersistentData == nil)
    then
        self.CopyStreamingAssetsToPersistentData = CS.Casinos.CopyStreamingAssetsToPersistentData2()
        self.CopyStreamingAssetsToPersistentData:CopyAsync('')
    end
    self.TimerUpdateCopyStreamingAssetsToPersistentData = self.CasinosContext.TimerShaft:RegisterTimer(30, self._timerUpdateCopyStreamingAssetsToPersistentData)

    -- 销毁所有资源，因为可以从Login返回到Launch
    -- Esc可以弹出退出确认对话框，随时退出
    -- 检测Bundle是否需要更新
    -- 弹框让玩家选择，更新Bundle
    -- 检测是否需要首次运行解压
    -- 首次运行解压
    -- 检测是否需要更新Data
    -- 更新Data
    -- 卸载Launch
    -- 加载并显示Login
end

---------------------------------------
function Context:Release()
    if(self.TimerUpdateCopyStreamingAssetsToPersistentData ~= nil)
    then
        self.TimerUpdateCopyStreamingAssetsToPersistentData:Close()
        self.TimerUpdateCopyStreamingAssetsToPersistentData = nil
    end
    print('Context:Release()')
end

---------------------------------------
-- 被C#回调，没有传递self
function Context:_timerUpdateCopyStreamingAssetsToPersistentData()
    local is_done = Context.CopyStreamingAssetsToPersistentData:IsDone()
    if(is_done)
    then
        Context.TimerUpdateCopyStreamingAssetsToPersistentData:Close()
        Context.TimerUpdateCopyStreamingAssetsToPersistentData = nil
        Context.CopyStreamingAssetsToPersistentData = nil
    else
        local value = Context.CopyStreamingAssetsToPersistentData.LeftCount
        local max = Context.CopyStreamingAssetsToPersistentData.TotalCount
        Context.Launch.PreLoading:UpdateLoadingProgress(max - value, max)
    end
end