-- Copyright(c) Cragon. All rights reserved.

TbDataUnitRedEnvelopes = TbDataBase:new()

function TbDataUnitRedEnvelopes:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id

	return o
end

function TbDataUnitRedEnvelopes:load(list_datainfo)
end


TbDataFactoryUnitRedEnvelopes = TbDataFactoryBase:new()

function TbDataFactoryUnitRedEnvelopes:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryUnitRedEnvelopes:createTbData(id)
	return TbDataUnitRedEnvelopes:new(id)
end