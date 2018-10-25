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

---------------------------------------
function ViewBase:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function ViewBase:OnCreate()
end

---------------------------------------
function ViewBase:OnDestroy()
end

---------------------------------------
function ViewBase:onUpdate(tm)
end

---------------------------------------
function ViewBase:OnHandleEv(ev)
end