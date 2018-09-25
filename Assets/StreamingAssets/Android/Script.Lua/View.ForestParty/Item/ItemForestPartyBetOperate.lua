ItemForestPartyBetOperate = {}

function ItemForestPartyBetOperate:new(o,com,bet_id)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
	self.BetSound = "ForestPartyClickBetOperate"
	self.BetId = bet_id
    self.ComBetOperate = com
    self.ComBetOperate.onClick:Add(
		function()
			self:onClickComBetOperate()
		end
	)
    local com_text = self.ComBetOperate:GetChild("ComText").asCom
    self.GTextPlayerBet = com_text:GetChild("TextPlayerBet").asTextField
    self.GTextTotalBet = com_text:GetChild("TextTotalBet").asTextField
    self.GTextBetMultiple = com_text:GetChild("TextBetMultiple").asTextField
    self.GTextPlayerBetWanOrYi = com_text:GetChild("TextPlayerBetWanOrYi").asTextField
    self.GTextTotalBetWanOrYi = com_text:GetChild("TextTotalBetWanOrYi").asTextField
	return o
end

function ItemForestPartyBetOperate:onClickComBetOperate()
	local view_mgr = ViewMgr:new(nil)
	local ev = view_mgr:getEv("EvUiForestPartyBet")
	if(ev == nil)
	then
		ev = EvUiForestPartyBet:new(nil)
	end
    ev.BetIndex = self.BetId
	view_mgr:sendEv(ev)
    CS.Casinos.CasinosContext.Instance:Play(self.BetSound, CS.Casinos._eSoundLayer.LayerNormal)
end

function ItemForestPartyBetOperate:UpdataBetMultiple(multiple)
	self.GTextBetMultiple.text = tostring(multiple)
end

function ItemForestPartyBetOperate:UpdateTotalBet(totalBet)
	if (totalBet < 10000)
	then
		self.GTextTotalBetWanOrYi.text = ""
        self.GTextTotalBet.text = tostring(totalBet)
	else
	then
		if (totalBet < 100000000)
		then
			self.GTextTotalBetWanOrYi.text = "��"
		else
		then
			self.GTextTotalBetWanOrYi.text = "��"
		end
        self.GTextTotalBet.text = CS.Casinos.CasinosContext.Instance.UiChipShowHelper:getGoldShowStr(totalBet, CS.Casinos.CasinosContext.Instance.LanMgr.LanBase)
	end
end