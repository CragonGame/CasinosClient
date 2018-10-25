TbDataForestPartyLottery = TbDataBase:new()

function TbDataForestPartyLottery:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.LotteryType = nil
	o.LotteryRandom = nil

	return o
end

function TbDataForestPartyLottery:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_LotteryType")
		then
			self.LotteryType = tonumber(data.data_value)
		end
		if(data.data_name == "I_LotteryRandom")
		then
			self.LotteryRandom = tonumber(data.data_value)
		end						
	end	
end


TbDataFactoryForestPartyLottery = TbDataFactoryBase:new()

function TbDataFactoryForestPartyLottery:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryForestPartyLottery:createTbData(id)
	return TbDataForestPartyLottery:new(id)
end