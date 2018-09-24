-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ItemData1 = {}

---------------------------------------
function ItemData1:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.item_objid = nil
    o.item_tbid = 0
    o.count = 0
    o.map_unit_data = nil

    return o
end

---------------------------------------
function ItemData1:setData(data)
    self.item_objid = data[1]
    self.item_tbid = data[2]
    self.count = data[3]
    local m_d = data[4]
    if m_d ~= nil then
        local m = {}
        for i, v in pairs(data[4]) do
            m[i + 1] = v
        end
        self.map_unit_data = m
    end
end