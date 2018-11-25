-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewEnterMatchNotify = ViewBase:new()

---------------------------------------
function ViewEnterMatchNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    o.Tween = nil
    self.TimerUpdate = nil
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    return o
end

---------------------------------------
function ViewEnterMatchNotify:OnCreate()
    self.GTransitionShow = self.ComUi:GetTransition("TransitionShow")
    local btn_close = self.ComUi:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:close()
            end
    )
    local btn_enterNow = self.ComUi:GetChild("Lan_Btn_EnterNow").asButton
    btn_enterNow.onClick:Add(
            function()
                self:onClickBtnEnterNow()
            end
    )
    self.ComTips2 = self.ComUi:GetChild("ComTips2").asCom
    local btn_enter = self.ComTips2:GetChild("Lan_Btn_Enter").asButton
    btn_enter.onClick:Add(
            function()
                self:onClickBtnEnterNow()
            end
    )
    local btn_cancel = self.ComTips2:GetChild("Lan_Btn_Cancel").asButton
    btn_cancel.onClick:Add(
            function()
                self:closeComTips2()
            end
    )
    local com_bg = self.ComTips2:GetChild("ComBgAndClose").asCom
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:closeComTips2()
            end
    )
    self.GTextTips1 = self.ComUi:GetChild("TextTips").asTextField
    self.GTextTips2 = self.ComTips2:GetChild("TextTips").asTextField
    self.GGroupTip1 = self.ComUi:GetChild("GoupTip1").asGroup
    self.LeftMatchBeginSedonds = 0
    self.MatchGuid = nil
    self.MatchName = nil
    self.HasCloseComTips2 = false

    self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(200, self, self._timerUpdate)
end

---------------------------------------
function ViewEnterMatchNotify:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
end

---------------------------------------
function ViewEnterMatchNotify:Init(match_beginTime, match_guid, match_name)
    local left_time = match_beginTime - CS.System.DateTime.Now
    self.LeftMatchBeginSedonds = left_time.Minutes * 60 + left_time.Seconds
    self.MatchGuid = match_guid
    self.MatchName = match_name
end

---------------------------------------
function ViewEnterMatchNotify:_timerUpdate(tm)
    self.LeftMatchBeginSedonds = self.LeftMatchBeginSedonds - tm
    local temp_seconds = math.ceil(self.LeftMatchBeginSedonds)
    if (temp_seconds <= 0) then
        self:close()
    else
        local temp = string.format(self.ViewMgr.LanMgr:getLanValue("MatchBegineTips"), self.MatchName, self:formatTime(temp_seconds))
        if (temp_seconds > 20) then
            self.GTextTips1.text = temp
            if (self.GGroupTip1.visible == false) then
                self.GGroupTip1.visible = true
                self.GTransitionShow:Play()
            end
        elseif (temp_seconds > 0 and temp_seconds <= 20) then
            if (self.GGroupTip1.visible == true) then
                self.GTextTips1.text = temp
            end
            if (self.HasCloseComTips2 == false) then
                if (self.ComTips2.visible == false) then
                    self.ComTips2.visible = true
                    self.Tween = ViewHelper:PopUi(self.ComTips2)
                end
                self.GTextTips2.text = temp--table.concat(temp,"",1,6)
            end
        end
    end
end

---------------------------------------
function ViewEnterMatchNotify:formatTime(time_seconds)
    local temp = {}
    temp[1] = ""
    temp[2] = ""
    temp[3] = ""
    temp[4] = self.ViewMgr.LanMgr:getLanValue("Second")
    if (time_seconds >= 60) then
        temp[1] = tostring(math.floor(time_seconds / 60))
        temp[2] = self.ViewMgr.LanMgr:getLanValue("MinuteEx")
        temp[3] = tostring(time_seconds % 60)
    else
        temp[3] = tostring(time_seconds)
    end
    return table.concat(temp)
end

---------------------------------------
function ViewEnterMatchNotify:closeComTips2()
    self.ComTips2.visible = false
    self.HasCloseComTips2 = true
    if (self.GGroupTip1.visible == false) then
        self.ViewMgr:DestroyView(self)
    end
end

---------------------------------------
function ViewEnterMatchNotify:close()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewEnterMatchNotify:onClickBtnEnterNow()
    local ev = self.ViewMgr:GetEv("EvUiRequestEnterMatch")
    if (ev == nil) then
        ev = EvUiRequestEnterMatch:new(nil)
    end
    ev.MatchGuid = self.MatchGuid
    self.ViewMgr:SendEv(ev)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewEnterMatchNotifyFactory = ViewFactory:new()

---------------------------------------
function ViewEnterMatchNotifyFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
    return o
end

---------------------------------------
function ViewEnterMatchNotifyFactory:CreateView()
    local view = ViewEnterMatchNotify:new(nil)
    return view
end