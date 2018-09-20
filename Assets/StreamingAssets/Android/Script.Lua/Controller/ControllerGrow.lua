-- Copyright(c) Cragon. All rights reserved.

ControllerGrow = ControllerBase:new(nil)

function ControllerGrow:new(o,controller_mgr,controller_data,guid)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self

	o.ControllerData = controller_data
	o.ControllerMgr = controller_mgr
	o.Guid = guid
	o.ViewMgr = ViewMgr:new(nil)
	o.CasinosContext = CS.Casinos.CasinosContext.Instance

    return o
end

function ControllerGrow:onCreate()
	self.RPC = self.ControllerMgr.RPC
	self.MC = CommonMethodType
	-- 成长奖励
	self.RPC:RegRpcMethod2(self.MC.PlayerGetGrowRewardNotify,function(result,get_golds)
		self:s2cPlayerGetGrowRewardNotify(result,get_golds)
	end)
	-- 成长奖励快照通知
	self.RPC:RegRpcMethod1(self.MC.PlayerGrowRewardSnapshotNotify,function(grow_data)
		self:s2cPlayerGrowRewardSnapshotNotify(grow_data)
	end)
	self.ViewMgr:bindEvListener("EvUiRequestGetGrowReward",self)
end

function ControllerGrow:onDestroy()
	self.ViewMgr:unbindEvListener(self)
end

function ControllerGrow:onUpdate(tm)
	if (self.BGrowData ~= nil)
	then
		if (self.BGrowData.NextGetRewardLeftTm > 0)
		then
			self.BGrowData.NextGetRewardLeftTm = self.BGrowData.NextGetRewardLeftTm - tm
            if (self.BGrowData.NextGetRewardLeftTm < 0)
			then
				self.BGrowData.NextGetRewardLeftTm = 0
			end
		end
	end
end

function ControllerGrow:onHandleEv(ev)
	if(ev.EventName == "EvUiRequestGetGrowReward")
	then
		self.RPC:RPC0(self.MC.PlayerGetGrowRewardRequest)
	end
end

function ControllerGrow:GetCurrentLevelMaxGetGold(current_level)
	return self.GrowConfig:getCurrentLevelMaxGetGold(current_level)
end

function ControllerGrow:s2cPlayerGetGrowRewardNotify(result,get_golds)
	if (result == ProtocolResult.Success)
	then
		ViewHelper:UiShowInfoSuccess(
        self.ViewMgr.LanMgr:getLanValue("SuccessGet")..UiChipShowHelper:getGoldShowStr(get_golds, self.ViewMgr.LanMgr.LanBase)..
				self.ViewMgr.LanMgr:getLanValue("Chip"))
	else
		local msg = self.ViewMgr.LanMgr:getLanValue("Get")..
				self.ViewMgr.LanMgr:getLanValue("Chip")..
					self.ViewMgr.LanMgr:getLanValue("Failed")
        ViewHelper:UiShowInfoFailed(msg)
	end
end

function ControllerGrow:s2cPlayerGrowRewardSnapshotNotify(grow_data)
	local data = BGrowData:new(nil)
	data:setData(grow_data)
	 self.BGrowData = data
	 local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityOnGrowRewardSnapshot")
	 if(ev == nil)
	 then
		ev = EvEntityOnGrowRewardSnapshot:new(nil)
	 end
     ev.grow_data = data
     self.ControllerMgr.ViewMgr:sendEv(ev)
end


ControllerGrowFactory = ControllerFactory:new()

function ControllerGrowFactory:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self
	self.ControllerName = "Grow"
    return o
end

function ControllerGrowFactory:createController(controller_mgr,controller_data,guid)
	local controller = ControllerGrow:new(nil,controller_mgr,controller_data,guid)
	return controller
end