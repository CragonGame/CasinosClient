-- Copyright(c) Cragon. All rights reserved.
require('RewardOnline')
require('RewardRelief')
require('RewardTiming')

---------------------------------------
ControllerReward = class(ControllerBase)

---------------------------------------
function ControllerReward:ctor(this, controller_data, controller_name)
    self.TimerUpdate = nil
    self.RewardOnline = RewardOnline
    self.RewardRelief = RewardRelief
    self.RewardTiming = RewardTiming
    self.RedPointRewardShow = false-- 小红点显示状态
    self.RewardOnline:Setup(self.ControllerMgr, self.ViewMgr, self)
    self.RewardRelief:Setup(self.ControllerMgr, self.ViewMgr, self)
    self.RewardTiming:Setup(self.ControllerMgr, self.ViewMgr, self)
end

---------------------------------------
function ControllerReward:OnCreate()
    self.ViewMgr:BindEvListener("EvViewRewardClickBtnTimingReward", self)
    self.ViewMgr:BindEvListener("EvViewRewardClickBtnOnlineReward", self)

    -- 获取在线奖励
    self.ControllerMgr.Rpc:RegRpcMethod2(self.MC.PlayerGetOnlineRewardRequestResult, function(result, reward)
        self:s2cPlayerGetOnlineRewardRequestResult(result, reward)
    end)
    -- 在线奖励推送
    self.ControllerMgr.Rpc:RegRpcMethod3(self.MC.PlayerGetOnlineRewardNotify, function(online_reward_state, left_reward_second, next_reward)
        self:s2cPlayerGetOnlineRewardNotify(online_reward_state, left_reward_second, next_reward)
    end)
    -- 定时奖励推送
    self.ControllerMgr.Rpc:RegRpcMethod1(CommonMethodType.PlayerGetTimingRewardNotify, function(r)
        self:OnPlayerGetTimingRewardNotify(r)
    end)
    -- 获取定时奖励
    self.ControllerMgr.Rpc:RegRpcMethod2(CommonMethodType.PlayerGetTimingRewardRequestResult, function(r1, r2)
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
        self.RewardTiming:OnClickBtnTimingReward()
    end
end

---------------------------------------
function ControllerReward:RefreshRedPoint()
    self.RedPointRewardShow = false
    if self.RewardOnline.CanGetReward or self.RewardTiming.CanGetReward then
        self.RedPointRewardShow = true
    end

    local ev = self:GetEv("EvCtrlRewardRedPointStateChange")
    if (ev == nil) then
        ev = EvCtrlRewardRedPointStateChange:new(nil)
    end
    ev.RedPointType = 'Reward';
    ev.Show = self.RedPointRewardShow;
    self:SendEv(ev)
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
ControllerRewardFactory = class(ControllerFactory)

function ControllerRewardFactory:GetName()
    return 'Reward'
end

function ControllerRewardFactory:CreateController(controller_data)
    local ctrl_name = self:GetName()
    local ctrl = ControllerReward:new(controller_data, ctrl_name)
    return ctrl
end