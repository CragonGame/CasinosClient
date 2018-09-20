ControllerRanking = ControllerBase:new(nil)

function ControllerRanking:new(o,controller_mgr,controller_data,guid)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self

	o.ControllerName = "Ranking"
	o.ViewMgr = ViewMgr:new(nil)
	o.ControllerData = controller_data
	o.ControllerMgr = controller_mgr
	o.Guid = guid
	o.ViewMgr = ViewMgr:new(nil)
	
    return o
end

function ControllerRanking:onCreate()
	self.RPC = self.ControllerMgr.RPC
	self.MC = CommonMethodType
	-- 获取金币排行榜
	self.RPC:RegRpcMethod1(self.MC.RankingChipNotify,function(list_gold)
		self:s2cRankingChipNotify(list_gold)
	end)
	-- 获取钻石排行榜
	self.RPC:RegRpcMethod1(self.MC.RankingGoldNotify,function(list_diamond)
		self:s2cRankingGoldNotify(list_diamond)
	end)
	-- 获取等级排行榜
	self.RPC:RegRpcMethod1(self.MC.RankingLevelNotify,function(list_level)
		self:s2cRankingLevelNotify(list_level)
	end)
	-- 获取礼物排行榜
	self.RPC:RegRpcMethod1(self.MC.RankingGiftNotify,function(list_gift)
		self:s2cRankingGiftNotify(list_gift)
	end)
	-- 获取豪胜排行榜
	self.RPC:RegRpcMethod1(self.MC.RankingWinGoldNotify,function(list_gift)
		self:s2cRankingWinGoldNotify(list_rank)
	end)
	self.ViewMgr:bindEvListener("EvUiGetRankingGold",self)
	self.ViewMgr:bindEvListener("EvUiGetRankingDiamond",self)
	self.ViewMgr:bindEvListener("EvUiGetRankingLevel",self)
	self.ViewMgr:bindEvListener("EvUiGetRankingGift",self)
	self.ViewMgr:bindEvListener("EvUiGetRankingWinGold",self)
	self.ViewMgr:bindEvListener("EvEntityGetPlayerInfoOther",self)
	
end

function ControllerRanking:onDestroy()
	self.ViewMgr:unbindEvListener(self)
end

function ControllerRanking:onUpdate(tm)
end

function ControllerRanking:onHandleEv(ev)
	if(ev.EventName == "EvUiGetRankingGold")
	then
		self:requestGetRanking(RankingListType.Chip)
	elseif(ev.EventName == "EvUiGetRankingDiamond")
	then
		self:requestGetRanking(RankingListType.Gold)
	elseif(ev.EventName == "EvUiGetRankingLevel")
	then
		self:requestGetRanking(RankingListType.Level)
	elseif(ev.EventName == "EvUiGetRankingGift")
	then
		self:requestGetRanking(RankingListType.Gift)
	elseif(ev.EventName == "EvUiGetRankingWinGold")
	then
		self:requestGetRanking(RankingListType.WinGold)
	elseif(ev.EventName == "EvEntityGetPlayerInfoOther")
	then
        if (self.ListRankingGold ~= nil)
		then
			local ranking_gold = nil
			for key,value in pairs(self.ListRankingGold) do
				if(value.player_guid == ev.player_info.PlayerInfoCommon.PlayerGuid)
				then
					ranking_gold = value
					break
				end
			end
            if (ranking_gold ~= nil)
			then
				ranking_gold.gold = ev.player_info.PlayerInfoMore.Gold
                ranking_gold.nick_name = ev.player_info.PlayerInfoCommon.NickName
			end
		end
        if (self.ListRankingDiamond ~= nil)
		then
			local ranking_diamond = nil
			for key,value in pairs(self.ListRankingDiamond) do
				if(value.player_guid == ev.player_info.PlayerInfoCommon.PlayerGuid)
				then
					ranking_diamond = value
					break
				end
			end
            if (ranking_diamond ~= nil)
			then
				ranking_diamond.diamond = ev.player_info.PlayerInfoMore.Diamond
                ranking_diamond.nick_name = ev.player_info.PlayerInfoCommon.NickName
			end
		end
        if (self.ListRankingLevel ~= nil)
		then
			local ranking_level = nil
			for key,value in pairs(self.ListRankingLevel) do
				if(value.player_guid == ev.player_info.PlayerInfoCommon.PlayerGuid)
				then
					ranking_level = value
				end
			end
            if (ranking_level ~= nil)
			then
				ranking_level.player_level = ev.player_info.PlayerInfoMore.Level
                ranking_level.nick_name = ev.player_info.PlayerInfoCommon.NickName
			end
		end
        if (self.ListRankingWinGold ~= nil)
		then
			local ranking_wingold = nil
			for key,value in pairs(self.ListRankingWinGold) do
				if(value.player_guid == ev.player_info.PlayerInfoCommon.PlayerGuid)
				then
					ranking_wingold = value
					break
				end
			end
            if (ranking_wingold ~= nil)
			then
				ranking_wingold.nick_name = ev.player_info.PlayerInfoCommon.NickName
			end
		end
	end
end

function ControllerRanking:getModle()
end

function ControllerRanking:s2cRankingChipNotify(list_gold)
	if(list_gold ~= nil and #list_gold > 0)
	then
		for i = 1,#list_gold do
			local temp = RankingGold:new(nil)
			temp:setData(list_gold[i])
			list_gold[i] = temp
		end
	end
	
	ViewHelper:UiEndWaiting()
    self.ListRankingGold = list_gold
    self:notifyGoldRanking()
end

function ControllerRanking:s2cRankingGoldNotify(list_diamond)
	ViewHelper:UiEndWaiting()
	if(list_diamond ~= nil and #list_diamond > 0)
	then
		for i = 1,#list_diamond do
			local temp = RankingDiamond:new(nil)
			temp:setData(list_diamond[i])
			list_diamond[i] = temp
		end
	end
    self.ListRankingDiamond = list_diamond
    self:notifyDiamondRanking()
end

function ControllerRanking:s2cRankingLevelNotify(list_level)
	ViewHelper:UiEndWaiting()
	if(list_level ~= nil and #list_level > 0)
	then
		for i = 1,#list_level do
			local temp = RankingLevel:new(nil)
			temp:setData(list_level[i])
			list_level[i] = temp
		end
	end
    self.ListRankingLevel = list_level
    self:notifyLevelRanking()
end

function ControllerRanking:s2cRankingGiftNotify(list_gift)
	ViewHelper:UiEndWaiting()
	if(list_gift ~= nil and #list_gift > 0)
	then
		for i = 1,#list_gift do
			local temp = RankingGift:new(nil)
			temp:setData(list_gift[i])
			list_gift[i] = temp
		end
	end
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityGetRankingGift")
	if(ev == nil)
	then
		ev = EvEntityGetRankingGift:new(nil)
	end
    ev.list_rankinggift = list_gift
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerRanking:s2cRankingWinGoldNotify(list_rank)
	ViewHelper:UiEndWaiting()
	local list_ranWingold = {}
	if(list_rank ~= nil and #list_rank > 0)
	then
		for i = 1,#list_rank do
			local temp = RankingWinGold:new(nil)
			temp:setData(list_rank[i])
			list_ranWingold[i] = temp
		end
	end
    self.ListRankingWinGold = list_ranWingold
	local ev = self.ControllerMgr.ViewMgr.getEv("EvEntityGetRankingWinGold")
	if(ev == nil)
	then
		ev = EvEntityGetRankingWinGold:new(nil)
	end
    ev.list_rank = list_rank
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerRanking:requestGetRanking(ranking_type)
	self.RPC:RPC1(self.MC.RankingRequest,ranking_type)
end

function ControllerRanking:createRankingUi()
	self.ControllerMgr.ViewMgr:createView("Ranking")
end

function ControllerRanking:notifyGoldRanking()
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityGetRankingGold")
	if(ev == nil)
	then
		ev = EvEntityGetRankingGold:new(nil)
	end
    ev.list_ranking = self.ListRankingGold
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerRanking:notifyDiamondRanking()
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityGetRankingDiamond")
	if(ev == nil)
	then
		ev = EvEntityGetRankingDiamond:new(nil)
	end
    ev.list_ranking = self.ListRankingDiamond
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerRanking:notifyLevelRanking()
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityGetRankingLevel")
	if(ev == nil)
	then
		ev = EvEntityGetRankingLevel:new(nil)
	end
    ev.list_ranking = self.ListRankingLevel
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

ControllerRankingFactory = ControllerFactory:new()

function ControllerRankingFactory:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self
	self.ControllerName = "Ranking"
    return o
end

function ControllerRankingFactory:createController(controller_mgr,controller_data,guid)
	local controller = ControllerRanking:new(nil,controller_mgr,controller_data,guid)
	return controller
end