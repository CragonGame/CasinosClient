ViewForestPartyBet = ViewBase:new()

function ViewForestPartyBet:new(o)
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

function ViewForestPartyBet:onCreate()
    self.ComBetPanel = self.ComUi:GetChild("ComBetPanel").asCom
    self.ComBetPanel:TweenMoveX(self.ComBetPanel.x - CS.UnityEngine.Screen.width, 0.5)
    local com_common = self.ComBetPanel:GetChild("ComCommon").asCom
    self.ForestPartyCommon = ForestPartyCommon:new(nil,com_common)
    local com_shade = self.ComBetPanel:GetChild("ComShade").asCom
    com_shade.onClick:Add(
		function()
			self:onClickComShade()
		end
	)
    self.LerpTweenMoveX = self.ComUi:GlobalToLocal(CS.UnityEngine.Vector2(CS.UnityEngine.Screen.width, 0)).x
    self.ArrayBetOperates = {}
	for i = 0 11 do
		local com_bet = self.ComBetPanel:GetChild("ComBetOperate" .. tostring(i)).asCom
        local betOperate = ItemForestPartyBetOperate:new(nil,com_bet, i)
        self.ArrayBetOperates[i] = betOperate
	end
    self:show()
    self:fillBetMultiple()
    self:updateTotalBet()
    self:updatePlayerBet()
end

function ViewForestPartyBet:onHandleEv(ev)
	if(ev ~= nil)
	then
		self.ForestPartyCommon:HandleEvent(ev)
		if(ev.EventName == "EvEntityForestPartyUpdateTotalBetPotInfo")
		then
			self:updateTotalBet()
		else if(ev.EventName == "EvEntityForestPartyUpdateSelfBetPot")
		then
			if (ev.IsAll)
			then
				self:updatePlayerBet()
			else
			then
				local index = ev.BetIndex
				local bet_value = ev.BetValue
				self.ArrayBetOperates[index]:UpdatePlayerBet(bet_value)
			end
		else if(ev.EventName == "EvEntityForestPartyGameEnd")
		then
			self:destory()
		end
	end
	
end

function ViewForestPartyBet:ResetBetLeftTime()
	self.ForestPartyCommon:StartCoutDown()
end

function ViewForestPartyBet:onUpdate(tm)
	self.ForestPartyCommon:Update(tm)
end

function ViewForestPartyBet:show()
	self.TweenBetPanel = self.ComBetPanel:TweenMoveX(self.ComBetPanel.position.x - self.LerpTweenMoveX, 0.5)
    self.TweenBetPanel:SetAutoKill(false)
    local bet_left_time = ControllerForestParty.BetLeftTime
    self.ForestPartyCommon:StartCoutDown()
end

function ViewForestPartyBet:onClickComShade()
	if (self.TweenBetPanel:IsPlaying())
	then
		return
	else
	then
		self:destory()
	end
end

function ViewForestPartyBet:destory()
	self.ComBetPanel:TweenMoveX(self.ComBetPanel.position.x + self.LerpTweenMoveX, 0.5).OnComplete(
		function()
			self.ViewMgr.destroyView(self)
		end
	)
end

function ViewForestPartyBet:fillBetMultiple()
	local map_betmultiple = ControllerForestParty.MapBetMultiple
	for key value in pairs(map_betmultiple) do
		local index = key
        local multiple = value
        self.ArrayBetOperates[index]:UpdataBetMultiple(multiple)
	end
end

function ViewForestPartyBet:updateTotalBet()
	local map_total_bet = ControllerForestParty.MapTotalBetpot
	for i = 0 11 do
		local bet_value = 0
		if(map_total_bet[i] ~= nil)
		then
			bet_value = map_total_bet[i]
		end
		self.ArrayBetOperates[i]:UpdateTotalBet(bet_value)
	end
end

function ViewForestPartyBet:updatePlayerBet()
	local map_player_bet = ControllerForestParty.MapSelfBetPot
	for i = 0 11 do
		local bet_value = 0
		if(map_player_bet[i] ~= nil)
		then
			bet_value = map_player_bet[i]
		end
        self.ArrayBetOperates[i]:UpdatePlayerBet(bet_value)
	end
end