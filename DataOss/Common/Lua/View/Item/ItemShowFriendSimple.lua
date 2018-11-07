-- Copyright(c) Cragon. All rights reserved.
-- 好友对话框左侧的一个Item

---------------------------------------
ItemShowFriendSimple = {}

---------------------------------------
function ItemShowFriendSimple:new(o, com, view_friend)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewFriend = view_friend
    o.GComFriendItem = com
    o.GComFriendItem.onClick:Add(
            function()
                o:onClickComFriendItem()
            end
    )
    o.ControlBtnSelect = o.GComFriendItem:GetController("PlayerSimpleControl")
    local head_icon = o.GComFriendItem:GetChild("HeadIcon").asCom
    o.ViewHeadIcon = ViewHeadIcon:new(nil, head_icon, nil,
            function(bo)
                o:loadIconDown(bo)
            end
    )
    o.GTextBtnNickName = o.GComFriendItem:GetChild("title").asTextField
    o.GTextBtnState = o.GComFriendItem:GetChild("TextState").asTextField
    return o
end

---------------------------------------
function ItemShowFriendSimple:setFriendInfo(view_friend, friend_item, is_friend)
    self.ViewFriend = view_friend
    self.FriendInfo = friend_item
    self.IsFriend = is_friend
    self.GTextBtnNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(self.FriendInfo.PlayerInfoCommon.NickName, 15, 4)
    local friend_state_str = ""
    if (self.IsFriend) then
        friend_state_str = self.ViewFriend.ControllerIM.IMFriendList:getFriendStateStr(self.FriendInfo.PlayerInfoCommon.PlayerGuid)
    else
        local state = CasinoHelper:TranslateFriendStateEx(self.FriendInfo)
        friend_state_str = CasinoHelper:TranslateFriendState(state)
    end
    local friend_state = friend_state_str
    self.GTextBtnState.text = friend_state
    self.ViewHeadIcon:SetPlayerInfo(self.FriendInfo.PlayerInfoCommon.IconName, self.FriendInfo.PlayerInfoCommon.AccountId,
            self.FriendInfo.PlayerInfoCommon.VIPLevel)
end

---------------------------------------
function ItemShowFriendSimple:isSelected(is_select)
    if (is_select) then
        self.ControlBtnSelect.selectedPage = "Select"
    else
        self.ControlBtnSelect.selectedPage = "UnSelect"
    end
end

---------------------------------------
function ItemShowFriendSimple:getFriendInfo()
    return self.FriendInfo
end

---------------------------------------
function ItemShowFriendSimple:loadIconDown(bo)
    if (bo) then
        local view_mgr = ViewMgr:new(nil)
        local ev = view_mgr:GetEv("EvLoadPlayerIconSuccess")
        if (ev == nil) then
            ev = EvLoadPlayerIconSuccess:new(nil)
        end
        ev.et_guid = self.FriendInfo.PlayerInfoCommon.PlayerGuid
        ev.icon = self.ViewHeadIcon.GLoaderPlayerIcon.texture.nativeTexture
        ev.fariy_t = self.ViewHeadIcon.GLoaderPlayerIcon.texture
        view_mgr:SendEv(ev)
    end
end

---------------------------------------
function ItemShowFriendSimple:onClickComFriendItem()
    if (self.IsFriend) then
        if (self.ViewFriend:getCurrentFriendInfo().PlayerInfoCommon.PlayerGuid == self.FriendInfo.PlayerInfoCommon.PlayerGuid) then
            return
        end
        self.ViewFriend:setCurrentFriend(self.FriendInfo)
    else
        if (self.ViewFriend:getCurrentRecommandInfo().PlayerInfoCommon.PlayerGuid == self.FriendInfo.PlayerInfoCommon.PlayerGuid) then
            return
        end
        self.ViewFriend:setCurrentRecommandFriend(self.FriendInfo)
    end
end