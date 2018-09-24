-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
Unit = {
    UnitType = nil,
    Item = nil,
    SenderGuid = nil,
    SenderNickName = nil,
}

---------------------------------------
function Unit:new(o, item)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Item = item

    return o
end

---------------------------------------
UnitFac = {}

---------------------------------------
function UnitFac:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    return o
end

---------------------------------------
function UnitFac:createUnit(item)
end

---------------------------------------
UnitSys = {}

---------------------------------------
function UnitSys:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil)
    then
        self.Instance = o
        self.MapUnitFac = {}
        self.MapUnitFac["GiftTmp"] = UnitFacGiftTmp:new(nil)
        self.MapUnitFac["GiftNormal"] = UnitFacGiftNormal:new(nil)
        self.MapUnitFac["Billing"] = UnitFacBilling:new(nil)
        self.MapUnitFac["Consume"] = UnitFacConsume:new(nil)
        self.MapUnitFac["MagicExpression"] = UnitFacMagicExpression:new(nil)
        self.MapUnitFac["GoodsVoucher"] = UnitFacGoodsVoucher:new(nil)
        self.MapUnitFac["GoldPackage"] = UnitFacGoldPackage:new(nil)
        self.MapUnitFac["WechatRedEnvelopes"] = UnitFacRedEnvelopes:new(nil)
    end

    return self.Instance
end

---------------------------------------
function UnitSys:createUnit(unit_name, item)
    local fac = self.MapUnitFac[unit_name]
    local l = fac:createUnit(item)
    return l
end