-- Copyright(c) Cragon. All rights reserved.

ViewDesktopHBankList = ViewBase:new()

function ViewDesktopHBankList:new(o)
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
    o.GListBeBankPlayerList = nil
    o.GTextWaitPlayerCount = nil
    o.GTextBankPlayerNickName = nil
    o.GTextBankPlayerGolds = nil
    o.GTextBeBankTips = nil
    o.GBtnBeBank = nil
    o.ControllerBeBank = nil
    o.GSliderBankPlayerStack = nil
    o.GTextBeBankPlayerGoldLimit = nil
    o.GTextBeBankSelfGold = nil
    o.CurrentTakeStack = 0
    o.MapBeBankPlayerInfo = nil
    o.BeBankMinTakeGolds = 0
    o.LeaveMingGolds = 0
    o.ViewDesktopH = nil

    return o
end

function ViewDesktopHBankList:onCreate()
	ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("BeBankList"))
    self.ViewMgr:bindEvListener("EvEntityDesktopHChangeBeBankerPlayerList",self)
    self.ViewMgr:bindEvListener("EvEntityDesktopHChangeBankerPlayer",self)
    self.ViewMgr:bindEvListener("EvEntityDesktopHBankerPlayerGoldChange",self)
	 local co_history_close = self.ComUi:GetChild("ComBgAndClose").asCom
     local btn_history_close = co_history_close:GetChild("BtnClose").asButton
     btn_history_close.onClick:Add(
		function()
			self:_onClickBtnBankPlayerClose()
		end
	 )
	 local com_shade = co_history_close:GetChild("ComShade").asCom
	 com_shade.onClick:Add(
		function()
			self:_onClickBtnBankPlayerClose()
		end
	 )
     local wait_count = self.ComUi:GetChild("BeBankPlayerCount")
     if (wait_count ~= nil)
     then
         self.GTextWaitPlayerCount = wait_count.asTextField
     end
     local bankplayer_nickname = self.ComUi:GetChild("BankPlayerNickName")
     if (bankplayer_nickname ~= nil)
     then
         self.GTextBankPlayerNickName = bankplayer_nickname.asTextField
         self.GTextBankPlayerGolds = self.ComUi:GetChild("BankPlayerGolds").asTextField
     end
     self.GTextBeBankTips = self.ComUi:GetChild("BeBankTips").asTextField
     self.GBtnBeBank = self.ComUi:GetChild("BeBank").asButton
     self.GBtnBeBank.onClick:Add(
             function()
                 self:_onClickBeBankPlayer()
                end)
     self.ControllerBeBank = self.ComUi:GetController("ControllerBeBank")
     self.GListBeBankPlayerList = self.ComUi:GetChild("ListPlayerInfo").asList
     local slider_bankstack = self.ComUi:GetChild("SliderBankStack")
     if (slider_bankstack ~= nil)
     then
         self.GSliderBankPlayerStack = slider_bankstack.asSlider
         self.GSliderBankPlayerStack.onChanged:Add(
                 function()
                     self:_pushStackValueChanged()
                    end)
         self.GSliderBankPlayerStack.enabled = false
         self.GTextBeBankPlayerGoldLimit = self.ComUi:GetChild("BankGoldLimit").asTextField
         self.GTextBeBankSelfGold = self.ComUi:GetChild("SelfGolds").asTextField
     end
     self.MapBeBankPlayerInfo = {}
end


function ViewDesktopHBankList:onDestroy()
    self.ViewMgr:unbindEvListener(self)
end

function ViewDesktopHBankList:onUpdate(elapsed_tm)
end

function ViewDesktopHBankList:onHandleEv(ev)   
	if(ev ~= nil)
	then
        if(ev.EventName == "EvEntityDesktopHChangeBeBankerPlayerList")
        then
            self:_initBeBankPlayer(ev.list_bebanker, ev.banker_player, ev.is_bankplayer)
        elseif(ev.EventName == "EvEntityDesktopHChangeBankerPlayer")
        then
            self:_initBeBankPlayer(ev.list_bebankplayer, ev.banker_player, ev.is_bankplayer)
		elseif(ev.EventName == "EvEntityDesktopHBankerPlayerGoldChange")
		then
            self:_updateBankPlayerInfo(ev.banker_player)
        end
	end
end

function ViewDesktopHBankList:initBeBankPlayer(desktoph, list_bebankplayer, bank_player, is_bankplayer)
   self.ViewDesktopH = desktoph
   local golds_info = self.ViewDesktopH.UiDesktopHBase:getBeBankPlayerMinGolds()
   self.BeBankMinTakeGolds = golds_info.MinTakeGolds
   self.LeaveMingGolds = golds_info.MinLeaveGolds
   self.CurrentTakeStack = UiChipShowHelper:getValideGold(self.BeBankMinTakeGolds)
   self:_initBeBankPlayer(list_bebankplayer, bank_player, is_bankplayer)
end

function ViewDesktopHBankList:_initBeBankPlayer(list_bebankplayer, bank_player, is_bankplayer)
   local have_player = is_bankplayer
   local self_data = nil
   if (list_bebankplayer ~= nil)
   then
       self.GListBeBankPlayerList:RemoveChildren()
       for i, v in pairs(list_bebankplayer) do
           local p = v
           if (p.PlayerInfoCommon.PlayerGuid ~= bank_player.PlayerInfoCommon.PlayerGuid)
           then
               if (p.PlayerInfoCommon.PlayerGuid == self.ViewDesktopH.ControllerDesktopH.Guid)
               then
                   have_player = true
                   self_data = p
               end

               local co_bebankplayerinfo = self.GListBeBankPlayerList:AddItemFromPool().asCom
               local player_info = ItemDesktopHBeBankPlayerInfo:new(nil,co_bebankplayerinfo, p,self.ViewMgr)
               self.MapBeBankPlayerInfo[p.PlayerInfoCommon.PlayerGuid] = player_info
           end
       end

       if (self.GTextWaitPlayerCount ~= nil)
       then
           self.GTextWaitPlayerCount.text = tostring(list_bebankplayer.Count)
       end
   end

    local l = 0
    if(have_player)
        then
        l = 1
    end
   self:_switchContrller(l)

   self:_updateBankPlayerInfo(bank_player)

   local min_takegolds = UiChipShowHelper:getGoldShowStr(self.BeBankMinTakeGolds, self.ViewMgr.LanMgr.LanBase)
   local min_leavegolds = UiChipShowHelper:getGoldShowStr(self.LeaveMingGolds, self.ViewMgr.LanMgr.LanBase)
   self.GTextBeBankTips.text = string.format(self.ViewMgr.LanMgr:getLanValue("ConditionOfBeBanker"), min_takegolds, min_leavegolds)
   if (self.GTextBeBankSelfGold ~= nil)
   then
       if (self_data ~= nil)
       then
           local value = (self_data.Gold - self.BeBankMinTakeGolds) * 100 / (self.ViewDesktopH.ControllerActor.PropGoldAcc:get() - self.BeBankMinTakeGolds)
           self.GSliderBankPlayerStack.value = value
           self.CurrentTakeStack = self_data.Gold
       end

       self:_setMinBeBankPlayerGold(true)

       self.GTextBeBankSelfGold.text = UiChipShowHelper:getGoldShowStr(self.ViewDesktopH.ControllerActor.PropGoldAcc:get(), self.ViewMgr.LanMgr.LanBase)
       local can_bebank = self.ViewDesktopH.ControllerActor.PropGoldAcc:get() >= self.BeBankMinTakeGolds
       if (can_bebank == false)
       then
           self.GTextBeBankSelfGold.color = CS.UnityEngine.Color.red
       end

       if (have_player == false and is_bankplayer == false)
       then
           self.GSliderBankPlayerStack.enabled = can_bebank
       end
   end
end

function ViewDesktopHBankList:_updateBankPlayerInfo(bank_player)
   if (bank_player ~= nil)
   then
       if (self.GTextBankPlayerNickName ~= nil)
       then
           self.GTextBankPlayerNickName.text = bank_player.PlayerInfoCommon.NickName

           local gold_str = UiChipShowHelper:getGoldShowStr(bank_player.Gold, self.ViewMgr.LanMgr.LanBase)
           if (DesktopHSysBankShowDBValue and
               CS.System.String.IsNullOrEmpty(bank_player.PlayerInfoCommon.PlayerGuid))
           then
               local sys_bank_initgold = self.ViewDesktopH.UiDesktopHBase:getSysBankPlayerInitGold()
               gold_str = UiChipShowHelper:getGoldShowStr(sys_bank_initgold, self.ViewMgr.LanMgr.LanBase)
           end
           self.GTextBankPlayerGolds.text = gold_str
       end
   end
end

function ViewDesktopHBankList:_pushStackValueChanged()
   self:_setMinBeBankPlayerGold(false)
end

function ViewDesktopHBankList:_setMinBeBankPlayerGold(is_init)
   if (is_init == false)
   then
       local slider_v = self.GSliderBankPlayerStack.value--math.ceil(self.GSliderBankPlayerStack.value)
       self.GSliderBankPlayerStack.value = slider_v
       local slider_value = slider_v / 100
       local current_v = (self.ViewDesktopH.ControllerActor.PropGoldAcc:get() - self.BeBankMinTakeGolds) * slider_value
       current_v = math.ceil(current_v)
       local current_value = current_v
       current_value = current_value + self.BeBankMinTakeGolds
       if (current_value >= self.BeBankMinTakeGolds)
       then
           if (slider_value == 1)
           then
               self.CurrentTakeStack = current_value           
           else           
               self.CurrentTakeStack = UiChipShowHelper:getValideGold(current_value)
           end
           self.GTextBeBankPlayerGoldLimit.text = UiChipShowHelper:getGoldShowStr(self.CurrentTakeStack, self.ViewMgr.LanMgr.LanBase)
       end
   else
       self.GTextBeBankPlayerGoldLimit.text = UiChipShowHelper:getGoldShowStr(self.CurrentTakeStack, self.ViewMgr.LanMgr.LanBase)
   end
end

function ViewDesktopHBankList:_switchContrller(index)
   self.ControllerBeBank.selectedIndex = index
end

function ViewDesktopHBankList:_onClickBeBankPlayer()
	local ev = self.ViewMgr:getEv("EvDesktopHClickBeBankPlayerBtn")
	if(ev == nil)
	then
		ev = EvDesktopHClickBeBankPlayerBtn:new(nil)
	end	
	ev.bebank_mingolds = self.BeBankMinTakeGolds
	ev.take_stack = self.CurrentTakeStack -- == 0 ? self.ViewDesktopH.ControllerActor.PropGoldAcc.get() : CurrentTakeStack
	self.ViewMgr:sendEv(ev)

    if (self.GSliderBankPlayerStack ~= nil)
    then
        self.GSliderBankPlayerStack.enabled = false
    end
end

function ViewDesktopHBankList:_onClickBtnBankPlayerClose()        
   self.ViewMgr:destroyView(self)
end



ViewDesktopHBankListFactory = ViewFactory:new()

function ViewDesktopHBankListFactory:new(o,ui_package_name,ui_component_name,
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

function ViewDesktopHBankListFactory:createView()	
	local view = ViewDesktopHBankList:new(nil)	
	return view
end