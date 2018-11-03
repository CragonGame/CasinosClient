-- Copyright(c) Cragon. All rights reserved.
-- 牌型界面，后续与普通桌中牌型界面命名调成一致

---------------------------------------
ViewDesktopHCardType = ViewBase:new()

---------------------------------------
function ViewDesktopHCardType:new(o)
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
    o.GCoCardType = nil
    return o
end

---------------------------------------
function ViewDesktopHCardType:OnCreate()
    self.ComUi.onClick:Add(
            function()
                self:_onClickCoCardType()
            end
    )
end

---------------------------------------
function ViewDesktopHCardType:showCardType(co_cardtype)
    self.GCoCardType = co_cardtype
    self.ComUi:AddChild(self.GCoCardType)
    self.GCoCardType:SetXY(-self.GCoCardType.width, self.ComUi.height / 2 - self.GCoCardType.height / 2)
    self.GCoCardType:TweenMoveX(0, 0.5)
end

---------------------------------------
function ViewDesktopHCardType:_onClickCoCardType()
    self.GCoCardType:TweenMoveX(-self.GCoCardType.width, 0.5):OnComplete(
            function()
                self.ViewMgr:DestroyView(self)
            end
    )
end

---------------------------------------
ViewDesktopHCardTypeFactory = ViewFactory:new()

---------------------------------------
function ViewDesktopHCardTypeFactory:new(o, ui_package_name, ui_component_name,
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
function ViewDesktopHCardTypeFactory:CreateView()
    local view = ViewDesktopHCardType:new(nil)
    return view
end