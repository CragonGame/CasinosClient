TbDataItemType = TbDataBase:new()

function TbDataItemType:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.ParentTbId = nil
	o.TypeName = nil
	o.TypeIcon = nil
	o.TypeDesc = nil

	return o
end

function TbDataItemType:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_ParentTbId")
		then
			self.ParentTbId = tonumber(data.data_value)
		end
		if(data.data_name == "T_TypeName")
		then
			self.TypeName = tostring(data.data_value)
		end
		if(data.data_name == "T_TypeIcon")
		then
			self.TypeIcon = tostring(data.data_value)		
		end
		if(data.data_name == "T_TypeDesc")
		then
			self.TypeDesc = tostring(data.data_value)		
		end
	end	
end
        
function TbDataItemType:getChildItemData(data_mgr)  
	local map_child = {}
	for key,value in pairs(data_mgr:GetMapData("ItemType")) do
		local item_type = value
        if (item_type.ParentTbId == self.Id)
		then
			map_child[item_type.Id] = item_type
		end
	end
    return map_child
end
        
function TbDataItemType:getCurrentTypeItems(data_mgr)
	local map_items = {}
	for key,value in pairs(data_mgr:GetMapData("Item")) do
		local item = value
        if (item.ItemTypeTbId == self.Id)
		then
			map_items[item.Id] = item
		end
	end
    return map_items
end


TbDataFactoryItemType = TbDataFactoryBase:new()

function TbDataFactoryItemType:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryItemType:createTbData(id)
	return TbDataItemType:new(id)
end