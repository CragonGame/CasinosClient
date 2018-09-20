TbDataDesktopHBetPotTexas = TbDataBase:new()

function TbDataDesktopHBetPotTexas:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.Index = nil

	return o
end

function TbDataDesktopHBetPotTexas:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_Index")
		then
			self.Index = tonumber(data.data_value)
		end		
	end	
end


TbDataFactoryDesktopHBetPotTexas = TbDataFactoryBase:new()

function TbDataFactoryDesktopHBetPotTexas:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryDesktopHBetPotTexas:createTbData(id)
	return TbDataDesktopHBetPotTexas:new(id)
end