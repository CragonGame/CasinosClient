-- Copyright(c) Cragon. All rights reserved.
-- MTT报名成功对话框提示

---------------------------------------
ViewApplySucceed = ViewBase:new()

---------------------------------------
function ViewApplySucceed:new(o)
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
    o.Instance = o
    o.Tween = nil
    return o
end

---------------------------------------
function ViewApplySucceed:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("SignUpSuccess"))
    local com_shade = self.ComUi:GetChild("ComBgAndClose").asCom
    com_shade.onClick:Add(
            function()
                self:close()
            end
    )
    local btn_ensure = self.ComUi:GetChild("Lan_Btn_Confirm").asButton
    btn_ensure.onClick:Add(
            function()
                self:close()
            end
    )
    self.GTextMatchName = self.ComUi:GetChild("TextMatchName").asTextField
    self.GTextEntryFee = self.ComUi:GetChild("TextEntryFee").asTextField
    self.GTextMatchBeginTime = self.ComUi:GetChild("TextMatchBeginTime").asTextField
end

---------------------------------------
function ViewApplySucceed:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewApplySucceed:SetMatchInfo(matchInfo)
    self.GTextMatchName.text = matchInfo.Name
    local text_SignUpfee = nil
    if (matchInfo.SignupFee == 0) then
        text_SignUpfee = self.ViewMgr.LanMgr:getLanValue("Free")
    else
        text_SignUpfee = tostring(matchInfo.SignupFee) .. "+" .. tostring(matchInfo.ServiceFee)
    end
    self.GTextEntryFee.text = text_SignUpfee
    local nowtm = CS.System.DateTime.Now
    local match_beginTm = matchInfo.DtMatchBegin
    local text_day = nil
    local day = match_beginTm.Day - nowtm.Day
    if (day == 0) then
        text_day = self.ViewMgr.LanMgr:getLanValue("Today")
    elseif (day == 1) then
        text_day = self.ViewMgr.LanMgr:getLanValue("Tomorrow")
    elseif (day == 2) then
        text_day = self.ViewMgr.LanMgr:getLanValue("AfterTomorrow")
    elseif (day > 2) then
        text_day = string.format("%02s%s%s", match_beginTm.Month, ".", tostring(match_beginTm.Day))
    end
    local text_time = string.format("%02s%s%02s", match_beginTm.Hour, ":", match_beginTm.Minute)
    self.GTextMatchBeginTime.text = text_day .. text_time
end

---------------------------------------
function ViewApplySucceed:close()
    self.ViewMgr:DestroyView(self)
    local ev = self.ViewMgr:GetEv("EvUiRequestPublicMatchList")
    if (ev == nil) then
        ev = EvUiRequestPublicMatchList:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
ViewApplySucceedFactory = ViewFactory:new()

---------------------------------------
function ViewApplySucceedFactory:new(o, ui_package_name, ui_component_name,
                                     ui_layer, is_single, fit_screen)
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
function ViewApplySucceedFactory:CreateView()
    local view = ViewApplySucceed:new(nil)
    return view
end