-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
Launch = {}

---------------------------------------
function Launch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil) then
        self.CurrentLan = 'ChineseSimplified'
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
    --print('Launch:Setup()')

    self:_checkCurrentLan()

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
    local lan = CurrentLan
    if (lan == "English") then
        tips = "Try to loading the config,please wait..."
    else
        if (lan == "Chinese" or lan == "ChineseSimplified") then
            tips = "正在努力加载配置，请耐心等待..."
        end
    end
    self.PreLoading:UpdateDesc(tips)

    -- 下载并加载bundle_xxx.lua
    local lua_pkg_name = string.format('bundle_%s', self.CasinosContext.Config.VersionBundle)
    local async_asset_loadgroup = self.CasinosContext.AsyncAssetLoadGroup
    local http_url_bundlexxx = string.format('https://cragon-king-oss.cragon.cn/%s/%s.lua',
            self.CasinosContext.Config.Platform, lua_pkg_name)
    --print(http_url_bundlexxx)
    async_asset_loadgroup:LoadWWWAsync(http_url_bundlexxx,
            function(url, www)
                self.CasinosLua:LoadLuaFromBytes(lua_pkg_name, www.text)
                self.CasinosLua:DoString(lua_pkg_name)

                -- 下载并加载Context.lua
                local http_url_context = string.format('https://cragon-king-oss.cragon.cn/%s/Data_%s/Context.lua',
                        self.CasinosContext.Config.Platform, DataSelect)
                --print(http_url_context)
                async_asset_loadgroup:LoadWWWAsync(http_url_context,
                        function(url, www)
                            self.CasinosLua:LoadLuaFromBytes('Context', www.text)
                            self.CasinosLua:DoString('Context')
                            self.Context = Context
                            self.Context:Init()
                        end
                )
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

---------------------------------------
function Launch:_checkCurrentLan()
    local lan_key = "LanKey"
    if (CS.UnityEngine.PlayerPrefs.HasKey(lan_key) == true) then
        self.CurrentLan = CS.UnityEngine.PlayerPrefs.GetString(lan_key)
    else
        self.CurrentLan = self.CasinosLua:GetSystemLanguageAsString()
    end
end

---------------------------------------
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