TbDataDesktopHBetOperateNiuNiu = TbDataBase:new()

function TbDataDesktopHBetOperateNiuNiu:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.OperateGolds = nil

	return o
end

function TbDataDesktopHBetOperateNiuNiu:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_OperateGolds")
		then
			self.OperateGolds = tonumber(data.data_value)
		end		
	end	
end


TbDataFactoryDesktopHBetOperateNiuNiu = TbDataFactoryBase:new()

function TbDataFactoryDesktopHBetOperateNiuNiu:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryDesktopHBetOperateNiuNiu:createTbData(id)
	return TbDataDesktopHBetOperateNiuNiu:new(id)
end