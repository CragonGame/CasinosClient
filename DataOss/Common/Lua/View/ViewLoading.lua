-- Copyright(c) Cragon. All rights reserved.
-- 加载界面

---------------------------------------
ViewLoading = ViewBase:new()

---------------------------------------
function ViewLoading:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Context = Context
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    return o
end

---------------------------------------
function ViewLoading:OnCreate()
    local pro = self.ComUi:GetChild("Progress")
    if (pro ~= nil) then
        self.GProgressBar = pro.asProgress
        self.GProgressBar.max = 100
        self.GProgressBar.value = 0
        self.GProgressBar.visible = false
    end

    local text = self.ComUi:GetChild("Tips")
    if (text ~= nil) then
        self.GTextFieldTips = text.asTextField
    end

    local com_bg = self.ComUi:GetChild("ComBg").asCom
    local image_bg = com_bg:GetChild("ImageMote").asImage
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    if (self.Context.Cfg.NeedHideClientUi == false) then
        image_bg.visible = false
    else
        image_bg.visible = true
    end
end

---------------------------------------
function ViewLoading:OnDestroy()
end

---------------------------------------
function ViewLoading:OnHandleEv(ev)
end

---------------------------------------
function ViewLoading.setLoadingProgress(progress)
    local loading = ViewLoading:new(nil)
    loading.GProgressBar.visible = true
    if (loading.GProgressBar ~= nil) then
        loading.GProgressBar.value = progress
        if (loading.GProgressBar.value < loading.GProgressBar.max) then
        else
            if (loading.OnFinished ~= nil) then
                --CS.FairyGUI.Timers.inst:Remove(loading._updateTips)
                loading.OnFinished()
            end
        end
    end
end

---------------------------------------
function ViewLoading:setTip(tip)
    self.ViewMgr.MianC.LuaHelper:DeleteAllTableEle(self.ListRandomTips)
    self.ListRandomTips[tip] = tip

    local data_version = CS.Casinos.CasinosContext.Instance.Config.InitDataVersion
    local data_version_key = self.ViewMgr.MianC.GetLocalVersionInfoKey()
    if (CS.UnityEngine.PlayerPrefs.HasKey(data_version_key)) then
        data_version = CS.UnityEngine.PlayerPrefs.GetString(data_version_key)
    end

    local gtext_version = self.ComUi:GetChild("Version")
    if (gtext_version ~= nil) then
        local version_text = gtext_version.asTextField
        version_text.text = string.format("Ӧ�ð汾��%s ���ݰ汾��%s", CS.UnityEngine.Application.version, data_version)
    end
end

---------------------------------------
function ViewLoading:setTips(list_tips)
    self.ViewMgr.MianC.LuaHelper:DeleteAllTableEle(loading.ListRandomTips)
    for k, v in pairs(list_tips) do
        self.ListRandomTips[k] = v
    end
end

---------------------------------------
function ViewLoading:_updateTips(param)
    local tips = nil
    local count = self.ViewMgr.MianC.LuaHelper:GetTableCount(self.ListRandomTips)
    if (count > 0) then
        tips = self.ListRandomTips[CS.UnityEngine.Random.Range(0, count)]
    end

    if ((tips ~= nil and string.len(tips) <= 0)) then
        if (self.GTextFieldTips ~= nil) then
            self.GTextFieldTips.text = tips
        end
    end
end

---------------------------------------
ViewLoadingFactory = ViewFactory:new()

---------------------------------------
function ViewLoadingFactory:new(o, ui_package_name, ui_component_name,
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
function ViewLoadingFactory:CreateView()
    local view = ViewLoading:new(nil)
    return view
end