ViewLotteryTicketTexas = {}

function ViewLotteryTicketTexas:new(o,view_lotteryticket)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	if(self.Instance == nil)
	then
		self.ViewLotteryTicket = view_lotteryticket
		self.Instance = o
	end

	return self.Instance
end

function ViewLotteryTicketTexas:findLotteryTicketCard(list_card)
	local com_card0 = self.ViewLotteryTicket.ComUi:GetChild("LoaderCard0").asCom
	local card0 = ItemLotteryTicketCard:new(nil,com_card0, self.ViewLotteryTicket)
	local com_card1 = self.ViewLotteryTicket.ComUi:GetChild("LoaderCard1").asCom
	local card1 = ItemLotteryTicketCard:new(nil,com_card1, self.ViewLotteryTicket)
	local com_card2 = self.ViewLotteryTicket.ComUi:GetChild("LoaderCard2").asCom
	local card2 = ItemLotteryTicketCard:new(nil,com_card2, self.ViewLotteryTicket)
	local com_card3 = self.ViewLotteryTicket.ComUi:GetChild("LoaderCard3").asCom
	local card3 = ItemLotteryTicketCard:new(nil,com_card3, self.ViewLotteryTicket)
	local com_card4 = self.ViewLotteryTicket.ComUi:GetChild("LoaderCard4").asCom
	local card4 = ItemLotteryTicketCard:new(nil,com_card4, self.ViewLotteryTicket)
	table.insert(list_card,card0)
	table.insert(list_card,card1)
	table.insert(list_card,card2)
	table.insert(list_card,card3)
	table.insert(list_card,card4)
end

function ViewLotteryTicketTexas:initBetPot(bet_pot,gold_percent)
	local loader_betpotinfo = bet_pot:GetChild("LoaderCardType").asLoader
	local packageName = self.ViewLotteryTicket.LotteryTicketPackName
	if (CS.Casinos.CasinosContext.Instance.UseLan)
	then
		packageName = self.ViewLotteryTicket.ViewMgr.LanMgr:getLanPackageName()
	end
	loader_betpotinfo.url = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(packageName, "BetPotInfo" .. tostring(gold_percent.Id))
end

function ViewLotteryTicketTexas:getBetPotIndex(card_type)
	if (card_type <= 2)
	then
		card_type = 0
	elseif(card_type >= 7)
	then
		card_type = 5
	else
		card_type = card_type - 2
	end
	return card_type
end

function ViewLotteryTicket:getCardType(list_card)
	local rank_type = CS.Casinos.CardTypeHelperTexas.GetHandRankHTexas(list_card)
	local r_t
	if rank_type ==  CS.Casinos.HandRankTypeTexasH.HighCard then
		r_t = 0
	elseif rank_type ==  CS.Casinos.HandRankTypeTexasH.Pair then
		r_t = 1
	elseif rank_type ==  CS.Casinos.HandRankTypeTexasH.TwoPairs then
		r_t = 2
	elseif rank_type ==  CS.Casinos.HandRankTypeTexasH.ThreeOfAKind then
		r_t = 3
	elseif rank_type ==  CS.Casinos.HandRankTypeTexasH.Straight then
		r_t = 4
	elseif rank_type ==  CS.Casinos.HandRankTypeTexasH.Flush then
		r_t = 5
	elseif rank_type ==  CS.Casinos.HandRankTypeTexasH.FullHouse then
		r_t = 6
	elseif rank_type ==  CS.Casinos.HandRankTypeTexasH.FourOfAKind then
		r_t = 7
	elseif rank_type ==  CS.Casinos.HandRankTypeTexasH.StraightFlush then
		r_t = 8
	elseif rank_type ==  CS.Casinos.HandRankTypeTexasH.RoyalFlush then
		r_t = 9
	end
	return r_t
end

function ViewLotteryTicketTexas:getCardTypeName(card_type)
	local rank_type = CS.Casinos.HandRankTypeTexasH.__CastFrom(card_type) --card_type--CS.Casinos.HandRankTypeTexasH.__CastFrom(card_type)
	local rank_name = ""
	if (rank_type == CS.Casinos.HandRankTypeTexasH.RoyalFlush or rank_type == CS.Casinos.HandRankTypeTexasH.StraightFlush
			or rank_type == CS.Casinos.HandRankTypeTexasH.FourOfAKind)
	then
		rank_name = "CaiChi"
	else
		rank_name = CS.Casinos.LuaHelper.ParseHandRankTypeTexasHToStr(rank_type)
	end

	return rank_name
end



UiLotteryTicketTexasFactory = {}

function UiLotteryTicketTexasFactory:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	return o
end

function UiLotteryTicketTexasFactory:GetName()
	return "Texas"
end

function UiLotteryTicketTexasFactory:CreateUiDesktopHBase(view_lotteryticket)
	local l = ViewLotteryTicketTexas:new(nil,view_lotteryticket)
	return l
end