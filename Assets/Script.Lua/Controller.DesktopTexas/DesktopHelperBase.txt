-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
DesktopHelperBase = {}

---------------------------------------
function DesktopHelperBase:new(o, co_player)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function DesktopHelperBase:GetDesktopRedisKeyPrefix(desktop_filter, is_private)
end

---------------------------------------
function DesktopHelperBase:IsValid()
end

---------------------------------------
function DesktopHelperBase:IsFull(desktop_filter, playerCount)
end

---------------------------------------
function DesktopHelperBase:GetPlayerPlayState(filter, desktop_type, desktop_guid)
end

---------------------------------------
function DesktopHelperBase:GetDesktopInfoFormat(data_mgr, desktop_filter, lan_base)
end

---------------------------------------
DesktopHelperBaseFactory = {}

---------------------------------------
function DesktopHelperBaseFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function DesktopHelperBaseFactory:GetName()
end

---------------------------------------
function DesktopHelperBaseFactory:CreateDesktopHelper()
end

---------------------------------------
DesktopFormatInfo = {}

---------------------------------------
function DesktopFormatInfo:new(o, DesktopFormat, Param)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.DesktopFormat = DesktopFormat
    o.Param = Param
    return o
end