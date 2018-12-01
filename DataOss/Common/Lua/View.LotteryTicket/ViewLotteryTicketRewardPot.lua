-- Copyright(c) Cragon. All rights reserved.
-- 时时彩奖池信息

---------------------------------------
-- 时时彩奖池玩家列表单个玩家信息
UiLotteryTicketRewardPotPlayerInfoItem = {}

---------------------------------------
function UiLotteryTicketRewardPotPlayerInfoItem:new(o, view_lottery, com, player_info, tm)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.Context = Context
    o.ViewLotteryTicket = view_lottery
    o.GCoPlayerInfo = com
    o.GTextNickName = o.GCoPlayerInfo:GetChild("NickName").asTextField
    o.GTextGolds = o.GCoPlayerInfo:GetChild("Golds").asTextField
    o.GTextTm = o.GCoPlayerInfo:GetChild("Time").asTextField
    o:RefreshPlayerInfo(player_info)
    o.GTextTm.text = tm
    return o
end

---------------------------------------
function UiLotteryTicketRewardPotPlayerInfoItem:RefreshPlayerInfo(player_info)
    self.GTextNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(player_info.Nickname, 27, 8)
    self.GTextGolds.text = UiChipShowHelper:getGoldShowStr(player_info.WinGold, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase)
end

---------------------------------------
-- 时时彩奖池信息
ViewLotteryTicketRewardPot = {}

---------------------------------------
function ViewLotteryTicketRewardPot:new(o, reward_pot, btn_rewardpot, lottery_ticket)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.Context = Context
    o.RewardPot = reward_pot
    o.GTextRewardPot = o.RewardPot:GetChild("RewardPotTotal").asTextField
    o.GTextLastRoundBaoZiTm = o.RewardPot:GetChild("LastRoundBaoZiTm").asTextField
    o.GBtnRewardPot = btn_rewardpot
    o.ViewLotteryTicket = lottery_ticket
    o.ControllerRewardPot = o.ViewLotteryTicket.ComUi:GetController("ControllerRewardPot")
    o.GListRewardPotPlayer = o.ViewLotteryTicket.ComUi:GetChild("ListRewardPotPlayerInfo").asList
    o.GBtnRewardPot.onClick:Add(
            function()
                o:_onClick()
            end
    )
    local group = o.ViewLotteryTicket.ComUi:GetChild("GroupRewardPotPlayerInfo").asGroup
    local co_shade = o.ViewLotteryTicket.ComUi:GetChildInGroup(group, "CoShade").asCom
    co_shade.x = 0
    co_shade.onClick:Add(
            function()
                o:_onClickShade()
            end
    )
    o:SwitchControllerRewardPot(false)
    return o
end

---------------------------------------
function ViewLotteryTicketRewardPot:SetRewardGolds(total_reward_golds)
    self.RewardPotGolds = total_reward_golds
    self.GTextRewardPot.text = UiChipShowHelper:getGoldShowStr(self.RewardPotGolds, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase, false)
end

---------------------------------------
function ViewLotteryTicketRewardPot:SetRewardPotInfo(total_golds, list_playerinfo)
    self.GTextRewardPot.text = UiChipShowHelper:getGoldShowStr(total_golds, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase, false)
    if (list_playerinfo ~= nil) then
        self.GListRewardPotPlayer:RemoveChildrenToPool()
        for key, value in pairs(list_playerinfo) do
            local co_playerinfo = self.GListRewardPotPlayer:AddItemFromPool().asCom
            local l_tm = CS.System.DateTime.Parse(value.Dt)
            UiLotteryTicketRewardPotPlayerInfoItem:new(nil, self.ViewLotteryTicket, co_playerinfo, value, CS.Casinos.UiHelper.getLocalTmToString(l_tm))
        end
    end
end

---------------------------------------
function ViewLotteryTicketRewardPot:SwitchControllerRewardPot(show_rules)
    if (show_rules) then
        self.ControllerRewardPot.selectedIndex = 0
    else
        self.ControllerRewardPot.selectedIndex = 1
    end
end

---------------------------------------
function ViewLotteryTicketRewardPot:_onClickShade()
    self.ControllerRewardPot.selectedIndex = 1
end

---------------------------------------
function ViewLotteryTicketRewardPot:_onClick()
    self:SwitchControllerRewardPot(self.ControllerRewardPot.selectedIndex == 1)
    local view_mgr = ViewMgr
    local ev = view_mgr:GetEv("EvLotteryTicketClickRewardPotBtn")
    if (ev == nil) then
        ev = EvLotteryTicketClickRewardPotBtn:new(nil)
    end
    view_mgr:SendEv(ev)
end