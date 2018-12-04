-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UiDesktopHCardTypeBase = {
    ListCard = nil
}

---------------------------------------
function UiDesktopHCardTypeBase:new(o, com)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function UiDesktopHCardTypeBase:SetCard(list_card)
end

---------------------------------------
function UiDesktopHCardTypeBase:GetCardTypeStr()
end

---------------------------------------
function UiDesktopHCardTypeBase:GetCardTypeByte()
end

---------------------------------------
DesktopHCardTypeBaseFac = {}

---------------------------------------
function DesktopHCardTypeBaseFac:new(o, com)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function DesktopHCardTypeBaseFac:GetName()
end

---------------------------------------
function DesktopHCardTypeBaseFac:CreateDesktopHCardType()
end