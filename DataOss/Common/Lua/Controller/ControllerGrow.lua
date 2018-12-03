-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ControllerGrow = class(ControllerBase)

---------------------------------------
function ControllerGrow:ctor(this, controller_data, controller_name)
end

---------------------------------------
function ControllerGrow:OnCreate()
    self.Rpc = self.ControllerMgr.Rpc
    self.MC = CommonMethodType
    -- 成长奖励
    self.Rpc:RegRpcMethod2(self.MC.PlayerGetGrowRewardNotify, function(result, get_golds)
        self:s2cPlayerGetGrowRewardNotify(result, get_golds)
    end)
    -- 成长奖励快照通知
    self.Rpc:RegRpcMethod1(self.MC.PlayerGrowRewardSnapshotNotify, function(grow_data)
        self:s2cPlayerGrowRewardSnapshotNotify(grow_data)
    end)
    self.ViewMgr:BindEvListener("EvUiRequestGetGrowReward", self)
end

---------------------------------------
function ControllerGrow:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ControllerGrow:onUpdate(tm)
    if (self.BGrowData ~= nil) then
        if (self.BGrowData.NextGetRewardLeftTm > 0) then
            self.BGrowData.NextGetRewardLeftTm = self.BGrowData.NextGetRewardLeftTm - tm
            if (self.BGrowData.NextGetRewardLeftTm < 0) then
                self.BGrowData.NextGetRewardLeftTm = 0
            end
        end
    end
end

---------------------------------------
function ControllerGrow:OnHandleEv(ev)
    if (ev.EventName == "EvUiRequestGetGrowReward") then
        self.Rpc:RPC0(self.MC.PlayerGetGrowRewardRequest)
    end
end

---------------------------------------
function ControllerGrow:GetCurrentLevelMaxGetGold(current_level)
    return self.GrowConfig:getCurrentLevelMaxGetGold(current_level)
end

---------------------------------------
function ControllerGrow:s2cPlayerGetGrowRewardNotify(result, get_golds)
    if (result == ProtocolResult.Success) then
        ViewHelper:UiShowInfoSuccess(
                self.ViewMgr.LanMgr:getLanValue("SuccessGet") .. UiChipShowHelper:GetGoldShowStr(get_golds, self.ViewMgr.LanMgr.LanBase) ..
                        self.ViewMgr.LanMgr:getLanValue("Chip"))
    else
        local msg = self.ViewMgr.LanMgr:getLanValue("Get") ..
                self.ViewMgr.LanMgr:getLanValue("Chip") ..
                self.ViewMgr.LanMgr:getLanValue("Failed")
        ViewHelper:UiShowInfoFailed(msg)
    end
end

---------------------------------------
function ControllerGrow:s2cPlayerGrowRewardSnapshotNotify(grow_data)
    local data = BGrowData:new(nil)
    data:setData(grow_data)
    self.BGrowData = data
    local ev = self:GetEv("EvEntityOnGrowRewardSnapshot")
    if (ev == nil) then
        ev = EvEntityOnGrowRewardSnapshot:new(nil)
    end
    ev.grow_data = data
    self:SendEv(ev)
end

---------------------------------------
ControllerGrowFactory = class(ControllerFactory)

function ControllerGrowFactory:GetName()
    return 'Grow'
end

function ControllerGrowFactory:CreateController(controller_data)
    local ctrl_name = self:GetName()
    local ctrl = ControllerGrow:new(controller_data, ctrl_name)
    return ctrl
end