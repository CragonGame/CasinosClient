-- Copyright(c) Cragon. All rights reserved.

ViewLotteryTicketRewardPot = {}

function ViewLotteryTicketRewardPot:new(o,reward_pot,btn_rewardpot,lottery_ticket)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.RewardPot = reward_pot
	o.GTextRewardPot = o.RewardPot:GetChild("RewardPotTotal").asTextField
	o.GTextLastRoundBaoZiTm = o.RewardPot:GetChild("LastRoundBaoZiTm").asTextField
	o.GBtnRewardPot = btn_rewardpot
	o.ViewLotteryTicket = lottery_ticket
	o.ControllerRewardPot = o.ViewLotteryTicket.ComUi:GetController("ControllerRewardPot")
	o.GListRewardPotPlayer = o.ViewLotteryTicket.ComUi:GetChild("ListRewardPotPlayerInfo").asList
	o.GBtnRewardPot.onClick:Add(
			function()
				o:onClick()
			end
	)
	local group = o.ViewLotteryTicket.ComUi:GetChild("GroupRewardPotPlayerInfo").asGroup
	local co_shade = o.ViewLotteryTicket.ComUi:GetChildInGroup(group, "CoShade").asCom
	co_shade.x = 0
	co_shade.onClick:Add(
			function()
				o:onClickShade()
			end
	)
	o:switchControllerRewardPot(false)

    return o
end

function ViewLotteryTicketRewardPot:setRewardGolds(total_reward_golds)
	self.RewardPotGolds = total_reward_golds
    self.GTextRewardPot.text = UiChipShowHelper:getGoldShowStr(self.RewardPotGolds, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase, false)
end

function ViewLotteryTicketRewardPot:setRewardPotInfo(total_golds,list_playerinfo)
	self.GTextRewardPot.text = UiChipShowHelper:getGoldShowStr(total_golds, self.ViewLotteryTicket.ViewMgr.LanMgr.LanBase, false)
	if (list_playerinfo ~= nil)
	then
		self.GListRewardPotPlayer:RemoveChildrenToPool()
		for key,value in pairs(list_playerinfo) do
			local co_playerinfo = self.GListRewardPotPlayer:AddItemFromPool().asCom
			local l_tm = CS.System.DateTime.Parse(value.Dt)
            ItemLotteryTicketRewardPotPlayerInfo:new(nil,self.ViewLotteryTicket,co_playerinfo, value, CS.Casinos.UiHelper.getLocalTmToString(l_tm))
		end
	end
end

function ViewLotteryTicketRewardPot:switchControllerRewardPot(show_rules)
	if(show_rules)
	then
		self.ControllerRewardPot.selectedIndex = 0
	else
		self.ControllerRewardPot.selectedIndex = 1
	end
end

function ViewLotteryTicketRewardPot:onClickShade()
	self.ControllerRewardPot.selectedIndex = 1
end

function ViewLotteryTicketRewardPot:onClick()
	self:switchControllerRewardPot(self.ControllerRewardPot.selectedIndex == 1)
	local view_mgr = ViewMgr:new(nil)
	local ev = view_mgr:GetEv("EvLotteryTicketClickRewardPotBtn")
	if(ev == nil)
	then
		ev = EvLotteryTicketClickRewardPotBtn:new(nil)
	end
	view_mgr:SendEv(ev)
end