-- Copyright(c) Cragon. All rights reserved.
-- 由ControllerPlayer管理

TimingReward = {}

function TimingReward:new(o,view_mgr)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self
	self.ViewMgr = view_mgr
    o.Type = TimingRewardType.None
    o.Get = false
    o.RewardGold = 0
    o.CanGetReward = false

    return o
end

function TimingReward:setTimingRewardData(reward)
    self.Type = reward.Type
    self.Get = reward.Get
    self.RewardGold = reward.RewardGold
    if (self.Type ~= TimingRewardType.None and self.Get == false)
    then
        self.CanGetReward = true
    else
        self.CanGetReward = false
    end
    self:_sendCanGetReward()
end

function TimingReward:onGetReward()
    if (self.CanGetReward == false)
    then
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("CanNotGetYet"))
    end
    return self.CanGetReward
end

function TimingReward:IfCanGetReward()
    return self.CanGetReward
end

function TimingReward:_sendCanGetReward()
    local ev = self.ViewMgr:getEv("EvEntityCanGetTimingReward")
    if(ev == nil)
    then
        ev = EvEntityCanGetTimingReward:new(nil)
    end
    ev.can_getreward = self.CanGetReward
    self.ViewMgr:sendEv(ev)
end