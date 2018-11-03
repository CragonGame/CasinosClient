-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
DesktopHDealer = {
    DealPotCardsIntervalTm = 1
}

---------------------------------------
function DesktopHDealer:new(o, fac_name)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ListDesktopHSinglePotCards = {}
    o.QueDealCards = {}
    o.QueShowCards = {}
    o.QueItemCard = {}
    o.QueItemCardBankPlayer = {}
    o.DealPotCardsTm = 0
    o.DesktopHDealerBase = nil
    o.FTasker = nil
    o.SinglePotCardsCount = 5
    o.InitCardCount = 20
    o.InitCardBankPlayerCount = 5
    o.FacName = fac_name
    for i = 0, o.InitCardCount - 1 do
        o:_createCard(false)
    end
    for i = 0, o.InitCardBankPlayerCount - 1 do
        o:_createCard(true)
    end
    return o
end

---------------------------------------
function DesktopHDealer:Update(tm)
    if (self.DesktopHDealerBase ~= nil) then
        self.DesktopHDealerBase:Update(tm)
    end
    for k, v in pairs(self.ListDesktopHSinglePotCards) do
        v:Update(tm)
    end
end

---------------------------------------
function DesktopHDealer:Destroy()
    for k, v in pairs(self.ListDesktopHSinglePotCards) do
        v:Destroy()
    end
    self.ListDesktopHSinglePotCards = {}
    for k, v in pairs(self.QueDealCards) do
        v:Destroy()
    end
    self.QueDealCards = {}
    for k, v in pairs(self.QueShowCards) do
        v:Destroy()
    end
    self.QueShowCards = {}
    self.QueItemCard = {}
    self.QueItemCardBankPlayer = {}
end

---------------------------------------
function DesktopHDealer:createDealerBase(factory_name)
    --[[{
        if (factory_name.Equals(CasinosModule.ZhongFB.ToString()))
        {
#if USE_DESKTOPHZHONGFB
            self:DesktopHDealerBase = new DesktopHDealerZhongFB(this)
#endif
        }--]]
end

---------------------------------------
function DesktopHDealer:createSinglePotCards(controller_desktoph, view_desktoph, dealer_pos, card_parent,
                                             listener, is_bankplayer)
    local cards = DesktopHCards:new(nil, controller_desktoph, view_desktoph, self, dealer_pos, card_parent, listener, is_bankplayer, self.FacName)
    table.insert(self.ListDesktopHSinglePotCards, cards)
    return cards
end

---------------------------------------
function DesktopHDealer:dealCard(deal_cardcount, move_cardwidth_percent,
                                 map_userdata, auto_showcard)
    if (self.DesktopHDealerBase ~= nil) then
        self.DesktopHDealerBase:dealBegin(
                function()
                end, map_userdata)
    else
        for k, v in pairs(self.ListDesktopHSinglePotCards) do
            table.insert(self.QueDealCards, v)
        end
        local c = table.remove(self.QueDealCards, 1)
        c:dealCardToPosThenTranslation(deal_cardcount, move_cardwidth_percent, auto_showcard)

        local t = CS.Casinos.FTMgr.Instance:startTask(DesktopHDealer.DealPotCardsIntervalTm)
        local map_param = {}
        map_param[0] = deal_cardcount
        map_param[1] = move_cardwidth_percent
        map_param[2] = auto_showcard
        self.FTasker = CS.Casinos.FTMgr.Instance:whenAll(map_param,
                function(map_param)
                    self:_dealCardToPosThenTranslation(map_param)
                end, t)
    end
end

---------------------------------------
function DesktopHDealer:dealCardAtPos(deal_cardcount, move_cardwidth_percent)
    for i, v in pairs(self.ListDesktopHSinglePotCards) do
        v:dealCardAtPos1(deal_cardcount, move_cardwidth_percent)
    end
end

---------------------------------------
function DesktopHDealer:showCard(is_onebyone, showcard_offset_tm)
    for k, v in pairs(self.ListDesktopHSinglePotCards) do
        table.insert(self.QueShowCards, v)
    end
    local c = table.remove(self.QueShowCards, 1)
    c:showCard(is_onebyone)

    local showcard_interval_tm = DesktopHDealer.DealPotCardsIntervalTm + showcard_offset_tm
    local t = CS.Casinos.FTMgr.Instance:startTask(showcard_interval_tm)
    local map_param = {}
    map_param[0] = is_onebyone
    map_param[1] = showcard_interval_tm
    self.FTasker = CS.Casinos.FTMgr.Instance:whenAll(map_param,
            function(map_param)
                self:_showCard(map_param)
            end, t)
end

---------------------------------------
function DesktopHDealer:resetCard()
    for i, v in pairs(self.ListDesktopHSinglePotCards) do
        v:resetCard()
    end

    if (self.DesktopHDealerBase ~= nil) then
        self.DesktopHDealerBase:Reset()
    end
    self.QueDealCards = {}
    self.QueShowCards = {}
    if (self.FTasker ~= nil) then
        self.FTasker:cancelTask()
        self.FTasker = nil
    end
    self.DealPotCardsTm = 0
end

---------------------------------------
function DesktopHDealer:getCard(is_bankplayer)
    local card = nil
    if (is_bankplayer == true) then
        local l = #self.QueItemCardBankPlayer
        if (l == 0) then
            self:_createCard(true)
        end
        card = table.remove(self.QueItemCardBankPlayer, 1)
    else
        local l = #self.QueItemCard
        if (l == 0) then
            self:_createCard(false)
        end
        card = table.remove(self.QueItemCard, 1)
    end

    return card
end

---------------------------------------
function DesktopHDealer:setResetCard(card, is_bankplayer)
    if (is_bankplayer) then
        table.insert(self.QueItemCardBankPlayer, card)
    else
        table.insert(self.QueItemCard, card)
    end
end

---------------------------------------
function DesktopHDealer:_createCard(is_bankplayer)
    local hundred_card = DesktopHCard:new(nil, self, is_bankplayer)
    if (is_bankplayer) then
        table.insert(self.QueItemCardBankPlayer, hundred_card)
    else
        table.insert(self.QueItemCard, hundred_card)
    end
end

---------------------------------------
function DesktopHDealer:_dealCardToPosThenTranslation(map_param)
    local l = #self.QueDealCards
    if (l > 0) then
        local deal_cardcount = map_param[0]
        local move_cardwidth_percent = map_param[1]
        local auto_showcard = map_param[2]
        local c = table.remove(self.QueDealCards, 1)
        c:dealCardToPosThenTranslation(deal_cardcount, move_cardwidth_percent, auto_showcard)
        local t = CS.Casinos.FTMgr.Instance:startTask(DesktopHDealer.DealPotCardsIntervalTm)
        self.FTasker = CS.Casinos.FTMgr.Instance:whenAll(map_param,
                function(map_param)
                    self:_dealCardToPosThenTranslation(map_param)
                end, t)
    end
end

---------------------------------------
function DesktopHDealer:_showCard(map_param)
    local l = #self.QueShowCards
    if (l > 0) then
        local is_onebyone = map_param[0]
        local c = table.remove(self.QueShowCards, 1)
        c:showCard(is_onebyone)
        local showcard_offset_tm = map_param[1]
        local t = CS.Casinos.FTMgr.Instance:startTask(showcard_offset_tm)
        self.FTasker = CS.Casinos.FTMgr.Instance:whenAll(map_param,
                function(map_param)
                    self:_showCard(map_param)
                end, t)
    end
end