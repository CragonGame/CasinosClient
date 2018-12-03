-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ControllerActivity = class(ControllerBase)

---------------------------------------
function ControllerActivity:ctor(this, controller_data, controller_name)
    self.CurrentActID = "Act180228"
end

---------------------------------------
function ControllerActivity:OnCreate()
    self.RPC = self.ControllerMgr.RPC
    self.MC = CommonMethodType
    -- 活动推送通知
    self.RPC:RegRpcMethod1(self.MC.ActivityNotify, function(list_activity)
        self:s2cActivityNotify(list_activity)
    end)
    self.ViewMgr:BindEvListener("EvUiRequestGetActivity", self)
    self.ControllerPlayer = self.ControllerMgr:GetController("ControllerPlayer")
    self.ListActivity = {}
    self:ConfigActivityInfo()
end

---------------------------------------
function ControllerActivity:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ControllerActivity:OnHandleEv(ev)
    if (ev.EventName == "EvUiRequestGetActivity") then
        self.RPC:RPC0(self.MC.ActivityRequest)
    end
end

---------------------------------------
function ControllerActivity:s2cActivityNotify(list_activity)
    self.ListActivity = list_activity
    local ev = self.ViewMgr:GetEv("EvEntityNotifyPushActivity")
    if (ev == nil) then
        ev = EvEntityNotifyPushActivity:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
-- 配置了content_image后的活动Item，会从Oss.Active目录去加载图片
function ControllerActivity:ConfigActivityInfo()
    CS.UnityEngine.PlayerPrefs.DeleteKey("Act180228")
    local temp = ItemActivity:new(nil, "Hot", self.ViewMgr.LanMgr:getLanValue("KeFuWeChatQRCode"), nil, "KeFuWeChatQRCode", false)
    table.insert(self.ListActivity, temp)
    temp = ItemActivity:new(nil, "", self.ViewMgr.LanMgr:getLanValue("OfficialTipsTitle"), self.ViewMgr.LanMgr:getLanValue("OfficialTipsContent"), "", false)
    table.insert(self.ListActivity, temp)
    temp = ItemActivity:new(nil, "", self.ViewMgr.LanMgr:getLanValue("Share"), nil, "Share", true)
    table.insert(self.ListActivity, temp)
end

---------------------------------------
ControllerActivityFactory = class(ControllerFactory)

function ControllerActivityFactory:GetName()
    return 'Activity'
end

function ControllerActivityFactory:CreateController(controller_data)
    local ctrl_name = self:GetName()
    local ctrl = ControllerActivity:new(controller_data, ctrl_name)
    return ctrl
end