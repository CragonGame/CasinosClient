ControllerMgr = {
	Instance = nil,	
	ViewMgr = nil,
	EventSys = nil,
	TableControllerFactory = {},
	TableController = {},
	TableControllerUpdate = {}
}

function ControllerMgr:new(o)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
    if(self.Instance==nil)
	then
		self.Instance = o
		self.LanMgr = nil
	end
    return self.Instance
end

function ControllerMgr:OnCreate()
	print("ControllerMgr:onCreate")	
	self.EventSys = EventSys:new(nil)
	self.ViewMgr = ViewMgr:new(nil)
	MainC:doString("ControllerBase")
	MainC:doString("ControllerFactory")
end

function ControllerMgr:OnDestroy()
	
end

function ControllerMgr:OnUpdate(tm)	
	--ControllerMgr.LuaHelper:CloneTableData(ControllerMgr.TableController,ControllerMgr.TableControllerUpdate)
	for k,v in pairs(self.TableController) do
		if(v ~= nil)
		then
			v:onUpdate(tm)
		end
	end
	--ControllerMgr.TableControllerUpdate = {}
end

function ControllerMgr:CreatePlayerControllers(player_data,guid)
    local c_actor = self:CreateController("Actor",player_data,guid)
	local c_player = self:CreateController("Player",nil,guid)
	local c_activity = self:CreateController("Activity",nil,guid)
	local c_bag = self:CreateController("Bag",nil,guid)
	local c_desk = self:CreateController("Desk",nil,guid)
	local c_deskh = self:CreateController("DeskH",nil,guid)
	local c_grow = self:CreateController("Grow",nil,guid)
	local c_im = self:CreateController("IM",nil,guid)
	local c_lobby = self:CreateController("Lobby",nil,guid)
	local c_ottery_ticket = self:CreateController("LotteryTicket",nil,guid)
	local c_marquee = self:CreateController("Marquee",nil,guid)
	local c_ranking = self:CreateController("Ranking",nil,guid)
	local c_trade = self:CreateController("Trade",nil,guid)
	local c_mtt = self:CreateController("Mtt",nil,guid)
	c_actor:onCreate()
	c_activity:onCreate()
	c_player:onCreate()
	c_bag:onCreate()
	c_desk:onCreate()
	c_deskh:onCreate()
	c_grow:onCreate()
	c_im:onCreate()
	c_lobby:onCreate()
	c_ottery_ticket:onCreate()
	c_marquee:onCreate()
	c_ranking:onCreate()
	c_trade:onCreate()
	c_mtt:onCreate()
end

function ControllerMgr:DestroyPlayerControllers()
	local t = {}
	for i, v in pairs(self.TableController) do
		if(i ~= "Login" and i ~= "UCenter")
		then
			t[i] = v
		end
	end

	for i, v in pairs(t) do
		v:onDestroy()
		local l = self.TableController[i]
		if(l ~= nil)
			then
			l = nil
			self.TableController[i] = nil
		end
	end
end

function ControllerMgr:CreateController(controller_name,controller_data,guid)
	-- local controller_mgr = ControllerMgr:new(nil)	
	local controller_factory = self.TableControllerFactory[controller_name]
	if(controller_factory == nil)
	then
		return nil
	end

	local controller = controller_factory:createController(self,controller_data,guid)
	self.TableController[controller_name] = controller
	return controller
end

function ControllerMgr:DestroyController(is_kickout)
	for k,v in pairs(self.TableController) do
		if(v ~= nil)
		then
			local controller_name = v.ControllerName
			v:onDestroy()
			self.TableController[controller_name] = nil
		end
	end
	--ControllerMgr.TableController = {}
end

function ControllerMgr:RegController(controller_name,controller_factory)	
	if(controller_factory ~= nil)
	then		
		self.TableControllerFactory[controller_name] = controller_factory
	end	
end

function ControllerMgr:GetController(controller_name)
	local controller = self.TableController[controller_name]
	return controller
end

function ControllerMgr:bindEvListener(ev_name,ev_listener)
	if(ControllerMgr.EventSys ~= nil)
	then
		ControllerMgr.EventSys:bindEvListener(ev_name,ev_listener)
	end
end

function ControllerMgr:unbindEvListener(ev_listener)
	if(ControllerMgr.EventSys ~= nil)
	then
		ControllerMgr.EventSys:unbindEvListener(ev_listener)
	end
end

function ControllerMgr:getEv(ev_name)
	local ev = nil
	if(ControllerMgr.EventSys ~= nil)
	then
		ev = ControllerMgr.EventSys:getEv(ev_name)
	end
	return ev
end

function ControllerMgr:sendEv(ev)
	if(ControllerMgr.EventSys ~= nil)
	then
		ControllerMgr.EventSys:sendEv(ev)
	end
end

function ControllerMgr:packData(data)
	local p_datas = self.RPC:PackData(data)
	return p_datas
end

function ControllerMgr:unpackData(data)
	local p_datas = self.RPC:UnPackData(data)
	return p_datas
end