-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
LanBase = {}

---------------------------------------
function LanBase:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function LanBase:getValue(key)
end

---------------------------------------
function LanBase:parseLanKeyValue(tb_datamgr)
end

---------------------------------------
function LanBase:getLanPackageName()
end

---------------------------------------
function LanBase:GetGoldShowStr(gold, show_short_way, precision_length)
end

---------------------------------------
function LanBase:GetGoldShowStr2(gold, show_short_way, precision_length)
end