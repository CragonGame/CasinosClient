-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewClubHelp = ViewBase:new()

---------------------------------------
function ViewClubHelp:new(o)
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
    self.Instance = o
    return o
end

---------------------------------------
function ViewClubHelp:OnCreate()
    ViewHelper:PopUi(self.ComUi, "牌友圈帮助")
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
function ViewClubHelp:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewClubHelpFactory = ViewFactory:new()

---------------------------------------
function ViewClubHelpFactory:new(o, ui_package_name, ui_component_name,
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
function ViewClubHelpFactory:CreateView()
    local view = ViewClubHelp:new(nil)
    return view
end