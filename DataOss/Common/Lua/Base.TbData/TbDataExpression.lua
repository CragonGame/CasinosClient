TbDataExpression = TbDataBase:new()

function TbDataExpression:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.ExpressionName = nil
	o.ExpressionType = nil

	return o
end

function TbDataExpression:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "T_ExpressionName")
		then
			self.ExpressionName = tostring(data.data_value)
		end
		if(data.data_name == "I_ExpressionType")
		then
			self.ExpressionType = tonumber(data.data_value)
		end
	end	
end


TbDataFactoryExpression = TbDataFactoryBase:new()

function TbDataFactoryExpression:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryExpression:createTbData(id)
	return TbDataExpression:new(id)
end