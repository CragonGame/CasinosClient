-- Copyright(c) Cragon. All rights reserved.

ItemUiShopGold = {}

function ItemUiShopGold:new(o,view_shop,gold,tb_item)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    o.ViewShop = view_shop
    o.GComGold = gold
    o.GComGold.onClick:Add(
            function()
                o:onClick()
            end
    )
    o.GTextGold = o.GComGold:GetChild("GoldValue").asTextField
    o.GLoaderIcon = o.GComGold:GetChild("LoaderGold").asLoader
    o.GBtnBuy = o.GComGold:GetChild("BtnBuy").asButton
    o.GTextPrice = o.GBtnBuy:GetChild("Price").asTextField
    o.GBtnBuy.onClick:Add(
            function()
                o:onClickBtnBuy()
            end
    )
    o.TbDataItem = tb_item
    local chip = view_shop.ViewMgr.LanMgr:getLanValue("Chip")
    local tb_mgr = CS.Casinos.CasinosContext.Instance.TbDataMgrLua
    local tb_unit = tb_mgr:GetData("UnitGoldPackage",tb_item.Id)
    o.GTextGold.text = UiChipShowHelper:getGoldShowStr(tb_unit.GoldValue * 10000, view_shop.ViewMgr.LanMgr.LanBase,nil,1)..chip
    o.GTextPrice.text = tb_item.Price
    o.GLoaderIcon.icon = CS.Casinos.CasinosContext.Instance.PathMgr.DirAbItem .. tostring(tb_item.Icon) .. ".ab"
    return o
end

function ItemUiShopGold:onClickBtnBuy()
    local ev = self.ViewShop.ViewMgr:GetEv("EvUiRequestBuyGold")
    if(ev == nil)
    then
        ev = EvUiRequestBuyGold:new(nil)
    end
    print("ItemUiShopGold:onClickBtnBuy      "..self.TbDataItem.Id)
    ev.buy_goldid = self.TbDataItem.Id
    self.ViewShop.ViewMgr:SendEv(ev)
end

function ItemUiShopGold:onClick()
    --self.ViewShop:setCurrentGold(self.TbDataItem)
end