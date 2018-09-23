-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
Context = {}

---------------------------------------
function Context:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    return o
end

---------------------------------------
function Context:Init()
end

---------------------------------------
function LuaContext:Release()
end