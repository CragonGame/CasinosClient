TbDataCfigZhongFBDesktopH = TbDataBase:new()

function TbDataCfigZhongFBDesktopH:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.SysPumpingRate = nil
	o.RewardRate = nil

	return o
end

function TbDataCfigZhongFBDesktopH:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "R_SysPumpingRate")
		then
			self.SysPumpingRate = tonumber(data.data_value)
		end
		if(data.data_name == "R_RewardRate")
		then
			self.RewardRate = tonumber(data.data_value)
		end						
	end	
end


TbDataFactoryCfigZhongFBDesktopH = TbDataFactoryBase:new()

function TbDataFactoryCfigZhongFBDesktopH:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryCfigZhongFBDesktopH:createTbData(id)
	return TbDataCfigZhongFBDesktopH:new(id)
end