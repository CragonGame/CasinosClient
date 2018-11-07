-- Copyright(c) Cragon. All rights reserved.
-- 登录界面，由Context调用ControllerMgr创建Login

---------------------------------------
ControllerMgr = {
    ViewMgr = nil,
    EventSys = nil,
    TableControllerFactory = {},
    TableController = {},
    TableControllerUpdate = {},
    Context = Context,
    Json = Context.Json,
    Rpc = Context.Rpc
}

---------------------------------------
function ControllerMgr:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.LanMgr = nil
    return o
end

---------------------------------------
function ControllerMgr:OnCreate()
    print("ControllerMgr:OnCreate")
    self.EventSys = EventSys:new(nil)
    self.ViewMgr = ViewMgr:new(nil)
    self.Context:DoString("ControllerBase")
    self.Context:DoString("ControllerFactory")
end

---------------------------------------
function ControllerMgr:OnDestroy()
end

---------------------------------------
function ControllerMgr:CreatePlayerControllers(player_data, guid)
    local c_actor = self:CreateController("Actor", player_data, guid)
    local c_player = self:CreateController("Player", nil, guid)
    local c_activity = self:CreateController("Activity", nil, guid)
    local c_bag = self:CreateController("Bag", nil, guid)
    local c_desk = self:CreateController("DesktopTexas", nil, guid)
    local c_deskh = self:CreateController("DesktopH", nil, guid)
    local c_grow = self:CreateController("Grow", nil, guid)
    local c_im = self:CreateController("IM", nil, guid)
    local c_lobby = self:CreateController("Lobby", nil, guid)
    local c_ottery_ticket = self:CreateController("LotteryTicket", nil, guid)
    local c_marquee = self:CreateController("Marquee", nil, guid)
    local c_ranking = self:CreateController("Ranking", nil, guid)
    local c_trade = self:CreateController("Trade", nil, guid)
    local c_mtt = self:CreateController("Mtt", nil, guid)
    c_actor:OnCreate()
    c_activity:OnCreate()
    c_player:OnCreate()
    c_bag:OnCreate()
    c_desk:OnCreate()
    c_deskh:OnCreate()
    c_grow:OnCreate()
    c_im:OnCreate()
    c_lobby:OnCreate()
    c_ottery_ticket:OnCreate()
    c_marquee:OnCreate()
    c_ranking:OnCreate()
    c_trade:OnCreate()
    c_mtt:OnCreate()
end

---------------------------------------
function ControllerMgr:DestroyPlayerControllers()
    local t = {}
    for i, v in pairs(self.TableController) do
        if (i ~= "Login" and i ~= "UCenter") then
            t[i] = v
        end
    end

    for i, v in pairs(t) do
        v:OnDestroy()
        local l = self.TableController[i]
        if (l ~= nil) then
            l = nil
            self.TableController[i] = nil
        end
    end
end

---------------------------------------
function ControllerMgr:CreateController(controller_name, controller_data, guid)
    local controller_factory = self.TableControllerFactory[controller_name]
    if (controller_factory == nil) then
        return nil
    end
    local controller = controller_factory:CreateController(self, controller_data, guid)
    self.TableController[controller_name] = controller
    return controller
end

---------------------------------------
function ControllerMgr:DestroyController(is_kickout)
    for k, v in pairs(self.TableController) do
        if (v ~= nil) then
            local controller_name = v.ControllerName
            v:OnDestroy()
            self.TableController[controller_name] = nil
        end
    end
    --ControllerMgr.TableController = {}
end

---------------------------------------
function ControllerMgr:RegController(controller_name, controller_factory)
    if (controller_factory ~= nil) then
        self.TableControllerFactory[controller_name] = controller_factory
    end
end

---------------------------------------
function ControllerMgr:GetController(controller_name)
    local controller = self.TableController[controller_name]
    return controller
end

---------------------------------------
function ControllerMgr:BindEvListener(ev_name, ev_listener)
    if (ControllerMgr.EventSys ~= nil) then
        ControllerMgr.EventSys:BindEvListener(ev_name, ev_listener)
    end
end

---------------------------------------
function ControllerMgr:UnbindEvListener(ev_listener)
    if (ControllerMgr.EventSys ~= nil) then
        ControllerMgr.EventSys:UnbindEvListener(ev_listener)
    end
end

---------------------------------------
function ControllerMgr:GetEv(ev_name)
    local ev = nil
    if (ControllerMgr.EventSys ~= nil) then
        ev = ControllerMgr.EventSys:GetEv(ev_name)
    end
    return ev
end

---------------------------------------
function ControllerMgr:SendEv(ev)
    if (ControllerMgr.EventSys ~= nil) then
        ControllerMgr.EventSys:SendEv(ev)
    end
end

---------------------------------------
function ControllerMgr:PackData(data)
    local p_datas = self.RPC:PackData(data)
    return p_datas
end

---------------------------------------
function ControllerMgr:UnpackData(data)
    local p_datas = self.RPC:UnpackData(data)
    return p_datas
end