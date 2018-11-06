-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UiLotteryTicketBase = {}

---------------------------------------
function UiLotteryTicketBase:new(o, view_lotteryticket)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function ViewLotteryTicket:FindLotteryTicketCard(list_card)
end

---------------------------------------
function ViewLotteryTicket:InitBetPot(bet_pot, gold_percent)
end

---------------------------------------
function ViewLotteryTicket:GetBetPotIndex(card_type)
end

---------------------------------------
function ViewLotteryTicket:GetCardTypeName(card_type)
end

---------------------------------------
UiLotteryTicketFactoryBase = {}

---------------------------------------
function UiLotteryTicketFactoryBase:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function UiLotteryTicketFactoryBase:GetName()
end

---------------------------------------
function UiLotteryTicketFactoryBase:CreateUiDesktopHBase(view_lotteryticket)
end