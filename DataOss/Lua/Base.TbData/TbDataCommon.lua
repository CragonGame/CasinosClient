TbDataCommon = TbDataBase:new()

function TbDataCommon:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.Key = nil
	o.Value = nil

	return o
end

function TbDataCommon:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "T_Key")
		then
			self.Key = tostring(data.data_value)
		end
		if(data.data_name == "T_Value")
		then
			self.Value = tostring(data.data_value)
		end
	end	
end


TbDataFactoryCommon = TbDataFactoryBase:new()

function TbDataFactoryCommon:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryCommon:createTbData(id)
	return TbDataCommon:new(id)
end