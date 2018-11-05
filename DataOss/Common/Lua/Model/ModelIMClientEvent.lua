-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
IMOfflineEventId = {
    AddFriendRequest = 0, -- 事件，收到对方添加好友请求
}

---------------------------------------
IMOfflineEvent = {}

function IMOfflineEvent:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o._id = 0
    o.EvId = nil
    o.Dt = nil
    o.MapData = 0
    return o
end

function IMOfflineEvent:setData(data)
    self._id = data[1]
    self.EvId = data[2]
    self.Dt = data[3]
    self.MapData = data[4]
end

function IMOfflineEvent:getData4Pack()
    local temp = {}
    table.insert(temp, self._id)
    table.insert(temp, self.EvId)
    table.insert(temp, self.Dt)
    table.insert(temp, self.MapData)
    return temp
end