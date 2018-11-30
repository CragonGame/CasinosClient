-- Copyright(c) Cragon. All rights reserved.
-- 屏幕中间悬停一段时间的提示框

---------------------------------------
ViewPermanentPosMsg = ViewBase:new()

---------------------------------------
function ViewPermanentPosMsg:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    self.TimerUpdate = nil
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    return o
end

---------------------------------------
function ViewPermanentPosMsg:OnCreate()
    local text = self.ComUi:GetChild("MsgText")
    if (text ~= nil) then
        self.GTextMsg = text.asTextField
    end

    self.AutoDestroyTm = 3
    self.Tm = 0
    self.ComUi.touchable = false

    self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(200, self, self._timerUpdate)
end

---------------------------------------
function ViewPermanentPosMsg:OnDestroy()
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
end

---------------------------------------
function ViewPermanentPosMsg:OnHandleEv(ev)
end

---------------------------------------
function ViewPermanentPosMsg:_timerUpdate(tm)
    self.Tm = self.Tm + tm
    if (self.Tm >= self.AutoDestroyTm) then
        self.ViewMgr:DestroyView(self)
    end
end

---------------------------------------
function ViewPermanentPosMsg:showInfo(info)
    self.GTextMsg.text = info
end

---------------------------------------
ViewPermanentPosMsgFactory = class(ViewFactory)

---------------------------------------
function ViewPermanentPosMsgFactory:CreateView()
    local view = ViewPermanentPosMsg:new(nil)
    return view
end