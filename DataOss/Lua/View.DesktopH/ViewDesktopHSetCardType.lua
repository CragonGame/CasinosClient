-- Copyright(c) Cragon. All rights reserved.

ViewDesktopHSetCardType = ViewBase:new()

function ViewDesktopHSetCardType:new(o)
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
	o.ViewDesktopH = nil
	o.GCoBankCardTypeParent = nil
	o.GListPotCardType = nil
	o.GBtnConfirmSetCardType = nil
	o.BankItemDesktopHSetCardType = nil
	o.MapPotItemDesktopHSetCardType = nil
	o.MapCardsType = nil
	o.BankPlayerPotIndex = 255

	return o
end

function ViewDesktopHSetCardType:onCreate()
	self.ViewDesktopH = self.ViewMgr:GetView("DesktopH")
	self.GCoBankCardTypeParent = self.ComUi:GetChild("CoBankCardTypeParent").asCom
	self.GListPotCardType = self.ComUi:GetChild("ListPotCardType").asList
	self.GBtnConfirmSetCardType = self.ComUi:GetChild("BtnConfirm").asButton
	self.GBtnConfirmSetCardType.onClick:Add(
			function()
				self:_setCardType()
			end
	)
	local co_shade = self.ComUi:GetChild("CoShade").asCom
	co_shade.onClick:Add(
			function()
				self:onClickBtnClose()
			end
	)
	self.MapPotItemDesktopHSetCardType = {}
	self.MapCardsType = {}
	local co_bankcardtype = CS.FairyGUI.UIPackage.CreateObject("DesktopHSetCardType", "ComboBoxCardType").asComboBox
	self.GCoBankCardTypeParent:AddChild(co_bankcardtype)
	self.BankItemDesktopHSetCardType = ItemDesktopHSetCardType:new(nil)
	self.BankItemDesktopHSetCardType:onCreate(co_bankcardtype, self.BankPlayerPotIndex, self.ViewDesktopH, self)

	for  i = 0, self.ViewDesktopH.ControllerDesktopH.DesktopHBase:getMaxBetpotIndex() do
		local co_item = self.GListPotCardType:AddItemFromPool().asComboBox
		local l = ItemDesktopHSetCardType:new(nil)
		l:onCreate(co_item, i, self.ViewDesktopH, self)
		self.MapPotItemDesktopHSetCardType[i] = l
	end
end

function ViewDesktopHSetCardType:setCurrentType(pot_index, card_type)
	self.MapCardsType[tostring(pot_index)] = card_type
end

function ViewDesktopHSetCardType:_setCardType()
	if (self.ViewDesktopH.ControllerPlayer.IsGm == false)
	then
		return
	end

	local ev = self.ViewMgr:GetEv("EvDesktopHundredChangeCardsType")
	if(ev == nil)
	then
		ev  = EvDesktopHundredChangeCardsType:new(nil)
	end
	ev.map_card_types = self.MapCardsType
	self.ViewMgr:SendEv(ev)

	self:onClickBtnClose()
end

function ViewDesktopHSetCardType:onClickBtnClose()
	self.ViewMgr:DestroyView(self)
end


ViewDesktopHSetCardTypeFactory = ViewFactory:new()

function ViewDesktopHSetCardTypeFactory:new(o,ui_package_name,ui_component_name,
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

function ViewDesktopHSetCardTypeFactory:CreateView()
	local view = ViewDesktopHSetCardType:new(nil)
	return view
end