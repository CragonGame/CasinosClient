-- Copyright(c) Cragon. All rights reserved.

TbDataPresetMsg = TbDataBase:new()

function TbDataPresetMsg:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	o.Id = id
	o.PresetMsg = nil
	o.PresetMsgType = nil

	return o
end

function TbDataPresetMsg:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i]
		if(data.data_name == "T_Msg")
		then
			self.PresetMsg = tostring(data.data_value)
		end
		if(data.data_name == "I_MsgType")
		then
			self.PresetMsgType = tonumber(data.data_value)
		end
	end	
end


TbDataFactoryPresetMsg = TbDataFactoryBase:new()

function TbDataFactoryPresetMsg:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryPresetMsg:createTbData(id)
	return TbDataPresetMsg:new(id)
end