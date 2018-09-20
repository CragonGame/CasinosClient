UnitRedEnvelopes = Unit:new()

function UnitRedEnvelopes:new(o,item)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    o.UnitType = "WechatRedEnvelopes"
    o.Item = item
    o.CreateTime = nil
    o.Value = 0
    if (o.Item.ItemData.map_unit_data == nil or o.Item.ItemData.map_unit_data.Count == 0)
    then
        o.CreateTime = CS.System.DateTime.UtcNow
        o:_saveData()
    end

    o:_setup()

    return o
end

function UnitRedEnvelopes:_setup()
    local create_time = self.Item.ItemData.map_unit_data[1]
    if (CS.System.String.IsNullOrEmpty(create_time) == false)
    then
        self.CreateTime = CS.System.DateTime.Parse(create_time)
    end

    local value = self.Item.ItemData.map_unit_data[2]
    local multiple,remainder = math.modf(value/100)
    local r_v = value / 100
    if remainder == 0 then
        r_v = multiple
    end
    self.Value = r_v
end

function UnitRedEnvelopes:_saveData()
    if (self.Item.ItemData.map_unit_data == nil)
    then
        self.Item.ItemData.map_unit_data = CS.Casinos.LuaHelper.GetNewStringStringMap()
    end

    self.Item.ItemData.map_unit_data[1] = CS.Casinos.EbTool.jsonSerialize(self.CreateTime)
    self.Item.ItemData.map_unit_data[2] = self.Value
end



UnitFacRedEnvelopes = UnitFac:new()

function UnitFacRedEnvelopes:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self

    return o
end

function UnitFacRedEnvelopes:createUnit(item)
    return UnitRedEnvelopes:new(nil,item)
end