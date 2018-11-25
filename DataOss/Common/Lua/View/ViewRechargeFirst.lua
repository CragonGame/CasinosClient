-- Copyright(c) Cragon. All rights reserved.
-- 首充对话框

---------------------------------------
ViewRechargeFirst = ViewBase:new()

---------------------------------------
function ViewRechargeFirst:new(o)
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
    self.Tween = nil
    return o
end

---------------------------------------
function ViewRechargeFirst:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi)
    local btn_comfirm = self.ComUi:GetChild("Lan_Btn_BuyNow").asButton
    btn_comfirm.onClick:Add(
            function()
                self:onClickCharge()
            end
    )
    local common_bgandreturn = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_return = common_bgandreturn:GetChild("BtnClose").asButton
    btn_return.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )
    local com_shade = common_bgandreturn:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )
end

---------------------------------------
function ViewRechargeFirst:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewRechargeFirst:onClickBtnReturn()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewRechargeFirst:onClickCharge()
    local ev = self.ViewMgr:GetEv("EvUiRequestFirstRecharge")
    if (ev == nil) then
        ev = EvUiRequestFirstRecharge:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self:onClickBtnReturn()
end

---------------------------------------
ViewRechargeFirstFactory = ViewFactory:new()

---------------------------------------
function ViewRechargeFirstFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewRechargeFirstFactory:CreateView()
    local view = ViewRechargeFirst:new(nil)
    return view
end