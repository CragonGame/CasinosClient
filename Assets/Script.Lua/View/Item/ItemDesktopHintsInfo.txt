-- Copyright(c) Cragon. All rights reserved.
-- 普通桌牌型的其中一条

---------------------------------------
ItemDesktopHintsInfo = {}

---------------------------------------
function ItemDesktopHintsInfo:new(o, key, item, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.ViewMgr = view_mgr
    o.mKey = key
    o.mItem = item
    o.mArrayLoaders = {}
    for i = 0, 4 do
        o.mArrayLoaders[i] = item:GetChild("loder_" .. tostring(i)).asLoader
    end
    o.mTitle = item:GetChild("title").asTextField
    return o
end

---------------------------------------
function ItemDesktopHintsInfo:setDesktopHintInfo(hint_info)
    self.mTitle.text = self.ViewMgr.LanMgr:getLanValue(hint_info.HintName)
    self.mArrayLoaders[0].icon = self.CasinosContext.PathMgr.DirAbCard .. string.lower(hint_info.CardFirstName) .. ".ab"
    self.mArrayLoaders[1].icon = self.CasinosContext.PathMgr.DirAbCard .. string.lower(hint_info.CardSecondName) .. ".ab"
    self.mArrayLoaders[2].icon = self.CasinosContext.PathMgr.DirAbCard .. string.lower(hint_info.CardThirdName) .. ".ab"
    self.mArrayLoaders[3].icon = self.CasinosContext.PathMgr.DirAbCard .. string.lower(hint_info.CardTurnName) .. ".ab"
    self.mArrayLoaders[4].icon = self.CasinosContext.PathMgr.DirAbCard .. string.lower(hint_info.CardRiverName) .. ".ab"
end