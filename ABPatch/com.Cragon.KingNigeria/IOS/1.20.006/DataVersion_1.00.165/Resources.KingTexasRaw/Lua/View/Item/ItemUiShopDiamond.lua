ItemUiShopDiamond = {}

function ItemUiShopDiamond:new(o,view_shop,diamond,tb_item)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    o.ViewShop = view_shop
    o.GComDiamond = diamond
    o.GTextDiamond = o.GComDiamond:GetChild("DiamondValue").asTextField
    o.GLoaderIcon = o.GComDiamond:GetChild("LoaderDiamond").asLoader
    o.GBtnBuy = o.GComDiamond:GetChild("BtnBuy").asButton
	o.GTextPrice = o.GBtnBuy:GetChild("Price").asTextField
    o.GBtnBuy.onClick:Add(
        function()
            o:onClickBtnBuy()
        end
    )
    o.TbDataItem = tb_item
    local coin = "钻石"
    if (CS.Casinos.CasinosContext.Instance.UseLan)
    then
        coin = view_shop.ViewMgr.LanMgr:getLanValue("钻石","Diamonds")
    end
    local tb_unit = CS.Casinos.CasinosContext.Instance.TbDataMgrLua:GetData("UnitBilling",tb_item.Id)
    o.GTextDiamond.text = tostring(tb_unit.Amount + tb_unit.Bonus)..coin
    o.GTextPrice.text = tb_item.Price
    o.GLoaderIcon.icon = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath
                (CS.Casinos.UiHelperCasinos.getABItemResourceTitlePath() .. string.lower(tb_item.Icon) .. ".ab")
    return o
end

function ItemUiShopDiamond:onClickBtnBuy()
    local ev = self.ViewShop.ViewMgr:getEv("EvUiRequestBuyDiamond")
    if(ev == nil)
    then
        ev = EvUiRequestBuyDiamond:new(nil)
    end
    print("ItemUiShopDiamond:onClickBtnBuy      "..self.TbDataItem.Id)
    ev.buy_diamondid = self.TbDataItem.Id
    self.ViewShop.ViewMgr:sendEv(ev)
end