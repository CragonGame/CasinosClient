ViewChatFriend = ViewBase:new()

function ViewChatFriend:new(o)
	o = o or {}
	setmetatable(o, self)
	self.__index = self
	self.ViewMgr = nil
	self.GoUi = nil
	self.ComUi = nil
	self.Panel = nil
	self.UILayer = nil
	self.InitDepth = nil
	self.ViewKey = nil

	return o
end

function ViewChatFriend:onCreate()
	ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("Message"))
	self.ControllerIM = self.ViewMgr.ControllerMgr:GetController("IM")
	self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
	self.ControllerPlayer = self.ViewMgr.ControllerMgr:GetController("Player")
	self.ViewPool = self.ViewMgr:getView("Pool")
	local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
	local btn_close = com_bg:GetChild("BtnClose").asButton
	btn_close.onClick:Add(
			function()
				self:onClickBtnClose()
			end
	)
	local com_shade = com_bg:GetChild("ComShade").asCom
	com_shade.onClick:Add(
			function()
				self:onClickBtnClose()
			end
	)
	self.ControllerIfHaveChatTarget = self.ComUi:GetController("ControllerMessage")
	self.ControllerWaiting = self.ComUi:GetController("ControllerWaiting")
	self.GListChatTarget = self.ComUi:GetChild("ListChatTarget").asList
	self.GListChatTarget.scrollItemToViewOnClick = false
	--self.GListChatTarget:SetVirtual()
	--self.GListChatTarget.itemRenderer = function(a,b)
	--	self:RenderListItemChatTarget(a,b)
	--end
	self.GListChatContent = self.ComUi:GetChild("ListChatContent").asList
	self.GListChatContent:SetVirtual()
	self.GListChatContent.itemRenderer = function(a, b)
		self:RenderListItemChatContent(a, b)
	end
	local com_input = self.ComUi:GetChild("ComInput").asCom
	self.GTextInputSendText = com_input:GetChild("TextInput").asTextInput
	self.GBtnSend = com_input:GetChild("Lan_Btn_Send").asButton
	self.GBtnSend.onClick:Add(
			function()
				self:onClickBtnSendMsg()
			end
	)
	self.GBtnChooseChatTarget = self.ComUi:GetChild("BtnChooseChatTarget").asButton
	self.GBtnChooseChatTarget.onClick:Add(
			function()
				self:onClickBtnChooseChatTarget()
			end
	)
	self.GBtnChatTargetProfile = self.ComUi:GetChild("BtnChatTargetProfile").asButton
	self.GBtnChatTargetProfile.onClick:Add(
			function()
				self:onClickBtnCurrentFriendProfile()
			end
	)
	self.GBtnPlayWithChatTarget = self.ComUi:GetChild("BtnPlayWithChatTarget").asButton
	self.GBtnPlayWithChatTarget.onClick:Add(
			function()
				self:onClickBtnPlayWithFriend()
			end
	)
	self.ChatRecordInitWidth = self.GListChatContent.width - 30
	self.MapChatTargets = {}
	self.ViewMgr:bindEvListener("EvEntityFriendOnlineStateChange", self)
	self.ViewMgr:bindEvListener("EvEntityNotifyDeleteFriend", self)
	self.ViewMgr:bindEvListener("EvEntityDeleteFriendChatRecordSuccess", self)
	self.ViewMgr:bindEvListener("EvEntityReceiveFriendSingleChat", self)
	self.ViewMgr:bindEvListener("EvEntityReceiveFriendChats", self)
	self.ViewMgr:bindEvListener("EvEntityChatRecordRequestResult", self)
end

function ViewChatFriend:onDestroy()
	self.ViewMgr:unbindEvListener(self)
end

function ViewChatFriend:onHandleEv(ev)
	if (ev.EventName == "EvEntityFriendOnlineStateChange")
	then
		local player_info = ev.player_info
		if (self.CurrentChatTarget ~= nil and player_info.PlayerInfoCommon.PlayerGuid == self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid and player_info.PlayerInfoMore.OnlineState == PlayerOnlineState.Offline)
		then
			self.FriendDesktopGuid = ""
			self:currentPlayerIsInGame(false)
		end
	elseif (ev.EventName == "EvEntityNotifyDeleteFriend")
	then
		local friend_etguid = ev.friend_etguid
		if (self.CurrentChatTarget ~= nil)
		then
			if (self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid == friend_etguid)
			then
				self.CurrentChatTarget = nil
				self:deleteRecord(friend_etguid)
			end
		end
	elseif (ev.EventName == "EvEntityDeleteFriendChatRecordSuccess")
	then
		self:deleteRecord(ev.friend_etguid)
	elseif (ev.EventName == "EvEntityReceiveFriendSingleChat")
	then
		if (self.CurrentChatTarget ~= nil)
		then
			local friend_guid = ev.friend_etguid
			if (friend_guid == self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid)
			then
				self:setCurrentChatMsg(ev.chat_msg)
			else
				self:renderChatTarget()
			end
		else
			self:initChatMsg("")
		end
	elseif (ev.EventName == "EvEntityReceiveFriendChats")
	then
		if (self.CurrentChatTarget ~= nil)
		then
			local friend_guid = ev.friend_etguid
			if (friend_guid == self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid)
			then
				self:setCurrentChatTarget(self.CurrentChatTarget, ev.list_allchats, false)
			else
				self:renderChatTarget()
			end
		else
			self:initChatMsg("")
		end
	elseif (ev.EventName == "EvEntityChatRecordRequestResult")
	then
		if (self.CurrentChatTarget ~= nil)
		then
			if (ev.friend_etguid == self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid)
			then
				self:setCurrentChatTarget(self.CurrentChatTarget, ev.list_allchats, false)
			end
		end
	end
end

function ViewChatFriend:addRecord(chat_record)
	if (self.CurrentChatTarget ~= nil)
	then
		local list_chat = self.ControllerIM.IMChat:getListChatShow(self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid)
		self.GListChatContent.numItems = #list_chat
		self.GListChatContent.scrollPane:ScrollBottom()
	end
end

function ViewChatFriend:deleteRecord(friend_etguid)
	self.MapChatTargets[friend_etguid] = nil
	if (self.CurrentChatTarget ~= nil and self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid == friend_etguid)
	then
		self.CurrentChatTarget = nil
		self:initChatMsg("")
	else
		self:renderChatTarget()
	end
end

function ViewChatFriend:onClickBtnCurrentFriendProfile()
	if (self.CurrentChatTarget == nil)
	then
		return
	end
	local ui_playerprofile = self.ViewMgr:createView("PlayerProfile")
	ui_playerprofile:setPlayerGuid(CS.Casions._ePlayerProfileType.Ranking, self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid, nil)
end

function ViewChatFriend:onClickBtnPlayWithFriend()
	if (self.CurrentChatTarget == nil or (self.FriendDesktopGuid == nil or self.FriendDesktopGuid == ""))
	then
		return
	end
	local ev = self.ViewMgr:getEv("EvUiClickViewInDesk")
	if (ev == nil)
	then
		ev = EvUiClickViewInDesk:new(nil)
	end
	ev.desk_etguid = self.FriendDesktopGuid
	ev.desktop_filter = self.ViewMgr:unpackData(self.CurrentChatTarget.PlayerPlayState.UserData2)-- CS.Casinos.LuaHelper.JsonDeserializeDesktopFilter(self.CurrentChatTarget.PlayerPlayState.UserData2)
	ev.seat_index = 255
	self.ViewMgr:sendEv(ev)
end

function ViewChatFriend:initChatMsg(current_chattarget)
	for i, v in pairs(self.MapChatTargets) do
		self.ViewPool:chatTargetInfoEnque(v)
	end
	self.MapChatTargets = {}
	self.GListChatTarget:RemoveChildren()

	local map_friend = self.ControllerIM.IMFriendList.MapFriendList
	local player_info = map_friend[current_chattarget]
	if (player_info ~= nil)
	then
		if (player_info.PlayerInfoCommon.PlayerGuid == current_chattarget)
		then
			self.CurrentChatTarget = player_info
		end
	end
	local list_chattarget = self.ControllerIM.IMChat.ListChatTarget
	if (#list_chattarget == 0)
	then
		self.ControllerIfHaveChatTarget.selectedIndex = 0
	else
		if (self.CurrentChatTarget == nil)
		then
			local map_chats = self.ControllerIM.IMChat.MapChats
			for key, value in pairs(map_chats) do
				local player_infoex = map_friend[key]
				if ((current_chattarget == nil or current_chattarget == "") and player_infoex ~= nil)
				then
					self:currentChatTargetChange(player_infoex.PlayerInfoCommon.PlayerGuid)
					self.CurrentChatTarget = player_infoex
					break
				end
			end
		end
		self:renderChatTarget()
	end
	local have_chat_select_index = 0
	if (self.CurrentChatTarget ~= nil)
	then
		have_chat_select_index = 1
		local list_chatrecords, need_reload_record = self.ControllerIM.IMChat:getPlayerChatMsgRecv(self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid)
		local list_chat = self.ControllerIM.IMChat:getListChatShow(self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid)
		self:setCurrentChatTarget(self.CurrentChatTarget, list_chat, need_reload_record)
	end
	self.ControllerIfHaveChatTarget.selectedIndex = have_chat_select_index
	self.GListChatContent.scrollPane:ScrollBottom()
end

function ViewChatFriend:setCurrentChatMsg(chat_record)
	local from_playeretguid = chat_record.sender_guid
	if ((from_playeretguid ~= self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid) and (from_playeretguid ~= self.ControllerPlayer.Guid))
	then
		return
	end
	local friend_guid = from_playeretguid
	if (from_playeretguid == self.ControllerPlayer.Guid)
	then
		friend_guid = chat_record.recver_guid
	end
	local ev = self.ViewMgr:getEv("EvUiChatConfirmRead")
	if (ev == nil)
	then
		ev = EvUiChatConfirmRead:new(nil)
	end
	ev.friend_etguid = friend_guid
	ev.msg_id = chat_record.msg_id
	self.ViewMgr:sendEv(ev)
	local child = nil
	if (self.MapChatTargets[friend_guid] ~= nil)
	then
		child = self.MapChatTargets[friend_guid]
		child:chatRecordChange(chat_record)
	end
	self:addRecord(chat_record)
end

function ViewChatFriend:setCurrentChatTarget(player_info, list_chatrecv, need_reload_record)
	if (player_info == nil)
	then
		self.GBtnChatTargetProfile.enabled = false
		self.FriendDesktopGuid = ""
		self:currentPlayerIsInGame(false)
		self.ControllerIfHaveChatTarget.selectedIndex = 0
		return
	end
	self.ControllerIfHaveChatTarget.selectedIndex = 1
	self.CurrentChatTarget = player_info
	local current_targetetguid = self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid
	if (need_reload_record)
	then
		self.ControllerWaiting.selectedIndex = 0
		return
	else
		self.ControllerWaiting.selectedIndex = 1
	end
	local player_playstate = self.CurrentChatTarget.PlayerPlayState
	if (player_playstate == nil)
	then
		self.FriendDesktopGuid = ""
	else
		self.FriendDesktopGuid = player_playstate.DesktopGuid
	end
	local last_chat_record = self.ControllerIM.IMChat:getLastChatMsgRecord(current_targetetguid)
	if (last_chat_record ~= nil)
	then
		local ev = self.ViewMgr:getEv("EvUiChatConfirmRead")
		if (ev == nil)
		then
			ev = EvUiChatConfirmRead:new(nil)
		end
		ev.friend_etguid = current_targetetguid
		ev.msg_id = last_chat_record.msg_id
		self.ViewMgr:sendEv(ev)
	end
	if (list_chatrecv ~= nil)
	then
		self.GListChatContent.numItems = #list_chatrecv
	else
		self.GListChatContent.numItems = 0
	end
	self:currentPlayerIsInGame(self.FriendDesktopGuid ~= nil and self.FriendDesktopGuid ~= "")
	self.GBtnChatTargetProfile.enabled = true
end

function ViewChatFriend:currentChatTargetRecordChange(last_record)
end

function ViewChatFriend:onClickBtnChooseChatTarget()
	local ev = self.ViewMgr:getEv("EvUiClickChooseFriendChatTarget")
	if (ev == nil)
	then
		ev = EvUiClickChooseFriendChatTarget:new(nil)
	end
	self.ViewMgr:sendEv(ev)
end

function ViewChatFriend:onClickBtnSendMsg()
	if (self.GTextInputSendText.text == nil or self.GTextInputSendText.text == "")
	then
		ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("SendNoEmpty"))
		return
	end
	local c_m = ChatMsg:new(nil)
	c_m.recver_guid = self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid
	c_m.recver_nickname = self.CurrentChatTarget.PlayerInfoCommon.NickName
	c_m.sender_guid = self.ControllerPlayer.Guid
	c_m.sender_nickname = self.ControllerActor.PropNickName:get()
	c_m.msg = self.GTextInputSendText.text
	local ev = self.ViewMgr:getEv("EvUiSendMsg")
	if (ev == nil)
	then
		ev = EvUiSendMsg:new(nil)
	end
	ev.chat_msg = c_m:getData4Pack()
	self.ViewMgr:sendEv(ev)
	self.GTextInputSendText.text = ""
end

function ViewChatFriend:onClickBtnClose()
	self.ViewMgr:destroyView(self)
end

function ViewChatFriend:renderChatTarget()
	for i, v in pairs(self.MapChatTargets) do
		self.ViewPool:chatTargetInfoEnque(v)
	end
	self.MapChatTargets = {}
	self.GListChatTarget:RemoveChildren()
	--self.GListChatTarget.numItems = #self.ControllerIM.IMChat.ListChatTarget
	for i, v in pairs(self.ControllerIM.IMChat.ListChatTarget) do
		local item = self.ViewPool:getItemChatTargetInfo()
		self.GListChatTarget:AddChild(item.Com)
		item:init(self)
		local friend_guid = v.FriendGuid
		local player_info = self.ControllerIM.IMFriendList.MapFriendList[friend_guid]
		if (player_info ~= nil)
		then
			local last_chat_record = self.ControllerIM.IMChat:getLastChatMsgRecord(friend_guid)
			local unread_count = self.ControllerIM.IMChat:getFriendNewChatCount(friend_guid)
			--item:setFriendInfo(player_info, "", last_chat_record, unread_count)
			local guid = ""
			if (self.CurrentChatTarget ~= nil)
			then
				guid = self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid
			end
			item:setFriendInfo(player_info, guid, last_chat_record, unread_count)
			self.MapChatTargets[friend_guid] = item
		end
	end
end

function ViewChatFriend:RenderListItemChatTarget(index, obj)
	local list_have_record_friend = self.ControllerIM.IMChat.ListChatTarget
	local com = CS.Casinos.LuaHelper.GObjectCastToGCom(obj)
	local item = self.ViewPool:getItemChatTargetInfo(com)
	item:init(com, self)
	if (#list_have_record_friend > index)
	then
		local friend_guid = list_have_record_friend[index + 1].FriendGuid
		local player_info = self.ControllerIM.IMFriendList.MapFriendList[friend_guid]
		if (player_info ~= nil)
		then
			local last_chat_record = self.ControllerIM.IMChat:getLastChatMsgRecord(friend_guid)
			local unread_count = self.ControllerIM.IMChat:getFriendNewChatCount(friend_guid)
			--item:setFriendInfo(player_info, "", last_chat_record, unread_count)
			local guid = ""
			if (self.CurrentChatTarget ~= nil)
			then
				guid = self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid
			end
			item:setFriendInfo(player_info, guid, last_chat_record, unread_count)
			self.MapChatTargets[friend_guid] = item
		end
	end
end

function ViewChatFriend:RenderListItemChatContent(index, obj)
	if (self.CurrentChatTarget == nil)
	then
		return
	end
	local list_chat = self.ControllerIM.IMChat:getListChatShow(self.CurrentChatTarget.PlayerInfoCommon.PlayerGuid)
	if (list_chat ~= nil)
	then
		if (#list_chat > index)
		then
			local com = CS.Casinos.LuaHelper.GObjectCastToGCom(obj)
			local item = self.ViewPool:getItemChatEx(com)
			local record = list_chat[index + 1]
			local is_self = record.sender_guid == self.ControllerPlayer.Guid
			local sender_name_color = ""
			if (is_self)
			then
				sender_name_color = "0000FF"
			end
			if (is_self)
			then
				item:setChat(record.sender_nickname, record.msg, record.sender_viplevel,
						record.dt, sender_name_color, "FFFFFF", false, self.ChatRecordInitWidth,
						CS.Casinos._eChatItemType.NormalChat, true, true, false, record.is_tm)
			else
				item:setChat(record.sender_nickname, record.msg, record.sender_viplevel,
						record.dt, sender_name_color, "FFFFFF", true, self.ChatRecordInitWidth,
						CS.Casinos._eChatItemType.NormalChat, true, true, false, record.is_tm)
			end
		end
	end
end

function ViewChatFriend:currentPlayerIsInGame(is_ingame)
	self.GBtnPlayWithChatTarget.enabled = is_ingame
end

function ViewChatFriend:currentChatTargetChange()
	local ev = self.ViewMgr:getEv("EvUiCurrentChatTargetChange")
	if (ev == nil)
	then
		ev = EvUiCurrentChatTargetChange:new(nil)
	end
	ev.current_chattarget = current_chattarget
	self.ViewMgr:sendEv(ev)
end

ViewChatFriendFactory = ViewFactory:new()

function ViewChatFriendFactory:new(o, ui_package_name, ui_component_name,
								   ui_layer, is_single, fit_screen)
	o = o or {}
	setmetatable(o, self)
	self.__index = self
	self.PackageName = ui_package_name
	self.ComponentName = ui_component_name
	self.UILayer = ui_layer
	self.IsSingle = is_single
	self.FitScreen = fit_screen
	return o
end

function ViewChatFriendFactory:createView()
	local view = ViewChatFriend:new(nil)
	return view
end