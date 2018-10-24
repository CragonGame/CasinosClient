-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UiDesktopTexasFlow = {}

---------------------------------------
function UiDesktopTexasFlow:new(o, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ViewMgr = view_mgr
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    return o
end

---------------------------------------
function UiDesktopTexasFlow:Create()
end

---------------------------------------
function UiDesktopTexasFlow:Destroy()
end

---------------------------------------
function UiDesktopTexasFlow:Update(elapsed_tm)
end