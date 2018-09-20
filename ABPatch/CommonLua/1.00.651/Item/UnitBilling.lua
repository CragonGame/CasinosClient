UnitBilling = Unit:new()

function UnitBilling:new(o,item)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.UnitType = "Billing"
	o.Item = item
	
	return o
end



UnitFacBilling = UnitFac:new()

function UnitFacBilling:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self	

	return o
end

function UnitFacBilling:createUnit(item)	
	return UnitBilling:new(nil,item)
end