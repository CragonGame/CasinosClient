-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
EvCtrlRedPointStateChange = EventBase:new(nil)

function EvCtrlRedPointStateChange:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.EventName = "EvCtrlRedPointStateChange"
    self.RedPointType = ''
    self.Show = false
    return o
end

function EvCtrlRedPointStateChange:Reset()
    self.RedPointType = ''
    self.Show = false
end