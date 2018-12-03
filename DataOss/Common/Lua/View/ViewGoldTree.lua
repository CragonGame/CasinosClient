-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewGoldTree = class(ViewBase)

---------------------------------------
function ViewGoldTree:ctor()
    self.Tween = nil
end

---------------------------------------
function ViewGoldTree:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi)
    self.NextGetRewardTm = 0
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerPlayer = self.ControllerMgr:GetController("Player")
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
    local ev = self.ViewMgr.GetEv("EvUiRequestGetGrowSnapShot")
    if (ev == nil) then
        ev = EvUiRequestGetGrowSnapShot:new(nil)
    end
    self.ViewMgr.SendEv(ev)
    self.ViewMgr:BindEvListener("EvEntityOnGrowRewardSnapshot", self)
end

---------------------------------------
function ViewGoldTree:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewGoldTree:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvEntityOnGrowRewardSnapshot") then
            local grow_data = ev.grow_data
            self:setGoldTreeSnapShot(grow_data)
        end
    end
end

---------------------------------------
function ViewGoldTree:onUpdate(tm)
    if (self.NextGetRewardTm > 0) then
        self.NextGetRewardTm = self.NextGetRewardTm - tm
        if (self.NextGetRewardTm > 0) then
            local tm_format = CS.Casinos.UiHelperCasinos.FormatTmFromSecondToMinute(self.NextGetRewardTm, false)
            self.GTextTakeGoldCountDownTime.text = tm_format
        else
            self.GTextTakeGoldCountDownTime.text = ""
        end
    end
end

---------------------------------------
function ViewGoldTree:setGoldTreeSnapShot(grow_data)
    if (grow_data == nil) then
        return
    end
    self.BGrowData = grow_data
    self.NextGetRewardTm = self.BGrowData.NextGetRewardLeftTm
    local tips = ""
    if (self.BGrowData.NextGetRewardGold > 0) then
        self.GTextTakeGoldTips.text = self.ViewMgr.LanMgr:getLanValue("LaterAdd")
                .. UiChipShowHelper:GetGoldShowStr(grow_data.NextGetRewardGold, self.ViewMgr.LanMgr.LanBase) .. self.ViewMgr.LanMgr:getLanValue("Chip")
    else
        tips = self.ViewMgr.LanMgr:getLanValue("MoneyTreeFullOfChips")
    end
    if (grow_data.CurLevel > 0) then
        self.ControllerTree.selectedIndex = 0
    else
        self.ControllerTree.selectedIndex = 1
    end
    self.GTextGoldTreeLevel.text = "LV." .. tostring(grow_data.CurLevel)
    local current_maxgetgold = self.ControllerPlayer:getCurrentLevelMaxGetGold(grow_data.CurLevel)
    local max_cangetgold = current_maxgetgold - grow_data.GetRewardGold
    self.GTextGoldLimit.text = grow_data.CanGetRewardGold .. self.ViewMgr.LanMgr:getLanValue("CurrentAvailable") ..
            max_cangetgold .. self.ViewMgr.LanMgr:getLanValue("SurplusAvailable")
    self.GTextMaxCanGetGold.text = UiChipShowHelper:GetGoldShowStr(current_maxgetgold, self.ViewMgr.LanMgr.LanBase)
    self.GBtnTakeGold.enabled = grow_data.CanGetRewardGold > 0
    local pro_value = 0
    if (max_cangetgold > 0) then
        pro_value = (grow_data.CanGetRewardGold) * 100 / max_cangetgold
    end
    self.GProGoldTree.value = pro_value
    if (max_cangetgold == 0) then
        tips = self.ViewMgr.LanMgr:getLanValue("ChipsFinishedTomorrowCom")
    end
    self.GTextTakeGoldCountDownTime.text = tips
end

---------------------------------------
function ViewGoldTree:onClickBtnCharge()
    self.ViewMgr:CreateView("Shop")
end

---------------------------------------
function ViewGoldTree:onClickGetGold()
    local ev = self.ViewMgr:GetEv("EvUiRequestGetGrowReward")
    if (ev == nil) then
        ev = EvUiRequestGetGrowReward:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewGoldTree:onClickBtnReturn()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewGoldTreeFactory = class(ViewFactory)

---------------------------------------
function ViewGoldTreeFactory:CreateView()
    local view = ViewGoldTree:new()
    return view
end