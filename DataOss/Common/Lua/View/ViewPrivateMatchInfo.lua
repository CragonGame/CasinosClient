-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
BtnSignupOption = {
    Signup = 1,
    CancelSignup = 2,
    Enter = 3
}

---------------------------------------
ViewPrivateMatchInfo = ViewBase:new()

---------------------------------------
function ViewPrivateMatchInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    return o
end

---------------------------------------
function ViewPrivateMatchInfo:OnCreate()
    local btn_return = self.ComUi:GetChild("BtnReturn").asButton
    btn_return.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local btn_overview = self.ComUi:GetChild("BtnOverView").asButton
    btn_overview.onClick:Add(
            function(a)
                self:onClickBtnTab(0)
            end
    )
    local btn_player = self.ComUi:GetChild("BtnPlayer").asButton
    btn_player.onClick:Add(
            function(a)
                self:onClickBtnTab(1)
            end
    )
    local btn_blind = self.ComUi:GetChild("BtnBlind").asButton
    btn_blind.onClick:Add(
            function(a)
                self:onClickBtnTab(2)
            end
    )
    self.GButtonDisMiss = self.ComUi:GetChild("BtnDisMiss").asButton
    self.GButtonDisMiss.onClick:Add(
            function()
                self:onClickBtnDisMiss()
            end
    )
    self.GControllerTab = self.ComUi:GetController("ControllerTab")
    -- 概述
    local com_overView = self.ComUi:GetChild("ComOverView").asCom
    self.GTextInvitationCode = self.ComUi:GetChild("TextInvitationCode").asTextField
    self.GTextMatchBeginTime = com_overView:GetChild("TextMatchTime").asTextField
    self.GTextOwnerName = com_overView:GetChild("TextOwner").asTextField
    self.GTextStartChip = com_overView:GetChild("TextStartChip").asTextField
    self.GTextMatchDiscribe = com_overView:GetChild("TextMatchDiscribe").asTextField
    self.GTextStartBlind = com_overView:GetChild("TextStartBlind").asTextField
    self.GTextRebuyTips = com_overView:GetChild("TextRebuyTips").asTextField
    self.GTextSignupNum = com_overView:GetChild("TextSignupNum").asTextField
    self.GTextRaiseBlindTime = com_overView:GetChild("TextRaiseBlindTime").asTextField
    self.GTextStopSignupTime = com_overView:GetChild("TextStopSignupTime").asTextField
    self.GTextAddonTips = com_overView:GetChild("TextAddonTips").asTextField
    self.GButtonShare = com_overView:GetChild("BtnShare").asButton
    self.GButtonShare.onClick:Add(
            function()
                self:onClickBtnShare()
            end
    )
    self.GButtonSignupOrCancel = com_overView:GetChild("BtnSignup").asButton
    self.GButtonSignupOrCancel.onClick:Add(
            function()
                self:onClickBtnSignUp()
            end
    )
    self.GTextSignupTitle = self.GButtonSignupOrCancel:GetChild("Title").asTextField
    -- player
    local com_player = self.ComUi:GetChild("ComPlayer").asCom
    self.GTextSignupNum1 = com_player:GetChild("TextSignupNum").asTextField
    self.GListPlayer = com_player:GetChild("ListPlayer").asList
    -- blind
    local com_blind = self.ComUi:GetChild("ComBlind").asCom
    self.GListBlind = com_blind:GetChild("ListBlind").asList

    self.BtnSignupOption = nil
    self.MatchGuid = nil
    self.IsSelfJoin = false
end

---------------------------------------
function ViewMatchInfo:OnHandleEv(ev)
    if (ev.EventName == "EvEntitySetMatchDetailedInfo") then
        self:SetMatchInfo(ev.MatchDetailedInfo)
    elseif (ev.EventName == "EvEntityResponseCancelSignupMatch") then
        if (ev.IsSucceed) then
            local msg_box = self.ViewMgr:CreateView("MsgBox")
            msg_box:showMsgBox1("", "取消报名成功",
                    function()
                        self.ViewMgr:DestroyView(msg_box)
                    end
            )
        end
        local match_guid = ev.MatchGuid
        if (match_guid == self.MatchGuid) then
            self.GTextBtnSignupTitle.text = "报名"
            self.BtnSignupOption = BtnSignupOption.Signup
        end
    elseif (ev.EventName == "EvEntitySignupSucceed") then
        local msg_box = self.ViewMgr:CreateView("MsgBox")
        msg_box:showMsgBox1("", "报名成功",
                function()
                    self.ViewMgr:DestroyView(msg_box)
                end
        )
        local match_guid = ev.MatchGuid
        if (match_guid == self.MatchGuid) then
            self.GTextBtnSignupTitle.text = "取消报名"
            self.BtnSignupOption = BtnSignupOption.CancelSignup
        end
    end
end

---------------------------------------
function ViewPrivateMatchInfo:Init(match_guid, is_selfJoin)
    local ev = self.ViewMgr:GetEv("EvUiRequestMatchDetailedInfo")
    if (ev == nil) then
        ev = EvUiRequestMatchDetailedInfo:new(nil)
    end
    ev.MatchGuid = match_guid
    self.ViewMgr:SendEv(ev)
    self.MatchGuid = match_guid
    self.IsSelfJoin = is_selfJoin
end

---------------------------------------
function ViewPrivateMatchInfo:SetMatchInfo(match_info)
    self.GTextInvitationCode.text = match_info.InvitationCode
    self.GTextMatchBeginTime.text = match_info.Info.DtMatchBegin.Month .. "月" .. match_info.Info.DtMatchBegin.Day .. "日"
            .. " " .. match_info.Info.DtMatchBegin.Hour .. match_info.Info.DtMatchBegin.Minute
    self.GTextOwnerName.text = match_info.Info.Name
    self.GTextStartChip.text = match_info.Info.InitScore
    if (match_info.Info.Discribe ~= nil and string.len(match_info.Info.Discribe) > 0) then
        self.GTextMatchDiscribe.text = match_info.Info.Discribe
    end
    -- 起始盲注
    local bindTableName = match_info.Info.RaiseBlindTbInfo.RaiseBlindTbName
    local startBlindId = match_info.Info.RaiseBlindTbInfo.BeginId
    local tbda_mgr = CS.Casinos.CasinosContext.Instance.TbDataMgrLua
    local tbda = tbda_mgr:GetMapData(bindTableName)
    self.GTextStartBlind.text = tbda[startBlindId].BlindsSmall .. "/" .. tbda[startBlindId].BlindsBig
    -- 重购提示
    local canRebuyMaxLevel = #match_info.Info.RaiseBlindTbInfo.ListRaiseBlindTbIdCanRebuy
    local temp = nil
    if (canRebuyMaxLevel == 1) then
        temp = "第1级"
    else
        temp = "第1" .. "-" .. canRebuyMaxLevel .. "级"
    end
    local temp1 = "可重购" .. match_info.Info.CanRebuyCount .. "次"
    self.GTextRebuyTips.text = temp .. temp1
    -- 已经报名
    local temp2 = match_info.Info.PlayerNum .. "/" .. match_info.PlayerNumMax
    self.GTextSignupNum = temp2
    self.GTextSignupNum1 = temp2
    -- 涨盲时间
    local raiseBlindTimeMinute = math.floor(match_info.Info.RaiseBlindTbInfo.RaiseBlindTmSpan / 60)
    self.GTextRaiseBlindTime.text = raiseBlindTimeMinute .. "分钟"
    -- 截止报名
    self.GTextStopSignupTime.text = string.format("%02s", match_info.Info.DtSignupClose.Hour) .. string.format("%02s", match_info.Info.DtSignupClose.Minute)
    -- 增购提示
    if (match_info.Info.CanAddonCount == 0) then
        self.GTextAddonTips = "无"
    else
        local temp = "第" .. canRebuyMaxLevel + 1 .. "级;"
        local addonTimes = 0
        if (match_info.Info.RaiseBlindTbInfo.AddonScore % match_info.Info.InitScore == 0) then
            addonTimes = match.floor(match_info.Info.RaiseBlindTbInfo.AddonScore / match_info.Info.InitScore)
        else
            addonTimes = string.format("%0.1f", match_info.Info.RaiseBlindTbInfo.AddonScore / match_info.Info.InitScore)
        end
        self.GTextAddonTips = temp .. "可获得" .. addonTimes .. "倍筹码"
    end
    --玩家列表
    self.GTextSignupNum1.text = match_info.Info.PlayerNum
    --盲注列表
    local temp_level = 0
    for i = match_info.Info.RaiseBlindTbInfo.BeginId, match_info.Info.RaiseBlindTbInfo.EndId do
        local blindInfo = tbda[i]
        temp_level = temp_level + 1
        local can_rebuy = false
        local can_addon = false
        if (temp_level <= canRebuyMaxLevel) then
            can_rebuy = true
        end
        if (match_info.Info.CanAddonCount > 0 and temp_level == canRebuyMaxLevel + 1) then
            can_addon = true
        end
        local com = self.GListBlind:AddItemFromPool()
        ItemMatchInfoBlind:new(nil, com, blindInfo, temp_level, can_rebuy, can_addon, raiseBlindTimeMinute, self.ViewMgr)
    end

    -- 设置比赛状态
    local nowTm = CS.System.DateTime.Now
    if (self.IsSelfJoin) then
        if (nowTm < match_info.Info.DtMatchBegin) then
            self.GTextBtnSignupTitle.text = "取消报名"
            self.BtnSignupOption = BtnSignupOption.CancelSignup
        else
            self.GTextBtnSignupTitle.text = "进入"
            self.BtnSignupOption = BtnSignupOption.Enter
        end
    else
        if (nowTm < match_info.Info.DtMatchBegin) then
            self.GTextBtnSignupTitle.text = "报名"
            self.BtnSignupOption = BtnSignupOption.Signup
        elseif (nowTm >= match_info.Info.DtMatchBegin and nowTm < match_info.Info.DtSignupClose) then
            self.GTextBtnSignupTitle.text = "延迟报名"
            self.BtnSignupOption = BtnSignupOption.Signup
        else
            self.GTextBtnSignupTitle.text = "报名截止"
            self.GButtonSignupOrCancel.enable = false
        end
    end
    if (self.ControllerMtt.Guid == match_info.Info.CreatePlayerGuid) then
        self.GButtonDisMiss.visible = true
    else
        self.GButtonDisMiss.visible = false
    end
end

---------------------------------------
function ViewPrivateMatchInfo:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewPrivateMatchInfo:onClickBtnTab(index)
    self.GControllerTab:SetSelectedIndex(index)
end

---------------------------------------
function ViewPrivateMatchInfo:onClickBtnDisMiss()
    local ev = self.ViewMgr:GetEv("EvUiRequesetDisMissMatch")
    if (ev == nil) then
        ev = EvUiRequesetDisMissMatch:new(nil)
    end
    ev.MatchGuid = self.MatchGuid
end

---------------------------------------
function ViewPrivateMatchInfo:onClickBtnShare()
end

---------------------------------------
function ViewPrivateMatchInfo:onClickBtnSignUp()
    if (self.BtnSignupOption == BtnSignupOption.Signup) then
        local ev = self.ViewMgr:GetEv("EvUiRequestSignupMatch")
        if (ev == nil) then
            ev = EvUiRequestSignupMatch:new(nil)
        end
        ev.MatchGuid = self.MatchGuid
        self.ViewMgr:SendEv(ev)
    elseif (self.BtnSignupOption == BtnSignupOption.CancelSignup) then
        local msg_box = self.ViewMgr:CreateView("MsgBox")
        msg_box:useTwoBtn("", "确定取消当前赛事报名？",
                function()
                    local ev = self.ViewMgr:GetEv("EvUiRequestCancelSignupMatch")
                    if (ev == nil) then
                        ev = EvUiRequestCancelSignupMatch:new(nil)
                    end
                    ev.MatchGuid = self.MatchGuid
                    self.ViewMgr:SendEv(ev)
                end,
                function()
                    self.ViewMgr:DestroyView(msg_box)
                end
        )
    elseif (self.BtnSignupOption == BtnSignupOption.Enter) then
        local ev = self.ViewMgr:GetEv("EvUiRequestEnterMatch")
        if (ev == nil) then
            ev = EvUiRequestEnterMatch:new(nil)
        end
        ev.MatchGuid = self.MatchGuid
        self.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
ViewPrivateMatchInfoFactory = ViewFactory:new()

---------------------------------------
function ViewPrivateMatchInfoFactory:new(o, ui_package_name, ui_component_name, ui_layer, is_single, fit_screen)
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
function ViewPrivateMatchInfoFactory:CreateView()
    local view = ViewPrivateMatchInfo:new(nil)
    return view
end