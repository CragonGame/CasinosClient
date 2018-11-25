-- Copyright(c) Cragon. All rights reserved.
require('RewardOnline')
require('RewardTiming')

---------------------------------------
ControllerReward = ControllerBase:new(nil)

---------------------------------------
function ControllerReward:new(o, controller_mgr, controller_data, guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Context = Context
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.ControllerData = controller_data
    o.ControllerMgr = controller_mgr
    o.Guid = guid
    o.ViewMgr = ViewMgr:new(nil)
    o.RewardOnline = RewardOnline:new(nil, o.ViewMgr)
    o.RewardTiming = RewardTiming:new(nil, o.ViewMgr)
    return o
end

---------------------------------------
function ControllerReward:OnCreate()
    --print('ControllerReward:OnCreate()')
end

---------------------------------------
function ControllerReward:OnDestroy()
    --print('ControllerReward:OnDestroy()')
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ControllerReward:OnHandleEv(ev)
end

---------------------------------------
ControllerRewardFactory = ControllerFactory:new()

---------------------------------------
function ControllerRewardFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerName = "Reward"
    return o
end

---------------------------------------
function ControllerRewardFactory:CreateController(controller_mgr, controller_data, guid)
    local controller = ControllerReward:new(nil, controller_mgr, controller_data, guid)
    return controller
end