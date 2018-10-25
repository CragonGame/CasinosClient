-- Copyright(c) Cragon. All rights reserved.
-- 跑马灯，消息排队播放

---------------------------------------
ControllerMarquee = ControllerBase:new(nil)

---------------------------------------
function ControllerMarquee:new(o, controller_mgr, controller_data, guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ControllerData = controller_data
    o.ControllerMgr = controller_mgr
    o.Guid = guid
    o.ViewMgr = ViewMgr:new(nil)
    o.MaxMarqueeCount = 20
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    return o
end

---------------------------------------
function ControllerMarquee:onCreate()
    self.ViewMgr:BindEvListener("EvRequestSendMarquee", self)
    self.RPC = self.ControllerMgr.RPC
    self.MC = CommonMethodType
    -- 跑马灯，响应玩家发送跑马灯广播请求
    self.RPC:RegRpcMethod1(self.MC.MarqueeRequestResult, function(result)
        self:s2cMarqueeRequestResult(result)
    end)
    -- 跑马灯广播推送
    self.RPC:RegRpcMethod1(self.MC.IMMarqueeNotify, function(im_marquee)
        self:s2cIMMarqueeNotify(im_marquee)
    end)
    self.ListIMMarquee = {}
    self.MapQueMarquee = {}
end

---------------------------------------
function ControllerMarquee:onDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ControllerMarquee:onHandleEv(ev)
    if (ev ~= nil)
    then
        if (ev.EventName == "EvRequestSendMarquee")
        then
            local im_marquee = BIMMarquee:new(nil)
            im_marquee.SenderType = IMMarqueeSenderType.Player
            im_marquee.SenderGuid = self.Guid
            im_marquee.NickName = ""
            im_marquee.Priority = IMMarqueePriority.Normal
            im_marquee.Msg = ev.msg
            self:requestSendMarquee(im_marquee:getData4Pack())
        end
    end
end

---------------------------------------
function ControllerMarquee:s2cMarqueeRequestResult(result)
    if (result == ProtocolResult.Failed)
    then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("SendNoticeFailed"))
    end
end

---------------------------------------
function ControllerMarquee:s2cIMMarqueeNotify(im_marquee)
    local data = BIMMarquee:new(nil)
    data:setData(im_marquee)
    local view_shootingtext = self.ControllerMgr.ViewMgr:GetView("ShootingText")
    if (view_shootingtext == nil)
    then
        view_shootingtext = self.ControllerMgr.ViewMgr:CreateView("ShootingText")
        view_shootingtext:init(false, true, true)
    end

    local priority = data.Priority
    local que_marquee = nil
    for key, value in pairs(self.MapQueMarquee) do
        if (key == priority)
        then
            que_marquee = value
            break
        end
    end
    if (que_marquee == nil)
    then
        que_marquee = {}
        self.MapQueMarquee[priority] = que_marquee
    end
    table.insert(que_marquee, data)

    local list_count = #self.ListIMMarquee
    if (list_count >= self.MaxMarqueeCount)
    then
        for i = 1, list_count - self.MaxMarqueeCount + 1 do
            self.ListIMMarquee[i] = nil
        end
    end
    table.insert(self.ListIMMarquee, data)

    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityReceiceMarquee")
    if (ev == nil)
    then
        ev = EvEntityReceiceMarquee:new(nil)
    end
    ev.im_marquee = data
    self.ControllerMgr.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerMarquee:requestSendMarquee(im_marque)
    print("ControllerMarquee:requestSendMarquee")
    self.RPC:RPC1(self.MC.MarqueeRequest, im_marque)
end

---------------------------------------
function ControllerMarquee:getNeedShowMarquee()
    local marquee = nil
    local que_highpriority = nil
    local contains_key = false
    for key, value in pairs(self.MapQueMarquee) do
        if (key == IMMarqueePriority.High)
        then
            que_highpriority = value
            contains_key = true
            break
        end
    end
    if (contains_key == true)
    then
        if (#que_highpriority > 0)
        then
            marquee = table.remove(que_highpriority, 1)
        end
    end

    if (marquee == nil)
    then
        local que_normalpriority = nil
        local contains_key2 = false
        for key, value in pairs(self.MapQueMarquee) do
            if (key == IMMarqueePriority.Normal)
            then
                que_normalpriority = value
                contains_key2 = true
                break
            end
        end
        if (contains_key2 == true)
        then
            if (#que_normalpriority > 0)
            then
                marquee = table.remove(que_normalpriority, 1)
            end
        end
    end

    return marquee
end

---------------------------------------
function ControllerMarquee:haveNeedShowMarquee()
    local have_marquee = false
    if (self.MapQueMarquee ~= nil)
    then
        for key, value in pairs(self.MapQueMarquee) do
            if (#value > 0)
            then
                have_marquee = true
                break
            end
        end
    end

    return have_marquee
end

---------------------------------------
ControllerMarqueeFactory = ControllerFactory:new()

---------------------------------------
function ControllerMarqueeFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerName = "Marquee"
    return o
end

---------------------------------------
function ControllerMarqueeFactory:createController(controller_mgr, controller_data, guid)
    local controller = ControllerMarquee:new(nil, controller_mgr, controller_data, guid)
    return controller
end