-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UiLotteryTicketCard = {}

---------------------------------------
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

---------------------------------------
-- 翻牌，牌正面牌型图片是动态加载的
function UiLotteryTicketCard:ShowCard(card_data)
    -- CS.FairyGUI.GTween.IsTweening(self.GImageCardBack) == true
    if card_data == nil or self.TweenTurnCard ~= nil then
        return
    end

    local card_name = string.format("%u", card_data.suit) .. "_" .. string.format("%u", card_data.type)
    self.GLoaderCard.icon = self.CasinosContext.PathMgr.DirAbCard .. tostring(card_name) .. ".ab"

    self.TweenTurnCard = CS.FairyGUI.GTween.To(0, 180, 1)
                           :SetTarget(self.GImageCardBack)
                           :SetEase(CS.FairyGUI.EaseType.SineOut)
                           :OnUpdate(
            function()
                local x = self.TweenTurnCard.value.x
                self.GImageCardBack.rotationY = x
                self.GLoaderCard.rotationY = -180 + x
                if (self.GLoaderCard.visible == false and x >= 90) then
                    self.GLoaderCard.visible = true
                    self.GImageCardBack.visible = false
                end
            end)
                           :OnComplete(
            function()
                self.TweenTurnCard = nil
            end)

    self.CasinosContext:Play("desk_new_card", CS.Casinos._eSoundLayer.LayerNormal)
end

---------------------------------------
function UiLotteryTicketCard:ResetCard()
    if (self.TweenTurnCard ~= nil) then
        self.TweenTurnCard:Kill()
        self.TweenTurnCard = nil
    end
    self.GLoaderCard.visible = false
    self.GLoaderCard.rotationY = 0
    self.GImageCardBack.visible = true
    self.GImageCardBack.rotationY = 0
end