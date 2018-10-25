-- Copyright(c) Cragon. All rights reserved.
-- 浮动提示

---------------------------------------
MsgInfo = {}

---------------------------------------
function MsgInfo:new(o, info, color)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.info = info
    o.color = color
    return o
end

---------------------------------------
ViewFloatMsg = ViewBase:new()

---------------------------------------
function ViewFloatMsg:new(o)
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
    o.GTextInfos = {}
    o.TransitionMoves = {}
    o.mQueMsgInfo = {}
    o.canSend = false
    return o
end

---------------------------------------
function ViewFloatMsg:onCreate()
    self.canSend = true
    self.GTextInfos[0] = self.ComUi:GetChild("GTextInfo0").asTextField
    self.GTextInfos[1] = self.ComUi:GetChild("GTextInfo1").asTextField
    self.GTextInfos[2] = self.ComUi:GetChild("GTextInfo2").asTextField
    self.TransitionMoves[0] = self.ComUi:GetTransition("TransitionMove0")
    self.TransitionMoves[1] = self.ComUi:GetTransition("TransitionMove1")
    self.TransitionMoves[2] = self.ComUi:GetTransition("TransitionMove2")
    self.ComUi.touchable = false
end

---------------------------------------
function ViewFloatMsg:showInfo(info, color)
    local msgInfo = MsgInfo:new(nil, info, color)
    local index = self:usableGTextIndex()
    if (self.canSend == true and index ~= -1) then
        self:showMsg(msgInfo, index)
    else
        table.insert(self.mQueMsgInfo, msgInfo)
    end
end

---------------------------------------
function ViewFloatMsg:showMsg(info, gTextIndex)
    self.GTextInfos[gTextIndex].text = info.info
    self.GTextInfos[gTextIndex].color = info.color
    self.TransitionMoves[gTextIndex]:Play(
            function()
                self:onTransitionComplete(gTextIndex)
            end)
    self.TransitionMoves[gTextIndex]:SetHook("aa",
            function()
                self:onTransitionToaa()
            end
    )
    self.canSend = false
end

---------------------------------------
function ViewFloatMsg:onTransitionToaa()
    self.canSend = true
    local l = #self.mQueMsgInfo
    if (l > 0) then
        local index = self:usableGTextIndex()
        if (index ~= -1) then
            local info = table.remove(self.mQueMsgInfo, 1)
            self:showMsg(info, index)
        end
    end
end

---------------------------------------
function ViewFloatMsg:onTransitionComplete(index)
    self.GTextInfos[index].text = ""
    if (#self.mQueMsgInfo > 0) then
        local info = table.remove(self.mQueMsgInfo, 1)
        self:showMsg(info, index)
    end
end

---------------------------------------
function ViewFloatMsg:usableGTextIndex()
    for i = 0, 2 do
        if (self.TransitionMoves[i].playing == false) then
            return i
        end
    end
    return -1
end

---------------------------------------
ViewFloatMsgFactory = ViewFactory:new()

---------------------------------------
function ViewFloatMsgFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewFloatMsgFactory:CreateView()
    local view = ViewFloatMsg:new(nil)
    return view
end