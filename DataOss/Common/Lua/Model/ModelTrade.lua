-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
BuyItemForTarget = {
    Me = 0, -- 本人
    OtherPlayer = 1, -- 他人（单人）
    DesktopOtherPlayer = 2, -- 普通桌上他人（单人）
    DesktopAllPlayer = 3, -- 普通桌上全体玩家
    DesktopHOtherPlayer = 4, -- 百人桌上他人（单人）
    DesktopHAllPlayer = 5, -- 百人桌上全体玩家
}

PriceType = {
    None = 0, --无效
    Chip = 1,
    Gold = 2,
    Money = 3,
    Point = 4,
}

---------------------------------------
BuyItemRequest = {}

function BuyItemRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.buyitem_tbid = 0
    o.target_type = 0
    o.target_etguid = nil

    return o
end

function BuyItemRequest:getData4Pack()
    local t = {}
    table.insert(t, self.buyitem_tbid)
    table.insert(t, self.target_type)
    table.insert(t, self.target_etguid)

    return t
end

---------------------------------------
BuyItemResponse = {}

function BuyItemResponse:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.result = 0
    o.buyitem_tbid = 0

    return o
end

function BuyItemResponse:setData(data)
    self.result = data[1]
    self.buyitem_tbid = data[2]
end

---------------------------------------
SellItemResponse = {}

function SellItemResponse:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.result = 0
    o.item_name = nil
    o.price_type = 0
    o.price = 0

    return o
end

function SellItemResponse:setData(data)
    self.result = data[1]
    self.item_name = data[2]
    self.price_type = data[3]
    self.price = data[4]
end

---------------------------------------
BuyRMBItemSuccessResponse = {}

function BuyRMBItemSuccessResponse:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.result = 0

    return o
end

function BuyRMBItemSuccessResponse:setData(data)
    self.result = data[1]
end

---------------------------------------
PurchaseCommon = {}

function PurchaseCommon:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.OrderId = nil
    o.PackageName = nil
    o.Sku = nil
    o.PurchaseTime = 0
    o.PurchaseState = 0
    o.Token = nil
    o.Receipt = nil
    return o
end

function PurchaseCommon:getData4Pack()
    local t = {}
    table.insert(t, self.OrderId)
    table.insert(t, self.PackageName)
    table.insert(t, self.Sku)
    table.insert(t, self.PurchaseTime)
    table.insert(t, self.PurchaseState)
    table.insert(t, self.Token)
    table.insert(t, self.Receipt)
    return t
end

---------------------------------------
PlayerAddress = {}

function PlayerAddress:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Name = nil
    o.PhoneNum = nil
    o.QQ = nil
    o.Weixin = 0
    o.Address = 0
    o.EMail = nil
    return o
end

-- 玩家收货地址，用于快递实物
function PlayerAddress:setData(data)
    local t = {}
    self.Name = data[1]
    self.PhoneNum = data[2]
    self.QQ = data[3]
    self.Weixin = data[4]
    self.Address = data[5]
    self.EMail = data[6]
    return t
end

function PlayerAddress:getData4Pack()
    local t = {}
    table.insert(t, self.Name)
    table.insert(t, self.PhoneNum)
    table.insert(t, self.QQ)
    table.insert(t, self.Weixin)
    table.insert(t, self.Address)
    table.insert(t, self.EMail)
    return t
end