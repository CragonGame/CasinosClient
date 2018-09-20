TbDataForestPartyBetOperate = TbDataBase:new()

function TbDataForestPartyBetOperate:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.OperateIndex = nil
	o.OperateGolds = nil

	return o
end

function TbDataForestPartyBetOperate:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "I_OperateIndex")
		then
			self.OperateIndex = tonumber(data.data_value)
		end
		if(data.data_name == "I_OperateGolds")
		then
			self.OperateGolds = tonumber(data.data_value)
		end				
	end	
end


TbDataFactoryForestPartyBetOperate = TbDataFactoryBase:new()

function TbDataFactoryForestPartyBetOperate:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryForestPartyBetOperate:createTbData(id)
	return TbDataForestPartyBetOperate:new(id)
end