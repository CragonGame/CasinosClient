-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
Launch = {}

---------------------------------------
function Launch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil) then
        self.PreViewMgr = nil
        self.PreLoading = nil
        self.PreMsgBox = nil
        self.CasinosContext = CS.Casinos.CasinosContext.Instance
        self.CasinosLua = CS.Casinos.CasinosContext.Instance.CasinosLua
        self.Context = nil
        self.UIPackagePreLoading = nil
        self.UIPackageMsgbox = nil
        self.Instance = o
    end
    return self.Instance
end

---------------------------------------
-- 初始化
function Launch:Setup()
    Launch:new(nil)
    print('Launch:Setup()')

    require 'PreViewMgr'
    require 'PreViewBase'
    require 'PreViewFactory'
    require 'PreViewLoading'
    require 'PreViewMsgBox'

    self.PreViewMgr = PreViewMgr:new(nil)
    self.PreViewMgr:Init()

    local ui_name_loading = "PreLoading"
    local ui_name_msgbox = "PreMsgBox"
    local launch_persistentdata_path = self.CasinosContext.PathMgr.PathLaunchRootPersistent
    local ui_path_loading = launch_persistentdata_path .. string.lower(ui_name_loading) .. ".ab"
    local ui_path_msgbox = launch_persistentdata_path .. string.lower(ui_name_msgbox) .. ".ab"
    local ab_preloading = CS.UnityEngine.AssetBundle.LoadFromFile(ui_path_loading)
    local ab_premsgbox = CS.UnityEngine.AssetBundle.LoadFromFile(ui_path_msgbox)
    self.UIPackagePreLoading = CS.FairyGUI.UIPackage.AddPackage(ab_preloading)
    self.UIPackagePreMsgbox = CS.FairyGUI.UIPackage.AddPackage(ab_premsgbox)

    self.PreLoading = self.PreViewMgr:createView("PreLoading")

    local tips = "正在努力加载配置，请耐心等待..."
    local lan = self.CasinosContext.CurrentLan
    if (lan == "English") then
        tips = "Try to loading the config,please wait..."
    else
        if (lan == "Chinese" or lan == "ChineseSimplified") then
            tips = "正在努力加载配置，请耐心等待..."
        end
    end
    self.PreLoading:UpdateDesc(tips)

    -- 下载并加载Context.lua
    local http_url = string.format('https://cragon-king-oss.cragon.cn/%s/Bundle_%s/Context.lua',
            self.CasinosContext.Config.Platform, self.CasinosContext.Config.VersionBundle)
    print(http_url)
    local async_asset_loadgroup = CS.Casinos.CasinosContext.Instance.AsyncAssetLoadGroup
    async_asset_loadgroup:LoadWWWAsync(http_url,
            function(url, www)
                self.CasinosLua:LoadLuaFromBytes('Context', www.text)
                require 'Context'
                self.Context = Context
                self.Context:Init()
            end
    )
end

---------------------------------------
-- Launch阶段完成
function Launch:Finish()
    if (self.PreViewMgr ~= nil) then
        self.PreViewMgr:Release()
        self.PreViewMgr = nil
        self.PreLoading = nil
        self.PreMsgBox = nil
    end

    package.preload['PreViewMsgBox'] = nil
    package.loaded['PreViewMsgBox'] = nil
    package.preload['PreViewLoading'] = nil
    package.loaded['PreViewLoading'] = nil
    package.preload['PreViewMgr'] = nil
    package.loaded['PreViewMgr'] = nil
    package.preload['PreViewFactory'] = nil
    package.loaded['PreViewFactory'] = nil
    package.preload['PreViewBase'] = nil
    package.loaded['PreViewBase'] = nil
    package.preload['ParticleHelper'] = nil
    package.loaded['ParticleHelper'] = nil

    if (self.UIPackagePreLoading ~= nil) then
        self.UIPackagePreLoading:UnloadAssets()
        self.UIPackagePreLoading = nil
    end
    if (self.UIPackageMsgbox ~= nil) then
        self.UIPackageMsgbox:UnloadAssets()
        self.UIPackageMsgbox = nil
    end

    print("Launch:Finish()")
end

---------------------------------------
-- 应用程序退出
function Launch:Close()
    if (self.Context ~= nil) then
        self.Context:Release()
        self.Context = nil
    end

    self:Finish()

    self.CasinosContext = nil
    self.CasinosLua = nil
    self.Instance = nil

    print("Launch:Release()")
end

--local view_premsgbox = self.PreViewMgr.createView("PreMsgBox")
--view_premsgbox:showMsgBox(self.CasinosContext.Config.VersionBundle,
--        function ()
--            print('ok')
--        end
--)

--self.CasinosContext:LuaAsyncLoadLocalUiBundle(
--        function()
--            Launch:_loadABPreLoadingDone()
--        end,
--        ui_path_loading, ui_path_msgbox)