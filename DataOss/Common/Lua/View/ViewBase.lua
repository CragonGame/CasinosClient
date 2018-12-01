-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewBase = class()

function ViewBase:ctor()
    self.ViewMgr = nil
    self.ViewKey = nil
    self.GoUi = nil
    self.ComUi = nil
    self.Panel = nil
    self.UILayer = nil
    self.InitDepth = nil
end

function ViewBase:OnCreate()
end

function ViewBase:OnDestroy()
end

function ViewBase:OnHandleEv(ev)
end

---------------------------------------
ViewFactory = class()

function ViewFactory:ctor(ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
end

function ViewFactory:CreateView()
end