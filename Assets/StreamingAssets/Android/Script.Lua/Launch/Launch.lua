-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
Launch = {}

---------------------------------------
function Launch:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self

    if(self.Instance==nil)
    then
        self.ControllerName = "Launch"
        self.LaunchConfigLoader = nil
        self.CanCheckLoadDataDone = false
        self.PreViewLoadDone = false
        self.InitializeDone = false
        self.Instance = o
    end

    return self.Instance
end

---------------------------------------
function Launch:Init()
    print("Launch:Init()")

    require 'PreViewMgr'
    require 'PreViewBase'
    require 'PreViewFactory'
    require 'PreViewLoading'
    require 'PreViewMsgBox'

    --casinos_context.CasinosLua:DoString("PreViewMgr")
    --casinos_context:setPreViewMgr()

    local launch = Launch:new(nil)
    launch.PreViewMgr = PreViewMgr:new(nil)
    launch.PreViewMgr:Init()

    local casinos_context = CS.Casinos.CasinosContext.Instance
    --local launch_persistentdata_path = casinos_context.PathMgr:combinePersistentDataPath(casinos_context.ResourcesRowPathRoot .. "Launch/")
    local launch_persistentdata_path = casinos_context.PathMgr.PathLaunchRootPersistent

    local ui_name_loading = "PreLoading"
    local ui_path_loading = launch_persistentdata_path .. ui_name_loading .. "/" .. string.lower(ui_name_loading) .. ".ab"
    local ui_name_msgbox = "PreMsgBox"
    local ui_path_msgbox = launch_persistentdata_path .. ui_name_msgbox .. "/" .. string.lower(ui_name_msgbox) .. ".ab"
    casinos_context:LuaAsyncLoadLocalUiBundle(
            function()
                launch:_loadABDone()
            end
    ,ui_path_loading,ui_path_msgbox)
end

---------------------------------------
function Launch:Release()
    print("Launch:Release()")
end

---------------------------------------
function Launch:Update(tm)
    local launch = Launch:new(nil)
    if (launch.LaunchConfigLoader ~= nil)
    then
        launch.LaunchConfigLoader:update(tm)
    end

    if (launch.CanCheckLoadDataDone)
    then
        local casinos_context = CS.Casinos.CasinosContext.Instance
        local load_datadone = casinos_context.LoadDataDone
        if (load_datadone==true and launch.PreViewLoadDone == true)
        then
            launch.CanCheckLoadDataDone = false

            local tips = "正在玩命加载美术资源，稍后即可进入游戏..."
            local lan = CS.Casinos.CasinosContext.Instance.CurrentLan
            if(lan == "English")
            then
                tips = "Loading art resources,can enter game soon..."
            else
                if(lan == "Chinese" or lan == "ChineseSimplified")
                then
                    tips = "正在玩命加载美术资源，稍后即可进入游戏..."
                end
            end

            local view_preloading = launch.PreViewMgr.createView("PreLoading")
            view_preloading.setTip(tips)
            casinos_context.Listener:OnInitializeBegin(
                function ()
                    launch:_initializeDone()
                end)
        end
    end
end

---------------------------------------
function Launch:onHandleEv(ev)
end

---------------------------------------
function Launch:getModle()
end

---------------------------------------
function Launch:CopyStreamingAssetsToPersistentDataPath()
    local controller_launch = Launch:new(nil)
    local tips = "首次进入游戏，解压资源，该过程不产生任何流量"
    local lan = CS.Casinos.CasinosContext.Instance.CurrentLan
    if(lan == "English")
    then
        tips = "First game,we need to copy some resources,it's not need any network flow."
    else
        if(lan == "Chinese" or lan == "ChineseSimplified")
        then
            tips = "首次进入游戏，解压资源，该过程不产生任何流量"
        end
    end
    local view_preloading = controller_launch.PreViewMgr.createView("PreLoading")
    view_preloading.setTip(tips)

    local launch = CS.Casinos.CasinosContext.Instance.Launch
    if (launch.ParseStreamingAssetsDataInfo ~= nil)
    then
        CS.Casinos.CasinosContext.Instance.CopyStreamingAssetsToPersistentDataPath:startCopy(launch.ParseStreamingAssetsDataInfo.ListData,
                function(current_index, total_count)
                    controller_launch:_firstCopyStreamingAssetsDataPro(current_index, total_count)
                end,
                Launch._firstCopyStreamingAssetsDataDown)
    end
end

---------------------------------------
function Launch:_loadABDone()
    local view_preloading = self.PreViewMgr.createView("PreLoading")
    local tips = "正在努力加载配置，请耐心等待..."
    local lan = CS.Casinos.CasinosContext.Instance.CurrentLan
    if(lan == "English")
    then
        tips = "Try to loading the config,please wait..."
    else
        if(lan == "Chinese" or lan == "ChineseSimplified")
        then
            tips = "正在努力加载配置，请耐心等待..."
        end
    end
    view_preloading.setTip(tips)
    self.PreViewLoadDone = true
    self.LaunchConfigLoader = CS.Casinos.LaunchConfigLoader()
    local maic_path = CS.Casinos.CasinosContext.Instance:GetMainCPath()
    self.LaunchConfigLoader:loadConfig(maic_path,
            function(config)
                self:_loadConfigDone(config)
            end
    )
end

---------------------------------------
function Launch:_firstCopyStreamingAssetsDataPro(current_index, total_count)
    -- local launch = Launch:new(nil)
    local pro = current_index / total_count
    local view_preloading = self.PreViewMgr.createView("PreLoading")
    view_preloading.setLoadingProgress(pro * 100)
end

---------------------------------------
function Launch._firstCopyStreamingAssetsDataDown()
    local launch = CS.Casinos.CasinosContext.Instance.Launch
    launch.ParseStreamingAssetsDataInfo:writeStreamingAssetsDataFileList2Persistent()
    CS.UnityEngine.PlayerPrefs.SetString(CS.Casinos.CasinosContext.LocalDataVersionKey,
            CS.Casinos.CasinosContext.Instance.Config.InitDataVersion)
    launch.ParseStreamingAssetsDataInfo = nil
end

---------------------------------------
function Launch:_loadConfigDone(config)
    -- local launch = Launch:new(nil)
    local casinos_context = CS.Casinos.CasinosContext.Instance
    casinos_context.CasinosLua:addLuaFile(casinos_context.Config.ConfigName, config)
    casinos_context.CasinosLua:doMainCLua(casinos_context.Config.ConfigName)
    casinos_context.MainCLua = MainC
    self.CanCheckLoadDataDone = true
end

---------------------------------------
function Launch:_initializeDone()
    self.InitializeDone = true
    CS.Casinos.CasinosContext.Instance.Listener:OnInitializeEnd()
end