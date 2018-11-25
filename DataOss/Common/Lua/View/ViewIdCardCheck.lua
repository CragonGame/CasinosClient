-- Copyright(c) Cragon. All rights reserved.
-- 身份证验证对话框

---------------------------------------
ViewIdCardCheck = ViewBase:new()

---------------------------------------
function ViewIdCardCheck:new(o)
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
    return o
end

---------------------------------------
function ViewIdCardCheck:OnCreate()
    local com_shade = self.ComUi:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickBtnCancel()
            end
    )
    self.BtnConfirm = self.ComUi:GetChild("Lan_Btn_Confirm").asButton
    self.BtnConfirm.onClick:Add(
            function()
                self:onClickBtnOK()
            end
    )

    local com_tips = self.ComUi:GetChild("ComInputName").asCom
    self.TextName = com_tips:GetChild("InputText").asTextField
    self.TextName.promptText = string.format("[color=#999999]%s[/color]", self.ViewMgr.LanMgr:getLanValue("EnterName"))
    self.TextName.onChanged:Set(
            function()
                self:_checkIdCardInput()
            end
    )

    local com_id = self.ComUi:GetChild("ComInputId").asCom
    self.TextId = com_id:GetChild("InputText").asTextField
    self.TextId.promptText = string.format("[color=#999999]%s[/color]", self.ViewMgr.LanMgr:getLanValue("EnterIdCard"))
    self.TextId.onChanged:Set(
            function()
                self:_checkIdCardInput()
            end
    )
    self:_checkIdCardInput()
end

---------------------------------------
function ViewIdCardCheck:onClickBtnOK()
    self.ViewMgr:DestroyView(self)
    local ev = self.ViewMgr:GetEv("EvCheckIdCard")
    if (ev == nil) then
        ev = EvCheckIdCard:new(nil)
    end
    ev.name = self.TextName.text
    ev.id_card = self.TextId.text
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewIdCardCheck:onClickBtnCancel()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewIdCardCheck:_checkIdCardInput()
    if (self.TextName == nil or self.TextId == nil) then
        return
    end

    if ((self.TextName ~= nil and string.len(self.TextName.text) > 0) and (self.TextId ~= nil and string.len(self.TextId.text) > 0)) then
        self.BtnConfirm.alpha = 1
        self.BtnConfirm.enabled = true
    else
        self.BtnConfirm.alpha = 0.5
        self.BtnConfirm.enabled = false
    end
end

---------------------------------------
ViewIdCardCheckFactory = ViewFactory:new()

---------------------------------------
function ViewIdCardCheckFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewIdCardCheckFactory:CreateView()
    local view = ViewIdCardCheck:new(nil)
    return view
end