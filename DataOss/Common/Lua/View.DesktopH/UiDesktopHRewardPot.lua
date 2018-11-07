-- Copyright(c) Cragon. All rights reserved.
-- TODO，目前被当做View使用，待整理

---------------------------------------
UiDesktopHRewardPot = ViewBase:new()

---------------------------------------
function UiDesktopHRewardPot:new(o, reward_pot, view_desktop)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.RewardPot = reward_pot
    o.GTextRewardPot = o.RewardPot:GetChild("RewardPot").asTextField
    o.ViewDesktopH = view_desktop
    o.MapRewardPotGolds = {}
    o.MapWinGold = {}
    o.MapFTaskerPlayPumpingBetPotGoldAni = {}
    o.ListLooseUiGold = {}
    o.RewardPot.onClick:Add(
            function()
                o:_onClick()
            end
    )
    o.ViewMgr = o.ViewDesktopH.ViewMgr
    return o
end

---------------------------------------
function UiDesktopHRewardPot:Destroy()
    for k, v in pairs(self.MapWinGold) do
        for g_k, g_v in pairs(v) do
            self.ViewDesktopH.UiDesktopHGoldPool:goldHEnPool(g_v)
        end
    end
    self:_cancelTask()
    self.MapWinGold = {}
    self.MapWinGold = nil
end

---------------------------------------
function UiDesktopHRewardPot:setSysPumpingGold(pot_index)
    local bet_pot = self.ViewDesktopH:getDesktopHBetPot(pot_index)
    local sys_pumping = self.MapRewardPotGolds[pot_index]
    if (sys_pumping ~= nil) then
        if (sys_pumping <= 0) then
            return
        end

        local list_golds = {}
        self.ViewDesktopH:createGolds(list_golds, nil, sys_pumping, bet_pot)
        self.MapWinGold[pot_index] = list_golds

        local to = self:_getRewardPotPos()
        local from = self.ViewDesktopH.UiDesktopHBanker:getBankPlayerCenterPos()
        local auto_destroy = true
        local gold_fix_pos = false
        local delay_tm = 0.0
        local delay_t = self.ViewDesktopH:GetMoveIntervalTm(#list_golds)
        if (pot_index ~= UiDesktopHBanker.BankerIndex) then
            auto_destroy = false
            gold_fix_pos = true

            local map_param = {}
            map_param[0] = sys_pumping
            map_param[1] = pot_index
            local t = CS.Casinos.FTMgr.Instance:startTask(UiDesktopHBetPot.GivePlayerAniTm - UiDesktopHBetPot.WinShowAniTm)
            local tasker = CS.Casinos.FTMgr.Instance:whenAll(map_param,
                    function(map_param)
                        self:_playBetPotSyspumpingGoldAni(map_param)
                    end,
                    t)
            self.MapFTaskerPlayPumpingBetPotGoldAni[pot_index] = tasker
        else
            self.MapWinGold[pot_index] = nil
            self.MapRewardPotGolds[pot_index] = nil

            local map_param = {}
            map_param[0] = sys_pumping
            map_param[1] = pot_index
            local t = CS.Casinos.FTMgr.Instance:startTask(UiDesktopHGold.MAX_CHIP_MOVE_TM)
            self.FTaskerSetPumpingGold = CS.Casinos.FTMgr.Instance:whenAll(map_param,
                    function(map_param)
                        self:_setPumpingGold(map_param)
                    end, t)
        end

        for k, v in pairs(list_golds) do
            if (pot_index ~= UiDesktopHBanker.BankerIndex) then
                to = bet_pot:getRandomChipPos()
            end

            v:initMove(from, to, UiDesktopHGold.MOVE_CHIP_TM, UiDesktopHGold.MOVE_SOUND, nil, nil, auto_destroy, delay_tm, gold_fix_pos)
            delay_tm = delay_tm + delay_t
        end
    end
end

---------------------------------------
function UiDesktopHRewardPot:showLooseGoldAni(pot_index, win_rewardpot_golds)
    if (win_rewardpot_golds == 0) then
        return
    end

    local from = self:_getRewardPotPos()
    if (pot_index == 255) then
        self.ListLooseUiGold = {}
        self.ViewDesktopH:createGolds(self.ListLooseUiGold, nil, win_rewardpot_golds, nil, 20)
        local to = self.ViewDesktopH.UiDesktopHBanker:getBankPlayerCenterPos()
        local delay_tm = 0
        local delay_t = self.ViewDesktopH:GetMoveIntervalTm(self.ListLooseUiGold.Count)
        for k, v in pairs(self.ListLooseUiGold) do
            v.initMove(from, to, UiDesktopHGold.MOVE_CHIP_TM, UiDesktopHGold.MOVE_SOUND, nil, nil, true, delay_tm, false)
            delay_tm = delay_tm + delay_t
        end
        self.ListLooseUiGold = {}
    else
        for k, v in pairs(self.ViewDesktopH:getDesktopHChairAll()) do
            if (v.SeatPlayerInfo ~= nil) then
                if (v.SeatPlayerInfo.PlayerInfoCommon.PlayerGuid ~= self.ViewDesktopH.ControllerPlayer.Guid) then
                    v:showWinGoldsAni(pot_index, from)
                end
            end
        end

        self.ViewDesktopH.UiDesktopHMe:showWinGoldsAni(pot_index, from)
        self.ViewDesktopH.UiDesktopHStandPlayer:showWinGoldsAni(pot_index, from)
    end

    if (self.CurrentTotalRewardPotGolds ~= self.CurrentNeedShowRewardPotGolds) then
        self.CurrentTotalRewardPotGolds = self.CurrentTotalRewardPotGolds - win_rewardpot_golds
        self:setRewardGolds1(self.CurrentTotalRewardPotGolds)
    end
end

---------------------------------------
function UiDesktopHRewardPot:SetRewardGolds(map_reward_golds, total_reward_golds)
    self.MapRewardPotGolds = map_reward_golds
    self.CurrentNeedShowRewardPotGolds = total_reward_golds
end

---------------------------------------
function UiDesktopHRewardPot:setRewardGolds1(reward_golds)
    self.CurrentTotalRewardPotGolds = reward_golds
    if (self.CurrentTotalRewardPotGolds < 0) then
        self.CurrentTotalRewardPotGolds = 0
    end
    self.GTextRewardPot.text = UiChipShowHelper:getGoldShowStr(self.CurrentTotalRewardPotGolds,
            self.ViewMgr.LanMgr.LanBase, false)
end

---------------------------------------
function UiDesktopHRewardPot:Reset()
    self.CurrentNeedShowRewardPotGolds = 0
    self.MapRewardPotGolds = {}
    for k, v in pairs(self.MapWinGold) do
        for g_k, g_v in pairs(v) do
            self.ViewDesktopH.UiDesktopHGoldPool:goldHEnPool(g_v)
        end
    end
    self:_cancelTask()
    self.MapWinGold = {}
end

---------------------------------------
function UiDesktopHRewardPot:_cancelTask()
    if (self.FTaskerSetPumpingGold ~= nil) then
        self.FTaskerSetPumpingGold:cancelTask()
        self.FTaskerSetPumpingGold = nil
    end
    for k, v in pairs(self.MapFTaskerPlayPumpingBetPotGoldAni) do
        if (v ~= nil) then
            v:cancelTask()
        end
    end
    self.MapFTaskerPlayPumpingBetPotGoldAni = {}
end

---------------------------------------
function UiDesktopHRewardPot:_getRewardPotPos()
    local pos = self.RewardPot.xy
    pos.x = pos.x + self.RewardPot.width * self.RewardPot.scaleX / 2
    pos.y = pos.y + self.RewardPot.height * self.RewardPot.scaleY / 2
    return pos
end

---------------------------------------
function UiDesktopHRewardPot:_setPumpingGold(map_param)
    local sys_pumping = map_param[0]
    --local pot_index = map_param[1]
    if (self.CurrentTotalRewardPotGolds ~= self.CurrentNeedShowRewardPotGolds) then
        self.CurrentTotalRewardPotGolds = self.CurrentTotalRewardPotGolds + sys_pumping
        self:setRewardGolds1(self.CurrentTotalRewardPotGolds)
    end
    self.FTaskerSetPumpingGold = nil
end

---------------------------------------
function UiDesktopHRewardPot:_playBetPotSyspumpingGoldAni(map_param)
    --local sys_pumping = map_param[0]
    local pot_index = map_param[1]
    local list_gold = self.MapWinGold[pot_index]
    if (list_gold ~= nil) then
        local to = self:_getRewardPotPos()
        local delay_tm = 0.0
        local delay_t = self.ViewDesktopH:GetMoveIntervalTm(#list_gold)
        --local bet_pot = self.ViewDesktopH:getDesktopHBetPot(pot_index)
        for k, v in pairs(list_gold) do
            v:initMove(v.GCoGold.xy, to,
                    UiDesktopHGold.MOVE_CHIP_TM, UiDesktopHGold.MOVE_SOUND, nil, nil, true, delay_tm, false)
            delay_tm = delay_tm + delay_t
        end
    end

    self.MapWinGold[pot_index] = nil
    self.MapRewardPotGolds[pot_index] = nil

    local t = CS.Casinos.FTMgr.Instance:startTask(UiDesktopHGold.MAX_CHIP_MOVE_TM)
    self.FTaskerSetPumpingGold = CS.Casinos.FTMgr.Instance:whenAll(map_param,
            function(map_param)
                self:_setPumpingGold(map_param)
            end, t)
end

---------------------------------------
function UiDesktopHRewardPot:_onClick()
    self.ViewMgr:CreateView("UiDesktopHRewardPot")
    local ev = self.ViewMgr:GetEv("EvDesktopHClickRewardPotBtn")
    if (ev == nil) then
        ev = EvDesktopHClickRewardPotBtn:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end