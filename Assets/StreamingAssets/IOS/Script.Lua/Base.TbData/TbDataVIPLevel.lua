-- Copyright(c) Cragon. All rights reserved.

TbDataVIPLevel = TbDataBase:new()

function TbDataVIPLevel:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.Level = nil
	o.ShowVIPSign = nil
	o.VIPPoint = nil
	o.LooseSendGoldPercent = nil
	o.ChargeSendPercent = nil

	return o
end

function TbDataVIPLevel:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_Level")
		then
			self.Level = tonumber(data.data_value)
		end
		if(data.data_name == "I_VIPSign")
		then
			self.ShowVIPSign = tonumber(data.data_value)
		end
		if(data.data_name == "I_VIPPoint")
		then
			self.VIPPoint = tonumber(data.data_value)		
		end
		if(data.data_name == "I_LooseSendGoldPercent")
		then
			self.LooseSendGoldPercent = tonumber(data.data_value)		
		end
		if(data.data_name == "I_ChargeSend")
		then
			self.ChargeSendPercent = tonumber(data.data_value)		
		end
	end	
end


TbDataFactoryVIPLevel = TbDataFactoryBase:new()

function TbDataFactoryVIPLevel:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryVIPLevel:createTbData(id)
	return TbDataVIPLevel:new(id)
end