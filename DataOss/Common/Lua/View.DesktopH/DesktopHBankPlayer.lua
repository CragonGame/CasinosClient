-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
DesktopHBankPlayer = {
    ShowCardEndTime = 0.5,
    BankerIndex = 255,
    ShowWinRewardPotGoldsTm = 0.5
}

---------------------------------------
function DesktopHBankPlayer:new(o, co_bankplayer, bankplayer_nickname, bankplayer_gold,
                                bank_playercardtypeparent, bank_playercardtype, bank_cardtypebg, chat_parent, view_desktoph)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    o.GComBank = co_bankplayer
    o.GComBank.onClick:Add(
            function()
                o:_onClick()
            end
    )
    o.BankPlayerNickName = bankplayer_nickname
    o.BankPlayerGold = bankplayer_gold
    o.BankPlayerCardParent = bank_playercardtypeparent
    o.GLoaderBankPlayerCardType = bank_playercardtype
    o.BankPlayerCardTypeBg = bank_cardtypebg
    o.GCoChatParent = chat_parent
    o.ViewDesktopH = view_desktoph
    o.UiHeadIcon = ViewHeadIcon:new(nil, o.GComBank)
    o.BankDesktopHCards = o.ViewDesktopH.DesktopHDealer:createSinglePotCards(o.ViewDesktopH.ControllerDesktopH, o.ViewDesktopH, o.ViewDesktopH.GCoDealer.xy,
            o.BankPlayerCardParent, o, true)
    o.BankDesktopHCards:updateToPos(o.BankPlayerCardParent.xy)
    o:_setCardTypeVisible(false)
    o.BankPlayerDataDesktopH = nil
    o.MapFTaskerGetWinGold = {}
    o.ItemChat = nil
    o.WinRewardPotGold = 0

    return o
end

---------------------------------------
function DesktopHBankPlayer:destroy()
    self:_cancelTask()
end

---------------------------------------
function DesktopHBankPlayer:update(tm)
    if (self.ItemChat ~= nil) then
        self.ItemChat:update(tm)
    end
end

---------------------------------------
function DesktopHBankPlayer:setBankPlayerInfo(bankplayer_datadesktoph)
    local bankplayer_changed = false
    if (self.BankPlayerDataDesktopH == nil
            or self.BankPlayerDataDesktopH.PlayerInfoCommon.PlayerGuid ~= bankplayer_datadesktoph.PlayerInfoCommon.PlayerGuid) then
        bankplayer_changed = true
    end

    self.BankPlayerDataDesktopH = bankplayer_datadesktoph

    if (bankplayer_changed) then
        self.UiHeadIcon:setPlayerInfoDesktopH(self.BankPlayerDataDesktopH, true)
    end

    self.BankPlayerNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(self.BankPlayerDataDesktopH.PlayerInfoCommon.NickName, 12, 3)
    self:_setBankPlayerGolds()
end

---------------------------------------
function DesktopHBankPlayer:updateBankPlayerInfo(bankplayer_datadesktoph)
    self.BankPlayerDataDesktopH = bankplayer_datadesktoph
end

---------------------------------------
function DesktopHBankPlayer:setCardsBank(list_card)
    self.BankDesktopHCards:setCards(list_card)
end

---------------------------------------
function DesktopHBankPlayer:showCardsEnd()
    if (self.FTaskerShowCardEnd ~= nil) then
        self.FTaskerShowCardEnd:cancelTask()
        self.FTaskerShowCardEnd = nil
    end
    local card_type = self.BankDesktopHCards:getCardTypeStr()
    local cardtype_info = self.ViewDesktopH.UiDesktopHBase:getCardTypeAndSoundPath(card_type, true)
    if (CS.System.String.IsNullOrEmpty(cardtype_info.CardTypeSoundPath) == false) then
        CS.Casinos.CasinosContext.Instance:Play(cardtype_info.CardTypeSoundPath, CS.Casinos._eSoundLayer.LayerNormal)
    end

    local t = CS.Casinos.FTMgr.Instance:startTask(DesktopHBankPlayer.ShowCardEndTime)
    self.FTaskerShowCardEnd = CS.Casinos.FTMgr.Instance:whenAll(nil,
            function(map_param)
                self:_showCardEnd(map_param)
            end,
            t)
end

---------------------------------------
function DesktopHBankPlayer:winRewardPotGolds(win_rewardpot_golds)
    self.WinRewardPotGolds = win_rewardpot_golds
end

---------------------------------------
function DesktopHBankPlayer:showGameEndGoldAni()
    if (self.WinRewardPotGolds ~= nil and self.WinRewardPotGolds > 0) then
        if (self.FTaskerGetRewardGold ~= nil) then
            self.FTaskerGetRewardGold:cancelTask()
            self.FTaskerGetRewardGold = nil
        end

        local t = CS.Casinos.FTMgr.Instance:startTask(DesktopHBankPlayer.ShowWinRewardPotGoldsTm)
        self.FTaskerGetRewardGold = CS.Casinos.FTMgr.Instance:whenAll(nil,
                function(map_param)
                    self:_getWinGoldsFromRewardPot(map_param)
                end,
                t)
    end
end

---------------------------------------
function DesktopHBankPlayer:showWinGoldAni(win_gold, list_golds, pot_index)
    if (win_gold <= 0) then
        return
    end

    local delay_tm = 0.0
    local delay_t = self.ViewDesktopH:getMoveIntervalTm(#list_golds)
    local to = self:getBankPlayerCenterPos()
    for k, v in pairs(list_golds) do
        v:initMove(v.GCoGold.xy, to,
                DesktopHUiGold.MOVE_CHIP_TM, DesktopHUiGold.MOVE_SOUND, nil, nil, true, delay_tm, false)
        delay_tm = delay_tm + delay_t
    end

    local t = CS.Casinos.FTMgr.Instance:startTask(DesktopHUiGold.MAX_CHIP_MOVE_TM)
    local tasker = CS.Casinos.FTMgr.Instance:whenAll(nil,
            function(map_param)
                self:_getWinGoldDone(map_param)
            end,
            t)
    self.MapFTaskerGetWinGold[pot_index] = tasker
end

---------------------------------------
function DesktopHBankPlayer:_getWinGoldDone(map_param)
    self:_setBankPlayerGolds()
    self.ViewDesktopH.DesktopHRewardPot:setSysPumpingGold(DesktopHBankPlayer.BankerIndex)
    if (self.ViewDesktopH.ControllerDesktopH.IsBankPlayer) then
        self.ViewDesktopH.DesktopHSelf:addDeltaGold(GoldAccChangeReason.DesktopHWin)
    end
end

---------------------------------------
function DesktopHBankPlayer:giveGoldToPot(pot_index)
    local from = self:getBankPlayerCenterPos()
    for k, v in pairs(self.ViewDesktopH:getDesktopHChairAll()) do
        if (v.SeatPlayerInfoHundred == nil) then
        elseif (v.SeatPlayerInfoHundred.PlayerInfoCommon.PlayerGuid == self.ViewDesktopH.ControllerDesktopH.Guid) then
        else
            v:showWinGoldsAni(pot_index, from)
        end
    end

    self.ViewDesktopH.DesktopHRewardPot:setSysPumpingGold(pot_index)
    self.ViewDesktopH.DesktopHSelf:showWinGoldsAni(pot_index, from)
    self.ViewDesktopH.DesktopHStandPlayer:showWinGoldsAni(pot_index, from)

    self:_setBankPlayerGolds()
    if (self.ViewDesktopH.ControllerDesktopH.IsBankPlayer) then
        self.ViewDesktopH.DesktopHSelf:addDeltaGold(GoldAccChangeReason.DesktopHLoose)
    end
end

---------------------------------------
function DesktopHBankPlayer:reset()
    self:_setCardTypeVisible(false)
    self:_cancelTask()
    self.WinRewardPotGolds = 0
end

---------------------------------------
function DesktopHBankPlayer:setChatText(chat_info)
    local sorting_order = self.GCoChatParent.sortingOrder + self.ViewDesktopH.DesktopHGoldPool:getMaxGoldSortOrder()
    if (self.ItemChat == nil) then
        local co_chatname = "CoChatLeft"
        self.ItemChat = self.ViewDesktopH.UiDesktopChatParent:addChat(co_chatname, self.ViewDesktopH.ComUi, self.GCoChatParent.position)
        self.ItemChat:setChatTextAndSortingOrder(chat_info, sorting_order)
    else
        self.ItemChat:setChatTextAndSortingOrder(chat_info, sorting_order)
    end
end

---------------------------------------
--function DesktopHBankPlayer:getHandRankByte()
--    local l = self.BankDesktopHCards:getCardTypeByte()
--    return l
--end

---------------------------------------
function DesktopHBankPlayer:getBankPlayerCenterPos()
    local pos = self.UiHeadIcon.GCoHeadIcon.xy
    local x = pos.x
    x = x + self.UiHeadIcon.GCoHeadIcon.width / 2
    pos.x = x
    local y = pos.y
    y = y + self.UiHeadIcon.GCoHeadIcon.height / 2
    pos.y = y
    return pos
end

---------------------------------------
function DesktopHBankPlayer:sendMagicExp(sender_guid, exp_tbid)
    local tb_datamgr = TbDataMgr:new(nil)
    local tb_magicexp = tb_datamgr:GetData("UnitMagicExpression", exp_tbid)
    if (tb_magicexp == nil) then
        return
    end

    local from_pos = nil
    if (self.BankPlayerDataDesktopH.PlayerInfoCommon.PlayerGuid == sender_guid) then
        from_pos = self.ViewDesktopH.DesktopHBankPlayer:getBankPlayerCenterPos()
    else
        local chair = self.ViewDesktopH:getDesktopHChairByGuid(sender_guid)
        if (chair ~= nil) then
            from_pos = chair:getChairCenterPos()
        else
            from_pos = self.ViewDesktopH.DesktopHStandPlayer:getStandPlayerCenterPos()
        end
    end

    local to_pos = self:getBankPlayerCenterPos()
    local view_mgr = ViewMgr:new(nil)
    local ui_pool = view_mgr:GetView("Pool")
    local item_magicsender = ui_pool:getMagicExpSender()
    self.ViewDesktopH.ComUi:AddChild(item_magicsender.GCoMagicExpSender)
    item_magicsender:sendMagicExp(from_pos, to_pos, exp_tbid)
end

---------------------------------------
function DesktopHBankPlayer:_setBankPlayerGolds()
    local gold_str = UiChipShowHelper:getGoldShowStr(self.BankPlayerDataDesktopH.Gold,
            self.ViewDesktopH.ViewMgr.LanMgr.LanBase)
    if (DesktopHSysBankShowDBValue and
            CS.System.String.IsNullOrEmpty(self.BankPlayerDataDesktopH.PlayerInfoCommon.PlayerGuid)) then
        local sys_bank_initgold = self.ViewDesktopH.UiDesktopHBase:getSysBankPlayerInitGold()
        gold_str = UiChipShowHelper:getGoldShowStr(sys_bank_initgold, self.ViewDesktopH.ViewMgr.LanMgr.LanBase)
    end

    self.BankPlayerGold.text = gold_str
end

---------------------------------------
function DesktopHBankPlayer:_showCardEnd(map_param)
    local card_type = self.BankDesktopHCards:getCardTypeStr()
    self:_setCardTypeVisible(true)
    local cardtype_info = self.ViewDesktopH.UiDesktopHBase:getCardTypeAndSoundPath(card_type, true)
    self.GLoaderBankPlayerCardType.icon = cardtype_info.CardTypePath
    self.FTaskerShowCardEnd = nil
end

---------------------------------------
function DesktopHBankPlayer:_getWinGoldsFromRewardPot(map_param)
    self.ViewDesktopH.DesktopHRewardPot:showLooseGoldAni(DesktopHBankPlayer.BankerIndex, self.WinRewardPotGolds)
    local t = CS.Casinos.FTMgr.Instance:startTask(DesktopHUiGold.MAX_CHIP_MOVE_TM)
    local tasker = CS.Casinos.FTMgr.Instance:whenAll(map_param,
            function(map_param)
                self:_getWinGoldDone(map_param)
            end,
            t)
    self.MapFTaskerGetWinGold[255] = tasker
    self.FTaskerGetRewardGold = nil
end

---------------------------------------
function DesktopHBankPlayer:_setCardTypeVisible(bo)
    self.GLoaderBankPlayerCardType.visible = bo
    if (bo == false) then
        self.GLoaderBankPlayerCardType.icon = nil
    end
end

---------------------------------------
function DesktopHBankPlayer:_cancelTask()
    if (self.FTaskerShowCardEnd ~= nil) then
        self.FTaskerShowCardEnd:cancelTask()
        self.FTaskerShowCardEnd = nil
    end
    if (self.FTaskerGetRewardGold ~= nil) then
        self.FTaskerGetRewardGold:cancelTask()
        self.FTaskerGetRewardGold = nil
    end
    for key, value in pairs(self.MapFTaskerGetWinGold) do
        if (value ~= nil) then
            value:cancelTask()
        end
    end
    self.MapFTaskerGetWinGold = {}
end

---------------------------------------
function DesktopHBankPlayer:_playerInfo(player_info, head_icon)
    self.UiHeadIcon.GLoaderPlayerIcon.texture = CS.FairyGUI.NTexture(head_icon)
end

---------------------------------------
function DesktopHBankPlayer:_onClick()
    if (CS.System.String.IsNullOrEmpty(self.BankPlayerDataDesktopH.PlayerInfoCommon.PlayerGuid) == false) then
        local view_mgr = ViewMgr:new(nil)
        local ui_profileother = view_mgr:CreateView("PlayerProfile")
        ui_profileother:setPlayerGuid(CS.Casinos._ePlayerProfileType.DesktopH, self.BankPlayerDataDesktopH.PlayerInfoCommon.PlayerGuid,
                function(player_info, head_icon)
                    self:_playerInfo(player_info, head_icon)
                end
        )
    end
end