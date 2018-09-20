TbDataForestPartyAnimalList = TbDataBase:new()

function TbDataForestPartyAnimalList:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.AnimalType = nil
	o.AnimalIndex = nil

	return o
end

function TbDataForestPartyAnimalList:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_AnimalType")
		then
			self.AnimalType = tonumber(data.data_value)
		end
		if(data.data_name == "I_AnimalIndex")
		then
			self.AnimalIndex = tonumber(data.data_value)
		end				
	end	
end


TbDataFactoryForestPartyAnimalList = TbDataFactoryBase:new()

function TbDataFactoryForestPartyAnimalList:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryForestPartyAnimalList:createTbData(id)
	return TbDataForestPartyAnimalList:new(id)
end