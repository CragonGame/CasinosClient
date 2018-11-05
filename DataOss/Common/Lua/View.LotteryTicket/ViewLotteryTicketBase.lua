-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewLotteryTicketBase = {}

---------------------------------------
function ViewLotteryTicketBase:new(o, view_lotteryticket)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function ViewLotteryTicket:findLotteryTicketCard(list_card)
end

---------------------------------------
function ViewLotteryTicket:initBetPot(bet_pot, gold_percent)
end

---------------------------------------
function ViewLotteryTicket:getBetPotIndex(card_type)
end

---------------------------------------
function ViewLotteryTicket:getCardTypeName(card_type)
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