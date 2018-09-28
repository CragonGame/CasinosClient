-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewWaitingCountDown = ViewBase:new()

---------------------------------------
function ViewWaitingCountDown:new(o)
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
function ViewWaitingCountDown:onCreate()
    local text = self.ComUi:GetChild("Tips")
    if (text ~= nil) then
        self.GTextTips = text.asTextField
    end

    self.TextMin = self.ComUi:GetChild("TextMin").asTextField
    self.TextSec = self.ComUi:GetChild("TextSec").asTextField
    self.UpdateTimeTm = 0
end

---------------------------------------
function ViewWaitingCountDown:onDestroy()
end

---------------------------------------
function ViewWaitingCountDown:onUpdate(tm)
    self.Tm = self.Tm - tm
    self:setTm()
    if self.Tm <= 0 then
        self.ViewMgr:destroyView(self)
    end
    --self.UpdateTimeTm = self.UpdateTimeTm + tm
    --if (self.UpdateTimeTm >= 1)
    --then
    --    self.UpdateTimeTm = 0
    --    self:setTm()
    --end
end

---------------------------------------
function ViewWaitingCountDown:onHandleEv(ev)
end

---------------------------------------
function ViewWaitingCountDown:setTips(tips, tm)
    self.GTextTips.text = tips
    self.Tm = tm
    self:setTm()
end

---------------------------------------
function ViewWaitingCountDown:setTm()
    if self.Tm < 0 then
        self.Tm = 0
    end
    local multiple = self.Tm / 60
    local remainder = self.Tm % 60
    local min = ""
    multiple = math.floor(multiple)
    if multiple < 10 then
        min = "0" .. tostring(multiple)
    else
        min = tostring(multiple)
    end
    local sec = ""
    remainder = math.floor(remainder)
    if remainder < 10 then
        sec = "0" .. tostring(remainder)
    else
        sec = tostring(remainder)
    end
    self.TextMin.text = min
    self.TextSec.text = sec
end

---------------------------------------
ViewWaitingCountDownFactory = ViewFactory:new()

---------------------------------------
function ViewWaitingCountDownFactory:new(o, ui_package_name, ui_component_name,
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
function ViewWaitingCountDownFactory:createView()
    local view = ViewWaitingCountDown:new(nil)
    return view
end