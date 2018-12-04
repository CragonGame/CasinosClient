-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewChatChooseTarget = class(ViewBase)

---------------------------------------
function ViewChatChooseTarget:ctor()
    self.Context = Context
    self.Tween = nil
end

---------------------------------------
function ViewChatChooseTarget:OnCreate()
    self.Tween = ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("ChooseFriendChat"))
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerIM = self.ControllerMgr:GetController("IM")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:onClickClose()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickClose()
            end
    )
    self.ControllerHaveFriend = self.ComUi:GetController("ControllerHaveFriend")
    self.GTextInputSearchTarget = self.ComUi:GetChild("TextInputSearch").asTextInput
    self.GListChatTarget = self.ComUi:GetChild("ListChatTarget").asList
    self.GListChatTarget:SetVirtual()
    self.GListChatTarget.itemRenderer = function(a, b)
        self:RenderListItemChatTarget(a, b)
    end
    self.ViewMgr:BindEvListener("EvUiClickChooseFriend", self)
end

---------------------------------------
function ViewChatChooseTarget:OnDestroy()
    if self.Tween ~= nil then
        self.Tween:Kill(false)
        self.Tween = nil
    end
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewChatChooseTarget:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvUiClickChooseFriend") then
            self:onClickClose()
        end
    end
end

---------------------------------------
function ViewChatChooseTarget:setFriendInfo(map_frienditem)
    if (map_frienditem == nil or LuaHelper:GetTableCount(map_frienditem) == 0) then
        self.ControllerHaveFriend.selectedIndex = 1
        return
    end
    self.ControllerHaveFriend.selectedIndex = 0
    self.GListChatTarget.numItems = LuaHelper:GetTableCount(map_frienditem)
end

---------------------------------------
function ViewChatChooseTarget:RenderListItemChatTarget(index, obj)
    local list_have_record_friend = self.ControllerIM.IMFriendList.ListFriendGuid
    local com = CS.Casinos.LuaHelper.GObjectCastToGCom(obj)
    if (self.Context.Cfg.UseLan) then
        self.ViewMgr.LanMgr:parseComponent(com)
    end
    local item = ItemChooseChatTargetInfo:new(nil, com, self.ControllerIM)
    if (#list_have_record_friend > index) then
        local friend_guid = list_have_record_friend[index + 1]
        local player_info = self.ControllerIM.IMFriendList.MapFriendList[friend_guid]
        if (player_info ~= nil) then
            item:setFriendInfo(player_info)
        end
    end
end

---------------------------------------
function ViewChatChooseTarget:onClickClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewChatChooseTargetFactory = class(ViewFactory)

---------------------------------------
function ViewChatChooseTargetFactory:CreateView()
    local view = ViewChatChooseTarget:new()
    return view
end