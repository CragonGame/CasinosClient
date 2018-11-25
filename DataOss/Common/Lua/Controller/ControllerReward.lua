-- Copyright(c) Cragon. All rights reserved.
require('RewardOnline')
require('RewardTiming')

---------------------------------------
ControllerReward = ControllerBase:new(nil)

---------------------------------------
function ControllerReward:new(o, controller_mgr, controller_data, guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Context = Context
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.ControllerData = controller_data
    o.ControllerMgr = controller_mgr
    o.MC = CommonMethodType
    o.Guid = guid
    o.ViewMgr = ViewMgr:new(nil)
    o.TimerUpdate = nil
    o.RewardOnline = RewardOnline:new(nil, o.ViewMgr)
    o.RewardTiming = RewardTiming:new(nil, o.ViewMgr)
    o.RedPointReward = false-- 小红点显示状态
    return o
end

---------------------------------------
function ControllerReward:OnCreate()
    self.ViewMgr:BindEvListener("EvRequestGetOnLineReward", self)
    self.ViewMgr:BindEvListener("EvViewRequestGetTimingReward", self)
    self.ViewMgr:BindEvListener("EvViewOnGetOnLineReward", self)

    -- 获取在线奖励
    self.ControllerMgr.RPC:RegRpcMethod2(self.MC.PlayerGetOnlineRewardRequestResult, function(result, reward)
        self:s2cPlayerGetOnlineRewardRequestResult(result, reward)
    end)
    -- 在线奖励推送
    self.ControllerMgr.RPC:RegRpcMethod3(self.MC.PlayerGetOnlineRewardNotify, function(online_reward_state, left_reward_second, next_reward)
        self:s2cPlayerGetOnlineRewardNotify(online_reward_state, left_reward_second, next_reward)
    end)
    -- 定时奖励推送
    self.ControllerMgr.RPC:RegRpcMethod1(CommonMethodType.PlayerGetTimingRewardNotify, function(r)
        self:OnPlayerGetTimingRewardNotify(r)
    end)
    -- 获取定时奖励
    self.ControllerMgr.RPC:RegRpcMethod2(CommonMethodType.PlayerGetTimingRewardRequestResult, function(r1, r2)
        self:OnPlayerGetTimingRewardRequestResult(r1, r2)
    end)

    self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(200, self, self._timerUpdate)
end

---------------------------------------
function ControllerReward:OnDestroy()
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ControllerReward:OnHandleEv(ev)
    if (ev.EventName == "EvRequestGetOnLineReward") then
        self.ControllerMgr.RPC:RPC0(self.MC.PlayerGetOnlineRewardRequest)
    elseif (ev.EventName == "EvViewOnGetOnLineReward") then
        self.RewardOnline:OnGetReward()
    elseif (ev.EventName == "EvViewRequestGetTimingReward") then
        local can_get = self.TimingReward:OnGetReward()
        if can_get then
            self.ControllerMgr.RPC:RPC0(self.MC.PlayerGetTimingRewardRequest)
        end
    end
end

---------------------------------------
function ControllerReward:s2cPlayerGetOnlineRewardRequestResult(result, reward)
    if (result == ProtocolResult.Success) then
        ViewHelper:UiShowInfoSuccess(string.format(self.ControllerMgr.LanMgr:getLanValue("GetOnlinReward"), tostring(reward)))
    else
        ViewHelper:UiShowInfoFailed(self.ControllerMgr.LanMgr:getLanValue("GetOnlinRewardFail"))
    end
end

---------------------------------------
function ControllerReward:s2cPlayerGetOnlineRewardNotify(online_reward_state, left_reward_second, next_reward)
    self.RewardOnline:SetOnlineRewardState(online_reward_state, left_reward_second, next_reward)
end

---------------------------------------
function ControllerReward:OnPlayerGetTimingRewardNotify(invite1)
    local reward = TimingRewardData:new(nil)
    reward:setData(invite1)
    self.RewardTiming:SetTimingRewardData(reward)
end

---------------------------------------
function ControllerReward:OnPlayerGetTimingRewardRequestResult(result, reward_gold)
    if result == ProtocolResult.Success then
        ViewHelper:UiShowInfoSuccess(string.format(self.ControllerMgr.LanMgr:getLanValue("GetRewardSuccess"), tostring(reward_gold)))
    end
end

---------------------------------------
function ControllerReward:_timerUpdate(tm)
    self.RewardOnline:Update()
end

---------------------------------------
ControllerRewardFactory = ControllerFactory:new()

---------------------------------------
function ControllerRewardFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerName = "Reward"
    return o
end

---------------------------------------
function ControllerRewardFactory:CreateController(controller_mgr, controller_data, guid)
    local controller = ControllerReward:new(nil, controller_mgr, controller_data, guid)
    return controller
end