-- Copyright(c) Cragon. All rights reserved.
-- 单个下注池

---------------------------------------
UiLotteryTicketBetPotItem = {}

---------------------------------------
function UiLotteryTicketBetPotItem:new(o, co_betpot, gold_percent, lottery_ticket)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.Context = Context
    o.GCoBetPot = co_betpot
    o.ViewLotteryTicket = lottery_ticket
    o.TbDataLotteryTicketGoldPercent = gold_percent
    o.GCoBetPot.onClick:Add(
            function()
                o:_onClick()
            end
    )
    o.GTextBetChipsTotal = o.GCoBetPot:GetChild("BetChipsTotal").asTextField
    o.GTextBetChipsSelf = o.GCoBetPot:GetChild("BetChipsSelf").asTextField
    o.GImageWinBg = o.GCoBetPot:GetChild("WinBg").asImage
    o.GImageWinBg.visible = false
    o.TranBet = o.GCoBetPot:GetTransition("AniBet")
    o.GTextBetChipsSelf.text = o.ViewLotteryTicket.ViewMgr.LanMgr:getLanValue("NoBet")
    o.ViewLotteryTicket.UiLotteryTicketBase:InitBetPot(o.GCoBetPot, o.TbDataLotteryTicketGoldPercent)
    return o
end

---------------------------------------
function UiLotteryTicketBetPotItem:InitBetPot(betpot_index)
    self.BetPotIndex = betpot_index
end

---------------------------------------
function UiLotteryTicketBetPotItem:IsWin()
    self.GCoBetPot.alpha = 1
    self.GImageWinBg.visible = true
end

---------------------------------------
function UiLotteryTicketBetPotItem:HideBetPot()
    self.GCoBetPot.alpha = 0.5
end

---------------------------------------
function UiLotteryTicketBetPotItem:SetBetPotInfo(betgold, self_golds)
    self:SetBetPotTotalChips(betgold)
    self:SetBetPotSelfChips(self_golds)
end

---------------------------------------
function UiLotteryTicketBetPotItem:SetBetPotTotalChips(total_chips)
    local text = ""
    if (total_chips ~= 0) then
        text = UiChipShowHelper:getGoldShowStr(total_chips, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase)
    end

    self.GTextBetChipsTotal.text = text
end

---------------------------------------
function UiLotteryTicketBetPotItem:SetBetPotSelfChips(self_betchips)
    self.SelfBetChips = self_betchips
    self.GTextBetChipsSelf.visible = true
    local text = UiChipShowHelper:getGoldShowStr(self.SelfBetChips, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase)
    if (self.SelfBetChips == 0) then
        text = self.ViewLotteryTicket.ViewMgr.LanMgr:getLanValue("NoBet")
    end
    self.GTextBetChipsSelf.text = text
end

---------------------------------------
function UiLotteryTicketBetPotItem:ResetBetPot()
    self.GTextBetChipsSelf.text = self.ViewLotteryTicket.ViewMgr.LanMgr:getLanValue("NoBet")
    self.GTextBetChipsTotal.text = ""
    self.SelfBetChips = 0
    self.GCoBetPot.alpha = 1
    self.GImageWinBg.visible = false
end

---------------------------------------
function UiLotteryTicketBetPotItem:_onClick()
    self.TranBet:Play()
    local view_mgr = ViewMgr
    local ev = view_mgr:GetEv("EvLotteryTicketBet")
    if (ev == nil) then
        ev = EvLotteryTicketBet:new(nil)
    end
    ev.bet_betpot_index = self.BetPotIndex
    view_mgr:SendEv(ev)
end