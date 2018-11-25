-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewReward = ViewBase:new()

---------------------------------------
function ViewReward:new(o)
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
function ViewReward:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("About"))
end

---------------------------------------
function ViewReward:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
end

---------------------------------------
ViewRewardFactory = ViewFactory:new()

---------------------------------------
function ViewRewardFactory:new(o, ui_package_name, ui_component_name,
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
function ViewRewardFactory:CreateView()
    local view = ViewAbout:new(nil)
    return view
end