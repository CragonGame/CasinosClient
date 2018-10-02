-- Copyright(c) Cragon. All rights reserved.

ViewBank = ViewBase:new()

function ViewBank:new(o)
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
	self.MinChip = 1
	self.TakeOutOrPutInPerGold = 1

    return o
end

function ViewBank:onCreate()
	ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("SafeBox"))
	self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.GControllerBank = self.ComUi:GetController("ControllerBank")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    self.GBtnClose = com_bg:GetChild("BtnClose").asButton
    self.GBtnClose.onClick:Add(
		function()
			self:onClickCloseBtn()
		end
	)
	local com_shade = com_bg:GetChild("ComShade").asCom
	com_shade.onClick:Add(
		function()
			self:onClickCloseBtn()
		end
	)
    self.GBtnSaveMoney = self.ComUi:GetChild("BtnSaveMoneyClose").asButton
    self.GBtnSaveMoney.onClick:Add(
		function()
			self:onClickSaveBtn()
		end
	)
    self.GBtnTakeMoney = self.ComUi:GetChild("BtnDrawMoneyClose").asButton
    self.GBtnTakeMoney.onClick:Add(
		function()
			self:onClickTakeOutBtn()
		end
	)
    self.GGroupPutIn = self.ComUi:GetChild("GroupPutIn").asGroup
    self.BankPutIn = Bank:new(nil,self, self.GGroupPutIn, self.TakeOutOrPutInPerGold)
    self.GGroupTakeOut = self.ComUi:GetChild("GroupTakeOut").asGroup
	self.BankTakeOut = Bank:new(nil,self, self.GGroupTakeOut, self.TakeOutOrPutInPerGold)
    self.GTextBottomPutIn = self.ComUi:GetChild("TextButtomPutIn").asTextField
    self.GTextTopPutIn = self.ComUi:GetChild("TextTopPutIn").asTextField
	self.GTextBottomTakeOut = self.ComUi:GetChild("TextBottomTakeOut").asTextField
    self.GTextTopTakeOut = self.ComUi:GetChild("TextTopTakeOut").asTextField
    self:setChipInfo()
	self.ViewMgr:bindEvListener("EvEntityGoldChanged",self)
	self.ViewMgr:bindEvListener("EvEntityBankGoldChange",self)
end

function ViewBank:onDestroy()
	self.ViewMgr:unbindEvListener(self)
end

function ViewBank:setBankOperateType(operate_type)
	if(operate_type == CS.Casinos._eFairyGUIBankOperateType.PutChipInBank)
	then
		self:onClickSaveBtn()
	elseif(operate_type == CS.Casinos._eFairyGUIBankOperateType.TakeChipFromBank)
	then
		self:onClickTakeOutBtn()
	end
end

function ViewBank:onHandleEv(ev)
	if(ev.EventName == "EvEntityGoldChanged")
	then
		if (self.BankPutIn ~= nil)
		then
			self.BankPutIn:refreshPlayerGold()
		end
        if (self.BankTakeOut ~= nil)
		then
			self.BankTakeOut:refreshPlayerGold()
		end
	elseif(ev.EventName == "EvEntityBankGoldChange")
	then
		if (self.BankPutIn ~= nil)
		then
			self.BankPutIn:refreshPlayerGold()
		end
        if (self.BankTakeOut ~= nil)
		then
			self.BankTakeOut:refreshPlayerGold()
		end
	end
end

function ViewBank:setChipInfo()
	self.mSelfGold = self.ControllerActor.PropGoldAcc:get()
    self.mBankGold = self.ControllerActor.PropGoldBank:get()
	local min_chip = self.MinChip
    if (min_chip > self.mSelfGold)
	then
		min_chip = self.mSelfGold
	end
    self.GTextTopPutIn.text = UiChipShowHelper:getGoldShowStr(self.mSelfGold, self.ViewMgr.LanMgr.LanBase)
    self.GTextBottomPutIn.text = UiChipShowHelper:getGoldShowStr(min_chip, self.ViewMgr.LanMgr.LanBase)
    self.GTextTopTakeOut.text = UiChipShowHelper:getGoldShowStr(self.mBankGold, self.ViewMgr.LanMgr.LanBase)
    self.GTextBottomTakeOut.text = UiChipShowHelper:getGoldShowStr(min_chip, self.ViewMgr.LanMgr.LanBase)
end

function ViewBank:onClickSaveBtn()
	self.GControllerBank.selectedPage = "PutInMoney"
end

function ViewBank:onClickTakeOutBtn()
	self.GControllerBank.selectedPage = "TakeOutMoney"
end

function ViewBank:onClickCloseBtn()
	self.ViewMgr:destroyView(self)
end



Bank = {}
function Bank:new(o,safe_box,group,takeout_putin_pergold)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
	o.ViewBank = safe_box
	o.TakeOutOrPutInPerGold = takeout_putin_pergold
	local com = o.ViewBank.ComUi
	o.GTextBankTakeGold = com:GetChildInGroup(group, "SafeBoxGold").asTextField
	o.GTextPlayerTakeGold = com:GetChildInGroup(group, "PlayerGold").asTextField
	local put_in = com:GetChildInGroup(group, "Lan_Btn_SaveChips")
	if (put_in ~= nil)
	then
		o.GBtnPutIn = put_in.asButton
		o.GBtnPutIn.onClick:Add(
			function()
				o:onClickPutIn()
			end
		)
	end
	local take_out = com:GetChildInGroup(group, "Lan_Btn_TakeChips")
	if (take_out ~= nil)
	then
		o.GBtnTakeOut = take_out.asButton
        o.GBtnTakeOut.onClick:Add(
			function()
				o:onClickTakeOut()
			end
		)
	end
    o.GTextBankTakeGold.text = UiChipShowHelper:getGoldShowStr(o.ViewBank.ControllerActor.PropGoldBank:get(), o.ViewBank.ViewMgr.LanMgr.LanBase)
    o.GTextPlayerTakeGold.text = UiChipShowHelper:getGoldShowStr(o.ViewBank.ControllerActor.PropGoldAcc:get(), o.ViewBank.ViewMgr.LanMgr.LanBase)
    local co_putin = com:GetChildInGroup(group, "CoNumOperatePutIn")
    if (co_putin ~= nil)
	then
		local co_numoperateputin = co_putin.asCom
        o.ItemNumOperatePutIn = ItemNumOperate:new(nil,co_numoperateputin, 0, 1, false,o.ViewBank.ViewMgr.LanMgr)
	end
    local co_takeout = com:GetChildInGroup(group, "CoNumOperateTakeOut")
    if (co_takeout ~= nil)
	then
		local co_numoperatetakeput = co_takeout.asCom
        o.ItemNumOperateTakeOut = ItemNumOperate:new(nil,co_numoperatetakeput, 0, 1, false,o.ViewBank.ViewMgr.LanMgr)
	end
	return o
end

function Bank:refreshPlayerGold()
	local gold_acc = self.ViewBank.ControllerActor.PropGoldAcc:get()
    self.GTextPlayerTakeGold.text = UiChipShowHelper:getGoldShowStr(gold_acc, self.ViewBank.ViewMgr.LanMgr.LanBase)
    local gold_bank = self.ViewBank.ControllerActor.PropGoldBank:get()
    self.GTextBankTakeGold.text = UiChipShowHelper:getGoldShowStr(gold_bank, self.ViewBank.ViewMgr.LanMgr.LanBase)
end

function Bank:onClickPutIn()
	local input_num = self.ItemNumOperatePutIn:getCurrentNum()
    if (input_num <= 0)
	then
		ViewHelper:UiShowInfoSuccess(self.ViewBank.ViewMgr.LanMgr:getLanValue("DepositTips"))
        return
	else
		if (input_num * self.TakeOutOrPutInPerGold > self.ViewBank.ControllerActor.PropGoldAcc:get())
		then
			ViewHelper:UiShowInfoSuccess(self.ViewBank.ViewMgr.LanMgr:getLanValue("DepositTips1"))
            return
		end
	end
	local ev = self.ViewBank.ViewMgr:getEv("EvUiRequestBankDeposit")
	if(ev == nil)
	then
		ev = EvUiRequestBankDeposit:new(nil)
	end
	ev.deposit_chip = input_num * self.TakeOutOrPutInPerGold
    self.ViewBank.ViewMgr:sendEv(ev)
    self.ItemNumOperatePutIn:setCurrentNum(0)
end

function Bank:onClickTakeOut()
	local takeout_num = self.ItemNumOperateTakeOut:getCurrentNum()
    if (takeout_num <= 0)
	then
		 ViewHelper:UiShowInfoSuccess(self.ViewBank.ViewMgr.LanMgr:getLanValue("GetoutAmountTips"))
         return
	else
		if (takeout_num * self.TakeOutOrPutInPerGold > self.ViewBank.ControllerActor.PropGoldBank:get())
		then
			ViewHelper:UiShowInfoSuccess(self.ViewBank.ViewMgr.LanMgr:getLanValue("GetoutAmountTips1"))
            return
		end
	end
	local ev = self.ViewBank.ViewMgr:getEv("EvUiRequestBankWithdraw")
	if(ev == nil)
	then
		ev = EvUiRequestBankWithdraw:new(nil)
	end
    ev.withdraw_chip = takeout_num * self.TakeOutOrPutInPerGold
	self.ViewBank.ViewMgr:sendEv(ev)
    self.ItemNumOperateTakeOut:setCurrentNum(0)
end

SendGold = {}

function SendGold:new(o,safe_box,group)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
	self.TakeOutOrPutInPerGold = 10000
    self.CanReSendTm = 3
	self.ViewBank = safe_box
    local com = self.ViewBank.ComUi
    self.GTextPlayerTakeGold = com:GetChildInGroup(group, "PlayerGold").asTextField
    self.GInputSendGold = com:GetChildInGroup(group, "SendGold").asTextInput
    self.GInputTargetId = com:GetChildInGroup(group, "TargetId").asTextInpu
    self.GBtnConfirmSend = com:GetChildInGroup(group, "BtnConfirmSend").asButton
    self.GBtnConfirmSend.onClick:Add(
		function()
			self:onClickSend()
		end
	)
    local confirm_tips = com:GetChildInGroup(group, "ConfirmTips")
    if (confirm_tips ~= nil)
	then
		self.GTextConfirmTips = confirm_tips.asTextField
	end
    self.GTextPlayerTakeGold.text = UiChipShowHelper:getGoldShowStr(self.ViewBank.ControllerActor.PropGoldAcc:get(), self.ViewBank.ViewMgr.LanMgr.LanBase)
    self.ConfirmSend = false
    self.ConfirmSendTm = self.CanReSendTm
	return o
end

function SendGold:update(tm)
	if (self.ConfirmSend)
	then
		self.ConfirmSendTm = self.ConfirmSendTm - tm
        self:sendConfirmTips(self.ConfirmSendTm)
        if (self.ConfirmSendTm <= 0)
		then
			self.ConfirmSend = false
            self.ConfirmSendTm = self.CanReSendTm
            self.GBtnConfirmSend.enabled = true
            self.GTextConfirmTips.visible = false
		end
	end
end

function SendGold:refreshPlayerTakeGold()
	self.GTextPlayerTakeGold.text = UiChipShowHelper:getGoldShowStr(self.ViewBank.ControllerActor.PropGoldAcc:get(), self.ViewBank.CasinosContext.LanMgr.LanBase)
end

function SendGold:onClickSend()
	local send_gold = 0
	local n = tonumber(self.GInputSendGold.text)
	if(n == nil)
	then
		ViewHelper:UiShowInfoSuccess(self.ViewBank.ViewMgr.LanMgr:getLanValue("EnterNumTips"))
        return
	else
		send_gold = n
		if(send_gold <= 0)
		then
			ViewHelper:UiShowInfoSuccess(self.ViewBank.ViewMgr.LanMgr:getLanValue("SendGoldTips"))
			return	
		end
	end
	if((self.GInputTargetId.text == nil or #self.GInputTargetId.text <= 0) or self.GInputTargetId.text == "0")
	then
		ViewHelper:UiShowInfoSuccess(self.ViewBank.ViewMgr.LanMgr:getLanValue("EnterIdTips"))
        return
	end
	local ev = self.ViewBank.ViewMgr:getEv("EvUiClickConfirmChipTransaction")
	if(ev == nil)
	then
		ev = EvUiClickConfirmChipTransaction:new(nil)
	end
	ev.chip = send_gold * self.TakeOutOrPutInPerGold
    ev.send_target_etguid = self.GInputTargetId.text
	self.ViewBank.ViewMgr:sendEv(ev)
    self.GBtnConfirmSend.enabled = false
	self.ConfirmSend = true
    self:sendConfirmTips(self.CanReSendTm)
end

function SendGold:sendConfirmTips(tm)
	if (self.GTextConfirmTips ~= nil)
	then
		if(self.GTextConfirmTips.visible == false)
		then
			self.GTextConfirmTips.visible = true
		end
        self.GTextConfirmTips.text = tostring(math.ceil(tm))
	end
end


ExchangeCode = {}

function ExchangeCode:new(o,safe_box,group)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
	self.ViewBank = safe_box
    local com = self.ViewBank.ComUi
    local co_inputacc = com:GetChildInGroup(group, "CoInputExChangeCodeAcc").asCom
    self.GInputExchangeCodeAcc = co_inputacc:GetChild("InputText").asTextInput
    local co_inputpwd = com:GetChildInGroup(group, "CoInputExChangeCodePwd").asCom
    self.GInputExchangeCodePwd = co_inputpwd:GetChild("InputText").asTextInput
    self.GBtnConfirmSend = com:GetChildInGroup(group, "BtnExchangeNow").asButton
    self.GBtnConfirmSend.onClick:Add(
		function()
			self:onClickExchange()
		end
	)
end

function ExchangeCode:onClickExchange()
	if(self.GInputExchangeCodeAcc.text == nil or #self.GInputExchangeCodeAcc.text <= 0)
	then
		ViewHelper:UiShowInfoSuccess(self.ViewBank.ViewMgr.LanMgr:getLanValue("EnterCardNumTips"))
        return
	end
	if(self.GInputExchangeCodePwd.text == nil or #self.GInputExchangeCodePwd.text <= 0)
	then
		ViewHelper:UiShowInfoSuccess(self.ViewBank.ViewMgr.LanMgr:getLanValue("EnterPwdTips"))
        return
	end
	local ev = self.ViewBank.ViewMgr:getEv("EvRequesetGetExchangeCodeItem")
	if(ev == nil)
	then
		ev = EvRequesetGetExchangeCodeItem:new(nil)
	end
    ev.acc = self.GInputExchangeCodeAcc.text
    ev.pwd = self.GInputExchangeCodePwd.text
    self.ViewBank.ViewMgr:sendEv(ev)
end



ViewBankFactory = ViewFactory:new()

function ViewBankFactory:new(o,ui_package_name,ui_component_name,
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

function ViewBankFactory:createView()	
	local view = ViewBank:new(nil)	
	return view
end