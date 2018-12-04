TbDataDesktopHBetReward = TbDataBase:new()

function TbDataDesktopHBetReward:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.BetValue = nil
	o.BetRewardValue = nil
	o.BetProgressValue = nil
	o.BetRewardOffset = 10000

	return o
end

function TbDataDesktopHBetReward:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_BetValue")
		then
			self.BetValue = tonumber(data.data_value) * self.BetRewardOffset
		end
		if(data.data_name == "I_BetRewardValue")
		then
			self.BetRewardValue = tonumber(data.data_value)
		end		
		if(data.data_name == "I_BetProgressValue")
		then
			self.BetProgressValue = tonumber(data.data_value)
		end					
	end	
end


TbDataFactoryDesktopHBetReward = TbDataFactoryBase:new()

function TbDataFactoryDesktopHBetReward:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryDesktopHBetReward:createTbData(id)
	return TbDataDesktopHBetReward:new(id)
end