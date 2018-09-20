-- 换钱对话框

ViewChipOperate = ViewBase:new()

function ViewChipOperate:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	self.ViewMgr = nil
	self.GoUi = nil
	self.ComUi = nil
	self.Panel = nil
	self.UILayer = nil
	self.InitDepth = nil
	self.ViewKey = nil

    return o
end

function ViewChipOperate:onCreate()
	ViewHelper:PopUi(self.ComUi)
	self.ViewMgr:bindEvListener("EvEntityPlayerGiveChipQueryRangeRequestResult",self)
	local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
	local btn_close = com_bg:GetChild("BtnClose").asButton
	btn_close.onClick:Add(
		function()
			self:onClose()
		end
	)
	local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
		function()
			self:onClose()
		end
	)
    self.GBtnConfirm = self.ComUi:GetChild("Lan_Btn_Confirm").asButton
    self.GBtnConfirm.onClick:Add(
		function()
			self:onConfirmBet()
		end
	)
    local com_slider = self.ComUi:GetChild("ComSlider").asCom
    self.GSlider = com_slider:GetChild("Slider").asSlider
    self.GBtnWheelPlus = com_slider:GetChild("BtnWheelPlus").asButton
    self.GBtnWheelPlus.onClick:Add(
		function()
			self:onClickPlus()
		end
	)
    self.GBtnWheelMinus = com_slider:GetChild("BtnWheelMinus").asButton
    self.GBtnWheelMinus.onClick:Add(
		function()
			self:onClickMinus()
		end
	)
    self.GTextPlusTips = com_slider:GetChild("TextPlusTips").asTextField
    self.GTextMinusTips = com_slider:GetChild("TextMinusTips").asTextField
    self.GTextCurrentValue = self.GSlider:GetChild("TextCurrentValue").asTextField
    self.GTextTitle = self.ComUi:GetChild("TextTitle").asTextField
    self.GTextFieldGetChipsTips = self.ComUi:GetChild("Lan_Text_AskingAvailableBalance").asTextField
    self.GTextTipsLeft = self.ComUi:GetChild("TextTipsLeft").asTextField
    self.GTextTipsRight = self.ComUi:GetChild("TextTipsRight").asTextField
    self.GTextFieldGetChipsTips.visible = false

	self.ControllerOperate = self.ComUi:GetController("ControllerOperate")
	self.TextInput = self.ComUi:GetChild("TextInput").asTextInput
end

function ViewChipOperate:onHandleEv(ev)
	if (ev.EventName == "EvEntityPlayerGiveChipQueryRangeRequestResult")
	then
        self:setChips(ev.give_chip_max, ev.give_chip_min)
        self.GTextFieldGetChipsTips.visible = false
        self.GBtnConfirm.enabled = ev.is_success
        self.GBtnWheelPlus.enabled = ev.is_success
        self.GBtnWheelMinus.enabled = ev.is_success
		self.GSlider.enabled = ev.is_success
	end
end

function ViewChipOperate:onDestroy()
	self.ViewMgr:unbindEvListener(self)
end

function ViewChipOperate:setChipsInfo(left_chip,desk_top_chip,desk_bottom_chip,operate_type,param,action)
	self.ChipOperateType = operate_type
    self.ActionOk = action
    self.Param = param
    self.LeftGold = left_chip
    local title = ""
    local tips_left = ""
    local tips_right = ""
	local select = 0
	if(self.ChipOperateType == CS.Casinos._eChipOperateType.BetGame)
	then
		self.GTextSelfLeft = self.ComUi:GetChild("TextChipValue").asTextField
		title = self.ViewMgr.LanMgr:getLanValue("BettingGame")
        tips_left = self.ViewMgr.LanMgr:getLanValue("LowerDesk")
        tips_right = self.ViewMgr.LanMgr:getLanValue("UpperDesk")
        self:setChips(desk_top_chip, desk_bottom_chip)
	elseif(self.ChipOperateType == CS.Casinos._eChipOperateType.Exchange)
	then
		self.GTextSelfLeft = self.ComUi:GetChild("TextChipValue").asTextField
		title = self.ViewMgr.LanMgr:getLanValue("ExchangeMoney")
        tips_left = self.ViewMgr.LanMgr:getLanValue("LowerExchange")
        tips_right = self.ViewMgr.LanMgr:getLanValue("UpperExchange")
        self:setChips(desk_top_chip, desk_bottom_chip)
	elseif(self.ChipOperateType == CS.Casinos._eChipOperateType.Transaction)
	then
		self.GTextSelfLeft = self.ComUi:GetChild("TextChipValueInput").asTextField
		self.GTextFieldGetChipsTips.visible = true
        title = self.ViewMgr.LanMgr:getLanValue("BargainingChip")
        tips_left = self.ViewMgr.LanMgr:getLanValue("LowerDeal")
        tips_right = self.ViewMgr.LanMgr:getLanValue("UpperDeal")
		select = 1
	end
	self.ControllerOperate.selectedIndex = select
	ViewHelper:SetUiTitle(self.GTextTitle,title)
    self.GTextTipsLeft.text = tips_left
    self.GTextTipsRight.text = tips_right
end

function ViewChipOperate:setChips(desk_top_chip,desk_bottom_chip)
	local top_chip = desk_top_chip
    local bottom_chip = desk_bottom_chip
    if (top_chip > self.LeftGold)
	then
		top_chip = self.LeftGold
	end

    self.GTextPlusTips.text = UiChipShowHelper:getGoldShowStr(top_chip,self.ViewMgr.LanMgr.LanBase)
    self.GTextMinusTips.text = UiChipShowHelper:getGoldShowStr(bottom_chip,self.ViewMgr.LanMgr.LanBase)
	if(self.ChipOperateType == CS.Casinos._eChipOperateType.Transaction) then
		self.GTextSelfLeft.text = UiChipShowHelper:getGoldShowStr(self.LeftGold,self.ViewMgr.LanMgr.LanBase,false)
		self.BetGold = 0
	else
		self.GTextSelfLeft.text = self.ViewMgr.LanMgr:getLanValue("YourBalance") .. ":" .. UiChipShowHelper:getGoldShowStr(self.LeftGold,self.ViewMgr.LanMgr.LanBase)
		self.ViewSlideEx = ViewSlideEx:new(nil,self.GSlider,top_chip,bottom_chip,0.5,
				function(value)
					if(value > self.LeftGold)
					then
						self.BetGold = self.LeftGold
					else
						self.BetGold = value
					end
					self.BetGold = UiChipShowHelper:getValideGold(self.BetGold)
					self.GTextCurrentValue.text = UiChipShowHelper:getGoldShowStr(self.BetGold,self.ViewMgr.LanMgr.LanBase)
				end
		)
		if(self.ViewSlideEx.CurrentValueNum > self.LeftGold)
		then
			self.BetGold = self.LeftGold
		else
			self.BetGold = self.ViewSlideEx.CurrentValueNum
		end
	end

    self.BetGold = UiChipShowHelper:getValideGold(self.BetGold)
    self.GTextCurrentValue.text = UiChipShowHelper:getGoldShowStr(self.BetGold,self.ViewMgr.LanMgr.LanBase)
end

function ViewChipOperate:changeSlideValue(is_plus)
	self.ViewSlideEx:changeValue(is_plus, 0.1)
end

function ViewChipOperate:onConfirmBet()
	if (self.ChipOperateType == CS.Casinos._eChipOperateType.Transaction)
	then
		local success,value = CS.System.Int64.TryParse(self.TextInput.text)
		if success == false or value == 0 then
			return
		end
		self.BetGold = value
		if (self.Param ~= nil and self.Param ~= "")
		then
			local ev = self.ViewMgr:getEv("EvUiClickConfirmChipTransaction")
			if(ev == nil)
			then
				ev = EvUiClickConfirmChipTransaction:new(nil)
			end
            ev.send_target_etguid = self.Param
            ev.chip = self.BetGold
            self.ViewMgr:sendEv(ev)
		end
	end

    if (self.ActionOk ~= nil)
	then
		self.ActionOk(true, self.BetGold)
	end

    self.ViewMgr:destroyView(self)
end

function ViewChipOperate:onClose()
	if (self.ActionOk ~= nil)
	then
		self.ActionOk(false, 0)
	end

     self.ViewMgr:destroyView(self)
end

function ViewChipOperate:onClickPlus()
	self:changeSlideValue(true)
end

function ViewChipOperate:onClickMinus()
	self:changeSlideValue(false)
end




ViewChipOperateFactory = ViewFactory:new()

function ViewChipOperateFactory:new(o,ui_package_name,ui_component_name,
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

function ViewChipOperateFactory:createView()	
	local view = ViewChipOperate:new(nil)	
	return view
end




ViewSlideEx = {}

function ViewSlideEx:new(o,slider,top_num,bottom_num,slider_defaultvalue,slider_changeaction)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.UISlider = slider
	o.UISlider.max = 1
	o.TopNum = top_num
	o.BottomNum = bottom_num
	o.UISlider.value = slider_defaultvalue
	o.CurrentValueNum = (o.TopNum + o.BottomNum) * o.UISlider.value
	o.UISlider.onChanged:Add(
			function()
				o:sliderChange()
			end
	)
	o.SliderChangeAction = slider_changeaction

	return  o
end

function ViewSlideEx:changeValue(is_plus,change_value)
	local value = self.UISlider.value
	if (is_plus)
	then
		value = value + change_value
	else
		value = value - change_value
	end

	if (value > 1)
	then
		value = 1
	end

	if (value < 0)
	then
		value = 0
	end

	self.UISlider.value = value
	self:sliderChange()
end

function ViewSlideEx:sliderChange()
	local value = (self.TopNum - self.BottomNum) * self.UISlider.value
	value = value + self.BottomNum
	self.CurrentValueNum = value
	if (self.SliderChangeAction ~= nil)
	then
		self.SliderChangeAction(self.CurrentValueNum)
	end
end