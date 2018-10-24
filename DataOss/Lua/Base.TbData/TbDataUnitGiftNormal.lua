-- Copyright(c) Cragon. All rights reserved.

TbDataUnitGiftNormal = TbDataBase:new()

function TbDataUnitGiftNormal:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id

	return o
end

function TbDataUnitGiftNormal:load(list_datainfo)
end


TbDataFactoryUnitGiftNormal = TbDataFactoryBase:new()

function TbDataFactoryUnitGiftNormal:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryUnitGiftNormal:createTbData(id)
	return TbDataUnitGiftNormal:new(id)
end