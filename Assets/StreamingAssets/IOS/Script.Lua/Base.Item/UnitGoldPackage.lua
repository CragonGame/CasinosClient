-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UnitGoldPackage = Unit:new()

---------------------------------------
function UnitGoldPackage:new(o, item)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.UnitType = "GoldPackage"
    o.Item = item
    o.CreateTime = nil
    if (o.Item.ItemData.map_unit_data == nil or o.Item.ItemData.map_unit_data.Count == 0)
    then
        o.CreateTime = CS.System.DateTime.UtcNow
        o:_saveData()
    end

    o:_setup()

    return o
end

---------------------------------------
function UnitGoldPackage:_setup()
    local create_time = self.Item.ItemData.map_unit_data[1]
    if (CS.System.String.IsNullOrEmpty(create_time) == false)
    then
        self.CreateTime = CS.Casinos.LuaHelper.JsonDeserializeDateTime(create_time)
    end
end

---------------------------------------
function UnitGoldPackage:_saveData()
    if (self.Item.ItemData.map_unit_data == nil)
    then
        self.Item.ItemData.map_unit_data = CS.Casinos.LuaHelper.GetNewStringStringMap()
    end
    self.Item.ItemData.map_unit_data[1] = CS.Casinos.EbTool.jsonSerialize(self.CreateTime)
end

---------------------------------------
UnitFacGoldPackage = UnitFac:new()

---------------------------------------
function UnitFacGoldPackage:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    return o
end

---------------------------------------
function UnitFacGoldPackage:createUnit(item)
    return UnitGoldPackage:new(nil, item)
end