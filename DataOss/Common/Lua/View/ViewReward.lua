-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
-- 在线奖励
UiRewardOnline = {
    CasinosContext = CS.Casinos.CasinosContext.Instance;
    LuaMgr = CS.Casinos.CasinosContext.Instance.LuaMgr;
    ControllerReward = nil;
    ViewMgr = nil;
    ComUi = nil;
    GTextInfo = '';
    CanGetOnlineReward = false;
    GBtnOnlineReward = nil
}

function UiRewardOnline:Create(view_mgr, com_ui)
    self.ViewMgr = view_mgr
    self.ComUi = com_ui
    self.ControllerReward = ControllerReward
    local com_rewardonline = self.ComUi:GetChild("RewardOnline").asCom

    self.GTextInfo = com_rewardonline:GetChild("TextInfo").asTextField
    self.GBtnOnlineReward = com_rewardonline:GetChild("BtnGetOnlineReward").asButton
    self.GBtnOnlineReward.onClick:Add(
            function()
                self:_onClickBtnOnlineReward()
            end
    )

    if self.ControllerReward.RewardOnline.CanGetReward == true then
        self.CanGetOnlineReward = true
        self.GBtnOnlineReward.enabled = true
        self.GTextInfo.text = string.format('点击领取[color=#D9D919]%s[/color]筹码在线奖励', tostring(self.ControllerReward.RewardOnline.NextReward))
    else
        self.CanGetOnlineReward = false
        self.GBtnOnlineReward.enabled = false
        -- self.ViewMgr.LanMgr:getLanValue("OnlineReward")
        self.GTextInfo.text = string.format('再过[color=#CC3299]%s[/color]后，可领取[color=#D9D919]%s[/color]筹码在线奖励',
                tostring(self.ControllerReward.RewardOnline.FormatLeftTm), tostring(self.ControllerReward.RewardOnline.NextReward))
    end
end

function UiRewardOnline:Destroy()
    self.GBtnOnlineReward.onClick:Clear()
end

function UiRewardOnline:RefreshLeftTmInfo()
    -- self.ViewMgr.LanMgr:getLanValue("OnlineReward")
    self.GTextInfo.text = string.format('再过[color=#CC3299]%s[/color]后，可领取[color=#D9D919]%s[/color]筹码在线奖励',
            tostring(self.ControllerReward.RewardOnline.FormatLeftTm), tostring(self.ControllerReward.RewardOnline.NextReward))
end

function UiRewardOnline:RefreshCanGetOnlineRewardState(can_get_reward)
    self.CanGetOnlineReward = can_get_reward
    self.GBtnOnlineReward.enabled = can_get_reward
    if self.ControllerReward.RewardOnline.CanGetReward == true then
        self.GTextInfo.text = string.format('点击领取[color=#D9D919]%s[/color]筹码在线奖励', tostring(self.ControllerReward.RewardOnline.NextReward))
    else
        -- self.ViewMgr.LanMgr:getLanValue("OnlineReward")
        self.GTextInfo.text = string.format('再过[color=#CC3299]%s[/color]后，可领取[color=#D9D919]%s[/color]筹码在线奖励',
                tostring(self.ControllerReward.RewardOnline.FormatLeftTm), tostring(self.ControllerReward.RewardOnline.NextReward))
    end
end

function UiRewardOnline:_onClickBtnOnlineReward()
    local ev = self.ViewMgr:GetEv("EvViewRewardClickBtnOnlineReward")
    if (ev == nil) then
        ev = EvViewRewardClickBtnOnlineReward:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
-- 救济金
UiRewardRelief = {
    CasinosContext = CS.Casinos.CasinosContext.Instance;
    LuaMgr = CS.Casinos.CasinosContext.Instance.LuaMgr;
    ControllerReward = nil;
    ViewMgr = nil;
    ComUi = nil;
    GTextInfo = '';
    CanGetOnlineReward = false;
    GBtnOnlineReward = nil
}

function UiRewardRelief:Create(view_mgr, com_ui)
    self.ViewMgr = view_mgr
    self.ComUi = com_ui
    self.ControllerReward = ControllerReward
end

function UiRewardRelief:Destroy()
end

---------------------------------------
-- 定时奖励
UiRewardTiming = {
    CasinosContext = CS.Casinos.CasinosContext.Instance;
    LuaMgr = CS.Casinos.CasinosContext.Instance.LuaMgr;
    ControllerReward = nil;
    ViewMgr = nil;
    ComUi = nil;
    GTextInfo = '';
    GBtnTimingReward = nil;
    CanGetTimingReward = false
}

function UiRewardTiming:Create(view_mgr, com_ui)
    self.ViewMgr = view_mgr
    self.ComUi = com_ui
    self.ControllerReward = ControllerReward
    local com_rewardtiming = self.ComUi:GetChild("RewardTiming").asCom

    self.GTextInfo = com_rewardtiming:GetChild("TextInfo").asTextField
    self.GTextInfo.text = string.format('每天[color=#CC3299]12~13[/color]点，[color=#CC3299]18~19[/color]点各领取[color=#D9D919]%s[/color]筹码定时奖励',
            tostring(self.ControllerReward.RewardTiming.RewardGold))
    self.GBtnTimingReward = com_rewardtiming:GetChild("BtnGetTimingReward").asButton
    self.GBtnTimingReward.onClick:Add(
            function()
                self:_onClickBtnTimingReward()
            end)

    if self.ControllerReward.RewardTiming.CanGetReward == true then
        self.CanGetTimingReward = true
        self.GBtnTimingReward.enabled = true
    else
        self.CanGetTimingReward = false
        self.GBtnTimingReward.enabled = false
    end
end

function UiRewardTiming:Destroy()
    self.GBtnTimingReward.onClick:Clear()
end

function UiRewardTiming:SetCanGetTimingReward(can_get_reward)
    self.CanGetTimingReward = can_get_reward
    self.GBtnTimingReward.enabled = can_get_reward
end

function UiRewardTiming:_onClickBtnTimingReward()
    if self.CanGetTimingReward then
        local ev = self.ViewMgr:GetEv("EvViewRewardClickBtnTimingReward")
        if (ev == nil) then
            ev = EvViewRewardClickBtnTimingReward:new(nil)
        end
        self.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
ViewReward = class(ViewBase)

---------------------------------------
function ViewReward:ctor()
    self.Tween = nil
    self.ControllerReward = ControllerReward
    self.UiRewardOnline = UiRewardOnline
    self.UiRewardRelief = UiRewardRelief
    self.UiRewardTiming = UiRewardTiming
end

---------------------------------------
function ViewReward:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, '福利')--self.ViewMgr.LanMgr:getLanValue("Reward"))

    self.ViewMgr:BindEvListener("EvCtrlRewardRefreshGetOnlineRewardLeftTm", self)

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

    self.UiRewardOnline:Create(self.ViewMgr, self.ComUi)
    self.UiRewardRelief:Create(self.ViewMgr, self.ComUi)
    self.UiRewardTiming:Create(self.ViewMgr, self.ComUi)
end

---------------------------------------
function ViewReward:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end

    self.UiRewardOnline:Destroy()
    self.UiRewardRelief:Destroy()
    self.UiRewardTiming:Destroy()
end

---------------------------------------
function ViewReward:OnHandleEv(ev)
    if (ev.EventName == "EvCtrlRewardRefreshGetOnlineRewardLeftTm") then
        self.UiRewardOnline:RefreshLeftTmInfo()
    elseif (ev.EventName == "EvCtrlRewardRefreshGetTimingRewardState") then
        self.UiRewardTiming:SetCanGetTimingReward(ev.can_getreward)
    end
end

---------------------------------------
function ViewReward:RefreshCanGetOnlineRewardState(can_getreward)
    self.UiRewardOnline:RefreshCanGetOnlineRewardState(can_getreward)
end

---------------------------------------
function ViewReward:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewRewardFactory = class(ViewFactory)

---------------------------------------
function ViewRewardFactory:CreateView()
    local view = ViewReward:new()
    return view
end