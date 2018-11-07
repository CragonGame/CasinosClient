-- Copyright(c) Cragon. All rights reserved.
-- 好友头像，主界面上。不包含好友对话框中的

---------------------------------------
ItemHeadIcon = {}

---------------------------------------
function ItemHeadIcon:new(o, com)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Com = com
    o.ViewHeadIcon = ViewHeadIcon:new(nil, o.Com, nil, nil)
    return o
end

---------------------------------------
function ItemHeadIcon:setFriendInfo(friend_info, on_click)
    self.PlayerInfo = friend_info
    self.ViewHeadIcon:SetPlayerInfo(friend_info.PlayerInfoCommon.IconName,
            friend_info.PlayerInfoCommon.AccountId, friend_info.PlayerInfoCommon.VIPLevel,
            friend_info.PlayerInfoMore.OnlineState == PlayerOnlineState.Online)
    self.Com.onClick:Add(on_click)
end

---------------------------------------
function ItemHeadIcon:setFriendInfo1(icon_name, account_id, vip_level, is_online, on_click)
    self.ViewHeadIcon:SetPlayerInfo(icon_name, account_id, vip_level, is_online)
    self.Com.onClick:Add(on_click)
end

---------------------------------------
function ItemHeadIcon:getFriendInfo()
    return self.PlayerInfo
end

---------------------------------------
function ItemHeadIcon:setFriendName()
    local text_name = self.Com:GetChild("TextNickName").asTextField
    text_name.text = self.PlayerInfo.PlayerInfoCommon.NickName
end