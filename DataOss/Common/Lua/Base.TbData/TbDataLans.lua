TbDataLans = TbDataBase:new()

function TbDataLans:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.LanName = nil
	o.LanType = nil
	o.LanIcon = nil
	o.LanKey = nil

	return o
end

function TbDataLans:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "T_LanName")
		then
			self.LanName = tostring(data.data_value)
		end
		if(data.data_name == "T_LanType")
		then
			self.LanType = tostring(data.data_value)
		end
		if(data.data_name == "T_LanIcon")
		then
			self.LanIcon = tostring(data.data_value)		
		end
		if(data.data_name == "T_LanKey")
		then
			self.LanKey = tostring(data.data_value)		
		end
	end	
end


TbDataFactoryLans = TbDataFactoryBase:new()

function TbDataFactoryLans:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryLans:createTbData(id)
	return TbDataLans:new(id)
end