-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
TbDataActorLevel = TbDataBase:new()

function TbDataActorLevel:new(id)
    o = {}
    setmetatable(o, self)
    self.__index = self
    o.Id = id
    o.Level = nil
    o.Experience = nil
    return o
end

---------------------------------------
function TbDataActorLevel:load(list_datainfo)
    for i = 0, list_datainfo.Count - 1 do
        local data = list_datainfo[i]
        if (data.data_name == "I_Level") then
            self.Level = tonumber(data.data_value)
        end
        if (data.data_name == "I_Experience") then
            self.Experience = tonumber(data.data_value)
        end
    end
end

---------------------------------------
TbDataFactoryActorLevel = TbDataFactoryBase:new()

---------------------------------------
function TbDataFactoryActorLevel:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function TbDataFactoryActorLevel:createTbData(id)
    return TbDataActorLevel:new(id)
end