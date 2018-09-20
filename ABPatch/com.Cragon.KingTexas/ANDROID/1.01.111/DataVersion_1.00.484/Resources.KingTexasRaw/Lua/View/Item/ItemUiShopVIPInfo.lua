ItemUiShopVIPInfo = {}

function ItemUiShopVIPInfo:new(o,view_shop,com,tb_viplevel)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
	o.ViewShop = view_shop
    o.GComVIP = com
    o.GTextLooseSendGoldPercent = o.GComVIP:GetChild("LooseSendGoldPercent").asTextField
    o.GTextSafeBox = o.GComVIP:GetChild("SafeBox").asTextField
    o.GTextChargeSend = o.GComVIP:GetChild("ChargeSend").asTextField
    o.ViewVIPSign = ViewVIPSign:new(nil,o.GComVIP:GetChild("CoVIP").asCom)
    o.ViewVIPSign:setVIPLevel(tb_viplevel.Level)
    o.GTextLooseSendGoldPercent.text = tb_viplevel.LooseSendGoldPercent .. view_shop.ViewMgr.LanMgr:getLanValue("Times")
    o.GTextSafeBox.text = UiChipShowHelper:getGoldShowStr(tb_viplevel.VIPPoint, view_shop.ViewMgr.LanMgr.LanBase, false)
    o.GTextChargeSend.text = tb_viplevel.ChargeSendPercent * 100 .. "%"

	return o
end

