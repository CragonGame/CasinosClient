-- Copyright(c) Cragon. All rights reserved.
-- 私聊消息记录，不持有数据，由IMChat持有数据
-- 保存，Append追加一条保存

IMChatRecord = {}

function IMChatRecord:new(o,co_im)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    self.ChatRecordPlayerPrefsTitle = "IMChat_"
    self.ChatRecordLast = "ChatRecordLast"
    self.ChatRecordCount = "ChatRecordCount"
    self.ChatIndex = "ChatIndex"
    self.ControllerIM = co_im
    self.PlayerPrefs = CS.UnityEngine.PlayerPrefs
    return o
end

-- 加载指定玩家的聊天记录
function IMChatRecord:loadPlayerChatRecordFromPlayerPrefs(player_guid)
    local playerprefs_chatrecordindex_key = self:getChatRecordLastMsgIdKey(player_guid)
    local need_reload_record = false
    local index = 0
    if (self.PlayerPrefs.HasKey(playerprefs_chatrecordindex_key) == false)
    then
        return nil,need_reload_record,index
    end

    local chatrecord_count_key = self:getChatRecordCountKey(player_guid)
    if (self.PlayerPrefs.HasKey(chatrecord_count_key) == false)
    then
        return nil,need_reload_record,index
    end

    local chat_indexkey = self:getChatIndexKey(player_guid)
    if (self.PlayerPrefs.HasKey(chat_indexkey))
    then
        index = self.PlayerPrefs.GetInt(chat_indexkey)
    end

    local player_chatrecord_max_index = self.PlayerPrefs.GetInt(playerprefs_chatrecordindex_key)
    local player_chatrecord_count = self.PlayerPrefs.GetInt(chatrecord_count_key)

    local player_chatrecord_min_index = player_chatrecord_max_index + 1 - player_chatrecord_count
    if (player_chatrecord_min_index < 0)
    then
        player_chatrecord_min_index = 0
    end

    local list_record = {}
    for i = player_chatrecord_max_index,player_chatrecord_min_index,-1 do
        local playerprefs_chatrecord_key = self:getChatRecordKey(player_guid, i)
        if (self.PlayerPrefs.HasKey(playerprefs_chatrecord_key))
        then
            local chat_record_s = self.PlayerPrefs.GetString(playerprefs_chatrecord_key)
            local chat_record_b = CS.System.Convert.FromBase64String(chat_record_s)
            local record = self.ControllerIM.ControllerMgr:unpackData(chat_record_b)
            if (record ~= nil)
            then
                table.insert(list_record,1,record)
            end
        else
            need_reload_record = true
            break
        end
    end

    return list_record,need_reload_record,index
end

-- 保存单条消息
function IMChatRecord:saveRecordToPlayerPrefs1(player_guid,record)
    self:saveChatRecord(player_guid, record)
    self:saveLastChatRecordId(player_guid, record.msg_id)
end

-- refresh_allrecord，true=删除本地后保存，false=追加保存
function IMChatRecord:saveRecordToPlayerPrefs2(player_guid,list_record,refresh_allrecord)
    if (list_record == nil or #list_record == 0)
    then
        return
    end
    if (refresh_allrecord)
    then
        local playerprefs_chatrecordindex_key = self:getChatRecordLastMsgIdKey(player_guid)
        if (self.PlayerPrefs.HasKey(playerprefs_chatrecordindex_key))
        then
            self.PlayerPrefs.DeleteKey(playerprefs_chatrecordindex_key)
        end

        local chatrecord_count_key = self:getChatRecordCountKey(player_guid)
        if (self.PlayerPrefs.HasKey(chatrecord_count_key))
        then
            self.PlayerPrefs.DeleteKey(chatrecord_count_key)
        end
    end
    for key,value in pairs(list_record) do
        self:saveChatRecord(player_guid,value)
    end

    local last_record = list_record[#list_record]
    self:saveLastChatRecordId(player_guid,last_record.msg_id)
end

-- 删除指定玩家的一条记录
function IMChatRecord:deleteChatRecord(player_guid,record)
    local playerprefs_chatrecord_key = self:getChatRecordKey(player_guid,record.msg_id)
    if (self.PlayerPrefs.HasKey(playerprefs_chatrecord_key))
    then
        self.PlayerPrefs.DeleteKey(playerprefs_chatrecord_key)
    end

    local chatrecord_count_key = self:getChatRecordCountKey(player_guid)
    local chat_count = self.PlayerPrefs.GetInt(chatrecord_count_key, 0)
    chat_count = chat_count - 1
    if (chat_count < 0)
    then
        chat_count = 0
    end
    self.PlayerPrefs.SetInt(chatrecord_count_key, chat_count)
end

-- 删除指定玩家的所有记录
function IMChatRecord:deleteChatRecord1(player_guid)
    local player_chatrecord_max_index = -1
    local playerprefs_chatrecordindex_key = self:getChatRecordLastMsgIdKey(player_guid)
    if (self.PlayerPrefs.HasKey(playerprefs_chatrecordindex_key))
    then
        player_chatrecord_max_index = self.PlayerPrefs.GetInt(playerprefs_chatrecordindex_key)
        self.PlayerPrefs.DeleteKey(playerprefs_chatrecordindex_key)
    end

    local chatrecord_count_key = self:getChatRecordCountKey(player_guid)
    if (self.PlayerPrefs.HasKey(chatrecord_count_key))
    then
        self.PlayerPrefs.DeleteKey(chatrecord_count_key)
    end

    local chat_indexkey = self:getChatIndexKey(player_guid)
    if (self.PlayerPrefs.HasKey(chat_indexkey))
    then
        self.PlayerPrefs.DeleteKey(chat_indexkey)
    end

    if (player_chatrecord_max_index >= 0)
    then
        local index = player_chatrecord_max_index
        while (true)
        do
            local playerprefs_chatrecord_key = self:getChatRecordKey(player_guid, index)
            if (self.PlayerPrefs.HasKey(playerprefs_chatrecord_key))
            then
                self.PlayerPrefs.DeleteKey(playerprefs_chatrecord_key)
                index = index - 1
            else
                break
            end
        end
    end
end

-- 保存指定玩家聊天消息的最新索引
function IMChatRecord:saveChatIndex(player_guid,index)
    local chat_indexkey = self:getChatIndexKey(player_guid)
    self.PlayerPrefs.SetInt(chat_indexkey, index)
end

function IMChatRecord:saveChatRecord(player_guid,record)
    local playerprefs_chatrecord_key = self:getChatRecordKey(player_guid,record.msg_id)
    local p_record = self.ControllerIM.ControllerMgr:packData(record)
    local chat_record_s = CS.System.Convert.ToBase64String(p_record)
    self.PlayerPrefs.SetString(playerprefs_chatrecord_key, chat_record_s)

    local chatrecord_count_key = self:getChatRecordCountKey(player_guid)
    local chat_count = self.PlayerPrefs.GetInt(chatrecord_count_key, 0)
    chat_count = chat_count + 1
    self.PlayerPrefs.SetInt(chatrecord_count_key, chat_count)
end

function IMChatRecord:saveLastChatRecordId(player_guid,msg_id)
    local playerprefs_chatrecordindex_key = self:getChatRecordLastMsgIdKey(player_guid)
    self.PlayerPrefs.SetInt(playerprefs_chatrecordindex_key, msg_id)
end

function IMChatRecord:getChatRecordLastMsgIdKey(friend_guid)
    CS.Casinos.CasinosContext.Instance:ClearSB()
    local sb = CS.Casinos.CasinosContext.Instance.SB
    sb:Append(self.ChatRecordPlayerPrefsTitle)
    sb:Append("_")
    sb:Append(self.ControllerIM.Guid)
    sb:Append("_")
    sb:Append(friend_guid)
    sb:Append("_")
    sb:Append(self.ChatRecordLast)
    return sb:ToString()
end

function IMChatRecord:getChatRecordCountKey(friend_guid)
    CS.Casinos.CasinosContext.Instance:ClearSB()
    local sb = CS.Casinos.CasinosContext.Instance.SB
    sb:Append(self.ChatRecordPlayerPrefsTitle)
    sb:Append("_")
    sb:Append(self.ControllerIM.Guid)
    sb:Append("_")
    sb:Append(friend_guid)
    sb:Append("_")
    sb:Append(self.ChatRecordCount)
    return sb:ToString()
end

function IMChatRecord:getChatRecordKey(friend_guid,index)
    CS.Casinos.CasinosContext.Instance:ClearSB()
    local sb = CS.Casinos.CasinosContext.Instance.SB
    sb:Append(self.ChatRecordPlayerPrefsTitle)
    sb:Append("_")
    sb:Append(self.ControllerIM.Guid)
    sb:Append("_")
    sb:Append(friend_guid)
    sb:Append("_")
    sb:Append(index)
    return sb:ToString()
end

function IMChatRecord:getChatIndexKey(friend_guid)
    CS.Casinos.CasinosContext.Instance:ClearSB()
    local sb = CS.Casinos.CasinosContext.Instance.SB
    sb:Append(self.ChatRecordPlayerPrefsTitle)
    sb:Append("_")
    sb:Append(self.ControllerIM.Guid)
    sb:Append("_")
    sb:Append(friend_guid)
    sb:Append("_")
    sb:Append(self.ChatIndex)
    return sb:ToString()
end
