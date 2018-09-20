TbDataUnitGiftTmp = TbDataBase:new()

function TbDataUnitGiftTmp:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.PeriodOfValidity = nil

	return o
end

function TbDataUnitGiftTmp:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_PeriodOfValidity")
		then
			self.PeriodOfValidity = tonumber(data.data_value)
		end
	end	
end


TbDataFactoryUnitGiftTmp = TbDataFactoryBase:new()

function TbDataFactoryUnitGiftTmp:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryUnitGiftTmp:createTbData(id)
	return TbDataUnitGiftTmp:new(id)
end