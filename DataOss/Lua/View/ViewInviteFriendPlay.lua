-- Copyright(c) Cragon. All rights reserved.

ViewInviteFriendPlay = ViewBase:new()

function ViewInviteFriendPlay:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	o.GoUi = nil
	o.ComUi = nil
	o.Panel = nil
	o.UILayer = nil
	o.InitDepth = nil
	o.ViewKey = nil

    return o
end

function ViewInviteFriendPlay:onCreate()
	ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("InviteFriendPlayCard"))
	self.ControllerIM = self.ViewMgr.ControllerMgr:GetController("IM")
    self.GListOnLine = self.ComUi:GetChild("ListOnLine").asList
    self.GListOnLine:SetVirtual()
    self.GListOnLine.itemRenderer =	function(index,item)
		self:rendererOnLineFriend(index,item)
	end
	
    self.GListOffLine = self.ComUi:GetChild("ListOffLine").asList
    self.GListOffLine:SetVirtual()
    self.GListOffLine.itemRenderer = function(index,item)
		self:rendererOffLineFriend(index,item)
	end
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
	self.ViewMgr:BindEvListener("EvEntityFriendOnlineStateChange",self)
end

function ViewInviteFriendPlay:onDestroy()
	self.ViewMgr:UnbindEvListener(self)
end

function ViewInviteFriendPlay:onHandleEv(ev)
	if(ev ~= nil)
	then
		if(ev.EventName == "EvEntityFriendOnlineStateChange")
		then
			self:setFriend()
		end
	end
end

function ViewInviteFriendPlay:setFriend()
	self.GListOnLine.numItems = #self.ControllerIM.IMFriendList.ListOnLineFriendGuid
    self.GListOffLine.numItems = #self.ControllerIM.IMFriendList.ListOffLineFriendGuid
end

function ViewInviteFriendPlay:onClickBtnClose()
	self.ViewMgr:DestroyView(self)
end

function ViewInviteFriendPlay:rendererOnLineFriend(index,item)
	local list_online_friend = self.ControllerIM.IMFriendList.ListOnLineFriendGuid
    if (#list_online_friend > index)
	then
		local friend_guid = list_online_friend[index + 1]
        local friend_info = self.ControllerIM.IMFriendList:getFriendInfo(friend_guid)
        if (friend_info ~= nil)
		then
			local com = CS.Casinos.LuaHelper.GObjectCastToGCom(item)
			local ui_item = ItemInviteFriendPlay:new(nil,com,self)
            ui_item:setFriendInfo(friend_info)
		end
	end
end

function ViewInviteFriendPlay:rendererOffLineFriend(index,item)
	local list_offline_friend = self.ControllerIM.IMFriendList.ListOffLineFriendGuid
    if (#list_offline_friend > index)
	then
		local friend_guid = list_offline_friend[index + 1]
        local friend_info = self.ControllerIM.IMFriendList:getFriendInfo(friend_guid)
        if (friend_info ~= nil)
		then
			local com = CS.Casinos.LuaHelper.GObjectCastToGCom(item)
			local ui_item = ItemInviteFriendPlay:new(nil,com,self)
            ui_item:setFriendInfo(friend_info)
		end
	end
end
	

			

ViewInviteFriendPlayFactory = ViewFactory:new()

function ViewInviteFriendPlayFactory:new(o,ui_package_name,ui_component_name,
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

function ViewInviteFriendPlayFactory:CreateView()
	local view = ViewInviteFriendPlay:new(nil)	
	return view
end