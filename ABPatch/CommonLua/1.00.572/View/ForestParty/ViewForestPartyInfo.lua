ViewForestPartyInfo = ViewBase:new()

function ViewForestPartyInfo:new(o)
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

function ViewForestPartyInfo:onCreate()
	local com_bgAndClose = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bgAndClose:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
		function()
			self:onClickBtnClose()
		end
	)
    local btn_playerList = self.ComUi:GetChild("BtnPlayerList").asButton
    btn_playerList.onClick:Add(
		funtion()
			self:onClickBtnPlayerList()
		end
	)
    local btn_beBankerList = self.ComUi:GetChild("BtnBeBankerList").asButton
    btn_beBankerList.onClick:Add(
		function()
			self:onClickBtnBeBankerList()
		end
	)
    local btn_beBanker = self.ComUi:GetChild("BtnBeBanker").asButton
    btn_beBanker.onClick:Add(
		function()
			self:onClickBtnBeBanker()
		end
	)
    self.ControllerInfo = self.ComUi:GetController("ControllerInfo")
    self.GListPlayer = self.ComUi:GetChild("ListPlayer").asList
    self.GTextOnLineAmount = self.ComUi:GetChild("TextOnlineAmount").asTextField
    self.GTextBeBankerLimit = self.ComUi:GetChild("TextBeBankerLimit").asTextField
    self.GTextBeBankerLimit.text = CS.Casinos.CasinosContext.Instance.UiChipShowHelper:getGoldShowStr
        (ControllerForestParty.TbdataDeskTop.BeBankerLimit, CS.Casinos.CasinosContext.Instance.LanMgr.LanBase, true, 0)
    self:onClickBtnBeBankerList()
end

function ViewForestPartyInfo:onHandleEv(ev)
	if(ev ~= nil)
	then
		if (ev.EventName == "EvEntityForestPartyUpdateBankerPlayerList")
		then
			if (self.ControllerInfo.selectedIndex == 1)
			then
				self:setListInfo(true)
			end
		end
	end
end

function ViewForestPartyInfo:onClickBtnClose()
	self.ViewMgr.destroyView(self)
end

function ViewForestPartyInfo:onClickBtnPlayerList()
	self.ControllerInfo:SetSelectedIndex(0)
    self:setListInfo(false)
end

function ViewForestPartyInfo:onClickBtnBeBankerList()
	self.ControllerInfo:SetSelectedIndex(1)
    self:setListInfo(true)
end

function ViewForestPartyInfo:onClickBtnBeBanker()
	local ev = self.ViewMgr:getEv("EvUiForestPartyBeBanker")
	if(ev == nil)
	then
		ev = EvUiForestPartyBeBanker:new(nil)
	end
	self.ViewMgr:sendEv(ev)
end

function ViewForestPartyInfo:setListInfo(be_banker)
	self.GListPlayer:RemoveChildrenToPool()
    local ListPlayer = {}
    if (be_banker)
	then
		ListPlayer = ControllerForestParty.ListBeBanker
	else
	then
		ListPlayer = ControllerForestParty.ListOnlinePlayer
	end
	if(table.getn(ListPlayer) > 0)
	then
		for key value in pairs(ListPlayer) do
			local player_data = value
            local com_player = self.GListPlayer:AddItemFromPool().asCom
            local com_headIcon = com_player:GetChild("ComHeadIcon").asCom
            local text_nickname = com_player:GetChild("NickName").asTextField
            local text_gold = com_player:GetChild("GoldAmount").asTextField
            local player_head_icon = ViewHeadIcon:new(nil,com_headIcon, null)
            player_head_icon:setPlayerInfo(player_data.PlayerInfoCommon.IconName,
            player_data.PlayerInfoCommon.AccountId, player_data.PlayerInfoCommon.VIPLevel)
            text_nickname.text = player_data.PlayerInfoCommon.NickName
            text_gold.text = CS.Casinos.CasinosContext.Instance.UiChipShowHelper:getGoldShowStr(player_data.Gold, CS.Casinos.CasinosContext.Instance.LanMgr.LanBase)
		end
	end
end
    