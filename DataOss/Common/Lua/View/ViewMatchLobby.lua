-- Copyright(c) Cragon. All rights reserved.
-- 赛事列表

---------------------------------------
ViewMatchLobby = class(ViewBase)

---------------------------------------
function ViewMatchLobby:ctor()
    self.Context = Context
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.TimerUpdate = nil
    self.UpdatePlayerNumTime = 0
    self.ListMatchItem = {}
    self.GTextTime = nil
end

---------------------------------------
function ViewMatchLobby:OnCreate()
    self.ViewMgr:BindEvListener("EvEntityGoldChanged", self)
    self.ViewMgr:BindEvListener("EvEntitySetPublicMatchLsit", self)
    self.ViewMgr:BindEvListener("EvEntityUpdatePublicMatchPlayerNum", self)
    self.ViewMgr:BindEvListener("EvEntitySignUpSucceed", self)
    self.ViewMgr:BindEvListener("EvRemoveMatch", self)

    local btn_return = self.ComUi:GetChild("BtnReturn").asButton
    btn_return.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )
    local btn_addchip = self.ComUi:GetChild("BtnAddChip").asButton
    btn_addchip.onClick:Add(
            function()
                self:onClickBtnAddChip()
            end
    )
    self.GComNoMatch = self.ComUi:GetChild("ComNoMatch").asCom
    self.GComNoSelfMatch = self.ComUi:GetChild("ComSelfNoMatch").asCom
    local btn_goapply = self.GComNoSelfMatch:GetChild("BtnApply").asButton
    btn_goapply.onClick:Add(
            function()
                self:onClickBtnGoApply()
            end
    )
    local com_title = self.ComUi:GetChild("ComTitle").asCom
    local btn_matchtime = com_title:GetChild("BtnStartTime").asButton
    btn_matchtime.onClick:Add(
            function()
                self:changeMatchListBeginTimeOrder()
            end
    )
    self.GControllerOrderTime = btn_matchtime:GetController("ControllerSate")
    local btn_signUpFee = com_title:GetChild("BtnSignUpFee").asButton
    btn_signUpFee.onClick:Add(
            function()
                self:changeMatchListSignUpFeeOrder()
            end
    )
    self.GTextTime = self.ComUi:GetChild("TextTime").asTextField
    self.GControllerOrderReward = btn_signUpFee:GetController("ControllerState")
    self.GTextSelfChip = btn_addchip:GetChild("TextSelfChip").asTextField
    self.GListMatchType = self.ComUi:GetChild("ListMatchType").asList
    self.GListMatch = self.ComUi:GetChild("ListMatch").asList
    self.GComSelfMatchNum = self.ComUi:GetChild("ComSelfMatchNum").asCom
    self.GTextSelfMatchNum = self.GComSelfMatchNum:GetChild("TextNum").asTextField
    self.ChipIconSolustion = self.ComUi:GetController("ChipIconSolustion")
    self.ChipIconSolustion.selectedIndex = self.Context.Cfg.ChipIconSolustion

    self.ControllerActor = self.ViewMgr.ControllerMgr:GetController("Actor")
    self.ControllerMTT = self.ViewMgr.ControllerMgr:GetController("MTT")
    self:setPlayerGoldAndDiamond()
    self.CurrentItemMatchType = nil
    self.ListMatchType = {}
    self.ListMatchItem = {}
    self:setMatchTypeList()
    self:setCurrentMatchType(self.ListMatchType[1])
    self.MatchListEarlyAhead = false
    self.MatchListBigRewardAhead = false
    self.SelfMatchNum = 0
    local bg = self.ComUi:GetChild("Bg")
    if (bg ~= nil) then
        ViewHelper:MakeUiBgFiteScreen(ViewMgr.STANDARD_WIDTH, ViewMgr.STANDARD_HEIGHT, self.ComUi.width, self.ComUi.height, bg.width, bg.height, bg, BgAttachMode.Center)
    end

    self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(200, self, self._timerUpdate)
end

---------------------------------------
function ViewMatchLobby:OnDestroy()
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewMatchLobby:OnHandleEv(ev)
    if (ev.EventName == "EvEntityGoldChanged") then
        self:setPlayerGoldAndDiamond()
    elseif (ev.EventName == "EvEntitySetPublicMatchLsit") then
        self:setMatchList()
        self.SelfMatchNum = ev.SelfMatchNum
        self:updateSelfSignUpNum()
    elseif (ev.EventName == "EvEntityUpdatePublicMatchPlayerNum") then
        local list_matchnum = ev.ListMatchNum
        for i = 1, #list_matchnum do
            local guid_num = list_matchnum[i]
            for i = 1, #self.ListMatchItem do
                local temp = self.ListMatchItem[i]
                if (temp.MatchInfo.Guid == guid_num.Guid) then
                    temp:UpdatePlayerNum(guid_num.PlayerNum)
                    break
                end
            end
        end
    elseif (ev.EventName == "EvEntitySignUpSucceed") then
        --[[local ev = self.ViewMgr:GetEv("EvUiRequestPublicMatchList")
        if(ev == nil)
        then
            ev = EvUiRequestPublicMatchList:new(nil)
        end
        self.ViewMgr:SendEv(ev)]]
        local match_guid = ev.MatchGuid
        for i = 1, #self.ListMatchItem do
            local temp = self.ListMatchItem[i]
            if (temp.MatchInfo.Guid == match_guid) then
                local view_applySucceed = self.ViewMgr:CreateView("ApplySucceed")
                view_applySucceed:SetMatchInfo(temp.MatchInfo)
                break
            end
        end
    elseif (ev.EventName == "EvRemoveMatch") then
        local match_guid = ev.MatchGuid
        local item
        local key
        for i = 1, #self.ListMatchItem do
            local temp = self.ListMatchItem[i]
            if (temp.MatchInfo.Guid == match_guid) then
                item = temp
                key = i
                break
            end
        end
        local com = item.Com
        self.GListMatch:RemoveChildToPool(com)
        table.remove(self.ListMatchItem, key)
    end
end

---------------------------------------
-- 定时更新
function ViewMatchLobby:_timerUpdate(tm)
    self.UpdatePlayerNumTime = self.UpdatePlayerNumTime + tm
    if (self.UpdatePlayerNumTime >= 30) then
        local ev = self.ViewMgr:GetEv("EvUiRequestUpdatePublicMatchPlayerNum")
        if (ev == nil) then
            ev = EvUiRequestUpdatePublicMatchPlayerNum:new(nil)
        end
        self.ViewMgr:SendEv(ev)
        local ev = self.ViewMgr:GetEv("EvUiRequestPublicMatchList")
        if (ev == nil) then
            ev = EvUiRequestPublicMatchList:new(nil)
        end
        self.ViewMgr:SendEv(ev)
        self.UpdatePlayerNumTime = 0
    end
    local dt_now = CS.System.DateTime.Now
    if (#self.ListMatchItem > 0) then
        for i = 1, #self.ListMatchItem do
            if (self.ListMatchItem[i] ~= nil) then
                self.ListMatchItem[i]:UpdateState(dt_now)
            end
        end
    end
    self.GTextTime.text = string.format("%02s", dt_now.Hour) .. ":" .. string.format("%02s", dt_now.Minute)
end

---------------------------------------
function ViewMatchLobby:onClickBtnReturn()
    self.ControllerMTT:Clear()
    self.ViewMgr:DestroyView(self)
    local ev = self.ViewMgr:GetEv("EvUiCreateMainUi")
    if (ev == nil) then
        ev = EvUiCreateMainUi:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewMatchLobby:setMatchTypeList()
    local com = self.GListMatchType:AddItemFromPool().asCom
    local item = ItemMatchType:new(nil, com, "All", self.ViewMgr.LanMgr:getLanValue("AllMatch"), self)
    table.insert(self.ListMatchType, item)
    local com1 = self.GListMatchType:AddItemFromPool().asCom
    local item1 = ItemMatchType:new(nil, com1, "MyApply", self.ViewMgr.LanMgr:getLanValue("MyMatch"), self)
    table.insert(self.ListMatchType, item1)
end

---------------------------------------
function ViewMatchLobby:setCurrentMatchType(item_mathtype)
    if (self.CurrentItemMatchType == item_mathtype) then
        return
    else
        if (self.CurrentItemMatchType ~= nil) then
            self.CurrentItemMatchType:BeSelectedOrNot(false)
        end
        self.CurrentItemMatchType = item_mathtype
        self.CurrentItemMatchType:BeSelectedOrNot(true)
    end
    local ev = self.ViewMgr:GetEv("EvUiRequestPublicMatchList")
    if (ev == nil) then
        ev = EvUiRequestPublicMatchList:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ViewMatchLobby:onClickBtnAddChip()
    self.ViewMgr:CreateView("Shop")
end

---------------------------------------
function ViewMatchLobby:setPlayerGoldAndDiamond()
    self.GTextSelfChip.text = UiChipShowHelper:GetGoldShowStr(self.ControllerActor.PropGoldAcc:get(), self.ViewMgr.LanMgr.LanBase)
end

---------------------------------------
function ViewMatchLobby:setMatchList()
    self.GComNoSelfMatch.visible = false
    self.GComNoMatch.visible = false
    self.ListMatchItem = {}
    self.GListMatch:RemoveChildrenToPool()
    if (self.CurrentItemMatchType.MatchType == "MyApply") then
        local list_selfMatch = self.ControllerMTT.ListSelfMatch
        if (#list_selfMatch == 0) then
            self.GComNoSelfMatch.visible = true
            return
        else
            for i = 1, #list_selfMatch do
                local com = self.GListMatch:AddItemFromPool().asCom
                local item_match = ItemMatchInfo:new(nil, com, list_selfMatch[i], true, self.ViewMgr)
                table.insert(self.ListMatchItem, item_match)
            end
        end
    elseif (self.CurrentItemMatchType.MatchType == "All")
    then
        local list_allMatch = self.ControllerMTT.ListAllMatch
        if (#list_allMatch == 0) then
            self.GComNoMatch.visible = true
            return
        else
            for i = 1, #list_allMatch do
                local match_info = list_allMatch[i]
                local is_selfMatch = false
                local list_selfMatch = self.ControllerMTT.ListSelfMatch
                if (#list_selfMatch > 0) then
                    for i = 1, #list_selfMatch do
                        if (list_selfMatch[i].Guid == match_info.Guid)
                        then
                            is_selfMatch = true
                        end
                    end
                end
                local item_match = nil
                if (is_selfMatch) then
                    if (CS.System.DateTime.Now < match_info.DtMatchBegin) then
                        local com = self.GListMatch:AddItemFromPool().asCom
                        item_match = ItemMatchInfo:new(nil, com, match_info, true, self.ViewMgr)
                    end
                else
                    if (CS.System.DateTime.Now < match_info.DtSignupClose) then
                        local com = self.GListMatch:AddItemFromPool().asCom
                        item_match = ItemMatchInfo:new(nil, com, match_info, false, self.ViewMgr)
                    end
                end
                if (item_match ~= nil) then
                    table.insert(self.ListMatchItem, item_match)
                end
            end
        end
        if (#self.ListMatchItem == 0) then
            self.GComNoMatch.visible = true
        end
    end
end

---------------------------------------
function ViewMatchLobby:onClickBtnGoApply()
    self:setCurrentMatchType(self.ListMatchType[1])
end

---------------------------------------
function ViewMatchLobby:changeMatchListBeginTimeOrder()
    if (self.MatchListEarlyAhead) then
        self.MatchListEarlyAhead = false
        self.GControllerOrderTime:SetSelectedIndex(0)
    else
        self.MatchListEarlyAhead = true
        self.GControllerOrderTime:SetSelectedIndex(1)
    end
    self:sortMatchByTime(self.MatchListEarlyAhead)
end

---------------------------------------
function ViewMatchLobby:changeMatchListSignUpFeeOrder()
    if (self.MatchListBigRewardAhead) then
        self.MatchListBigRewardAhead = false
        self.GControllerOrderReward:SetSelectedIndex(0)
    else
        self.MatchListBigRewardAhead = true
        self.GControllerOrderReward:SetSelectedIndex(1)
    end
    self:sortMatchBySignUpFee(self.MatchListBigRewardAhead)
end

---------------------------------------
function ViewMatchLobby:sortMatchByTime(early_ahead)
    local list_sort = nil
    if (self.CurrentItemMatchType.MatchType == "MyApply") then
        list_sort = self.ControllerMTT.ListSelfMatch
    elseif (self.CurrentItemMatchType.MatchType == "All") then
        list_sort = self.ControllerMTT.ListAllMatch
    end
    if (#list_sort == 0) then
        return
    end
    table.sort(list_sort,
            function(a, b)
                if (early_ahead) then
                    return a.DtMatchBegin < b.DtMatchBegin
                else
                    return a.DtMatchBegin > b.DtMatchBegin
                end
            end
    )
    self:setMatchList()
end

---------------------------------------
function ViewMatchLobby:sortMatchBySignUpFee(big_ahead)
    local list_sort = nil
    if (self.CurrentItemMatchType.MatchType == "MyApply") then
        list_sort = self.ControllerMTT.ListSelfMatch
    elseif (self.CurrentItemMatchType.MatchType == "All") then
        list_sort = self.ControllerMTT.ListAllMatch
    end
    if (#list_sort == 0) then
        return
    end
    table.sort(list_sort,
            function(a, b)
                if (big_ahead) then
                    return a.SignupFee > b.SignupFee
                else
                    return a.SignupFee < b.SignupFee
                end
            end
    )
    self:setMatchList()
end

---------------------------------------
function ViewMatchLobby:RemoveMatchItem(match_item)
    for i = 1, #self.ListMatchItem do
        if (self.ListMatchItem[i] == match_item) then
            local com = match_item.Com
            self.GListMatch:RemoveChildToPool(com)
            table.remove(self.ListMatchItem, i)
            break
        end
    end
    if (self.CurrentItemMatchType.MatchType == "MyApply") then
        self.SelfMatchNum = #self.ListMatchItem
        self:updateSelfSignUpNum()
    end
end

---------------------------------------
function ViewMatchLobby:updateSelfSignUpNum()
    if (self.SelfMatchNum > 0) then
        self.GComSelfMatchNum.visible = true
        self.GTextSelfMatchNum.text = tostring(self.SelfMatchNum)
    else
        self.GComSelfMatchNum.visible = false
    end
end

---------------------------------------
ViewMatchLobbyFactory = class(ViewFactory)

---------------------------------------
function ViewMatchLobbyFactory:CreateView()
    local view = ViewMatchLobby:new()
    return view
end