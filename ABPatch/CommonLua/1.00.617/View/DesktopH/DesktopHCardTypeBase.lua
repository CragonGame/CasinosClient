DesktopHCardTypeBase = {
	ListCard = nil
}

function DesktopHCardTypeBase:new(o,com)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	
    return o
end

function DesktopHCardTypeBase:SetCard(list_card)	
end

function DesktopHCardTypeBase:GetCardTypeStr()	
end

function DesktopHCardTypeBase:GetCardTypeByte()	
end


DesktopHCardTypeBaseFac = {	
}

function DesktopHCardTypeBaseFac:new(o,com)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	
    return o
end

function DesktopHCardTypeBaseFac:GetName()	
end

function DesktopHCardTypeBaseFac:CreateDesktopHCardType()	
end