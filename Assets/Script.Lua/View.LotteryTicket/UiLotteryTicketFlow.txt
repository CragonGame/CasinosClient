-- Copyright(c) Cragon. All rights reserved.
-- 时时彩动画流程，所有的GTween动画都在此类中

---------------------------------------
UiLotteryTicketFlow = {}

---------------------------------------
function UiLotteryTicketFlow:new(lottery_ticket)
    o = {}
    setmetatable(o, self)
    self.__index = self
    o.Context = Context
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.ViewLotteryTicket = lottery_ticket
    return o
end

---------------------------------------
function UiLotteryTicketFlow:Close()
    self:_resetCardList()
end

---------------------------------------
function UiLotteryTicketFlow:InitLotteryTicketData(lotteryticket_data)
    if (lotteryticket_data.ListCard ~= nil) then
        self:_turnCardList(lotteryticket_data.ListCard)
    else
        self:_resetCardList()
    end
end

---------------------------------------
function UiLotteryTicketFlow:OnEnterBetState(map_betrepeatinfo)
    self:_resetCardList()
end

---------------------------------------
function UiLotteryTicketFlow:OnEnterGameEndState(gameend_detail, me_wingold)
    self:_turnCardList(gameend_detail.ListCard)
end

---------------------------------------
-- 翻牌，牌正面牌型图片是动态加载的
function UiLotteryTicketFlow:_turnCardList(list_card)
    for i, v in pairs(list_card) do
        local card_data = v
        local card = self.ViewLotteryTicket.UiCardList[i]
        if card_data ~= nil and card.TweenTurnCard == nil then
            -- CS.FairyGUI.GTween.IsTweening(self.GImageCardBack) == true
            local card_name = string.format("%u", card_data.suit) .. "_" .. string.format("%u", card_data.type)
            card.GLoaderCard.icon = self.CasinosContext.PathMgr.DirAbCard .. tostring(card_name) .. ".ab"

            card.TweenTurnCard = CS.FairyGUI.GTween.To(0, 180, 1)
            card.TweenTurnCard:SetTarget(card.GImageCardBack):SetEase(CS.FairyGUI.EaseType.SineOut)
                :OnUpdate(
                    function()
                        local x = card.TweenTurnCard.value.x
                        card.GImageCardBack.rotationY = x
                        card.GLoaderCard.rotationY = -180 + x
                        if (card.GLoaderCard.visible == false and x >= 90) then
                            card.GLoaderCard.visible = true
                            card.GImageCardBack.visible = false
                        end
                    end)
                :OnComplete(
                    function()
                        card.TweenTurnCard = nil
                    end)

            self.CasinosContext:Play("desk_new_card", CS.Casinos._eSoundLayer.LayerNormal)
        end
    end
end

---------------------------------------
function UiLotteryTicketFlow:_resetCardList()
    for i, card in pairs(self.ViewLotteryTicket.UiCardList) do
        if (card.TweenTurnCard ~= nil) then
            card.TweenTurnCard:Kill()
            card.TweenTurnCard = nil
        end
        card.GLoaderCard.visible = false
        card.GLoaderCard.rotationY = 0
        card.GImageCardBack.visible = true
        card.GImageCardBack.rotationY = 0
    end
end