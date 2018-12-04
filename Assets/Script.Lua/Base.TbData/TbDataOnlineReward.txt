-- Copyright(c) Cragon. All rights reserved.

TbDataOnlineReward = TbDataBase:new()

function TbDataOnlineReward:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.TmSpan = nil
	o.RewardGold = nil

	return o
end

function TbDataOnlineReward:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_TmSpan")
		then
			self.TmSpan = tonumber(data.data_value)
		end
		if(data.data_name == "I_RewardGold")
		then
			self.RewardGold = tonumber(data.data_value)
		end
	end	
end


TbDataFactoryOnlineReward = TbDataFactoryBase:new()

function TbDataFactoryOnlineReward:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryOnlineReward:createTbData(id)
	return TbDataOnlineReward:new(id)
end