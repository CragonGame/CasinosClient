TbDataDesktopHBetPotNiuNiu = TbDataBase:new()

function TbDataDesktopHBetPotNiuNiu:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.Index = nil

	return o
end

function TbDataDesktopHBetPotNiuNiu:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_Index")
		then
			self.Index = tonumber(data.data_value)
		end		
	end	
end


TbDataFactoryDesktopHBetPotNiuNiu = TbDataFactoryBase:new()

function TbDataFactoryDesktopHBetPotNiuNiu:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryDesktopHBetPotNiuNiu:createTbData(id)
	return TbDataDesktopHBetPotNiuNiu:new(id)
end