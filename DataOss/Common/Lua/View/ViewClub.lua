-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewClub = class(ViewBase)

---------------------------------------
function ViewClub:ctor()
    self.UpdatePlayerNumTime = 0
end

---------------------------------------
function ViewClub:OnCreate()
    self.ControllerMgr.ViewMgr:BindEvListener("EvEntitySetPrivateMatchLsit", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvEntityUpdatePrivateMatchPlayerNum", self)
    self.ControllerMgr.ViewMgr:BindEvListener("EvEntityGetMatchInfoByInvitationCodeSucceed", self)
    self.GTransitionShow = self.ComUi:GetTransition("TransitionShow")
    self.GTransitionShow:Play()
    local btn_return = self.ComUi:GetChild("BtnReturn").asButton
    btn_return.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )
    local btn_creatematch = self.ComUi:GetChild("BtnCreateMatch").asButton
    btn_creatematch.onClick:Add(
            function()
                self:onClickBtnCreateMatch()
            end
    )
    local btn_createtable = self.ComUi:GetChild("BtnCreateTable").asButton
    btn_createtable.onClick:Add(
            function()
                self:onClickBtnCreateTable()
            end
    )
    local btn_joinMatch = self.ComUi:GetChild("BtnJoinMatch").asButton
    btn_joinMatch.onClick:Add(
            function()
                self:onClickBtnJoinMatch()
            end
    )
    local btn_club = self.ComUi:GetChild("BtnClub").asButton
    btn_club.onClick:Add(
            function()
                self:onClickBtnClub()
            end
    )
    local btn_clubHelp = self.ComUi:GetChild("BtnHelp").asButton
    btn_clubHelp.onClick:Add(
            function()
                self:onClickBtnHelp()
            end
    )
    self.GListAlreadyCreatedMatch = self.ComUi:GetChild("ListMatch").asList
    --self:requesetPrivateMatchList()
    self.ListItemMatch = {}
end

---------------------------------------
function ViewClub:OnDestroy()
end

---------------------------------------
function ViewClub:onUpdate(tm)
    self.UpdatePlayerNumTime = self.UpdatePlayerNumTime + tm
    if (self.UpdatePlayerNumTime >= 30) then
        local ev = self.ViewMgr:GetEv("EvUiRequestUpdatePrivateMatchPlayerNum")
        if (ev == nil) then
            ev = EvUiRequestUpdatePrivateMatchPlayerNum:new(nil)
        end
        self.ViewMgr:SendEv(ev)
        self.UpdatePlayerNumTime = 0
    end
end

---------------------------------------
function ViewClub:OnHandleEv(ev)
    if (ev.EventName == "EvEntitySetPrivateMatchLsit") then
        self:setAlreadyCreatedMatchList(ev.ListMatch, ev.ListApplyMatchGuid)
    elseif (ev.EventName == "EvEntityUpdatePrivateMatchPlayerNum") then
        local list_matchnum = ev.ListMatchNum
        for i = 1, #list_matchnum do
            local guid_num = list_matchnum[i]
            for i = 1, #self.ListItemMatch do
                local temp = self.ListItemMatch[i]
                if (guid_num.Guid == temp.MatchInfo.Guid) then
                    temp:UpdatePlayerNum(guid_num.PlayerNum)
                end
            end
        end
    elseif (ev.EventName == "EvEntityGetMatchInfoByInvitationCodeSucceed") then
        local match_guid = ev.MatchGuid
        local isSelfJoin = false
        for i = 1, #self.ListItemMatch do
            if (match_guid == self.ListItemMatch[i].MatchGuid and self.ListItemMatch[i].IsSelfJoin == true) then
                isSelfJoin = true
            end
        end
        local view_privateMatchInfo = self.ViewMgr:CreateView("PrivateMatchInfo")
        view_privateMatchInfo:Init(match_guid, isSelfJoin)
    end
end

---------------------------------------
function ViewClub:onClickBtnReturn()
    self.ViewMgr:DestroyView(self)
    local ev = self.ViewMgr:GetEv("EvUiCreateMainUi")
    if (ev == nil) then
        ev = EvUiCreateMainUi:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewClub:onClickBtnCreateMatch()
    self.ViewMgr:CreateView("CreateMatch")
end

---------------------------------------
function ViewClub:onClickBtnCreateTable()
    ViewHelper:UiShowInfoSuccess("施工中，敬请期待")
end

---------------------------------------
function ViewClub:onClickBtnJoinMatch()
    self.ViewMgr:CreateView("JoinMatch")
end

---------------------------------------
function ViewClub:onClickBtnClub()
    ViewHelper:UiShowInfoSuccess("施工中，敬请期待")
end

---------------------------------------
function ViewClub:onClickBtnHelp()
    self.ViewMgr:CreateView("ClubHelp")
end

---------------------------------------
function ViewClub:setAlreadyCreatedMatchList(list_match, list_applyguid)
    self.ListItemMatch = {}
    self.GListAlreadyCreatedMatch:RemoveChildrenToPool()
    if (list_match == nil or #list_match == 0) then
        return
    end
    for i = 1, #list do
        local match = list[i]
        local com = self.GListAlreadyCreatedMatch:AddItemFromPool()
        local item = ItemPrivateMatch:new(nil, com, match)
        self.ListItemMatch[i] = item
    end
    if (list_applyguid ~= nil and #list_applyguid > 0) then
        for i = 1, #list_applyguid do
            local guid = list_applyguid[i]
            for i = 1, #self.ListItemMatch do
                local temp = self.ListItemMatch[i]
                if (guid == temp.Guid) then
                    temp:SetMatchState("已经报名")
                    temp.IsSelfJoin = true
                    break
                end
            end
        end
    end
end

---------------------------------------
function ViewClub:requesetPrivateMatchList()
    local ev = self.ViewMgr:GetEv("EvUiRequestPrivateMatchList")
    if (ev == nil) then
        ev = EvUiRequestPrivateMatchList:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
ViewClubFactory = class(ViewFactory)

---------------------------------------
function ViewClubFactory:CreateView()
    local view = ViewClub:new()
    return view
end