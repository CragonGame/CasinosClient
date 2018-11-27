-- Copyright(c) Cragon. All rights reserved.
-- 由ControllerReward管理

---------------------------------------
RewardOnline = {}

---------------------------------------
function RewardOnline:new(controller_mgr, view_mgr)
    local o = {}
    setmetatable(o, self)
    self.__index = self
    self.ViewMgr = view_mgr
    self.ControllerReward = ControllerReward
    self.ControllerMgr = controller_mgr
    self.MC = CommonMethodType
    self.OnlineRewardState = OnlineRewardState.CountDown
    self.LeftTm = 0
    self.CanGetReward = false
    self.NextReward = 0
    self.FormatLeftTm = ""
    return o
end

---------------------------------------
function RewardOnline:Update(tm)
    if (self.CanGetReward == false) then
        if (self.LeftTm > 0) then
            self.LeftTm = self.LeftTm - tm
            self.FormatLeftTm = CS.Casinos.LuaHelper.FormatTmFromSecondToMinute(self.LeftTm, false)

            local ev = self.ViewMgr:GetEv("EvCtrlRewardRefreshGetOnlineRewardLeftTm")
            if (ev == nil) then
                ev = EvCtrlRewardRefreshGetOnlineRewardLeftTm:new(nil)
            end
            ev.left_reward_second = self.FormatLeftTm
            ev.give_chip_min = give_gold_min
            ev.is_success = is_success
            self.ViewMgr:SendEv(ev)

            if (self.LeftTm <= 0) then
                self.CanGetReward = true
                self:RefreshCanGetOnlineRewardState()
            end
        end
    end
end

---------------------------------------
function RewardOnline:SetOnlineRewardState(online_reward_state, left_reward_second, next_reward)
    self.OnlineRewardState = online_reward_state
    self.NextReward = next_reward
    if (self.OnlineRewardState == OnlineRewardState.Wait4GetReward) then
        self.CanGetReward = true
        self.LeftTm = 0
    else
        self.CanGetReward = false
        self.LeftTm = left_reward_second
    end
    self:RefreshCanGetOnlineRewardState()
end

---------------------------------------
function RewardOnline:OnClickBtnOnlineReward()
    if (self.CanGetReward == true) then
        self.ControllerMgr.RPC:RPC0(self.MC.PlayerGetOnlineRewardRequest)
    else
        ViewHelper:UiShowInfoSuccess(string.format(self.ViewMgr.LanMgr:getLanValue("OnlineReward"), tostring(self.FormatLeftTm), tostring(self.NextReward)))
    end
end

---------------------------------------
function RewardOnline:RefreshCanGetOnlineRewardState()
    local view_reward = self.ViewMgr:GetView('Reward')
    if view_reward ~= nil then
        view_reward:RefreshCanGetOnlineRewardState(self.CanGetReward)
    end
    self.ControllerReward:RefreshRedPoint()-- 刷新小红点状态
end