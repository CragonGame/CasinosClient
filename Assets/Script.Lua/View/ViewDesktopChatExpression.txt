-- Copyright(c) Cragon. All rights reserved.
-- 普通桌本人头像点击弹出的半圆对话框

---------------------------------------
ViewDesktopChatExpression = class(ViewBase)

---------------------------------------
function ViewDesktopChatExpression:ctor()
end

---------------------------------------
function ViewDesktopChatExpression:OnCreate()
    self.ControllerPlayer = self.ControllerMgr:GetController("Player")
    self.ControllerActor = self.ControllerMgr:GetController("Actor")
    self.MapExpression = {}
    for i = 0, 19 do
        local item_name = CS.Casinos.CasinosContext.Instance:AppendStrWithSB("(", tostring(i + 1), ")")
        local com = self.ComUi:GetChild("Exp_" .. tostring(i)).asCom
        com.name = item_name
        com.onClick:Add(
                function(a)
                    self:onClickExp(a)
                end
        )
        local loader = com:GetChild("Loader").asLoader
        self.MapExpression[item_name] = loader
        local item_url = CS.FairyGUI.UIPackage.GetItemURL("Common", item_name)
        loader.icon = item_url
    end
    self.GBtnOk = self.ComUi:GetChild("BtnOk").asButton
    self.GBtnOk.onClick:Add(
            function()
                self:onClickExchangeChip()
            end
    )
    self.ComUi.onClick:Add(
            function()
                self.ViewMgr:DestroyView(self)
            end
    )
end

---------------------------------------
function ViewDesktopChatExpression:onClickExp(context)
    local c_m = ChatMsg:new(nil)
    c_m.recver_guid = ""
    c_m.recver_nickname = ""
    c_m.sender_guid = self.ControllerPlayer.Guid
    c_m.sender_nickname = self.ControllerActor.PropNickName:get()
    c_m.msg = CS.Casinos.LuaHelper.EventDispatcherCastToGComponent(context.sender).name
    local ev = self:GetEv("EvUiSendMsg")
    if (ev == nil) then
        ev = EvUiSendMsg:new(nil)
    end
    ev.chat_msg = c_m:getData4Pack()
    self:SendEv(ev)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewDesktopChatExpression:onClickExchangeChip()
    local ev = self:GetEv("EvUiCreateExchangeChip")
    if (ev == nil) then
        ev = EvUiCreateExchangeChip:new(nil)
    end
    self:SendEv(ev)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewDesktopChatExpressionFactory = class(ViewFactory)

---------------------------------------
function ViewDesktopChatExpressionFactory:CreateView()
    local view = ViewDesktopChatExpression:new()
    return view
end