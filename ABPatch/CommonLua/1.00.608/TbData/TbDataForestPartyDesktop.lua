oTbDataForestPartyDesktop = TbDataBase:new()

function TbDataForestPartyDesktop:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.BeBankerLimit = nil
	o.BetLimitMax = nil
	o.CaiJinMinNum = nil
	o.CaiJinMaxNum = nil
	o.SysPumping = nil
	o.Offset = 10000

	return o
end

function TbDataForestPartyDesktop:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_BeBankerLimit")
		then
			self.BeBankerLimit = tonumber(data.data_value) * self.Offset
		end
		if(data.data_name == "I_BetLimitMax")
		then
			self.BetLimitMax = tonumber(data.data_value) * self.Offset
		end				
		if(data.data_name == "I_CaiJinMinNum")
		then
			self.CaiJinMinNum = tonumber(data.data_value)
		end
		if(data.data_name == "I_CaiJinMaxNum")
		then
			self.CaiJinMaxNum = tonumber(data.data_value)
		end	
		if(data.data_name == "I_SysPumping")
		then
			self.SysPumping = tonumber(data.data_value)
		end	
	end	
end


TbDataFactoryForestPartyDesktop = TbDataFactoryBase:new()

function TbDataFactoryForestPartyDesktop:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryForestPartyDesktop:createTbData(id)
	return TbDataForestPartyDesktop:new(id)
end