-- Copyright(c) Cragon. All rights reserved.

TbDataUnitGoldPackage = TbDataBase:new()

function TbDataUnitGoldPackage:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	self.Id = id
	o.GoldValue = nil

	return o
end

function TbDataUnitGoldPackage:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i] 
		if(data.data_name == "R_GoldValue")
		then
			self.GoldValue = tonumber(data.data_value)
		end
	end	
end


TbDataFactoryUnitGoldPackage = TbDataFactoryBase:new()

function TbDataFactoryUnitGoldPackage:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryUnitGoldPackage:createTbData(id)
	return TbDataUnitGoldPackage:new(id)
end