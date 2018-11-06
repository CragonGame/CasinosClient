-- Copyright(c) Cragon. All rights reserved.
-- ViewLotteryTicket模块数据结构定义

---------------------------------------
-- 单张牌数据结构
UiLotteryTicketCard = {}

function UiLotteryTicketCard:new(lottery_ticket, co_card)
    o = {}
    setmetatable(o, self)
    self.__index = self
    o.Context = Context
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.ViewLotteryTicket = lottery_ticket
    o.GCoCard = co_card
    o.GLoaderCard = o.GCoCard:GetChild("LoaderCard").asLoader
    o.GImageCardBack = o.GCoCard:GetChild("CardBack").asImage
    o.GLoaderCard.visible = false
    o.GImageCardBack.visible = true
    o.TweenTurnCard = nil
    return o
end