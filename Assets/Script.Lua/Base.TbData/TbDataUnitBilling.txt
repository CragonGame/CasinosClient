-- Copyright(c) Cragon. All rights reserved.

TbDataUnitBilling = TbDataBase:new()

function TbDataUnitBilling:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	self.Id = id
	o.Amount = nil
	o.Bonus = nil
	o.StoreSKU = nil

	return o
end

function TbDataUnitBilling:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_Amount")
		then
			self.Amount = tonumber(data.data_value)
		end
		if(data.data_name == "I_Bonus")
		then
			self.Bonus = tonumber(data.data_value)
		end
		if(data.data_name == "T_StoreSKU")
		then
			self.StoreSKU = tostring(data.data_value)		
		end
	end	
end


TbDataFactoryUnitBilling = TbDataFactoryBase:new()

function TbDataFactoryUnitBilling:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryUnitBilling:createTbData(id)
	return TbDataUnitBilling:new(id)
end