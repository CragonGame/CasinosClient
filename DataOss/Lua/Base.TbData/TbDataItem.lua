TbDataItem = TbDataBase:new()

function TbDataItem:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.ItemTypeTbId = nil
	o.UnitType = nil
	o.Name = nil
	o.Icon = nil
	o.Desc = nil
	o.PriceType = nil
	o.Price = nil
	o.Discount = nil
	o.CanOverlay = nil
	o.MaxOverlayCount = nil
	o.CanSell = nil
	o.BagType = nil
	o.UseWhenEnterBag = false

	return o
end

function TbDataItem:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_ItemTypeTbId")
		then
			self.ItemTypeTbId = tonumber(data.data_value)
		end
		if(data.data_name == "T_UnitType")
		then
			self.UnitType = tostring(data.data_value)
		end
		if(data.data_name == "T_Name")
		then
			self.Name = tostring(data.data_value)		
		end
		if(data.data_name == "T_Icon")
		then
			self.Icon = tostring(data.data_value)		
		end
		if(data.data_name == "T_Desc")
		then
			self.Desc = tostring(data.data_value)		
		end
		if(data.data_name == "I_PriceType")
		then
			self.PriceType = tonumber(data.data_value)		
		end
		if(data.data_name == "I_Price")
		then
			self.Price = tonumber(data.data_value)		
		end
		if(data.data_name == "I_Discount")
		then
			self.Discount = tonumber(data.data_value)		
		end
		if(data.data_name == "I_CanOverlay")
		then
			self.CanOverlay = tonumber(data.data_value)		
		end
		if(data.data_name == "I_MaxOverlayCount")
		then
			self.MaxOverlayCount = tonumber(data.data_value)		
		end
		if(data.data_name == "I_CanSell")
		then
			self.CanSell = tonumber(data.data_value)		
		end
		if(data.data_name == "I_ItemBagType")
		then
			self.BagType = tonumber(data.data_value)		
		end
		if(data.data_name == "I_EnterBagUse")
		then
			local d = tonumber(data.data_value)
			if d == 1 then
				self.UseWhenEnterBag = true
			else
				self.UseWhenEnterBag = false
			end
		end
	end	
end


TbDataFactoryItem = TbDataFactoryBase:new()

function TbDataFactoryItem:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryItem:createTbData(id)
	return TbDataItem:new(id)
end