-- Copyright(c) Cragon. All rights reserved.
require('RewardOnline')
require('RewardTiming')

---------------------------------------
ControllerReward = ControllerBase:new(nil)

---------------------------------------
function ControllerReward:new(controller_mgr, controller_data, guid)
    local o = {}
    setmetatable(o, self)
    self.__index = self
    self.Context = Context
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerData = controller_data
    self.ControllerMgr = controller_mgr
    self.MC = CommonMethodType
    self.Guid = guid
    self.ViewMgr = ViewMgr:new(nil)
    self.TimerUpdate = nil
    self.RewardOnline = RewardOnline:new(self.ControllerMgr, self.ViewMgr)
    self.RewardTiming = RewardTiming:new(self.ControllerMgr, self.ViewMgr)
    self.RedPointRewardShow = false-- 小红点显示状态
    return o
end

---------------------------------------
function ControllerReward:OnCreate()
    self.ViewMgr:BindEvListener("EvViewRewardClickBtnTimingReward", self)
    self.ViewMgr:BindEvListener("EvViewRewardClickBtnOnlineReward", self)

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
    if (ev.EventName == "EvViewRewardClickBtnOnlineReward") then
        self.RewardOnline:OnClickBtnOnlineReward()
    elseif (ev.EventName == "EvViewRewardClickBtnTimingReward") then
        self.TimingReward:OnClickBtnTimingReward()
    end
end

---------------------------------------
function ControllerReward:RefreshRedPoint()
    self.RedPointRewardShow = false
    if self.RewardOnline.CanGetReward or self.RewardTiming.CanGetReward then
        self.RedPointRewardShow = true
    end

    local ev = self.ControllerMgr.ViewMgr:GetEv("EvCtrlRewardRedPointStateChange")
    if (ev == nil) then
        ev = EvCtrlRewardRedPointStateChange:new(nil)
    end
    ev.RedPointType = 'Reward';
    ev.Show = self.RedPointRewardShow;
    self.ControllerMgr.ViewMgr:SendEv(ev)
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
    self.RewardOnline:Update(tm)
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
    local controller = ControllerReward:new(controller_mgr, controller_data, guid)
    return controller
end