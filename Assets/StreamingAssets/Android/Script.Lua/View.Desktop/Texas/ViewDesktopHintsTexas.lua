-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewDesktopHintsTexas = ViewBase:new(nil)

---------------------------------------
function ViewDesktopHintsTexas:new(o)
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
function ViewDesktopHintsTexas:onCreate()
    self.TransitionCreate = self.ComUi:GetTransition("TransitionCreate")
    self.TransitionCreate:Play()
    self.GHintsList = self.ComUi:GetChild("Grid").asList
    local com_shade = self.ComUi:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local co_content = self.ComUi:GetChild("Content").asCom
    local btn_close = co_content:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    self:_initDesktopHintInfo()
end

---------------------------------------
function ViewDesktopHintsTexas:_initDesktopHintInfo()
    local l = self.ViewMgr.TbDataMgr:GetMapData("HintsInfoTexas")
    for k, v in pairs(l) do
        local com = self.GHintsList:AddItemFromPool().asCom
        self.GHintsList:AddChild(com)
        local hint = ItemDesktopHintsInfo:new(nil, k, com, self.ViewMgr)
        hint:setDesktopHintInfo(v)
    end
end

---------------------------------------
function ViewDesktopHintsTexas:_onClickBtnClose()
    self.TransitionCreate:PlayReverse(
            function()
                self.ViewMgr:destroyView(self)
            end
    )
end

---------------------------------------
ViewDesktopHintsTexasFactory = ViewFactory:new()

---------------------------------------
function ViewDesktopHintsTexasFactory:new(o, ui_package_name, ui_component_name,
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
function ViewDesktopHintsTexasFactory:createView()
    local view = ViewDesktopHintsTexas:new(nil)
    return view
end