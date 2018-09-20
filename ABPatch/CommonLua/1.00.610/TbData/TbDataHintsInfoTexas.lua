TbDataHintsInfoTexas = TbDataBase:new()

function TbDataHintsInfoTexas:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	self.Id = id
	o.HintName = nil
	o.CardFirstName = nil
	o.CardSecondName = nil
	o.CardThirdName = nil
	o.CardTurnName = nil
	o.CardRiverName = nil

	return o
end

function TbDataHintsInfoTexas:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "T_HintName")
		then
			self.HintName = tostring(data.data_value)
		end
		if(data.data_name == "T_CardFirst")
		then
			self.CardFirstName = tostring(data.data_value)
		end
		if(data.data_name == "T_CardSecond")
		then
			self.CardSecondName = tostring(data.data_value)		
		end
		if(data.data_name == "T_CardThird")
		then
			self.CardThirdName = tostring(data.data_value)		
		end
		if(data.data_name == "T_CardTurn")
		then
			self.CardTurnName = tostring(data.data_value)		
		end
		if(data.data_name == "T_CardRiver")
		then
			self.CardRiverName = tostring(data.data_value)		
		end
	end	
end


TbDataFactoryHintsInfoTexas = TbDataFactoryBase:new()

function TbDataFactoryHintsInfoTexas:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryHintsInfoTexas:createTbData(id)
	return TbDataHintsInfoTexas:new(id)
end