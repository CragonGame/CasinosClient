ViewLobby = ViewBase:new()

function ViewLobby:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	self.FilterKey = "DesktopFilterTP"
	o.ViewMgr = nil
	o.GoUi = nil
	o.ComUi = nil
	o.Panel = nil
	o.UILayer = nil
	o.InitDepth = nil
	o.ViewKey = nil

	return o
end

function ViewLobby:onCreate()
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
	self.ControllerFriendOrBet = self.ComUi:GetController("ControllerFriendOrBet")
	self.ControllerFriendOrBet:SetSelectedIndex(1)
	self.GTextBetHints = self.ComUi:GetChild("BetHint").asTextField
	self.GTextNoFriendTips = self.ComUi:GetChild("NoFriendTips").asTextField
	self.GListDesk = self.ComUi:GetChild("ListDesk").asList
	self.GListDesk.scrollPane.onScrollEnd:Add(
			function()
				self:onListDeskScorllEnd()
			end
	)
	local com_operateList = self.ComUi:GetChild("ComBetRangeOperateList").asCom
	self.GListBetRangeOperate = com_operateList:GetChild("ListBetRangeOperate").asList
	self.GListBetRangeOperate.scrollPane.onScrollEnd:Add(
			function()
				self:onBetChipOperateListScrollEnd()
			end
	)
	self.GListBetRangeOperate.scrollPane.scrollStep = 200
	self.GListFriendHeadIcon = self.ComUi:GetChild("ListFriend").asList
	self.GListFriendHeadIcon:SetVirtual()
	self.GListFriendHeadIcon.itemRenderer = self.rendererPlayingFriend
	local btn_seat = self.ComUi:GetChild("BtnSeat").asCom
	btn_seat.onClick:Add(
			function()
				self:onClickBtnSeat()
			end
	)
	self.GTextSeatType = btn_seat:GetChild("TextSeatType").asTextField
	local btn_fullDesk = self.ComUi:GetChild("BtnFullDesk").asCom
	btn_fullDesk.onClick:Add(
			function()
				self:onClickBtnFullDesk()
			end
	)
	self.GTextHideOrShow = btn_fullDesk:GetChild("TextHideOrShow").asTextField
	local btn_return = self.ComUi:GetChild("BtnReturn").asCom
	btn_return.onClick:Add(
			function()
				self:onClickBtnReturn()
			end
	)
	local btn_personInfo = self.ComUi:GetChild("BtnPersonInfo").asCom
	btn_personInfo.onClick:Add(
			function()
				self:onClickBtnPersonInfo()
			end
	)
	local btn_friend = self.ComUi:GetChild("BtnFriend").asButton
	btn_friend.onClick:Add(
			function()
				self:onClickBtnFriend()
			end
	)
	local btn_bet = self.ComUi:GetChild("BtnBet").asButton
	btn_bet.onClick:Add(
			function()
				self:onClickBtnBet()
			end
	)
	local btn_betAdd = self.ComUi:GetChild("BtnBetAdd").asButton
	btn_betAdd.onClick:Add(
			function()
				self:onClickBtnBetAdd()
			end
	)
	local btn_betReduce = self.ComUi:GetChild("BtnBetReduce").asButton
	btn_betReduce.onClick:Add(
			function()
				self:onClickBtnBetReduce()
			end
	)
	self.ListPlayingFriend = {}
	self.MapDesktopFilter = {}
	self.MapDeskItem = {}
	self.MapBetChipOperateItem = {}

	self:initPlayerInfo()
	--local bg = self.ComUi:GetChild("Bg")
	--if (bg ~= nil)
	--then
	--	ViewHelper:makeUiBgFiteScreen(bg, self.ComUi.width, self.ComUi.height, bg.width, bg.height)
	--end
	self.ChipIconSolustion = self.ComUi:GetController("ChipIconSolustion")
	self.ChipIconSolustion.selectedIndex = ChipIconSolustion
	self.ViewMgr:bindEvListener("EvEntityGetLobbyDeskList",self)
	self.ViewMgr:bindEvListener("EvEntitySearchDesktopFollowFriend",self)
	self.ViewMgr:bindEvListener("EvEntityFriendOnlineStateChange",self)
	self.ViewMgr:bindEvListener("EvEntityNotifyDeleteFriend",self)
	self.ViewMgr:bindEvListener("EvEntityRefreshFriendList",self)
	self.ViewMgr:bindEvListener("EvEntityRefreshFriendInfo",self)
	self.ViewMgr:bindEvListener("EvEntitySearchPlayingFriend",self)
end

function ViewLobby:onDestroy()
	self.ViewMgr:unbindEvListener(self)
end

function ViewLobby:onHandleEv(ev)
	if(ev ~= nil)
	then
		if(ev.EventName == "EvEntityGetLobbyDeskList")
		then
			if (self.ControllerFriendOrBet.selectedIndex == 1)
			then
				self:setDeskTopInfo(ev.list_desktop)
			end
		elseif(ev.EventName == "EvEntitySearchDesktopFollowFriend")
		then
			if (self.ControllerFriendOrBet.selectedIndex == 0)
			then
				local temp = {}
				table.insert(temp,ev.desktop_info)
				self:setDeskTopInfo(temp)
			end
		elseif(ev.EventName == "EvEntityFriendOnlineStateChange")
		then
			if (self.ControllerFriendOrBet.selectedIndex == 0)
			then
				local player_info = ev.player_info
				if (player_info.PlayerInfoMore.OnlineState == PlayerOnlineState.Offline)
				then
					self:removePlayingFriend(player_info.PlayerInfoCommon.PlayerGuid)
				end
			end
		elseif(ev.EventName == "EvEntityNotifyDeleteFriend")
		then
			if (self.ControllerFriendOrBet.selectedIndex == 0)
			then
				local friend_etguid = ev.friend_etguid
				self:removePlayingFriend(friend_etguid)
			end
		elseif(ev.EventName == "EvEntityRefreshFriendList")
		then
			if (self.ControllerFriendOrBet.selectedIndex == 0)
			then
				self:refreshPlayingFriend()
			end
		elseif(ev.EventName == "EvEntityRefreshFriendInfo")
		then
			if (self.ControllerFriendOrBet.selectedIndex == 0)
			then
				self:refreshPlayingFriend(ev.player_info)
			end
		elseif(ev.EventName == "EvEntitySearchPlayingFriend")
		then
			self:setCurrentPlayingFriend(ev.list_playerinfo)
		end
	end
end

function ViewLobby:setLobbyModel()--lobby_model
	self.LobbyFilterKey = self.FilterKey --.. self.DesktopTypeTexas
	self:initDesktopSearchFilter()
end

function ViewLobby:initPlayerInfo()
	local com_headIcon = self.ComUi:GetChild("ComHeadIcon").asCom
	local viewHeadIcon = ViewHeadIcon:new(nil,com_headIcon)
	viewHeadIcon:setMainPlayerInfo(self.ControllerActor)
	local text_name = self.ComUi:GetChild("PlayerName").asTextField
	text_name.text =  CS.Casinos.UiHelper.addEllipsisToStr(self.ControllerActor.PropNickName:get(),12,5)
	local text_playerGoldAmount = self.ComUi:GetChild("PlayerGoldAmount").asTextField
	text_playerGoldAmount.text = tostring(self.ControllerActor.PropDiamond:get())
	local text_playerChipAmount = self.ComUi:GetChild("PlayerChipAmount").asTextField
	text_playerChipAmount.text = UiChipShowHelper:getGoldShowStr(self.ControllerActor.PropGoldAcc:get(), self.ViewMgr.LanMgr.LanBase)
end

function ViewLobby:chooseCurrentPlayingFriend(player_info)
	self.CurrentPlayingFriendInfo = player_info
	local ev = self.ViewMgr:getEv("EvUiRequestGetCurrentFriendPlayDesk")
	if(ev == nil)
	then
		ev = EvUiRequestGetCurrentFriendPlayDesk:new(nil)
	end
	ev.player_guid = self.CurrentPlayingFriendInfo.PlayerInfoCommon.PlayerGuid
	self.ViewMgr:sendEv(ev);
end

function ViewLobby:requestLobbyDesk()
	local dektop_filter = DesktopFilter:new(nil)
	dektop_filter.FactoryName = "Texas"
	dektop_filter.IncludeFull = self.DeskSearchFilter.is_seat_full
	local p_d = self.DeskSearchFilter:getData4Pack()
	local p_filter = self.ViewMgr:packData(p_d)
	dektop_filter.FilterData = p_filter --CS.Casinos.LuaHelper.ProtobufSerializeDesktopFilterTexas(self.DeskSearchFilter)
	local ev = self.ViewMgr:getEv("EvUiClickSearchDesk")
	if(ev == nil)
	then
		ev = EvUiClickSearchDesk:new(nil)
	end
	ev.desktop_searchfilter = dektop_filter
	self.ViewMgr:sendEv(ev)
end

function ViewLobby:setDeskTopInfo(list_desktop)
	ViewHelper:UiEndWaiting()
	self.GListDesk.visible = true
	self.GListDesk.scrollPane.posX = 0
	self.GListDesk:RemoveChildrenToPool()
	for key,value in pairs(self.MapDeskItem) do
		value:recycleSeats()
	end
	self.MapDeskItem = {}
	if (#list_desktop == 0)
	then
		return
	end
	for i = 1,#list_desktop do
		local item = self.GListDesk:AddItemFromPool()
		local desktop_info1 = self.ViewMgr:unpackData(list_desktop[i].DesktopData)--CS.Casinos.LuaHelper.ProtobufDeserializeDesktopInfoTexas(self.CasinosContext.MemoryStream,list_desktop[i].DesktopData)
		local desktop_info = DesktopInfoTexas:new(nil)
		desktop_info.desktop_etguid = desktop_info1[1]
		desktop_info.seat_num  = desktop_info1[2]
		desktop_info.game_speed  = desktop_info1[3]
		desktop_info.is_vip = desktop_info1[4]
		desktop_info.desktop_tableid = desktop_info1[5]
		desktop_info.list_seat_player = desktop_info1[6]
		desktop_info.seat_player_num = desktop_info1[7]
		desktop_info.all_player_num = desktop_info1[8]
		local deskItem = ItemLobbyDesk:new(nil,item, desktop_info,
				function()
					self:onClickCreateDesktopBtn()
				end
		,self.ViewMgr)
		deskItem:init()
		deskItem:NotBeCenter(false)
		self.MapDeskItem[i] = deskItem
	end
	self.CurrentDesk = self.MapDeskItem[1]
	self.CurrentDesk:SwitchBig(true)
	self:keepCreateTableBtnFollowTable()
end

function ViewLobby:setCurrentPlayingFriend(list_playerinfo)
	self.ListPlayingFriend = {}
	for key,value in pairs(list_playerinfo) do
		table.insert(self.ListPlayingFriend,value)
	end
	if (#self.ListPlayingFriend)
	then
		self.GTextNoFriendTips.text = self.ViewMgr.LanMgr:getLanValue("NoFriendPlayCard")
		self.GListDesk:RemoveChildren()
	else
		self.GTextNoFriendTips.text = ""
		self.GListFriendHeadIcon.numItems = #self.ListPlayingFriend
	end
end

function ViewLobby:removePlayingFriend(player_guid)
	local playing_friend = nil
	local playing_friend_key = nil
	for key,value in pairs(self.ListPlayingFriend) do
		if(value.PlayerInfoCommon.PlayerGuid == player_guid)
		then
			playing_friend = value
			playing_friend_key = key
			break
		end
	end
	if (playing_friend ~= nil)
	then
		if (self.CurrentPlayingFriendInfo ~= nil and
				self.CurrentPlayingFriendInfo.PlayerInfoCommon.PlayerGuid == player_guid)
		then
			self.CurrentPlayingFriendInfo = nil
		end
		self.ListPlayingFriend[playing_friend_key] = nil
		self.GListFriendHeadIcon.numItems = #self.ListPlayingFriend
	end
	if (#self.ListPlayingFriend <= 0)
	then
		self.GListDesk:RemoveChildren()
		self.GTextNoFriendTips.text = self.ViewMgr.LanMgr:getLanValue("NoFriendPlayCard")
	end
end

function ViewLobby:refreshPlayingFriend(friend_info)
	local player_playstate = friend_info.PlayerPlayState
	local friend_desktopguid = nil
	if(player_playstate == nil)
	then
		friend_desktopguid = ""
	else
		friend_desktopguid = player_playstate.DesktopGuid
	end
	if(friend_desktopguid == nil or friend_desktopguid == "")
	then
		self:removePlayingFriend(friend_info.PlayerInfoCommon.PlayerGuid)
	else
		self:refreshPlayingFriend()
	end
end

function ViewLobby:onClickBtnSeat()
	local seat_num = self.DeskSearchFilter.seat_num
	if(seat_num == TexasDesktopSeatNum.Five)
	then
		seat_num = TexasDesktopSeatNum.Nine
	elseif(seat_num == TexasDesktopSeatNum.Nine)
	then
		seat_num = TexasDesktopSeatNum.Five
	end
	self.DeskSearchFilter.seat_num = seat_num
	self:onSearchFilterChanged()
end

function ViewLobby:onClickBtnFullDesk()
	local isFull = self.DeskSearchFilter.is_seat_full
	if(isFull)
	then
		self.DeskSearchFilter.is_seat_full = false
	else
		self.DeskSearchFilter.is_seat_full = true
	end
	self:onSearchFilterChanged()
end

function ViewLobby:onClickBtnPersonInfo()
	if (self.ControllerFriendOrBet.selectedIndex == 0)
	then
		return
	end
	local chip_acc = self.ControllerActor.PropGoldAcc:get()
	local scroll_index = -1
	local tbdata_desktopinfo = TexasHelper:getTbDataDesktopInfoSuitable(self.ViewMgr.TbDataMgr, chip_acc)
	local betChipOperateItem = self.MapBetChipOperateItem[tbdata_desktopinfo.Id]
	scroll_index = betChipOperateItem.CurrentIndex
	if (scroll_index ~= -1)
	then
		local a = self:GetCurrentItemIndex(self.GListBetRangeOperate)
		if (a == scroll_index)
		then
			local tips = self.ViewMgr.LanMgr:getLanValue("HasSelectedRightTable")
			ViewHelper:UiShowInfoSuccess(tips)
		end
		self.GListBetRangeOperate:ScrollToView(scroll_index, true)
	end
end

function ViewLobby:onClickBtnFriend()
	self.ControllerFriendOrBet:SetSelectedIndex(0)
	self:refreshPlayingFriend()
end

function ViewLobby:onClickBtnBet()
	self.ControllerFriendOrBet:SetSelectedIndex(1)
	self:requestLobbyDesk()
end

function ViewLobby:onClickBtnBetAdd()
	self.GListBetRangeOperate.scrollPane:ScrollRight(1, true)
end

function ViewLobby:onClickBtnBetReduce()
	self.GListBetRangeOperate.scrollPane:ScrollLeft(1, true)
end

function ViewLobby:onClickBtnReturn()
	local ev = self.ViewMgr:getEv("EvUiCreateMainUi")
	if(ev == nil)
	then
		ev = EvUiCreateMainUi:new(nil)
	end
	self.ViewMgr:sendEv(ev)
end

function ViewLobby:onClickCreateDesktopBtn()
	local mb_createdesk = self.ViewMgr:createView("CreateDeskTop")
	self.MapDesktopFilter = nil
	self.MapDesktopFilter = {}
	self.MapDesktopFilter[0] = self.DeskSearchFilter.seat_num
	self.MapDesktopFilter[1] = self.DeskSearchFilter.game_speed
	mb_createdesk:setCreateInfo(self.DeskSearchFilter.desktop_tableid, self.MapDesktopFilter)
end

function ViewLobby:initDesktopSearchFilter()
	local map_deskinfo = self.ViewMgr.TbDataMgr:GetMapData("DesktopInfoTexas")
	local select_item = nil
	if (CS.UnityEngine.PlayerPrefs.HasKey(self.LobbyFilterKey))
	then
		--local filter = CS.UnityEngine.PlayerPrefs.GetString(self.LobbyFilterKey)
		--self.DeskSearchFilter = self.ViewMgr:unpackData(filter)
		self.DeskSearchFilter = DesktopFilterTexas:new(nil)
		self.DeskSearchFilter.is_vip = false
		self.DeskSearchFilter.is_seat_full = false
		self.DeskSearchFilter.seat_num = TexasDesktopSeatNum.Nine
		self.DeskSearchFilter.desktop_tableid = map_deskinfo[1].Id
	else
		self.DeskSearchFilter = DesktopFilterTexas:new(nil)
		self.DeskSearchFilter.is_vip = false
		self.DeskSearchFilter.is_seat_full = false
		self.DeskSearchFilter.seat_num = TexasDesktopSeatNum.Nine
		self.DeskSearchFilter.desktop_tableid = map_deskinfo[1].Id
	end
	local index = 0
	for key,value in pairs(map_deskinfo) do
		local item = self.GListBetRangeOperate:AddItemFromPool()
		local betChipOperateItem = ItemBetChipRange:new(nil,item,value,index)
		self.MapBetChipOperateItem[key] = betChipOperateItem
		index = index + 1
	end
	if(self.MapBetChipOperateItem[self.DeskSearchFilter.desktop_tableid] == nil)
	then
		self.DeskSearchFilter.desktop_tableid = map_deskinfo[1].Id
	else
		select_item = self.MapBetChipOperateItem[self.DeskSearchFilter.desktop_tableid]
	end
	if (select_item ~= nil)
	then
		self.GListBetRangeOperate:ScrollToView(select_item.CurrentIndex)
	end
	self:onSearchFilterChanged()
	self:onBetChipOperateListScrollEnd()
end

function ViewLobby:onBetChipOperateListScrollEnd()
	local currentItemIndex = self:GetCurrentItemIndex(self.GListBetRangeOperate)
	local current_item = self:getCurrentChipItem(currentItemIndex)
	local betChipOperateItem = self.MapBetChipOperateItem[current_item.EbData.Id]
	local tb_desktop = betChipOperateItem:getEbData()
	local tips = ""
	if (tb_desktop ~= nil)
	then
		local tb_d = tb_desktop
		tips = "Buy-in:" .. UiChipShowHelper:getGoldShowStr(tb_d.BetMin,self.ViewMgr.LanMgr.LanBase) .. "/"
				.. UiChipShowHelper:getGoldShowStr(tb_d.BetMax,self.ViewMgr.LanMgr.LanBase)
	end
	self.DeskSearchFilter.desktop_tableid = tb_desktop.Id
	local gold_acc = self.ControllerActor.PropGoldAcc:get()
	local can_play = TexasHelper:enoughChip4DesktopBetMin(self.CasinosContext.TbDataMgrLua, gold_acc,
			self.DeskSearchFilter.desktop_tableid)
	if (can_play == false)
	then
		tips = self.ViewMgr.LanMgr:getLanValue("NoEnoughChipCanotPlay")
	end
	self.GTextBetHints.text = tips
	self:onSearchFilterChanged()
end

function ViewLobby:onListDeskScorllEnd()
	if (LuaHelper:GetTableCount(self.MapDeskItem)> 0)
	then
		local currentItemDeskIndex = self:GetCurrentItemIndex(self.GListDesk)
		self.CurrentDesk = self.MapDeskItem[currentItemDeskIndex + 1]
		self.CurrentDesk:SwitchBig(true)
		for key,value in pairs(self.MapDeskItem) do
			if(value ~= self.CurrentDesk)
			then
				value:NotBeCenter(true)
			end
		end
		self:keepCreateTableBtnFollowTable()
	end
end

function ViewLobby:onSearchFilterChanged()
	local filter =  self.ViewMgr:packData(self.DeskSearchFilter)-- CS.EbTool.jsonSerialize(self.DeskSearchFilter)
	CS.UnityEngine.PlayerPrefs.SetString(self.LobbyFilterKey, filter)
	self.GListDesk.visible = false
	local player_num = self.DeskSearchFilter.seat_num
	local filter_tips = ""
	if(player_num == TexasDesktopSeatNum.Unlimited)
	then
		filter_tips = self.ViewMgr.LanMgr:getLanValue("AnySeat")
	elseif(player_num == TexasDesktopSeatNum.Five)
	then
		filter_tips = self.ViewMgr.LanMgr:getLanValue("Five")
	elseif(player_num == TexasDesktopSeatNum.Nine)
	then
		filter_tips = self.ViewMgr.LanMgr:getLanValue("Nine")
	end
	self.GTextSeatType.text = filter_tips
	local show_fullDesk = self.DeskSearchFilter.is_seat_full
	if(show_fullDesk)
	then
		filter_tips = self.ViewMgr.LanMgr:getLanValue("Show")
	else
		filter_tips = self.ViewMgr.LanMgr:getLanValue("Hide")
	end
	self.GTextHideOrShow.text = filter_tips
	self:requestLobbyDesk()
end

function ViewLobby:keepCreateTableBtnFollowTable()
	if (#self.MapDeskItem> 0)
	then
		self.MapDeskItem[1]:ShowCreateBtnL()
		self.MapDeskItem[#self.MapDeskItem]:ShowCreateBtnR()
	end
end

function ViewLobby:GetCurrentItemIndex(list)
	if (list.scrollPane.posX == 0)
	then
		return 0
	end
	if(list.scrollPane.percX == 1)
	then
		return list.numItems - 1
	end

	return (list:GetFirstChildInView() ) % list.numItems--+ 1
end

function ViewLobby:getCurrentChipItem(index)
	local item = nil
	for key,value in pairs(self.MapBetChipOperateItem) do
		if(value.CurrentIndex == index)
		then
			item = value
			break
		end
	end

	return item
end

function ViewLobby:rendererPlayingFriend(index,item)
	if (#self.ListPlayingFriend > index)
	then
		local playing_friend_info = self.ListPlayingFriend[index + 1]
		if (self.CurrentPlayingFriendInfo == nil)
		then
			self.CurrentPlayingFriendInfo = playing_friend_info
			self:chooseCurrentPlayingFriend(self.CurrentPlayingFriendInfo)
		else
			if (self.CurrentPlayingFriendInfo.PlayerInfoCommon.PlayerGuid == playing_friend_info.PlayerInfoCommon.PlayerGuid)
			then
				self:chooseCurrentPlayingFriend(self.CurrentPlayingFriendInfo)
			end
		end
		local com = CS.Casinos.LuaHelper.GObjectCastToGCom(item)
		local playing_friend = ItemHeadIconWithNickName:new(nil,com)
		playing_friend:setFriendInfo(playing_friend_info, self.onClickPlayingFriend)
		playing_friend:setFriendName()
	end
end

function ViewLobby:onClickPlayingFriend(context)
	local com = CS.Casinos.LuaHelper.GObjectCastToGCom(context.sender)
	local playing_friend = ItemHeadIconWithNickName:new(nil,com)
	local playing_friend_info = playing_friend:getFriendInfo()
	self:chooseCurrentPlayingFriend(playing_friend_info)
end

function ViewLobby:refreshPlayingFriend()
	local ev = self.ViewMgr:getEv("EvUiClickSearchFriendsDesk")
	if(ev == nil)
	then
		ev = EvUiClickSearchFriendsDesk:new(nil)
	end
	ev.friend_state = CS.Casinos._eFriendStateClient.TexasDesktopClassic
	self.ViewMgr:sendEv(ev)
end




ViewLobbyFactory = ViewFactory:new()

function ViewLobbyFactory:new(o,ui_package_name,ui_component_name,
							  ui_layer,is_single,fit_screen)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	self.PackageName = ui_package_name
	self.ComponentName = ui_component_name
	self.UILayer = ui_layer
	self.IsSingle = is_single
	self.FitScreen = fit_screen
	return o
end

function ViewLobbyFactory:createView()
	local view = ViewLobby:new(nil)
	return view
end