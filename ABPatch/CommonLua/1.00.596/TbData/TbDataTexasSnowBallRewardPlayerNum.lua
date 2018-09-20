TbDataTexasSnowBallRewardPlayerNum = TbDataBase:new()

function TbDataTexasSnowBallRewardPlayerNum:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.MinPlayerNum = 0
	o.MaxPlayerNum = 0
	o.RewardNum = 0
	return o
end

function TbDataTexasSnowBallRewardPlayerNum:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_MinPlayerNum")
		then
			self.MinPlayerNum = tonumber(data.data_value)
		end
		if(data.data_name == "I_MinPlayerMax")
		then
			self.MaxPlayerNum = tonumber(data.data_value)
		end
		if(data.data_name == "I_RewardNum")
		then
			self.RewardNum = tonumber(data.data_value)
		end
	end	
end


TbDataFactoryTexasSnowBallRewardPlayerNum = TbDataFactoryBase:new()

function TbDataFactoryTexasSnowBallRewardPlayerNum:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryTexasSnowBallRewardPlayerNum:createTbData(id)
	return TbDataTexasSnowBallRewardPlayerNum:new(id)
end