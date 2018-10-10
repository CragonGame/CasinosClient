-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ItemLotteryTicketCard = {}

---------------------------------------
function ItemLotteryTicketCard:new(o, co_card, lottery_ticket)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GCoCard = co_card
    o.GLoaderCard = o.GCoCard:GetChild("LoaderCard").asLoader
    o.GImageCardBack = o.GCoCard:GetChild("CardBack").asImage
    o.ViewLotteryTicket = lottery_ticket
    o.GLoaderCard.visible = false
    o.GImageCardBack.visible = true
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    return o
end

---------------------------------------
function ItemLotteryTicketCard:showCard(card_data)
    if (card_data == nil) then
        return
    end
    if (CS.FairyGUI.GTween.IsTweening(self.GImageCardBack)) then
        return
    end

    local card_name = string.format("%u", card_data.suit) .. "_" .. string.format("%u", card_data.type)
    self.GLoaderCard.icon = self.CasinosContext.PathMgr:combinePersistentDataPath(CS.Casinos.UiHelperCasinos:getABCardResourceTitlePath() .. tostring(card_name) .. ".ab")

    self.TweenerTurnCard = CS.FairyGUI.GTween.To(0, 180, 1)
                             :SetTarget(self.GImageCardBack)
                             :SetEase(CS.FairyGUI.EaseType.SineOut)
                             :OnUpdate(
            function()
                local x = self.TweenerTurnCard.value.x
                self.GImageCardBack.rotationY = x
                self.GLoaderCard.rotationY = -180 + x
                if (self.GLoaderCard.visible == false and x >= 90) then
                    self.GLoaderCard.visible = true
                    self.GImageCardBack.visible = false
                end
            end)
                             :OnComplete(
            function()
                self.TweenerTurnCard = nil
            end)

    self.CasinosContext:Play("desk_new_card", CS.Casinos._eSoundLayer.LayerNormal)
end

---------------------------------------
function ItemLotteryTicketCard:resetCard()
    if (self.TweenerTurnCard ~= nil) then
        self.TweenerTurnCard:Kill()
        self.TweenerTurnCard = nil
    end
    self.GLoaderCard.visible = false
    self.GLoaderCard.rotationY = 0
    self.GImageCardBack.visible = true
    self.GImageCardBack.rotationY = 0
end