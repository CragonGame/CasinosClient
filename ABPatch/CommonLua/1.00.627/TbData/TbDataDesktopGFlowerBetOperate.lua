TbDataDesktopGFlowerBetOperate = TbDataBase:new()

function TbDataDesktopGFlowerBetOperate:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.DesktopId = nil
	o.BetOperate = nil
	o.BetIcon = nil

	return o
end

function TbDataDesktopGFlowerBetOperate:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_DesktopId")
		then
			self.DesktopId = tonumber(data.data_value)
		end
		if(data.data_name == "I_BetOperate")
		then
			self.BetOperate = tonumber(data.data_value)
		end		
		if(data.data_name == "T_BetIcon")
		then
			self.BetIcon = tostring(data.data_value)
		end	
	end	
end


TbDataFactoryDesktopGFlowerBetOperate = TbDataFactoryBase:new()

function TbDataFactoryDesktopGFlowerBetOperate:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryDesktopGFlowerBetOperate:createTbData(id)
	return TbDataDesktopGFlowerBetOperate:new(id)
end