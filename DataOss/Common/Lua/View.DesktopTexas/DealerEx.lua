-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
_tWinnerDataEx = {}

---------------------------------------
function _tWinnerDataEx:new(player, win_chips, map_win_pot)
    o = {}
    setmetatable(o, self)
    self.__index = self
    o.player = player
    o.win_chips = win_chips
    o.map_win_pot = map_win_pot
    return o
end

---------------------------------------
_WinnerPlayerInfo = {}

---------------------------------------
function _WinnerPlayerInfo:new(win_playerguid, win_golds, map_win_pot)
    o = {}
    setmetatable(o, self)
    self.__index = self
    o.win_playerguid = win_playerguid
    o.win_golds = win_golds
    o.map_win_pot = map_win_pot
    return o
end

---------------------------------------
_tChairInfo = {}

---------------------------------------
function _tChairInfo:new(o, com_chair, com_sit, com_seatparent, image_sit, image_invite, index)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GComChair = com_chair
    o.GComSitOrInvite = com_sit
    o.GComSeatPlayerParent = com_seatparent
    o.GImagePlayerSit = image_sit
    o.GImagePlayerInvite = image_invite
    o.ChairIndex = index
    return o
end

---------------------------------------
DealerEx = {}

---------------------------------------
function DealerEx:new(o, ui_desktop, dealer_listener, card1, card2, card3, card4, card5)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.mQueueDealCards = {}
    o.mQueueGiveWinnerChips = {}
    o.mQueueDisabledCardDealing = {}
    o.mQueueDealingEnabledCardCommon = {}
    o.mQueueShowEnabledCardCommon = {}
    o.mQueueResetCardCommon = {}
    o.mCurrentDealingCardCommon = nil
    o.mCurrentShowingCardCommon = nil
    o.mCurrentResetCardCommon = nil
    o.mMapCardCommon = {}
    o.mActionDealCardDone = nil
    o.mActionDealOnePlayer = nil
    o.ActionGiveWinnerChipsDone = nil
    o.mIDealerListener = dealer_listener
    o.mUiDesktop = ui_desktop
    o.mCanDeal = false
    o.mCanShowWinnerCard = false
    o.mCanGiveWinnerChips = false
    o.mCanShowCommonCard = false
    o.mShowCommonCardTm = 0
    o.mDealCardTm = 0
    o.mShowWinnerCardTm = 0
    o.mGiveChipsTm = 0
    o:_createUiCardCommon(card1)
    o:_createUiCardCommon(card2)
    o:_createUiCardCommon(card3)
    o:_createUiCardCommon(card4)
    o:_createUiCardCommon(card5)
    o.CardBack = "CardBack"
    o.HighLight = "HighLight"
    o.DesktopPackageName = "Desktop"
    o.CardDealingName = "ComDealCard"
    o.DealPlayerCardTm = 0.3
    o.ShowCommonCardTm = 0.2
    o.ShowWinnerCardTm = 1
    o.GiveChipsTm = 0.5
    return o
end

---------------------------------------
function DealerEx:Update(elapsed_tm)
    if (self.mCanDeal == true) then
        self.mDealCardTm = self.mDealCardTm + elapsed_tm
        if (self.mDealCardTm >= self.DealPlayerCardTm) then
            self.mDealCardTm = 0
            local l = #self.mQueueDealCards
            if (l > 0) then
                if (self.mActionDealOnePlayer ~= nil) then
                    local player = table.remove(self.mQueueDealCards, 1)
                    self.mActionDealOnePlayer(player)
                end
            else
                if (self.mActionDealCardDone ~= nil) then
                    self.mActionDealCardDone()
                end

                self.mCanDeal = false
            end
        end
    end

    if (self.mCanGiveWinnerChips) then
        self.mGiveChipsTm = self.mGiveChipsTm + elapsed_tm
        if (self.mGiveChipsTm >= self.GiveChipsTm) then
            self.mGiveChipsTm = 0
            if self.GiveChipsTm ~= 1.5 then
                self.GiveChipsTm = self.GiveChipsTm + 1
            end
            local show_winnerchips_count = #self.mQueueGiveWinnerChips
            if (show_winnerchips_count > 0) then
                self:_giveChipsToPlayer()
            else
                self.mCanGiveWinnerChips = false
            end
        end
    end

    if (self.mShowCommonCardTm > 0) then
        self.mShowCommonCardTm = self.mShowCommonCardTm - elapsed_tm
        if (self.mShowCommonCardTm <= 0) then
            self.mCanShowCommonCard = true
        end
    end

    if (self.mCanShowCommonCard) then
        local show_enabled_cardcommon_count = #self.mQueueShowEnabledCardCommon
        if (show_enabled_cardcommon_count > 0) then
            if (self.mCurrentShowingCardCommon == nil) then
                self.mCurrentShowingCardCommon = table.remove(self.mQueueShowEnabledCardCommon, 1)
                self.mCurrentShowingCardCommon:show(true,
                        function()
                            self:_showCommonCardCallBack()
                        end)
                self.mCanShowCommonCard = false
            end
        end
    end
end

---------------------------------------
function DealerEx:Destroy()
    self.mQueueDealCards = {}
    self.mQueueDealCards = nil
    self.mQueueGiveWinnerChips = {}
    self.mQueueGiveWinnerChips = nil
    self.mQueueDisabledCardDealing = {}
    self.mQueueDisabledCardDealing = nil
    self.mQueueDealingEnabledCardCommon = {}
    self.mQueueDealingEnabledCardCommon = nil
    self.mQueueShowEnabledCardCommon = {}
    self.mQueueShowEnabledCardCommon = nil
    self.mQueueResetCardCommon = {}
    self.mQueueResetCardCommon = nil
    self.mCurrentDealingCardCommon = nil
    self.mCurrentShowingCardCommon = nil
    self.mCurrentResetCardCommon = nil
    self.mMapCardCommon = {}
    self.mActionDealCardDone = nil
    self.mActionGiveWinnerChipsDone = nil
    self.mActionResetCommonCardEnd = nil
    self.mIDealerListener = nil
end

---------------------------------------
function DealerEx:dealPlayerCard(list_player, action_dealcarddone, deal_oneplayer)
    self.mActionDealCardDone = action_dealcarddone
    self.mActionDealOnePlayer = deal_oneplayer

    for k, v in pairs(list_player) do
        local c_p = LuaHelper:TableContainsV(self.mQueueDealCards, v)
        if (c_p == false) then
            table.insert(self.mQueueDealCards, v)
        end
    end

    self.mCanDeal = true
end

---------------------------------------
function DealerEx:dealCommonCard(card, com_card)
    local card_common = self.mMapCardCommon[com_card.name]
    card_common:setCardData(card)

    print('3张公共牌发牌 开始')--被调用了3次
    local c = LuaHelper:TableContainsV(self.mQueueDealingEnabledCardCommon, card_common)
    if (c == false) then
        table.insert(self.mQueueDealingEnabledCardCommon, card_common)
    end

    local c1 = LuaHelper:TableContainsV(self.mQueueResetCardCommon, card_common)
    if (c1 == false) then
        table.insert(self.mQueueResetCardCommon, card_common)
    end

    if (self.mCurrentDealingCardCommon == nil) then
        self.mCurrentDealingCardCommon = table.remove(self.mQueueDealingEnabledCardCommon, 1)
        self.mCurrentDealingCardCommon:deal(
                function()
                    print('3张公共牌发牌 完毕')-- 被调用了1次
                    self:_dealCommonCardCallBack()
                end
        )
    end
end

---------------------------------------
function DealerEx:showCommonCard(card, com_card, bet_player_count)
    if (bet_player_count > 0) then
        self.mShowCommonCardTm = self.ShowCommonCardTm * bet_player_count
        self.mCanShowCommonCard = false
    else
        self.mCanShowCommonCard = true
    end

    local common_name = com_card.name
    local card_common = self.mMapCardCommon[common_name]
    card_common:setCardData(card)

    local c = LuaHelper:TableContainsV(self.mQueueResetCardCommon, card_common)
    if (c == false) then
        table.insert(self.mQueueResetCardCommon, card_common)
    end

    local cl = LuaHelper:TableContainsV(self.mQueueShowEnabledCardCommon, card_common)
    if (cl == false) then
        table.insert(self.mQueueShowEnabledCardCommon, card_common)
    end
end

---------------------------------------
function DealerEx:showCommonCardScreenshot(card, com_card)
    local card_common = self.mMapCardCommon[com_card.name]
    if (card_common ~= nil) then
        card_common:setCardData(card)
        card_common:show(false)

        local c = LuaHelper:TableContainsV(self.mQueueResetCardCommon, card_common)
        if (c == false) then
            table.insert(self.mQueueResetCardCommon, card_common)
        end
    end
end

---------------------------------------
function DealerEx:showCommonCardType(need_showhighlight, list_cards_data, list_cards_all_data, is_end)
    for k, v in pairs(self.mMapCardCommon) do
        v:showHighLight(need_showhighlight, list_cards_data, list_cards_all_data, is_end)
    end
end

---------------------------------------
function DealerEx:hideCommonCardType()
    for k, v in pairs(self.mMapCardCommon) do
        v:hideHightLight()
    end
end

---------------------------------------
function DealerEx:resetCommonCardType(action_resetend)
    self:_clearDealingAndShowQue()
    self.mActionResetCommonCardEnd = action_resetend

    local l = #self.mQueueResetCardCommon
    if (self.mCurrentResetCardCommon == nil and l > 0) then
        self.mCurrentResetCardCommon = table.remove(self.mQueueResetCardCommon, 1)
        self.mCurrentResetCardCommon:reset(true,
                function(card)
                    self:_resetCommonCardCallBack(card)
                end
        )
    end
end

---------------------------------------
function DealerEx:resetCard(com_card)
    local card_common = self.mMapCardCommon[com_card.name]
    if (card_common ~= nil) then
        card_common:reset(false)
    end
end

---------------------------------------
function DealerEx:clearCurrentResetCard()
    self:_clearDealingAndShowQue()

    for k, v in pairs(self.mMapCardCommon) do
        v:reset(false,
                function(card)
                    self:_resetCommonCardCallBack(card)
                end
        )
    end

    self.mQueueResetCardCommon = {}
    self.mCurrentResetCardCommon = nil
end

---------------------------------------
function DealerEx:clearQueue()
    self.mCanShowCommonCard = false
    self.mCanShowWinnerCard = false
    self.mCanGiveWinnerChips = false
    self.mGiveChipsTm = 0
    self.mShowWinnerCardTm = 0
    self.mShowCommonCardTm = 0
    self.mQueueResetCardCommon = {}
    self.mCurrentResetCardCommon = nil
    self.mQueueGiveWinnerChips = {}
    self:_clearDealingAndShowQue()
end

---------------------------------------
function DealerEx:_clearDealingAndShowQue()
    self.mCanDeal = false
    self.mDealCardTm = 0
    self.mQueueDealCards = {}
    self.mQueueDealingEnabledCardCommon = {}
    self.mCurrentDealingCardCommon = nil
    self.mQueueShowEnabledCardCommon = {}
    self.mCurrentShowingCardCommon = nil
end

---------------------------------------
function DealerEx:giveWinnerChips(list_winner, action_givechipsdone)
    self.mActionGiveWinnerChipsDone = action_givechipsdone
    local t_loser = {}
    for k, v in pairs(list_winner) do
        local player = self.mUiDesktop:getPlayer(v.win_playerguid)

        if (player ~= nil) then
            local winner_data = _tWinnerDataEx:new(player, v.win_golds, v.map_win_pot)
            if v.win_golds > 0 then
                local t_win = {}
                table.insert(t_win, winner_data)
                table.insert(self.mQueueGiveWinnerChips, t_win)
            else
                table.insert(t_loser, winner_data)
            end
        end
    end

    if #t_loser > 0 then
        table.insert(self.mQueueGiveWinnerChips, t_loser)
    end

    local l = #self.mQueueGiveWinnerChips
    if l > 3 then
        for i, v in pairs(self.mQueueGiveWinnerChips) do
            for j, j_v in pairs(v) do
                j_v.player.UiDesktopPlayerInfo:sendWinnerGolds(j_v.win_chips, j_v.map_win_pot)
            end
        end
        self.mQueueGiveWinnerChips = {}
        if (self.mActionGiveWinnerChipsDone ~= nil) then
            self.mActionGiveWinnerChipsDone()
            self.mActionGiveWinnerChipsDone = nil
        end
    else
        if l >= 1 then
            self.GiveChipsTm = 0.5
            self.mCanGiveWinnerChips = true
        end
    end
end

---------------------------------------
function DealerEx:getCardDealing()
    local card = table.remove(self.mQueueDisabledCardDealing, 1)
    if (card == nil) then
        local co_carddealing = CS.FairyGUI.UIPackage.CreateObject(self.DesktopPackageName, self.CardDealingName)
        self.mUiDesktop.ComUi:AddChild(co_carddealing)
        card = UiCardDealingEx:new(nil, co_carddealing.asCom)
    end
    return card
end

---------------------------------------
function DealerEx:cardObjDealingEnqueue(card)
    card:Reset()
    table.insert(self.mQueueDisabledCardDealing, card)
end

---------------------------------------
function DealerEx:_giveChipsToPlayer()
    local t_winner = table.remove(self.mQueueGiveWinnerChips, 1)

    if (t_winner == nil or #t_winner == 0) then
        return
    end

    for j, j_v in pairs(t_winner) do
        if j_v.player ~= nil and j_v.player.UiDesktopPlayerInfo ~= nil then
            j_v.player.UiDesktopPlayerInfo:sendWinnerGolds(j_v.win_chips, j_v.map_win_pot)
        end
    end

    local l = #self.mQueueGiveWinnerChips
    if (l == 0) then
        if (self.mActionGiveWinnerChipsDone ~= nil) then
            self.mActionGiveWinnerChipsDone()
            self.mActionGiveWinnerChipsDone = nil
        end
    end
end

---------------------------------------
function DealerEx:_dealCommonCardCallBack()
    if self.mQueueDealingEnabledCardCommon ~= nil then
        local l = #self.mQueueDealingEnabledCardCommon
        if (l > 0) then
            self.mCurrentDealingCardCommon = table.remove(self.mQueueDealingEnabledCardCommon, 1)
            self.mCurrentDealingCardCommon:deal(
                    function()
                        self:_dealCommonCardCallBack()
                    end
            )
        else
            self.mCurrentDealingCardCommon = nil
            self.mIDealerListener:commonCardDealEnd()
        end
    end
end

---------------------------------------
function DealerEx:_showCommonCardCallBack()
    local l = #self.mQueueShowEnabledCardCommon
    if (l > 0) then
        self.mCurrentShowingCardCommon = table.remove(self.mQueueShowEnabledCardCommon, 1)
        self.mCurrentShowingCardCommon:show(true,
                function()
                    self:_showCommonCardCallBack()
                end
        )
    else
        self.mCurrentShowingCardCommon = nil

        self.mIDealerListener:commonCardShowEnd()
    end
end

---------------------------------------
function DealerEx:_resetCommonCardCallBack(ui_card)
    local card_common = ui_card
    if (self.mCurrentResetCardCommon == card_common) then
        local l = #self.mQueueResetCardCommon
        if (l > 0) then
            self.mCurrentResetCardCommon = table.remove(self.mQueueResetCardCommon, 1)
            self.mCurrentResetCardCommon:reset(true,
                    function(card)
                        self:_resetCommonCardCallBack(card)
                    end
            )
        else
            self.mCurrentResetCardCommon = nil
            if self.mActionResetCommonCardEnd ~= nil then
                self.mActionResetCommonCardEnd()
                self.mActionResetCommonCardEnd = nil
            end
        end
    end
end

---------------------------------------
function DealerEx:_createUiCardCommon(card)
    local name = card.name
    local card_common = UiCardCommonEx:new(nil, card)
    self.mMapCardCommon[name] = card_common
end