-- Copyright(c) Cragon. All rights reserved.

TbDataTexasRaiseBlinds = TbDataBase:new()

function TbDataTexasRaiseBlinds:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.BlindsSmall = 0
	o.BlindsBig = 0
	o.Ante = 0
	o.BlindType = 0
	o.BlindId = 0

	return o
end

function TbDataTexasRaiseBlinds:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_BlindsSmall")
		then
			self.BlindsSmall = tonumber(data.data_value)
		end
		if(data.data_name == "I_BlindsBig")
		then
			self.BlindsBig = tonumber(data.data_value)
		end
		if(data.data_name == "I_Ante")
		then
			self.Ante = tonumber(data.data_value)
		end
		if(data.data_name == "I_BlindType")
		then
			self.BlindType = tonumber(data.data_value)
		end
		if(data.data_name == "I_BlindId")
		then
			self.BlindId = tonumber(data.data_value)
		end
	end	
end


TbDataFactoryTexasRaiseBlinds = TbDataFactoryBase:new()

function TbDataFactoryTexasRaiseBlinds:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryTexasRaiseBlinds:createTbData(id)
	return TbDataTexasRaiseBlinds:new(id)
end