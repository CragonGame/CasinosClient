-- Copyright(c) Cragon. All rights reserved.

TbDataTbDataTexasSnowBallRewardInfo = TbDataBase:new()

function TbDataTbDataTexasSnowBallRewardInfo:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.StartRank = 0
	o.EndRank = 0
	o.RewardRatio = 0
	o.TableId = 0
	return o
end

function TbDataTbDataTexasSnowBallRewardInfo:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_StartRank")
		then
			self.StartRank = tonumber(data.data_value)
		end
		if(data.data_name == "I_EndRank")
		then
			self.EndRank = tonumber(data.data_value)
		end
		if(data.data_name == "R_RewardRatio")
		then
			self.RewardRatio = tonumber(data.data_value)
		end
		if(data.data_name == "I_TableId")
		then
			self.TableId = tonumber(data.data_value)
		end
	end	
end


TbDataFactoryTexasSnowBallRewardInfo = TbDataFactoryBase:new()

function TbDataFactoryTexasSnowBallRewardInfo:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryTexasSnowBallRewardInfo:createTbData(id)
	return TbDataTbDataTexasSnowBallRewardInfo:new(id)
end