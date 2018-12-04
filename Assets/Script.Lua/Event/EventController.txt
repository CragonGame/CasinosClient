-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
-- 福利控制器，小红点状态刷新
EvCtrlRewardRedPointStateChange = EventBase:new(nil)

function EvCtrlRewardRedPointStateChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCtrlRewardRedPointStateChange"
    self.RedPointType = ''
    self.Show = false
    return o
end

function EvCtrlRewardRedPointStateChange:Reset()
    self.RedPointType = ''
    self.Show = false
end

---------------------------------------
-- 福利控制器，在线奖励可领取倒计时
EvCtrlRewardRefreshGetOnlineRewardLeftTm = EventBase:new(nil)

function EvCtrlRewardRefreshGetOnlineRewardLeftTm:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCtrlRewardRefreshGetOnlineRewardLeftTm"
    self.left_reward_second = 0
    return o
end

function EvCtrlRewardRefreshGetOnlineRewardLeftTm:Reset()
    self.left_reward_second = 0
end

---------------------------------------
-- 福利控制器，定时奖励可领取状态刷新
EvCtrlRewardRefreshGetTimingRewardState = EventBase:new(nil)

function EvCtrlRewardRefreshGetTimingRewardState:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCtrlRewardRefreshGetTimingRewardState"
    self.can_getreward = false
    return o
end

function EvCtrlRewardRefreshGetTimingRewardState:Reset()
    self.can_getreward = false
end