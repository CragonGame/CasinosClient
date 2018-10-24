-- Copyright (c) Cragon. All rights reserved.

---------------------------------------
EventSys = {
    Instance = nil,
    TableEvListener = {}, -- key evname value table listener
    TableListenerEv = {}, -- key listener value table evname
    TableEvListenerTransfer = {}, -- value listener
    TableEvEv = {} -- key evname value ev evPool
}

---------------------------------------
function EventSys:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil) then
        self.Instance = o
    end
    return self.Instance
end

---------------------------------------
function EventSys:onCreate()
    print("EventSys:onCreate")
    Context:DoString("EventBase")
end

---------------------------------------
function EventSys:bindEvListener(ev_name, ev_listener)
    local table_listener = self.TableEvListener[ev_name]
    if (table_listener == nil) then
        table_listener = {}
        self.TableEvListener[ev_name] = table_listener
    end
    table_listener[ev_listener] = ev_listener

    local table_ev = self.TableListenerEv[ev_listener]
    if (table_ev == nil) then
        table_ev = {}
        self.TableListenerEv[ev_listener] = table_ev
    end
    table_ev[ev_name] = ev_name
end

---------------------------------------
function EventSys:unbindEvListener(ev_listener)
    local table_ev = self.TableListenerEv[ev_listener]
    if (table_ev ~= nil) then
        for k, v in pairs(table_ev) do
            local table_listener = self.TableEvListener[v]
            if (table_listener ~= nil) then
                table_listener[ev_listener] = nil
            end
        end
        self.TableListenerEv[ev_listener] = {}
    end
end

---------------------------------------
function EventSys:sendEv(ev)
    local ev_name = ev.EventName
    local table_listener = self.TableEvListener[ev_name]
    if (table_listener ~= nil) then
        for k, v in pairs(table_listener) do
            if (v ~= nil) then
                v:onHandleEv(ev)
            end
        end
    end

    ev:reset()
    self.TableEvEv[ev_name] = ev
end

---------------------------------------
function EventSys:getEv(ev_name)
    local ev = self.TableEvEv[ev_name]
    return ev
end