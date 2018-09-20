ForestPartyCommon{}

function ForestPartyCommon:new(o,com_common)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
	self.BetCountDownSound = "ForestPartyBetCountDown"
	self.ComCommon = com_common
    self.ComBanker = self.ComCommon:GetChild("ComBanker").asCom
    self.ComCountDown = self.ComCommon:GetChild("ComCountDown").asCom
    self.ComSelfGold = self.ComCommon:GetChild("ComPlayerGold").asCom
    self.GBtnBeBanker = self.ComCommon:GetChild("BtnBeBanker").asButton
    self.GBtnBeBanker.onClick:Add(
		function()
			self:onClickBtnBeBanker()
		end
	)
    self.GBtnNotBeBanker = self.ComCommon:GetChild("BtnNotBeBanker").asButton
    self.GBtnNotBeBanker.onClick:Add(
		function()
			self:onClickBtnNotBeBanker()
		end
	)
    self.GBtnBetRepeat = self.ComCommon:GetChild("BtnBetRepet").asButton
    self.GBtnBetRepeat.onClick:Add(
		function()
			self:onClickBtnBetRepeat()
		end
	)
    self.GTextSelfGold = self.ComSelfGold:GetChild("TextSelfGold").asTextField
    self.GTextSelfClearGold = self.ComSelfGold:GetChild("TextSelfClearGold").asTextField
    self.GTextSelfGoldWanOrYi = self.ComSelfGold:GetChild("TextSelfGoldWanOrYi").asTextField
    self.GTextSelfClearGoldWanOrYi = self.ComSelfGold:GetChild("TextSelfClearGoldWanOrYi").asTextField
    self.GTextCountDown = self.ComCountDown:GetChild("TextCount").asTextField
    self.GListRewardHistory = self.ComCommon:GetChild("ListRewardHistory").asList
    self.TransitionClearGold = self.ComSelfGold:GetTransition("TransitionClearGold")
    local btn_chat = self.ComCommon:("BtnChat").asButton
    btn_chat.onClick:Add(
		function()
			self:onClickBtnChat()
		end
	)
    local btn_info = self.ComCommon:("BtnInfo").asButton
    btn_info.onClick:Add(
		function()
			self:onClickBtnInfo()
		end
	)
    local btn_shop = self.ComCommon:("BtnShop").asButton
    btn_shop.onClick:Add(
		function()
			self:onClickBtnShop()
		end
	)
    local btn_betChange = self.ComCommon:GetChild("BtnBetChange").asButton
    self.GTextBetValue = btn_betChange:GetChild("TextBetValue").asTextField
    btn_betChange.onClick:Add(
		function()
			self:onClickBtnBetOperateChange()
		end
	)
    local btn_betRepeatAutoOrManual = self.ComCommon:GetChild("BtnBetRepeatAutoOrManual").asButton
    self.ControllerBetAutoOrManual = btn_betRepeatAutoOrManual:GetController("ControllerBetAutoOrManual")
    btn_betRepeatAutoOrManual.onClick:Add(
		function()
			self:onClickBtnBetRepeatAutoOrManual()
		end
	)
    self.ForestPartyBanker = ForestPartyBanker:new(nil,self.ComBanker)
    self.CountDownTime = -1
    self.HasPlayCountDown1 = false
    self.HasPlayCountDown2 = false
    self.HasPlayCountDown3 = false
    self:init()
    return o
end

function ForestPartyCommon:init()
	local lamp_record = ControllerForestParty.ListLotteryRecord
	for key value in pairs(lamp_record) do
		self:addLotteryRecord(value,false)
	end
    self.GBtnNotBeBanker.visible = ControllerForestParty.IsBankPlayer
    self.GBtnBetRepeat.enabled = table.getn(ControllerForestParty.MapSelfBetPot) == 0
    local self_gold = ControllerActor.Def.PropGoldAcc:get()
    self:setSelfGold(self_gold)
    self:setBetOperate()
    self:setBetRepeatIsAutoOrManual()
    self:SetBankerInfo()
end

function ForestPartyCommon:HandleEvent(ev)
	if (ev.EventName == "EvEntityForestPartyBetState")
	then
		self.HasPlayCountDown1 = false
        self.HasPlayCountDown2 = false
        self.HasPlayCountDown3 = false
        self.GBtnBetRepeat.enabled = true
        self.TweenGListRewardHistory:Kill()
        local lamp_record = {}
        lamp_record = ControllerForestParty.ListLotteryRecord
        self.GListRewardHistory:RemoveChildrenToPool()
		for key value in pairs(lamp_record) do
			self:addLotteryRecord(value,false)
		end
        
        self:SetBankerInfo()
	else if(ev.EventName == "EvEntityForestPartyGameEnd")
	then
		self.GBtnBetRepeat.enabled = false
	else if(ev.EventName == "EvEntityForestPartyUpdateAutoBetState")
	then
		self:setBetRepeatIsAutoOrManual()
	else if(ev.EventName == "EvEntityForestPartyUpdateBetOperate")
	then
		self:setBetOperate()
	else if(ev.EventName == "EvEntityForestPartyForbiddenBtnBetRepeat")
	then
		self.GBtnBetRepeat.enabled = false
	else if(ev.EventName == "EvEntityForestPartyBuyItem")
	then
        self:playerBuyItem(ev.map_items)
	else if(ev.EventName == "EvEntityGoldChanged")
	then
        self.setSelfGold(ev.gold_acc)
	end
end

function ForestPartyCommon:Update(elapsed_tm)
	if (self.CountDownTime == -1)
	then
		return
	else
	then
		self.CountDownTime = self.CountDownTime - elapsed_tm 
        local text_time = string.format(%u,CS.System.Math.Ceiling(self.CountDownTime))
        if (text_time == 3 and self.HasPlayCountDown3 == false)
		then
			CS.Casinos.CasinosContext.Instance:play(self.BetCountDownSound, CS.Casinos._eSoundLayer.LayerNormal)
            self.HasPlayCountDown3 = true
		end
        if (text_time == 2 and self.HasPlayCountDown2 == false)
		then
			CS.Casinos.CasinosContext.Instance:play(self.BetCountDownSound, CS.Casinos._eSoundLayer.LayerNormal)
            self.HasPlayCountDown2 = true
		end
        if (text_time == 1 and self.HasPlayCountDown1 == false)
		then
			CS.Casinos.CasinosContext.Instance:play(self.BetCountDownSound, CS.Casinos._eSoundLayer.LayerNormal)
            self.HasPlayCountDown1 = true
		end
        self.GTextCountDown.text = tostring(text_time)
	end
end

function ForestPartyCommon:SetBankerInfo()
	self.ForestPartyBanker:SetBankerInfo()
    self.GBtnNotBeBanker.visible = ControllerForestParty.IsBankPlayer
end

function ForestPartyCommon:PlayClearGoldTransition()
	local self_winlose_gold = ControllerForestParty.SelfWinLoseGold
    if (self_winlose_gold < 10000)
	then
		self.GTextSelfClearGoldWanOrYi.text = ""
        self.GTextSelfClearGold.text = tostring(self_winlose_gold)
	else
	then
		if (self_winlose_gold < 100000000)
		then
			self.GTextSelfClearGoldWanOrYi.text = "��"
		else
		then
			self.GTextSelfClearGoldWanOrYi.text = "��"
		end
        self.GTextSelfClearGold.text = CS.Casinos.CasinosContext.Instance.UiChipShowHelper:getGoldShowStr(self_winlose_gold, CS.Casinos.CasinosContext.Instance.LanMgr.LanBase)
	end
    self.TransitionClearGold:Play(3, 0, nil)
end

function ForestPartyCommon:StopClearGoldTransition()
	self.TransitionClearGold:Stop(true, false)
    self.GTextSelfClearGold.text = ""
    self.GTextSelfClearGoldWanOrYi.text = ""
end

function ForestPartyCommon:StartCoutDown()
	local time = ControllerForestParty.BetLeftTime
    self.CountDownTime = time
end

function ForestPartyCommon:StopCoutDown()
	self.CountDownTime = -1
    self.GTextCountDown.text = ""
end

function ForestPartyCommon:addLotteryRecord(record,use_transition)
	local com_record = self.GListRewardHistory:GetFromPool(CS.FairyGUI.UIPackage.GetItemURL("ForestParty", "ComRewardHistory")).asCom
    self.GListRewardHistory:AddChildAt(com_record, 0)
    local loader_icon = com_record:GetChild("LoaderIcon").asLoader
    local str_res_name
    if (record.LotteryType == CS.Casinos.LotteryType.Normal)
	then
		str_res_name = record.AnimalType.ToString() .. record.BlockColor.ToString()
	else
	then
		str_res_name = record.LotteryType.ToString()
	end
    loader_icon.url = CS.FairyGUI.UIPackage.GetItemURL("ForestParty", "HistoryRecord" .. str_res_name)
    if (use_transition)
	then
		local transition_rotate = com_record:GetTransition("TransitionRotate");
        transition_rotate:Play(
			function()
				if (self.GListRewardHistory.numChildren >= 11)
				then
					self.TweenGListRewardHistory = self.GListRewardHistory:TweenMoveX(self.GListRewardHistory.x + com_record.width, 0.3).OnComplete(
						function()
							self.GListRewardHistory:RemoveChildToPoolAt(10)
							self.GListRewardHistory.x = self.GListRewardHistory.x - com_record.width
						end
					)
				end
			end
		)
	end
end

function ForestPartyCommon:setSelfGold(self_gold)
	if (self_gold < 10000)
	then
		self.GTextSelfGoldWanOrYi.text = ""
	else if(self_gold < 100000000)
	then
		self.GTextSelfGoldWanOrYi.text = "��"
	else
	then
		self.GTextSelfGoldWanOrYi.text = "��"
	end
    local str_self_gold = CS.Casinos.CasinosContext.Instance.UiChipShowHelper:getGoldShowStr(self_gold, CS.Casinos.CasinosContext.Instance.LanMgr.LanBase)
    self.GTextSelfGold.text = str_self_gold
end

function ForestPartyCommon:setBetOperate()
	local bet_index = ControllerForestParty.BetOprateIndex
    local map_betoperate = ControllerForestParty.MapBetOperate
    local bet_value = map_betoperate[bet_index]
    self.GTextBetValue.text = tostring(bet_value)
    self.GTextBetValue.text = CS.Casinos.CasinosContext.Instance.UiChipShowHelper:getGoldShowStr(bet_value, CS.Casinos.CasinosContext.Instance.LanMgr.LanBase, true, 0)
end

function ForestPartyCommon:setBetRepeatIsAutoOrManual()
	local is_auto_bet_repeate = ControllerForestParty.IsAutoBetRepeate
	if(is_auto_bet_repeate)
	then
		 self.ControllerBetAutoOrManual.SetSelectedIndex = 0
	else
	then
		 self.ControllerBetAutoOrManual.SetSelectedIndex = 1
	end
end

function ForestPartyCommon:onClickBtnBeBanker()
	local view_mgr = ViewMgr:new(nil)
	local ev = view_mgr:getEv("EvUiForestPartyBeBanker")
	if(ev == nil)
	then
		ev = EvUiForestPartyBeBanker:new(nil)
	end
	view_mgr:senEv(ev)
end

function ForestPartyCommon:onClickBtnNotBeBanker()
	local view_mgr = ViewMgr:new(nil)
	local ev = view_mgr:getEv("EvUiForestPartyNotBeBanker")
	if(ev == nil)
	then
		ev = EvUiForestPartyNotBeBanker:new(nil)
	end
	view_mgr:senEv(ev)
end

function ForestPartyCommon:onClickBtnBetOperateChange()
	local view_mgr = ViewMgr:new(nil)
	local ev = view_mgr:getEv("EvUiForestPartyChangeBetOperate")
	if(ev == nil)
	then
		ev = EvUiForestPartyChangeBetOperate:new(nil)
	end
	view_mgr:senEv(ev)
end

function ForestPartyCommon:onClickBtnBetRepeatAutoOrManual()
	local view_mgr = ViewMgr:new(nil)
	local ev = view_mgr:getEv("EvUiForestPartyChangeBetRepeatState")
	if(ev == nil)
	then
		ev = EvUiForestPartyChangeBetRepeatState:new(nil)
	end
	view_mgr:senEv(ev)
end

function ForestPartyCommon:onClickBtnChat()
	local view_mgr = ViewMgr:new(nil)
	local ui_chat = view_mgr.createView("Chat")
    ui_chat:init(_eUiChatType.DesktopH)
end

function ForestPartyCommon:onClickBtnInfo()
	local view_mgr = ViewMgr:new(nil)
	view_mgr.createView("ForestPartyInfo")
end

function ForestPartyCommon:onClickBtnShop()
	local view_mgr = ViewMgr:new(nil)
	view_mgr.createView("Shop")
end

function ForestPartyCommon:onClickBtnBetRepeat()
	local view_mgr = ViewMgr:new(nil)
	local ev = view_mgr:getEv("EvUiForestPartyBetRepeat")
	if(ev == nil)
	then
		ev = EvUiForestPartyBetRepeat:new(nil)
	end
	view_mgr:senEv(ev)
end

function ForestPartyCommon:playerBuyItem(map_items)
	if (map_items == nil)
	then
		return
	end
    local eb_data_mgr = CasinosContext.Instance.EbDataMgr
    foreach (var i in map_items)
    {
        Item item = new Item(eb_data_mgr, null, i.Value);
        if (item.UnitLink.UnitType == UnitType.MagicExpression)
        {
            if (i.Key.Equals(CoPlayer.CoPlayerForestParty.BankerData.PlayerInfoCommon.PlayerGuid))
            {
                sendBankerMagicExp(item.TbDataItem.Id);
            }
        }
    }
end

function ForestPartyCommon:sendBankerMagicExp(exp_tbid)
	TbDataUnitMagicExpression tb_magicexp = CasinosContext.Instance.EbDataMgr.GetData<TbDataUnitMagicExpression>(exp_tbid)
    if (tb_magicexp == null)
    {
        return;
    }
    Vector3 from_pos = Vector3.zero + new Vector3(Screen.width, Screen.height)
    Vector3 to_pos = ComBanker.position + new Vector3(ComBanker.width / 2, ComBanker.height / 2)
    var ui_pool = CasinosContext.Instance.UiMgr.getUi<UiPool>()
    var item_magicsender = ui_pool.getMagicExpSender()
    item_magicsender.sendMagicExp(from_pos, to_pos, exp_tbid)
end



