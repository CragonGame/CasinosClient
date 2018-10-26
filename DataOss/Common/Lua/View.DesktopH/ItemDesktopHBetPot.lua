-- Copyright(c) Cragon. All rights reserved.
-- 一个下注池，4个池即有4个该类的实例；被DesktopHBetPot持有

---------------------------------------
ItemDesktopHBetPot = {}

---------------------------------------
function ItemDesktopHBetPot:new(o, ui_desktoph, parent, co_betpot)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewDesktopH = ui_desktoph
    o.GListParent = parent
    o.GCoBetPot = co_betpot
    o.GGraphBetPotArea = o.GCoBetPot:GetChild("BetPotArea").asGraph
    o.GCoBetPot.onClick:Add(
            function()
                o:_onClick()
            end
    )
    o.GTextBetChipsTotal = o.GCoBetPot:GetChild("BetChipsTotal").asTextField
    o.GTextBetChipsSelf = o.GCoBetPot:GetChild("BetChipsSelf").asTextField
    o.GCoCardParent = o.GCoBetPot:GetChild("CoCardParent").asCom
    o.GLoaderCardType = o.GCoBetPot:GetChild("LoaderCardType").asLoader
    local cardtype_bg = o.GCoBetPot:GetChild("CardTypeBg")
    local cardtypebg_image
    if (cardtype_bg == nil)
    then
        cardtypebg_image = nil
    else
        cardtypebg_image = cardtype_bg.asImage
    end
    o.GCardTypeBg = cardtypebg_image
    o:_setCardTypeVisible(false)
    o.GTextBetChipsSelf.visible = false
    o.IsWin = false
    o.WinRewardPotGolds = 0
    o.DesktopHundredCards = nil
    o.IsLastPot = false
    o.BetPotIndex = 0
    o.SelfBetChips = 0
    o.FTasker = nil
    return o
end

---------------------------------------
function ItemDesktopHBetPot:initBetPot(betpot_index, is_lastpot)
    self.BetPotIndex = betpot_index
    self.IsLastPot = is_lastpot
    self.DesktopHundredCards = self.ViewDesktopH.DesktopHDealer:createSinglePotCards(self.ViewDesktopH.ControllerDesktopH, self.ViewDesktopH,
            self.ViewDesktopH.GCoDealer.xy, self.GCoCardParent, self, false)
end

---------------------------------------
function ItemDesktopHBetPot:setGameResult(result, win_rewardpot_gold)
    self.IsWin = result.is_win
    self.WinRewardPotGolds = win_rewardpot_gold
    self.DesktopHundredCards:setCards(result.list_card)
    self:setBetPotTotalChips(result.bet_gold)
end

---------------------------------------
function ItemDesktopHBetPot:setBetPotTotalChips(total_chips)
    if (total_chips > 0)
    then
        self.GTextBetChipsTotal.text = UiChipShowHelper:getGoldShowStr2(total_chips, self.ViewDesktopH.ViewMgr.LanMgr.LanBase)
    else
        self.GTextBetChipsTotal.text = ""
    end
end

---------------------------------------
function ItemDesktopHBetPot:setBetPotSelfChips(self_betchips)
    self.SelfBetChips = self_betchips
    if (self.SelfBetChips > 0)
    then
        self.GTextBetChipsSelf.visible = true
        self.GTextBetChipsSelf.text = UiChipShowHelper:getGoldShowStr2(self.SelfBetChips, self.ViewDesktopH.ViewMgr.LanMgr.LanBase)
    else
        self.GTextBetChipsSelf.text = ""
        self.GTextBetChipsSelf.visible = false
    end
end

---------------------------------------
function ItemDesktopHBetPot:showCardsEnd()
    local card_type = self.DesktopHundredCards:getCardTypeStr()
    local cardtype_info = self.ViewDesktopH.UiDesktopHBase:getCardTypeAndSoundPath(card_type, self.IsWin)
    if (CS.System.String.IsNullOrEmpty(cardtype_info.CardTypeSoundPath) == false)
    then
        CS.Casinos.CasinosContext.Instance:Play(cardtype_info.CardTypeSoundPath, CS.Casinos._eSoundLayer.LayerNormal)
    end

    if (self.FTasker ~= nil)
    then
        self.FTasker:cancelTask()
        self.FTasker = nil
    end
    local t = CS.Casinos.FTMgr.Instance:startTask(DesktopHBankPlayer.ShowCardEndTime)
    self.FTasker = CS.Casinos.FTMgr.Instance:whenAll(nil,
            function(map_param)
                self:_showCardEnd(map_param)
            end
    , t)
end

---------------------------------------
function ItemDesktopHBetPot:resetBetPot()
    if (self.FTasker ~= nil)
    then
        self.FTasker:cancelTask()
        self.FTasker = nil
    end
    self.GTextBetChipsSelf.text = ""
    self.GTextBetChipsTotal.text = ""
    self.SelfBetChips = 0
    self:_setCardTypeVisible(false)
    self.GTextBetChipsSelf.visible = false
    self.GTextBetChipsSelf.color = CS.UnityEngine.Color.white
    self.ViewDesktopH.UiDesktopHBase:betPotIsReset(self)
end

---------------------------------------
function ItemDesktopHBetPot:Destroy()
    if (self.FTasker ~= nil)
    then
        self.FTasker:cancelTask()
    end
end

---------------------------------------
function ItemDesktopHBetPot:resetGoldsInfo()
    self.GTextBetChipsSelf.text = ""
    self.GTextBetChipsTotal.text = ""
    self.SelfBetChips = 0
    self.GTextBetChipsSelf.visible = false
    self.GTextBetChipsSelf.color = CS.UnityEngine.Color.white
end

---------------------------------------
function ItemDesktopHBetPot:_showCardEnd(map_param)
    self:_setCardTypeVisible(true)
    self.GTextBetChipsSelf.visible = true
    local color = CS.UnityEngine.Color.white
    local card_type = self.DesktopHundredCards:getCardTypeStr()
    local cardtype_info = self.ViewDesktopH.UiDesktopHBase:getCardTypeAndSoundPath(card_type, self.IsWin)
    self.GLoaderCardType.icon = cardtype_info.CardTypePath

    if (self.ViewDesktopH.ControllerDesktopH.IsBankPlayer == false)
    then
        local self_betresult = self.ViewDesktopH.ViewMgr.LanMgr:getLanValue("NotBet")
        if (self.IsWin)
        then
            --self_betresult = self.ViewDesktopH.ControllerDesktopH.DesktopHBase:getGameReusltTips(self.DesktopHundredCards:getCardTypeByte(), self.SelfBetChips)
            color = CS.UnityEngine.Color.yellow
        else
            --self_betresult = self.ViewDesktopH.ControllerDesktopH.DesktopHBase:getGameReusltTips(self.ViewDesktopH.DesktopHBankPlayer:getHandRankByte(), self.SelfBetChips)
        end

        self.GTextBetChipsSelf.text = self_betresult
        self.GTextBetChipsSelf.color = color
    end

    if (self.IsWin)
    then
        self.ViewDesktopH.UiDesktopHBase:betPotIsWin(self)
    end

    if (self.IsLastPot)
    then
        self.ViewDesktopH:showCardEnd()
    end
end

---------------------------------------
function ItemDesktopHBetPot:_setCardTypeVisible(bo)
    self.GLoaderCardType.visible = bo
    if (bo == false)
    then
        self.GLoaderCardType.icon = nil
    end
end

---------------------------------------
function ItemDesktopHBetPot:UpdateBetPotCardParentPos()
    local card_parentpos = self.GListParent.xy + self.GListParent.container.xy
            + self.GCoBetPot.xy + self.GCoCardParent.xy
    self.DesktopHundredCards:updateToPos(card_parentpos)
end

---------------------------------------
function ItemDesktopHBetPot:_onClick()
    self.ViewDesktopH:bet(self.BetPotIndex)
end