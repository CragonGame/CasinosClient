-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UnitConsume = Unit:new()

---------------------------------------
function UnitConsume:new(o, item)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.UnitType = "Consume"
    o.Item = item
    o.GiveBy = ""
    o.GiveEtGuid = nil
    o.CreateTime = nil
    if (o.Item.ItemData.map_unit_data == nil or o.Item.ItemData.map_unit_data.Count == 0) then
        o.CreateTime = CS.System.DateTime.UtcNow
        o:_saveData()
    end
    o:_setup()
    return o
end

---------------------------------------
function UnitConsume:_setup()
    local nick_name = self.Item.ItemData.map_unit_data[1]
    if (CS.System.String.IsNullOrEmpty(nick_name) == false) then
        self.GiveBy = nick_name
    end

    local et_guid = self.Item.ItemData.map_unit_data[2]
    if (CS.System.String.IsNullOrEmpty(et_guid) == false) then
        self.GiveEtGuid = et_guid
    end

    local create_time = self.Item.ItemData.map_unit_data[3]
    if (CS.System.String.IsNullOrEmpty(create_time) == false) then
        self.CreateTime = CS.System.DateTime.Parse(create_time)
    end
end

---------------------------------------
function UnitConsume:_saveData()
    if (self.Item.ItemData.map_unit_data == nil)
    then
        self.Item.ItemData.map_unit_data = CS.Casinos.LuaHelper.GetNewStringStringMap()
    end
    self.Item.ItemData.map_unit_data[1] = self.GiveBy
    self.Item.ItemData.map_unit_data[2] = self.GiveEtGuid
    self.Item.ItemData.map_unit_data[3] = self.CreateTime:ToString()
end

---------------------------------------
UnitFacConsume = UnitFac:new()

---------------------------------------
function UnitFacConsume:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    return o
end

---------------------------------------
function UnitFacConsume:createUnit(item)
    return UnitConsume:new(nil, item)
end