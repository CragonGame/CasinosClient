-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewDesktopHHelp = class(ViewBase)

---------------------------------------
function ViewDesktopHHelp:ctor()
    self.Tween = nil
end

---------------------------------------
function ViewDesktopHHelp:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("Help"))
    local co_history_close = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_history_close = co_history_close:GetChild("BtnClose").asButton
    btn_history_close.onClick:Add(
            function()
                self:_onClickBtnHelpClose()
            end
    )
    local com_shade = co_history_close:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnHelpClose()
            end
    )
end

---------------------------------------
function ViewDesktopHHelp:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
function ViewDesktopHHelp:setComHelp(co_help)
    local com_helpparent = self.ComUi:GetChild("ComHelpParent").asCom
    self.ComUi:AddChild(co_help)
    co_help.position = com_helpparent.position
end

---------------------------------------
function ViewDesktopHHelp:_onClickBtnHelpClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewDesktopHHelpFactory = class(ViewFactory)

---------------------------------------
function ViewDesktopHHelpFactory:CreateView()
    local view = ViewDesktopHHelp:new()
    return view
end
	