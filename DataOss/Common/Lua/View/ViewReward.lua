-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewReward = ViewBase:new()

---------------------------------------
function ViewReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    o.Tween = nil
    return o
end

---------------------------------------
function ViewReward:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, '福利')--self.ViewMgr.LanMgr:getLanValue("Reward"))

    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
end

---------------------------------------
function ViewReward:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewReward:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewRewardFactory = ViewFactory:new()

---------------------------------------
function ViewRewardFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewRewardFactory:CreateView()
    local view = ViewReward:new(nil)
    return view
end