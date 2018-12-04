TbDataLotteryTicketGoldPercent = TbDataBase:new()

function TbDataLotteryTicketGoldPercent:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.WinGoldsPercent = nil
	o.WinRewardPotPercent = nil

	return o
end

function TbDataLotteryTicketGoldPercent:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_WinGoldsPercent")
		then
			self.WinGoldsPercent = tonumber(data.data_value)
		end
		if(data.data_name == "I_WinRewardPotPercent")
		then
			self.WinRewardPotPercent = tonumber(data.data_value)
		end
	end	
end


TbDataFactoryLotteryTicketGoldPercent = TbDataFactoryBase:new()

function TbDataFactoryLotteryTicketGoldPercent:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryLotteryTicketGoldPercent:createTbData(id)
	return TbDataLotteryTicketGoldPercent:new(id)
end