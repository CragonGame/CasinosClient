-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UiDesktopHCardTypeTexas = UiDesktopHCardTypeBase:new(nil)

---------------------------------------
function UiDesktopHCardTypeTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.CardTypeTexas = nil
    return o
end

---------------------------------------
function UiDesktopHCardTypeTexas:SetCard(list_card)
    self.ListCard = list_card
    self.CardTypeTexas = CS.Casinos.CardTypeHelperTexas.GetHandRankHTexas(self.ListCard)
end

---------------------------------------
-- 返回值是牌型在Lan表中的Key
function UiDesktopHCardTypeTexas:GetCardTypeStr()
    local l = CS.Casinos.LuaHelper.ParseHandRankTypeTexasHToStr(self.CardTypeTexas)
    return l
end

---------------------------------------
-- 未使用
--function UiDesktopHCardTypeTexas:GetCardTypeByte()
--    local l = CS.Casinos.LuaHelper.ProtobufSerializeHandRankTypeTexasH(self.CardTypeTexas)
--    return l
--end

---------------------------------------
DesktopHCardTypeTexasFac = DesktopHCardTypeBaseFac:new(nil)

---------------------------------------
function DesktopHCardTypeTexasFac:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function DesktopHCardTypeTexasFac:GetName()
    return "Texas"
end

---------------------------------------
function DesktopHCardTypeTexasFac:CreateDesktopHCardType()
    local l = UiDesktopHCardTypeTexas:new(nil)
    return l
end