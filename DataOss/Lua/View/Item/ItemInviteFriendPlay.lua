-- Copyright(c) Cragon. All rights reserved.

ItemInviteFriendPlay = {}

function ItemInviteFriendPlay:new(o,com,view)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
	o.ViewInviteFriendPlay = view
	o.Com = com
	o.AlreadyInvite = false
	local com_headicon = o.Com:GetChild("CoHeadIcon").asCom
    o.ViewHeadIcon = ViewHeadIcon:new(nil,com_headicon)
    o.GTextName = o.Com:GetChild("TextTitle").asTextField
    o.GTextTm = o.Com:GetChild("TextOutLineTime").asTextField
    o.Com.onClick:Add(
		function()
			o:onClickItem()
		end
	)
	return o
end

function ItemInviteFriendPlay:setFriendInfo(friend_item)
	self.FriendInfo = friend_item;
    self.ViewHeadIcon:setPlayerInfo(friend_item.PlayerInfoCommon.IconName, friend_item.PlayerInfoCommon.AccountId,
        friend_item.PlayerInfoCommon.VIPLevel)
    self.GTextName.text = CS.Casinos.UiHelper.addEllipsisToStr(friend_item.PlayerInfoCommon.NickName,30,9)
    local friend_state = self.ViewInviteFriendPlay.ControllerIM.IMFriendList:getFriendStateStr(self.FriendInfo.PlayerInfoCommon.PlayerGuid)
    self.GTextTm.text = friend_state
end

function ItemInviteFriendPlay:onClickItem()
	local view_mgr = self.ViewInviteFriendPlay.ViewMgr
	if (self.AlreadyInvite == false)
	then
		local player_playstate = self.FriendInfo.PlayerPlayState
		local ev = view_mgr:GetEv("EvUiInviteFriendPlayTogether")
		if(ev == nil)
		then
			ev = EvUiInviteFriendPlayTogether:new(nil)
		end
        ev.friend_guid = self.FriendInfo.PlayerInfoCommon.PlayerGuid
		if(player_playstate == nil)
		then
			ev.friend_desktopguid = ""
		else
			ev.friend_desktopguid = player_playstate.DesktopGuid
		end
        view_mgr:SendEv(ev)
        self.GTextTm.text = view_mgr.LanMgr:getLanValue("HaveInvited")
        self.GTextTm.color = CS.UnityEngine.Color.yellow
        self.AlreadyInvite = true
	else
		ViewHelper:UiShowInfoFailed(view_mgr.LanMgr:getLanValue("DontRepeatInvitation"))
	end
end
