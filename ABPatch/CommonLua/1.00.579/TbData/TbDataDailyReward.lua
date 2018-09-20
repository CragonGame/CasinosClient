TbDataDailyReward = TbDataBase:new()

function TbDataDailyReward:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.Reward = nil
	o.VIPExtraReward = nil

	return o
end

function TbDataDailyReward:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_Reward")
		then
			self.Reward = tonumber(data.data_value)
		end
		if(data.data_name == "I_VIPExtraReward")
		then
			self.VIPExtraReward = tonumber(data.data_value)
		end
	end	
end


TbDataFactoryDailyReward = TbDataFactoryBase:new()

function TbDataFactoryDailyReward:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryDailyReward:createTbData(id)
	return TbDataDailyReward:new(id)
end