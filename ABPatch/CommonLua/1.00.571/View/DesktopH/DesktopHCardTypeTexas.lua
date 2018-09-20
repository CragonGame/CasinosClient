DesktopHCardTypeTexas = DesktopHCardTypeBase:new(nil)

function DesktopHCardTypeTexas:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	o.CardTypeTexas = nil

    return o
end

function DesktopHCardTypeTexas:SetCard(list_card)	
	self.ListCard = list_card
	self.CardTypeTexas = CS.Casinos.CardTypeHelperTexas.GetHandRankHTexas(self.ListCard)
end

function DesktopHCardTypeTexas:GetCardTypeStr()
    local l = CS.Casinos.LuaHelper.ParseHandRankTypeTexasHToStr(self.CardTypeTexas)
	return l
end

function DesktopHCardTypeTexas:GetCardTypeByte()
    local l = CS.Casinos.LuaHelper.ProtobufSerializeHandRankTypeTexasH(self.CardTypeTexas)
	return l
end


DesktopHCardTypeTexasFac = DesktopHCardTypeBaseFac:new(nil)

function DesktopHCardTypeTexasFac:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	
    return o
end

function DesktopHCardTypeTexasFac:GetName()	
	return "Texas"
end

function DesktopHCardTypeTexasFac:CreateDesktopHCardType()
    local l = DesktopHCardTypeTexas:new(nil)
	return l
end