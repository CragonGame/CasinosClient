TbDataUnitConsume = TbDataBase:new()

function TbDataUnitConsume:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id

	return o
end

function TbDataUnitConsume:load(list_datainfo)
end


TbDataFactoryUnitConsume = TbDataFactoryBase:new()

function TbDataFactoryUnitConsume:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryUnitConsume:createTbData(id)
	return TbDataUnitConsume:new(id)
end