ViewMatchInfo = ViewBase:new()

function ViewMatchInfo:new(o)
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

function ViewMatchInfo:onCreate()
    local controller_mgr = ControllerMgr:new(nil)
    self.ControllerActor = controller_mgr:GetController("Actor")
    ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("MatchInfo"))
    self.ViewMgr:bindEvListener("EvEntitySetMatchDetailedInfo", self)
    self.ViewMgr:bindEvListener("EvEntitySetRaiseBlindTbInfo", self)
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:close()
            end
    )
    local btn_close = com_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:close()
            end
    )
    local com_tab = self.ComUi:GetChild("ComTab").asCom
    local btn_overview = com_tab:GetChild("BtnOverView").asButton
    btn_overview.onClick:Add(
            function(a)
                self:selectTab(0)
            end
    )
    local btn_matchstate = com_tab:GetChild("BtnMatchState").asButton
    btn_matchstate.onClick:Add(
            function(a)
                self:selectTab(1)
            end
    )
    local btn_rank = com_tab:GetChild("BtnRank").asButton
    btn_rank.onClick:Add(
            function(a)
                self:selectTab(2)
            end
    )
    local btn_blind = com_tab:GetChild("BtnBlind").asButton
    btn_blind.onClick:Add(
            function(a)
                self:selectTab(3)
            end
    )
    local btn_reward = com_tab:GetChild("BtnReward").asButton
    btn_reward.onClick:Add(
            function(a)
                self:selectTab(4)
            end
    )
    self.GControllerTab = com_tab:GetController("ControllerTab")
    self.GControllerMatchInfo = self.ComUi:GetController("ControllerMatchInfo")

    local com_overview = self.ComUi:GetChild("ComOverView").asCom
    self.GBtnShare = com_overview:GetChild("BtnShare").asButton
    self.GBtnShare.onClick:Add(
            function()
                self:onClickShare()
            end)
    self.GBtnShare.enabled = false
    self.GBtnApply = com_overview:GetChild("BtnApply").asButton
    self.GBtnApply.onClick:Add(
            function()
                self:onClickBtnApply()
            end
    )
    self.GBtnApply.enabled = false
    self.GTextBtnApplyTitle = self.GBtnApply:GetChild("Lan_Text_SignUp").asTextField
    self.GControllerOverView = com_overview:GetController("ControllerOverView")
    local com_overview_text = com_overview:GetChild("ComOverViewText").asCom
    self.GMatchTitle = com_overview_text:GetChild("MatchTitle").asTextField
    self.GTextApplyFee = com_overview_text:GetChild("TextApplyFee").asTextField
    self.GTextMatchTime = com_overview_text:GetChild("TextMatchTime").asTextField
    self.GTextStopApplyTime = com_overview_text:GetChild("TextStopApplyTime").asTextField
    self.GTextEntryNumber = com_overview_text:GetChild("TextEntryNumber").asTextField
    self.GTextInitialChip = com_overview_text:GetChild("TextInitialChip").asTextField
    self.GTextReBuyTips = com_overview_text:GetChild("TextRepurchaseTips").asTextField
    self.GTextAddonTips = com_overview_text:GetChild("TextPurchaseTips").asTextField
    self.GComReBuy = com_overview_text:GetChild("ComReBuy").asCom
    self.GGroupAddon = com_overview_text:GetChild("GroupAddon").asGroup
    self.TextUniqId = com_overview:GetChild("TextUniqId").asTextField

    local com_matchtate = self.ComUi:GetChild("ComMatchState").asCom
    self.GControllerMatchState = com_matchtate:GetController("ControllerMatchState")
    self.GTextMatchStateRank = com_matchtate:GetChild("TextMatchStateRank").asTextField
    self.GTextMatchStateApplyNumber = com_matchtate:GetChild("TextMatchStateApplyNumber").asTextField
    self.GTextMatchStateRewardNumber = com_matchtate:GetChild("TextMatchStateRewardNumber").asTextField
    self.GTextMatchStateCurrentLevel = com_matchtate:GetChild("TextMatchStateCurrentLevel").asTextField
    self.GTextMatchUsedTime = com_matchtate:GetChild("TextMatchUsedTime").asTextField
    self.GTextMatchStateCurrentAnte = com_matchtate:GetChild("TextMatchStateCurrentAnte").asTextField
    self.GTextMatchStateCurrentBlind = com_matchtate:GetChild("TextMatchStateCurrentBlind").asTextField
    self.GTextMatchStateNextAnte = com_matchtate:GetChild("TextMatchStateNextAnte").asTextField
    self.GTextMatchStateNextBlind = com_matchtate:GetChild("TextMatchStateNextBlind").asTextField
    self.GTextMatchStateAverageChip = com_matchtate:GetChild("TextMatchStateAverageChip").asTextField
    self.GTextCurrentLevelLeftTime = com_matchtate:GetChild("TextCurrentLevelLeftTime").asTextField

    local com_rank = self.ComUi:GetChild("ComRank").asCom
    self.GControllerRank = com_rank:GetController("ControllerRank")
    self.GListRank = com_rank:GetChild("ListRank").asList

    local com_blind = self.ComUi:GetChild("ComBlind").asCom
    self.GTextBindTips = com_blind:GetChild("TextBindTips").asTextField
    self.GListBlind = com_blind:GetChild("ListBlind").asList

    local com_reward = self.ComUi:GetChild("ComReward").asCom
    self.GComRewardExplain = com_reward:GetChild("ComRewardExplain").asCom
    self.GComRewardExplain.onClick:Add(
            function()
                self:onClickComRewardExplain()
            end
    )
    self.GTextRewardTips = com_reward:GetChild("TextRewardTips").asTextField
    self.GListReward = com_reward:GetChild("ListReward").asList

    self.MatchGuid = nil
    self.DtMatchBegin = nil
    self.CurrentLevelLeftSeconds = 0
    self.IsInDesk = false
    self.IsSelfJoin = false
    --self.GBtnShare.enabled = false
    self:selectTab(0)
end

function ViewMatchInfo:Init(match_guid, isIndesk, isSelfJoin)
    local ev = self.ViewMgr:getEv("EvUiRequestMatchDetailedInfo")
    if (ev == nil)
    then
        ev = EvUiRequestMatchDetailedInfo:new(nil)
    end
    ev.MatchGuid = match_guid
    ev.MatchType = MatchTexasScopeType.Public
    self.ViewMgr:sendEv(ev)
    if (isIndesk)
    then
        self.GControllerRank:SetSelectedIndex(0)
        self.GControllerOverView:SetSelectedIndex(0)
        self.GControllerMatchState:SetSelectedIndex(0)
    else
        self.GControllerRank:SetSelectedIndex(1)
        self.GControllerOverView:SetSelectedIndex(1)
        self.GControllerMatchState:SetSelectedIndex(1)
    end
    self.IsInDesk = isIndesk
    self.IsSelfJoin = isSelfJoin
    self.MatchGuid = match_guid
end

function ViewMatchInfo:onHandleEv(ev)
    if (ev.EventName == "EvEntitySetMatchDetailedInfo")
    then
        self:SetMatchInfo(ev.MatchDetailedInfo)
    elseif (ev.EventName == "EvEntitySetRaiseBlindTbInfo")
    then
        local raise_blind_info = ev.raise_blind_info
        local current_raiseblind_tbid = ev.current_raiseblind_tbid
        local allBlindLevel = raise_blind_info.EndId - raise_blind_info.BeginId
        local currentLevel = current_raiseblind_tbid - raise_blind_info.BeginId + 1
        self.GTextMatchStateCurrentLevel.text = tostring(currentLevel) .. "/" ..tostring(allBlindLevel)
        local table_type = raise_blind_info.BlindType
        local current_bindInfo = TbDataHelper:GetTexasRaiseBlindByTypeAndId(table_type, current_raiseblind_tbid)
        self.GTextMatchStateCurrentAnte.text = tostring(current_bindInfo.Ante)
        self.GTextMatchStateCurrentBlind.text = tostring(current_bindInfo.BlindsSmall) .. "/" .. tostring(current_bindInfo.BlindsBig)
        local next_blindInfo = TbDataHelper:GetTexasRaiseBlindByTypeAndId(table_type, current_raiseblind_tbid + 1)
        self.GTextMatchStateNextAnte.text = tostring(next_blindInfo.Ante)
        self.GTextMatchStateNextBlind.text = tostring(next_blindInfo.BlindsSmall) .. "/" .. tostring(next_blindInfo.BlindsBig)
    end
end

function ViewMatchInfo:onUpdate(tm)
    if (self.GControllerMatchInfo.selectedIndex == 1 and self.GControllerMatchState.selectedIndex == 0)
    then
        local matchUseTime = CS.System.DateTime.Now - self.MatchInfo.DtMatchBegin
        self.GTextMatchUsedTime.text = string.format("%02s", matchUseTime.Minutes) .. ":" .. string.format("%02s", matchUseTime.Seconds)
        if (self.CurrentLevelLeftSeconds > 0)
        then
            self.CurrentLevelLeftSeconds = self.CurrentLevelLeftSeconds - tm
            local intSeconds = math.ceil(self.CurrentLevelLeftSeconds)
            local hours = nil
            local minutes = nil
            local seconds = nil
            if (intSeconds >= 3600)
            then
                hours, t = math.modf(intSeconds / 3600)
                local left_seconds = intSeconds - hours * 3600
                if (left_seconds >= 60)
                then
                    minutes, t = math.modf(left_seconds / 60)
                    seconds = left_seconds - minutes * 60
                else
                    minutes = 0
                    seconds = left_seconds
                end
            else
                hours = 0
                if (intSeconds >= 60)
                then
                    minutes, t = math.modf(intSeconds / 60)
                    seconds = intSeconds - minutes * 60
                else
                    minutes = 0
                    seconds = intSeconds
                end
            end
            self.GTextCurrentLevelLeftTime.text = string.format("%02s", hours) .. ":" .. string.format("%02s", minutes) .. ":" .. string.format("%02s", seconds)
        else
            self.GTextCurrentLevelLeftTime.text = ""
        end
    end
end

function ViewMatchInfo:onDestroy()
    self.ViewMgr:unbindEvListener(self)
end

function ViewMatchInfo:SetMatchInfo(detailedMatchInfo)
    local match_info = detailedMatchInfo.Info
    self.TextUniqId.text = string.format("Id:%s",match_info.UniqId)
    self.MatchInfo = match_info
    self.GBtnShare.enabled = true
    self.GBtnApply.enabled = true
    local raiseBlindInfo = match_info.RaiseBlindTbInfo
    local table_type = raiseBlindInfo.BlindType
    --概述
    if (self.IsInDesk == false)
    then
        self:getBtnApplyState()
    end
    self.GMatchTitle.text = match_info.Name
    local text_signUpfee = nil
    if (match_info.SignupFee == 0)
    then
        text_signUpfee = self.ViewMgr.LanMgr:getLanValue("FreeMatch")
    else
        text_signUpfee = UiChipShowHelper:getGoldShowStr3(match_info.SignupFee) .. "+" .. UiChipShowHelper:getGoldShowStr3(match_info.ServiceFee) .. self.ViewMgr.LanMgr:getLanValue("MatchFee")
    end
    self.GTextApplyFee.text = text_signUpfee
    self.GTextMatchTime.text = self:formatTime(match_info.DtMatchBegin)
    self.GTextStopApplyTime.text = self:formatTime(match_info.DtSignupClose)
    self.GTextEntryNumber.text = string.format("%s%s - %s%s",detailedMatchInfo.PlayerNumMin,self.ViewMgr.LanMgr:getLanValue("People"),detailedMatchInfo.PlayerNumMax,self.ViewMgr.LanMgr:getLanValue("People"))
    self.GTextInitialChip.text = UiChipShowHelper:getGoldShowStr3(match_info.InitScore)
    if (match_info.CanRebuyCount > 0)
    then
        local blind_list = raiseBlindInfo.ListRaiseBlindTbIdCanRebuy
        local format_info = ""
        if (#blind_list == 1)
        then
            local level = blind_list[1] - raiseBlindInfo.BeginId + 1
            format_info = tostring(level)
        elseif (#blind_list > 1)
        then
            local start_level = blind_list[1] - raiseBlindInfo.BeginId + 1
            local end_level = blind_list[#blind_list] - raiseBlindInfo.BeginId + 1
            format_info = tostring(start_level) .. "-" .. tostring(end_level)
        end
        self.GComReBuy.visible = true
        self.GTextReBuyTips.text = string.format(self.ViewMgr.LanMgr:getLanValue("RebuyTip"),tostring(match_info.CanRebuyCount),format_info,
                UiChipShowHelper:getGoldShowStr3(raiseBlindInfo.RebuyGold),UiChipShowHelper:getGoldShowStr3(raiseBlindInfo.RebuyScore))
    else
        self.GComReBuy.visible = false
    end
    if (match_info.CanAddonCount > 0)
    then
        local addon_list = raiseBlindInfo.ListRaiseBlindTbIdCanAddon
        local format_info = ""
        if (#addon_list == 1)
        then
            local level = addon_list[1] - raiseBlindInfo.BeginId + 1
            format_info = tostring(level)
        elseif (#addon_list > 1)
        then
            local start_level = addon_list[1] - raiseBlindInfo.BeginId + 1
            local end_level = addon_list[#addon_list] - raiseBlindInfo.BeginId + 1
            format_info = tostring(start_level) .. "-" .. tostring(end_level)
        end
        self.GGroupAddon.visible = true
        self.GTextAddonTips.text =  string.format(self.ViewMgr.LanMgr:getLanValue("AddOnTip"),tostring(match_info.CanAddonCount),format_info,
                UiChipShowHelper:getGoldShowStr3(raiseBlindInfo.AddonGold),UiChipShowHelper:getGoldShowStr3(raiseBlindInfo.AddonScore))
    else
        self.GGroupAddon.visible = false
    end
    if (self.IsInDesk)
    then
        local currentInfo = detailedMatchInfo.RealtimeMoreInfo
        -- 赛况
        self.GTextMatchStateRank.text = tostring(currentInfo.Info.MyRanking) .. "/" .. tostring(currentInfo.Info.PlayerLeftNum)
        self.GTextMatchStateApplyNumber.text = tostring(currentInfo.Info.PlayerLeftNum)
        self.GTextMatchStateRewardNumber.text = tostring(currentInfo.Info.PlayerNumReward)
        self.GTextMatchStateAverageChip.text = tostring(currentInfo.Info.AverageScore)
        self.DtMatchBegin = match_info.DtMatchBegin:ToLocalTime()
        self.CurrentLevelLeftSeconds = currentInfo.Info.RaiseBlindLeftSecond
        -- 排名
        local list_rank = currentInfo.ListPlayerRanking
        if (list_rank ~= nil and #list_rank > 0)
        then
            for i = 1, #list_rank do
                local item = list_rank[i]
                local com = self.GListRank:AddItemFromPool()
                ItemMatchInfoRank:new(nil,com,item,i,self.ViewMgr.LanMgr.LanBase)
            end
        end
    end
    -- 盲注
    local tabletips = nil
    if (match_info.SeatNum == 6)
    then
        tabletips = self.ViewMgr.LanMgr:getLanValue("SixSeat")
    elseif (match_info.SeatNum == 9)
    then
        tabletips = self.ViewMgr.LanMgr:getLanValue("NineSeat")
    end
    self.GTextBindTips.text = match_info.Name .. tabletips

    local blind_level = 1
    for i = raiseBlindInfo.BeginId, raiseBlindInfo.EndId do
        local canRebuy = false
        local canAddon = false
        for a = 1, #raiseBlindInfo.ListRaiseBlindTbIdCanRebuy do
            if (i == raiseBlindInfo.ListRaiseBlindTbIdCanRebuy[a])
            then
                canRebuy = true
                break
            end
        end
        for a = 1, #raiseBlindInfo.ListRaiseBlindTbIdCanAddon do
            if (i == raiseBlindInfo.ListRaiseBlindTbIdCanAddon[a])
            then
                canAddon = true
                break
            end
        end
        local data = TbDataHelper:GetTexasRaiseBlindByTypeAndId(table_type, i)
        local com = self.GListBlind:AddItemFromPool()
        ItemMatchInfoBlind:new(nil, com, data, blind_level, canRebuy, canAddon, raiseBlindInfo.RaiseBlindTmSpan,self.ViewMgr)
        blind_level = blind_level + 1
    end
    --奖励
    local reward_info = detailedMatchInfo.Reward
    local isSnowBallReward = match_info.IsSnowballReward
    local tips = ""
    local l_reward = reward_info.ListReward
    if(isSnowBallReward)
    then
        self.GComRewardExplain.visible = true
        tips = string.format(self.ViewMgr.LanMgr:getLanValue("SnowBallMatchReward"),reward_info.SnowballTotalReward)
        local table_playerNum = self.ViewMgr.TbDataMgr:GetMapData("TexasSnowBallRewardPlayerNum")
        local current_rewardid
        for i, v in pairs(table_playerNum) do
            if match_info.PlayerNum >= v.MinPlayerNum and match_info.PlayerNum <= v.MaxPlayerNum then
                current_rewardid = i
                break
            end
        end
        local list_info = TbDataHelper:GetTexasSnowBallRewradInfoByTableId(current_rewardid)
        for i, v in pairs(list_info) do
            local r = l_reward[i]
            local reward_gold = v.RewardRatio * reward_info.SnowballTotalRewardCurrent / 100
            if r ~= nil then
                local gold = r.Gold
                gold = gold + reward_gold
                r.Gold = gold
            else
                local r_i = BMatchTexasRewardItem:new(nil)
                r_i.RankingBegin = v.StartRank
                local e_r = v.EndRank
                if e_r ==0 then
                    e_r = v.StartRank
                end
                r_i.RankingEnd = e_r
                r_i.Gold = reward_gold
                table.insert(l_reward,r_i)
            end
        end
    else
        self.GComRewardExplain.visible = false
        tips = match_info.Name .. tabletips
    end
    self.GTextRewardTips.text = tips
    if(#l_reward > 0)
    then
        for  i = 1,#l_reward do
            local reward = l_reward[i]
            local com = self.GListReward:AddItemFromPool()
            ItemMatchInfoReward:new(nil,com,reward)
            if(i == #l_reward and isSnowBallReward)
            then
                local aTextField = CS.FairyGUI.GTextField()
                com:AddChild(aTextField)
                aTextField.pivotX  = 0.5
                aTextField.pivotY  = 0.5
                aTextField:SetSize(710,40)
                aTextField.x = 4
                aTextField.y = 82
                aTextField.align = CS.FairyGUI.AlignType.Center
                aTextField.verticalAlign = CS.FairyGUI.VertAlignType.Middle
                aTextField.UBBEnabled = true
                aTextField.text = self.ViewMgr.LanMgr:getLanValue("SnowBallMatchTip")
            end
        end
    end
end

function ViewMatchInfo:updateMatchState(left_seconds, begin_time)
    if (left_seconds >= 3600)
    then
        hours, t = math.modf(left_seconds / 3600)
        local left_seconds = left_seconds - hours * 3600
        if (left_seconds >= 60)
        then
            minutes, t = math.modf(left_seconds / 60)
            seconds = left_seconds - minutes * 60
        else
            minutes = 0
            seconds = left_seconds
        end
    else
        hours = 0
        if (left_seconds >= 60)
        then
            minutes, t = math.modf(left_seconds / 60)
            seconds = left_seconds - minutes * 60
        else
            minutes = 0
            seconds = left_seconds
        end
    end
end

function ViewMatchInfo:selectTab(index)
    self.GControllerMatchInfo:SetSelectedIndex(index)
    self.GControllerTab:SetSelectedIndex(index)
end

function ViewMatchInfo:onClickShare()
    local content = string.format(self.ViewMgr.LanMgr:getLanValue("MatchInviteTip"),self.ControllerActor.PropNickName:get(),
            self:formatTime(self.MatchInfo.DtMatchBegin),self.MatchInfo.Name)
    local pic_name = "Resources.KingTexasRaw/Icon/ShareIcon.png"
    local pic_path = CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(pic_name)
    Native.Instance:ShareContent(CS.cn.sharesdk.unity3d.PlatformType.WeChat, content, pic_path, self.MatchInfo.Name..self.ViewMgr.LanMgr:getLanValue("MatchInvite"),
            Native.Instance.ShareUrl, CS.cn.sharesdk.unity3d.ContentType.Webpage)
end

function ViewMatchInfo:onClickBtnApply()
    local btnApplyState = self:getBtnApplyState()
    if (btnApplyState == 2) -- 进入
    then
        local ev = self.ViewMgr:getEv("EvUiRequestEnterMatch")
        if (ev == nil)
        then
            ev = EvUiRequestEnterMatch:new(nil)
        end
        ev.MatchGuid = self.MatchInfo.Guid
        self.ViewMgr:sendEv(ev)
    elseif (btnApplyState == 3) -- 退赛
    then
        local msg_box = self.ViewMgr:createView("MsgBox")
        local content = self.ViewMgr.LanMgr:getLanValue("QuitMatchTip")
        msg_box:useTwoBtn("", content,
                function()
                    local ev = self.ViewMgr:getEv("EvUiRequestCancelSignupMatch")
                    if (ev == nil)
                    then
                        ev = EvUiRequestCancelSignupMatch:new(nil)
                    end
                    ev.MatchGuid = self.MatchGuid
                    self.ViewMgr:sendEv(ev)
                    self.ViewMgr:destroyView(msg_box)
                    self.ViewMgr:destroyView(self)
                end,
                function()
                    self.ViewMgr:destroyView(msg_box)
                    self.ViewMgr:destroyView(self)
                end
        )
    elseif (btnApplyState == 4) -- 报名
    then
        local msg_box = self.ViewMgr:createView("MsgBox")
        msg_box:useTwoBtn("", string.format(self.ViewMgr.LanMgr:getLanValue("SignUpTip"),UiChipShowHelper:getGoldShowStr3(self.MatchInfo.SignupFee),UiChipShowHelper:getGoldShowStr3(self.MatchInfo.ServiceFee)),
                function()
                    local ev = self.ViewMgr:getEv("EvUiRequestSignUpMatch")
                    if (ev == nil)
                    then
                        ev = EvUiRequestSignUpMatch:new(nil)
                    end
                    ev.MatchGuid = self.MatchGuid
                    self.ViewMgr:sendEv(ev)
                    self.ViewMgr:destroyView(msg_box)
                end,
                function()
                    self.ViewMgr:destroyView(msg_box)
                end
        )
        self.ViewMgr:destroyView(self)
    end
end

function ViewMatchInfo:onClickComRewardExplain()
    self.ViewMgr:createView("SnowBallReward")
end

function ViewMatchInfo:close()
    self.ViewMgr:destroyView(self)
end

function ViewMatchInfo:getBtnApplyState()
    if (self.IsSelfJoin)
    then
        local leftMatchBeginTime = self.MatchInfo.DtMatchBegin - CS.System.DateTime.Now
        if (leftMatchBeginTime.Minutes <= 4 and leftMatchBeginTime.Minutes > 0)
        then
            --self.GBtnApply.enabled = false
            self.GTextBtnApplyTitle.text = self.ViewMgr.LanMgr:getLanValue("QuitMatch")
            return 3 -- 已经报名但不可退赛
        elseif (leftMatchBeginTime.Minutes <= 0)
        then
            self.GTextBtnApplyTitle.text = self.ViewMgr.LanMgr:getLanValue("Enter")
            return 2 -- 已经报名 可以进入
        else
            self.GTextBtnApplyTitle.text = self.ViewMgr.LanMgr:getLanValue("QuitMatch")
            return 3 -- 已经报名 可以退赛
        end
    else
        self.GTextBtnApplyTitle.text = self.ViewMgr.LanMgr:getLanValue("SignUp")
        return 4 -- 尚未报名
    end
end

function ViewMatchInfo:formatTime(dtTime)
    local nowtm = CS.System.DateTime.Now
    local day = dtTime.Day - nowtm.Day
    local text_day = nil
    if (day == 0)
    then
        text_day = self.ViewMgr.LanMgr:getLanValue("Today")
    elseif (day == 1)
    then
        text_day = self.ViewMgr.LanMgr:getLanValue("Tomorrow")
    elseif (day == 2)
    then
        text_day = self.ViewMgr.LanMgr:getLanValue("AfterTomorrow")
    else
        text_day = string.format("%02s%s%s",dtTime.Month,".",tostring(dtTime.Day))
    end
    local temp = {}
    temp[1] = string.format("%02s", dtTime.Hour)
    temp[2] = ":"
    temp[3] = string.format("%02s", dtTime.Minute)
    local text_time = table.concat(temp)
    return text_day .. text_time
end

ViewMatchInfoFactory = ViewFactory:new()

function ViewMatchInfoFactory:new(o, ui_package_name, ui_component_name,
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

function ViewMatchInfoFactory:createView()
    local view = ViewMatchInfo:new(nil)
    return view
end