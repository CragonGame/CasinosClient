-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
IMFriendList = {}

---------------------------------------
function IMFriendList:new(o, co_im)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerIM = co_im
    self.MapFriendList = {}
    self.MapFriendState = {}
    self.ListFriendGuid = {}
    self.ListOnLineFriendGuid = {}
    self.ListOffLineFriendGuid = {}
    self.MapFriendRecordIsChecked = {}
    self.ListRecommendPlayer = {}
    self.ListPlayInDesktopFriend = {}
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    return o
end

---------------------------------------
function IMFriendList:OnIMAddFriendReqRequestResult(result)
    self.ControllerIM:showIMResult(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("RequestAddFriend"), result)
end

---------------------------------------
function IMFriendList:OnIMAddFriendResRequestResult(result)
    self.ControllerIM:showIMResult(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("ProAddFriendRequest"), result)
end

---------------------------------------
function IMFriendList:OnIMDeleteFriendRequestResult(result)
    self.ControllerIM:showIMResult(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("RequestDeleteFriend"), result)
end

---------------------------------------
function IMFriendList:OnIMFriendLoginNotify(player_guid)
    local player_info = self.MapFriendList[player_guid]
    if (player_info ~= nil) then
        player_info.PlayerInfoMore.OnlineState = PlayerOnlineState.Online
        self.MapFriendState[player_guid] = CasinoHelper:TranslateFriendStateEx(player_info)
        if (#self.ListOffLineFriendGuid > 0) then
            for i = 1, #self.ListOffLineFriendGuid do
                if (player_guid == self.ListOffLineFriendGuid[i]) then
                    self.ListOffLineFriendGuid[i] = nil
                    break
                end
            end
        end
        local contains = false
        if (#self.ListOnLineFriendGuid > 0) then
            for i = 1, #self.ListOnLineFriendGuid do
                if (player_guid == self.ListOnLineFriendGuid[i]) then
                    contains = true
                    break
                end
            end
        end
        if (contains == false) then
            table.insert(self.ListOnLineFriendGuid, player_guid)
        end
        local friend_onLine = self.ControllerIM.ViewMgr:CreateView("FriendOnLine")
        friend_onLine:setFriendInfo(player_info)
        local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityFriendOnlineStateChange")
        if (ev == nil) then
            ev = EvEntityFriendOnlineStateChange:new(nil)
        end
        ev.player_info = player_info
        self.ControllerIM.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
function IMFriendList:OnIMFriendLogoutNotify(player_guid)
    local player_info = self.MapFriendList[player_guid]
    if (player_info ~= nil) then
        player_info.PlayerInfoMore.OnlineState = PlayerOnlineState.Offline
        self.MapFriendState[player_guid] = CasinoHelper:TranslateFriendStateEx(player_info)
        if (#self.ListOnLineFriendGuid > 0) then
            for i = 1, #self.ListOnLineFriendGuid do
                if (player_guid == self.ListOnLineFriendGuid[i]) then
                    self.ListOnLineFriendGuid[i] = nil
                    break
                end
            end
        end
        local contains = false
        if (#self.ListOffLineFriendGuid > 0) then
            for i = 1, #self.ListOffLineFriendGuid do
                if (player_guid == self.ListOffLineFriendGuid[i]) then
                    contains = true
                    break
                end
            end
        end
        if (contains == false) then
            table.insert(self.ListOffLineFriendGuid, player_guid)
        end
        local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityFriendOnlineStateChange")
        if (ev == nil) then
            ev = EvEntityFriendOnlineStateChange:new(nil)
        end
        ev.player_info = player_info
        self.ControllerIM.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
function IMFriendList:OnIMFriendInfoCommonUpdateNotify(player_info_common)
    local p_i = PlayerInfo:new(nil)
    p_i:setData(player_info_common)
    local player_info = self.MapFriendList[p_i.PlayerInfoCommon.PlayerGuid]
    if (player_info ~= nil) then
        player_info.PlayerInfoCommon = p_i.PlayerInfoCommon
        self.MapFriendState[player_info.PlayerInfoCommon.PlayerGuid] = CasinoHelper:TranslateFriendStateEx(player_info)
        local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityRefreshFriendInfo")
        if (ev == nil) then
            ev = EvEntityRefreshFriendInfo:new(nil)
        end
        ev.player_info = player_info
        self.ControllerIM.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
function IMFriendList:OnIMFriendInfoMoreUpdateNotify(player_info_more)
    local p_i = PlayerInfo:new(nil)
    p_i:setData(player_info_more)
    local player_info = self.MapFriendList[p_i.PlayerInfoCommon.PlayerGuid]
    if (player_info ~= nil) then
        player_info.PlayerInfoCommon = p_i.PlayerInfoCommon
        player_info.PlayerInfoMore = p_i.PlayerInfoMore
        self.MapFriendState[player_info.PlayerInfoCommon.PlayerGuid] = CasinoHelper:TranslateFriendStateEx(player_info)
        local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityRefreshFriendInfo")
        if (ev == nil) then
            ev = EvEntityRefreshFriendInfo:new(nil)
        end
        ev.player_info = player_info
        self.ControllerIM.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
function IMFriendList:OnIMFriendInfoRealtimeUpdateNotify(player_info_realtime)
    local p_i = PlayerInfo:new(nil)
    p_i:setData(player_info_realtime)
    local player_info = self.MapFriendList[p_i.PlayerInfoCommon.PlayerGuid]
    if (player_info ~= nil) then
        player_info.PlayerInfoCommon = p_i.PlayerInfoCommon
        player_info.PlayerPlayState = p_i.PlayerPlayState
        self.MapFriendState[player_info.PlayerInfoCommon.PlayerGuid] = CasinoHelper:TranslateFriendStateEx(player_info)
        local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityRefreshFriendInfo")
        if (ev == nil) then
            ev = EvEntityRefreshFriendInfo:new(nil)
        end
        ev.player_info = player_info
        self.ControllerIM.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
function IMFriendList:OnIMFriendListAddNotify(list_player)
    for key, value in pairs(list_player) do
        local p_i = PlayerInfo:new(nil)
        p_i:setData(value)
        local player_guid = p_i.PlayerInfoCommon.PlayerGuid
        self.MapFriendList[player_guid] = p_i
        local friend_state = CasinoHelper:TranslateFriendStateEx(p_i)
        self.MapFriendState[player_guid] = friend_state
        local contains = false
        for key1, value1 in pairs(self.ListFriendGuid) do
            if (value1 == player_guid) then
                contains = true
                break
            end
        end
        if (contains == false) then
            table.insert(self.ListFriendGuid, player_guid)
        end
        self.MapFriendRecordIsChecked[player_guid] = false
        self.ControllerIM.IMChat:loadPlayerChatMsgRecv(player_guid)
        if (p_i.PlayerInfoMore.OnlineState == PlayerOnlineState.Offline) then
            local contains2 = false
            for key1, value1 in pairs(self.ListOffLineFriendGuid) do
                if (value1 == player_guid) then
                    contains2 = true
                    break
                end
            end
            if (contains2 == false) then
                table.insert(self.ListOffLineFriendGuid, player_guid)
            end
        else
            local contains3 = false
            for key1, value1 in pairs(self.ListOffLineFriendGuid) do
                if (value1 == player_guid) then
                    contains3 = true
                    break
                end
            end
            if (contains3 == false) then
                table.insert(self.ListOnLineFriendGuid, player_guid)
            end
        end
    end

    self.ControllerIM.IMChat:sortChatPlayerList()
    local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityRefreshFriendList")
    if (ev == nil) then
        ev = EvEntityRefreshFriendList:new(nil)
    end
    ev.map_friendinfo = self.MapFriendList
    self.ControllerIM.ViewMgr:SendEv(ev)
end

---------------------------------------
function IMFriendList:OnIMFriendListRemoveNotify(player_guid)
    self.MapFriendRecordIsChecked[player_guid] = nil
    self.MapFriendState[player_guid] = nil
    local k_f
    for key, value in pairs(self.ListFriendGuid) do
        if (value == player_guid) then
            k_f = key
        end
    end
    if k_f ~= nil then
        self.ListFriendGuid[k_f] = nil
    end
    local k_o
    for key, value in pairs(self.ListOnLineFriendGuid) do
        if (value == player_guid) then
            k_o = key
        end
    end
    if k_o ~= nil then
        self.ListOnLineFriendGuid[k_o] = nil
    end
    local k_off
    for key, value in pairs(self.ListOffLineFriendGuid) do
        if (value == player_guid) then
            k_off = key
        end
    end
    if k_off ~= nil then
        self.ListOffLineFriendGuid[k_off] = nil
    end
    if (self.MapFriendList[player_guid] ~= nil) then
        self.MapFriendList[player_guid] = nil
        ViewHelper:UiShowInfoSuccess(self.ControllerIM.ControllerMgr.LanMgr:getLanValue("DeleteFriendSuccess"))
        self.ControllerIM.IMChat:deletePlayerChatRecord(player_guid)
        local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityNotifyDeleteFriend")
        if (ev == nil) then
            ev = EvEntityNotifyDeleteFriend:new(nil)
        end
        ev.map_friend = self.MapFriendList
        ev.friend_etguid = player_guid
        self.ControllerIM.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
function IMFriendList:OnIMFindFriendNotify(list_player)
    local l_p = {}
    for i, v in pairs(list_player) do
        local p_i = PlayerInfo:new(nil)
        p_i:setData(v)
        table.insert(l_p, p_i)
    end
    local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityFindFriend")
    if (ev == nil) then
        ev = EvEntityFindFriend:new(nil)
    end
    ev.list_friend_item = l_p
    self.ControllerIM.ViewMgr:SendEv(ev)
end

---------------------------------------
function IMFriendList:OnIMRecommandFriendNotify(list_player)
    if (list_player ~= nil) then
        self.ListRecommendPlayer = {}
        for key, value in pairs(list_player) do
            local p_i = PlayerInfo:new(nil)
            p_i:setData(value)
            table.insert(self.ListRecommendPlayer, p_i)
        end
    end
    local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityRecommendPlayerList")
    if (ev == nil) then
        ev = EvEntityRecommendPlayerList:new(nil)
    end
    ev.list_recommend = self.ListRecommendPlayer
    self.ControllerIM.ViewMgr:SendEv(ev)
end

---------------------------------------
function IMFriendList:OnIMEventPush2ClientNotify(ev)
    local i_e = IMOfflineEvent:new(nil)
    i_e:setData(ev)
    if (i_e.EvId == IMOfflineEventId.AddFriendRequest) then
        local view_friendRequest = self.ControllerIM.ViewMgr:CreateView("AgreeOrDisAddFriendRequest")
        view_friendRequest:addFriend(i_e)
    end
end

---------------------------------------
function IMFriendList:OnIMFriendGoldUpdate(gold_update)
    local gold_data = BFriendGoldUpdate:new(nil)
    gold_data:setData(gold_update)
    local friend_guid = gold_data.FriendGuid
    local friend_info = self.MapFriendList[friend_guid]
    if friend_info ~= nil then
        friend_info.PlayerInfoMore.Gold = gold_data.GoldAcc
        local ev = self.ControllerIM.ViewMgr:GetEv("EvEntityFriendGoldChange")
        if (ev == nil) then
            ev = EvEntityFriendGoldChange:new(nil)
        end
        ev.friend_guid = friend_guid
        ev.current_gold = gold_data.GoldAcc
        self.ControllerIM.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
function IMFriendList:RequestIMAddFriendReq(player_guid)
    self.ControllerIM.ControllerMgr.RPC:RPC1(CommonMethodType.IMAddFriendReqRequest, player_guid)
end

---------------------------------------
function IMFriendList:RequestIMAddFriendRes(player_guid, result)
    self.ControllerIM.ControllerMgr.RPC:RPC2(CommonMethodType.IMAddFriendResRequest, player_guid, result)
end

---------------------------------------
function IMFriendList:RequestIMDeleteFriend(player_guid)
    self.ControllerIM.ControllerMgr.RPC:RPC1(CommonMethodType.IMDeleteFriendRequest, player_guid)
end

---------------------------------------
function IMFriendList:RequestIMFindFriend(search_filter)
    self.ControllerIM.ControllerMgr.RPC:RPC1(CommonMethodType.IMFindFriendRequest, search_filter)
end

---------------------------------------
function IMFriendList:getInDesktopFriendList(friend_state)
    self.ListPlayInDesktopFriend = {}
    for key, value in pairs(self.MapFriendState) do
        if (value == friend_state) then
            local friend_info = self:getFriendInfo(key)
            table.insert(self.ListPlayInDesktopFriend, friend_info)
        end
    end
    return self.ListPlayInDesktopFriend
end

---------------------------------------
function IMFriendList:getFriendRecordIsChecked(friend_guid)
    local friend_ischeked = false
    if (self.MapFriendRecordIsChecked[friend_guid] ~= nil) then
        friend_ischeked = self.MapFriendRecordIsChecked[friend_guid]
    end
    return friend_ischeked
end

---------------------------------------
function IMFriendList:getFriendInfo(friend_guid)
    local player_info = self.MapFriendList[friend_guid]
    return player_info
end

---------------------------------------
function IMFriendList:getFriendState(friend_guid)
    local state = _eFriendStateClient.Offline
    if (self.MapFriendState[friend_guid] ~= nil) then
        state = self.MapFriendState[friend_guid]
    end
    return state
end

---------------------------------------
function IMFriendList:getFriendStateStr(friend_guid)
    local state = _eFriendStateClient.Offline
    if (self.MapFriendState[friend_guid] ~= nil) then
        state = self.MapFriendState[friend_guid]
    end
    local state_str = CasinoHelper:TranslateFriendState(state)

    return state_str
end

---------------------------------------
function IMFriendList:setFriendRecordIsChecked(friend_guid)
    self.MapFriendRecordIsChecked[friend_guid] = true
end