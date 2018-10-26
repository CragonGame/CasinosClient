-- Copyright(c) Cragon. All rights reserved.

DesktopHBetPot = {
    GivePlayerAniTm = 3.2,
    WinShowAniTm = 2
}

function DesktopHBetPot:new(o,betpot_index,bet_pot,ui_desktoph)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
	o.ItemDesktopHBetPot = bet_pot
	o.TotalBetPotGold = 0
    o.BetPotIndex = betpot_index
    o.ListUiGolds = {}
    o.ViewDesktopH = ui_desktoph
    o.BDesktopHNotifyGameEndBetPot = nil
    o.FTaskerShowGameEndGoldAni = nil
    o.FTaskerSendGoldToPlayer = nil
    o.LooseShowAniTm = 0.2
	o.MaxUiChipNum = 100
	o.PosOffSet = 20

    return o
end

function DesktopHBetPot:initBetPotInfo(bet_all_golds, self_betgolds)
            self:resetBetPot()

            self.TotalBetPotGold = bet_all_golds
            local ListTmpUiGolds= {}
            if (bet_all_golds > 0)
            then
                self.ViewDesktopH:createGolds(self.ListUiGolds, ListTmpUiGolds, bet_all_golds, self, 20)
            end

            ListTmpUiGolds = nil

            self.ItemDesktopHBetPot:setBetPotTotalChips(bet_all_golds)

            self:setBetSelfChipsToPot(self_betgolds)
end

function DesktopHBetPot:Destroy()
            self:_goldEnPool()
            self:_cancelTask()
            self.ItemDesktopHBetPot:Destroy()
end

function DesktopHBetPot:updateBetPotInfo(betpot_golds)
            self.TotalBetPotGold = self.TotalBetPotGold + betpot_golds
            self.ItemDesktopHBetPot:setBetPotTotalChips(self.TotalBetPotGold)
end

function DesktopHBetPot:setGameResult(betpot_gameresult, win_rewardpot_gold)
            self.BDesktopHNotifyGameEndBetPot = betpot_gameresult
            self.ItemDesktopHBetPot:setGameResult(self.BDesktopHNotifyGameEndBetPot, win_rewardpot_gold)
end

function DesktopHBetPot:betGolds(from, gold_value)
    local ListTmpUiGolds= {}
            self.ViewDesktopH:createGolds(self.ListUiGolds, ListTmpUiGolds, gold_value, self)
            local delay_tm = 0.0
            local l = #ListTmpUiGolds
            local delay_t = self.ViewDesktopH:getMoveIntervalTm(l)
			for k,v in pairs(ListTmpUiGolds) do
				local to = self:getRandomChipPos()
					v:initMove(from, to, DesktopHUiGold.MOVE_CHIP_TM, DesktopHUiGold.MOVE_SOUND, nil, nil, false, delay_tm, true)
						delay_tm = delay_tm + delay_t
			end
           ListTmpUiGolds = nil
end

function DesktopHBetPot:setBetSelfChipsToPot(self_betchips)
            self.ItemDesktopHBetPot:setBetPotSelfChips(self_betchips)
end

function DesktopHBetPot:showGameEndGoldAni(betpot_show_win_ani_tm, give_winplayer_gold_ani_tm)
            self:_cancelTask()
            local show_gameend_gold_ani_tm = 0
            if (self.ItemDesktopHBetPot.IsWin == false)
            then
                show_gameend_gold_ani_tm = self.LooseShowAniTm
            else            
                show_gameend_gold_ani_tm = betpot_show_win_ani_tm
                local t1 = CS.Casinos.FTMgr.Instance:startTask(give_winplayer_gold_ani_tm)
                self.FTaskerSendGoldToPlayer = CS.Casinos.FTMgr.Instance:whenAll(nil,
					function(map_param)
						self:_resetGoldInfo(map_param)
					end
				, t1)
            end

            local t = CS.Casinos.FTMgr.Instance:startTask(show_gameend_gold_ani_tm)
            self.FTaskerShowGameEndGoldAni = CS.Casinos.FTMgr.Instance:whenAll(nil,
				function(map_param)
					self:_showGameEndGoldAni(map_param)
				end
			, t)
end

function DesktopHBetPot:resetBetPot()
            self:_cancelTask()
            self.TotalBetPotGold = 0
            self:_goldEnPool()
            self.ItemDesktopHBetPot:resetBetPot()
            self.BDesktopHNotifyGameEndBetPot = nil
end

function DesktopHBetPot:_showGameEndGoldAni(map_param)
            if (self.ItemDesktopHBetPot.IsWin == false)
            then
                if (self.BDesktopHNotifyGameEndBetPot ~= nil)
                then
                    self.ViewDesktopH.DesktopHBankPlayer:showWinGoldAni(self.BDesktopHNotifyGameEndBetPot.winloose_gold, self.ListUiGolds, self.BetPotIndex)
                end
                self.ListUiGolds= {}
                self.ItemDesktopHBetPot:resetGoldsInfo()
            else
                self.ViewDesktopH.DesktopHBankPlayer:giveGoldToPot(self.BetPotIndex)
                self.ViewDesktopH.DesktopHRewardPot:showLooseGoldAni(self.BetPotIndex, self.ItemDesktopHBetPot.WinRewardPotGolds)
            end
            self.FTaskerShowGameEndGoldAni = nil
end

function DesktopHBetPot:_resetGoldInfo(map_param)        
            self.ItemDesktopHBetPot:resetGoldsInfo()
            self:_goldEnPool()
            self.FTaskerSendGoldToPlayer = nil
end

function DesktopHBetPot:_cancelTask()
            if (self.FTaskerShowGameEndGoldAni ~= nil)
            then
                self.FTaskerShowGameEndGoldAni:cancelTask()
                self.FTaskerShowGameEndGoldAni = nil
            end
            if (self.FTaskerSendGoldToPlayer ~= nil)
            then
                self.FTaskerSendGoldToPlayer:cancelTask()
                self.FTaskerSendGoldToPlayer = nil
            end
end

function DesktopHBetPot:getRandomChipPos()
            local pos = self.ItemDesktopHBetPot.GListParent.xy
                + self.ItemDesktopHBetPot.GListParent.container.xy
                + self.ItemDesktopHBetPot.GCoBetPot.xy
                + self.ItemDesktopHBetPot.GGraphBetPotArea.xy
            local betpotarea_halfwidth = self.ItemDesktopHBetPot.GGraphBetPotArea.width / 2
            local betpotarea_halfheight = self.ItemDesktopHBetPot.GGraphBetPotArea.height / 2
            pos.x = pos.x + betpotarea_halfwidth
            pos.y = pos.y + betpotarea_halfheight
            pos.x = pos.x + CS.UnityEngine.Random.Range(-betpotarea_halfwidth + self.PosOffSet, betpotarea_halfwidth - self.PosOffSet)
            pos.y = pos.y + CS.UnityEngine.Random.Range(-betpotarea_halfheight + self.PosOffSet, betpotarea_halfheight - self.PosOffSet)

            return pos
end

function DesktopHBetPot:_goldEnPool()
			for k,v in pairs(self.ListUiGolds) do
				self.ViewDesktopH.DesktopHGoldPool:goldHEnPool(v)
			end
            self.ListUiGolds= {}
end