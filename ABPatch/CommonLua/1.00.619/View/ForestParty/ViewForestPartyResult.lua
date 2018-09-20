ViewForestPartyResult = ViewBase:new()

function ViewForestPartyResult:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
    if(self.Instance==nil)
	then
		self.ViewMgr = nil
		self.GoUi = nil
		self.ComUi = nil
		self.Panel = nil
		self.UILayer = nil
		self.InitDepth = nil
		self.ViewKey = nil
		self.Instance = o
	end

    return self.Instance
end

function ViewForestPartyResult:onCreate()
	self.AutoDestroyTm = 4
    self.TransitionShowResult = self.ComUi:GetTransition("TransitionShowResult")
    self.ControllerBanker = self.ComUi:GetController("ControllerBanker")
    self.GTextSystemBankerName = self.ComUi:GetChild("TextSystemBankerName").asTextField
    self.GTextPlayerBankerName = self.ComUi:GetChild("TextPlayerBankerName").asTextField
    self.GTextBankerWinLoseGold = self.ComUi:GetChild("TextBankerWinLoseGold").asTextField
    self.GTextSelfName = self.ComUi:GetChild("TextSelfName").asTextField
    self.GTextSelfWinLoseGold = self.ComUi:GetChild("TextSelfWinLoseGold").asTextField
    self.GTextCajin = self.ComUi:GetChild("TextCajin").asTextField
    self.GGroupBanker = self.ComUi:GetChild("GroupBanker").asGroup
    self.GGroupSelf = self.ComUi:GetChild("GroupSelf").asGroup
    self.LoaderBankerWinLose = self.ComUi:GetChild("LoaderBankerWinLose").asLoader
    self.LoaderSelfWinLose = self.ComUi:GetChild("LoaderSelfWinLose").asLoader
    self.LoaderLotteryType = self.ComUi:GetChild("LoaderLotteryType").asLoader
    self.GListLotterySamll = self.ComUi:GetChild("ListLotterySmall").asList
    self.GListLotteryBig = self.ComUi:GetChild("ListLotteryBig").asList
    local btn_confirm = self.ComUi:GetChild("BtnConfirm").asButton
    btn_confirm.onClick:Add(
		function()
			self:onClickBtnConfirm()
		end
	)
    self:setLotteryResult()
end

function ViewForestPartyResult:onUpdate(tm)
	self.AutoDestroyTm = self.AutoDestroyTm - tm
    if (self.AutoDestroyTm <= 0)
	then
		self:destory()
	end
end

function ViewForestPartyResult:onHandleEv(ev)
	if(ev ~= nil)
	then
		if(ev.EventName == "EvEntityForestPartyBetState")
		then
			self:destory()
		end
	end
end

function ViewForestPartyResult:onClickBtnConfirm()
	self:destory()
end

function ViewForestPartyResult:setLotteryResult()
	local result = ControllerForestParty.GameResult
    local banker_data = ControllerForestParty.BankerData
    local banker_guid = banker_data.PlayerInfoCommon.PlayerGuid
    if (banker_guid == "")
	then
		self.ControllerBanker:SetSelectedIndex(0)
        self.GTextSystemBankerName.text = banker_data.PlayerInfoCommon.NickName
	else
	then
		self.ControllerBanker:SetSelectedIndex(1)
        self.GTextPlayerBankerName.text = banker_data.PlayerInfoCommon.NickName
        self.GTextBankerWinLoseGold.text = CS.Casinos.CasinosContext.Instance.UiChipShowHelper:getGoldShowStr(result.BankerWinLooseGold, CS.Casinos.CasinosContext.Instance.LanMgr.LanBase)
        if (result.BankerWinLooseGold >= 0)
		then
			self.LoaderBankerWinLose.url = CS.FairyGUI.UIPackage.GetItemURL("ForestPartyResult", "Win")
		else
		then
			self.LoaderBankerWinLose.url = CS.FairyGUI.UIPackage.GetItemURL("ForestPartyResult", "Lose")
		end
	end
    local self_bet = false
    local map_self_betpot = ControllerForestParty.MapSelfBetPot
    if (map_self_betpot.Count > 0)
	then
		self_bet = true
	end
    if (self_bet)
	then
		self.GTextSelfName.text = ControllerActor.Def.PropNickName:get()
        local self_winlose_gold = ControllerForestParty.SelfWinLoseGold
        self.GTextSelfWinLoseGold.text = CS.Casinos.CasinosContext.Instance.UiChipShowHelper:getGoldShowStr(self_winlose_gold, CS.Casinos.CasinosContext.Instance.LanMgr.LanBase)
        if (self_winlose_gold >= 0)
		then
			self.LoaderSelfWinLose.url = CS.FairyGUI.UIPackage.GetItemURL("ForestPartyResult", "Win")
		else
		then
			self.LoaderSelfWinLose.url = CS.FairyGUI.UIPackage.GetItemURL("ForestPartyResult", "Lose")
		end
	else
	then
		self.GGroupBanker.y = self.GGroupBanker.y + 40
        self.GGroupSelf.visible = false
	end
    local lottery_type = result.ResultLotteryType
    if (lottery_type ~= CS.Casinos.LotteryType.Normal)
	then
		if (lottery_type == CS.Casinos.LotteryType.FanBei)
		then
			if (result.Beishu == 2)
			then
				self.LoaderLotteryType.url = CS.FairyGUI.UIPackage.GetItemURL("ForestPartyResult", "FanBei2")
			else if(result.Beishu == 3)
			then
				self.LoaderLotteryType.url = CS.FairyGUI.UIPackage.GetItemURL("ForestPartyResult", "FanBei3")
			end
		else
		then
			self.LoaderLotteryType.url = CS.FairyGUI.UIPackage.GetItemURL("ForestPartyResult", lottery_type.ToString())
		end
	end
    if (lottery_type == CS.Casinos.LotteryType.CaiJin)
	then
		self.GTextCajin.text = result.Caijin.ToString()
	end
    local list_lamps = result.ListLamps
    local lottery_count = list_lamps.Count
	local glist_lottery = nil
	if(lottery_count > 4)
	then
		glist_lottery = self.GListLotterySamll
	else
	then
		glist_lottery = self.GListLotteryBig
	end
	for i = 0 lottery_count - 1 do
		local com_result = glist_lottery:AddItemFromPool().asCom
        local item_result = ItemForestPartyLotteryResult:new(nil,com_result, list_lamps[i])
	end
    
    self.TransitionShowResult:Play()
end

function ViewForestPartyResult:destory()
	self.ViewMgr.destroyView(self)
end


