-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewDesktopHMenu = ViewBase:new()

---------------------------------------
function ViewDesktopHMenu:new(o)
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
    o.CoMenuEx = nil
    o.ViewDesktopH = nil
    o.Tween = nil
    return o
end

---------------------------------------
function ViewDesktopHMenu:OnCreate()
    self.ComUi.onClick:Add(
            function()
                self:_onClickMenuCo()
            end)
    self.CoMenuEx = self.ComUi:GetChild("CoMenuEx").asCom
    local btn_return = self.CoMenuEx:GetChild("BtnReturn").asButton
    btn_return.onClick:Add(
            function()
                self:_onClickBtnReturn()
            end)
    local btn_cardtype = self.CoMenuEx:GetChild("BtnCardType").asButton
    btn_cardtype.onClick:Add(
            function()
                self:_onClickBtnCardType()
            end)
    local btn_help = self.CoMenuEx:GetChild("BtnHelp").asButton
    btn_help.onClick:Add(
            function()
                self:_onClickBtnHelp()
            end)
    local btn_charge = self.CoMenuEx:GetChild("BtnRecharge").asButton
    btn_charge.onClick:Add(
            function()
                self:_onClickBtnCharge()
            end)
    local btn_reward = self.CoMenuEx:GetChild("BtnReward").asButton
    btn_reward.onClick:Add(
            function()
                --local ev = self.ViewMgr:GetEv("EvViewClickShowReward")
                self.ViewMgr:CreateView('Reward')
            end)
    self.ViewDesktopH = self.ViewMgr:GetView("DesktopH")
end

---------------------------------------
function ViewDesktopHMenu:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

------------------------------------------
function ViewDesktopHMenu:showMenu(have_reward)
    self.CoMenuEx:SetXY(0, -self.CoMenuEx.height)
    self.Tween = self.CoMenuEx:TweenMoveY(0, 0.25)
end

---------------------------------------
function ViewDesktopHMenu:_onClickMenuCo()
    self.CoMenuEx:TweenMoveY(-self.CoMenuEx.height, 0.25):OnComplete(
            function()
                self.ViewMgr:DestroyView(self)
            end
    )
end

---------------------------------------
function ViewDesktopHMenu:_onClickBtnReturn()
    local ev = self.ViewMgr:GetEv("EvViewClickLeaveDesktopH")
    if (ev == nil) then
        ev = EvUiClickLeaveDesktopHundred:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewDesktopHMenu:_onClickBtnCardType()
    local card_type = self.ViewMgr:CreateView("DesktopHCardType")
    local p = self.ViewDesktopH:getDesktopBasePackageName()
    local co_cardtype = CS.FairyGUI.UIPackage.CreateObject(p, self.ViewDesktopH.UiDesktopHComDesktopHCardTypeTitle .. self.ViewDesktopH.FactoryName).asCom
    self.ViewMgr.LanMgr:parseComponent(co_cardtype)
    card_type:showCardType(co_cardtype)
end

---------------------------------------
function ViewDesktopHMenu:_onClickBtnHelp()
    local help = self.ViewMgr:CreateView("DesktopHHelp")
    local p = self.ViewDesktopH:getDesktopBasePackageName()
    local co_betpot = CS.FairyGUI.UIPackage.CreateObject(p, self.ViewDesktopH.UiDesktopHComDesktopHHelpTitle .. self.ViewDesktopH.FactoryName).asCom
    self.ViewMgr.LanMgr:parseComponent(co_betpot)
    help:setComHelp(co_betpot)
end

---------------------------------------
function ViewDesktopHMenu:_onClickBtnCharge()
    self.ViewMgr:CreateView("Shop")
end

---------------------------------------
ViewDesktopHMenuFactory = ViewFactory:new()

---------------------------------------
function ViewDesktopHMenuFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewDesktopHMenuFactory:CreateView()
    local view = ViewDesktopHMenu:new(nil)
    return view
end