-- Copyright(c) Cragon. All rights reserved.

ViewDesktopTypeBase = {}

function ViewDesktopTypeBase:new(o,view_desktop)
    o = o or {}
    setmetatable(o,self)
    self.__index = self

    return o
end

function ViewDesktopTypeBase:onHandleEv(ev)

end

ViewDesktopTypeBaseFactory = {}

function ViewDesktopTypeBaseFactory:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self

    return o
end

function ViewDesktopTypeBaseFactory:GetName()
end

function ViewDesktopTypeBaseFactory:CreateViewDesktopType(view_desktop)
end