-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewFactory = {
    PackageName = nil,
    ComponentName = nil,
    UILayer = nil,
    IsSingle = nil,
    FitScreen = nil
}

---------------------------------------
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

---------------------------------------
function ViewFactory:createView()
end