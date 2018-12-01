-- Copyright(c) Cragon. All rights reserved.
-- 定时奖励，由ControllerReward管理

---------------------------------------
RewardTiming = {}

---------------------------------------
function RewardTiming:new(controller_mgr, view_mgr)
    local o = {}
    setmetatable(o, self)
    self.__index = self
    self.ViewMgr = view_mgr
    self.ControllerReward = ControllerReward
    self.ControllerMgr = controller_mgr
    self.MC = CommonMethodType
    self.Type = TimingRewardType.None
    self.Get = false
    self.RewardGold = 0
    self.CanGetReward = false
    return o
end

---------------------------------------
function RewardTiming:SetTimingRewardData(reward_data)
    self.Type = reward_data.Type
    self.Get = reward_data.Get
    self.RewardGold = reward_data.RewardGold
    if (self.Type ~= TimingRewardType.None and self.Get == false) then
        self.CanGetReward = true
    else
        self.CanGetReward = false
    end

    local ev = self.ViewMgr:GetEv("EvCtrlRewardRefreshGetTimingRewardState")
    if (ev == nil) then
        ev = EvCtrlRewardRefreshGetTimingRewardState:new(nil)
    end
    ev.can_getreward = self.CanGetReward
    self.ViewMgr:SendEv(ev)

    self.ControllerReward:RefreshRedPoint()-- 刷新小红点状态
end

---------------------------------------
function RewardTiming:OnClickBtnTimingReward()
    if (self.CanGetReward == true) then
        self.ControllerMgr.RPC:RPC0(self.MC.PlayerGetTimingRewardRequest)
    else
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("CanNotGetYet"))
    end
end