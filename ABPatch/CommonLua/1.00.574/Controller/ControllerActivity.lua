ControllerActivity = ControllerBase:new(nil)

function ControllerActivity:new(o,controller_mgr,controller_data,guid)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self

	o.ControllerData = controller_data
	o.ControllerMgr = controller_mgr
	o.Guid = guid
	o.CurrentActID = "Act180228"
	o.ViewMgr = ViewMgr:new(nil)

    return o
end

function ControllerActivity:onCreate()
	self.RPC = self.ControllerMgr.RPC
	self.MC = CommonMethodType
	-- 活动推送通知
	self.RPC:RegRpcMethod1(self.MC.ActivityNotify,function(list_activity)
		self:s2cActivityNotify(list_activity)
	end)
	self.ViewMgr:bindEvListener("EvUiRequestGetActivity",self)
	self.ControllerPlayer = self.ControllerMgr:GetController("ControllerPlayer")
	self.ListActivity = {}
	self:ConfigActivityInfo()
end

function ControllerActivity:ConfigActivityInfo()
	CS.UnityEngine.PlayerPrefs.DeleteKey("Act180228")
	local temp = ItemActivity:new(nil,"",self.ViewMgr.LanMgr:getLanValue("OfficialTipsTitle"),self.ViewMgr.LanMgr:getLanValue("OfficialTipsContent"),"",false)
	table.insert(self.ListActivity,temp)
	temp = ItemActivity:new(nil,"",self.ViewMgr.LanMgr:getLanValue("Share"),nil,"Share",true)
	table.insert(self.ListActivity,temp)
end


function ControllerActivity:onDestroy()
	self.ViewMgr:unbindEvListener(self)
end

function ControllerActivity:onHandleEv(ev)
	if(ev.EventName == "EvUiRequestGetActivity")
	then
		self.RPC:RPC0(self.MC.ActivityRequest)
	end
end

function ControllerActivity:s2cActivityNotify(list_activity)
	self.ListActivity = list_activity
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityNotifyPushActivity")
	if(ev == nil)
	then
		ev = EvEntityNotifyPushActivity:new(nil)
	end
	self.ControllerMgr.ViewMgr:sendEv(ev)
end



ControllerActivityFactory = ControllerFactory:new()

function ControllerActivityFactory:new(o)
	o = o or {}  
    setmetatable(o,self)  
    self.__index = self
	self.ControllerName = "Activity"
    return o
end

function ControllerActivityFactory:createController(controller_mgr,controller_data,guid)
	local controller = ControllerActivity:new(nil,controller_mgr,controller_data,guid)
	return controller
end