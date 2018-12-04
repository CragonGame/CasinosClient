-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
TbDataBase = {
    Id = 0,
}

---------------------------------------
function TbDataBase:new(id)
    o = {}
    setmetatable(o, self)
    self.__index = self
    o.Id = id
    return o
end

---------------------------------------
function TbDataBase:load(list_datainfo)
end

---------------------------------------
TbDataFactoryBase = {
}

---------------------------------------
function TbDataFactoryBase:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function TbDataFactoryBase:createTbData(id)
end