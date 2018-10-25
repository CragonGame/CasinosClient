-- Copyright(c) Cragon. All rights reserved.
-- 代表一个宝箱

ItemDesktopHBetReward = ViewBase:new()

function ItemDesktopHBetReward:new(o,bet_reward,co_betreward,betreward_tbid,state,view_mgr)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    o.ViewMgr = view_mgr
    o.BetReward = bet_reward
    o.GCoBetReward = co_betreward
    o.BetRewardTbId = betreward_tbid
    o.DesktopHDialyGetBetRewardState = state
    o.GTextBetRewardValue = o.GCoBetReward:GetChild("RewardValue").asTextField
    o.GTextBetValue = o.GCoBetReward:GetChild("BetValue").asTextField
    local tb_reward = o.ViewMgr.TbDataMgr:GetData("DesktopHBetReward",betreward_tbid)
    o.GTextBetRewardValue.text = UiChipShowHelper:getGoldShowStr
    (tb_reward.BetRewardValue, o.ViewMgr.LanMgr.LanBase)
    o.GTextBetValue.text = UiChipShowHelper:getGoldShowStr
    (tb_reward.BetValue, o.ViewMgr.LanMgr.LanBase)
    o.GCoBetReward.onClick:Add(
            function()
                o:_onClickBetReward()
            end
    )

    return o
end

function ItemDesktopHBetReward:_onClickBetReward()
    local tips = ""
    local tb_datamgr = self.ViewMgr.TbDataMgr
    if (self.DesktopHDialyGetBetRewardState == DesktopHDialyGetBetRewardState.Get)
    then
        tips = self.ViewMgr.LanMgr:getLanValue("HasBeenReceivedTreasure")
    else
        local reward_gold = 0
        local t_desktophbetreward = tb_datamgr:GetMapData("DesktopHBetReward")
        for k,v in pairs(t_desktophbetreward) do
            local state = self.BetReward.BDesktopHDialyBetReward.MapGetRewardState[k]
            if (state ~= DesktopHDialyGetBetRewardState.Get)
            then
                local tb_data = v
                if (k <= self.BetRewardTbId)
                then
                    reward_gold = reward_gold + tb_data.BetRewardValue
                end
            end
        end

        local tb_reward = tb_datamgr:GetData("DesktopHBetReward",self.BetRewardTbId)
        tips = string.format(self.ViewMgr.LanMgr:getLanValue("BetRechCanCollectBox"),
                UiChipShowHelper:getGoldShowStr(tb_reward.BetValue, self.ViewMgr.LanMgr.LanBase),
                UiChipShowHelper:getGoldShowStr(reward_gold, self.ViewMgr.LanMgr.LanBase))

        local ev = self.ViewMgr:GetEv("EvDesktopHGetBetReward")
        if(ev == nil)
        then
            ev = EvDesktopHGetBetReward:new(nil)
        end
        self.ViewMgr:SendEv(ev)
    end

    ViewHelper:UiShowInfoSuccess(tips)
end