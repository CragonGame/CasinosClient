-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
LuaContext = {}

---------------------------------------
function LuaContext:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
	print('LuaContext:new(o)')
    return o
end

---------------------------------------
function LuaContext:Init()
    print('LuaContext:Init()')
end

---------------------------------------
function LuaContext:Release()
    print('LuaContext:Release()')
end