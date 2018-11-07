-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewAgreeOrDisAddFriendRequest = ViewBase:new()

---------------------------------------
function ViewAgreeOrDisAddFriendRequest:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.Context = Context
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    self.FriendGuid = nil
    return o
end

---------------------------------------
function ViewAgreeOrDisAddFriendRequest:OnCreate()
    ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("FriendRequest"))
    self.ControllerPlayer = self.ViewMgr.ControllerMgr:GetController("Player")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local head_icon = self.ComUi:GetChild("CoHeadIcon").asCom
    self.ViewHeadIcon = ViewHeadIcon:new(nil, head_icon)
    self.GTextNickName = self.ComUi:GetChild("NickName").asTextField
    self.GTextChips = self.ComUi:GetChild("Chips").asTextField
    self.GTextLevel = self.ComUi:GetChild("Level").asTextField
    self.GBtnConfirm = self.ComUi:GetChild("Lan_Btn_Accept").asButton
    self.GBtnConfirm.onClick:Add(
            function()
                self:onClickBtnConfirm()
            end
    )
    self.ChipIconSolustion = self.ComUi:GetController("ChipIconSolustion")
    self.ChipIconSolustion.selectedIndex = self.Context.Cfg.ChipIconSolustion
    self.ViewMgr:BindEvListener("EvEntityGetPlayerInfoOther", self)
end

---------------------------------------
function ViewAgreeOrDisAddFriendRequest:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewAgreeOrDisAddFriendRequest:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvEntityGetPlayerInfoOther") then
            local player_info = ev.player_info
            if ((self.FriendGuid ~= nil and string.len(self.FriendGuid) > 0) and self.FriendGuid == player_info.PlayerInfoCommon.PlayerGuid and self.Ticket == ev.ticket) then
                self:SetPlayerInfo(player_info)
            end
        end
    end
end

---------------------------------------
function ViewAgreeOrDisAddFriendRequest:addFriend(ev)
    self.IMOfflineEvent = ev
    local from_player_guid = ev.MapData["FromPlayerGuid"]
    local ticket = self.ControllerPlayer:requestGetPlayerInfoOther(from_player_guid)
    self.FriendGuid = from_player_guid
    self.Ticket = ticket
end

---------------------------------------
function ViewAgreeOrDisAddFriendRequest:SetPlayerInfo(player_info)
    self.FriendInfo = player_info
    self.ViewHeadIcon:SetPlayerInfo(player_info.PlayerInfoCommon.IconName, player_info.PlayerInfoCommon.AccountId, player_info.PlayerInfoCommon.VIPLevel)
    self.GTextNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(player_info.PlayerInfoCommon.NickName, 33, 10)
    self.GTextChips.text = UiChipShowHelper:getGoldShowStr(player_info.PlayerInfoMore.Gold, self.ViewMgr.LanMgr.LanBase)
    local level = self.ViewMgr.LanMgr:getLanValue("Level")
    self.GTextLevel.text = level .. player_info.PlayerInfoMore.Level
end

---------------------------------------
function ViewAgreeOrDisAddFriendRequest:onClickBtnConfirm()
    local ev = self.ViewMgr:GetEv("EvUiAgreeAddFriend")
    if (ev == nil) then
        ev = EvUiAgreeAddFriend:new(nil)
    end
    ev.from_etguid = self.FriendGuid
    ev.ev = self.IMOfflineEvent
    self.ViewMgr:SendEv(ev)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewAgreeOrDisAddFriendRequest:_onClickBtnClose()
    local ev = self.ViewMgr:GetEv("EvUiRefuseAddFriend")
    if (ev == nil) then
        ev = EvUiRefuseAddFriend:new(nil)
    end
    ev.from_etguid = self.FriendGuid
    ev.ev = self.IMOfflineEvent
    self.ViewMgr:SendEv(ev)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewAgreeOrDisAddFriendRequestFactory = ViewFactory:new()

---------------------------------------
function ViewAgreeOrDisAddFriendRequestFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewAgreeOrDisAddFriendRequestFactory:CreateView()
    local view = ViewAgreeOrDisAddFriendRequest:new(nil)
    return view
end