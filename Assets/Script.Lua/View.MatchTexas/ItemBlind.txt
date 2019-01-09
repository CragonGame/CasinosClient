-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ItemBlind = {}

---------------------------------------
function ItemBlind:new(o, com, data, lanbase)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    local text_level = com:GetChild("TextLevel").asTextField
    text_level.text = tostring(data.Id)
    local text_blind = com:GetChild("TextBlind").asTextField
    text_blind.text = UiChipShowHelper:GetGoldShowStr3(data.BlindsSmall) .. "/" .. UiChipShowHelper:GetGoldShowStr3(data.BlindsBig)
    local text_ante = com:GetChild("TextAnte").asTextField
    text_ante.text = UiChipShowHelper:GetGoldShowStr3(data.Ante)
    return o
end