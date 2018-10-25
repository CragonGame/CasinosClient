-- Copyright(c) Cragon. All rights reserved.
-- 商城，消耗品

ItemUiShopConsume = {}

function ItemUiShopConsume:new(o,view_shop,com,tb_item)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
	o.ViewShop = view_shop
	o.GComItem = com
    o.TbDataItem = tb_item
    o.GTextConsumeName = o.GComItem:GetChild("ConsumeName").asTextField
    o.GTextConsumeName.text = view_shop.ViewMgr.LanMgr:getLanValue(tb_item.Name)
    
    o.GLoaderConsume = o.GComItem:GetChild("LoaderConsume").asLoader
    o.GLoaderConsume.icon = CS.FairyGUI.UIPackage.GetItemURL(view_shop.ViewMgr.LanMgr:getLanPackageName(), tb_item.Icon)
    o.GBtnBuy = o.GComItem:GetChild("BtnBuy").asButton
	o.GTextConsumeCount = o.GBtnBuy:GetChild("Price").asTextField
    o.GTextConsumeCount.text = UiChipShowHelper:getGoldShowStr(tb_item.Price, view_shop.ViewMgr.LanMgr.LanBase)
    o.GBtnBuy.onClick:Add(
		function()
			o:onClickBtnBuy()
		end
	)
	return o
end

function ItemUiShopConsume:onClickBtnBuy()
	local ev = self.ViewShop.ViewMgr:GetEv("EvUiBuyItem")
	if(ev == nil)
	then
		ev = EvUiBuyItem:new(nil)
	end
	ev.item_id = self.TbDataItem.Id
	local controller_player = self.ViewShop.ViewMgr.ControllerMgr:GetController("Player")
	ev.to_etguid = controller_player.Guid
    self.ViewShop.ViewMgr:SendEv(ev)
end