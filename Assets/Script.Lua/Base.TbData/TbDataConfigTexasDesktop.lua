TbDataConfigTexasDesktop = TbDataBase:new()

function TbDataConfigTexasDesktop:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.DesktopFee = nil
	o.SysPumpingRate = nil

	return o
end

function TbDataConfigTexasDesktop:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "R_DesktopFee")
		then
			self.DesktopFee = tonumber(data.data_value)
		end
		if(data.data_name == "R_SysPumpingRate")
		then
			self.SysPumpingRate = tonumber(data.data_value)
		end		
	end	
end


TbDataFactoryConfigTexasDesktop = TbDataFactoryBase:new()

function TbDataFactoryConfigTexasDesktop:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryConfigTexasDesktop:createTbData(id)
	return TbDataConfigTexasDesktop:new(id)
end