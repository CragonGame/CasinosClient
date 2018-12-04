TbDataForestPartyBetPotMultiple = TbDataBase:new()

function TbDataForestPartyBetPotMultiple:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.SolutionId = nil
	o.BetPotIndex = nil
	o.BetPotMultiple = nil
	o.AnimalType = nil
	o.AnimalColor = nil

	return o
end

function TbDataForestPartyBetPotMultiple:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_SolutionId")
		then
			self.SolutionId = tonumber(data.data_value)
		end
		if(data.data_name == "I_BetPotIndex")
		then
			self.BetPotIndex = tonumber(data.data_value)
		end				
		if(data.data_name == "I_BetPotMultiple")
		then
			self.BetPotMultiple = tonumber(data.data_value)
		end
		if(data.data_name == "I_AnimalType")
		then
			self.AnimalType = tonumber(data.data_value)
		end	
		if(data.data_name == "I_AnimalColor")
		then
			self.AnimalColor = tonumber(data.data_value)
		end	
	end	
end


TbDataFactoryForestPartyBetPotMultiple = TbDataFactoryBase:new()

function TbDataFactoryForestPartyBetPotMultiple:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryForestPartyBetPotMultiple:createTbData(id)
	return TbDataForestPartyBetPotMultiple:new(id)
end