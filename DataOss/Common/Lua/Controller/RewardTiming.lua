-- Copyright(c) Cragon. All rights reserved.
-- 由ControllerReward管理

---------------------------------------
RewardTiming = {}

---------------------------------------
function RewardTiming:new(o, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ViewMgr = view_mgr
    o.Type = TimingRewardType.None
    o.Get = false
    o.RewardGold = 0
    o.CanGetReward = false
    return o
end

---------------------------------------
function RewardTiming:SetTimingRewardData(reward)
    self.Type = reward.Type
    self.Get = reward.Get
    self.RewardGold = reward.RewardGold
    if (self.Type ~= TimingRewardType.None and self.Get == false) then
        self.CanGetReward = true
    else
        self.CanGetReward = false
    end
    self:_sendCanGetReward()
end

---------------------------------------
function RewardTiming:OnGetReward()
    if (self.CanGetReward == false) then
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("CanNotGetYet"))
    end
    return self.CanGetReward
end

---------------------------------------
function RewardTiming:IfCanGetReward()
    return self.CanGetReward
end

---------------------------------------
function RewardTiming:_sendCanGetReward()
    local ev = self.ViewMgr:GetEv("EvEntityCanGetTimingReward")
    if (ev == nil) then
        ev = EvEntityCanGetTimingReward:new(nil)
    end
    ev.can_getreward = self.CanGetReward
    self.ViewMgr:SendEv(ev)
end