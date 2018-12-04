TbDataDesktopFastBet = TbDataBase:new()

function TbDataDesktopFastBet:new(id)
    o = {}
    setmetatable(o, self)
    self.__index = self
    o.Id = id
    o.DesktopId = 0
    o.DesktopType = DesktopType.Classic
    o.IsPreflop = true
    o.Type1 = DesktopFastBetType.BigBlindMuilty
    o.Value1 = 0.0
    o.Type2 = DesktopFastBetType.BigBlindMuilty
    o.Value2 = 0.0
    o.Type3 = DesktopFastBetType.BigBlindMuilty
    o.Value3 = 0.0
    o.Type4 = DesktopFastBetType.BigBlindMuilty
    o.Value4 = 0.0
    o.Type5 = DesktopFastBetType.BigBlindMuilty
    o.Value5 = 0.0
    o.Type6 = DesktopFastBetType.BigBlindMuilty
    o.Value6 = 0.0
    o.Type7 = DesktopFastBetType.BigBlindMuilty
    o.Value7 = 0.0
    o.Type8 = DesktopFastBetType.BigBlindMuilty
    o.Value8 = 0.0

    return o
end

function TbDataDesktopFastBet:load(list_datainfo)
    for i = 0, list_datainfo.Count - 1 do
        local data = list_datainfo[i]
        if (data.data_name == "I_DesktopId")
        then
            self.DesktopId = tonumber(data.data_value)
        end
        if (data.data_name == "I_DesktopType")
        then
            self.DesktopType = tonumber(data.data_value)
        end
        if (data.data_name == "I_IsPreflop")
        then
            self.IsPreflop = tonumber(data.data_value) == 1
        end
        if (data.data_name == "I_1Type")
        then
            self.Type1 = tonumber(data.data_value)
        end
        if (data.data_name == "R_1Value")
        then
            self.Value1 = tonumber(data.data_value)
        end
        if (data.data_name == "I_2Type")
        then
            self.Type2 = tonumber(data.data_value)
        end
        if (data.data_name == "R_2Value")
        then
            self.Value2 = tonumber(data.data_value)
        end
        if (data.data_name == "I_3Type")
        then
            self.Type3 = tonumber(data.data_value)
        end
        if (data.data_name == "R_3Value")
        then
            self.Value3 = tonumber(data.data_value)
        end
        if (data.data_name == "I_4Type")
        then
            self.Type4 = tonumber(data.data_value)
        end
        if (data.data_name == "R_4Value")
        then
            self.Value4 = tonumber(data.data_value)
        end
        if (data.data_name == "I_5Type")
        then
            self.Type5 = tonumber(data.data_value)
        end
        if (data.data_name == "R_5Value")
        then
            self.Value5 = tonumber(data.data_value)
        end
        if (data.data_name == "I_6Type")
        then
            self.Type6 = tonumber(data.data_value)
        end
        if (data.data_name == "R_6Value")
        then
            self.Value6 = tonumber(data.data_value)
        end
        if (data.data_name == "I_7Type")
        then
            self.Type7 = tonumber(data.data_value)
        end
        if (data.data_name == "R_7Value")
        then
            self.Value7 = tonumber(data.data_value)
        end
        if (data.data_name == "I_8Type")
        then
            self.Type8 = tonumber(data.data_value)
        end
        if (data.data_name == "R_8Value")
        then
            self.Value8 = tonumber(data.data_value)
        end
    end
end

TbDataFactoryDesktopFastBet = TbDataFactoryBase:new()

function TbDataFactoryDesktopFastBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    return o
end

function TbDataFactoryDesktopFastBet:createTbData(id)
    return TbDataDesktopFastBet:new(id)
end

DesktopType = {
    Classic = 1,
    MTT = 2,
}

DesktopFastBetType = {
    ConstValue = 1,
    BigBlindMuilty = 2,
    BetPotMuilty = 3,
}

DesktopFastBetInfo = {

}

function DesktopFastBetInfo:new()
    o = {}
    setmetatable(o, self)
    self.__index = self
    o.DesktopFastBetType = DesktopFastBetType.BigBlindMuilty
    o.DesktopFastBetValue = 0
    o.BigThanStack = false
    o.NeedBetValue = 0

    return o
end
