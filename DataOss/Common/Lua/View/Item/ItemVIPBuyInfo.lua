-- Copyright(c) Cragon. All rights reserved.
-- 已废弃

---------------------------------------
ItemVIPBuyInfo = {}

---------------------------------------
function ItemVIPBuyInfo:new(o, com_item)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    com_item.onClick:Add(
            function()
                o:_onClick()
            end
    )
    o.GTextdays = com_item:GetChild("Days").asTextField
    o.GTextcost = com_item:GetChild("Cost").asTextField
    return o
end

---------------------------------------
function ItemVIPBuyInfo:setVipInfo(vip_info)
    self.VIPInfoTbId = vip_info.Id
    local tb_mgr = TbDataMgr:new(nil)
    local unit_item = tb_mgr:GetData("UnitBilling", vip_info.Id)
    local view_mgr = ViewMgr:new(nil)
    self.GTextdays.text = unit_item.Amount .. unit_item.Bonus .. view_mgr.LanMgr:getLanValue("Day")
    self.GTextcost.text = "￥" .. vip_info.Price
end

---------------------------------------
function ItemVIPBuyInfo:_onClick()
    local view_mgr = ViewMgr:new(nil)
    local ev = view_mgr:GetEv("EvEntityBuyVIP")
    if (ev == nil) then
        ev = EvEntityBuyVIP:new(nil)
    end
    ev.buy_id = self.VIPInfoTbId
    view_mgr:SendEv(ev)
end