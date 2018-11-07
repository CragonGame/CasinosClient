-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewBase = {
    ViewMgr = nil,
    GoUi = nil,
    ComUi = nil,
    Panel = nil,
    UILayer = nil,
    InitDepth = nil,
    ViewKey = nil
}

function ViewBase:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

function ViewBase:OnCreate()
end

function ViewBase:OnDestroy()
end

function ViewBase:OnHandleEv(ev)
end

---------------------------------------
ViewFactory = {
    PackageName = nil,
    ComponentName = nil,
    UILayer = nil,
    IsSingle = nil,
    FitScreen = nil
}

function ViewFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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

function ViewFactory:CreateView()
end