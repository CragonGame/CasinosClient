-- Copyright(c) Cragon. All rights reserved.
-- 由ControllerPlayer管理

---------------------------------------
OnLineReward = {}

---------------------------------------
function OnLineReward:new(o, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ViewMgr = view_mgr
    o.OnlineRewardState = OnlineRewardState.CountDown
    o.LeftTm = 0
    o.CanGetReward = false
    o.NextReward = 0
    o.FormatLeftTm = ""
    return o
end

---------------------------------------
function OnLineReward:Update()
    local tm = 1
    if (self.CanGetReward == false) then
        if (self.LeftTm > 0) then
            self.LeftTm = self.LeftTm - tm
            self.FormatLeftTm = CS.Casinos.LuaHelper.FormatTmFromSecondToMinute(self.LeftTm, false)
            local ev = self.ViewMgr:getEv("EvEntityRefreshLeftOnlineRewardTm")
            if (ev == nil) then
                ev = EvEntityRefreshLeftOnlineRewardTm:new(nil)
            end
            ev.left_reward_second = self.FormatLeftTm
            ev.give_chip_min = give_gold_min
            ev.is_success = is_success
            self.ViewMgr:sendEv(ev)

            if (self.LeftTm <= 0) then
                self.CanGetReward = true
                self:_sendCanGetReward()
            end
        end
    end
end

---------------------------------------
function OnLineReward:setOnlineRewardState(online_reward_state, left_reward_second, next_reward)
    self.OnlineRewardState = online_reward_state
    self.NextReward = next_reward
    if (self.OnlineRewardState == OnlineRewardState.Wait4GetReward)
    then
        self.CanGetReward = true
    else
        self.CanGetReward = false
        self.LeftTm = left_reward_second
    end
    self:_sendCanGetReward()
end

---------------------------------------
function OnLineReward:onGetReward()
    if (self.CanGetReward == true)
    then
        local ev = self.ViewMgr:getEv("EvRequestGetOnLineReward")
        if (ev == nil)
        then
            ev = EvRequestGetOnLineReward:new(nil)
        end
        self.ViewMgr:sendEv(ev)
    else
        ViewHelper:UiShowInfoSuccess(string.format(self.ViewMgr.LanMgr:getLanValue("OnlineReward"), tostring(self.FormatLeftTm), tostring(self.NextReward)))
    end
end

---------------------------------------
function OnLineReward:IfCanGetReward()
    return self.CanGetReward
end

---------------------------------------
function OnLineReward:_sendCanGetReward()
    local ev = self.ViewMgr:getEv("EvEntityCanGetOnlineReward")
    if (ev == nil)
    then
        ev = EvEntityCanGetOnlineReward:new(nil)
    end
    ev.can_getreward = self.CanGetReward
    self.ViewMgr:sendEv(ev)
end