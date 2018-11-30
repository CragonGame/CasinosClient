-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewResetPwd = ViewBase:new()

---------------------------------------
function ViewResetPwd:new(o)
    o = o or {}
    setmetatable(o, self)
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

---------------------------------------
function ViewResetPwd:OnCreate()
    self.GroupGetPwd = self.ComUi:GetChild("ForgetPwd").asGroup
    local btn_resetpwd = self.ComUi:GetChildInGroup(self.GroupGetPwd, "Lan_Btn_ResetPwd").asButton
    btn_resetpwd.onClick:Add(
            function()
                self:onClickBtnResetPwd()
            end
    )
    local btn_returnex = self.ComUi:GetChildInGroup(self.GroupGetPwd, "Lan_Btn_Return").asButton
    btn_returnex.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )
    local text_acc = self.ComUi:GetChildInGroup(self.GroupGetPwd, "InputAcc").asTextInput
    text_acc.promptText = self.ViewMgr.LanMgr:getLanValue("InputUserName")
    text_acc.onChanged:Set(
            function()
                self:checkGetPwdInput()
            end
    )
    local text_superpwd = self.ComUi:GetChildInGroup(self.GroupGetPwd, "InputSuperPwd").asTextInput
    text_superpwd.promptText = self.ViewMgr.LanMgr:getLanValue("InputSuperPwd")
    text_superpwd.onChanged:Set(
            function()
                self:checkGetPwdInput()
            end
    )
    local text_newpwd = self.ComUi:GetChildInGroup(self.GroupGetPwd, "InputNewPwd").asTextInput
    text_newpwd.promptText = self.ViewMgr.LanMgr:getLanValue("InputNewPwd")
    text_newpwd.onChanged:Set(
            function()
                self:checkGetPwdInput()
            end
    )
    local text_confirmnewpwd = self.ComUi:GetChildInGroup(self.GroupGetPwd, "InputConfirmNewPwd").asTextInput
    text_confirmnewpwd.promptText = self.ViewMgr.LanMgr:getLanValue("ConfirmNewPwd")
    text_confirmnewpwd.onChanged:Set(
            function()
                self:checkGetPwdInput()
            end
    )
    self:checkGetPwdInput()
    --local bg = self.ComUi:GetChild("Bg")
    --if (bg ~= nil)
    --then
    --	CS.Casinos.UiHelperCasinos.MakeUiBgFiteScreen(bg, self.ComUi.width, self.ComUi.height, bg.width, bg.height)
    --end
    local btn_return = self.ComUi:GetChild("BtnReturn").asButton
    btn_return.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )
end

---------------------------------------
function ViewResetPwd:onClickBtnReturn()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewResetPwd:onClickBtnResetPwd()
    local text_acc = self.ComUi:GetChildInGroup(self.GroupGetPwd, "InputAcc").asTextInput
    local text_pwd = self.ComUi:GetChildInGroup(self.GroupGetPwd, "InputNewPwd").asTextInput
    local text_superpwd = self.ComUi:GetChildInGroup(self.GroupGetPwd, "InputSuperPwd").asTextInput
    local ev = self.ViewMgr:GetEv("EvUiRequestResetPwd")
    if (ev == nil) then
        ev = EvUiRequestResetPwd:new(nil)
    end
    ev.account_name = text_acc.text
    ev.super_pwd = text_superpwd.text
    ev.new_pwd = text_pwd.text
    self.ViewMgr:SendEv(ev)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewResetPwd:checkGetPwdInput()
    local text_acc = self.ComUi:GetChildInGroup(self.GroupGetPwd, "InputAcc").asTextInput
    local text_newpwd = self.ComUi:GetChildInGroup(self.GroupGetPwd, "InputNewPwd").asTextInput
    local text_superpwd = self.ComUi:GetChildInGroup(self.GroupGetPwd, "InputSuperPwd").asTextInput
    local text_confirmnewpwd = self.ComUi:GetChildInGroup(self.GroupGetPwd, "InputConfirmNewPwd").asTextInput
    local btn_resetpwd = self.ComUi:GetChildInGroup(self.GroupGetPwd, "Lan_Btn_ResetPwd").asButton
    if ((text_acc.text ~= nil and text_acc.text ~= "") and
            (text_newpwd.text ~= nil and text_newpwd.text ~= "") and
            (text_superpwd.text ~= nil and text_superpwd.text ~= "") and
            (text_confirmnewpwd.text ~= nil and text_confirmnewpwd.text ~= "")) then
        btn_resetpwd.enabled = true
    else
        btn_resetpwd.enabled = false
    end
end

---------------------------------------
ViewResetPwdFactory = class(ViewFactory)

---------------------------------------
function ViewResetPwdFactory:CreateView()
    local view = ViewResetPwd:new(nil)
    return view
end