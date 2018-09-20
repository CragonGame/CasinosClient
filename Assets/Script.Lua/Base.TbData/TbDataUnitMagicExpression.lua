-- Copyright(c) Cragon. All rights reserved.

TbDataUnitMagicExpression = TbDataBase:new()

function TbDataUnitMagicExpression:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.ExpIcon = nil
	o.MagicExpLimit = nil
	o.AniName = nil
	o.AudioName = nil
	o.MagicExpMoveType = nil

	return o
end

function TbDataUnitMagicExpression:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "T_ExpIcon")
		then
			self.ExpIcon = tostring(data.data_value)
		end
		if(data.data_name == "I_UseLimit")
		then
			self.MagicExpLimit = tonumber(data.data_value)
		end
		if(data.data_name == "T_AniName")
		then
			self.AniName = tostring(data.data_value)		
		end
		if(data.data_name == "T_AudioName")
		then
			self.AudioName = tostring(data.data_value)		
		end
		if(data.data_name == "I_MoveType")
		then
			self.MagicExpMoveType = tonumber(data.data_value)		
		end
	end	
end


TbDataFactoryUnitMagicExpression = TbDataFactoryBase:new()

function TbDataFactoryUnitMagicExpression:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryUnitMagicExpression:createTbData(id)
	return TbDataUnitMagicExpression:new(id)
end