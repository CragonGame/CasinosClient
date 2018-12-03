-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
DesktopTexasMTT = DesktopTypeBase:new(nil)

---------------------------------------
function DesktopTexasMTT:new(o, desktop_base, co_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ControllerPlayer = co_mgr:GetController("Player")
    o.ControllerActor = co_mgr:GetController("Actor")
    o.ControllerLobby = co_mgr:GetController("Lobby")
    o.ControllerDesktop = co_mgr:GetController("DesktopTexas")
    o.UpdateRaiseBlindTm = 0
    o.DesktopBase = desktop_base
    o.CanUpdateT = false
    o.Name = "TexasMTT"
    self.ConstUpdateRaiseBlindTm = 1
    return o
end

---------------------------------------
function DesktopTexasMTT:OnDestroy(need_createmainui)
    local view_mgr = self.ControllerDesktop.ViewMgr
    if (need_createmainui) then
        local match_lobby = view_mgr:GetView("MatchLobby")
        if (match_lobby == nil) then
            match_lobby = view_mgr:CreateView("MatchLobby")
        end
        CS.Casinos.CasinosContext.Instance:Play("MainBg1", CS.Casinos._eSoundLayer.Background)
    end
end

---------------------------------------
function DesktopTexasMTT:Update(elapsed_tm)
    if self.CanUpdateT then
        local raise_blind_lefts = self.RaiseBlindLeftSecond
        if raise_blind_lefts > 0 then
            raise_blind_lefts = raise_blind_lefts - elapsed_tm
            self.RaiseBlindLeftSecond = raise_blind_lefts
        end

        self.UpdateRaiseBlindTm = self.UpdateRaiseBlindTm + elapsed_tm
        if self.UpdateRaiseBlindTm >= self.ConstUpdateRaiseBlindTm then
            self.UpdateRaiseBlindTm = 0
            local view_mgr = self.ControllerDesktop.ViewMgr
            local ev = view_mgr:GetEv("EvEntityMTTUpdateRaiseBlindTm")
            if (ev == nil) then
                ev = EvEntityMTTUpdateRaiseBlindTm:new(nil)
            end
            ev.RaiseBlindTm = math.ceil(self.RaiseBlindLeftSecond)
            view_mgr:SendEv(ev)
        end
    end
end

---------------------------------------
function DesktopTexasMTT:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvUiClickPlayerReturn") then
            local method_info = MethodInfoTexasDesktop:new(nil)
            method_info.id = MethodTypeTexasDesktop.PlayerCancelAutoActionRequest
            method_info.data = nil
            self:_userRequest(method_info)
        elseif (ev.EventName == "EvUiClickCancelAutoAction") then
            local method_info = MethodInfoTexasDesktop:new(nil)
            method_info.id = MethodTypeTexasDesktop.PlayerCancelAutoActionRequest
            method_info.data = nil
            self:_userRequest(method_info)
        elseif ev.EventName == "EvUiMTTCreateRebuyOrAddOn" then
            self:createRebuyOrAddon(0)
        elseif ev.EventName == "EvEntityMatchGameOver" then
            if self.MatchGuid == ev.game_over.MatchGuid then
                local all_valid_player = self.DesktopBase:getAllValidPlayer()
                local view_mgr = self.ControllerDesktop.ViewMgr
                local view_mtt_gameresult = view_mgr:CreateView("MTTGameResult")
                view_mtt_gameresult:setResult(ev.game_over, #all_valid_player < 2)
                local msg_box = view_mgr:CreateView("MsgBox")
                if msg_box ~= nil then
                    view_mgr:DestroyView(msg_box)
                end
            end
        elseif ev.EventName == "EvEntitySetMatchDetailedInfo" then
            local view_mgr = self.ControllerDesktop.ViewMgr
            local ev = view_mgr:GetEv("EvEntitySetRaiseBlindTbInfo")
            if (ev == nil) then
                ev = EvEntitySetRaiseBlindTbInfo:new(nil)
            end
            ev.raise_blind_info = self.BDesktopSnapshotMatchTexas.RaiseBlindTbInfo
            ev.current_raiseblind_tbid = self.BDesktopSnapshotMatchTexas.RealtimeInfo.CurrentRaiseBlindTbId
            view_mgr:SendEv(ev)
        elseif ev.EventName == "EvEntityDesktopPlayerLeaveChair" then
            if self.DesktopBase.MeP.PlayerDataDesktop.DesktopPlayerState == TexasDesktopPlayerState.Ob then
                local all_valid_player = self.DesktopBase:getAllValidPlayer()
                if #all_valid_player == 0 then
                    local msg_box = ViewHelper:UiShowMsgBox(self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("NoPlayer"), function()
                        local view_mgr = self.ControllerDesktop.ViewMgr
                        local ev = view_mgr:GetEv("EvUiClickExitDesk")
                        if (ev == nil) then
                            ev = EvUiClickExitDesk:new(nil)
                        end
                        view_mgr:SendEv(ev)
                        view_mgr:DestroyView(msg_box)
                    end)
                end
            end
        end
    end
end

---------------------------------------
function DesktopTexasMTT:SetDesktopSnapshotData(snapshot_data)
    self.BDesktopSnapshotMatchTexas = snapshot_data.match_texas
    self.MatchGuid = snapshot_data.match_texas.MatchGuid
    self.BlindType = snapshot_data.match_texas.RaiseBlindTbInfo.BlindType
    local current_blindid = snapshot_data.match_texas.RealtimeInfo.CurrentRaiseBlindTbId
    local t_blind = TbDataHelper:GetTexasRaiseBlindByTypeAndId(self.BlindType, current_blindid)
    self.SmallBlind = t_blind.BlindsSmall
    self.BigBlind = t_blind.BlindsBig
    if self.BDesktopSnapshotMatchTexas.Pause then
        self.CanUpdateT = false
    else
        self.CanUpdateT = true
    end
    if snapshot_data.match_texas.RealtimeInfo.CurrentRaiseBlindTbId ~= snapshot_data.match_texas.RealtimeInfo.RaiseBlindIdNext then
        self.RaiseBlindLeftSecond = 0
    else
        self.RaiseBlindLeftSecond = snapshot_data.match_texas.RealtimeInfo.RaiseBlindLeftSecond
    end
    self.ServerAutoAction = snapshot_data.match_texas.MyInfo.ServerAutoAction
    self:refreshBtnRebuyAddOnStat()
    --local left_sec = snapshot_data.match_texas.MyInfo.GameOverCountDownLeftSec
    --if left_sec > 0 then
    --    self.NeedRebuyOrAddOn = true
    --    self:createRebuyOrAddon(math.ceil(left_sec))
    --else
    --    self.NeedRebuyOrAddOn = false
    --end
end

---------------------------------------
function DesktopTexasMTT:DesktopUser(method_id, method_data)
    if (method_id == MethodTypeTexasDesktop.TexasMTTServerAutoActionStateChangeNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid)) then
            return
        end
        local auto_action = self.ControllerDesktop.ControllerMgr:UnpackData(method_data)

        local m_p = self.DesktopBase.MeP
        if m_p ~= nil then
            m_p:mttAutoAction(auto_action)
        end
    elseif (method_id == MethodTypeTexasDesktop.TexasMTTUpdateRealtimeInfoNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid)) then
            return
        end

        local real_info_data = self.ControllerDesktop.ControllerMgr:UnpackData(method_data)
        local realtime_info = BMatchTexasRealtimeInfo:new(nil)
        realtime_info:setData(real_info_data)
        self.BDesktopSnapshotMatchTexas.RealtimeInfo = realtime_info
        self:refreshBtnRebuyAddOnStat()
        local current_blindid = realtime_info.CurrentRaiseBlindTbId
        local t_blind = TbDataHelper:GetTexasRaiseBlindByTypeAndId(self.BlindType, current_blindid)
        self.SmallBlind = t_blind.BlindsSmall
        self.BigBlind = t_blind.BlindsBig
        if realtime_info.CurrentRaiseBlindTbId ~= realtime_info.RaiseBlindIdNext then
            self.RaiseBlindLeftSecond = realtime_info.RaiseBlindLeftSecond
            self.CanUpdateT = false
        end

        local view_mgr = self.ControllerDesktop.ViewMgr
        local ev = view_mgr:GetEv("EvEntityMTTUpdateRealtimeInfo")
        if (ev == nil) then
            ev = EvEntityMTTUpdateRealtimeInfo:new(nil)
        end
        ev.RealtimeInfo = realtime_info
        ev.blind_type = self.BlindType
        view_mgr:SendEv(ev)
    elseif (method_id == MethodTypeTexasDesktop.TexasMTTUpdateProcessNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid)) then
            return
        end

        local process_type = self.ControllerDesktop.ControllerMgr:UnpackData(method_data)
        --local process = DesktopNotifyMTTUpdateProcess:new(nil)
        --process:setData(data1)

        local view_mgr = self.ControllerDesktop.ViewMgr
        local view_process = view_mgr:GetView("MTTProcess")
        if view_process == nil then
            view_process = view_mgr:CreateView("MTTProcess")
        end
        view_process:setProcess(process_type)
    elseif (method_id == MethodTypeTexasDesktop.TexasMTTDesktopStartOrPause) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid)) then
            return
        end

        local notify = self.ControllerDesktop.ControllerMgr:UnpackData(method_data)
        local notifyex = BMatchTexasDesktopStartOrPauseNotify:new(nil)
        notifyex:setData(notify)
        if notifyex.Pause then
            self.CanUpdateT = true
        end
        self.BDesktopSnapshotMatchTexas.Pause = notifyex.Pause
        self.BDesktopSnapshotMatchTexas.PauseCountDownLeftSec = notifyex.PauseCountdownLeftSec

        local view_mgr = self.ControllerDesktop.ViewMgr
        local ev = view_mgr:GetEv("EvMTTPauseChanged")
        if (ev == nil) then
            ev = EvMTTPauseChanged:new(nil)
        end
        ev.pause_info = notifyex
        view_mgr:SendEv(ev)
    elseif (method_id == MethodTypeTexasDesktop.TexasMTTDesktoPlayerInfoUpdate) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid)) then
            return
        end

        local notify = self.ControllerDesktop.ControllerMgr:UnpackData(method_data)
        local player_info = BMatchTexasPlayerInfo:new(nil)
        player_info:setData(notify)

        if player_info.PlayerGuid == self.ControllerDesktop.PlayerGuid then
            self.NeedRebuyOrAddOn = false
            self.BDesktopSnapshotMatchTexas.MyInfo.RebuyNum = player_info.RebuyCount
            self.BDesktopSnapshotMatchTexas.MyInfo.AddonNum = player_info.AddonCount
            --self.DesktopBase.MeP:rebuyOrAddOn(player_info.Score)
            self:refreshBtnRebuyAddOnStat()
            ViewHelper:UiShowMsgBox(string.format(self.ControllerDesktop.ViewMgr.LanMgr:getLanValue("RebuySuccess"), self.BDesktopSnapshotMatchTexas.RaiseBlindTbInfo.AddonScore)) -- "RebuyTip1"  "AddonSuccess"
        else
            --local view_mgr = self.ControllerDesktop.ViewMgr
            --local ev = view_mgr:GetEv("EvUpdatePlayerScore")
            --if (ev == nil)
            --then
            --    ev = EvUpdatePlayerScore:new(nil)
            --end
            --ev.PlayerGuid = player_info.PlayerGuid
            --ev.Score = player_info.Score
            --view_mgr:SendEv(ev)
        end
    end
end

---------------------------------------
function DesktopTexasMTT:preflopBegin()
    self.CanUpdateT = true
end

---------------------------------------
function DesktopTexasMTT:_userRequest(method_info)
    self.ControllerDesktop:UserRequest("Texas", method_info:getData4Pack())
end

---------------------------------------
function DesktopTexasMTT:chipIsEnough(need_chip)
    local enough = self.ControllerActor.PropGoldAcc:get() >= need_chip

    local view_mgr = self.ControllerDesktop.ViewMgr
    if (enough == false) then
        local msg_box = view_mgr:CreateView("MsgBox")
        local tips = self.ControllerDesktop.ViewMgr.LanMgr:getLanValue("ChipNotEnoughTips")
        tips = string.format(tips, UiChipShowHelper:GetGoldShowStr(need_chip, self.ControllerDesktop.ViewMgr.LanMgr.LanBase))
        local title = self.ControllerDesktop.ViewMgr.LanMgr:getLanValue("ChipNotEnough")
        msg_box:showMsgBox1(title, tips,
                function(bo)
                    if (bo) then
                        local view_mgr = self.ControllerDesktop.ViewMgr
                        local ev = view_mgr:GetEv("EvUiClickShop")
                        if (ev == nil) then
                            ev = EvUiClickShop:new(nil)
                        end
                        view_mgr:SendEv(ev)
                    end
                end
        )
    end

    return enough
end

---------------------------------------
function DesktopTexasMTT:checkMeStack(me_stack)
    if (me_stack == 0) then
        self.NeedRebuyOrAddOn = true
        self:createRebuyOrAddon(8)
    end
end

---------------------------------------
function DesktopTexasMTT:createRebuyOrAddon(tm)
    local match_texas = self.BDesktopSnapshotMatchTexas
    local view_mgr = self.ControllerDesktop.ViewMgr
    local cose_chip = 0
    local buy_score = 0
    local alow_name = ""
    local alow_time = 0
    if self.MeCanRebuy == true then
        cose_chip = match_texas.RaiseBlindTbInfo.RebuyGold
        buy_score = match_texas.RaiseBlindTbInfo.RebuyScore
        alow_name = self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("Rebuy")
        alow_time = #match_texas.RaiseBlindTbInfo.ListRaiseBlindTbIdCanRebuy - match_texas.MyInfo.RebuyNum
    elseif self.MeCanAddon == true then
        cose_chip = match_texas.RaiseBlindTbInfo.AddonGold
        buy_score = match_texas.RaiseBlindTbInfo.AddonScore
        alow_name = self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("Addon")
        alow_time = #match_texas.RaiseBlindTbInfo.ListRaiseBlindTbIdCanAddon - match_texas.MyInfo.AddonNum
    else
        return
    end
    local view_msg = view_mgr:CreateView("MsgBox")
    view_msg:useTwoBtn2("", string.format(self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("BebuyAddonTip"), cose_chip, buy_score, alow_name, alow_time),
            self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("Buy"), self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("NotBuy"), tm, true, function()
                self:_confirmRebuyOrAddon()
            end, function()
                self:_cancelRebuyOrAddon()
            end)
end

---------------------------------------
function DesktopTexasMTT:refreshBtnRebuyAddOnStat()
    local match_texas = self.BDesktopSnapshotMatchTexas
    local can_rebuy = false
    local can_rebuy_c = 0
    if match_texas.RaiseBlindTbInfo.ListRaiseBlindTbIdCanRebuy ~= nil then
        can_rebuy = LuaHelper:TableContainsV(match_texas.RaiseBlindTbInfo.ListRaiseBlindTbIdCanRebuy, match_texas.RealtimeInfo.CurrentRaiseBlindTbId)
        can_rebuy_c = #match_texas.RaiseBlindTbInfo.ListRaiseBlindTbIdCanRebuy
    end
    local can_addon = false
    local can_addon_c = 0
    if can_rebuy == false then
        if match_texas.RaiseBlindTbInfo.ListRaiseBlindTbIdCanAddon ~= nil then
            can_addon = LuaHelper:TableContainsV(match_texas.RaiseBlindTbInfo.ListRaiseBlindTbIdCanAddon, match_texas.RealtimeInfo.CurrentRaiseBlindTbId)
            can_addon_c = #match_texas.RaiseBlindTbInfo.ListRaiseBlindTbIdCanAddon
        end
    end

    local left_rebuy_tm = can_rebuy_c - match_texas.MyInfo.RebuyNum
    local left_addon_tm = can_addon_c - match_texas.MyInfo.AddonNum
    local can_rebuy1 = false
    if can_rebuy and left_rebuy_tm > 0 then
        can_rebuy1 = true
    end
    self.MeCanRebuy = can_rebuy1 and self.BDesktopSnapshotMatchTexas.Pause == false

    local can_addon1 = false
    if can_addon and left_addon_tm > 0 then
        can_addon1 = true
    end
    self.MeCanAddon = can_addon1 and self.BDesktopSnapshotMatchTexas.Pause == false
    local view_mgr = self.ControllerDesktop.ViewMgr
    local ev = view_mgr:GetEv("EvEntityMTTPlayerRebuyOrAddonRefresh")
    if (ev == nil) then
        ev = EvEntityMTTPlayerRebuyOrAddonRefresh:new(nil)
    end
    ev.can_rebuy = self.MeCanRebuy
    ev.can_addon = self.MeCanAddon
    view_mgr:SendEv(ev)
end

---------------------------------------
function DesktopTexasMTT:_confirmRebuyOrAddon()
    if self.MeCanRebuy == true then
        local enough = self:chipIsEnough(self.BDesktopSnapshotMatchTexas.RaiseBlindTbInfo.RebuyGold)
        if enough then
            self.ControllerDesktop:MatchTexasRequestRebuy(self.MatchGuid)
        end
    elseif self.MeCanAddon == true then
        local enough = self:chipIsEnough(self.BDesktopSnapshotMatchTexas.RaiseBlindTbInfo.AddonGold)
        if enough then
            self.ControllerDesktop:MatchTexasRequestAddon(self.MatchGuid)
        end
    end
end

---------------------------------------
function DesktopTexasMTT:_cancelRebuyOrAddon()
    if self.NeedRebuyOrAddOn then
        self.ControllerDesktop:MatchTexasRequestGiveUpRebuyOrAddon(self.MatchGuid)
        self.NeedRebuyOrAddOn = false
    end
end

---------------------------------------
function DesktopTexasMTT:getFastBetKey()
    local desktop_id = self.BlindType
    local t_preflop = {}
    table.insert(t_preflop, desktop_id)
    table.insert(t_preflop, DesktopType.MTT)
    table.insert(t_preflop, "true")
    local t_not_preflop = {}
    table.insert(t_not_preflop, desktop_id)
    table.insert(t_not_preflop, DesktopType.MTT)
    table.insert(t_not_preflop, "false")
    local t_p_s = table.concat(t_preflop)
    local t_np_s = table.concat(t_not_preflop)
    return t_p_s, t_np_s
end

---------------------------------------
DesktopTexasMTTFactory = DesktopTypeBaseFactory:new(nil)

---------------------------------------
function DesktopTexasMTTFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function DesktopTexasMTTFactory:GetName()
    return "TexasMTT"
end

---------------------------------------
function DesktopTexasMTTFactory:CreateDesktopType(desktop_base, co_mgr)
    local l = DesktopTexasMTT:new(nil, desktop_base, co_mgr)
    return l
end