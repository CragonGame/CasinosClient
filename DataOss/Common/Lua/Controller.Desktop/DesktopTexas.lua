-- Copyright(c) Cragon. All rights reserved.
-- 桌子销毁时，该类销毁；获取快照时该类创建

---------------------------------------
TexasSeatInfo = {}

function TexasSeatInfo:new(o, index, player_texas)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.index = index
    o.player_texas = player_texas
    return o
end

---------------------------------------
DesktopTexas = DesktopBase:new(nil)

---------------------------------------
function DesktopTexas:new(o, co_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ControllerPlayer = co_mgr:GetController("Player")
    o.ControllerActor = co_mgr:GetController("Actor")
    o.ControllerLobby = co_mgr:GetController("Lobby")
    o.ControllerDesktop = co_mgr:GetController("Desktop")
    o.CurrentUnSendDesktopMsg = nil
    o.MapAllPlayer = {}
    o.MePlayer = nil
    o.Guid = nil
    o.MeAllCard = {}
    o.CurrentRountMaxBet = 0
    o.MapPlayerTexas = {} --有座玩家
    o.CommunityCards = {}
    o.PotMain = 0
    o.DesktopState = TexasDesktopState.Idle
    o.PlayerTurn = PlayerTurnDataTexas:new(nil)
    o.PlayerTurn.player_guid = ""
    o.LastPlayerTurn = ""
    o.PlayerTurnLeftTm = 0
    o.MapSeatPlayerChatIsLock = {}
    o.QuePlayerTexas = {}
    o.QueUiPlayerInfo = {}
    o.LockSpectatorChat = false
    o.AllSeat = nil
    o.GameEnd = false
    o.ReBeginTm = 0 --GameEnd状态时长
    o.AlreadyShowDown = false --Showdown完成标志
    o.MeP = nil
    o.DesktopTypeBase = nil
    o.TFastBetInfoPreFlop = {}
    o.TFastBetInfoNotPreFlop = {}
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    return o
end

---------------------------------------
function DesktopTexas:GetDesktopPlayerByGuid(player_guid)
    local desktopPlayer = self.MapAllPlayer[player_guid]
    return desktopPlayer
end

---------------------------------------
function DesktopTexas:OnCreate()
    self.MapDesktopTypeBaseFac = {}
    self:regDesktopTypeBaseFactory(DesktopTexasClassicFactory:new(nil))
    self:regDesktopTypeBaseFactory(DesktopTexasMTTFactory:new(nil))
end

---------------------------------------
function DesktopTexas:OnDestroy(need_createmainui)
    self.MePlayer = nil
    self.MeP = nil
    for k, v in pairs(self.MapPlayerTexas) do
        self:_releasePlayerTexas(v)
    end
    self.MapPlayerTexas = {}
    self.PotMain = 0
    self.MeAllCard = {}
    self.CurrentRountMaxBet = 0
    self.CommunityCards = {}
    self.AllSeat = {}
    self.PlayerTurn = PlayerTurnDataTexas:new(nil)
    self.PlayerTurnLeftTm = 0
    self.MapSeatPlayerChatIsLock = {}
    self.CurrentUnSendDesktopMsg = ""

    local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
    local que_uicount = #self.QueUiPlayerInfo
    if (que_uicount > 0) then
        for i = 0, que_uicount do
            local ui_playerinfo = table.remove(self.QueUiPlayerInfo, 1)
            view_mgr:DestroyView(ui_playerinfo)
        end
    end
    self.QueUiPlayerInfo = nil
    self.QuePlayerTexas = {}
    self.QuePlayerTexas = nil
    local ui_desktop = view_mgr:GetView("DesktopTexas")
    view_mgr:DestroyView(ui_desktop)

    self.DesktopTypeBase:OnDestroy(need_createmainui)
    self.DesktopTypeBase = nil
end

---------------------------------------
function DesktopTexas:Update(elapsed_tm)
    if self.DesktopTypeBase ~= nil then
        self.DesktopTypeBase:Update(elapsed_tm)
    end
    for k, v in pairs(self.MapPlayerTexas) do
        v:Update(elapsed_tm)
    end
end

---------------------------------------
function DesktopTexas:OnHandleEv(ev)
    if (ev ~= nil) then
        self.DesktopTypeBase:OnHandleEv(ev)

        if (ev.EventName == "EvUiClickFlod") then
            self: requestPlayerActionFold()
        elseif (ev.EventName == "EvUiClickCheck") then
            self: requestPlayerActionCheck()
        elseif (ev.EventName == "EvUiClickCall") then
            self: requestPlayerActionCall()
        elseif (ev.EventName == "EvUiClickRaise") then
            self:requestPlayerActionRaise(ev.raise_gold)
        elseif (ev.EventName == "EvUiClickOB") then
            self:requestPlayerOb()
        elseif (ev.EventName == "EvUiClickWaitWhile") then
            self:requestPlayerWaitWhile()
        elseif (ev.EventName == "EvUiClickAutoAction") then
            self:requestPlayerAutoAction(ev.auto_action_type)
        elseif (ev.EventName == "EvUiClickCancelAutoAction") then
            self:requestPlayerCancelAutoAction()
        elseif (ev.EventName == "EvUiRequestLockPlayerChat") then
            local player_guid = ev.player_guid
            local lock_player = ev.requestLock
            self.MapSeatPlayerChatIsLock[player_guid] = lock_player
        elseif (ev.EventName == "EvUiRequestLockAllDesktopPlayer") then
            for k, v in pairs(self.MapSeatPlayerChatIsLock) do
                self.MapSeatPlayerChatIsLock[k] = ev.requestLock
            end
        elseif (ev.EventName == "EvUiRequestLockAllSpectator") then
            self.LockSpectatorChat = ev.requestLock
        elseif (ev.EventName == "EvUiSendMsg") then
            local chat_msg = ev.chat_msg
            local recver_guid = chat_msg[4]
            if (CS.System.String.IsNullOrEmpty(recver_guid)) then
                self.ControllerDesktop:RequestSendMsg(chat_msg)
                self.CurrentUnSendDesktopMsg = ""
            end
        elseif (ev.EventName == "EvUiSetUnSendDesktopMsg") then
            self.CurrentUnSendDesktopMsg = ev.text
        elseif (ev.EventName == "EvUiDesktopClickLockChat") then
            local ui_lockchat = self.ControllerDesktop.ControllerMgr.ViewMgr:CreateView("LockChatTexas")
            ui_lockchat:initLockChat(self.AllSeat)
        elseif (ev.EventName == "EvUiClickShowCard") then
            if self.MeP ~= nil then
                local state = self.MeP.ShowCardState
                local change_to = TexasPlayerShowCardState.None
                if ev.click_card1 then
                    if state == TexasPlayerShowCardState.First then
                        change_to = TexasPlayerShowCardState.None
                    elseif state == TexasPlayerShowCardState.Second then
                        change_to = TexasPlayerShowCardState.FirstAndSecond
                    elseif state == TexasPlayerShowCardState.FirstAndSecond then
                        change_to = TexasPlayerShowCardState.Second
                    else
                        change_to = TexasPlayerShowCardState.First
                    end
                else
                    if state == TexasPlayerShowCardState.First then
                        change_to = TexasPlayerShowCardState.FirstAndSecond
                    elseif state == TexasPlayerShowCardState.Second then
                        change_to = TexasPlayerShowCardState.None
                    elseif state == TexasPlayerShowCardState.FirstAndSecond then
                        change_to = TexasPlayerShowCardState.First
                    else
                        change_to = TexasPlayerShowCardState.Second
                    end
                end
                self:requestShowCard(change_to)
                self.MeP:setShowCardState(change_to)
            end
        elseif (ev.EventName == "EvUpdatePlayerScore") then
            local seat = self:getSeatByGuid(ev.PlayerGuid)
            if seat ~= nil and seat.player_texas ~= nil then
                seat.player_texas:rebuyOrAddOn(ev.Score)
            end
        end
    end
end

---------------------------------------
function DesktopTexas:SetDesktopSnapshotData(desktop_data, is_init)
    ViewHelper:UiEndWaiting()
    self.ControllerLobby:HideLobby()

    self.IsSnapshot = true
    local snapshot_data1 = self.ControllerDesktop.ControllerMgr:UnpackData(desktop_data.DesktopData)
    local snapshot_data = DesktopSnapshotDataTexas:new(nil)
    snapshot_data:setData(snapshot_data1)
    local desktoptype_facname = "TexasClassic"
    local desktop_type = DesktopType.Classic
    if snapshot_data.match_texas ~= nil then
        desktoptype_facname = "TexasMTT"
        desktop_type = DesktopType.MTT
    end

    local desktoptype_factory = self:GetDesktopTypeBaseFactory(desktoptype_facname)
    self.DesktopTypeBase = desktoptype_factory:CreateDesktopType(self, self.ControllerDesktop.ControllerMgr)
    self.DesktopTypeBase.DesktopTexas = self
    self.DesktopGuid = snapshot_data.desktop_guid
    self.SeatNum = snapshot_data.seat_num
    self.IsVIP = snapshot_data.is_vip
    self.IsPrivate = snapshot_data.is_private
    self.GameSpeed = snapshot_data.game_speed
    self.ActionWaitingTm = snapshot_data.desktop_action_waiting_tm
    self.WaitWhileTm = snapshot_data.desktop_waitwhile_tm
    self.DealerSeatIndex = snapshot_data.dealer_seat_index
    self.DesktopState = snapshot_data.desktop_state

    if (snapshot_data.desktop_state == TexasDesktopState.GameEnd) then
        self.GameEnd = true
    else
        self.GameEnd = false
    end

    local pot_m = snapshot_data.pot_total
    if (self.GameEnd == true) then
        pot_m = 0
    end
    self.PotMain = pot_m
    self.ListPot = snapshot_data.list_pot
    self.Guid = desktop_data.DesktopGuid

    self.CommunityCards = {}
    local list_commoncard = snapshot_data.list_community_cards
    if (list_commoncard ~= nil) then
        for i, v in pairs(list_commoncard) do
            local suit = v[1]
            local type = v[2]
            local card = CS.Casinos.Card(suit, type)
            table.insert(self.CommunityCards, card)
        end
    end

    local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
    local ui_desk = view_mgr:GetView("DesktopTexas")
    if (ui_desk == nil) then
        ui_desk = view_mgr:CreateView("DesktopTexas")
        self.CasinosContext:StopAllSceneSound()
    end

    self.AllSeat = {}
    for i = 0, self.SeatNum - 1 do
        local seat_info = TexasSeatInfo:new(nil, i, nil)
        self.AllSeat[i] = seat_info
    end

    for k, v in pairs(self.MapPlayerTexas) do
        self:_releasePlayerTexas(v)
    end

    self.MapPlayerTexas = {}

    self.DesktopTypeBase:SetDesktopSnapshotData(snapshot_data)
    local pre_flop_key, not_preflop = self.DesktopTypeBase:getFastBetKey()
    self.TFastBetInfoPreFlop = TbDataHelper:GetDesktopFastBet(pre_flop_key)
    self.TFastBetInfoNotPreFlop = TbDataHelper:GetDesktopFastBet(not_preflop)

    ui_desk:setDesktopSnapshotData(self, snapshot_data, is_init, desktoptype_facname)
    self.MeAllCard = {}
    if (snapshot_data.my_hand_card ~= nil) then
        for i, v in pairs(snapshot_data.my_hand_card) do
            local suit = v[1]
            local type = v[2]
            local card = CS.Casinos.Card(suit, type)
            table.insert(self.MeAllCard, card)
        end
    end

    if (snapshot_data.list_actorinfo ~= nil) then
        local t_list_actorinfo = snapshot_data.list_actorinfo
        local me_data = nil
        for i, v in pairs(t_list_actorinfo) do
            if (v.PlayerGuid == self.ControllerPlayer.Guid) then
                me_data = v
                break
            end
        end
        self:_initActorMirror(me_data, true)

        for i, v in pairs(t_list_actorinfo) do
            if (v.PlayerGuid ~= self.ControllerPlayer.Guid) then
                self:_initActorMirror(v, false)
            end
        end

        if (self.MeP ~= nil) then
            if (#self.MeAllCard > 0) then
                local first_card = nil
                local second_card = nil
                if (self.MeP.PlayerDataDesktop.DesktopPlayerState == TexasDesktopPlayerState.InGame) then
                    first_card = self.MeAllCard[1]
                    second_card = self.MeAllCard[2]
                end

                if (first_card ~= nil) then
                    self.MeP:setHoleCards(first_card, second_card, self.GameEnd == false, true)
                end
            end
        end
    end

    self:_setPlayerTurn(snapshot_data.player_turn, snapshot_data.player_turn_lefttm, true)

    if (snapshot_data.list_showdown ~= nil) then
        local l_c = #snapshot_data.list_showdown
        if (l_c > 0) then
            for i, v in pairs(snapshot_data.list_showdown) do
                local value = v
                local hole_card = PlayerHoleCardDataTexas:new(nil)
                hole_card:setData(value)
                local player = self.MapPlayerTexas[hole_card.player_guid]
                if (player ~= nil) then
                    player:setHoleCards(CS.Casinos.Card(value.first_card[1], value.first_card[2]),
                            CS.Casinos.Card(value.second_card[1], value.second_card[2]), false, true)
                    player:showDown()
                end
            end
        end
    end

    if (snapshot_data.list_winner ~= nil) then
        local l_c = #snapshot_data.list_winner
        if (l_c > 0) then
            for i, v in pairs(snapshot_data.list_winner) do
                local player = self.MapPlayerTexas[v.player_guid]
                if (player ~= nil) then
                    player:isWinner(v.win_chip)
                end
            end
        end
    end

    if self.GameEnd and snapshot_data.list_showcard ~= nil then
        local l_s = #snapshot_data.list_showcard
        if (l_s > 0) then
            for i, v in pairs(snapshot_data.list_showcard) do
                local player = self.MapPlayerTexas[v.player_guid]
                if (player ~= nil) then
                    local card1 = nil
                    if v.first_card ~= nil then
                        card1 = CS.Casinos.Card(v.first_card[1], v.first_card[2])
                    end
                    local card2 = nil
                    if v.second_card ~= nil then
                        card2 = CS.Casinos.Card(v.second_card[1], v.second_card[2])
                    end
                    player:setShowCardData(card1, card2)
                end
            end
        end
    end

    if self.MeP ~= nil then
        self.MeP:setShowCardState(snapshot_data.showcard_state)
    end

    self.IsSnapshot = false
end

---------------------------------------
function DesktopTexas:PlayerEnter(player_data)
    if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
        return
    end

    local player_d = PlayerDataDesktop:new(nil)
    player_d:setData(player_data)
    local p = self.MapPlayerTexas[player_d.PlayerGuid]
    if (p ~= nil) then
        return
    end

    self:_initActorMirror(player_d)

    local content = string.format("%s%s", player_d.NickName, self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("EnterDesk"))
    self.ControllerDesktop:addDesktopMsg("", self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("TheDealer"), 0, content)
    self.CurrentUnSendDesktopMsg = ""
end

---------------------------------------
function DesktopTexas:PlayerLeave(player_guid)
    if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
        return
    end

    local player_texas = self.MapPlayerTexas[player_guid]
    if (player_texas ~= nil) then
        self:_leaveDesktop(player_texas)
        local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
        local ev = view_mgr:GetEv("EvEntityDesktopPlayerLeaveChair")
        if (ev == nil) then
            ev = EvEntityDesktopPlayerLeaveChair:new(nil)
        end
        ev.guid = player_guid
        view_mgr:SendEv(ev)
    end
end

---------------------------------------
function DesktopTexas:PlayerSitdown(sitdown_data1)
    if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
        return
    end

    local sitdown_data = PlayerSitdownData:new(nil)
    sitdown_data:setData(sitdown_data1)
    local player_texas = self.MapPlayerTexas[sitdown_data.player_guid]
    if (player_texas ~= nil) then
        if (self:isValidSeatIndex(sitdown_data.seat_index)) then
            self.AllSeat[sitdown_data.seat_index].player_texas = player_texas
            local stack = tonumber(sitdown_data.user_data1)
            player_texas:playerSitdown(sitdown_data.seat_index,
                    stack, TexasDesktopPlayerState.Wait4Next, PlayerActionTypeTexas.None)

            local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
            local ev = view_mgr:GetEv("EvEntityDesktopPlayerSit")
            if (ev == nil) then
                ev = EvEntityDesktopPlayerSit:new(nil)
            end
            ev.guid = player_texas.PlayerDataDesktop.PlayerGuid
            ev.account_id = player_texas.PlayerDataDesktop.AccountId
            ev.nick_name = player_texas.PlayerDataDesktop.NickName
            ev.icon_name = player_texas.PlayerDataDesktop.IconName
            ev.vip_level = player_texas.PlayerDataDesktop.VIPLevel
            view_mgr:SendEv(ev)
        end
    end
end

---------------------------------------
function DesktopTexas:PlayerOb(player_guid)
    local player_texas = self.MapPlayerTexas[player_guid]
    if (player_texas ~= nil) then
        player_texas:playerOb()
        if (player_texas.IsMe) then
            self.MeAllCard = {}
        end

        for k, v in pairs(self.AllSeat) do
            if (v.player_texas ~= nil and v.player_texas.Guid == player_guid) then
                v.player_texas = nil
                local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
                local ev = view_mgr:GetEv("EvEntityDesktopPlayerLeaveChair")
                if (ev == nil) then
                    ev = EvEntityDesktopPlayerLeaveChair:new(nil)
                end
                ev.guid = player_guid
                view_mgr:SendEv(ev)
                break
            end
        end
    end
end

---------------------------------------
function DesktopTexas:PlayerWaitWhile(player_guid)
    if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
        return
    end

    local player_texas = self.MapPlayerTexas[player_guid]
    if (player_texas ~= nil) then
        player_texas:playerWaitWhile()
    end
end

---------------------------------------
function DesktopTexas:PlayerReturn(return_data1)
    local return_data = PlayerReturnData:new(nil)
    return_data:setData(return_data1)
    local player_texas = self.MapPlayerTexas[return_data.player_guid]
    if (player_texas ~= nil) then
        local stack = tonumber(return_data.user_data1)
        local state = tonumber(return_data.user_data2)
        player_texas:playerReturn(stack, state, PlayerActionTypeTexas.None)
    end
end

---------------------------------------
function DesktopTexas:DesktopUser(info_user)
    local method_i = self.ControllerDesktop.ControllerMgr:UnpackData(info_user[2])
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = method_i[1]
    method_info.data = method_i[2]
    self.DesktopTypeBase:DesktopUser(method_info.id, method_info.data)
    if (method_info.id == MethodTypeTexasDesktop.DesktopIdleNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        local idle1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        local idle = TexasDesktopNotifyIdle:new(nil)
        idle:setData(idle1)
        self.GameEnd = false
        self.ReBeginTm = -1
        self.DesktopState = TexasDesktopState.Idle
        self.PotMain = 0
        self.ListPot = {}
        self.ListSeatChipEnterPot = {}
        self.CommunityCards = {}
        self.MeAllCard = {}
        self.LastPlayerTurn = ""

        if (self.AllSeat ~= nil) then
            for k, v in pairs(self.AllSeat) do
                if (v.player_texas ~= nil) then
                    v.player_texas:deskIdle()
                end
            end
        end

        if idle.list_player_state ~= nil then
            for i, v in pairs(idle.list_player_state) do
                local player = self:_getActorMirror(v.player_guid)
                if player ~= nil then
                    player:playerStateChanged(v)
                end
            end
        end

        local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
        local ev = view_mgr:GetEv("EvEntityDesktopIdleNotify")
        if (ev == nil) then
            ev = EvEntityDesktopIdleNotify:new(nil)
        end
        view_mgr:SendEv(ev)
    elseif (method_info.id == MethodTypeTexasDesktop.DesktopPreFlopNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        local desktop_preflop1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        local desktop_preflop = DesktopNotifyDesktopPreFlopTexas:new(nil)
        desktop_preflop:setData(desktop_preflop1)
        ViewHelper:UiEndWaiting()
        self.GameEnd = false
        self.ReBeginTm = -1
        self.AlreadyShowDown = false
        self.ControllerDesktop:addDesktopMsg("", self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("TheDealer"), 0, self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("GameBegine"))
        self.DesktopState = TexasDesktopState.PreFlop
        self.PotMain = desktop_preflop.pot_total
        self.ListPot = desktop_preflop.list_pot
        self.ListSeatChipEnterPot = {}
        self.MeAllCard = {}
        self.CommunityCards = {}
        self.CurrentRountMaxBet = 0
        self.DesktopTypeBase:preflopBegin()

        for k, v in pairs(self.AllSeat) do
            if (v.player_texas ~= nil) then
                v.player_texas:setIsBtn(false)
            end
        end

        if (desktop_preflop.list_player_preflop_data ~= nil) then
            local l_c = #desktop_preflop.list_player_preflop_data
            if (l_c > 0) then
                for i, v in pairs(desktop_preflop.list_player_preflop_data) do
                    local p_d = v
                    local player = self.MapPlayerTexas[p_d.player_guid]
                    if (player ~= nil) then
                        player:preflopBegin()
                        player.PlayerDataDesktop.Stack = p_d.stack
                        player:onPropStackChanged()
                    end
                end
            end
        end

        if desktop_preflop.list_player_state ~= nil then
            for i, v in pairs(desktop_preflop.list_player_state) do
                local player = self:_getActorMirror(v.player_guid)
                if player ~= nil then
                    player:playerStateChanged(v)
                end
            end
        end

        self.DealerSeatIndex = desktop_preflop.dealer_seat_index
        local seat_btn = self:getSeatByRealIndex(desktop_preflop.dealer_seat_index)
        if (seat_btn ~= nil) then
            seat_btn.player_texas:setIsBtn(true)
        end

        local seat_sb = self:getSeatByGuid(desktop_preflop.smallblind_guid)
        if (seat_sb ~= nil) then
            seat_sb.player_texas:setSmallBlind(desktop_preflop.small_blind)
        else
            print("小盲注玩家为空")
        end

        local seat_bb = self:getSeatByGuid(desktop_preflop.bigblind_guid)
        if (seat_bb ~= nil) then
            seat_bb.player_texas:setBigBlind(desktop_preflop.big_blind)
        else
            print("大盲注玩家为空")
        end

        local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
        local ev = view_mgr:GetEv("EvEntityDesktopPreFlopNotify")
        if (ev == nil) then
            ev = EvEntityDesktopPreFlopNotify:new(nil)
        end
        ev.pot_total = desktop_preflop.pot_total
        ev.list_pot = desktop_preflop.list_pot
        view_mgr:SendEv(ev)

        local card_first = nil
        local card_second = nil
        if (self.MeP ~= nil and desktop_preflop.first_card ~= nil and desktop_preflop.second_card ~= nil) then
            card_first = CS.Casinos.Card(desktop_preflop.first_card[1], desktop_preflop.first_card[2])
            card_second = CS.Casinos.Card(desktop_preflop.second_card[1], desktop_preflop.second_card[2])
            self.MeP:setHoleCards(card_first, card_second, false, false)
            table.insert(self.MeAllCard, card_first)
            table.insert(self.MeAllCard, card_second)
        end
    elseif (method_info.id == MethodTypeTexasDesktop.DesktopFlopNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        local desktop_flop_1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        local desktop_flop = DesktopNotifyDesktopFlopTexas:new(nil)
        desktop_flop:setData(desktop_flop_1)
        local t_playerchips_inpot = DesktopTexas:getChipEnterPotInfoByPlayer(desktop_flop.chip_enter_pot.list_seatchip_enter_pot)
        local bet_player_count = 0
        for k, v in pairs(self.AllSeat) do
            if (v.player_texas ~= nil) then
                local is_bet_golds = v.player_texas:roundEnd(t_playerchips_inpot[v.player_texas.Guid])
                if (is_bet_golds == true) then
                    bet_player_count = bet_player_count + 1
                end
            end
        end
        self.DesktopState = TexasDesktopState.Flop
        self.PotMain = desktop_flop.chip_enter_pot.pot_total
        self.ListPot = desktop_flop.chip_enter_pot.list_pot
        self.ListSeatChipEnterPot = desktop_flop.list_seatchip_enter_pot
        self.CurrentRountMaxBet = 0

        local first_card = CS.Casinos.Card(desktop_flop.first_card[1], desktop_flop.first_card[2])
        local second_card = CS.Casinos.Card(desktop_flop.second_card[1], desktop_flop.second_card[2])
        local third_card = CS.Casinos.Card(desktop_flop.third_card[1], desktop_flop.third_card[2])
        table.insert(self.CommunityCards, first_card)
        table.insert(self.CommunityCards, second_card)
        table.insert(self.CommunityCards, third_card)

        for k, v in pairs(self.AllSeat) do
            if (v.player_texas ~= nil) then
                v.player_texas:flop()
            end
        end

        local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
        local ev = view_mgr:GetEv("EvEntityDesktopFlopNotify")
        if (ev == nil) then
            ev = EvEntityDesktopFlopNotify:new(nil)
        end
        ev.first_card = first_card
        ev.second_card = second_card
        ev.third_card = third_card
        ev.bet_player_count = bet_player_count
        view_mgr:SendEv(ev)
    elseif (method_info.id == MethodTypeTexasDesktop.DesktopTurnNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        local desktop_turn1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        local desktop_turn = DesktopNotifyDesktopTurnTexas:new(nil)
        desktop_turn:setData(desktop_turn1)
        local t_playerchips_inpot = DesktopTexas:getChipEnterPotInfoByPlayer(desktop_turn.chip_enter_pot.list_seatchip_enter_pot)
        local bet_player_count = 0
        for k, v in pairs(self.AllSeat) do
            if (v.player_texas ~= nil) then
                local is_bet_golds = v.player_texas:roundEnd(t_playerchips_inpot[v.player_texas.Guid])
                if (is_bet_golds) then
                    bet_player_count = bet_player_count + 1
                end
            end
        end

        self.DesktopState = TexasDesktopState.Turn
        self.PotMain = desktop_turn.chip_enter_pot.pot_total
        self.ListPot = desktop_turn.chip_enter_pot.list_pot
        self.ListSeatChipEnterPot = desktop_turn.list_seatchip_enter_pot
        self.CurrentRountMaxBet = 0

        local turn_card = CS.Casinos.Card(desktop_turn.card[1], desktop_turn.card[2])
        table.insert(self.CommunityCards, turn_card)

        for k, v in pairs(self.AllSeat) do
            if (v.player_texas ~= nil) then
                v.player_texas:turn()
            end
        end

        local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
        local ev = view_mgr:GetEv("EvEntityDesktopTurnNotify")
        if (ev == nil) then
            ev = EvEntityDesktopTurnNotify:new(nil)
        end
        ev.turn_card = turn_card
        ev.bet_player_count = bet_player_count
        view_mgr:SendEv(ev)
    elseif (method_info.id == MethodTypeTexasDesktop.DesktopRiverNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        local desktop_river1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        local desktop_river = DesktopNotifyDesktopRiverTexas:new(nil)
        desktop_river:setData(desktop_river1)
        local t_playerchips_inpot = DesktopTexas:getChipEnterPotInfoByPlayer(desktop_river.chip_enter_pot.list_seatchip_enter_pot)
        local bet_player_count = 0
        for k, v in pairs(self.AllSeat) do
            if (v.player_texas ~= nil) then
                local is_bet_golds = v.player_texas:roundEnd(t_playerchips_inpot[v.player_texas.Guid])
                if (is_bet_golds) then
                    bet_player_count = bet_player_count + 1
                end
            end
        end
        self.DesktopState = TexasDesktopState.River
        self.PotMain = desktop_river.chip_enter_pot.pot_total
        self.ListPot = desktop_river.chip_enter_pot.list_pot
        self.ListSeatChipEnterPot = desktop_river.list_seatchip_enter_pot
        self.CurrentRountMaxBet = 0

        local river_card = CS.Casinos.Card(desktop_river.card[1], desktop_river.card[2])
        table.insert(self.CommunityCards, river_card)

        for k, v in pairs(self.AllSeat) do
            if (v.player_texas ~= nil) then
                v.player_texas:river()
            end
        end

        local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
        local ev = view_mgr:GetEv("EvEntityDesktopRiverNotify")
        if (ev == nil) then
            ev = EvEntityDesktopRiverNotify:new(nil)
        end
        ev.river_card = river_card
        ev.bet_player_count = bet_player_count
        view_mgr:SendEv(ev)
    elseif (method_info.id == MethodTypeTexasDesktop.DesktopGameEndNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        if self.DesktopState == TexasDesktopState.GameEnd then
            return
        end

        local desktop_gameend1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        local desktop_gameend = DesktopNotifyGameEndTexas:new(nil)
        desktop_gameend:setData(desktop_gameend1)

        if desktop_gameend.chip_enter_pot ~= nil then
            local t_playerchips_inpot = DesktopTexas:getChipEnterPotInfoByPlayer(desktop_gameend.chip_enter_pot.list_seatchip_enter_pot)
            self.LastPlayerTurn = ""
            for k, v in pairs(self.AllSeat) do
                if (v.player_texas ~= nil) then
                    v.player_texas:roundEnd(t_playerchips_inpot[v.player_texas.Guid])
                end
            end

            self.PotMain = desktop_gameend.chip_enter_pot.pot_total
            self.ListPot = desktop_gameend.chip_enter_pot.list_pot
            self.ListSeatChipEnterPot = desktop_gameend.list_seatchip_enter_pot
        end

        self.PlayerTurn = PlayerTurnDataTexas:new(nil)
        self.DesktopState = TexasDesktopState.GameEnd
        self.GameEnd = true
        self.ReBeginTm = desktop_gameend.rebegin_tm
        self.CurrentRountMaxBet = 0

        for i, v in pairs(desktop_gameend.list_allplayer_adddata) do
            local player = self:_getActorMirror(v.player_guid)
            if player ~= nil then
                player:addExpAndPoint(v.add_exp, v.add_point)
            end
        end

        for i, v in pairs(self.MapAllPlayer) do
            if v ~= nil then
                v:gameEnd()
            end
        end

        if desktop_gameend.list_player_state ~= nil then
            for i, v in pairs(desktop_gameend.list_player_state) do
                local player = self:_getActorMirror(v.player_guid)
                if player ~= nil then
                    if player.PlayerDataDesktop.DesktopPlayerState ~= v.state then
                        player:playerStateChanged(v)
                    end
                end
            end
        end

        if (desktop_gameend.list_winner ~= nil) then
            local sb = {}
            for i, v in pairs(desktop_gameend.list_winner) do
                local d = v
                local player = self.MapPlayerTexas[d.player_guid]
                if (player ~= nil) then
                    if d.win_chip > 0 then
                        player:isWinner(d.win_chip)
                    end
                    player.PlayerDataDesktop.Stack = d.stack
                end
            end

            local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
            local ev = view_mgr:GetEv("EvEntityDesktopGameEndNotifyTexas")
            if (ev == nil) then
                ev = EvEntityDesktopGameEndNotifyTexas:new(nil)
            end
            ev.list_winner = desktop_gameend.list_winner
            view_mgr:SendEv(ev)
        else
            print("GameEnd 赢家列表为空")
        end
    elseif (method_info.id == MethodTypeTexasDesktop.DesktopShowdownNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        local desktop_showdown1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        local desktop_showdown = DesktopNotifyDesktopShowdownTexas:new(nil)
        desktop_showdown:setData(desktop_showdown1)
        local t_playerchips_inpot = DesktopTexas:getChipEnterPotInfoByPlayer(desktop_showdown.chip_enter_pot.list_seatchip_enter_pot)
        self.AlreadyShowDown = true
        self.LastPlayerTurn = ""
        for k, v in pairs(self.AllSeat) do
            if (v.player_texas ~= nil) then
                v.player_texas:roundEnd(t_playerchips_inpot[v.player_texas.Guid])
            end
        end
        self.PotMain = desktop_showdown.chip_enter_pot.pot_total
        self.ListPot = desktop_showdown.chip_enter_pot.list_pot
        self.ListSeatChipEnterPot = desktop_showdown.list_seatchip_enter_pot
        for i, v in pairs(desktop_showdown.list_carddata_left) do
            local card = CS.Casinos.Card(v[1], v[2])
            table.insert(self.CommunityCards, card)
        end

        local l_pc = desktop_showdown.list_playerholecard
        if (l_pc ~= nil) then
            local l_c = #l_pc
            if (l_c > 0) then
                for i, v in pairs(l_pc) do
                    local d = v
                    local player_texas = self.MapPlayerTexas[d.player_guid]
                    if (player_texas ~= nil) then
                        if (player_texas.IsMe == false) then
                            player_texas:setHoleCards(CS.Casinos.Card(d.first_card[1], d.first_card[2]),
                                    CS.Casinos.Card(d.second_card[1], d.second_card[2]), false, false)
                        end

                        player_texas:showDown(desktop_showdown.list_carddata_left)
                    end
                end
            end

            local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
            local ev = view_mgr:GetEv("EvEntityDesktopShowdownNotify")
            if (ev == nil) then
                ev = EvEntityDesktopShowdownNotify:new(nil)
            end
            ev.desktop_showdown = desktop_showdown
            view_mgr:SendEv(ev)
        else
            print("Showdown 赢家列表为空")
        end
    elseif (method_info.id == MethodTypeTexasDesktop.PlayerStateChangeNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        local player_state_change1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        local player_state_change = DesktopNotifyPlayerStateChangeTexas:new(nil)
        player_state_change:setData(player_state_change1)
        if (player_state_change.list_playerstate ~= nil) then
            for i, v in pairs(player_state_change.list_playerstate) do
                local player_texas = self.MapPlayerTexas[v.player_guid]
                if (player_texas == nil) then
                    return
                end
                player_texas:playerStateChanged(v)
            end
        end
    elseif (method_info.id == MethodTypeTexasDesktop.PlayerActionNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        local player_action1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        local player_action = DesktopNotifyPlayerActionTexas:new(nil)
        player_action:setData(player_action1)

        local seat = self:getSeatByGuid(player_action.player_guid)
        if (seat ~= nil) then
            if (seat.player_texas.Guid == self.PlayerTurn.player_guid) then
                self.LastPlayerTurn = self.PlayerTurn.player_guid
                self.PlayerTurn.player_guid = ""
            end
            seat.player_texas:playerAction(player_action)
        end
    elseif (method_info.id == MethodTypeTexasDesktop.PlayerGetTurnNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        local player_turn1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        local player_turn = DesktopNotifyPlayerGetTurnTexas:new(nil)
        player_turn:setData(player_turn1)

        self:_setPlayerTurn(player_turn.player_turn, self.ActionWaitingTm, false)
    elseif (method_info.id == MethodTypeTexasDesktop.PlayerShowCardResponse) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        local show_card1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        if self.MeP ~= nil then
            self.MeP:setShowCardState(show_card1)
        end

    elseif (method_info.id == MethodTypeTexasDesktop.PlayerShowCardStateNotify) then
        if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
            return
        end

        local show_card1 = self.ControllerDesktop.ControllerMgr:UnpackData(method_info.data)
        local show_card = TexasDesktopNotifyPlayerShowCard:new(nil)
        show_card:setData(show_card1)
        for i, v in pairs(show_card.list_showcard) do
            local player = self:_getActorMirror(v.player_guid)
            if player ~= nil then

                local card1 = nil
                if v.first_card ~= nil then
                    card1 = CS.Casinos.Card(v.first_card[1], v.first_card[2])
                end
                local card2 = nil
                if v.second_card ~= nil then
                    card2 = CS.Casinos.Card(v.second_card[1], v.second_card[2])
                end
                player:setShowCardData(card1, card2)
            end
        end
    end
end

---------------------------------------
function DesktopTexas:DesktopPlayerGiftChangeNotify(data)
    if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
        return
    end
    local change_gift = DesktopNotifyPlayerCurrentGiftChange:new(nil)
    change_gift.player_guid = data[1]
    local i_data = data[2]
    local item_data = ItemData1:new(nil)
    item_data:setData(i_data)
    change_gift.item_data = item_data
    local player_texas = self.MapPlayerTexas[change_gift.player_guid]
    if (player_texas ~= nil) then
        player_texas:setGift(change_gift.item_data, false)
    end
end

---------------------------------------
function DesktopTexas:DesktopBuyAndSendItemNotify(data)
    if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
        return
    end
    local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
    local ui_desk = view_mgr:GetView("DesktopTexas")
    ui_desk:playerBuyAndSendItem(data)
end

---------------------------------------
function DesktopTexas:DesktopChat(msg)
    if (CS.System.String.IsNullOrEmpty(self.DesktopGuid)) then
        return
    end

    local lock_chat = self.MapSeatPlayerChatIsLock[msg.sender_guid]
    local player_texas = self.MapPlayerTexas[msg.sender_guid]
    if (player_texas ~= nil) then
        if (lock_chat == false or (self:getSeatByGuid(msg.sender_guid) == nil and self.LockSpectatorChat == false)) then
            self.ControllerDesktop:addDesktopMsg(msg.sender_guid, player_texas.PlayerDataDesktop.NickName,
                    msg.sender_viplevel, msg.msg)
            self.CurrentUnSendDesktopMsg = ""
        end

        player_texas:desktopChat(msg)
    end
end

---------------------------------------
function DesktopTexas:getAllValidPlayer()
    local list_player = {}
    for k, v in pairs(self.AllSeat) do
        if (v.player_texas ~= nil) then
            list_player[k] = v.player_texas
        end
    end

    return list_player
end

---------------------------------------
function DesktopTexas:playerRaise()
    local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
    local ev = view_mgr:GetEv("EvUiPotMainChanged")
    if (ev == nil) then
        ev = EvUiPotMainChanged:new(nil)
    end
    ev.pot_mian = self.PotMain
    view_mgr:SendEv(ev)
end

---------------------------------------
function DesktopTexas:requestPlayerWaitWhile()
    self.ControllerDesktop:RequestPlayerWaitWhile()
end

---------------------------------------
function DesktopTexas:requestPlayerReturn(chip)
    self.ControllerDesktop:RequestPlayerReturn(chip)
end

---------------------------------------
function DesktopTexas:requestPlayerOb()
    self.ControllerDesktop:RequestPlayerOb()
end

---------------------------------------
function DesktopTexas:requestPlayerSitdown(sitdown_info)
    ViewHelper:UiBeginWaiting(self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr:getLanValue("Seating"))
    self.ControllerDesktop:RequestPlayerSitdown(sitdown_info)
end

---------------------------------------
function DesktopTexas:requestPlayerPushStack(chip)
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = MethodTypeTexasDesktop.PlayerPushStackRequest
    method_info.data = self.ControllerDesktop.ControllerMgr:PackData(chip)

    self:_userRequest(method_info)
end

---------------------------------------
function DesktopTexas:requestPlayerAutoAction(auto_action)
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = MethodTypeTexasDesktop.PlayerAutoActionRequest
    method_info.data = self.ControllerDesktop.ControllerMgr:PackData(auto_action)

    self:_userRequest(method_info)
end

---------------------------------------
function DesktopTexas:requestPlayerCancelAutoAction()
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = MethodTypeTexasDesktop.PlayerCancelAutoActionRequest
    method_info.data = nil

    self:_userRequest(method_info)
end

---------------------------------------
function DesktopTexas:requestPlayerActionBet(chip)
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = MethodTypeTexasDesktop.PlayerActionBetRequest
    method_info.data = self.ControllerDesktop.ControllerMgr:PackData(chip)

    self:_userRequest(method_info)
end

---------------------------------------
function DesktopTexas:requestPlayerActionFold()
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = MethodTypeTexasDesktop.PlayerActionFoldRequest
    method_info.data = nil

    self:_userRequest(method_info)
end

---------------------------------------
function DesktopTexas:requestPlayerActionCheck()
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = MethodTypeTexasDesktop.PlayerActionCheckRequest
    method_info.data = nil

    self:_userRequest(method_info)
end

---------------------------------------
function DesktopTexas:requestPlayerActionCall()
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = MethodTypeTexasDesktop.PlayerActionCallRequest
    method_info.data = nil

    self:_userRequest(method_info)
end

---------------------------------------
function DesktopTexas:requestPlayerActionRaise(chip)
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = MethodTypeTexasDesktop.PlayerActionRaiseRequest
    method_info.data = self.ControllerDesktop.ControllerMgr:PackData(chip)

    self:_userRequest(method_info)
end

---------------------------------------
function DesktopTexas:requestPlayerActionReRaise(chip)
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = MethodTypeTexasDesktop.PlayerActionReRaiseRequest
    method_info.data = self.ControllerDesktop.ControllerMgr:PackData(chip)

    self:_userRequest(method_info)
end

---------------------------------------
function DesktopTexas:requestPlayerActionAllIn()
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = MethodTypeTexasDesktop.PlayerActionAllInRequest
    method_info.data = nil

    self:_userRequest(method_info)
end

---------------------------------------
function DesktopTexas:requestShowCard(card_state)
    local method_info = MethodInfoTexasDesktop:new(nil)
    method_info.id = MethodTypeTexasDesktop.PlayerShowCardRequest
    method_info.data = self.ControllerDesktop.ControllerMgr:PackData(card_state)

    self:_userRequest(method_info)
end

---------------------------------------
function DesktopTexas:getChipEnterPotInfoByPlayer(list_playerchip_enter_pot)
    local t_playerchips_inpot = {}
    if list_playerchip_enter_pot ~= nil then
        for i, v in pairs(list_playerchip_enter_pot) do
            for j, j_v in pairs(v) do
                local player = j_v
                local t_player_inpot = t_playerchips_inpot[player]
                if t_player_inpot == nil then
                    t_player_inpot = {}
                    t_playerchips_inpot[player] = t_player_inpot
                end
                table.insert(t_player_inpot, i)
            end
        end
    end

    return t_playerchips_inpot
end

---------------------------------------
function DesktopTexas:isValidSeatIndex(seat_index)
    if (seat_index < 0 or seat_index >= self.SeatNum) then
        return false
    else
        return true
    end
end

---------------------------------------
-- 座位索引映射后的动画播放
function DesktopTexas:changeOtherPlayerUiSeatIndex()
    for k, v in pairs(self.AllSeat) do
        if (v.player_texas ~= nil and v.player_texas ~= self.MeP) then
            v.player_texas:changeUiSeatIndex()
        end
    end

    local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
    local ui_desk = view_mgr:GetView("DesktopTexas")
    if (ui_desk ~= nil) then
        local all_player = self:getAllValidPlayer()
        ui_desk:setCurrentSeatActor(all_player)
    end
end

---------------------------------------
function DesktopTexas:getSeatByGuid(et_player_guid)
    for k, v in pairs(self.AllSeat) do
        if (v.player_texas ~= nil and v.player_texas.Guid == et_player_guid) then
            return v
        end
    end

    return nil
end

---------------------------------------
function DesktopTexas:getSeatByIndex(seat_index_cur)
    if (self:isValidSeatIndex(seat_index_cur) == false) then
        return nil
    end

    return self.AllSeat[seat_index_cur]
end

---------------------------------------
function DesktopTexas:getSeatByRealIndex(seat_index_real)
    if (self:isValidSeatIndex(seat_index_real) == false) then
        return nil
    end

    local seat_info = nil
    for i, v in pairs(self.AllSeat) do
        if v.player_texas ~= nil and v.player_texas.PlayerDataDesktop.SeatIndex == seat_index_real then
            seat_info = v
            break
        end
    end

    return seat_info
end

---------------------------------------
function DesktopTexas:getNextSeat(et_player_guid_cur)
    local seat_cur = nil
    for k, v in pairs(self.AllSeat) do
        if (v.player_texas ~= nil and v.player_texas.Guid == et_player_guid_cur) then
            seat_cur = v
            break
        end
    end

    if (seat_cur == nil) then
        return nil
    end

    local l = getNextSeat(seat_cur.index)
    return l
end

---------------------------------------
function DesktopTexas:getNextSeat(seat_index_cur)
    seat_index_cur = seat_index_cur + 1
    if (seat_index_cur >= self.SeatNum) then
        seat_index_cur = 0
    end
    return self.AllSeat[seat_index_cur]
end

---------------------------------------
function DesktopTexas:getPlayerCount()
    local player_num = 0
    for k, v in pairs(self.AllSeat) do
        if (v.player_texas ~= nil) then
            player_num = player_num + 1
        end
    end
    return player_num
end

---------------------------------------
-- genUiPlayerInfo
function DesktopTexas:getPlayerInfo()
    local player_info = nil
    local l = #self.QueUiPlayerInfo
    if (l > 0) then
        player_info = table.remove(self.QueUiPlayerInfo, 1)
    else
        player_info = self.ControllerDesktop.ControllerMgr.ViewMgr:CreateView("DesktopPlayerInfoTexas")
    end
    return player_info
end

---------------------------------------
function DesktopTexas:releaseUiPlayerInfo(player_info)
    player_info:release()
    table.insert(self.QueUiPlayerInfo, player_info)
end

---------------------------------------
-- 换筹码对话框
function DesktopTexas:createBetGame(action, bet_min, bet_max)
    local ui_bet_game = self.ControllerDesktop.ControllerMgr.ViewMgr:CreateView("ChipOperate")
    local bet_maxchips = bet_max
    local bet_minchips = bet_min
    ui_bet_game:setChipsInfo(self.ControllerActor.PropGoldAcc:get(),
            bet_maxchips, bet_minchips, CS.Casinos._eChipOperateType.BetGame, nil, action)
end

---------------------------------------
function DesktopTexas:_getPlayerTexas()
    local player = nil
    local l = #self.QuePlayerTexas
    if (l > 0) then
        player = table.remove(self.QuePlayerTexas, 1)
    else
        player = DesktopPlayerTexas:new(nil, self)
    end

    return player
end

---------------------------------------
function DesktopTexas:_releasePlayerTexas(player)
    player:Destroy()
    table.insert(self.QuePlayerTexas, player)
end

---------------------------------------
function DesktopTexas:_setPlayerTurn(turn_data, auto_show_operate)
    if (turn_data == nil) then
        return
    end
    if (turn_data ~= nil) then
        local l_v_a = turn_data.player_turn_lefttm
        if (l_v_a == 0) then
            return
        end
    end
    self.PlayerTurn.player_guid = turn_data.player_guid

    if (self.MeP ~= nil) then
        local is_mechange = false
        local is_me = false
        if self.LastPlayerTurn == self.MeP.Guid then
            is_mechange = true
        end
        if turn_data.player_guid == self.MeP.Guid then
            is_me = true
        end
        self.MeP:playerChangeTurn(is_mechange, is_me)
    end

    for k, v in pairs(self.AllSeat) do
        if (v.player_texas ~= nil) then
            if (v.player_texas.Guid == turn_data.player_guid) then
                v.player_texas:playerGetTurn(turn_data, auto_show_operate)
            end
        end
    end
end

---------------------------------------
function DesktopTexas:_getActorMirror(player_guid)
    return self.MapPlayerTexas[player_guid]
end

---------------------------------------
function DesktopTexas:_initActorMirror(player_data, is_me)
    if player_data ~= nil then
        local player_texas = self:_getPlayerTexas()
        player_texas:SetPlayerData(player_data, self.DesktopState)
        self.MapPlayerTexas[player_texas.Guid] = player_texas
        self.MapAllPlayer[player_texas.Guid] = player_texas

        local seat_index = player_texas.PlayerDataDesktop.SeatIndex
        if (self:isValidSeatIndex(seat_index)) then
            self.AllSeat[seat_index].player_texas = player_texas
            self.MapSeatPlayerChatIsLock[player_texas.Guid] = false
        end
    end
end

---------------------------------------
function DesktopTexas:_leaveDesktop(player_texas)
    self.MapPlayerTexas[player_texas.Guid] = nil
    self.MapAllPlayer[player_texas.Guid] = nil
    local content = string.format("%s%s", player_texas.PlayerDataDesktop.NickName, self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("LeaveDesk"))--CS.Casinos.CasinosContext.Instance:AppendStrWithSB(player_texas.PlayerDataDesktop.PlayerInfoCommon.NickName, "离开桌子")
    self.ControllerDesktop:addDesktopMsg("", self.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("TheDealer"), 0, content)

    for k, v in pairs(self.AllSeat) do
        if (v.player_texas ~= nil and v.player_texas.Guid == player_texas.Guid) then
            v.player_texas = nil
            break
        end
    end

    self:_releasePlayerTexas(player_texas)
end

---------------------------------------
function DesktopTexas:_userRequest(method_info)
    self.ControllerDesktop:UserRequest("Texas", method_info:getData4Pack())
end

---------------------------------------
function DesktopTexas:regDesktopTypeBaseFactory(desktop_fac)
    self.MapDesktopTypeBaseFac[desktop_fac:GetName()] = desktop_fac
end

---------------------------------------
function DesktopTexas:GetDesktopTypeBaseFactory(fac_name)
    return self.MapDesktopTypeBaseFac[fac_name]
end

---------------------------------------
DesktopTexasFactory = DesktopBaseFactory:new(nil)

---------------------------------------
function DesktopTexasFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o
end

---------------------------------------
function DesktopTexasFactory:GetName()
    return "Texas"
end

---------------------------------------
function DesktopTexasFactory:CreateDesktop(co_mgr)
    local l = DesktopTexas:new(nil, co_mgr)
    l:OnCreate()
    return l
end