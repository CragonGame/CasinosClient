ViewTexasMTT = ViewDesktopTypeBase:new()

function ViewTexasMTT:new(o, view_desktop)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewDesktop = view_desktop
    o.DesktopType = "MTT"
    local com_desktop = view_desktop.ComUi
    o.BtnRebuy = com_desktop:GetChild("Lan_Btn_RebuyChip").asButton
    o.BtnRebuy.onClick:Add(
            function()
                o:_onClickBtnRebuyOrAddon()
            end)
    o.BtnAddon = com_desktop:GetChild("Lan_Btn_AddonChip").asButton
    o.BtnAddon.onClick:Add(
            function()
                o:_onClickBtnRebuyOrAddon()
            end)
    ViewHelper:setGObjectVisible(false, o.BtnRebuy)
    ViewHelper:setGObjectVisible(false, o.BtnAddon)
    o.GBtnMenu = o.ViewDesktop.ComUi:GetChild("BtnMenu").asButton
    o.GBtnMenu.onClick:Add(
            function()
                o:_onClickMenu()
            end
    )
    local loader_bg = com_desktop:GetChild("LoaderBg").asLoader
    --local bg_path = CS.Casinos.CasinosContext.Instance:AppendStrWithSB(CS.Casinos.CasinosContext.Instance.ABResourcePathTitle,
    --        "DesktopImage/mttbg.ab")
    loader_bg.icon = CS.FairyGUI.UIPackage.GetItemURL("Desktop","MTTBg")--CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(bg_path)
    ViewHelper:makeUiBgFiteScreen(ViewMgr.STANDARD_WIDTH,ViewMgr.STANDARD_HEIGHT, o.ViewDesktop.ComUi.width, o.ViewDesktop.ComUi.height, loader_bg.width, loader_bg.height,loader_bg,BgAttachMode.Center)
    local loader_desk = com_desktop:GetChild("LoaderDesk").asLoader
    --local desk_path = CS.Casinos.CasinosContext.Instance:AppendStrWithSB(CS.Casinos.CasinosContext.Instance.ABResourcePathTitle,
    --        "DesktopImage/mttdesk.ab")
    loader_desk.icon = CS.FairyGUI.UIPackage.GetItemURL("Desktop","MTTDesk")--CS.Casinos.CasinosContext.Instance.PathMgr:combinePersistentDataPath(desk_path)

    o.ComRealTimeInfo = com_desktop:GetChild("ComRealTimeInfo").asCom
    o.ComRealTimeInfo.onClick:Add(
            function()
                o:_onClickRealTimeInfo()
            end)
    o.TextRankingAScore = o.ComRealTimeInfo:GetChild("TextRankingAScore").asTextField

    return o
end

function ViewTexasMTT:onHandleEv(ev)
    if ev ~= nil then
        if ev.EventName == "EvEntityMTTUpdateRealtimeInfo" then
            self:setRealTimeInfo(ev.RealtimeInfo, ev.blind_type)
        elseif ev.EventName == "EvEntityMTTUpdateRaiseBlindTm" then
            self:setRaiseBlindTm(ev.RaiseBlindTm)
        elseif ev.EventName == "EvEntityMTTPlayerRebuyOrAddonRefresh" then
            local can_rebuy = ev.can_rebuy
            local can_addon = ev.can_addon
            ViewHelper:setGObjectVisible(can_rebuy, self.BtnRebuy)
            ViewHelper:setGObjectVisible(can_addon, self.BtnAddon)
        elseif ev.EventName == "EvEntitySelfIsOB" then
            ViewHelper:setGObjectVisible(false, self.ComRealTimeInfo)
            ViewHelper:setGObjectVisible(false, self.BtnRebuy)
            ViewHelper:setGObjectVisible(false, self.BtnAddon)
        elseif ev.EventName == "EvMTTPauseChanged" then
            local p_i = ev.pause_info
            self.MatchTexas.Pause = p_i.Pause
            self.MatchTexas.PauseCountDownLeftSec = p_i.PauseCountdownLeftSec
            if self.MatchTexas.Pause then
                self.TextBlind.text = self.ViewDesktop.ViewMgr.LanMgr:getLanValue("WaitMatchBegine")
            else
                self:setRaiseBlindTm(self.MatchTexas.RealtimeInfo.RaiseBlindLeftSecond)
            end
        elseif ev.EventName == "EvEntityMatchGameOver" then
            if self.MatchGuid == ev.game_over.MatchGuid then
                ViewHelper:setGObjectVisible(false, self.ComRealTimeInfo)
                ViewHelper:setGObjectVisible(false, self.BtnRebuy)
                ViewHelper:setGObjectVisible(false, self.BtnAddon)
            end
        end
    end
end

function ViewTexasMTT:setSnapShot(snapshot, is_init)
    local match_texas = snapshot.match_texas
    self.MatchTexas = match_texas
    self.Ante = 0
    self.MatchGuid = match_texas.MatchGuid

    local group_reward_blind = self.ComRealTimeInfo:GetChild("GroupRewardBlind").asGroup
    self.TextReward = self.ComRealTimeInfo:GetChildInGroup(group_reward_blind, "TextReward").asTextField
    local group_blind = self.ComRealTimeInfo:GetChild("GroupBlind").asGroup
    local text_blind_group = group_blind
    local show_group_reward = match_texas.IsSnowballReward
    local show_group_blind = true
    if match_texas.IsSnowballReward == true then
        text_blind_group = group_reward_blind
        show_group_blind = false
    end
    ViewHelper:setGObjectVisible(show_group_reward, group_reward_blind)
    ViewHelper:setGObjectVisible(show_group_blind, group_blind)
    self.TextBlind = self.ComRealTimeInfo:GetChildInGroup(text_blind_group, "TextBlind").asTextField
    self.BlindBg = self.ComRealTimeInfo:GetChildInGroup(text_blind_group, "BlindBg").asImage
    self:setRaiseBlindTm(match_texas.RealtimeInfo.RaiseBlindLeftSecond)
    self:setRealTimeInfo(match_texas.RealtimeInfo, match_texas.RaiseBlindTbInfo.BlindType)
    if is_init == true then
        local hide_chair = false
        for k, v in pairs(self.ViewDesktop.MapAllUiChairInfo) do
            local chair_info = v
            if (self.ViewDesktop.Desktop.SeatNum == TexasDesktopSeatNum.Five)
            then
                local a = (k + 3) % 2
                if (a == 0)
                then
                    chair_info.GComChair.visible = false
                    hide_chair = true
                else
                    hide_chair = false
                end
            end

            chair_info.GComSitOrInvite.visible = false
            if hide_chair == false then
                self.ViewDesktop.MapAllValidPlayerSeat[k] = v
                self.ViewDesktop.MapValidNoPlayerSeat[k] = chair_info
            end
        end
        self:_checkSeat()
    end

    local getdesktop_leftsec = match_texas.PauseCountDownLeftSec
    local view_mgr = self.ViewDesktop.ViewMgr
    if getdesktop_leftsec > 0 then
        local waiting_countdown = view_mgr:getView("WaitingCountDown")
        if waiting_countdown == nil then
            waiting_countdown = view_mgr:createView("WaitingCountDown")
        end
        waiting_countdown:setTips(view_mgr.LanMgr:getLanValue("MatchBegine"), getdesktop_leftsec)
    else
        local waiting_countdown = view_mgr:getView("WaitingCountDown")
        if waiting_countdown ~= nil then
            view_mgr:destroyView(waiting_countdown)
        end
        local can_rebuy = self.ViewDesktop.Desktop.DesktopTypeBase.MeCanRebuy
        ViewHelper:setGObjectVisible(can_rebuy, self.BtnRebuy)
        local can_addon = self.ViewDesktop.Desktop.DesktopTypeBase.MeCanAddon
        ViewHelper:setGObjectVisible(can_addon, self.BtnAddon)
    end
    ViewHelper:setGObjectVisible(true, self.ComRealTimeInfo)
end

function ViewTexasMTT:preflopBegin()
    self:_showRebuyAndAddonBtnAndRealTimeInfo()
    local current_blindid = self.MatchTexas.RealtimeInfo.CurrentRaiseBlindTbId
    local t_blind = TbDataHelper:GetTexasRaiseBlindByTypeAndId(self.MatchTexas.RaiseBlindTbInfo.BlindType, current_blindid)
    self:setMatchDescribe(t_blind.BlindsSmall, t_blind.BlindsBig, t_blind.Ante)
end

function ViewTexasMTT:_showRebuyAndAddonBtnAndRealTimeInfo()
    if self.ViewDesktop.Desktop.MeP.PlayerDataDesktop.DesktopPlayerState == TexasDesktopPlayerState.Ob then
        ViewHelper:setGObjectVisible(false, self.ComRealTimeInfo)
        ViewHelper:setGObjectVisible(false, self.BtnRebuy)
        ViewHelper:setGObjectVisible(false, self.BtnAddon)
    else
        ViewHelper:setGObjectVisible(true, self.ComRealTimeInfo)
        local can_rebuy = self.ViewDesktop.Desktop.DesktopTypeBase.MeCanRebuy
        ViewHelper:setGObjectVisible(can_rebuy, self.BtnRebuy)
        local can_addon = self.ViewDesktop.Desktop.DesktopTypeBase.MeCanAddon
        ViewHelper:setGObjectVisible(can_addon, self.BtnAddon)
    end
end

function ViewTexasMTT:_checkSeat()
    for k, v in pairs(self.ViewDesktop.MapAllValidPlayerSeat)
    do
        local seat = self.ViewDesktop.MapValidNoPlayerSeat[k]
        if (seat == nil)
        then
            if (v ~= nil)
            then
                ViewHelper:setGObjectVisible(false, v.GComChair)
            end
        end
        for k, v in pairs(self.ViewDesktop.MapValidNoPlayerSeat)
        do
            ViewHelper:setGObjectVisible(true, v.GComChair)
        end
    end
end

function ViewTexasMTT:setRealTimeInfo(realtime_info, blind_type)
    local view_mgr = self.ViewDesktop.ViewMgr
    local t_ranking_score = {}
    table.insert(t_ranking_score, realtime_info.MyRanking)
    table.insert(t_ranking_score, "/")
    table.insert(t_ranking_score, realtime_info.PlayerLeftNum)
    table.insert(t_ranking_score, "  ")
    table.insert(t_ranking_score, view_mgr.LanMgr:getLanValue("AverageScore"))
    table.insert(t_ranking_score, ":")
    table.insert(t_ranking_score, realtime_info.AverageScore)
    self.TextRankingAScore.text = table.concat(t_ranking_score)
    if (self.MatchTexas.IsSnowballReward == true)
    then
        local t_reward = {}
        table.insert(t_reward, view_mgr.LanMgr:getLanValue("TotalRewardGold"))
        table.insert(t_reward, ":")
        table.insert(t_reward, realtime_info.TotalRewardGold)
        table.insert(t_reward, "  ")
        table.insert(t_reward, view_mgr.LanMgr:getLanValue("PlayerNumReward"))
        table.insert(t_reward, ":")
        table.insert(t_reward, realtime_info.PlayerNumReward)
        self.TextReward.text = table.concat(t_reward)
    end

    if realtime_info.CurrentRaiseBlindTbId ~= realtime_info.RaiseBlindIdNext then
        self.TextBlind.text = view_mgr.LanMgr:getLanValue("RaiseBlindNext")
        ViewHelper:setGObjectVisible(true, self.BlindBg)
    end
    local current_blindid = realtime_info.CurrentRaiseBlindTbId
    local t_blind = TbDataHelper:GetTexasRaiseBlindByTypeAndId(blind_type, current_blindid)
    self:setMatchDescribe(t_blind.BlindsSmall, t_blind.BlindsBig, t_blind.Ante)
    if self.MatchTexas.Pause then
        self.TextBlind.text = view_mgr.LanMgr:getLanValue("WaitMatchBegine")
    end
end

function ViewTexasMTT:setRaiseBlindTm(tm)
    local raise_tm = ""
    local view_mgr = self.ViewDesktop.ViewMgr
    if tm <= 0 then
        raise_tm = view_mgr.LanMgr:getLanValue("RaiseBlindNext")
        ViewHelper:setGObjectVisible(true, self.BlindBg)
    else
        local t_raise_blind = {}
        table.insert(t_raise_blind, view_mgr.LanMgr:getLanValue("RaiseBlindTm"))
        table.insert(t_raise_blind, ":")
        local tm_str, is_out = CS.Casinos.UiHelper.checkTime(tm)
        table.insert(t_raise_blind, tm_str)
        raise_tm = table.concat(t_raise_blind)
        if tm <= 10 then
            ViewHelper:setGObjectVisible(true, self.BlindBg)
        else
            ViewHelper:setGObjectVisible(false, self.BlindBg)
        end
    end

    self.TextBlind.text = raise_tm
end

function ViewTexasMTT:setMatchDescribe(s_b, b_b, ante)
    local view_mgr = self.ViewDesktop.ViewMgr
    local t_describe = {}
    table.insert(t_describe, view_mgr.LanMgr:getLanValue("Blind"))
    table.insert(t_describe, ":")
    table.insert(t_describe, s_b)
    table.insert(t_describe, "/")
    table.insert(t_describe, b_b)
    table.insert(t_describe, " ")
    table.insert(t_describe, view_mgr.LanMgr:getLanValue("Ante"))
    table.insert(t_describe, ":")
    table.insert(t_describe, ante)
    self.ViewDesktop.TextDesktopDescribe.text = table.concat(t_describe)
end

function ViewTexasMTT:_onClickMenu()
    local view_mgr = self.ViewDesktop.ViewMgr
    local msg_box = view_mgr:getView("MsgBox")
    if msg_box == nil then
        msg_box = view_mgr:createView("MsgBox")
    end
    msg_box:useTwoBtn("", view_mgr.LanMgr:getLanValue("ExitMatchTip"),
            function()
                local ev = self.ViewDesktop.ViewMgr:getEv("EvUiClickExitDesk")
                if (ev == nil)
                then
                    ev = EvUiClickExitDesk:new(nil)
                end
                self.ViewDesktop.ViewMgr:sendEv(ev)
                self.ViewDesktop.ViewMgr:destroyView(msg_box)
            end,
            function()
                self.ViewDesktop.ViewMgr:destroyView(msg_box)
            end
    )
end

function ViewTexasMTT:_onClickRealTimeInfo()
    local view_matchinfo = self.ViewDesktop.ViewMgr:getView("MatchInfo")
    if view_matchinfo == nil then
        view_matchinfo = self.ViewDesktop.ViewMgr:createView("MatchInfo")
    end
    view_matchinfo:Init(self.MatchGuid, true, true)
end

function ViewTexasMTT:_onClickBtnRebuyOrAddon()
    local view_mgr = self.ViewDesktop.ViewMgr
    local ev = view_mgr:getEv("EvUiMTTCreateRebuyOrAddOn")
    if (ev == nil)
    then
        ev = EvUiMTTCreateRebuyOrAddOn:new(nil)
    end
    view_mgr:sendEv(ev)
end

ViewTexasMTTTypeFactory = {}

function ViewTexasMTTTypeFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    return o
end

function ViewTexasMTTTypeFactory:GetName()
    return "TexasMTT"
end

function ViewTexasMTTTypeFactory:CreateViewDesktopType(view_desktop)
    local l = ViewTexasMTT:new(nil, view_desktop)
    return l
end