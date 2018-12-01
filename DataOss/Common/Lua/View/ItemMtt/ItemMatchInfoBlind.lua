-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ItemMatchInfoBlind = {}

---------------------------------------
function ItemMatchInfoBlind:new(o, com, info, level, can_rebuy, can_addon, raiseBlindTime, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GTextLevel = com:GetChild("TextLevel").asTextField
    o.GTextBlind = com:GetChild("TextBlind").asTextField
    o.GTextAnte = com:GetChild("TextAnte").asTextField
    o.GTextRaiseBlindTime = com:GetChild("BlindnessTime").asTextField
    o.ComRebuy = com:GetChild("ComRepurchase").asCom
    o.GComAddon = com:GetChild("ComPurchase").asCom
    o.GTextLevel.text = level
    o.GTextBlind.text = UiChipShowHelper:GetGoldShowStr3(info.BlindsSmall) .. "/" .. UiChipShowHelper:GetGoldShowStr3(info.BlindsBig)
    o.GTextAnte.text = UiChipShowHelper:GetGoldShowStr3(info.Ante)
    o.GTextRaiseBlindTime.text = raiseBlindTime .. view_mgr.LanMgr:getLanValue("Second")
    o.ComRebuy.visible = can_rebuy
    o.GComAddon.visible = can_addon
    return o
end