-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UiDesktopHBanker = {
    ShowCardEndTime = 0.5,
    BankerIndex = 255,
    ShowWinRewardPotGoldsTm = 0.5
}

---------------------------------------
function UiDesktopHBanker:new(o, co_bankplayer, bankplayer_nickname, bankplayer_gold,
                                bank_playercardtypeparent, bank_playercardtype, bank_cardtypebg, chat_parent, view_desktoph)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Context = Context
    o.GComBank = co_bankplayer
    o.BankPlayerNickName = bankplayer_nickname
    o.BankPlayerGold = bankplayer_gold
    o.BankPlayerCardParent = bank_playercardtypeparent
    o.GLoaderBankPlayerCardType = bank_playercardtype
    o.BankPlayerCardTypeBg = bank_cardtypebg
    o.GCoChatParent = chat_parent
    o.ViewDesktopH = view_desktoph
    o.UiHeadIcon = ViewHeadIcon:new(nil, o.GComBank)
    o.BankDesktopHCards = o.ViewDesktopH.UiDesktopHDealer:createSinglePotCards(
            o.ViewDesktopH.ControllerDesktopH, o.ViewDesktopH, o.ViewDesktopH.GCoDealer.xy, o.BankPlayerCardParent, o, true)
    o.BankDesktopHCards:updateToPos(o.BankPlayerCardParent.xy)
    o:_setCardTypeVisible(false)
    o.BankPlayerDataDesktopH = nil
    o.MapFTaskerGetWinGold = {}
    o.ItemChat = nil
    o.WinRewardPotGold = 0
    o.GComBank.onClick:Add(
            function()
                o:_onClick()
            end
    )
    return o
end

---------------------------------------
function UiDesktopHBanker:Destroy()
    self:_cancelTask()
end

---------------------------------------
function UiDesktopHBanker:Update(tm)
    if (self.ItemChat ~= nil) then
        self.ItemChat:Update(tm)
    end
end

---------------------------------------
function UiDesktopHBanker:SetBankerInfo(bankplayer_datadesktoph)
    local bankplayer_changed = false
    if (self.BankPlayerDataDesktopH == nil
            or self.BankPlayerDataDesktopH.PlayerInfoCommon.PlayerGuid ~= bankplayer_datadesktoph.PlayerInfoCommon.PlayerGuid) then
        bankplayer_changed = true
    end

    self.BankPlayerDataDesktopH = bankplayer_datadesktoph

    if (bankplayer_changed) then
        self.UiHeadIcon:SetPlayerInfoDesktopH(self.BankPlayerDataDesktopH, true)
    end

    self.BankPlayerNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(self.BankPlayerDataDesktopH.PlayerInfoCommon.NickName, 12, 3)
    self:_setBankPlayerGolds()
end

---------------------------------------
function UiDesktopHBanker:RefreshBankerInfo(bankplayer_datadesktoph)
    self.BankPlayerDataDesktopH = bankplayer_datadesktoph
end

---------------------------------------
function UiDesktopHBanker:SetBankerCards(list_card)
    self.BankDesktopHCards:setCards(list_card)
end

---------------------------------------
function UiDesktopHBanker:showCardsEnd()
    if (self.FTaskerShowCardEnd ~= nil) then
        self.FTaskerShowCardEnd:cancelTask()
        self.FTaskerShowCardEnd = nil
    end
    local card_type = self.BankDesktopHCards:getCardTypeStr()
    local cardtype_info = self.ViewDesktopH.UiDesktopHBase:getCardTypeAndSoundPath(card_type, true)
    if (CS.System.String.IsNullOrEmpty(cardtype_info.CardTypeSoundPath) == false) then
        CS.Casinos.CasinosContext.Instance:Play(cardtype_info.CardTypeSoundPath, CS.Casinos._eSoundLayer.LayerNormal)
    end

    local t = CS.Casinos.FTMgr.Instance:startTask(UiDesktopHBanker.ShowCardEndTime)
    self.FTaskerShowCardEnd = CS.Casinos.FTMgr.Instance:whenAll(nil,
            function(map_param)
                self:_showCardEnd(map_param)
            end,
            t)
end

---------------------------------------
function UiDesktopHBanker:winRewardPotGolds(win_rewardpot_gold)
    self.WinRewardPotGolds = win_rewardpot_gold
end

---------------------------------------
function UiDesktopHBanker:showGameEndGoldAni()
    if (self.WinRewardPotGolds ~= nil and self.WinRewardPotGolds > 0) then
        if (self.FTaskerGetRewardGold ~= nil) then
            self.FTaskerGetRewardGold:cancelTask()
            self.FTaskerGetRewardGold = nil
        end

        local t = CS.Casinos.FTMgr.Instance:startTask(UiDesktopHBanker.ShowWinRewardPotGoldsTm)
        self.FTaskerGetRewardGold = CS.Casinos.FTMgr.Instance:whenAll(nil,
                function(map_param)
                    self:_getWinGoldsFromRewardPot(map_param)
                end, t)
    end
end

---------------------------------------
function UiDesktopHBanker:showWinGoldAni(win_gold, list_golds, pot_index)
    if (win_gold <= 0) then
        return
    end

    local delay_tm = 0.0
    local delay_t = self.ViewDesktopH:GetMoveIntervalTm(#list_golds)
    local to = self:getBankPlayerCenterPos()
    for k, v in pairs(list_golds) do
        v:initMove(v.GCoGold.xy, to,
                UiDesktopHGold.MOVE_CHIP_TM, UiDesktopHGold.MOVE_SOUND, nil, nil, true, delay_tm, false)
        delay_tm = delay_tm + delay_t
    end

    local t = CS.Casinos.FTMgr.Instance:startTask(UiDesktopHGold.MAX_CHIP_MOVE_TM)
    local tasker = CS.Casinos.FTMgr.Instance:whenAll(nil,
            function(map_param)
                self:_getWinGoldDone(map_param)
            end, t)
    self.MapFTaskerGetWinGold[pot_index] = tasker
end

---------------------------------------
function UiDesktopHBanker:_getWinGoldDone(map_param)
    self:_setBankPlayerGolds()
    self.ViewDesktopH.UiDesktopHRewardPot:setSysPumpingGold(UiDesktopHBanker.BankerIndex)
    if (self.ViewDesktopH.ControllerDesktopH.IsBankPlayer) then
        self.ViewDesktopH.UiDesktopHMe:addDeltaGold(GoldAccChangeReason.DesktopHWin)
    end
end

---------------------------------------
function UiDesktopHBanker:giveGoldToPot(pot_index)
    local from = self:getBankPlayerCenterPos()
    for k, v in pairs(self.ViewDesktopH:getDesktopHChairAll()) do
        if (v.SeatPlayerInfo == nil) then
        elseif (v.SeatPlayerInfo.PlayerInfoCommon.PlayerGuid == self.ViewDesktopH.ControllerDesktopH.Guid) then
        else
            v:showWinGoldsAni(pot_index, from)
        end
    end

    self.ViewDesktopH.UiDesktopHRewardPot:setSysPumpingGold(pot_index)
    self.ViewDesktopH.UiDesktopHMe:showWinGoldsAni(pot_index, from)
    self.ViewDesktopH.UiDesktopHStandPlayer:showWinGoldsAni(pot_index, from)

    self:_setBankPlayerGolds()
    if (self.ViewDesktopH.ControllerDesktopH.IsBankPlayer) then
        self.ViewDesktopH.UiDesktopHMe:addDeltaGold(GoldAccChangeReason.DesktopHLoose)
    end
end

---------------------------------------
function UiDesktopHBanker:Reset()
    self:_setCardTypeVisible(false)
    self:_cancelTask()
    self.WinRewardPotGolds = 0
end

---------------------------------------
function UiDesktopHBanker:setChatText(chat_info)
    local sorting_order = self.GCoChatParent.sortingOrder + self.ViewDesktopH.UiDesktopHGoldPool:getMaxGoldSortOrder()
    if (self.ItemChat == nil) then
        local co_chatname = "CoChatLeft"
        self.ItemChat = self.ViewDesktopH.UiDesktopChatParent:addChat(co_chatname, self.ViewDesktopH.ComUi, self.GCoChatParent.position)
        self.ItemChat:setChatTextAndSortingOrder(chat_info, sorting_order)
    else
        self.ItemChat:setChatTextAndSortingOrder(chat_info, sorting_order)
    end
end

---------------------------------------
--function UiDesktopHBanker:getHandRankByte()
--    local l = self.BankDesktopHCards:getCardTypeByte()
--    return l
--end

---------------------------------------
function UiDesktopHBanker:getBankPlayerCenterPos()
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
function UiDesktopHBanker:SendMagicExpression(sender_guid, exp_tbid)
    local tb_magicexp = self.Context.TbDataMgr:GetData("UnitMagicExpression", exp_tbid)
    if (tb_magicexp == nil) then
        return
    end

    local from_pos = nil
    if (self.BankPlayerDataDesktopH.PlayerInfoCommon.PlayerGuid == sender_guid) then
        from_pos = self.ViewDesktopH.UiDesktopHBanker:getBankPlayerCenterPos()
    else
        local chair = self.ViewDesktopH:getDesktopHChairByGuid(sender_guid)
        if (chair ~= nil) then
            from_pos = chair:getChairCenterPos()
        else
            from_pos = self.ViewDesktopH.UiDesktopHStandPlayer:getStandPlayerCenterPos()
        end
    end

    local to_pos = self:getBankPlayerCenterPos()
    local view_mgr = ViewMgr:new(nil)
    local ui_pool = view_mgr:GetView("Pool")
    local item_magicsender = ui_pool:getMagicExpSender()
    self.ViewDesktopH.ComUi:AddChild(item_magicsender.GCoMagicExpSender)
    item_magicsender:SendMagicExpression(from_pos, to_pos, exp_tbid)
end

---------------------------------------
function UiDesktopHBanker:_setBankPlayerGolds()
    local gold_str = UiChipShowHelper:getGoldShowStr(self.BankPlayerDataDesktopH.Gold, self.ViewDesktopH.ViewMgr.LanMgr.LanBase)
    if (self.Context.Cfg.DesktopHSysBankShowDBValue and CS.System.String.IsNullOrEmpty(self.BankPlayerDataDesktopH.PlayerInfoCommon.PlayerGuid)) then
        local sys_bank_initgold = self.ViewDesktopH.UiDesktopHBase:getSysBankPlayerInitGold()
        gold_str = UiChipShowHelper:getGoldShowStr(sys_bank_initgold, self.ViewDesktopH.ViewMgr.LanMgr.LanBase)
    end
    self.BankPlayerGold.text = gold_str
end

---------------------------------------
function UiDesktopHBanker:_showCardEnd(map_param)
    local card_type = self.BankDesktopHCards:getCardTypeStr()
    self:_setCardTypeVisible(true)
    local cardtype_info = self.ViewDesktopH.UiDesktopHBase:getCardTypeAndSoundPath(card_type, true)
    self.GLoaderBankPlayerCardType.icon = cardtype_info.CardTypePath
    self.FTaskerShowCardEnd = nil
end

---------------------------------------
function UiDesktopHBanker:_getWinGoldsFromRewardPot(map_param)
    self.ViewDesktopH.UiDesktopHRewardPot:showLooseGoldAni(UiDesktopHBanker.BankerIndex, self.WinRewardPotGolds)
    local t = CS.Casinos.FTMgr.Instance:startTask(UiDesktopHGold.MAX_CHIP_MOVE_TM)
    local tasker = CS.Casinos.FTMgr.Instance:whenAll(map_param,
            function(map_param)
                self:_getWinGoldDone(map_param)
            end, t)
    self.MapFTaskerGetWinGold[255] = tasker
    self.FTaskerGetRewardGold = nil
end

---------------------------------------
function UiDesktopHBanker:_setCardTypeVisible(bo)
    self.GLoaderBankPlayerCardType.visible = bo
    if (bo == false) then
        self.GLoaderBankPlayerCardType.icon = nil
    end
end

---------------------------------------
function UiDesktopHBanker:_cancelTask()
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
function UiDesktopHBanker:_playerInfo(player_info, head_icon)
    self.UiHeadIcon.GLoaderPlayerIcon.texture = CS.FairyGUI.NTexture(head_icon)
end

---------------------------------------
function UiDesktopHBanker:_onClick()
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