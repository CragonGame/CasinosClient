TbDataDesktopHBetPotGFlower = TbDataBase:new()

function TbDataDesktopHBetPotGFlower:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.Index = nil

	return o
end

function TbDataDesktopHBetPotGFlower:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_Index")
		then
			self.Index = tonumber(data.data_value)
		end		
	end	
end


TbDataFactoryDesktopHBetPotGFlower = TbDataFactoryBase:new()

function TbDataFactoryDesktopHBetPotGFlower:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryDesktopHBetPotGFlower:createTbData(id)
	return TbDataDesktopHBetPotGFlower:new(id)
end