TbDataDesktopHBetOperateTexas = TbDataBase:new()

function TbDataDesktopHBetOperateTexas:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.OperateGolds = nil

	return o
end

function TbDataDesktopHBetOperateTexas:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_OperateGolds")
		then
			self.OperateGolds = tonumber(data.data_value)
		end		
	end	
end


TbDataFactoryDesktopHBetOperateTexas = TbDataFactoryBase:new()

function TbDataFactoryDesktopHBetOperateTexas:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryDesktopHBetOperateTexas:createTbData(id)
	return TbDataDesktopHBetOperateTexas:new(id)
end