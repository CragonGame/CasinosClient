-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ItemChatTargetInfo = {}

---------------------------------------
function ItemChatTargetInfo:new(o, com)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Com = com
    o.ViewChatFriend = view_chatfriend
    local co_headicon = o.Com:GetChild("CoHeadIcon").asCom
    o.ViewHeadIcon = ViewHeadIcon:new(nil, co_headicon)
    o.GTextName = o.Com:GetChild("TextName").asTextField
    o.GTextLastRecord = o.Com:GetChild("TextLastRecord").asTextField
    o.GTextSendTm = o.Com:GetChild("TextSendTm").asTextField
    o.GTextNewRecordCount = o.Com:GetChild("NewRecordCount").asTextField
    o.ControlSelect = o.Com:GetController("ControlSelect")
    o.ControllerNewRecord = o.Com:GetController("ControllerNewRecord")
    o.Com.onClick:Add(
            function()
                o:_onClick()
            end
    )
    local press = CS.FairyGUI.LongPressGesture(o.Com)
    press.once = true
    press.onAction:Add(
            function()
                o:onPress()
            end
    )

    return o
end

---------------------------------------
function ItemChatTargetInfo:init(view_chatfriend)
    self.ViewChatFriend = view_chatfriend
end

---------------------------------------
function ItemChatTargetInfo:setFriendInfo(friend_item, current_chat_guid, chat_record, unread_chatcount)
    self.GTextLastRecord.text = ""
    self.GTextSendTm.text = ""
    self.GTextNewRecordCount.text = ""
    self.ControlSelect:SetSelectedIndex(0)
    self.ControllerNewRecord:SetSelectedIndex(0)
    if (friend_item == nil) then
        return
    end
    local icon_name = friend_item.PlayerInfoCommon.IconName
    if (self.FriendInfo == nil or self.FriendInfo.PlayerInfoCommon.AccountId ~= friend_item.PlayerInfoCommon.AccountId) then
        self.ViewHeadIcon:SetPlayerInfo(icon_name, friend_item.PlayerInfoCommon.AccountId, friend_item.PlayerInfoCommon.VIPLevel)
    end
    self.FriendInfo = friend_item
    self.GTextName.text = CS.Casinos.UiHelper.addEllipsisToStr(self.FriendInfo.PlayerInfoCommon.NickName, 18, 5)
    self.GTextSendTm.text = ""
    self:showLastChatRecord(chat_record)
    self.HaveNewRecord = unread_chatcount > 0
    if (self.HaveNewRecord) then
        self.ControllerNewRecord.selectedIndex = 0
        self.GTextNewRecordCount.text = tostring(unread_chatcount)
    else
        self.ControllerNewRecord.selectedIndex = 1
    end
    if (self.FriendInfo.PlayerInfoCommon.PlayerGuid == current_chat_guid) then
        self.ControllerNewRecord.selectedIndex = 1
        self.ControlSelect.selectedIndex = 1
    else
        self.ControlSelect.selectedIndex = 0
    end
end

---------------------------------------
function ItemChatTargetInfo:chatRecordChange(last_record)
    self:showLastChatRecord(last_record)
end

---------------------------------------
function ItemChatTargetInfo:showFriendLastRecordAndUnReadRecordCount(friend_guid)
    local last_chat_record = ControllerIM.IMChat:getLastChatMsgRecord(friend_guid)
    self:showLastChatRecord(last_chat_record)
    local friend_unreadchat_count = ControllerIM.IMChat:getFriendNewChatCount(friend_guid)
    if (friend_unreadchat_count > 0) then
        self.ControllerNewRecord.selectedIndex = 0
        self.GTextNewRecordCount.text = friend_unreadchat_count.ToString()
    else
        self.ControllerNewRecord.selectedIndex = 1
    end
end

---------------------------------------
function ItemChatTargetInfo:showLastChatRecord(last_chat_record)
    if (last_chat_record ~= nil) then
        local last_content = last_chat_record.msg
        self.GTextLastRecord.text = last_content
        local d_tm = CS.System.DateTime.Parse(last_chat_record.dt)
        local last_sendtime = CS.Casinos.LuaHelper.DataTimeToString(d_tm, "yyyy.MM.dd HH:mm")
        self.GTextSendTm.text = last_sendtime
    end
end

---------------------------------------
function ItemChatTargetInfo:_onClick()
    self.ControllerNewRecord.selectedIndex = 1
    self.GTextNewRecordCount.text = ""
    local guid = self.FriendInfo.PlayerInfoCommon.PlayerGuid
    self.ViewChatFriend:initChatMsg(guid)
end

---------------------------------------
function ItemChatTargetInfo:onPress()
    local msg_box = self.ViewChatFriend.ViewMgr:CreateView("MsgBox")
    msg_box:showMsgBox2(self.ViewChatFriend.ViewMgr.LanMgr:getLanValue("DeleteRecode"),
            string.format(self.ViewChatFriend.ViewMgr.LanMgr:getLanValue("DetermineDeleteRecord"),
                    self.FriendInfo.PlayerInfoCommon.NickName), self.FriendInfo.PlayerInfoCommon.PlayerGuid,
            function(bo, guid)
                if (bo) then
                    local view_mgr = ViewMgr:new(nil)
                    local ev = view_mgr:GetEv("EvUiClickDeleteFriendChatRecord")
                    if (ev == nil) then
                        ev = EvUiClickDeleteFriendChatRecord:new(nil)
                    end
                    ev.friend_etguid = guid
                    view_mgr:SendEv(ev)
                end
            end)
end

---------------------------------------
function ItemChatTargetInfo:Reset()
    self.ViewChatFriend = nil
    self.FriendInfo = nil
end