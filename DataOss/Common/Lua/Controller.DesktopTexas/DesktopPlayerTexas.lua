-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
DesktopPlayerTexas = {}

---------------------------------------
function DesktopPlayerTexas:new(o, desktop)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.DesktopTexas = desktop
    o.UiDesktopPlayerInfo = nil
    o.UiSeatIndex = nil
    o.Guid = nil
    o.IsInGame = nil
    o.GUID_KEY = "EtGuid"
    o.PlayerOperate = nil
    o.IsMe = nil -- 是否是本人
    o.FirstCard = nil
    o.SecondCard = nil
    o.IsFold = false --{ get { return self.PlayerDataDesktop.PlayerData.PlayerActionType == PlayerActionTypeTexas.Fold } }-- 玩家是否已盖牌
    o.UiMiddleSeatIndexRealIndex = nil
    o.PlayerDataDesktop = nil
    o.IsCanCheckChipIfNotEnough = nil
    o.IsGameEnd = nil
    o.GameEndCheckChipTime = nil
    o.WaitWhileTm = nil
    return o
end

---------------------------------------
function DesktopPlayerTexas:SetPlayerData(player_data, desktop_state)
    self.IsCanCheckChipIfNotEnough = false
    self.GameEndCheckChipTime = 0
    self.UiDesktopPlayerInfo = nil
    self.IsGameEnd = desktop_state == TexasDesktopState.GameEnd
    local p_d = PlayerDataDesktopTexas:new(nil)
    p_d:setData(self.DesktopTexas.ControllerDesktop.ControllerMgr.RPC.MessagePack, player_data)
    self.PlayerDataDesktop = p_d
    if self.DesktopTexas.CurrentRountMaxBet < p_d.CurrentRoundBet then
        self.DesktopTexas.CurrentRountMaxBet = p_d.CurrentRoundBet
    end
    self.Guid = self.PlayerDataDesktop.PlayerGuid
    self.IsMe = (self.PlayerDataDesktop.PlayerGuid == self.DesktopTexas.ControllerPlayer.Guid)
    self.WaitWhileTm = self.PlayerDataDesktop.WaitWhileTime
    self.OperateSound = {}
    local gender = "boy"
    if p_d.Gender == true then
        gender = "boy"
    else
        gender = "girl"
    end
    self.OperateSound[PlayerActionTypeTexas.Fold] = string.format("fold_%s", gender)
    self.OperateSound[PlayerActionTypeTexas.Call] = string.format("call_%s", gender)
    self.OperateSound[PlayerActionTypeTexas.Raise] = string.format("raise_%s", gender)
    self.OperateSound[PlayerActionTypeTexas.AllIn] = string.format("allin_%s", gender)

    if (self.IsMe) then
        self.DesktopTexas.MePlayer = self
        self.DesktopTexas.MeP = self
        if (self.DesktopTexas:isValidSeatIndex(self.PlayerDataDesktop.SeatIndex)) then
            self.UiSeatIndex = 4 -- math.floor(self.DesktopTexas.SeatNum / 2)
            --if (self.DesktopTexas.SeatNum == 5)
            --then
            --    self.UiSeatIndex = (self.UiSeatIndex * 2)
            --end
            self.UiMiddleSeatIndexRealIndex = self.PlayerDataDesktop.SeatIndex
        else
            self.UiMiddleSeatIndexRealIndex = 255
            self.UiSeatIndex = 255
        end

        self.PlayerOperate = self.DesktopTexas.ControllerPlayer.ControllerMgr.ViewMgr:CreateView("DesktopPlayerOperateTexas")
        self.PlayerOperate:setMeActorMirror(self)
    else
        self:_setUiSeatIndex()
    end

    self:_createUiPlayerInfo()
    self:_checkActorState()
    self:_checkActorAction(true)

    if self.IsMe then
        self:mttAutoAction(self.DesktopTexas.DesktopTypeBase.ServerAutoAction)
    end
end

---------------------------------------
function DesktopPlayerTexas:Destroy()
    if (self.UiDesktopPlayerInfo ~= nil) then
        self.DesktopTexas:releaseUiPlayerInfo(self.UiDesktopPlayerInfo)
        self.UiDesktopPlayerInfo = nil
    end

    local view_mgr = self.DesktopTexas.ControllerPlayer.ControllerMgr.ViewMgr
    local ui_desk = view_mgr:GetView("DesktopTexas")
    if (ui_desk ~= nil) then
        ui_desk:PlayerLeave(self.UiSeatIndex)
    end

    if self.PlayerOperate ~= nil then
        view_mgr:DestroyView(self.PlayerOperate)
        self.PlayerOperate = nil
    end

    self.FirstCard = nil
    self.SecondCard = nil
    self.UiSeatIndex = 255
    self.UiMiddleSeatIndexRealIndex = 255
    self.PlayerDataDesktop = nil
    self.IsGameEnd = false
    self.ShowCardState = TexasPlayerShowCardState.None
end

---------------------------------------
function DesktopPlayerTexas:Update(elapsed_tm)
    if (self.IsMe) then
        if (self.IsGameEnd and self.GameEndCheckChipTime > 0) then
            self.GameEndCheckChipTime = self.GameEndCheckChipTime - elapsed_tm
            if (self.GameEndCheckChipTime < 0) then
                self.IsCanCheckChipIfNotEnough = true
                self.IsGameEnd = false
                self.GameEndCheckChipTime = 0
            end
        end

        if (self.IsCanCheckChipIfNotEnough and (self.PlayerDataDesktop.DesktopPlayerState ~= TexasDesktopPlayerState.Ob)) then
            self.IsCanCheckChipIfNotEnough = false

            self.DesktopTexas.DesktopTypeBase:checkMeStack(self.PlayerDataDesktop.Stack)
        end
    end

    if (self.PlayerDataDesktop.DesktopPlayerState == TexasDesktopPlayerState.WaitWhile) then
        self.WaitWhileTm = self.WaitWhileTm + elapsed_tm
        local back_tm = self.DesktopTexas.WaitWhileTm - self.WaitWhileTm
        local tm = math.ceil(back_tm)
        if (self.PlayerOperate ~= nil) then
            self.PlayerOperate:setLeaveWhileTips(tm)
        end

        if back_tm > 0 then
            if self.IsMe then
                self.UiDesktopPlayerInfo:setLeaveWhileTime(back_tm)
            end
        else
            self.UiDesktopPlayerInfo:setLeaveWhileTime(back_tm)
        end
    end
end

---------------------------------------
function DesktopPlayerTexas:deskIdle()
    self.IsGameEnd = false
    self.PlayerDataDesktop.CurrentTotalBet = 0

    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:deskIdle()
    end
end

---------------------------------------
function DesktopPlayerTexas:preflopBegin()
    self.PlayerDataDesktop.PlayerActionType = PlayerActionTypeTexas.None
    self.ShowCardState = TexasPlayerShowCardState.None
    self.PlayerDataDesktop.CurrentTotalBet = 0
    self.PlayerDataDesktop.CurrentRoundBet = 0
    self.IsGameEnd = false
    self.FirstCard = nil
    self.SecondCard = nil

    if (self.PlayerOperate ~= nil) then
        self.PlayerOperate:preflopBegin()
    end

    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:preflopBegin()
    end
end

---------------------------------------
function DesktopPlayerTexas:flop()
    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:flop()
    end
end

---------------------------------------
function DesktopPlayerTexas:turn()
    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:turn()
    end
end

---------------------------------------
function DesktopPlayerTexas:river()
    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:river()
    end
end

---------------------------------------
function DesktopPlayerTexas:roundEnd(t_playerchips_inpot)
    local lastround_betgolds = self.PlayerDataDesktop.CurrentRoundBet

    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:roundEnd(t_playerchips_inpot)
    end

    self.PlayerDataDesktop.CurrentRoundBet = 0

    return lastround_betgolds > 0
end

---------------------------------------
function DesktopPlayerTexas:setShowCardState(showcard_state)
    self.ShowCardState = showcard_state
    if self.UiDesktopPlayerInfo ~= nil then
        self.UiDesktopPlayerInfo:setShowCardState(showcard_state)
    end
end

---------------------------------------
function DesktopPlayerTexas:setShowCardData(first_card, second_card)
    if self.UiDesktopPlayerInfo ~= nil then
        self.UiDesktopPlayerInfo:setShowCardData(first_card, second_card)
    end
end

---------------------------------------
function DesktopPlayerTexas:setIsBtn(is_btn)
    if (self.UiDesktopPlayerInfo ~= nil)
    then
        self.UiDesktopPlayerInfo:setIsBtn(is_btn)
    end
end

---------------------------------------
function DesktopPlayerTexas:setSmallBlind(small_blind)
    self.PlayerDataDesktop.DesktopPlayerState = TexasDesktopPlayerState.InGame
    self.PlayerDataDesktop.PlayerActionType = PlayerActionTypeTexas.Bet
    self.PlayerDataDesktop.CurrentRoundBet = small_blind

    if small_blind > 0 then
        self:_raiseChip(small_blind, false)
    end

    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:setSmallBlind()
    end
end

---------------------------------------
function DesktopPlayerTexas:setBigBlind(big_blind)
    self.PlayerDataDesktop.DesktopPlayerState = TexasDesktopPlayerState.InGame
    self.PlayerDataDesktop.PlayerActionType = PlayerActionTypeTexas.Bet
    self.PlayerDataDesktop.CurrentRoundBet = big_blind

    if big_blind > 0 then
        self:_raiseChip(big_blind, true)
    end

    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:setBigBlind()
    end
end

---------------------------------------
--function DesktopPlayerTexas:setMustBet(bet_chips, is_lastperson)
--    self.PlayerDataDesktop.DesktopPlayerState = TexasDesktopPlayerState.InGame
--    self.PlayerDataDesktop.PlayerActionType = PlayerActionTypeTexas.Bet
--    self.PlayerDataDesktop.CurrentRoundBet = bet_chips
--
--    self:_raiseChip(bet_chips, is_lastperson)
--end

---------------------------------------
function DesktopPlayerTexas:setHoleCards(first_card, second_card, show_card, is_init)
    self.FirstCard = first_card
    self.SecondCard = second_card

    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:setCard(show_card, self.FirstCard, self.SecondCard, is_init)
    end
end

---------------------------------------
function DesktopPlayerTexas:showDown(carddata_left)
    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:showDown(self.FirstCard, self.SecondCard, carddata_left)
    end

    if (self.PlayerOperate ~= nil) then
        self.PlayerOperate:showDown()
    end
end

---------------------------------------
function DesktopPlayerTexas:isWinner(win_chips)
    if (self.UiDesktopPlayerInfo ~= nil) then
        --self.UiDesktopPlayerInfo:showWinnerCard()
        self.UiDesktopPlayerInfo:showWinnerHandType(win_chips)
    end
end

---------------------------------------
function DesktopPlayerTexas:gameEnd()
    -- 切换到暂离状态检查是否需要购买筹码
    self.ShowCardState = TexasPlayerShowCardState.None
    if (self.IsMe) then
        self.GameEndCheckChipTime = self.DesktopTexas.ReBeginTm - 3.7
    end

    self.IsGameEnd = true

    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:gameEnd()
    end

    if (self.PlayerOperate ~= nil) then
        self.PlayerOperate:gameEnd()
    end

    self:onPropStackChanged()
end

---------------------------------------
function DesktopPlayerTexas:addExpAndPoint(exp, point)
    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:addExpAndPoint(exp, point)
    end
end

---------------------------------------
function DesktopPlayerTexas:playerStateChanged(player_state_data)
    if (player_state_data.state ~= TexasDesktopPlayerState.WaitWhile) then
        self.WaitWhileTm = 0
    end
    self.PlayerDataDesktop.DesktopPlayerState = player_state_data.state
    self.PlayerDataDesktop.PlayerActionType = player_state_data.action
    self:_checkActorState()
    self:_checkActorAction(false)
end

---------------------------------------
function DesktopPlayerTexas:playerAction(player_action)
    self.PlayerDataDesktop.CurrentRoundBet = player_action.current_round_bet
    self.PlayerDataDesktop.CurrentTotalBet = player_action.current_total_bet
    self.PlayerDataDesktop.Stack = player_action.current_stack
    self.PlayerDataDesktop.DesktopPlayerState = player_action.state
    self.PlayerDataDesktop.PlayerActionType = player_action.action
    self.DesktopTexas.PotMain = player_action.pot_total

    if (player_action.current_bet_chip > 0) then
        self:_raiseChip(player_action.current_bet_chip, false)
    end
    local casinos_context = CS.Casinos.CasinosContext.Instance
    if self.PlayerDataDesktop.PlayerActionType ~= PlayerActionTypeTexas.Check then
        casinos_context:Play(self.OperateSound[self.PlayerDataDesktop.PlayerActionType], CS.Casinos._eSoundLayer.LayerNormal)
        if self.PlayerDataDesktop.PlayerActionType == PlayerActionTypeTexas.AllIn then
            casinos_context:Play("allin_fire", CS.Casinos._eSoundLayer.LayerNormal)
            if self.UiDesktopPlayerInfo ~= nil then
                self.UiDesktopPlayerInfo:allIn()
            end
        end
    end
    self:_checkActorAction(false)
    self:onPropStackChanged()
end

---------------------------------------
function DesktopPlayerTexas:playerGetTurn(turn_data, auto_show_operate)
    if self.IsGameEnd then
        return
    end

    if (self.UiDesktopPlayerInfo ~= nil) then
        if (turn_data ~= nil) then
            self.UiDesktopPlayerInfo:playerTurn(true, turn_data.player_turn_lefttm)
        else
            self.UiDesktopPlayerInfo:playerTurn(false, turn_data.player_turn_lefttm)
        end
    end

    if (self == self.DesktopTexas.MePlayer and turn_data ~= nil) then
        if (self.PlayerOperate ~= nil) then
            local t_fastbet = self.DesktopTexas.TFastBetInfoNotPreFlop
            if self.DesktopTexas.DesktopState == TexasDesktopState.PreFlop then
                t_fastbet = self.DesktopTexas.TFastBetInfoPreFlop
            end

            for i, v in pairs(t_fastbet) do
                local need_stack = v.DesktopFastBetValue
                if v.DesktopFastBetType == DesktopFastBetType.BigBlindMuilty then
                    need_stack = need_stack * self.DesktopTexas.DesktopTypeBase.BigBlind
                elseif v.DesktopFastBetType == DesktopFastBetType.BetPotMuilty then
                    need_stack = need_stack * self.DesktopTexas.PotMain
                end

                local big_stack = true
                local raise_chip = self.DesktopTexas.CurrentRountMaxBet * 2 - self.PlayerDataDesktop.CurrentRoundBet
                if need_stack < self.PlayerDataDesktop.Stack or need_stack < raise_chip then
                    big_stack = false
                end
                v.BigThanStack = big_stack
                v.NeedBetValue = math.ceil(need_stack)
            end

            local call_chip = self:getCallChip()
            self.PlayerOperate:isMeTurn(turn_data, auto_show_operate, t_fastbet, call_chip)
        end
    end
end

---------------------------------------
function DesktopPlayerTexas:playerOb()
    self.FirstCard = nil
    self.SecondCard = nil
    self.PlayerDataDesktop.Stack = 0
    self.PlayerDataDesktop.SeatIndex = 255
    self.PlayerDataDesktop.DesktopPlayerState = TexasDesktopPlayerState.Ob
    self:_checkActorState()
end

---------------------------------------
function DesktopPlayerTexas:playerSitdown(seat_index, stack, state, action)
    self.PlayerDataDesktop.SeatIndex = seat_index
    self.PlayerDataDesktop.Stack = stack
    self.PlayerDataDesktop.DesktopPlayerState = state
    self.PlayerDataDesktop.PlayerActionType = action

    CS.Casinos.CasinosContext.Instance:Play("sit", CS.Casinos._eSoundLayer.LayerNormal)
    if (self.IsMe) then
        self.UiSeatIndex = 4 -- math.floor(self.DesktopTexas.SeatNum / 2)
        --if (self.DesktopTexas.SeatNum == 5)
        --then
        --    self.UiSeatIndex = (self.UiSeatIndex * 2)
        --end
        self.UiMiddleSeatIndexRealIndex = self.PlayerDataDesktop.SeatIndex
        self.DesktopTexas:changeOtherPlayerUiSeatIndex()
        ViewHelper:UiEndWaiting()
    else
        self:_setUiSeatIndex()
    end

    self:_createUiPlayerInfo()
    self:_checkActorState()
end

---------------------------------------
function DesktopPlayerTexas:playerWaitWhile()
    self.PlayerDataDesktop.WaitWhileTime = 0
    self.PlayerDataDesktop.DesktopPlayerState = TexasDesktopPlayerState.WaitWhile
    self.FirstCard = nil
    self.SecondCard = nil
    self:_checkActorState()
end

---------------------------------------
function DesktopPlayerTexas:playerReturn(stack, state, action)
    self.PlayerDataDesktop.Stack = stack
    self.PlayerDataDesktop.DesktopPlayerState = state
    self.PlayerDataDesktop.PlayerActionType = action
    self.FirstCard = nil
    self.SecondCard = nil
    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:playerReturn()
    end
    self:_checkActorAction(true)
    self:_checkActorState()
end

---------------------------------------
function DesktopPlayerTexas:pushStack(stack)
    self.PlayerDataDesktop.Stack = stack
    self:onPropStackChanged()
end

---------------------------------------
function DesktopPlayerTexas:rebuyOrAddOn(score)
    self.PlayerDataDesktop.Stack = score
    self:onPropStackChanged()
end

---------------------------------------
function DesktopPlayerTexas:mttAutoAction(auto_action)
    local view_mgr = self.DesktopTexas.ControllerPlayer.ControllerMgr.ViewMgr
    local ev = view_mgr:GetEv("EvEntitySelfAutoActionChange")
    if (ev == nil) then
        ev = EvEntitySelfAutoActionChange:new(nil)
    end
    ev.is_autoaction = auto_action
    view_mgr:SendEv(ev)
end

---------------------------------------
-- 桌子内聊天广播
function DesktopPlayerTexas:desktopChat(msg)
end

---------------------------------------
function DesktopPlayerTexas:SetGift(player_gift, is_sendgift)
    self.PlayerDataDesktop.CurrentGiftItemData = player_gift

    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:SetGift(player_gift, self.PlayerDataDesktop.PlayerGuid, is_sendgift)
    end
end

---------------------------------------
function DesktopPlayerTexas:SendMagicExpression(exp)
    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:SendMagicExpression(exp)
    end
end

---------------------------------------
function DesktopPlayerTexas:changeUiSeatIndex()
    self:_setUiSeatIndex()
    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:playerChangeSeat()
    end
end

---------------------------------------
function DesktopPlayerTexas:playerChangeTurn(is_mechange, is_me)
    if (self.PlayerOperate ~= nil) then
        local call_chip = self:getCallChip()
        self.PlayerOperate:playerTurnChanged(call_chip, is_mechange, is_me)
    end
end

---------------------------------------
--function DesktopPlayerTexas:reset(player_data)
--    self.PlayerDataDesktop = CS.Casinos.LuaHelper.getPlayerDataDesktopTexas(player_data)
--    if (self.UiDesktopPlayerInfo ~= nil)
--    then
--        self.UiDesktopPlayerInfo:Reset()
--    end
--end

---------------------------------------
function DesktopPlayerTexas:getCallChip()
    local max_bet = self.DesktopTexas.CurrentRountMaxBet
    local big_blind = self.DesktopTexas.DesktopTypeBase.BigBlind
    if max_bet ~= 0 and max_bet < big_blind then
        max_bet = big_blind
    end
    local call_chip = max_bet - self.PlayerDataDesktop.CurrentRoundBet
    return call_chip
end

---------------------------------------
function DesktopPlayerTexas:onPropStackChanged()
    if (self.UiDesktopPlayerInfo ~= nil and self.IsGameEnd == false) then
        self.UiDesktopPlayerInfo:playerStackChange()
    end
end

---------------------------------------
function DesktopPlayerTexas:_checkActorState()
    local player_state = self.PlayerDataDesktop.DesktopPlayerState
    if (self == self.DesktopTexas.MePlayer) then
        if (self.PlayerOperate ~= nil) then
            self.PlayerOperate:meStateChange(player_state)
        end
    end
    self.IsInGame = player_state ~= TexasDesktopPlayerState.Ob

    if (player_state == TexasDesktopPlayerState.Ob) then
        self.PlayerDataDesktop.CurrentRoundBet = 0
        self.PlayerDataDesktop.PlayerActionType = PlayerActionTypeTexas.None
        local seat_index = self.UiSeatIndex
        self.UiSeatIndex = 255
        if (self.UiDesktopPlayerInfo ~= nil) then
            self.UiDesktopPlayerInfo:playerOb(seat_index)
            self.DesktopTexas:releaseUiPlayerInfo(self.UiDesktopPlayerInfo)
            self.UiDesktopPlayerInfo = nil
        end
        if self.IsMe then
            local view_mgr = self.DesktopTexas.ControllerPlayer.ControllerMgr.ViewMgr
            local ev = view_mgr:GetEv("EvEntitySelfIsOB")
            if (ev == nil)
            then
                ev = EvEntitySelfIsOB:new(nil)
            end
            view_mgr:SendEv(ev)
        end
    elseif (player_state == TexasDesktopPlayerState.Wait4Next) then
        if (self.UiDesktopPlayerInfo ~= nil) then
            self.UiDesktopPlayerInfo:playerWait4Next()
        end
    elseif (player_state == TexasDesktopPlayerState.WaitWhile) then
        self.PlayerDataDesktop.CurrentRoundBet = 0
        self.PlayerDataDesktop.PlayerActionType = PlayerActionTypeTexas.None
        if (self.UiDesktopPlayerInfo ~= nil) then
            self.UiDesktopPlayerInfo:playerWaitWhile()
        end
    elseif (player_state == TexasDesktopPlayerState.InGame) then
        if (self.UiDesktopPlayerInfo ~= nil) then
            self.UiDesktopPlayerInfo:playerInGame()
        end
    end
end

---------------------------------------
function DesktopPlayerTexas:_checkActorAction(showshade_whenwait4next)
    if (self.UiDesktopPlayerInfo ~= nil) then
        if (self.IsMe == false and self.PlayerDataDesktop.PlayerActionType == PlayerActionTypeTexas.Fold) then
            self.UiDesktopPlayerInfo:playerFold()
        end

        self.UiDesktopPlayerInfo:setPlayerStateAndAction(self.PlayerDataDesktop.DesktopPlayerState,
                self.PlayerDataDesktop.PlayerActionType, showshade_whenwait4next)
        if self.PlayerOperate ~= nil then
            self.PlayerOperate:selfAction()
        end
    end
end

---------------------------------------
function DesktopPlayerTexas:_raiseChip(raise_chip, is_big_blind)
    self.DesktopTexas:playerRaise()
    if self.DesktopTexas.CurrentRountMaxBet < self.PlayerDataDesktop.CurrentRoundBet then
        self.DesktopTexas.CurrentRountMaxBet = self.PlayerDataDesktop.CurrentRoundBet
    end
    if (self.UiDesktopPlayerInfo ~= nil) then
        self.UiDesktopPlayerInfo:raiseChips()
    end
end

---------------------------------------
function DesktopPlayerTexas:_createUiPlayerInfo()
    if (CS.System.String.IsNullOrEmpty(self.DesktopTexas.DesktopGuid) == false) then
        local ui_desk = self.DesktopTexas.ControllerPlayer.ControllerMgr.ViewMgr:GetView("DesktopTexas")
        if (ui_desk == nil or self.DesktopTexas:isValidSeatIndex(self.PlayerDataDesktop.SeatIndex) == false) then
            return
        end

        if (self.UiDesktopPlayerInfo == nil) then
            local seat_info = ui_desk:getUiSeatInfo(self.UiSeatIndex)
            self.UiDesktopPlayerInfo = self.DesktopTexas:getPlayerInfo()
            local seat_num = self.DesktopTexas.SeatNum
            if seat_num == 6 then
                seat_num = 9
            end
            self.UiDesktopPlayerInfo:SetPlayerInfo(self, nil, seat_info.GComSeatPlayerParent.xy, seat_num,
                    self.DesktopTexas.ActionWaitingTm, self.IsGameEnd)
        end
    end
end

---------------------------------------
-- UiSeatIndex与SeatIndex的映射关系计算，在快照和玩家坐下时调用
function DesktopPlayerTexas:_setUiSeatIndex()
    local ui_index = self.PlayerDataDesktop.SeatIndex
    local valid = self.DesktopTexas:isValidSeatIndex(ui_index)
    local seat_num = self.DesktopTexas.SeatNum
    if seat_num == 6 then
        seat_num = 9
    end
    if (valid and self.DesktopTexas.MeP.UiMiddleSeatIndexRealIndex ~= 255) then
        local half_seatnum = math.floor(seat_num / 2)
        ui_index = ui_index + (half_seatnum - self.DesktopTexas.MeP.UiMiddleSeatIndexRealIndex + seat_num)
        local remainder = ui_index % seat_num
        ui_index = remainder
    end

    self.UiSeatIndex = ui_index

    if (seat_num == 5) then
        self.UiSeatIndex = (self.UiSeatIndex * 2)
    end
end