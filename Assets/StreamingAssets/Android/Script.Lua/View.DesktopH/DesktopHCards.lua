-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
DesktopHCards = {
    DEAL_CARD_TM = 0.1,
    SHOW_CARD_TM = 5,
    ShowOneCardIntervalTm = 1
}

---------------------------------------
function DesktopHCards:new(o, controller_desktoph, view_desktoph, dealer, dealer_pos, card_parent, listener, is_bankplayer, fac_name)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.DealerPos = dealer_pos
    o.GCoCardParent = card_parent
    o.DealCardTm = 0
    o.CanDealCard = false
    o.CanShowCard = false
    o.AutoShowCard = false
    o.DesktopHundredDealer = dealer
    o.DesktopHShowCardsListener = listener
    o.ListDesktopHCard = {}
    o.QueDealDesktopHCard = {}
    o.QueShowCard = {}
    o.ViewDesktopH = view_desktoph
    o.IsBankPlayer = is_bankplayer
    o.ControllerDesktopH = controller_desktoph
    o.ListCard = {}
    o.ShowCardTm = 0
    o.ShowOneCardInterverTime = 0
    o.MoveCardWidthPercent = 0
    local fac = o.ViewDesktopH.ControllerDesktopH:GetDesktopHCardTypeBaseFactory(fac_name)
    o.DesktopHCardTypeBase = fac:CreateDesktopHCardType()
    o.ToPos = nil
    return o
end

---------------------------------------
function DesktopHCards:destroy()
    if (self.ListCard ~= nil) then
        self.ListCard = {}
        self.ListCard = nil
    end
    self.ListDesktopHCard = {}
    self.ListDesktopHCard = nil
    self.QueDealDesktopHCard = {}
    self.QueDealDesktopHCard = nil
    self.QueShowCard = {}
    self.QueShowCard = nil
end

---------------------------------------
function DesktopHCards:update(elapsed_tm)
    if (self.CanDealCard) then
        self.DealCardTm = self.DealCardTm + elapsed_tm
        if (self.DealCardTm >= DesktopHCards.DEAL_CARD_TM) then
            self.DealCardTm = 0
            local l = #self.QueDealDesktopHCard
            if (l > 0) then
                local card = table.remove(self.QueDealDesktopHCard, 1)
                card:dealCardToPosThenTranslation(self.ToPos, self.MoveCardWidthPercent)
            else
                self.CanDealCard = false
                if (self.AutoShowCard) then
                    self.CanShowCard = true
                end
            end
        end
    end

    if (self.CanShowCard) then
        self.ShowCardTm = self.ShowCardTm + elapsed_tm
        if (self.ShowCardTm >= DesktopHCards.SHOW_CARD_TM) then
            self.CanShowCard = false
            self.ShowCardTm = 0
            self:showCard()
        end
    end

    local l = #self.QueShowCard
    if (l > 0) then
        self.ShowOneCardInterverTime = self.ShowOneCardInterverTime + elapsed_tm
        if (self.ShowOneCardInterverTime >= DesktopHCards.ShowOneCardIntervalTm) then
            self.ShowOneCardInterverTime = 0
            local card = table.remove(self.QueShowCard, 1)
            card:showCard(
                    function()
                        l = #self.QueShowCard
                        if (l == 0) then
                            self.DesktopHShowCardsListener:showCardsEnd()
                        end
                    end
            )
        end
    end
end

---------------------------------------
function DesktopHCards:updateToPos(pos)
    self.ToPos = pos
end

---------------------------------------
function DesktopHCards:setCards(list_card)
    self.ListCard = list_card
    local list_c = {}
    for key, value in pairs(self.ListCard) do
        local c = CS.Casinos.Card(value.suit, value.type)
        table.insert(list_c, c)
    end

    self.DesktopHCardTypeBase:SetCard(list_c)

    for i, v in pairs(self.ListCard) do
        local card = nil
        if (self.AutoShowCard == true) then
            card = self.DesktopHundredDealer:getCard(self.IsBankPlayer)
            card:setParent(self.GCoCardParent)
            table.insert(self.ListDesktopHCard, card)
            card:setCardData(v)
        else
            if (#self.ListDesktopHCard >= #self.ListCard) then
                card = self.ListDesktopHCard[i]
            end
        end
        if (card ~= nil) then
            card:setCardData(self.ListCard[i])
        end
    end
end

---------------------------------------
function DesktopHCards:getCardTypeStr()
    local l = self.DesktopHCardTypeBase:GetCardTypeStr()
    return l
end

---------------------------------------
--function DesktopHCards:getCardTypeByte()
--    local l = self.DesktopHCardTypeBase:GetCardTypeByte()
--    return l
--end

---------------------------------------
function DesktopHCards:dealCardToPosThenTranslation(deal_cardcount, move_cardwidth_percent, auto_showcard)
    self.AutoShowCard = auto_showcard
    self.MoveCardWidthPercent = move_cardwidth_percent
    for i = 1, deal_cardcount do
        local card = nil
        if (self.AutoShowCard == false) then
            card = self.DesktopHundredDealer:getCard(self.IsBankPlayer)
            card:setParent(self.GCoCardParent)
            self.ListDesktopHCard[i] = card
        else
            card = self.ListDesktopHCard[i]
        end

        card:setDealCardData(self.DealerPos, i - 1)
        self.QueDealDesktopHCard[i] = card
    end

    local l = #self.QueDealDesktopHCard
    if (l > 0) then
        local card = table.remove(self.QueDealDesktopHCard, 1)
        card:dealCardToPosThenTranslation(self.ToPos, self.MoveCardWidthPercent)
        self.CanDealCard = true
    end
end

---------------------------------------
function DesktopHCards:dealCardAtPos1(deal_cardcount, move_cardwidth_percent)
    self.AutoShowCard = false
    self.MoveCardWidthPercent = move_cardwidth_percent

    for i = 0, deal_cardcount - 1 do
        local card = self.DesktopHundredDealer:getCard(self.IsBankPlayer)
        card:setParent(self.GCoCardParent)
        table.insert(self.ListDesktopHCard, card)
        card:setDealCardData(self.DealerPos, i)
        card:dealCardAtPos2(self.ToPos, self.MoveCardWidthPercent)
        if (self.ListCard ~= nil and #self.ListCard > 0) then
            card:setCardData(self.ListCard[i + 1])
        end
    end
end

---------------------------------------
function DesktopHCards:showCard(is_onebyone)
    for k, v in pairs(self.ListDesktopHCard) do
        if (is_onebyone) then
            table.insert(self.QueShowCard, v)
        else
            v:showCard(
                    function()
                    end
            )
        end
    end

    local l = #self.QueShowCard
    if (l > 0) then
        local card = table.remove(self.QueShowCard, 1)
        card:showCard(
                function()
                end
        )
    end

    if (is_onebyone == false) then
        self.DesktopHShowCardsListener:showCardsEnd()
    end
end

---------------------------------------
function DesktopHCards:resetCard()
    for k, v in pairs(self.ListDesktopHCard) do
        v:resetCard()
    end
    self.ListDesktopHCard = {}
    self.QueDealDesktopHCard = {}
    self.QueShowCard = {}
    if (self.ListCard ~= nil) then
        self.ListCard = {}
    end
    self.CanDealCard = false
    self.CanShowCard = false
    self.AutoShowCard = false
    self.DealCardTm = 0
    self.ShowCardTm = 0
end