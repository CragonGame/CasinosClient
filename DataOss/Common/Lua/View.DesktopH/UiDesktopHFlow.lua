-- Copyright(c) Cragon. All rights reserved.
-- 百人桌动画流程，所有的GTween动画都在此类中

---------------------------------------
UiDesktopHFlow = {}

---------------------------------------
function UiDesktopHFlow:new(view_desktoph)
    o = {}
    setmetatable(o, self)
    self.__index = self
    o.Context = Context
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.ViewDesktopH = view_desktoph
    print('UiDesktopH:new()')
    return o
end

---------------------------------------
function UiDesktopHFlow:Close()
    print('UiDesktopH:Close()')
end

---------------------------------------
function UiDesktopHFlow:InitDesktopHData()
end

---------------------------------------
function UiDesktopHFlow:OnEnterReadyState()
end

---------------------------------------
function UiDesktopHFlow:OnEnterBetState()
end

---------------------------------------
function UiDesktopHFlow:OnEnterGameEndState()
end

---------------------------------------
function UiDesktopHFlow:OnEnterRestState()
end

---------------------------------------
function UiDesktopHFlow:OnUpdateBetPotBetInfo()
end

---------------------------------------
function UiDesktopHFlow:BetGold()
end