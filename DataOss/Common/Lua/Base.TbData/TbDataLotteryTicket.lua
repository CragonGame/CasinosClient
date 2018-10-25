TbDataLotteryTicket = TbDataBase:new()

function TbDataLotteryTicket:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.RewardRate = nil
	o.SysPumpingRate = nil

	return o
end

function TbDataLotteryTicket:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "R_RewardRate")
		then
			self.RewardRate = tonumber(data.data_value)
		end
		if(data.data_name == "R_RewardRate")
		then
			self.SysPumpingRate = tonumber(data.data_value)
		end
	end	
end


TbDataFactoryLotteryTicket = TbDataFactoryBase:new()

function TbDataFactoryLotteryTicket:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryLotteryTicket:createTbData(id)
	return TbDataLotteryTicket:new(id)
end