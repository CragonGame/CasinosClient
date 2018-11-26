-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
-- 在线奖励
UiRewardOnline = {
    CasinosContext = CS.Casinos.CasinosContext.Instance;
    LuaMgr = CS.Casinos.CasinosContext.Instance.LuaMgr;
    ViewMgr = nil;
    ComUi = nil;
    GTextOnlineCountDownTm = 0;
    GBtnOnlineReward = nil
}

function UiRewardOnline:Create(view_mgr, com_ui)
    self.ViewMgr = view_mgr
    self.ComUi = com_ui
    local com_rewardonline = self.ComUi:GetChild("RewardOnline").asCom

    self.GTextOnlineCountDownTm = com_rewardonline:GetChild("TextCountdown").asTextField
    self.GBtnOnlineReward = com_rewardonline:GetChild("BtnGetOnlineReward").asButton
    self.GBtnOnlineReward.onClick:Add(
            function()
                self:_onClickBtnOnlineReward()
            end
    )
end

function UiRewardOnline:Destroy()
    self.GBtnOnlineReward.onClick:Clear()
end

function UiRewardOnline:SetOnlineRewardLeftTm(left_tm)
    self.GTextOnlineCountDownTm.text = left_tm
end

function UiRewardOnline:_onClickBtnOnlineReward()
    local ev = self.ViewMgr:GetEv("EvViewOnGetOnLineReward")
    if (ev == nil) then
        ev = EvOnGetOnLineReward:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
-- 定时奖励
UiRewardTiming = {
    CasinosContext = CS.Casinos.CasinosContext.Instance;
    LuaMgr = CS.Casinos.CasinosContext.Instance.LuaMgr;
    ViewMgr = nil;
    ComUi = nil;
    GBtnTimingReward = nil;
    CanGetTimingReward = false
}

function UiRewardTiming:Create(view_mgr, com_ui)
    self.ViewMgr = view_mgr
    self.ComUi = com_ui
    local com_rewardtiming = self.ComUi:GetChild("RewardTiming").asCom

    self.GBtnTimingReward = com_rewardtiming:GetChild("BtnGetTimingReward").asButton
    self.GBtnTimingReward.onClick:Add(
            function()
                self:_onClickBtnTimingReward()
            end
    )
end

function UiRewardTiming:Destroy()
    self.GBtnTimingReward.onClick:Clear()
end

function UiRewardTiming:SetCanGetTimingReward(can_get_reward)
    self.CanGetTimingReward = can_get_reward
end

function UiRewardTiming:_onClickBtnTimingReward()
    if self.CanGetTimingReward then
        local ev = self.ViewMgr:GetEv("EvViewRequestGetTimingReward")
        if (ev == nil) then
            ev = EvRequestGetTimingReward:new(nil)
        end
        self.ViewMgr:SendEv(ev)
    end
end

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
    o.CanGetTimingReward = false
    o.UiRewardOnline = UiRewardOnline
    o.UiRewardTiming = UiRewardTiming
    return o
end

---------------------------------------
function ViewReward:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("Reward"))

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
    self.UiRewardTiming:Create(self.ViewMgr, self.ComUi)
end

---------------------------------------
function ViewReward:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end

    self.UiRewardOnline:Destroy()
    self.UiRewardTiming:Destroy()
end

---------------------------------------
function ViewReward:SetOnlineRewardLeftTm(left_tm)
    self.UiRewardOnline:SetOnlineRewardLeftTm(left_tm)
end

---------------------------------------
function ViewReward:SetCanGetTimingReward(can_get_reward)
    self.UiRewardTiming:SetCanGetTimingReward(can_get_reward)
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