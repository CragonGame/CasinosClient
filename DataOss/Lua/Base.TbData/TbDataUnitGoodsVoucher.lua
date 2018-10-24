-- Copyright(c) Cragon. All rights reserved.

TbDataUnitGoodsVoucher = TbDataBase:new()

function TbDataUnitGoodsVoucher:new(id)
	o = {}
	setmetatable(o,self)
	self.__index = self
	self.Id = id

	return o
end

function TbDataUnitGoodsVoucher:load(list_datainfo)
	for i = 0, list_datainfo.Count - 1 do
		local data = list_datainfo[i] 
	end	
end


TbDataFactoryUnitGoodsVoucher = TbDataFactoryBase:new()

function TbDataFactoryUnitGoodsVoucher:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self  		
	
    return o
end

function TbDataFactoryUnitGoodsVoucher:createTbData(id)
	return TbDataUnitGoodsVoucher:new(id)
end