-- Copyright(c) Cragon. All rights reserved.
-- 选择分享类型的对话框

ViewShareType = ViewBase:new()

function ViewShareType:new(o)
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

    return o
end

function ViewShareType:onCreate()
    self.ComUi.onClick:Add(
            function()
                self:onClickClose()
            end)

    self.BtnWeChat = self.ComUi:GetChild("BtnWeChat").asButton
    self.BtnWeChat.onClick:Add(
            function()
                self:onClickBtnWeChat()
            end
    )
    self.BtnWeChatMoments = self.ComUi:GetChild("BtnWeChatMoments").asButton
    self.BtnWeChatMoments.onClick:Add(
            function()
                self:onClickBtnWeChatMoments()
            end
    )
end

function ViewShareType:onUpdate(tm)
end

function ViewShareType:onClickBtnWeChat()
    local ev = self.ViewMgr:getEv("EvClickShare")
    if(ev == nil)
    then
        ev = EvClickShare:new(nil)
    end
    ev.ShareType = ShareType.WeChat
    self.ViewMgr:sendEv(ev)
end

function ViewShareType:onClickBtnWeChatMoments()
    local ev = self.ViewMgr:getEv("EvClickShare")
    if(ev == nil)
    then
        ev = EvClickShare:new(nil)
    end
    ev.ShareType = ShareType.WeChatMoments
    self.ViewMgr:sendEv(ev)
end

function ViewShareType:onClickClose()
    self.ViewMgr:destroyView(self)
end

ViewShareTypeFactory = ViewFactory:new()

function ViewShareTypeFactory:new(o, ui_package_name, ui_component_name,
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

function ViewShareTypeFactory:createView()
    local view = ViewShareType:new(nil)
    return view
end