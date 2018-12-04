-- Copyright(c) Cragon. All rights reserved.
-- 救济金，由ControllerReward管理

---------------------------------------
RewardRelief = {}

---------------------------------------
function RewardRelief:Setup(controller_mgr, view_mgr, controller_reward)
    self.ControllerMgr = controller_mgr
    self.ViewMgr = view_mgr
    self.ControllerReward = controller_reward
    self.MC = CommonMethodType
    self.OnlineRewardState = OnlineRewardState.CountDown
    self.LeftTm = 0
    self.CanGetReward = false
    self.NextReward = 0
    self.FormatLeftTm = ""
end

---------------------------------------
function RewardRelief:SetOnlineRewardState(online_reward_state, left_reward_second, next_reward)
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
function RewardRelief:OnClickBtnOnlineReward()
    if (self.CanGetReward == true) then
        self.ControllerMgr.Rpc:RPC0(self.MC.PlayerGetOnlineRewardRequest)
    else
        ViewHelper:UiShowInfoSuccess(string.format(self.ViewMgr.LanMgr:getLanValue("OnlineReward"), tostring(self.FormatLeftTm), tostring(self.NextReward)))
    end
end

---------------------------------------
function RewardRelief:RefreshCanGetOnlineRewardState()
    local view_reward = self.ViewMgr:GetView('Reward')
    if view_reward ~= nil then
        view_reward:RefreshCanGetOnlineRewardState(self.CanGetReward)
    end
    self.ControllerReward:RefreshRedPoint()-- 刷新小红点状态
end