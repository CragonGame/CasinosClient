-- Copyright(c) Cragon. All rights reserved.
-- 钱包的一个Item

ItemUiPurseGold = {}

function ItemUiPurseGold:new(o,view_shop,gold,tb_item)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    self.Context = Context
    o.ViewShop = view_shop
    o.GComGold = gold
    o.GTextGold = o.GComGold:GetChild("GoldValue").asTextField
    --o.GLoaderIcon = o.GComGold:GetChild("LoaderGold").asLoader
    o.GBtnBuy = o.GComGold:GetChild("BtnBuy").asButton
	o.GTextPrice = o.GBtnBuy:GetChild("Price").asTextField
    o.GBtnBuy.onClick:Add(
            function()
                o:onClickBtnBuy()
            end
    )
    o.TbDataItem = tb_item
    --local chip = "筹码"
    --if (self.Context.Cfg.UseLan) then
    --    chip = view_shop.ViewMgr.LanMgr:getLanValue("筹码","Chip")
    --end
    local tb_mgr = CS.Casinos.CasinosContext.Instance.TbDataMgrLua
    local tb_unit = tb_mgr:GetData("UnitBilling",tb_item.Id)
    o.GTextGold.text = UiChipShowHelper:getGoldShowStr((tb_unit.Amount + tb_unit.Bonus), view_shop.ViewMgr.LanMgr.LanBase,nil,0)--..chip
    o.GTextPrice.text = tb_item.Price
    --o.GLoaderIcon.icon = CS.Casinos.CasinosContext.Instance.PathMgr.DirAbItem .. tostring(tb_item.Icon) .. ".ab"
    return o
end

function ItemUiPurseGold:onClickBtnBuy()
    local ev = self.ViewShop.ViewMgr:GetEv("EvUiRequestBuyGold")
    if(ev == nil)
    then
        ev = EvUiRequestBuyGold:new(nil)
    end
    ev.buy_goldid = self.TbDataItem.Id
    self.ViewShop.ViewMgr:SendEv(ev)
end