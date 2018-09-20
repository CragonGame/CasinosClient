-- Copyright(c) Cragon. All rights reserved.

ControllerFactory = {
	ControllerName = nil
}

function ControllerFactory:new(o)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
    return o
end

function ControllerFactory:createController(controller_mgr,controller_data,guid)
	
end