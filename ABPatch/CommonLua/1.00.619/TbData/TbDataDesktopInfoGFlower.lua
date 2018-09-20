TbDataDesktopInfoGFlower = TbDataBase:new()

function TbDataDesktopInfoGFlower:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.MustBetValue = nil
	o.EnterLimitMin = nil
	o.EnterLimitMax = nil
	o.RoomIconName = nil
	o.FireBetValue = nil
	o.IsClassicModel = nil
	o.ShowInLobby = nil
	o.LimitMultiply = 10000

	return o
end

function TbDataDesktopInfoGFlower:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_MustBetValue")
		then
			self.MustBetValue = tonumber(data.data_value)
		end
		if(data.data_name == "I_EnterLimitMin")
		then
			self.EnterLimitMin = tonumber(data.data_value)* self.LimitMultiply
		end		
		if(data.data_name == "I_EnterLimitMax")
		then
			self.EnterLimitMax = tonumber(data.data_value)* self.LimitMultiply
		end		
		if(data.data_name == "T_RoomIconName")
		then
			self.RoomIconName = tostring(data.data_value)
		end
		if(data.data_name == "I_FireBetValue")
		then
			self.FireBetValue = tonumber(data.data_value)* self.LimitMultiply
		end		
		if(data.data_name == "I_IsClassicModel")
		then
			self.IsClassicModel = tonumber(data.data_value)
		end		
		if(data.data_name == "I_ShowInLobby")
		then
			self.ShowInLobby = tonumber(data.data_value)
		end	
	end	
end


TbDataFactoryDesktopInfoGFlower = TbDataFactoryBase:new()

function TbDataFactoryDesktopInfoGFlower:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryDesktopInfoGFlower:createTbData(id)
	return TbDataDesktopInfoGFlower:new(id)
end