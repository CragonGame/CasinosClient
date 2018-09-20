-- Copyright(c) Cragon. All rights reserved.

BagType = {
    None = 0, -- 无背包，目前魔法表情，充值道具是不进入背包的，无需进行背包判满
    Bag = 1, -- 普通背包，需要判满
    GiftTmp = 2, -- 临时礼物背包，等价于无限空间，无需判满
}

ItemOperate = {}

function ItemOperate:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.operate_id = nil
    o.item_objid = nil

    return o
end

function ItemOperate:getData4Pack(data)
    local temp = {}
    table.insert(temp, self.operate_id)
    table.insert(temp, self.item_objid)

    return temp
end

ItemOperateNotifyData = {}
function ItemOperateNotifyData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.result = 0
    o.operate_id = nil
    o.item_id = 0
    o.item_objid = nil
    o.overlap_num = 0

    return o
end

function ItemOperateNotifyData:setData(data)
    self.result = data[1]
    self.operate_id = data[2]
    self.item_id = data[3]
    self.item_objid = data[4]
    self.overlap_num = data[5]
end