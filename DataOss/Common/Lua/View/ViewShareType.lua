-- Copyright(c) Cragon. All rights reserved.
-- 选择分享类型的对话框

---------------------------------------
ViewShareType = class(ViewBase)

---------------------------------------
function ViewShareType:ctor()
end

---------------------------------------
function ViewShareType:OnCreate()
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

---------------------------------------
function ViewShareType:onClickBtnWeChat()
    local ev = self:GetEv("EvClickShare")
    if (ev == nil) then
        ev = EvClickShare:new(nil)
    end
    ev.ShareType = ShareType.WeChat
    self:SendEv(ev)
end

---------------------------------------
function ViewShareType:onClickBtnWeChatMoments()
    local ev = self:GetEv("EvClickShare")
    if (ev == nil) then
        ev = EvClickShare:new(nil)
    end
    ev.ShareType = ShareType.WeChatMoments
    self:SendEv(ev)
end

---------------------------------------
function ViewShareType:onClickClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewShareTypeFactory = class(ViewFactory)

---------------------------------------
function ViewShareTypeFactory:CreateView()
    local view = ViewShareType:new()
    return view
end