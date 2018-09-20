ItemLotteryTicketBetPot = {}

function ItemLotteryTicketBetPot:new(o,co_betpot,gold_percent,lottery_ticket)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.GCoBetPot = co_betpot
    o.ViewLotteryTicket = lottery_ticket
    o.TbDataLotteryTicketGoldPercent = gold_percent
    o.GCoBetPot.onClick:Add(
		function()
			o:onClick()
		end
	)
    o.GTextBetChipsTotal = o.GCoBetPot:GetChild("BetChipsTotal").asTextField
    o.GTextBetChipsSelf = o.GCoBetPot:GetChild("BetChipsSelf").asTextField          
    o.GImageWinBg = o.GCoBetPot:GetChild("WinBg").asImage
    o.GImageWinBg.visible = false
    o.TranBet = o.GCoBetPot:GetTransition("AniBet")
    o.GTextBetChipsSelf.text = o.ViewLotteryTicket.ViewMgr.LanMgr:getLanValue("NoBet")
    o.ViewLotteryTicket.ViewLotteryTicketBase:initBetPot(o.GCoBetPot, o.TbDataLotteryTicketGoldPercent)
	return o
end

function ItemLotteryTicketBetPot:initBetPot(betpot_index)
	self.BetPotIndex = betpot_index
end

function ItemLotteryTicketBetPot:isWin()
	self.GCoBetPot.alpha = 1
    self.GImageWinBg.visible = true
end

function ItemLotteryTicketBetPot:hideBetPot()
	self.GCoBetPot.alpha = 0.5
end

function ItemLotteryTicketBetPot:setBetPotInfo(betgold,self_golds)
	self:setBetPotTotalChips(betgold)
	self:setBetPotSelfChips(self_golds)
end

function ItemLotteryTicketBetPot:setBetPotTotalChips(total_chips)
	local text = ""
    if (total_chips ~= 0)
	then
		text = UiChipShowHelper:getGoldShowStr(total_chips, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase)
	end

    self.GTextBetChipsTotal.text = text
end

function ItemLotteryTicketBetPot:setBetPotSelfChips(self_betchips)
	self.SelfBetChips = self_betchips
    self.GTextBetChipsSelf.visible = true
    local text = UiChipShowHelper:getGoldShowStr(self.SelfBetChips, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase)
    if (self.SelfBetChips == 0)
	then
		text = self.ViewLotteryTicket.ViewMgr.LanMgr:getLanValue("NoBet")
	end
    self.GTextBetChipsSelf.text = text
end

function ItemLotteryTicketBetPot:resetBetPot()
	self.GTextBetChipsSelf.text = self.ViewLotteryTicket.ViewMgr.LanMgr:getLanValue("NoBet")
    self.GTextBetChipsTotal.text = ""
    self.SelfBetChips = 0
    self.GCoBetPot.alpha = 1
    self.GImageWinBg.visible = false
end

function ItemLotteryTicketBetPot:onClick()
	self.TranBet:Play()
	local view_mgr = ViewMgr:new(nil)
	local ev = view_mgr:getEv("EvLotteryTicketBet")
	if(ev == nil)
	then
		ev = EvLotteryTicketBet:new(nil)
	end
	ev.bet_betpot_index = self.BetPotIndex
	view_mgr:sendEv(ev)
end

