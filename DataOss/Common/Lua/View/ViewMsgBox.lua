-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewMsgBox = ViewBase:new()

---------------------------------------
function ViewMsgBox:new(o)
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
    return o
end

---------------------------------------
function ViewMsgBox:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi)
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickBtnCancel()
            end
    )
    local btn_ok = self.ComUi:GetChild("Lan_Btn_Confirm").asButton
    btn_ok.onClick:Add(
            function()
                self:onClickBtnOK()
            end
    )
    local text_title = self.ComUi:GetChild("TextTitle")
    if (text_title ~= nil) then
        self.TextTitle = text_title.asTextField
    end
    local com_tips = self.ComUi:GetChild("ComTips").asCom
    self.TextTips = com_tips:GetChild("Tips").asTextField
    self.AutoTm = 0
end

---------------------------------------
function ViewMsgBox:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewMsgBox:onUpdate(tm)
    if self.AutoTm > 0 then
        local auto_tm = self.AutoTm
        auto_tm = auto_tm - tm
        if self.BtnAuto ~= nil then
            self.BtnAuto.text = string.format(self.BtnTitle, math.floor(auto_tm))
        end
        self.AutoTm = auto_tm
        if auto_tm <= 0 then
            if self.AutoActionCancel ~= nil then
                self.ViewMgr:DestroyView(self)

                self.AutoActionCancel()
                self.AutoActionCancel = nil
            end
        end
    end
end

---------------------------------------
function ViewMsgBox:showMsgBox1(title, tips, operate_action)
    if (self.TextTitle ~= nil) then
        self.TextTitle.text = title
    end
    self.TextTips.text = tips
    self.ActionConfirm = operate_action
end

---------------------------------------
function ViewMsgBox:showMsgBox2(title, tips, map_param, operate_action)
    if (self.TextTitle ~= nil) then
        self.TextTitle.text = title
    end
    self.TextTips.text = tips
    self.MapParam = map_param
    self.ActionConfirm2 = operate_action
end

---------------------------------------
function ViewMsgBox:showMsgBox3(info, ok, cancel)
    self.TextTips.text = info
    self.ActionOk = ok
    self.ActionCancel = cancel
end

---------------------------------------
function ViewMsgBox:showMsgBox4(info, ok)
    self.TextTips.text = info
    self.ActionOk = ok
end

---------------------------------------
function ViewMsgBox:useTwoBtn(title, content, action_ensure, action_cancel)
    self.TextTitle.text = title
    self.TextTips.text = content
    local controller_state = self.ComUi:GetController("ControllerState")
    controller_state:SetSelectedIndex(1)
    local btn_ensure = self.ComUi:GetChild("Lan_Btn_Confirm1").asButton
    btn_ensure.onClick:Add(action_ensure)
    local btn_cancel = self.ComUi:GetChild("Lan_Btn_Cancel").asButton
    btn_cancel.onClick:Add(action_cancel)
end

---------------------------------------
function ViewMsgBox:useTwoBtn2(title, content, confirm_title, cancel_title, auto_tm, auto_tm_showin_cancelbtn, action_ensure, action_cancel)
    self.TextTitle.text = title
    self.TextTips.text = content
    self.AutoTm = auto_tm
    self.AutoActionCancel = action_cancel
    self.ActionOk = action_ensure
    local controller_state = self.ComUi:GetController("ControllerState")
    controller_state:SetSelectedIndex(1)
    local btn_ensure = self.ComUi:GetChild("Lan_Btn_Confirm1").asButton
    btn_ensure.title = confirm_title
    btn_ensure.onClick:Add(
            function()
                self:onClickBtnOK()
            end
    )
    local btn_cancel = self.ComUi:GetChild("Lan_Btn_Cancel").asButton
    btn_cancel.title = cancel_title
    btn_cancel.onClick:Add(
            function()
                self:onClickBtnCancel()
            end)
    local title1 = confirm_title
    local self_btn = btn_ensure
    if auto_tm_showin_cancelbtn == true then
        title1 = cancel_title
        self_btn = btn_cancel
    end
    local t_title = {}
    local title = title1
    local title_1 = title
    if auto_tm > 0 then
        table.insert(t_title, title1)
        table.insert(t_title, "(")
        table.insert(t_title, "%u")
        table.insert(t_title, ")")
        title = table.concat(t_title)
        title_1 = string.format(title, auto_tm)
    end

    self.BtnTitle = title
    self.BtnAuto = self_btn
    self.BtnAuto.text = title_1
end

---------------------------------------
function ViewMsgBox:onClickBtnOK()
    self.ViewMgr:DestroyView(self)

    if (self.ActionOk ~= nil) then
        self.ActionOk()
    end

    if (self.ActionConfirm ~= nil) then
        self.ActionConfirm(true)
    end

    if (self.ActionConfirm2 ~= nil) then
        self.ActionConfirm2(true, self.MapParam)
    end
end

---------------------------------------
function ViewMsgBox:onClickBtnCancel()
    self.ViewMgr:DestroyView(self)

    if (self.ActionCancel ~= nil) then
        self.ActionCancel()
    end

    if (self.ActionConfirm ~= nil) then
        self.ActionConfirm(false)
    end

    if (self.ActionConfirm2 ~= nil) then
        self.ActionConfirm2(false, self.MapParam)
    end
end

---------------------------------------
ViewMsgBoxFactory = ViewFactory:new()

---------------------------------------
function ViewMsgBoxFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewMsgBoxFactory:CreateView()
    local view = ViewMsgBox:new(nil)
    return view
end