ItemHeadIconWithNickName = {}

function ItemHeadIconWithNickName:new(o,com,view_mgr)
	o = o or {}
	setmetatable(o, self)
	self.__index = self
	o.Com = com
	o.ViewMgr = view_mgr
	local com_headIcon = o.Com:GetChild("ComHeadIcon").asCom
	o.ViewHeadIcon = ViewHeadIcon:new(nil,com_headIcon)
	return o
end

function ItemHeadIconWithNickName:setFriendInfo1(friend_info)
	self.PlayerInfo = friend_info
	self.ViewHeadIcon:setPlayerInfo(friend_info.PlayerInfoCommon.IconName,
			friend_info.PlayerInfoCommon.AccountId, friend_info.PlayerInfoCommon.VIPLevel,
			friend_info.PlayerInfoMore.OnlineState == PlayerOnlineState.Online)
	self.Com.onClick:Add(function ()
		self:onClick()
	end)
end

function ItemHeadIconWithNickName:setFriendInfo(icon_name, account_id, vip_level, is_online, on_click)
	self.ViewHeadIcon:setPlayerInfo(icon_name, account_id, vip_level, is_online)
	self.Com.onClick:Add(on_click)
end

function ItemHeadIconWithNickName:getFriendInfo()
	return self.PlayerInfo
end

function ItemHeadIconWithNickName:setFriendName()
	local text_name = self.Com:GetChild("NickName").asTextField
	text_name.text = self.PlayerInfo.PlayerInfoCommon.NickName
end

function ItemHeadIconWithNickName:onClick()
	local ev = self.ViewMgr:getEv("EvClickIconWithNickName")
	if(ev == nil)
	then
		ev = EvClickIconWithNickName:new(nil)
	end
	ev.player = self.PlayerInfo
	self.ViewMgr:sendEv(ev)
end