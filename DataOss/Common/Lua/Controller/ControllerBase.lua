-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ControllerBase = {
    ControllerMgr = nil,
    ControllerName = nil,
    ControllerData = nil,
    Model = nil
}

function ControllerBase:new(o, controller_mgr, controller_data, guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

function ControllerBase:OnCreate()
end

function ControllerBase:OnDestroy()
end

function ControllerBase:OnHandleEv(ev)
end

---------------------------------------
ControllerFactory = {
    ControllerName = nil
}

function ControllerFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

function ControllerFactory:CreateController(controller_mgr, controller_data, guid)
end