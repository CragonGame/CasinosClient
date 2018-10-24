-- Copyright(c) Cragon. All rights reserved.

ViewGoldTree = ViewBase:new()

function ViewGoldTree:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
    if(self.Instance==nil)
	then
		self.ViewMgr = nil
		self.GoUi = nil
		self.ComUi = nil
		self.Panel = nil
		self.UILayer = nil
		self.InitDepth = nil
		self.ViewKey = nil
		self.Instance = o
	end

    return self.Instance
end

function ViewGoldTree:onCreate()
	ViewHelper:PopUi(self.ComUi)
	self.NextGetRewardTm = 0
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.ControllerPlayer = self.ViewMgr.ControllerMgr:GetController("Player")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
		function()
			self:onClickBtnReturn()
		end
	)
    self.ControllerTree = self.ComUi:GetController("ControllerTree")
    self.GBtnCharge = self.ComUi:GetChild("Lan_Btn_RecharegeNow").asButton
    self.GBtnCharge.onClick:Add(
		function()
			self:onClickBtnCharge()
		end
	)
    self.GProGoldTree = self.ComUi:GetChild("ProGoldTree").asProgress
    self.GBtnTakeGold = self.ComUi:GetChild("Lan_Btn_GetIn").asButton
    self.GBtnTakeGold.onClick:Add(
		function()
			self:onClickGetGold()
		end
	)
    self.GTextGoldLimit = self.ComUi:GetChild("TakeGoldLimit").asTextField
    self.GTextTakeGoldCountDownTime = self.ComUi:GetChild("TakeGoldCountDownTime").asTextField
    self.GTextTakeGoldTips = self.ComUi:GetChild("TakeGoldTips").asTextField
    self.GTextGoldTreeLevel = self.ComUi:GetChild("GoldTreeLevel").asTextField
    self.GTextMaxCanGetGold = self.ComUi:GetChild("MaxCanGetGold").asTextField
    self.ControllerTree.selectedIndex = 1
    self:setGoldTreeSnapShot(self.ControllerPlayer.BGrowData)
	local ev = self.ViewMgr.getEv("EvUiRequestGetGrowSnapShot")
	if(ev == nil)
	then
		ev = EvUiRequestGetGrowSnapShot:new(nil)
	end
	self.ViewMgr.sendEv(ev)
	self.ViewMgr:bindEvListener("EvEntityOnGrowRewardSnapshot",self)
end

function ViewGoldTree:onDestroy()
	self.ViewMgr:unbindEvListener(self)
end

function ViewGoldTree:onHandleEv(ev)
	if(ev ~= nil)
	then
		if(ev.EventName == "EvEntityOnGrowRewardSnapshot")
		then
			local grow_data = ev.grow_data
            self:setGoldTreeSnapShot(grow_data)
		end
	end
end

function ViewGoldTree:onUpdate(tm)
	if (self.NextGetRewardTm > 0)
	then
		self.NextGetRewardTm = self.NextGetRewardTm - tm
        if (self.NextGetRewardTm > 0)
		then
			local tm_format = CS.Casinos.UiHelperCasinos.FormatTmFromSecondToMinute(self.NextGetRewardTm, false)
            self.GTextTakeGoldCountDownTime.text = tm_format
		else
			self.GTextTakeGoldCountDownTime.text = ""
		end
	end
end

function ViewGoldTree:setGoldTreeSnapShot(grow_data)
	if (grow_data == nil)
	then
		return
	end
    self.BGrowData = grow_data
    self.NextGetRewardTm = self.BGrowData.NextGetRewardLeftTm
    local tips = ""
    if (self.BGrowData.NextGetRewardGold > 0)
	then
		self.GTextTakeGoldTips.text = self.ViewMgr.LanMgr:getLanValue("LaterAdd")
            .. UiChipShowHelper:getGoldShowStr(grow_data.NextGetRewardGold, self.ViewMgr.LanMgr.LanBase) .. self.ViewMgr.LanMgr:getLanValue("Chip")
	else
		tips = self.ViewMgr.LanMgr:getLanValue("MoneyTreeFullOfChips")
	end
	if(grow_data.CurLevel > 0)
	then
		self.ControllerTree.selectedIndex = 0
	else
		self.ControllerTree.selectedIndex = 1
	end
    self.GTextGoldTreeLevel.text = "LV." .. tostring(grow_data.CurLevel)
    local current_maxgetgold = self.ControllerPlayer:getCurrentLevelMaxGetGold(grow_data.CurLevel)
    local max_cangetgold = current_maxgetgold - grow_data.GetRewardGold
	self.GTextGoldLimit.text = grow_data.CanGetRewardGold .. self.ViewMgr.LanMgr:getLanValue("CurrentAvailable") ..
							   max_cangetgold .. self.ViewMgr.LanMgr:getLanValue("SurplusAvailable")
    self.GTextMaxCanGetGold.text = UiChipShowHelper:getGoldShowStr(current_maxgetgold, self.ViewMgr.LanMgr.LanBase)
	self.GBtnTakeGold.enabled = grow_data.CanGetRewardGold > 0
    local pro_value = 0
    if (max_cangetgold > 0)
	then
		pro_value = (grow_data.CanGetRewardGold) * 100 / max_cangetgold
	end
    self.GProGoldTree.value = pro_value
    if (max_cangetgold == 0)
	then
		tips = self.ViewMgr.LanMgr:getLanValue("ChipsFinishedTomorrowCom")
	end
    self.GTextTakeGoldCountDownTime.text = tips
end

function ViewGoldTree:onClickBtnCharge()
	self.ViewMgr:createView("Shop")
end

function ViewGoldTree:onClickGetGold()
	local ev = self.ViewMgr:getEv("EvUiRequestGetGrowReward")
	if(ev == nil)
	then
		ev = EvUiRequestGetGrowReward:new(nil)
	end
	self.ViewMgr:sendEv(ev)
end

function ViewGoldTree:onClickBtnReturn()
	self.ViewMgr:destroyView(self)
end
	

			

ViewGoldTreeFactory = ViewFactory:new()

function ViewGoldTreeFactory:new(o,ui_package_name,ui_component_name,
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

function ViewGoldTreeFactory:createView()	
	local view = ViewGoldTree:new(nil)	
	return view
end