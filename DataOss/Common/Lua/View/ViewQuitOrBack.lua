-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewQuitOrBack = class(ViewBase)

---------------------------------------
function ViewQuitOrBack:ctor()
    self.Tween = nil
end

---------------------------------------
function ViewQuitOrBack:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi)
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickBack()
            end
    )
    self.BtnQuit = self.ComUi:GetChild("Lan_Btn_QuitGame").asButton
    self.BtnQuit.onClick:Add(
            function()
                self:onClickQuit()
            end
    )
    self.BtnBack = self.ComUi:GetChild("Lan_Btn_Back").asButton
    self.BtnBack.onClick:Add(
            function()
                self:onClickBack()
            end
    )
end

---------------------------------------
function ViewQuitOrBack:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewQuitOrBack:setQuitTimeAndIfCanBackGameAndTips(quit_tm, can_back, tips)
    self.CancelTm = quit_tm
    self.BtnBack.enabled = can_back

    local final_tips = tips
    if (final_tips == nil or final_tips == "") then
        final_tips = '确认退出吗'
    end
    local obj_tips = self.ComUi:GetChild("Tips").asTextField
    obj_tips.text = final_tips
end

---------------------------------------
function ViewQuitOrBack:setCountDownTm()
    local tips = self.ViewMgr.LanMgr:getLanValue("QuitGame")
    self.BtnQuit.title = tips .. "(" .. tostring(self.CancelTm) .. ")"
end

---------------------------------------
function ViewQuitOrBack:onClickQuit()
    CS.UnityEngine.Application.Quit()
end

---------------------------------------
function ViewQuitOrBack:onClickBtnLogin()
    local ev = self:GetEv("EvUiClickLogin")
    if (ev == nil) then
        ev = EvUiClickLogin:new(nil)
    end
    self:SendEv(ev)
end

---------------------------------------
function ViewQuitOrBack:onClickBack()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewQuitOrBackFactory = class(ViewFactory)

---------------------------------------
function ViewQuitOrBackFactory:CreateView()
    local view = ViewQuitOrBack:new()
    return view
end