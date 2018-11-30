-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewDesktopMenuTexas = class(ViewBase)

---------------------------------------
function ViewDesktopMenuTexas:ctor()
    self.GCoCardType = nil
end

---------------------------------------
function ViewDesktopMenuTexas:OnCreate()
    self.GBtnStandup = self.ComUi:GetChild("Lan_Btn_StandUp").asButton
    self.GBtnStandup.onClick:Add(
            function()
                self:_onClickStandUp()
            end)
    self.GBtnLeaveInMiddle = self.ComUi:GetChild("Lan_Btn_LeaveHalfWay").asButton
    self.GBtnLeaveInMiddle.onClick:Add(
            function()
                self:_onClickLeaveInMiddle()
            end)
    self.TransitiOnCreate = self.ComUi:GetTransition("TransitionCreate")
    self.TransitiOnCreate:Play()

    local com_shade = self.ComUi:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickContinue()
            end
    )

    local btn_exitgame = self.ComUi:GetChild("Lan_Btn_ExitGame").asButton
    btn_exitgame.onClick:Add(
            function()
                self:_onClickExit()
            end
    )

    local btn_inviteFriend = self.ComUi:GetChild("Lan_Btn_InviteFriend").asButton
    btn_inviteFriend.onClick:Add(
            function()
                self:_onClickInviteFriend()
            end
    )

    local btn_hint = self.ComUi:GetChild("Lan_Btn_CardType").asButton
    btn_hint.onClick:Add(
            function()
                self:_onClickHelp()
            end
    )

    local btn_reward = self.ComUi:GetChild("BtnReward").asButton
    btn_reward.onClick:Add(
            function()
                self:close()
                self.ViewMgr:CreateView('Reward')
            end)

    self.GBtnRedPoint = self.ComUi:GetChild("BtnRedPoint").asCom
    self.mIsOb = false
    self.mIsWaitwhile = false
end

---------------------------------------
function ViewDesktopMenuTexas:OnDestroy()
end

---------------------------------------
function ViewDesktopMenuTexas:SetPlayerState(is_ob, is_waitwhile, have_reward)
    self.mIsOb = is_ob
    self.mIsWaitwhile = is_waitwhile
    if (is_ob) then
        self.GBtnStandup.enabled = false
        self.GBtnLeaveInMiddle.enabled = false
    elseif (is_waitwhile) then
        self.GBtnLeaveInMiddle.enabled = false
    else
        self.GBtnStandup.enabled = true
        self.GBtnLeaveInMiddle.enabled = true
    end
    self.GBtnRedPoint.visible = have_reward
end

---------------------------------------
function ViewDesktopMenuTexas:_onClickContinue()
    self:close()
end

---------------------------------------
function ViewDesktopMenuTexas:_onClickExit()
    self.TransitiOnCreate:PlayReverse(
            function()
                self.ViewMgr:DestroyView(self)
                local ev = self.ViewMgr:GetEv("EvUiClickExitDesk")
                if (ev == nil) then
                    ev = EvUiClickExitDesk:new(nil)
                end
                self.ViewMgr:SendEv(ev)
            end
    )
end

---------------------------------------
function ViewDesktopMenuTexas:_onClickInviteFriend()
    local ev = self.ViewMgr:GetEv("EvUiClickInviteFriendPlay")
    if (ev == nil) then
        ev = EvUiClickInviteFriendPlay:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self:close()
end

---------------------------------------
function ViewDesktopMenuTexas:_onClickLeaveInMiddle()
    if (self.mIsOb == false and self.mIsWaitwhile == false) then
        local ev = self.ViewMgr:GetEv("EvUiClickWaitWhile")
        if (ev == nil) then
            ev = EvUiClickWaitWhile:new(nil)
        end
        self.ViewMgr:SendEv(ev)
    end
    self:close()
end

---------------------------------------
function ViewDesktopMenuTexas:_onClickStandUp()
    if (self.mIsOb == false) then
        local ev = self.ViewMgr:GetEv("EvUiClickOB")
        if (ev == nil) then
            ev = EvUiClickOB:new(nil)
        end
        self.ViewMgr:SendEv(ev)
    end
    self:close()
end

---------------------------------------
function ViewDesktopMenuTexas:_onClickHelp()
    self.ViewMgr:CreateView("DesktopHintsTexas")
    self:close()
end

---------------------------------------
function ViewDesktopMenuTexas:close()
    --close()
    self.TransitiOnCreate:PlayReverse(
            function()
                self.ViewMgr:DestroyView(self)
            end
    )
end

---------------------------------------
ViewDesktopMenuTexasFactory = class(ViewFactory)

---------------------------------------
function ViewDesktopMenuTexasFactory:CreateView()
    local view = ViewDesktopMenuTexas:new()
    return view
end