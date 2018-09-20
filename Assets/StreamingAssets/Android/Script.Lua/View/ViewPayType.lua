-- Copyright(c) Cragon. All rights reserved.
-- 选择支付宝微信支付对话框

ViewPayType = ViewBase:new()

function ViewPayType:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.ViewMgr = nil
	o.GoUi = nil
	o.ComUi = nil
	o.Panel = nil
	o.UILayer = nil
	o.InitDepth = nil
	o.ViewKey = nil

	return o
end

function ViewPayType:onCreate()
	ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("PayType"))
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.ControllerPay = self.ComUi:GetController("ControllerPay")
	local group_notios = self.ComUi:GetChild("GroupNotIOS").asGroup
	local group_ios = self.ComUi:GetChild("GroupIOS").asGroup
	local group_parent = group_ios
	self.ControllerPay.selectedIndex = 0
	--if(self.CasinosContext.UnityAndroid)
	--then
		group_parent = group_notios
		self.ControllerPay.selectedIndex = 1
	--end
	self.GBtnWeiChat = self.ComUi:GetChildInGroup(group_parent, "BtnWeiChat").asButton
	self.GBtnWeiChat.onClick:Add(
			function()
				self:onClickWeiChat()
			end
	)
	ViewHelper:setGObjectVisible(UseWeiChatPay, self.GBtnWeiChat)
	self.GBtnZhiFuBao = self.ComUi:GetChildInGroup(group_parent, "BtnAliPay").asButton
	self.GBtnZhiFuBao.onClick:Add(
			function()
				self:onClickZhiFuBao()
			end
	)
	ViewHelper:setGObjectVisible(UseALiPay, self.GBtnZhiFuBao)
	local btn = self.ComUi:GetChild("BtnNeiGou").asButton
	btn.onClick:Add(
			function()
				self:onClickNeiGou()
			end
	)
	ViewHelper:setGObjectVisible(UseIAP, btn)
	local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
	local co_shade = com_bg:GetChild("ComShade").asCom
	co_shade.onClick:Add(
			function()
				self.ViewMgr:destroyView(self)
			end
	)
end

function ViewPayType:BuyItem(is_first_recharge,tb_id)
	self.IsFirstRecharge = is_first_recharge
	self.BuyItemTbId = tb_id
end

function ViewPayType:onClickWeiChat()
	self:buyItem("wx")
end

function ViewPayType:onClickZhiFuBao()
	self:buyItem("alipay")
end

function ViewPayType:onClickNeiGou()
	self:buyItem("iap")
end

function ViewPayType:buyItem(pay_type)
	local ev = self.ViewMgr:getEv("EvUiRequestBuyItem")
	if(ev == nil)
	then
		ev = EvUiRequestBuyItem:new(nil)
	end
	ev.item_count = 1
	ev.item_tbid = self.BuyItemTbId
	ev.is_firstrecharge = self.IsFirstRecharge
	ev.pay_type = pay_type
	self.ViewMgr:sendEv(ev)
	self.ViewMgr:destroyView(self)
end



ViewPayTypeFactory = ViewFactory:new()

function ViewPayTypeFactory:new(o,ui_package_name,ui_component_name,
								ui_layer,is_single,fit_screen)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	self.PackageName = ui_package_name
	self.ComponentName = ui_component_name
	self.UILayer = ui_layer
	self.IsSingle = is_single
	self.FitScreen = fit_screen
	return o
end

function ViewPayTypeFactory:createView()
	local view = ViewPayType:new(nil)
	return view
end