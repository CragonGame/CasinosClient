-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ControllerBase = class()

function ControllerBase:ctor(this, controller_data, controller_name)
    self.ControllerData = controller_data
    self.ControllerName = controller_name
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.Context = Context
    self.ModelMgr = ModelMgr
    self.ControllerMgr = ControllerMgr
    self.ViewMgr = ViewMgr
    self.EventSys = EventSys
    self.Rpc = self.ControllerMgr.Rpc
    self.MC = CommonMethodType
    self.TbDataMgr = self.Context.TbDataMgr
    self.LanMgr = self.Context.LanMgr
end

function ControllerBase:OnCreate()
end

function ControllerBase:OnDestroy()
end

function ControllerBase:OnHandleEv(ev)
end

---------------------------------------
function ControllerBase:BindEvListener(ev_name, ev_listener)
    if (self.EventSys ~= nil) then
        self.EventSys:BindEvListener(ev_name, ev_listener)
    end
end

---------------------------------------
function ControllerBase:UnbindEvListener(ev_listener)
    if (self.EventSys ~= nil) then
        self.EventSys:UnbindEvListener(ev_listener)
    end
end

---------------------------------------
function ControllerBase:GetEv(ev_name)
    local ev = nil
    if (self.EventSys ~= nil) then
        ev = self.EventSys:GetEv(ev_name)
    end
    return ev
end

---------------------------------------
function ControllerBase:SendEv(ev)
    if (self.EventSys ~= nil) then
        self.EventSys:SendEv(ev)
    end
end

---------------------------------------
ControllerFactory = class()

function ControllerFactory:GetName()
end

function ControllerFactory:CreateController(controller_data)
end