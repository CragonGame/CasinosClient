-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
Item = {}

---------------------------------------
function Item:new(o, tb_data_mgr, item_data)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.EbDataMgr = tb_data_mgr
    o.ItemData = item_data
    o.TbDataItem = o.EbDataMgr:GetData("Item", o.ItemData.item_tbid)
    local unit_sys = UnitSys:new(nil)
    o.UnitLink = unit_sys:createUnit(o.TbDataItem.UnitType, o)
    return o
end