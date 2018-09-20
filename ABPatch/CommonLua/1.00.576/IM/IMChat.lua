IMChat = {}

function IMChat:new(o,co_im)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    self.MaxCacheChatCount = 100
    self.PlayerPrefChatTitle = "PlayerChat_"
    self.ControllerIM = co_im
    self.MapChats = {}
    self.MapChatsShow = {}
    self.MapUnReadChats = {}
    self.ListChatTarget = {}
    self.MapNeedLoadChatRecordPlayer = {}
    self.MapChatIndex = {}
    self.MapChatIndexTmp = {}
    return o
end

function IMChat:OnIMChatRecvMsgNotify(msg)
    local self_guid = self.ControllerIM.Guid
    local c_msg = ChatMsgClientRecv:new(nil)
    c_msg:setData(msg)
    if(c_msg.sender_guid ~= self_guid  and c_msg.recver_guid ~= self_guid)
    then
        return
    end
    local player_guid = c_msg.sender_guid
    if (c_msg.sender_guid == self_guid)
    then
        player_guid = c_msg.recver_guid
    end
    self:recvMsg(self.MapUnReadChats, c_msg, player_guid, true)
    self:recvMsg(self.MapChats, c_msg, player_guid, false)
    self:sortChatPlayerList()
end

function IMChat:OnIMChatRecvBatchMsgNotify(list_msg)
    if (list_msg == nil or #list_msg == 0)
    then
        return
    end

    --local t_msg = {}
    --for i, v in pairs(list_msg) do
    --	local c_msg = ChatMsgClientRecv:new(nil)
    --	c_msg:setData(v)
    --	table.insert(t_msg,c_msg)
    --end
    local first_record = list_msg[1]
    local self_guid = self.ControllerIM.Guid
    if(first_record.sender_guid ~= self_guid  and first_record.recver_guid ~= self_guid)
    then
        return
    end

    local player_guid = first_record.sender_guid
    if (first_record.sender_guid == self_guid)
    then
        player_guid = first_record.recver_guid
    end

    self:recvMsgs(self.MapUnReadChats, list_msg, player_guid, true, false)
    self:recvMsgs(self.MapChats, list_msg, player_guid, false, false)
    self:sortChatPlayerList()
end

function IMChat:OnIMChatRecordRequestResult(list_msg)
    if (list_msg == nil or #list_msg == 0)
    then
        return
    end

    local first_record = list_msg[1]
    local player_guid = first_record.sender_guid
    if (first_record.sender_guid == self.ControllerIM.Guid)
    then
        player_guid = first_record.recver_guid
    end
    print(#list_msg)
    local t = LuaHelper:ReverseTable(list_msg)
    print(#t)
    self:recvMsgs(self.MapChats, list_msg, player_guid, false, true)
    self:sortChatPlayerList()

    local list_chatshow = self:getListChatShow(player_guid)
    local ev = self.ControllerIM.ControllerMgr.ViewMgr:getEv("EvEntityChatRecordRequestResult")
    if(ev == nil)
    then
        ev = EvEntityChatRecordRequestResult:new(nil)
    end
    ev.list_allchats = list_chatshow
    ev.friend_etguid = player_guid
    self.ControllerIM.ControllerMgr.ViewMgr:sendEv(ev)
end

function IMChat:requestChatRecord(player_guid,msg_id)
    self.ControllerIM.ControllerMgr.RPC:RPC2(CommonMethodType.IMChatRecordRequest, player_guid,msg_id)
end

function IMChat:requestChatReadConfirm(player_guid,msg_id)
    self.ControllerIM.ControllerMgr.RPC:RPC2(CommonMethodType.IMChatReadConfirmRequest, player_guid,msg_id)
end

function IMChat:requestIMChatSendMsg(chat_msg)
    self.ControllerIM.ControllerMgr.RPC:RPC1(CommonMethodType.IMChatSendMsgRequest, chat_msg)
end

function IMChat:deletePlayerChatRecord(friend_guid)
    self.MapChats[friend_guid] = nil
    self.MapChatsShow[friend_guid] = nil
    local target = nil
    for key,value in pairs(self.ListChatTarget) do
        if(value.FriendGuid == friend_guid)
        then
            target = value
            break
        end
    end
    if(target ~= nil)
    then
        LuaHelper:TableRemoveV(self.ListChatTarget,target)
    end
    self:resortChatList(friend_guid, false)
    self.ControllerIM.IMChatRecord:deleteChatRecord1(friend_guid)
    local ev = self.ControllerIM.ControllerMgr.ViewMgr:getEv("EvEntityDeleteFriendChatRecordSuccess")
    if(ev == nil)
    then
        ev = EvEntityDeleteFriendChatRecordSuccess:new(nil)
    end
    ev.friend_etguid = friend_guid
    self.ControllerIM.ControllerMgr.ViewMgr:sendEv(ev)
end

function IMChat:loadPlayerChatMsgRecv(player_guid)
    local list_record,need_reload,index1 = self.ControllerIM.IMChatRecord:loadPlayerChatRecordFromPlayerPrefs(player_guid)
    if (need_reload == true)
    then
        self.MapNeedLoadChatRecordPlayer[player_guid] = true
    end

    if (list_record ~= nil)
    then
        self.MapChats[player_guid] = list_record
        for key,value in pairs(list_record) do
            self:createChatShowTm(player_guid,value)
        end
        self.MapChatIndex[player_guid] = index1
        self:addAndSortChatTarget(player_guid, list_record[#list_record].dt, true)
        self:sortChatPlayerList()
    end
end

function IMChat:getPlayerChatMsgRecv(player_guid)
    local need_reload_record = false
    local is_checked = self.ControllerIM.IMFriendList:getFriendRecordIsChecked(player_guid)
    local list_msgrecv = nil
    if (is_checked)
    then
        list_msgrecv = self.MapChats[player_guid]
        return list_msgrecv,need_reload_record
    end

    local need_reload = false
    if(self.MapNeedLoadChatRecordPlayer[player_guid] ~= nil)
    then
        need_reload = self.MapNeedLoadChatRecordPlayer[player_guid]
    end
    if (need_reload)
    then
        need_reload_record = true
        self:requestChatRecord(player_guid, 0)
    else
        need_reload_record = false
        list_msgrecv = {}
        if(self.MapChats[player_guid] ~= nil)
        then
            list_msgrecv = self.MapChats[player_guid]
        end
    end

    self.MapNeedLoadChatRecordPlayer[player_guid] = nil
    self.ControllerIM.IMFriendList:setFriendRecordIsChecked(player_guid)

    return list_msgrecv,need_reload_record
end

function IMChat:getListChatShow(friend_guid)
    local list_show = self.MapChatsShow[friend_guid]
    return list_show
end

function IMChat:getLastChatMsgRecord(friend_etguid)
    local list_chats = self.MapChats[friend_etguid]
    local last_record = nil
    if (list_chats ~= nil and #list_chats > 0)
    then
        last_record = list_chats[#list_chats]
    end

    return last_record
end

function IMChat:getFriendNewChatCount(friend_etguid)
    local list_chats = self.MapUnReadChats[friend_etguid]
    if(list_chats == nil)
    then
        return 0
    else
        return #list_chats
    end
end

function IMChat:getAllNewChatCount()
    local temp = 0
    for key,value in pairs(self.MapUnReadChats) do
        temp = temp + #value
    end
    return temp
end

function IMChat:sortChatPlayerList()
    local lua_helper = LuaHelper:new(nil)
    if (lua_helper:GetTableCount(self.MapChatIndex) > 0)
    then
        --[[table.sort(self.MapChatIndex,)

        MapChatIndex = MapChatIndex.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
        MapChats = MapChats.OrderByDescending((x) =>
        {
            int v = 0;
            MapChatIndex.TryGetValue(x.Key, out v);
            return v;
        }).ToDictionary(x => x.Key, y => y.Value);]]
    end
end

function IMChat:sortMapChatTarget()
    table.sort(self.ListChatTarget,
            function(a,b)
                return a.LastChatTm > b.LastChatTm
            end
    )
end

function IMChat:addNewChatTarget(friend_guid)
    local exist = false
    for key,value in pairs(self.ListChatTarget) do
        if(value.FriendGuid == friend_guid)
        then
            exist = true
            break
        end
    end
    if(exist == false)
    then
        local chat_target = ChatTargetSortInfo:new(nil)
        chat_target.FriendGuid = friend_guid
        chat_target.LastChatTm = CS.System.DateTime.UtcNow
        table.insert(self.ListChatTarget,1,chat_target)
    end
end

function IMChat:getHaveChatRecordFriendList()
    local list = {}
    for key,value in pairs(self.MapChats) do
        if(value ~= nil and #value > 0)
        then
            table.insert(list,key)
        end
    end
    return list
end

function IMChat:getChatRecords(friend_guid)
    local list_chats = self.MapChats[friend_guid]
    return list_chats
end

function IMChat:recvMsg(map_records,msg,player_guid,is_unreadmap)
    local list_chatrecv = map_records[player_guid]
    if (list_chatrecv == nil)
    then
        list_chatrecv = {}
        map_records[player_guid] = list_chatrecv
    end
    if (#list_chatrecv >= self.MaxCacheChatCount)
    then
        local record = list_chatrecv[1]
        table.remove(list_chatrecv,1)
        self.ControllerIM.IMChatRecord:deleteChatRecord(player_guid, record)
    end
    table.insert(list_chatrecv,msg)
    if (is_unreadmap)
    then
        return
    else
        self:addAndSortChatTarget(player_guid, msg.dt, true)
        self:createChatShowTm(player_guid, msg)
    end

    self:resortChatList(player_guid, true)
    self.ControllerIM.IMChatRecord:saveRecordToPlayerPrefs1(player_guid,msg)
    local ev = self.ControllerIM.ControllerMgr.ViewMgr:getEv("EvEntityReceiveFriendSingleChat")
    if(ev == nil)
    then
        ev = EvEntityReceiveFriendSingleChat:new(nil)
    end
    ev.chat_msg = msg
    ev.friend_etguid = player_guid
    self.ControllerIM.ControllerMgr.ViewMgr:sendEv(ev)
end

function IMChat:recvMsgs(map_records,list_msg,player_guid,is_unreadmap,refresh_allrecord)
    local list_chatrecv = map_records[player_guid]
    if (list_chatrecv == nil)
    then
        list_chatrecv = {}
        map_records[player_guid] = list_chatrecv
    end

    if (refresh_allrecord)
    then
        list_chatrecv = {}
    end

    for key,value in pairs(list_msg) do
        table.insert(list_chatrecv,value)
    end

    if (#list_chatrecv > self.MaxCacheChatCount)
    then
        local move_length = #list_chatrecv - self.MaxCacheChatCount
        if (is_unreadmap == false)
        then
            for i = 1,move_length do
                self.ControllerIM.IMChatRecord:deleteChatRecord(player_guid, list_chatrecv[i])
            end
        end
        for i = 1,move_length do
            table.remove(list_chatrecv,1)
        end
    end

    if (is_unreadmap)
    then
        return
    else
        self:addAndSortChatTarget(player_guid, list_msg[#list_msg].dt)
        for key,value in pairs(list_msg) do
            self:createChatShowTm(player_guid, value)
        end
    end

    self:resortChatList(player_guid,true)
    if (#list_msg > self.MaxCacheChatCount)
    then
        self.ControllerIM.IMChatRecord:saveRecordToPlayerPrefs2(player_guid, list_chatrecv, refresh_allrecord)
    else
        self.ControllerIM.IMChatRecord:saveRecordToPlayerPrefs2(player_guid, list_msg, refresh_allrecord)
    end

    local list_chatshow = self:getListChatShow(player_guid)
    local ev = self.ControllerIM.ControllerMgr.ViewMgr:getEv("EvEntityReceiveFriendChats")
    if(ev == nil)
    then
        ev = EvEntityReceiveFriendChats:new(nil)
    end
    ev.list_allchats = list_chatshow
    ev.friend_etguid = player_guid
    self.ControllerIM.ControllerMgr.ViewMgr:sendEv(ev)
end

function IMChat:resortChatList(player_guid,is_add)
    if (is_add)
    then
        if (self.MapChatIndex[player_guid] ~= nil)
        then
            local vurrent_index = self.MapChatIndex[player_guid]
            local tmp = {}
            for key,value in pairs(self.MapChatIndex) do
                if(value < vurrent_index)
                then
                    table.insert(tmp,value)
                end
            end
            if (tmp ~= nil and #tmp > 0)
            then
                for key,value in pairs(tmp) do
                    self.MapChatIndexTmp[key] = value
                end
                for key,value in pairs(self.MapChatIndexTmp) do
                    local index = value
                    index = index + 1
                    self.MapChatIndex[key] = index
                end
                self.MapChatIndexTmp = {}
            end

            self.MapChatIndex[player_guid] = 0
        else
            for key,value in pairs(self.MapChatIndex) do
                self.MapChatIndexTmp[key] = value
            end
            for key,value in pairs(self.MapChatIndexTmp) do
                local index = value
                index = index + 1
                self.MapChatIndex[key] = index
            end
            self.MapChatIndexTmp = {}
            self.MapChatIndex[player_guid] = 0
        end
    else
        if (self.MapChatIndex[player_guid] ~= nil)
        then
            local vurrent_index = self.MapChatIndex[player_guid]
            local tmp = {}
            for key,value in pairs(tmp) do
                if(value > vurrent_index)
                then
                    table.insert(tmp,value)
                end
            end
            if (tmp ~= nil and #tmp > 0)
            then
                for key,value in pairs(tmp) do
                    self.MapChatIndexTmp[key] = value
                end
                for key,value in pairs(self.MapChatIndexTmp) do
                    local index = value
                    index = index + 1
                    self.MapChatIndex[key] = index
                end
                self.MapChatIndexTmp = {}
            end
        end
        for key,value in pairs(self.MapChatIndex) do
            if(key == player_guid)
            then
                value = nil
            end
        end
    end
    for key,value in pairs(self.MapChatIndex) do
        self.ControllerIM.IMChatRecord:saveChatIndex(key, value)
    end
end

function IMChat:addAndSortChatTarget(player_guid,dt,need_sort)
    need_sort = false
    local chattarget_sortinfo = nil
    for key,value in pairs(self.ListChatTarget) do
        if(value.FriendGuid == player_guid)
        then
            chattarget_sortinfo = value
        end
    end
    if (chattarget_sortinfo == nil)
    then
        chattarget_sortinfo = ChatTargetSortInfo:new(nil)
        table.insert(self.ListChatTarget,chattarget_sortinfo)
    end
    chattarget_sortinfo.FriendGuid = player_guid
    chattarget_sortinfo.LastChatTm = dt
    if (need_sort)
    then
        self:sortMapChatTarget()
    end
end

function IMChat:createChatShowTm(player_guid,new_chat)
    if (new_chat == nil)
    then
        return
    end

    local list_chatshow = self.MapChatsShow[player_guid]
    if (list_chatshow == nil)
    then
        list_chatshow = {}
        self.MapChatsShow[player_guid] = list_chatshow
    end

    if (#list_chatshow == 0)
    then
        local client_show = ChatMsgClientShow:new(nil)
        client_show.dt = new_chat.dt
        client_show.is_tm = true
        table.insert(list_chatshow,client_show)
    else
        local last_chat = list_chatshow[#list_chatshow]
        local l_tm = CS.System.DateTime.Parse(last_chat.dt)
        local n_tm = CS.System.DateTime.Parse(new_chat.dt)
        local is_same_minute = CS.Casinos.UiHelper.timeIsSameMinute(l_tm,n_tm)
        if (is_same_minute == false)
        then
            local client_show = ChatMsgClientShow:new(nil)
            client_show.dt = new_chat.dt
            client_show.is_tm = true
            table.insert(list_chatshow,client_show)
        end
    end
    local real_chat = self:transationChat(new_chat)
    table.insert(list_chatshow,real_chat)
end

function IMChat:transationChat(chat)
    local chat_show = ChatMsgClientShow:new(nil)
    chat_show.dt = chat.dt
    chat_show.msg_id = chat.msg_id
    chat_show.sender_guid = chat.sender_guid
    chat_show.sender_nickname = chat.sender_nickname
    chat_show.sender_viplevel = chat.sender_viplevel
    chat_show.recver_guid = chat.recver_guid
    chat_show.recver_nickname = chat.recver_nickname
    chat_show.recver_viplevel = chat.recver_viplevel
    chat_show.msg = chat.msg
    chat_show.dt = chat.dt
    chat_show.is_tm = false
    return chat_show
end


ChatTargetSortInfo = {
    FriendGuid = nil,
    LastChatTm = nil
}

function ChatTargetSortInfo:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    return o
end

ChatMsgClientShow = {
    msg_id,
    sender_guid,
    sender_nickname,
    sender_viplevel,
    recver_guid,
    recver_nickname,
    recver_viplevel,
    msg,
    dt,
    is_tm
}

function ChatMsgClientShow:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    return o
end