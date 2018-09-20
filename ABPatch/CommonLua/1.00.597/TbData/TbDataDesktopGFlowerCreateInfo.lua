TbDataDesktopGFlowerCreateInfo = TbDataBase:new()

function TbDataDesktopGFlowerCreateInfo:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.BetValue = nil
	o.DesktopId = nil
	o.EnterLimit1 = nil
	o.EnterLimit2 = nil
	o.EnterLimit3 = nil
	o.EnterLimit4 = nil
	o.EnterLimit5 = nil

	return o
end

function TbDataDesktopGFlowerCreateInfo:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "F_BetValue")
		then
			self.BetValue = tonumber(data.data_value)
		end
		if(data.data_name == "I_DesktopId")
		then
			self.DesktopId = tonumber(data.data_value)
		end		
		if(data.data_name == "F_EnterLimit1")
		then
			self.EnterLimit1 = tonumber(data.data_value)
		end		
		if(data.data_name == "F_EnterLimit2")
		then
			self.EnterLimit2 = tonumber(data.data_value)
		end
		if(data.data_name == "F_EnterLimit3")
		then
			self.EnterLimit3 = tonumber(data.data_value)
		end		
		if(data.data_name == "F_EnterLimit4")
		then
			self.EnterLimit4 = tonumber(data.data_value)
		end
		if(data.data_name == "F_EnterLimit5")
		then
			self.EnterLimit5 = tonumber(data.data_value)
		end		
	end	
end


TbDataFactoryDesktopGFlowerCreateInfo = TbDataFactoryBase:new()

function TbDataFactoryDesktopGFlowerCreateInfo:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryDesktopGFlowerCreateInfo:createTbData(id)
	return TbDataDesktopGFlowerCreateInfo:new(id)
end