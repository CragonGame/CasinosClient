TbDataCfigNiuNiuDesktopHGoldPercent = TbDataBase:new()

function TbDataCfigNiuNiuDesktopHGoldPercent:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.HandRankTypeGFlowerH = nil
	o.GoldPercent = nil
	o.WinRewardPotPercent = nil
	o.FactoryName = nil

	return o
end

function TbDataCfigNiuNiuDesktopHGoldPercent:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_HandRankTypeH")
		then
			self.HandRankTypeGFlowerH = tonumber(data.data_value)
		end
		if(data.data_name == "R_GoldPercent")
		then
			self.GoldPercent = tonumber(data.data_value)
		end		
		if(data.data_name == "R_WinRewardPotPercent")
		then
			self.WinRewardPotPercent = tonumber(data.data_value)
		end
		if(data.data_name == "T_FactoryName")
		then
			self.FactoryName = tostring(data.data_value)
		end	
	end	
end


TbDataFactoryCfigNiuNiuDesktopHGoldPercent = TbDataFactoryBase:new()

function TbDataFactoryCfigNiuNiuDesktopHGoldPercent:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryCfigNiuNiuDesktopHGoldPercent:createTbData(id)
	return TbDataCfigNiuNiuDesktopHGoldPercent:new(id)
end