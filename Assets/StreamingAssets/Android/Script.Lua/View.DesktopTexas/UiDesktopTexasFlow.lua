-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
UiDesktopTexasFlow = {}

---------------------------------------
function UiDesktopTexasFlow:new(o, com_card)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
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
