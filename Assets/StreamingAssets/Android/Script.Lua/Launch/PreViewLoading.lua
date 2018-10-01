-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ParticleHelper = {}

---------------------------------------
function ParticleHelper:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    if (self.Instance == nil) then
        self.Instance = o
        self.TableParticle = {}
        self.TableSpine = {}
    end
    return self.Instance
end

---------------------------------------
function ParticleHelper:GetParticel(path)
    local particle = self.TableParticle[path]
    if (particle == nil) then
        local particle_path = self.CasinosContext.PathMgr:combinePersistentDataPath(
                ViewHelper:getABParticleResourceTitlePath() .. path)
        particle = CS.UnityEngine.AssetBundle.LoadFromFile(particle_path)
        self.TableParticle[path] = particle
    end
    return particle
end

---------------------------------------
function ParticleHelper:GetSpine(path)
    local spine = self.TableSpine[path]
    if (spine == nil) then
        local spine_path = self.CasinosContext.PathMgr:combinePersistentDataPath(
                self.CasinosContext.ABResourcePathTitle .. path)
        spine = CS.UnityEngine.AssetBundle.LoadFromFile(spine_path)
        self.TableSpine[path] = spine
    end
    return spine
end

---------------------------------------
function ParticleHelper:GetPreSpine(path)
    local spine = self.TableSpine[path]
    if (spine == nil) then
        local casinos_context = CS.Casinos.CasinosContext.Instance
        local launch_persistentdata_path = casinos_context.PathMgr.PathLaunchRootPersistent
        local spine_path = launch_persistentdata_path .. string.lower(path) .. ".ab"
        spine = CS.UnityEngine.AssetBundle.LoadFromFile(spine_path)
        self.TableSpine[path] = spine
    end
    return spine
end

---------------------------------------
PreViewLoading = PreViewBase:new()

---------------------------------------
function PreViewLoading:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ViewMgr = nil
    self.GoUi = nil
    self.ComUi = nil
    self.Panel = nil
    self.UILayer = nil
    self.InitDepth = nil
    self.ViewKey = nil
    self.ShowSPine = true
    self.PlayerAnim = nil
    self.MoteRender = nil
    self.HolderMote = nil
    self.DengLongAnim = nil
    self.DengLongRender = nil
    self.AbLoadingMarry = nil
    self.AbDenglong = nil
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.CasinosLua = CS.Casinos.CasinosContext.Instance.CasinosLua
    if (self.Instance == nil) then
        self.Instance = o
    end
    return self.Instance
end

---------------------------------------
function PreViewLoading:onCreate()
    local pro = self.ComUi:GetChild("Progress")
    if (pro ~= nil) then
        self.GProgressBar = pro.asProgress
        self.GProgressBar.max = 100
        self.GProgressBar.value = 0
    end

    local text = self.ComUi:GetChild("Tips")
    if (text ~= nil) then
        self.GTextFieldTips = text.asTextField
    end

    self.OnFinished = nil
    self.IsAuto = false
    self.ListRandomTips = {}
    --CS.FairyGUI.Timers.inst:Add(0, 0, self._updateTips)

    local com_bg = self.ComUi:GetChild("ComBg")
    local bg = com_bg:GetChild("Bg")
    self:makeUiBgFiteScreen(1066, 640, self.ComUi.width, self.ComUi.height, bg.width, bg.height, bg, 2, { self.HolderMote })

    local image_mote = com_bg:GetChild("ImageMote").asImage
    local p_helper = ParticleHelper:new(nil)

    if (self.ShowSPine) then
        image_mote.visible = false
        self.AbLoadingMarry = p_helper:GetPreSpine("LoadingMarry")
        local atlas = self.AbLoadingMarry:LoadAsset("Mary_Loading.atlas")
        local texture = self.AbLoadingMarry:LoadAsset("Mary_Loading")
        local json = self.AbLoadingMarry:LoadAsset("Mary_LoadingJson")
        self.PlayerAnim = CS.Casinos.SpineHelper.LoadResourcesPrefab(atlas, texture, json, "Spine/Skeleton")
        self.PlayerAnim.transform.localScale = CS.Casinos.LuaHelper.GetVector3(70, 70, 1000)
        self.PlayerAnim:Initialize(false)
        self.PlayerAnim.loop = true
        self.PlayerAnim.AnimationName = "animation"
        self.PlayerAnim.transform.gameObject.name = "LoadingMote"
        self.MoteRender = self.PlayerAnim.transform.gameObject:GetComponent("MeshRenderer")
        self.MoteRender.sortingOrder = 4
        self.HolderMote = self.ComUi:GetChild("HolderMote").asGraph
        self.HolderMote:SetNativeObject(CS.FairyGUI.GoWrapper(self.PlayerAnim.transform.gameObject))
    else
        image_mote.visible = true
    end

    self.AbDenglong = p_helper:GetPreSpine("DengLong")
    local atlas1 = self.AbDenglong:LoadAsset("denglong.atlas")
    local texture1 = self.AbDenglong:LoadAsset("denglong")
    local json1 = self.AbDenglong:LoadAsset("denglongJson")

    local denglong_parent = self.ComUi:GetChild("DengLongParent").asCom
    self.DengLongAnim = CS.Casinos.SpineHelper.LoadResourcesPrefab(atlas1, texture1, json1, "Spine/Skeleton")
    self.DengLongAnim.transform.parent = denglong_parent.displayObject.gameObject.transform
    self.DengLongAnim.transform.localPosition =  CS.Casinos.LuaHelper.GetVector3(-10, -90, -318)
    self.DengLongAnim.transform.localScale = CS.Casinos.LuaHelper.GetVector3(90, 90, 90)
    self.DengLongAnim.transform.gameObject.layer = denglong_parent.displayObject.gameObject.layer
    self.DengLongAnim:Initialize(false)
    self.DengLongAnim.loop = true
    self.DengLongAnim.transform.gameObject.name = "DengLong"
    self.DengLongAnim.AnimationName = "animation"
    self.DengLongRender = self.DengLongAnim.transform.gameObject:GetComponent("MeshRenderer")
    self.DengLongRender.sortingOrder = 4
end

---------------------------------------
function PreViewLoading:onDestroy()
    if (self.PlayerAnim ~= nil) then
        --self.CasinosLua:DestroyGameObject(self.PlayerAnim.transform.gameObject)
        self.PlayerAnim = nil
        self.MoteRender = nil
        self.HolderMote = nil
    end
    if (self.DengLongAnim ~= nil) then
        --self.CasinosLua:DestroyGameObject(self.DengLongAnim.transform.gameObject)
        self.DengLongAnim = nil
        self.DengLongRender = nil
    end

    if (self.AbLoadingMarry ~= nil) then
        self.AbLoadingMarry:Unload(true)
        self.AbLoadingMarry = nil
    end
    if (self.AbDenglong ~= nil) then
        self.AbDenglong:Unload(true)
        self.AbDenglong = nil
    end

    self.CasinosContext = nil
    self.Instance = nil
end

---------------------------------------
function PreViewLoading:onHandleEv(ev)
end

---------------------------------------
function PreViewLoading:fireAutoLoadingProgress()
    self.GProgressBar.visible = true
    self.IsAuto = true
end

---------------------------------------
function PreViewLoading:fireManualLoadingProgress(progress, loading_info)
    self.IsAuto = false
    if (progress ~= 0) then
        self.GProgressBar.visible = true
    end

    self:setTip(loading_info)
    local cur = self.GProgressBar.value
    cur = cur + progress
    if (self.GProgressBar ~= nil) then
        self.GProgressBar.value = cur
        if (self.GProgressBar.value < loading.GProgressBar.max) then
        else
            if (self.OnFinished ~= nil) then
                -- CS.FairyGUI.Timers.inst:Remove(loading._updateTips)
                self:OnFinished()
            end
        end
    end
end

---------------------------------------
-- 更新进度条上方提示文字
function PreViewLoading:UpdateDesc(desc)
    self.GTextFieldTips.text = desc
end

---------------------------------------
-- 更新进度条
function PreViewLoading:UpdateLoadingProgress(value, max)
    self.GProgressBar.visible = true
    self.GProgressBar.value = value
    self.GProgressBar.max = max
end

---------------------------------------
function PreViewLoading:setLoadingProgress(progress)
    self.GProgressBar.visible = true
    if (self.GProgressBar ~= nil) then
        self.GProgressBar.value = progress
        if (self.GProgressBar.value < self.GProgressBar.max) then
        else
            if (self.OnFinished ~= nil) then
                --CS.FairyGUI.Timers.inst:Remove(loading._updateTips)
                self:OnFinished()
            end
        end
    end
end

---------------------------------------
function PreViewLoading:setTip(tip)
    self.ListRandomTips = {}
    self.ListRandomTips[tip] = tip
    local c = CS.Casinos.CasinosContext.Instance

    local gtext_version = self.ComUi:GetChild("Version")
    if (gtext_version ~= nil) then
        local version_text = gtext_version.asTextField
        local app_version = "应用版本"
        local data_versionex = "数据版本"
        local lan = c.CurrentLan
        if (lan == "English") then
            app_version = "AppVersion"
            data_versionex = "DataVersion"
        else
            if (lan == "Chinese" or lan == "ChineseSimplified") then
                app_version = "应用版本"
                data_versionex = "数据版本"
            end
        end
        local en = "Pro"
        if GatewayIp ~= nil and string.find(GatewayIp, "dev") then
            en = "Dev"
        end
        local version_bundle = c.Config.VersionBundle
        local version_data = c.Config.VersionDataPersistent
        version_text.text = string.format("%s: %s,  %s: %s %s", app_version, version_bundle, data_versionex, version_data, en)
    end
end

---------------------------------------
function PreViewLoading:setTips(list_tips)
    self.ListRandomTips = {}
    for k, v in pairs(list_tips) do
        self.ListRandomTips[k] = v
    end
end

---------------------------------------
function PreViewLoading:_updateTips(param)
    local tips_key, tips_value = nil
    local count = self.PreViewMgr:GetTableCount(self.ListRandomTips)
    if (count > 0) then
        tips_key, tips_value = self.PreViewMgr:GetAndRemoveTableFirstEle(self.ListRandomTips)
    end
    if ((tips_key ~= nil and string.len(tips_key) > 0)) then
        if (self.GTextFieldTips ~= nil) then
            self.GTextFieldTips.text = tips_key
        end
    end
end

---------------------------------------
function PreViewLoading:_playProgress(param)
    if (self.GProgressBar ~= nil) then
        self.GProgressBar.visible = true
        if (self.GProgressBar.value <= self.GProgressBar.max) then
            local value = self.GProgressBar.value
            value = value + 2
            self.GProgressBar.value = value
        else
            if (self.OnFinished ~= nil) then
                self:OnFinished()
            end
        end
    end
end

---------------------------------------
function PreViewLoading:makeUiBgFiteScreen(design_width, design_height, logic_width, logic_height, image_width, image_height, obj, anchor_mode, t_anchor_point)
    local w = logic_width / design_width
    local h = logic_height / design_height
    if (w >= h) then
        obj.width = logic_width
        obj.height = logic_width * image_height / image_width
        obj.x = 0
        local y = 0
        if anchor_mode == 1 then
            y = 0
            if t_anchor_point ~= nil then
                for i, v in pairs(t_anchor_point) do
                    local p = v.xy
                    local p_y = p.y
                    local p_p = CS.Casinos.LuaHelper.GetVector2(p.x, p_y - p_y / 2 * w)
                    v.xy = p_p
                end
            end
        elseif anchor_mode == 2 then
            y = (logic_height - obj.height) / 2
            if t_anchor_point ~= nil then
                for i, v in pairs(t_anchor_point) do
                    local p = v.xy
                    local p_y = p.y
                    local p_p = CS.Casinos.LuaHelper.GetVector2(p.x, p_y + p_y / 2 * (w - h) / 2)
                    v.xy = p_p
                end
            end
        elseif anchor_mode == 3 then
            y = logic_height - obj.height
            if t_anchor_point ~= nil then
                for i, v in pairs(t_anchor_point) do
                    local p = v.xy
                    local p_y = p.y
                    local p_p = CS.Casinos.LuaHelper.GetVector2(p.x, p_y + p_y / 2 * w)
                    v.xy = p_p
                end
            end
        end
        obj.y = y
    else
        obj.height = logic_height
        obj.width = logic_height * image_width / image_height
        obj.x = (logic_width - obj.width) / 2
        obj.y = 0
        if t_anchor_point ~= nil then
            for i, v in pairs(t_anchor_point) do
                local p = v.xy
                local p_y = p.y
                local p_p = CS.Casinos.LuaHelper.GetVector2(p.x, p_y + p_y / 2 * (h - w) / 2)
                v.xy = p_p
            end
        end
    end
end

---------------------------------------
PreViewLoadingFactory = PreViewFactory:new()

---------------------------------------
function PreViewLoadingFactory:new(o, ui_package_name, ui_component_name,
                                   ui_layer, is_single, fit_screen)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
    return o
end

---------------------------------------
function PreViewLoadingFactory:createView()
    local view = PreViewLoading:new(nil)
    return view
end

--CS.FairyGUI.Timers.inst.Remove(loading._playProgress)
--CS.FairyGUI.Timers.inst:Add(0.01, 0, loading._playProgress)
--if (self.ShowSPine) then
--    CS.UnityEngine.GameObject.Destroy(self.PlayerAnim.transform.gameObject)
--end
--CS.UnityEngine.GameObject.Destroy(self.DengLongAnim.transform.gameObject)
--if (self.IsAuto == true) then
--    --CS.FairyGUI.Timers.inst:Remove(self._playProgress)
--end
--CS.FairyGUI.Timers.inst:Remove(self._updateTips)