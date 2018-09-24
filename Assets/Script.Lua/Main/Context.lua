-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
--BundleUpdateURL = ''
--BundleVersionState = ''
--CommonLuaVersion = '1.00.652'
--CommonLuaRootURL = 'cragon-lua-oss.cragon.cn/1.00.652/'
--CommonLuaHelper = 'Helper/LuaHelper.lua'
--CommonWWWLoader = 'Helper/WWWLoader.lua'
--CommonLuaLoaderPath = 'Helper/CommonLuaLoader.lua'
--ProjectDataLoaderPath = 'Helper/ProjectDataLoader.lua'
--CommonLuaFileListTxtName = 'CommonLuaFileList.txt'
--DBListCommon = 'KingCommon|KingDesktop|KingDesktopH'
--DBListServer = 'KingServer'
--DBListClient = 'KingClient'
UCenterDomain = 'ucenter.cragon.cn'
AutopatcherUrl = 'cragon-king-oss.cragon.cn/autopatcher/VersionInfo.xml'
PlayerIconDomain = 'cragon-king-oss.cragon.cn/images/'
BotIconDomain = 'cragon-king-oss.cragon.cn/ucenter/'
SysNoticeInfoUrl = ''
GatewayIp = 'king-gateway.cragon.cn'
GatewayPort = 5882
BundleUpdateStata = 0
BundleUpdateVersion = '1.00.067'
BundleUpdateURL = 'https://cragon-king-oss.cragon.cn/KingTexas.apk'
DataVersion = '1.00.605'
DataRootURL = 'https://cragon-king-oss.cragon.cn/ANDROID/Data_1.00.605/'
DataFileListFileName = 'datafilelist.txt'
ServerState = 0 --服务器状态:0正常；1维护
SysNotice = '' --系统公告
ClientShowFPS = false -- 客户端显示FPS信息 false 不显示 true 显示
FPSLimit = 60
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
BeeCloudTestSecret = '7bbc79a8-f310-4d76-a582-2622242c23f5'
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
    self.LaunchStep = {}
    return o
end

---------------------------------------
function Context:Init()
    Context:new(nil)
    print('Context:Init()')

    -- 销毁所有资源，因为可以从Login返回到Launch
    -- Esc可以弹出退出确认对话框，随时退出

    Context:_initLaunchStep()
    Context:_nextLaunchStep()

    -- 弹框让玩家选择，更新Bundle，调用Native Api安装Bundle
end

---------------------------------------
function Context:Release()
    if (self.TimerUpdateCopyStreamingAssetsToPersistentData ~= nil)
    then
        self.TimerUpdateCopyStreamingAssetsToPersistentData:Close()
        self.TimerUpdateCopyStreamingAssetsToPersistentData = nil
    end
    print('Context:Release()')
end

---------------------------------------
-- 初始化LaunchStep
function Context:_initLaunchStep()
    -- 检测Bundle是否需要更新
    if (BundleUpdateStata == 1 and BundleUpdateVersion ~= nil and BundleUpdateURL ~= nil and self.CasinosContext.Config.VersionBundle ~= BundleUpdateVersion)
    then
        self.LaunchStep[1] = "UpdateBundle"
    end

    -- 检测是否需要首次运行解压
    self.LaunchStep[2] = "CopyStreamingAssetsToPersistentData"

    -- 检测是否需要更新Data
    if (DataVersion ~= self.CasinosContext.Config.VersionDataPersistent)
    then
        self.LaunchStep[3] = "UpdateData"
    end

    -- 进入Login界面
    self.LaunchStep[4] = "ShowLogin"
end

---------------------------------------
-- 执行下一步LaunchStep
function Context:_nextLaunchStep()
    -- 更新Bundle
    if (self.LaunchStep[1] ~= nil)
    then
        return
    end

    -- 首次运行解压
    if (self.LaunchStep[2] ~= nil)
    then
        local desc_copy = "首次运行解压资源，不消耗流量"
        self.Launch.PreLoading:UpdateDesc(desc_copy)
        Context.Launch.PreLoading:UpdateLoadingProgress(0, 100)
        if (self.CopyStreamingAssetsToPersistentData == nil)
        then
            self.CopyStreamingAssetsToPersistentData = CS.Casinos.CopyStreamingAssetsToPersistentData2()
            self.CopyStreamingAssetsToPersistentData:CopyAsync('')
        end
        self.TimerUpdateCopyStreamingAssetsToPersistentData = self.CasinosContext.TimerShaft:RegisterTimer(30, self._timerUpdateCopyStreamingAssetsToPersistentData)
        return
    end

    -- 更新Data
    if (self.LaunchStep[3] ~= nil)
    then
        local desc_copy = "更新游戏数据"
        self.Launch.PreLoading:UpdateDesc(desc_copy)
        Context.Launch.PreLoading:UpdateLoadingProgress(0, 100)
        local http_url = DataRootURL .. DataFileListFileName
        print(http_url)
        local async_asset_loadgroup = CS.Casinos.CasinosContext.Instance.AsyncAssetLoadGroup
        async_asset_loadgroup:LoadWWWAsync(http_url,
                function(url, www)
                    -- 比较Oss上的datafilelist.txt和Persistent中的datafilelist.txt差异集，获取需要更新的Data列表
                    local datafilelist_persistent = self.CasinosContext.PathMgr:combinePersistentDataPath(DataFileListFileName)
                    print(datafilelist_persistent)
                    self.RemoteDataFileListContent = www.text
                    local persistent_datafilelist_content = self.CasinosLua:ReadAllText(datafilelist_persistent)
                    self.UpdateRemoteToPersistentData = CS.Casinos.UpdateRemoteToPersistentData()
                    local update_data_count = self.UpdateRemoteToPersistentData:UpateAsync(self.RemoteDataFileListContent, persistent_datafilelist_content, DataRootURL)
                    print('需更新Data数量：' .. update_data_count)
                    self.TimerUpdateRemoteToPersistentData = self.CasinosContext.TimerShaft:RegisterTimer(30, self._timerUpdateRemoteToPersistentData)
                end
        )
        return
    end

    -- 卸载Launch，加载并显示Login
    if (self.LaunchStep[4] ~= nil)
    then
        self.LaunchStep[4] = nil

        local desc_copy = "准备进入登录界面"
        self.Launch.PreLoading:UpdateDesc(desc_copy)
        self.Launch.PreLoading:UpdateLoadingProgress(100, 100)

        local path_lua_root = self.CasinosContext.PathMgr:combinePersistentDataPath("Script.Lua/");
        self.CasinosLua:LoadLuaFromDir(path_lua_root);

        -- 加载登录界面

    end
end

---------------------------------------
-- 定时器，首次运行解压。被C#回调，没有传递self。
function Context:_timerUpdateCopyStreamingAssetsToPersistentData()
    local is_done = Context.CopyStreamingAssetsToPersistentData:IsDone()
    if (is_done)
    then
        Context.TimerUpdateCopyStreamingAssetsToPersistentData:Close()
        Context.TimerUpdateCopyStreamingAssetsToPersistentData = nil
        Context.CopyStreamingAssetsToPersistentData = nil

        -- 执行下一步LaunchStep
        Context.LaunchStep[2] = nil
        Context:_nextLaunchStep()
    else
        local value = Context.CopyStreamingAssetsToPersistentData.LeftCount
        local max = Context.CopyStreamingAssetsToPersistentData.TotalCount
        Context.Launch.PreLoading:UpdateLoadingProgress(max - value, max)
    end
end

---------------------------------------
-- 定时器，更新Data。被C#回调，没有传递self。
function Context:_timerUpdateRemoteToPersistentData()
    local is_done = Context.UpdateRemoteToPersistentData:IsDone()
    if (is_done)
    then
        Context.TimerUpdateRemoteToPersistentData:Close()
        Context.TimerUpdateRemoteToPersistentData = nil
        Context.UpdateRemoteToPersistentData = nil

        -- 用Remote DataFileList.txt覆盖Persistent中的；并更新VersionDataPersistent
        local datafilelist_persistent = Context.CasinosContext.PathMgr:combinePersistentDataPath(DataFileListFileName)
        CS.System.IO.File.WriteAllText(datafilelist_persistent, Context.RemoteDataFileListContent)
        Context.RemoteDataFileListContent = nil
        Context.CasinosContext.Config:WriteVersionDataPersistent(DataVersion)

        -- 执行下一步LaunchStep
        Context.LaunchStep[3] = nil
        Context:_nextLaunchStep()
    else
        local value = Context.UpdateRemoteToPersistentData.LeftCount
        local max = Context.UpdateRemoteToPersistentData.TotalCount
        Context.Launch.PreLoading:UpdateLoadingProgress(max - value, max)
    end
end

---------------------------------------
-- 字符串分隔方法
function string:split(sep)
    local sep, fields = sep or ":", {}
    local pattern = string.format("([^%s]+)", sep)
    self:gsub(pattern, function (c) fields[#fields + 1] = c end)
    return fields
end

---------------------------------------
-- 打印Table
function PrintTable(t)
    local print_r_cache = {}
    local function sub_print_r(t, indent)
        if (print_r_cache[tostring(t)]) then
            print(indent .. "*" .. tostring(t))
        else
            print_r_cache[tostring(t)] = true
            if (type(t) == "table") then
                for pos, val in pairs(t) do
                    if (type(val) == "table") then
                        print(indent .. "[" .. pos .. "] => " .. tostring(t) .. " {")
                        sub_print_r(val, indent .. string.rep(" ", string.len(pos) + 8))
                        print(indent .. string.rep(" ", string.len(pos) + 6) .. "}")
                    elseif (type(val) == "string") then
                        print(indent .. "[" .. pos .. '] => "' .. val .. '"')
                    else
                        print(indent .. "[" .. pos .. "] => " .. tostring(val))
                    end
                end
            else
                print(indent .. tostring(t))
            end
        end
    end
    if (type(t) == "table") then
        print(tostring(t) .. " {")
        sub_print_r(t, "  ")
        print("}")
    else
        sub_print_r(t, "  ")
    end
    print()
end