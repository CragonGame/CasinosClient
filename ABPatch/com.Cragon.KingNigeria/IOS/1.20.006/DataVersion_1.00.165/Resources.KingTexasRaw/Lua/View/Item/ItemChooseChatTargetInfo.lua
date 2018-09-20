ItemChooseChatTargetInfo = {}

function ItemChooseChatTargetInfo:new(o,com,controller_im)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
	o.Com = com
	o.ControllerIM = controller_im
    local co_headicon = o.Com:GetChild("CoHeadIcon").asCom
    o.ViewHeadIcon = ViewHeadIcon:new(nil,co_headicon)
    o.GTextName = o.Com:GetChild("TextName").asTextField
    o.GTextState = o.Com:GetChild("TextState").asTextField
    o.GBtnChoose = o.Com:GetChild("Lan_Btn_Choose").asButton
    o.GBtnChoose.onClick:Add(
		function()
			o:onClickChooseTarget()
		end
	)
    o.Com.onClick:Add(
		function()
			o:onClickChooseTarget()
		end
	)
	return o
end

function ItemChooseChatTargetInfo:setFriendInfo(friend_item)
	self.FriendInfo = friend_item
    local icon_name = self.FriendInfo.PlayerInfoCommon.IconName
    self.ViewHeadIcon:setPlayerInfo(self.FriendInfo.PlayerInfoCommon.IconName, self.FriendInfo.PlayerInfoCommon.AccountId,
        self.FriendInfo.PlayerInfoCommon.VIPLevel)
    self.GTextName.text = self.FriendInfo.PlayerInfoCommon.NickName
    local friend_state = self.ControllerIM.IMFriendList:getFriendStateStr(self.FriendInfo.PlayerInfoCommon.PlayerGuid)
    self.GTextState.text = friend_state
end

function ItemChooseChatTargetInfo:onClickChooseTarget()
	local view_mgr = ViewMgr:new(nil)
	local ev = ViewMgr:getEv("EvUiClickChooseFriend")
	if(ev == nil)
	then
		ev = EvUiClickChooseFriend:new(nil)
	end
    ev.friend_info = self.FriendInfo
    ev.is_choosechat = true
    view_mgr:sendEv(ev)
end

