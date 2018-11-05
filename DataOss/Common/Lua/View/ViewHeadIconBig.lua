-- Copyright(c) Cragon. All rights reserved.
-- 全屏头像

---------------------------------------
ViewHeadIconBig = ViewBase:new()

---------------------------------------
function ViewHeadIconBig:new(o)
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
function ViewHeadIconBig:OnCreate()
    self.GLoaderPlayerIcon = self.ComUi:GetChild("LoaderIcon").asLoader
    self.ComUi.onClick:Add(
            function()
                self.ViewMgr:DestroyView(self)
            end
    )
end

---------------------------------------
function ViewHeadIconBig:setIcon(icon)
    if icon ~= nil then
        self.GLoaderPlayerIcon.visible = true
        self.GLoaderPlayerIcon.icon = icon
    end
end

---------------------------------------
function ViewHeadIconBig:setIcon(t)
    if t ~= nil then
        self.GLoaderPlayerIcon.visible = true
        self.GLoaderPlayerIcon.texture = CS.FairyGUI.NTexture(t)
    end
end

---------------------------------------
ViewHeadIconBigFactory = ViewFactory:new()

---------------------------------------
function ViewHeadIconBigFactory:new(o, ui_package_name, ui_component_name,
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
function ViewHeadIconBigFactory:CreateView()
    local view = ViewHeadIconBig:new(nil)
    return view
end