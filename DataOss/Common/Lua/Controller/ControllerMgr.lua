-- Copyright(c) Cragon. All rights reserved.
-- 登录界面，由Context调用ControllerMgr创建Login
-- Controller全部是单实例的
-- Controller始终存在，无需使用者调用Create
require('ControllerBase')
require('ControllerActivity')
require('ControllerActor')
require('ControllerBag')
require('ControllerDesktopTexas')
require('ControllerDesktopH')
require('ControllerGrow')
require('ControllerIM')
require('ControllerLobby')
require('ControllerLogin')
require('ControllerLotteryTicket')
require('ControllerMarquee')
require('ControllerMTT')
require('ControllerPlayer')
require('ControllerRanking')
require('ControllerReward')
require('ControllerTrade')
require('ControllerUCenter')
require('ControllerWallet')

---------------------------------------
ControllerMgr = {
    EventSys = nil,
    Context = Context,
    Json = Context.Json,
    Rpc = Context.Rpc,
    LanMgr = Context.LanMgr,
    TableControllerFactory = {},
    TableController = {},
    TableControllerUpdate = {},
}

---------------------------------------
function ControllerMgr:Create()
    self.EventSys = EventSys
    self.TbDataMgr = self.Context.TbDataMgr
    self.Rpc = self.Context.Rpc
    self.LanMgr = self.Context.LanMgr

    -- 注册所有ControllerFactory类到ControllerMgr中，按音序排序
    self:RegController(ControllerActivityFactory:new(nil))
    self:RegController(ControllerActorFactory:new(nil))
    self:RegController(ControllerBagFactory:new(nil))
    self:RegController(ControllerDesktopTexasFactory:new(nil))
    self:RegController(ControllerDesktopHFactory:new(nil))
    self:RegController(ControllerGrowFactory:new(nil))
    self:RegController(ControllerIMFactory:new(nil))
    self:RegController(ControllerLobbyFactory:new(nil))
    self:RegController(ControllerLoginFactory:new(nil))
    self:RegController(ControllerLotteryTicketFactory:new(nil))
    self:RegController(ControllerMarqueeFactory:new(nil))
    self:RegController(ControllerMTTFactory:new(nil))
    self:RegController(ControllerPlayerFactory:new(nil))
    self:RegController(ControllerRankingFactory:new(nil))
    self:RegController(ControllerRewardFactory:new(nil))
    self:RegController(ControllerTradeFactory:new(nil))
    self:RegController(ControllerUCenterFactory:new(nil))
    self:RegController(ControllerWalletFactory:new(nil))
end

---------------------------------------
function ControllerMgr:Destroy()
end

---------------------------------------
function ControllerMgr:CreatePlayerControllers(player_data, player_guid)
    self:CreateController("Actor", player_data)
    self:CreateController("Activity", nil)
    self:CreateController("Bag", nil)
    self:CreateController("DesktopTexas", nil)
    self:CreateController("DesktopH", nil)
    self:CreateController("Grow", nil)
    self:CreateController("IM", nil)
    self:CreateController("Lobby", nil)
    self:CreateController("LotteryTicket", nil)
    self:CreateController("Marquee", nil)
    self:CreateController("Ranking", nil)
    self:CreateController("Trade", nil)
    self:CreateController("MTT", nil)
    self:CreateController("Reward", nil)
    self:CreateController("Player", player_guid)
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
function ControllerMgr:CreateController(controller_name, controller_data)
    local controller_factory = self.TableControllerFactory[controller_name]
    if (controller_factory == nil) then
        return nil
    end
    local controller = controller_factory:CreateController(controller_data)
    self.TableController[controller_name] = controller
    controller:OnCreate()
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
function ControllerMgr:RegController(controller_factory)
    self.TableControllerFactory[controller_factory:GetName()] = controller_factory
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
    local p_datas = self.Rpc:PackData(data)
    return p_datas
end

---------------------------------------
function ControllerMgr:UnpackData(data)
    local p_datas = self.Rpc:UnpackData(data)
    return p_datas
end