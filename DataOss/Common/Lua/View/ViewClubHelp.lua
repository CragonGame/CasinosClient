-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewClubHelp = class(ViewBase)

---------------------------------------
function ViewClubHelp:ctor()
    self.Tween = nil
end

---------------------------------------
function ViewClubHelp:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, "牌友圈帮助")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
end

---------------------------------------
function ViewClubHelp:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewClubHelp:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewClubHelpFactory = class(ViewFactory)

---------------------------------------
function ViewClubHelpFactory:CreateView()
    local view = ViewClubHelp:new()
    return view
end