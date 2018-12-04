-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UnitGiftNormal = Unit:new()

---------------------------------------
function UnitGiftNormal:new(o, item)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.UnitType = "GiftNormal"
    o.Item = item
    o.GiveBy = nil
    o.GivePlayerEtGuid = nil
    o.CreateTime = nil
    if (o.Item.ItemData.map_unit_data == nil or o.Item.ItemData.map_unit_data.Count == 0) then
        o.CreateTime = CS.System.DateTime.UtcNow
        o:_saveData()
    end
    o:_setup()
    return o
end

---------------------------------------
function UnitGiftNormal:_setup()
    local nick_name = self.Item.ItemData.map_unit_data[1]
    if (CS.System.String.IsNullOrEmpty(nick_name) == false) then
        self.GiveBy = nick_name
    end

    local et_guid = self.Item.ItemData.map_unit_data[2]
    if (CS.System.String.IsNullOrEmpty(et_guid) == false) then
        self.GivePlayerEtGuid = et_guid
    end

    local create_time = self.Item.ItemData.map_unit_data[3]
    if (CS.System.String.IsNullOrEmpty(create_time) == false) then
        self.CreateTime = CS.System.DateTime.Parse(create_time)
    end
end

---------------------------------------
function UnitGiftNormal:_saveData()
    if (self.Item.ItemData.map_unit_data == nil) then
        self.Item.ItemData.map_unit_data = CS.Casinos.LuaHelper.GetNewStringStringMap()
    end
    self.Item.ItemData.map_unit_data[1] = self.GiveBy
    self.Item.ItemData.map_unit_data[2] = self.GivePlayerEtGuid
    self.Item.ItemData.map_unit_data[3] = self.CreateTime:ToString()
end

---------------------------------------
UnitFacGiftNormal = UnitFac:new()

---------------------------------------
function UnitFacGiftNormal:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function UnitFacGiftNormal:createUnit(item)
    return UnitGiftNormal:new(nil, item)
end