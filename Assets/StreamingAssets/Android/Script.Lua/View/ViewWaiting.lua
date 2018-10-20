-- Copyright(c) Cragon. All rights reserved.
-- 全屏等待界面

---------------------------------------
ViewWaiting = ViewBase:new()

---------------------------------------
function ViewWaiting:new(o)
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
function ViewWaiting:onCreate()
    local text = self.ComUi:GetChild("Tips")
    if (text ~= nil) then
        self.GTextTips = text.asTextField
    end

    self.AutoDestroyTm = 5
    self.Tm = 0
    self.RandomTips = {}
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips1"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips2"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips3"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips4"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips5"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips6"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips7"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips8"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips9"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips10"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips11"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips12"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips13"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips14"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips15"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips16"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips17"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips18"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips19"))
    table.insert(self.RandomTips, self.ViewMgr.LanMgr:getLanValue("WaitingTips20"))

    self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(100, self, self._timerUpdate)
end

---------------------------------------
function ViewWaiting:onDestroy()
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
end

---------------------------------------
function ViewWaiting:onHandleEv(ev)
end

---------------------------------------
function ViewWaiting:_timerUpdate(tm)
    self.Tm = self.Tm + tm
    if (self.Tm >= self.AutoDestroyTm) then
        self.ViewMgr:destroyView(self)
    end
end

---------------------------------------
function ViewWaiting:setTips(tips, auto_destroytm, random_tips)
    if (random_tips == nil) then
        random_tips = true
    end

    if (tips == nil or string.len(tips) == 0) then
        if (random_tips) then
            local temp = nil
            if (#self.RandomTips > 1) then
                temp = CS.UnityEngine.Random.Range(1, #self.RandomTips + 0.99)
                temp = math.floor(temp)
                self.GTextTips.text = self.RandomTips[temp]
            else
                temp = ""
            end
        end
    else
        self.GTextTips.text = tips
    end
    if (auto_destroytm == nil) then
        self.AutoDestroyTm = 5
    end
end

---------------------------------------
ViewWaitingFactory = ViewFactory:new()

---------------------------------------
function ViewWaitingFactory:new(o, ui_package_name, ui_component_name,
                                ui_layer, is_single, fit_screen)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
    return o
end

---------------------------------------
function ViewWaitingFactory:createView()
    local view = ViewWaiting:new(nil)
    return view
end