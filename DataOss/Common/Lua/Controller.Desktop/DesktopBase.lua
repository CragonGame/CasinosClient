-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
DesktopBase = {}

---------------------------------------
function DesktopBase:new(o, co_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerMgr = nil
    self.CurrentUnSendDesktopMsg = nil
    self.MapAllPlayer = nil
    self.MePlayer = nil
    self.Guid = nil
    return o
end

---------------------------------------
function DesktopBase:GetDesktopPlayerByGuid(player_guid)
end

---------------------------------------
function DesktopBase:OnDestroy(need_createmainui)
end

---------------------------------------
function DesktopBase:Update(elapsed_tm)
end

---------------------------------------
function DesktopBase:OnHandleEv(ev)
end

---------------------------------------
function DesktopBase:SetDesktopSnapshotData(desktop_data, is_init)
end

---------------------------------------
function DesktopBase:PlayerEnter(player_data)
end

---------------------------------------
function DesktopBase:PlayerLeave(player_guid)
end

---------------------------------------
function DesktopBase:PlayerSitdown(sitdown_data)
end

---------------------------------------
function DesktopBase:PlayerOb(player_guid)
end

---------------------------------------
function DesktopBase:PlayerWaitWhile(player_guid)
end

---------------------------------------
function DesktopBase:PlayerReturn(return_data)
end

---------------------------------------
function DesktopBase:DesktopUser(info_user)
end

---------------------------------------
function DesktopBase:DesktopChat(msg)
end

---------------------------------------
function DesktopBase:getAllValidPlayer()
end

---------------------------------------
DesktopBaseFactory = {}

---------------------------------------
function DesktopBaseFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function DesktopBaseFactory:GetName()
end

---------------------------------------
function DesktopBaseFactory:CreateDesktop(co_mgr)
end        