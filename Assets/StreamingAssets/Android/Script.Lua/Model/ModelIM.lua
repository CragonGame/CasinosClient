-- Copyright(c) Cragon. All rights reserved.

IMResult = {
    Success = 0, -- 通用，成功
    Failed = 1, -- 失败ji
    Exist = 2, -- 已存在
    Timeout = 3, -- 超时
    DbError = 4, -- 通用，数据库内部错误

    FriendExist = 5, -- 好友已存在
    FriendNotExist = 6, -- 好友不存在
    AddFriendCanntAddSelf = 7, -- 不可以添加自己为好友
    AddFriendInBlackList = 8, -- 添加了在黑名单中的好友
    AddFriendEventExist = 9, -- 添加好友事件，已存在
    AddFriendEventNotExist = 10, -- 添加好友事件，不存在
}

AddFriendResult = {
    None = 0, -- 未处理
    Ignore = 1, -- 忽略处理
    Agree = 2, -- 同意处理
    Reject = 3,-- 拒绝处理
}

ChatMsg = {}
function ChatMsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.sender_guid = nil
    o.sender_nickname = nil
    o.sender_viplevel = 0
    o.recver_guid = nil
    o.recver_nickname = nil
    o.recver_viplevel = 0
    o.msg = nil

    return o
end

function ChatMsg:setData(data)
    self.sender_guid = data[1]
    self.sender_nickname = data[2]
    self.sender_viplevel = data[3]
    self.recver_guid = data[4]
    self.recver_nickname = data[5]
    self.recver_viplevel = data[6]
    self.msg = data[7]
end

function ChatMsg:getData4Pack()
    local t = {}
    table.insert(t, self.sender_guid)
    table.insert(t, self.sender_nickname)
    table.insert(t, self.sender_viplevel)
    table.insert(t, self.recver_guid)
    table.insert(t, self.recver_nickname)
    table.insert(t, self.recver_viplevel)
    table.insert(t, self.msg)
    return t
end

ChatTextInfo = {}
function ChatTextInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.sender_etguid = nil
    o.sender_name = nil
    o.sender_viplevel = 0
    o.chat_content = nil

    return o
end

ChatMsgClientRecv = {}
function ChatMsgClientRecv:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.msg_id = 0
    o.sender_guid = nil
    o.sender_nickname = nil
    o.sender_viplevel = 0
    o.recver_guid = 0
    o.recver_nickname = nil
    o.recver_viplevel = nil
    o.msg = nil
    o.dt = nil

    return o
end

function ChatMsgClientRecv:setData(data)
    self.msg_id = data[1]
    self.sender_guid = data[2]
    self.sender_nickname = data[3]
    self.sender_viplevel = data[4]
    self.recver_guid = data[5]
    self.recver_nickname = data[6]
    self.recver_viplevel = data[7]
    self.msg = data[8]
    self.dt = data[9]
end

function ChatMsgClientRecv:getData4Pack()
    local temp = {}
    table.insert(temp, self.msg_id)
    table.insert(temp, self.sender_guid)
    table.insert(temp, self.sender_nickname)
    table.insert(temp, self.sender_viplevel)
    table.insert(temp, self.recver_guid)
    table.insert(temp, self.recver_nickname)
    table.insert(temp, self.recver_viplevel)
    table.insert(temp, self.msg)
    table.insert(temp, self.dt)

    return temp
end

BFriendGoldUpdate = {}
function BFriendGoldUpdate:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.FriendGuid = 0
    o.GoldAcc = nil

    return o
end

function BFriendGoldUpdate:setData(data)
    self.FriendGuid = data[1]
    self.GoldAcc = data[2]
end

function BFriendGoldUpdate:getData4Pack()
    local temp = {}
    table.insert(temp, self.FriendGuid)
    table.insert(temp, self.GoldAcc)

    return temp
end