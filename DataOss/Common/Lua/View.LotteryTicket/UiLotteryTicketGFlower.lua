-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UiLotteryTicketGFlower = {}

---------------------------------------
function UiLotteryTicketGFlower:new(view_lotteryticket)
    o = {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil) then
        o.Context = Context
        o.CasinosContext = CS.Casinos.CasinosContext.Instance
        o.ViewLotteryTicket = view_lotteryticket
        self.Instance = o
    end
    return self.Instance
end

---------------------------------------
function UiLotteryTicketGFlower:SetupLotteryTicketCardList(list_card)
    local com_card0 = self.ViewLotteryTicket.ComUi:GetChild("LoaderCard0").asCom
    local card0 = UiLotteryTicketCard:new(self.ViewLotteryTicket, com_card0)
    local com_card1 = self.ViewLotteryTicket.ComUi:GetChild("LoaderCard1").asCom
    local card1 = UiLotteryTicketCard:new(self.ViewLotteryTicket, com_card1)
    local com_card2 = self.ViewLotteryTicket.ComUi:GetChild("LoaderCard2").asCom
    local card2 = UiLotteryTicketCard:new(self.ViewLotteryTicket, com_card2)
    list_card:Add(card0)
    list_card:Add(card1)
    list_card:Add(card2)
end

---------------------------------------
function UiLotteryTicketGFlower:InitBetPot(bet_pot, gold_percent)
    local win_percent = bet_pot:GetChild("WinPercent").asTextField
    local win_percent_str = ""
    if (gold_percent.WinGoldsPercent == 0) then
        win_percent_str = "彩池金" .. gold_percent.WinRewardPotPercent * 100 .. "%"
    else
        win_percent_str = gold_percent.WinGoldsPercent .. "倍"
    end
    win_percent.text = win_percent_str

    local loader_betpotinfo = bet_pot:GetChild("LoaderCardType").asLoader
    local rank_type = CS.Casinos.HandRankTypeGFlowerH.__CastFrom(gold_percent.Id)
    local type_name = rank_type.ToString() .. "Win"
    loader_betpotinfo.icon = CS.Casinos.UiHelperCasinos:FormatePackageImagePath(self.ViewLotteryTicket.LotteryTicketPackName, type_name)
end

---------------------------------------
function UiLotteryTicketGFlower:GetBetPotIndex(card_type)
    return card_type
end

---------------------------------------
function UiLotteryTicketGFlower:GetCardType(list_card)
    local rank_type = CS.Casinos.CardTypeHelperGFlower.GetHandRankHGFlower(list_card)
    return rank_type
end

---------------------------------------
function UiLotteryTicketGFlower:GetCardTypeName(card_type)
    local rank_type = card_type--CS.Casinos.HandRankTypeGFlowerH.__CastFrom(card_type)
    if (rank_type == CS.Casinos.HandRankTypeGFlowerH.RoyalBaoZi) then
        rank_type = CS.Casinos.HandRankTypeGFlowerH.BaoZi
    end
    return rank_type:ToString()
end

---------------------------------------
UiLotteryTicketGFlowerFactory = {}

---------------------------------------
function UiLotteryTicketGFlowerFactory:GetName()
    return CasinosModule.GFlower:ToString()
end

---------------------------------------
function UiLotteryTicketGFlowerFactory:CreateUiDesktopHBase(view_lotteryticket)
    UiLotteryTicketGFlower(view_lotteryticket)
end