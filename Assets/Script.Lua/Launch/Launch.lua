-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
-- 配置，Launch
LaunchConfig = {}

function LaunchConfig:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.CasinosLua = CS.Casinos.CasinosContext.Instance.CasinosLua
    self.Env = 'Pro'
    self.CurrentLan = 'ChineseSimplified'
    return o
end

---------------------------------------
Launch = {}

---------------------------------------
function Launch:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil) then
        self.LaunchCfg = LaunchConfig:new(nil)
        self.LaunchStep = {}
        self.Env = 'Pro'
        self.CurrentLan = 'ChineseSimplified'
        self.PreViewMgr = nil
        self.PreLoading = nil
        self.PreMsgBox = nil
        self.CasinosContext = CS.Casinos.CasinosContext.Instance
        self.CasinosLua = CS.Casinos.CasinosContext.Instance.CasinosLua
        self.Context = nil
        self.TimerUpdate = nil
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

    -- 获取Env值
    local env_key = "Env"
    if (CS.UnityEngine.PlayerPrefs.HasKey(env_key) == true) then
        self.Env = CS.UnityEngine.PlayerPrefs.GetString(env_key)
    end

    print('Launch:Setup() Env=' .. self.Env)

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

    CS.FairyGUI.Stage.inst.onKeyDown:Add(
            function(context)
                local key_code = context.inputEvent.keyCode
                if (key_code == CS.UnityEngine.KeyCode.Escape) then
                    local view_premsgbox = PreViewMgr:getView("PreMsgBox")
                    if (view_premsgbox == nil) then
                        view_premsgbox = PreViewMgr:createView("PreMsgBox")
                    end
                    view_premsgbox:showMsgBox('确认退出吗',
                            function()
                                PreViewMgr:destroyView(view_premsgbox)
                                CS.UnityEngine.Application.Quit()
                            end,
                            function()
                                PreViewMgr:destroyView(view_premsgbox)
                            end
                    )
                end
            end)

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
    print(http_url_bundlexxx)
    async_asset_loadgroup:LoadWWWAsync(http_url_bundlexxx,
            function(url, www)
                self.CasinosLua:LoadLuaFromBytes(lua_pkg_name, www.text)
                self.CasinosLua:DoString(lua_pkg_name)

                -- 下载并加载Context.lua
                local data_select = DataSelectPro
                if (self.Env ~= nil and self.Env == 'Dev') then
                    data_select = DataSelectDev
                end
                local http_url_context = string.format('https://cragon-king-oss.cragon.cn/%s/Data_%s/Context.lua',
                        self.CasinosContext.Config.Platform, data_select)
                print(http_url_context)
                async_asset_loadgroup:LoadWWWAsync(http_url_context,
                        function(url, www)
                            self.CasinosLua:LoadLuaFromBytes('Context', www.text)
                            self.CasinosLua:DoString('Context')
                            self.Context = Context:new(nil, self.Env, data_select)
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

    --package.preload['PreViewMsgBox'] = nil
    --package.loaded['PreViewMsgBox'] = nil
    --package.preload['PreViewLoading'] = nil
    --package.loaded['PreViewLoading'] = nil
    --package.preload['PreViewMgr'] = nil
    --package.loaded['PreViewMgr'] = nil
    --package.preload['PreViewFactory'] = nil
    --package.loaded['PreViewFactory'] = nil
    --package.preload['PreViewBase'] = nil
    --package.loaded['PreViewBase'] = nil
    --package.preload['ParticleHelper'] = nil
    --package.loaded['ParticleHelper'] = nil

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
-- App暂停，恢复运行
function Launch:OnApplicationPause(pause)
    --print('OnApplicationPause, Pause=' .. tostring(pause))
    if (self.Context ~= nil) then
    else
    end
end

---------------------------------------
-- App获取，失去焦点
function Launch:OnApplicationFocus(focus_state)
    --print('OnApplicationFocus, FocusState=' .. tostring(focus_state))
    if (self.Context ~= nil) then
    else
    end
end

---------------------------------------
-- Android的退出确认
function Launch:OnAndroidQuitConfirm()
    print('OnAndroidQuitConfirm')
    if (self.Context ~= nil) then
    else
    end
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