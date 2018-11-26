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
    o.CanGetTimingReward = false
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

    local com_rewardonline = self.ComUi:GetChild("RewardOnline").asCom

    self.GTextOnlineCountDownTm = com_rewardonline:GetChild("TextCountdown").asTextField
    self.GBtnOnlineReward = com_rewardonline:GetChild("BtnGetOnlineReward").asButton
    self.GBtnOnlineReward.onClick:Add(
            function()
                self:_onClickOnlineReward()
            end
    )

    local com_rewardtiming = self.ComUi:GetChild("RewardTiming").asCom

    self.GBtnTimingReward = com_rewardtiming:GetChild("BtnGetTimingReward").asButton
    self.GBtnTimingReward.onClick:Add(
            function()
                self:_onClickTimingReward()
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
function ViewReward:SetOnlineRewardLeftTm(left_tm)
    self.GTextOnlineCountDownTm.text = left_tm
end

---------------------------------------
function ViewReward:SetCanGetTimingReward(can_get_reward)
    self.CanGetTimingReward = can_get_reward
end

---------------------------------------
function ViewReward:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewReward:_onClickOnlineReward()
    local ev = self.ViewMgr:GetEv("EvViewOnGetOnLineReward")
    if (ev == nil) then
        ev = EvOnGetOnLineReward:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewReward:_onClickTimingReward()
    if self.CanGetTimingReward then
        local ev = self.ViewMgr:GetEv("EvViewRequestGetTimingReward")
        if (ev == nil) then
            ev = EvRequestGetTimingReward:new(nil)
        end
        self.ViewMgr:SendEv(ev)
    end
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