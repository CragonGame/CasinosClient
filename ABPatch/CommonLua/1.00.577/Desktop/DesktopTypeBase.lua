DesktopTypeBase = {}

function DesktopTypeBase:new(o,desktop_base,co_mgr)
	o = o or {}
	setmetatable(o,self)
	self.__index = self

	return o
end

function DesktopTypeBase:onDestroy()
end

function DesktopTypeBase:onUpdate(elapsed_tm)
end

function DesktopTypeBase:onHandleEv(ev)
end

function DesktopTypeBase:SetDesktopSnapshotData(desktop_data,is_init)
end

function DesktopTypeBase:PlayerEnter(player_data)
end

function DesktopTypeBase:PlayerLeave(player_guid)
end

function DesktopTypeBase:PlayerSitdown(sitdown_data)
end

function DesktopTypeBase:PlayerOb(player_guid)
end

function DesktopTypeBase:PlayerWaitWhile(player_guid)
end

function DesktopTypeBase:PlayerReturn(return_data) 
end

function DesktopTypeBase:DesktopUser(method_id, method_data)
end

function DesktopTypeBase:DesktopChat(msg)	
end

function DesktopTypeBase:getAllValidPlayer()
end



DesktopTypeBaseFactory = {}

function DesktopTypeBaseFactory:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self

	return o
end

function DesktopTypeBaseFactory:GetName()
end

function DesktopTypeBaseFactory:CreateDesktopType(desktop_base,co_mgr)
end        