-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
IMMarqueeSenderType = {
    System = 0,
    Player = 1
}

IMMarqueePriority = {
    Normal = 0,
    High = 1
}

---------------------------------------
BIMMarquee = {}

function BIMMarquee:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.SenderType = 0
    o.SenderGuid = nil
    o.NickName = nil
    o.VIPLevel = 0
    o.Priority = 0
    o.Msg = nil
    return o
end

function BIMMarquee:setData(data)
    self.SenderType = data[1]
    self.SenderGuid = data[2]
    self.NickName = data[3]
    self.VIPLevel = data[4]
    self.Priority = data[5]
    self.Msg = data[6]
end

function BIMMarquee:getData4Pack()
    local temp = {}
    table.insert(temp, self.SenderType)
    table.insert(temp, self.SenderGuid)
    table.insert(temp, self.NickName)
    table.insert(temp, self.VIPLevel)
    table.insert(temp, self.Priority)
    table.insert(temp, self.Msg)
    return temp
end