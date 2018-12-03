-- Copyright(c) Cragon. All rights reserved.
-- 首充对话框

---------------------------------------
ViewRechargeFirst = class(ViewBase)

---------------------------------------
function ViewRechargeFirst:ctor()
    self.Tween = nil
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
    local ev = self:GetEv("EvUiRequestFirstRecharge")
    if (ev == nil) then
        ev = EvUiRequestFirstRecharge:new(nil)
    end
    self:SendEv(ev)
    self:onClickBtnReturn()
end

---------------------------------------
ViewRechargeFirstFactory = class(ViewFactory)

---------------------------------------
function ViewRechargeFirstFactory:CreateView()
    local view = ViewRechargeFirst:new()
    return view
end