-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewDesktopHMenu = class(ViewBase)

function ViewDesktopHMenu:ctor()
    self.CoMenuEx = nil
    self.ViewDesktopH = nil
    self.TweenShow = nil
    self.TweenHide = nil
end

---------------------------------------
function ViewDesktopHMenu:OnCreate()
    self.ViewDesktopH = self.ViewMgr:GetView("DesktopH")
    self.CoMenuEx = self.ComUi:GetChild("CoMenuEx").asCom
    self.GBtnRedPoint = self.CoMenuEx:GetChild("BtnRedPoint").asCom

    self.ComUi.onClick:Add(
            function()
                self:_onClickMenuHide()
            end)

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
    local btn_shop = self.CoMenuEx:GetChild("BtnRecharge").asButton
    btn_shop.onClick:Add(
            function()
                self:_onClickBtnShop()
            end)
    local btn_reward = self.CoMenuEx:GetChild("BtnReward").asButton
    btn_reward.onClick:Add(
            function()
                self:_onClickBtnReward()
            end)
end

---------------------------------------
function ViewDesktopHMenu:OnDestroy()
    self.ComUi.onClick:Clear()

    if self.TweenShow ~= nil then
        self.TweenShow:Kill(false)
        self.TweenShow = nil
    end
    if self.TweenHide ~= nil then
        self.TweenHide:Kill(false)
        self.TweenHide = nil
    end
end

------------------------------------------
function ViewDesktopHMenu:ShowMenu(have_reward)
    self.GBtnRedPoint.visible = have_reward
    self.CoMenuEx:SetXY(0, -self.CoMenuEx.height)
    self.TweenShow = self.CoMenuEx:TweenMoveY(0, 0.25)
end

---------------------------------------
function ViewDesktopHMenu:_onClickMenuHide()
    self.TweenHide = self.CoMenuEx:TweenMoveY(-self.CoMenuEx.height, 0.25):OnComplete(
            function()
                self.ViewMgr:DestroyView(self)
            end
    )
end

---------------------------------------
-- 返回
function ViewDesktopHMenu:_onClickBtnReturn()
    local ev = self.ViewMgr:GetEv("EvViewClickLeaveDesktopH")
    if (ev == nil) then
        ev = EvUiClickLeaveDesktopHundred:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
-- 牌型
function ViewDesktopHMenu:_onClickBtnCardType()
    local p = self.ViewDesktopH:getDesktopBasePackageName()
    local co_cardtype = CS.FairyGUI.UIPackage.CreateObject(p, self.ViewDesktopH.UiDesktopHComDesktopHCardTypeTitle .. self.ViewDesktopH.FactoryName).asCom
    self.ViewMgr.LanMgr:parseComponent(co_cardtype)

    local view_desktophcardtype = self.ViewMgr:CreateView("DesktopHCardType")
    view_desktophcardtype:showCardType(co_cardtype)
end

---------------------------------------
-- 帮助
function ViewDesktopHMenu:_onClickBtnHelp()
    local p = self.ViewDesktopH:getDesktopBasePackageName()
    local co_betpot = CS.FairyGUI.UIPackage.CreateObject(p, self.ViewDesktopH.UiDesktopHComDesktopHHelpTitle .. self.ViewDesktopH.FactoryName).asCom
    self.ViewMgr.LanMgr:parseComponent(co_betpot)

    local view_desktophhelp = self.ViewMgr:CreateView("DesktopHHelp")
    view_desktophhelp:setComHelp(co_betpot)
end

---------------------------------------
-- 商城
function ViewDesktopHMenu:_onClickBtnShop()
    self.ViewMgr:CreateView("Shop")
end

---------------------------------------
-- 福利
function ViewDesktopHMenu:_onClickBtnReward()
    self.ViewMgr:CreateView("Reward")
end

---------------------------------------
ViewDesktopHMenuFactory = class(ViewFactory)

---------------------------------------
function ViewDesktopHMenuFactory:CreateView()
    local view = ViewDesktopHMenu:new()
    return view
end