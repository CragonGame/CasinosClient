-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ControllerDeskH = ControllerBase:new(nil)

---------------------------------------
function ControllerDeskH:new(o, controller_mgr, controller_data, guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    o.ControllerData = controller_data
    o.ControllerMgr = controller_mgr
    o.Guid = guid
    o.ViewMgr = ViewMgr:new(nil)
    self.QueBetpotWinLooseResultCount = 10

    return o
end

---------------------------------------
function ControllerDeskH:onCreate()
    self.ViewMgr:bindEvListener("EvUiClickDesktopHundred", self)
    self.ViewMgr:bindEvListener("EvUiClickLeaveDesktopHundred", self)
    self.ViewMgr:bindEvListener("EvDesktopHClickBetOperateType", self)
    self.ViewMgr:bindEvListener("EvUiDesktopHSeatDown", self)
    self.ViewMgr:bindEvListener("EvUiDesktopHStandUp", self)
    self.ViewMgr:bindEvListener("EvDesktopHClickBeBankPlayerBtn", self)
    self.ViewMgr:bindEvListener("EvEntityRequestGetDesktopHData", self)
    self.ViewMgr:bindEvListener("EvDesktopHClickStandPlayerBtn", self)
    self.ViewMgr:bindEvListener("EvDesktopHundredChangeCardsType", self)
    self.ViewMgr:bindEvListener("EvDesktopHClickRewardPotBtn", self)
    self.ViewMgr:bindEvListener("EvDesktopHGetBetReward", self)
    self.ViewMgr:bindEvListener("EvDesktopHInitBetReward", self)
    self.ViewMgr:bindEvListener("EvDesktopHBet", self)
    self.ViewMgr:bindEvListener("EvDesktopHRepeatBet", self)
    self.ViewMgr:bindEvListener("EvUiSendMsg", self)
    self.ViewMgr:bindEvListener("EvUiSetUnSendDesktopMsg", self)
    self.ViewMgr:bindEvListener("EvEntityGoldChanged", self)
    self.ViewMgr:bindEvListener("EvEntityPlayerEnterDesktop", self)
    self.ControllerPlayer = self.ControllerMgr:GetController("Player")
    self.ControllerActor = self.ControllerMgr:GetController("Actor")
    self.ControllerIM = self.ControllerMgr:GetController("IM")
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.MapBetPotSelfBetGolds = {}
    self.MapBetPotStandPlayerBetGolds = {}
    self.MapBetPotStandPlayerBetDeltaGolds = {}
    self.MapBetRepeatInfo = {}
    self.MapCurrentRoundSelfBetInfo = {}
    self.IsBankPlayer = false
    self.SeatIndex = 255
    local operate_id = -1
    if (CS.UnityEngine.PlayerPrefs.HasKey("CurrentTbBetOperateIdDesktopH"))
    then
        operate_id = CS.UnityEngine.PlayerPrefs.GetInt("CurrentTbBetOperateIdDesktopH")
    end
    self.CurrentTbBetOperateId = operate_id
    self.MapBetPotWinlooseRecord = {}
    self.MapCanOperateId = {}
    self.ListBeBankPlayer = {}
    self.MapSeatPlayer = {}
    self.ListDesktopChat = {}
    self.DesktopHGuid = ""
    self.MapDesktopHBaseFac = {}
    self:regDesktopHBaseFactory(DesktopHTexasFactory:new(nil))
    self.MapDesktopHCardTypeBaseFac = {}
    self:regDesktopHCardTypeBaseFactory(DesktopHCardTypeTexasFac:new(nil))
    local rpc = self.ControllerMgr.RPC
    local m_c = CommonMethodType
    rpc:RegRpcMethod3(m_c.DesktopHNotifySnapshot, function(desktoph_snapshot, map_my_betinfo, map_my_winlooseinfo)
        self:s2cDesktopHNotifySnapshot(desktoph_snapshot, map_my_betinfo, map_my_winlooseinfo)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHNotifyIdle, function(left_tm)
        self:s2cDesktopHNotifyIdle(left_tm)
    end)
    rpc:RegRpcMethod3(m_c.DesktopHNotifyReady2, function(left_tm, play_guid, map_userdata)
        self:s2cDesktopHNotifyReady2(left_tm, play_guid, map_userdata)
    end)
    rpc:RegRpcMethod2(m_c.DesktopHNotifyBet, function(left_tm, max_total_bet_gold)
        self:s2cDesktopHNotifyBet(left_tm, max_total_bet_gold)
    end)
    rpc:RegRpcMethod2(m_c.DesktopHNotifyGameEnd, function(game_result, map_my_winlooseinfo)
        self:s2cDesktopHNotifyGameEnd(game_result, map_my_winlooseinfo)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHNotifyRest, function(left_tm)
        self:s2cDesktopHNotifyRest(left_tm)
    end)
    rpc:RegRpcMethod0(m_c.DesktopHNotifyPlayerLeave, function()
        self:s2cDesktopHNotifyPlayerLeave()
    end)
    rpc:RegRpcMethod1(m_c.DesktopHNotifyUpdateBetPotInfo, function(bet_info)
        self:s2cDesktopHNotifyUpdateBetPotInfo(bet_info)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHNotifyUpdateSeatPlayerGold, function(map_seatplayer_gold)
        self:s2cDesktopHNotifyUpdateSeatPlayerGold(map_seatplayer_gold)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHNotifyUpdateBankPlayer, function(banker_info)
        self:s2cDesktopHNotifyUpdateBankPlayer(banker_info)
    end)
    rpc:RegRpcMethod2(m_c.DesktopHNotifyUpdateBankPlayerStack, function(player_guid, new_stack)
        self:s2cDesktopHNotifyUpdateBankPlayerStack(player_guid, new_stack)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHNotifySeatPlayerChanged, function(map_all_seatplayer)
        self:s2cDesktopHNotifySeatPlayerChanged(map_all_seatplayer)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHNotifyBeBankerPlayerListAdd, function(add_player)
        self:s2cDesktopHNotifyBeBankerPlayerListAdd(add_player)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHNotifyBeBankerPlayerListRemove, function(remove_playerguid)
        self:s2cDesktopHNotifyBeBankerPlayerListRemove(remove_playerguid)
    end)
    rpc:RegRpcMethod0(m_c.DesktopHNotifyPlayerWillStandup, function()
        self:s2cDesktopHNotifyPlayerWillStandup()
    end)
    rpc:RegRpcMethod0(m_c.DesktopHNotifyPlayerWillBeNotBank, function()
        self:s2cDesktopHNotifyPlayerWillBeNotBank()
    end)
    rpc:RegRpcMethod1(m_c.DesktopHNotifyPlayerCurrentGiftChanged, function(map_itemdata)
        self:s2cDesktopHNotifyPlayerCurrentGiftChanged(map_itemdata)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHNotifyPlayerChat, function(msg)
        self:s2cDesktopHNotifyPlayerChat(msg)
    end)
    rpc:RegRpcMethod2(m_c.DesktopHNotifyBuyAndSendItem, function(sender_guid, map_recv_itemdata)
        self:s2cDesktopHNotifyBuyAndSendItem(sender_guid, map_recv_itemdata)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHResponsePlayerLeave, function(can_leave)
        self:s2cDesktopHResponsePlayerLeave(can_leave)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHResponseBetFailed, function(map_my_betinfo)
        self:s2cDesktopHResponseBetFailed(map_my_betinfo)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHResponseBeBankerPlayer, function(result)
        self:s2cDesktopHResponseBeBankerPlayer(result)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHResponseNotBeBankerPlayer, function(result)
        self:s2cDesktopHResponseNotBeBankerPlayer(result)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHResponseSitdown, function(result)
        self:s2cDesktopHResponseSitdown(result)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHResponseStandup, function(result)
        self:s2cDesktopHResponseStandup(result)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHResponseGetStandPlayerList, function(list_stand)
        self:s2cDesktopHResponseGetStandPlayerList(list_stand)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHResponseSetCardsType, function(result)
        self:s2cDesktopHResponseSetCardsType(result)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHResponseGetWinRewardPotInfo, function(win_rewardpot_info)
        self:s2cDesktopHResponseGetWinRewardPotInfo(win_rewardpot_info)
    end)
    rpc:RegRpcMethod1(m_c.DesktopHResponseInitDailyBetReward, function(init_dailybet_reward)
        self:s2cDesktopHResponseInitDailyBetReward(init_dailybet_reward)
    end)
    rpc:RegRpcMethod3(m_c.DesktopHResponseGetDailyBetReward, function(result, reward_gold, bet_reward)
        self:s2cDesktopHResponseGetDailyBetReward(result, reward_gold, bet_reward)
    end)
end

---------------------------------------
function ControllerDeskH:onDestroy()
    self.ViewMgr:unbindEvListener(self)
    if (self.FTaskerLeaveDesktopH ~= nil)
    then
        self.FTaskerLeaveDesktopH:cancelTask()
        self.FTaskerLeaveDesktopH = nil
    end
end

---------------------------------------
function ControllerDeskH:onUpdate(tm)
end

---------------------------------------
function ControllerDeskH:onHandleEv(ev)
    if (self.DesktopHBase ~= nil)
    then
        self.DesktopHBase:onHandleEvent(ev)
    end

    if (ev.EventName == "EvUiClickDesktopHundred")
    then
        ViewHelper:UiBeginWaiting(self.ViewMgr.LanMgr:getLanValue("EnterTable"))
        self.ControllerMgr.RPC:RPC1(CommonMethodType.DesktopHRequestEnter, ev.factory_name)
    elseif (ev.EventName == "EvUiClickLeaveDesktopHundred")
    then
        self.ControllerMgr.RPC:RPC0(CommonMethodType.DesktopHRequestLeave)
    elseif (ev.EventName == "EvDesktopHClickBetOperateType")-- 更换选择下注的预置筹码
    then
        CS.UnityEngine.PlayerPrefs.SetInt("CurrentTbBetOperateIdDesktopH", ev.tb_bet_operateid)
        self.CurrentTbBetOperateId = ev.tb_bet_operateid
        local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHCurrentBetOperateTypeChange")
        if (ev == nil)
        then
            ev = EvEntityDesktopHCurrentBetOperateTypeChange:new(nil)
        end
        self.ControllerMgr.ViewMgr:sendEv(ev)
    elseif (ev.EventName == "EvUiDesktopHSeatDown")
    then
        local min_golds = ev.min_golds

        if (min_golds > self.ControllerActor.PropGoldAcc:get())
        then
            local tips = string.format(self.ViewMgr.LanMgr:getLanValue("SitDownFailedTips1"),
                    self.ViewMgr.LanMgr:getLanValue("Chip")
            , UiChipShowHelper:getGoldShowStr(min_golds, self.ControllerMgr.LanMgr.LanBase))
            ViewHelper:UiShowMsgBox(tips,
                    function()
                        self.ControllerMgr.ViewMgr:createView("Shop")
                    end
            )
            return
        end
        local exists = false
        for key, value in pairs(self.ListBeBankPlayer) do
            if (value.PlayerInfoCommon.PlayerGuid == self.Guid)
            then
                exists = true
                break
            end
        end
        if (exists)
        then
            ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("RequestSitSuccess"))
        end
        self.ControllerMgr.RPC:RPC1(CommonMethodType.DesktopHRequestSitdown, ev.seat_index)
    elseif (ev.EventName == "EvUiDesktopHStandUp")
    then
        self.ControllerMgr.RPC:RPC0(CommonMethodType.DesktopHRequestStandup)
    elseif (ev.EventName == "EvDesktopHClickBeBankPlayerBtn")
    then
        local exists = false
        for key, value in pairs(self.ListBeBankPlayer) do
            if (value.PlayerInfoCommon.PlayerGuid == self.Guid)
            then
                exists = true
                break
            end
        end
        if (self.IsBankPlayer or exists)
        then
            if (exists or self.IsBankPlayer)
            then
                self.ControllerMgr.RPC:RPC0(CommonMethodType.DesktopHRequestNotBeBankerPlayer)
            end
        else
            local min_golds = 0
            local stack = self.ControllerActor.PropGoldAcc:get()
            min_golds = ev.bebank_mingolds
            stack = ev.take_stack

            if (min_golds > self.ControllerActor.PropGoldAcc:get())
            then
                local tips = string.format(self.ViewMgr.LanMgr:getLanValue("RequestBeBankerFailed1"),
                        self.ViewMgr.LanMgr:getLanValue("Chip"),
                        UiChipShowHelper:getGoldShowStr(min_golds, self.ControllerMgr.LanMgr.LanBase))
                ViewHelper:UiShowMsgBox(tips,
                        function()
                            self.ControllerMgr.ViewMgr:createView("Shop")
                        end
                )
                return
            end

            local is_seatplayer = self:isSeatPlayer(self.Guid)
            if (is_seatplayer)
            then
                ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("RequestBeBankerSuccess"))
            end

            self.ControllerMgr.RPC:RPC1(CommonMethodType.DesktopHRequestBeBankerPlayer, stack)
        end
    elseif (ev.EventName == "EvEntityRequestGetDesktopHData")
    then
        if (self.DesktopHGuid == nil or self.DesktopHGuid == "")
        then
            return
        end

        self.ControllerMgr.RPC:RPC0(CommonMethodType.DesktopHRequestSnapshot)
    elseif (ev.EventName == "EvDesktopHClickStandPlayerBtn")-- 拉取所有无座玩家列表，暂未使用
    then
        self.ControllerMgr.RPC:RPC0(CommonMethodType.DesktopHRequestGetStandPlayerList)
    elseif (ev.EventName == "EvDesktopHundredChangeCardsType")
    then
        self.ControllerMgr.RPC:RPC1(CommonMethodType.DesktopHRequestSetCardsType, ev.map_card_types)
    elseif (ev.EventName == "EvDesktopHClickRewardPotBtn")
    then
        ViewHelper:UiBeginWaiting()
        self.ControllerMgr.RPC:RPC0(CommonMethodType.DesktopHRequestGetWinRewardPotInfo)
    elseif (ev.EventName == "EvDesktopHGetBetReward")
    then
        self.ControllerMgr.RPC:RPC1(CommonMethodType.DesktopHRequestGetDailyBetReward, ev.factory_name)
    elseif (ev.EventName == "EvDesktopHInitBetReward")
    then
        self.ControllerMgr.RPC:RPC1(CommonMethodType.DesktopHRequestInitDailyBetReward, ev.factory_name)
    elseif (ev.EventName == "EvDesktopHBet")
    then
        if (self.DesktopHState ~= _eDesktopHState.Bet)
        then
            return
        end

        if (self.CurrentTbBetOperateId == -1)
        then
            local tips = self.ViewMgr.LanMgr:getLanValue("Chip") ..
                    self.ViewMgr.LanMgr:getLanValue("NotEnough")
            ViewHelper:UiShowInfoSuccess(tips)
            return
        end

        if (self.IsBankPlayer == true)
        then
            ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("BankerBetTips"))
            return
        end

        local bet_chips = self.DesktopHBase:getOperateGold(self.CurrentTbBetOperateId)
        local can_betsuccess = self:betGold(ev.bet_betpot_index, bet_chips)

        if (can_betsuccess == true)
        then
            self.ControllerMgr.RPC:RPC2(CommonMethodType.DesktopHRequestBet, ev.bet_betpot_index, bet_chips)
        end
    elseif (ev.EventName == "EvDesktopHRepeatBet")
    then
        if (self.DesktopHState ~= _eDesktopHState.Bet)
        then
            return
        end

        if (self.CurrentTbBetOperateId == -1)
        then
            local tips = self.ViewMgr.LanMgr:getLanValue("Chip") ..
                    self.ViewMgr.LanMgr:getLanValue("NotEnough")
            ViewHelper:UiShowInfoSuccess(tips)
            return
        end

        if (self.IsBankPlayer)
        then
            ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("BankerBetTips"))
            return
        end
        local lua_helper = LuaHelper:new(nil)
        if (lua_helper:GetTableCount(self.MapBetRepeatInfo) > 0)
        then
            local all_golds = 0
            for key, value in pairs(self.MapBetRepeatInfo) do
                all_golds = all_golds + value
            end
            local max_percent = self.DesktopHBase:getMaxCannotBetPecent()
            local total_golds = self.ControllerActor.PropGoldAcc:get()
            local after_bet_golds = self.TotalBetGolds + all_golds
            if (after_bet_golds * max_percent > total_golds + self.TotalBetGolds)
            then
                ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("BetLimitTips"))
                return
            end

            local can_betsuccess = false
            for key, value in pairs(self.MapBetRepeatInfo) do
                can_betsuccess = self:betGold(key, value)
                if (can_betsuccess == false)
                then
                    break
                end
            end
            if (can_betsuccess)
            then
                self.ControllerMgr.RPC:RPC1(CommonMethodType.DesktopHRequestBetRepeat, self.MapBetRepeatInfo)
            end
        end
    elseif (ev.EventName == "EvUiSendMsg")-- 百人桌聊天
    then
        local chat_msg = ev.chat_msg
        local recver_guid = chat_msg[4]
        if (recver_guid == nil or recver_guid == "")
        then
            self.ControllerMgr.RPC:RPC1(CommonMethodType.DesktopHRequestChat, chat_msg)
            self.CurrentUnSendDesktopMsg = ""
        end
    elseif (ev.EventName == "EvUiSetUnSendDesktopMsg")
    then
        self.CurrentUnSendDesktopMsg = ev.text
    elseif (ev.EventName == "EvEntityGoldChanged")-- 本人AccGold改变
    then
        self:updateSuitBetOperateId()
    elseif (ev.EventName == "EvEntityPlayerEnterDesktop")-- 进入普通桌
    then
        self:clearDesktopHundred(false)
    end
end

---------------------------------------
-- 快照响应
function ControllerDeskH:s2cDesktopHNotifySnapshot(desktoph_snapshot, map_my_betinfo, map_my_winlooseinfo)
    ViewHelper:UiEndWaiting()
    local can_enter = true
    local d_d = nil
    if (desktoph_snapshot == nil)
    then
        can_enter = false
    else
        d_d = BDesktopHData:new(nil)
        d_d:setData(desktoph_snapshot)
        if (d_d.desktoph_guid == nil or d_d.desktoph_guid == "") then
            can_enter = false
        end
    end
    if can_enter == false then
        local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityPlayerEnterDesktopHFailed")
        if (ev == nil)
        then
            ev = EvEntityPlayerEnterDesktopHFailed:new(nil)
        end
        self.ControllerMgr.ViewMgr:sendEv(ev)
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("EnterTableFailed"))
        return
    end

    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityPlayerEnterDesktopH")
    if (ev == nil)
    then
        ev = EvEntityPlayerEnterDesktopH:new(nil)
    end
    self.ControllerMgr.ViewMgr:sendEv(ev)
    self.DesktopHGuid = d_d.desktoph_guid
    if (d_d.list_bebanker ~= nil)
    then
        self.ListBeBankPlayer = d_d.list_bebanker
    end
    local fac = self:GetDesktopHBaseFactory(d_d.factory_name)
    self.DesktopHBase = fac:CreateDesktop(self, d_d.factory_name)
    self:updateSuitBetOperateId()
    self.DesktopHState = d_d.state
    self.TotalBetGolds = 0

    if (d_d.map_betpot_winloose_record ~= nil)
    then
        for i, v in pairs(d_d.map_betpot_winloose_record) do
            self.MapBetPotWinlooseRecord[i] = v
        end
    end

    if (d_d.map_seatplayer ~= nil)
    then
        self.MapSeatPlayer = d_d.map_seatplayer
    end
    for key, value in pairs(map_my_betinfo) do
        self.MapBetPotSelfBetGolds[key] = value
        self.MapCurrentRoundSelfBetInfo[key] = value
        self.TotalBetGolds = self.TotalBetGolds + value
    end
    self.BankPlayer = d_d.banker_player
    self.RewardPotGolds = d_d.reward_pot
    self.IsBankPlayer = false
    if (self.BankPlayer.PlayerInfoCommon.PlayerGuid == self.Guid)
    then
        self.IsBankPlayer = true
    end

    self.CasinosContext:stopAllSceneSound()
    self.ControllerPlayer:destroyMainUi()
    local ui_desktoph = self.ControllerMgr.ViewMgr:getView("DesktopH")
    if (ui_desktoph == nil)
    then
        ui_desktoph = self.ControllerMgr.ViewMgr:createView("DesktopH")
    end

    local t_map_my_winlooseinfo = nil
    if map_my_winlooseinfo ~= nil then
        t_map_my_winlooseinfo = {}
        for i, v in pairs(map_my_winlooseinfo) do
            local w_l = BDesktopHNotifyGameEndBetPotPlayerWinLooseInfo:new(nil)
            w_l:setData(v)
            t_map_my_winlooseinfo[i] = w_l
        end
    end
    ui_desktoph:initDesktopHundred(d_d, map_my_betinfo, t_map_my_winlooseinfo)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHNotifyIdle(left_tm)
    self.DesktopHState = _eDesktopHState.Idle
    self.MapBetPotSelfBetGolds = {}
    self.MapBetPotStandPlayerBetDeltaGolds = {}
    self.MapBetPotStandPlayerBetGolds = {}
    self.TotalBetGolds = 0
end

---------------------------------------
function ControllerDeskH:s2cDesktopHNotifyReady2(left_tm, play_guid, map_userdata)
    self.DesktopHState = _eDesktopHState.Ready
    self.MapCurrentRoundSelfBetInfo = {}
    self.MapBetPotSelfBetGolds = {}
    self.MapBetPotStandPlayerBetDeltaGolds = {}
    self.MapBetPotStandPlayerBetGolds = {}
    self.TotalBetGolds = 0
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHReadyState")
    if (ev == nil)
    then
        ev = EvEntityDesktopHReadyState:new(nil)
    end
    ev.left_tm = left_tm
    ev.map_userdata = map_userdata
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHNotifyBet(left_tm, max_total_bet_gold)
    self.DesktopHState = _eDesktopHState.Bet
    self.TotalBetGolds = 0
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHBetState")
    if (ev == nil)
    then
        ev = EvEntityDesktopHBetState:new(nil)
    end
    ev.map_betrepeatinfo = self.MapBetRepeatInfo
    ev.left_tm = left_tm
    ev.max_total_bet_gold = max_total_bet_gold
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHNotifyGameEnd(game_result, map_my_winlooseinfo)
    if (self.DesktopHBase == nil)
    then
        return
    end

    local g_r = BDesktopHGameResult:new(nil)
    g_r:setData(game_result)
    if (g_r.desktoph_guid ~= self.DesktopHGuid)
    then
        return
    end

    self.DesktopHState = _eDesktopHState.GameEnd
    if (g_r.premature_termination == true)
    then
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("DirectLotteryTips"))
    end

    for key, value in pairs(g_r.map_betpot_info) do
        local list_history = self.MapBetPotWinlooseRecord[key]
        if (list_history == nil)
        then
            list_history = {}
            self.MapBetPotWinlooseRecord[key] = list_history
        end
        if (#list_history >= self.QueBetpotWinLooseResultCount)
        then
            table.remove(list_history, 1)
        end
        table.insert(list_history, value.is_win)
    end
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHGameEndState")
    if (ev == nil)
    then
        ev = EvEntityDesktopHGameEndState:new(nil)
    end
    ev.game_result = g_r

    local t_map_my_winlooseinfo = nil
    if map_my_winlooseinfo ~= nil then
        t_map_my_winlooseinfo = {}
        for i, v in pairs(map_my_winlooseinfo) do
            local w_l = BDesktopHNotifyGameEndBetPotPlayerWinLooseInfo:new(nil)
            w_l:setData(v)
            t_map_my_winlooseinfo[i] = w_l
        end
    end
    ev.map_my_winlooseinfo = t_map_my_winlooseinfo
    self.ControllerMgr.ViewMgr:sendEv(ev)
    self.RewardPotGolds = g_r.rewardpot_info.gold_after
end

---------------------------------------
function ControllerDeskH:s2cDesktopHNotifyRest(left_tm)
    self.DesktopHState = _eDesktopHState.Rest
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHGameRestState")
    if (ev == nil)
    then
        ev = EvEntityDesktopHGameRestState:new(nil)
    end
    ev.left_tm = left_tm
    self.ControllerMgr.ViewMgr:sendEv(ev)
    self.MapBetPotSelfBetGolds = {}
    self.MapBetPotStandPlayerBetGolds = {}
    self.MapBetPotStandPlayerBetDeltaGolds = {}
    self.TotalBetGolds = 0
    self.MapBetRepeatInfo = {}
    for key, value in pairs(self.MapCurrentRoundSelfBetInfo) do
        self.MapBetRepeatInfo[key] = value
    end
    self.MapCurrentRoundSelfBetInfo = {}
end

---------------------------------------
-- 本人离开
function ControllerDeskH:s2cDesktopHNotifyPlayerLeave()
    if (self.FTaskLeaveDesktopH ~= nil)
    then
        self.FTaskLeaveDesktopH:setTaskDone()
        self.FTaskLeaveDesktopH = nil
    end
    self:clearDesktopHundred(true)
end

---------------------------------------
-- 刷新4个下注池信息
function ControllerDeskH:s2cDesktopHNotifyUpdateBetPotInfo(bet_info)
    local b_i = BDesktopHNotifyBetDeltaInfo:new(nil)
    b_i:setData(bet_info)
    self.MapBetPotStandPlayerBetDeltaGolds = {}
    if (b_i.map_standplayer_betdeltainfo ~= nil)
    then
        for key, value in pairs(b_i.map_standplayer_betdeltainfo) do
            local bet_gold = 0
            if (self.MapBetPotStandPlayerBetGolds[key] ~= nil)
            then
                bet_gold = self.MapBetPotStandPlayerBetGolds[key]
            end
            bet_gold = bet_gold + value
            self.MapBetPotStandPlayerBetGolds[key] = bet_gold
            if (self.IsBankPlayer == false and self.SeatIndex == 255)
            then
                local self_betgold = 0
                if (self.MapBetPotSelfBetGolds[key] ~= nil)
                then
                    self_betgold = self.MapBetPotSelfBetGolds[key]
                end
                bet_gold = bet_gold - self_betgold
                self.MapBetPotStandPlayerBetDeltaGolds[key] = bet_gold
            else
                self.MapBetPotStandPlayerBetDeltaGolds[key] = value
            end
        end
    end

    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHUpdateBetPotBetInfo")
    if (ev == nil)
    then
        ev = EvEntityDesktopHUpdateBetPotBetInfo:new(nil)
    end
    ev.map_betpot_betdeltainfo = b_i.map_betpot_betdeltainfo
    ev.map_standplayer_betdeltainfo = self.MapBetPotStandPlayerBetDeltaGolds
    ev.list_seatplayer_betinfo = b_i.list_seatplayer_betdeltainfo
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHNotifyUpdateSeatPlayerGold(map_seatplayer_gold)
    for key, value in pairs(map_seatplayer_gold) do
        local list_player = nil
        for key, value in pairs(self.MapSeatPlayer) do
            if (value ~= nil and value.PlayerInfoCommon.PlayerGuid == key)
            then
                if (list_player == nil)
                then
                    list_player = {}
                end
                table.insert(list_player, value)
            end
        end
        if (list_player ~= nil and #list_player > 0)
        then
            local player_data = list_player[1].Value
            player_data.Gold = value
        end
    end
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHSeatPlayerGoldChanged")
    if (ev == nil)
    then
        ev = EvEntityDesktopHSeatPlayerGoldChanged:new(nil)
    end
    ev.map_seatplayer_golds = map_seatplayer_gold
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
-- 上庄，下庄，庄家列表
function ControllerDeskH:s2cDesktopHNotifyUpdateBankPlayer(banker_info)
    local b_i = PlayerDataDesktopH:new(nil)
    b_i:setData(banker_info)
    if (b_i.PlayerInfoCommon.PlayerGuid == self.Guid)
    then
        self.IsBankPlayer = true
    else
        self.IsBankPlayer = false
    end

    self.BankPlayer = b_i

    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHChangeBankerPlayer")
    if (ev == nil)
    then
        ev = EvEntityDesktopHChangeBankerPlayer:new(nil)
    end
    ev.banker_player = b_i
    ev.list_bebankplayer = self.ListBeBankPlayer
    ev.is_bankplayer = self.IsBankPlayer
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHNotifyUpdateBankPlayerStack(player_guid, new_stack)
    if (self.BankPlayer.PlayerInfoCommon.PlayerGuid == player_guid)
    then
        self.BankPlayer.Gold = new_stack
    end
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHBankerPlayerGoldChange")
    if (ev == nil)
    then
        ev = EvEntityDesktopHBankerPlayerGoldChange:new(nil)
    end
    ev.banker_player = self.BankPlayer
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
-- 有座玩家坐下，站起，换座
function ControllerDeskH:s2cDesktopHNotifySeatPlayerChanged(map_all_seatplayer)
    self.MapSeatPlayer = {}
    if map_all_seatplayer ~= nil then
        for i, v in pairs(map_all_seatplayer) do
            local p_d = PlayerDataDesktopH:new(nil)
            p_d:setData(v)
            self.MapSeatPlayer[i] = p_d
        end
    end
    local is_seat = false
    for key, value in pairs(self.MapSeatPlayer) do
        if (value.PlayerInfoCommon.PlayerGuid == self.Guid)
        then
            is_seat = true
            self.SeatIndex = key
            break
        end
    end
    if (is_seat == false)
    then
        self.SeatIndex = 255
    end
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHChangeSeatPlayer")
    if (ev == nil)
    then
        ev = EvEntityDesktopHChangeSeatPlayer:new(nil)
    end
    ev.map_seat_player = self.MapSeatPlayer
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHNotifyBeBankerPlayerListAdd(add_player)
    if (add_player ~= nil)
    then
        local p_d = PlayerDataDesktopH:new(nil)
        p_d:setData(add_player)
        table.insert(self.ListBeBankPlayer, p_d)
    end
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHChangeBeBankerPlayerList")
    if (ev == nil)
    then
        ev = EvEntityDesktopHChangeBeBankerPlayerList:new(nil)
    end
    ev.list_bebanker = self.ListBeBankPlayer
    ev.banker_player = self.BankPlayer
    ev.is_bankplayer = self.IsBankPlayer
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHNotifyBeBankerPlayerListRemove(remove_playerguid)
    if (remove_playerguid ~= nil)
    then
        local player_info = nil
        local player_info_key = nil
        for key, value in pairs(self.ListBeBankPlayer) do
            if (value.PlayerInfoCommon.PlayerGuid == remove_playerguid)
            then
                player_info = value
                player_info_key = key
                break
            end
        end
        if (player_info ~= nil)
        then
            table.remove(self.ListBeBankPlayer, player_info_key)
        end

    end
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHChangeBeBankerPlayerList")
    if (ev == nil)
    then
        ev = EvEntityDesktopHChangeBeBankerPlayerList:new(nil)
    end
    ev.list_bebanker = self.ListBeBankPlayer
    ev.banker_player = self.BankPlayer
    ev.is_bankplayer = self.IsBankPlayer
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
-- 有座玩家连续几把没下注，提示下把离座
function ControllerDeskH:s2cDesktopHNotifyPlayerWillStandup()
    ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("KickOutSeatTips"))
end

---------------------------------------
-- 提示下把下庄
function ControllerDeskH:s2cDesktopHNotifyPlayerWillBeNotBank()
    ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("NotBeBankTips"))
end

---------------------------------------
-- 百人桌头像暂时未显示礼物
function ControllerDeskH:s2cDesktopHNotifyPlayerCurrentGiftChanged(map_itemdata)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHNotifyPlayerChat(msg)
    if (self.DesktopHBase ~= nil)
    then
        self.DesktopHBase:DesktopHChat(msg)
    end
    self.CurrentUnSendDesktopMsg = ""
end

---------------------------------------
function ControllerDeskH:s2cDesktopHNotifyBuyAndSendItem(sender_guid, map_recv_itemdata)
    if (self.DesktopHBase == nil)
    then
        return
    end
    local t_map_recv_itemdata = {}
    for i, v in pairs(map_recv_itemdata) do
        local i_d = ItemData1:new(nil)
        i_d:setData(v)
        t_map_recv_itemdata[i] = i_d
    end
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHBuyItem")
    if (ev == nil)
    then
        ev = EvEntityDesktopHBuyItem:new(nil)
    end
    ev.map_items = t_map_recv_itemdata
    ev.sender_guid = sender_guid
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHResponsePlayerLeave(can_leave)
    if (can_leave == false)
    then
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("AutoLeaveDesktopHTips"))
    end
end

---------------------------------------
function ControllerDeskH:s2cDesktopHResponseBetFailed(map_my_betinfo)
    if (map_my_betinfo ~= nil)
    then
        self.MapBetPotSelfBetGolds = {}
        self.MapCurrentRoundSelfBetInfo = {}
        for key, value in pairs(map_my_betinfo) do
            self.MapBetPotSelfBetGolds[key] = value
            self.MapCurrentRoundSelfBetInfo[key] = value
            self.TotalBetGolds = self.TotalBetGolds + value
        end
    end
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHBetFailed")
    if (ev == nil)
    then
        ev = EvEntityDesktopHBetFailed:new(nil)
    end
    ev.map_self_betgolds = self.MapBetPotSelfBetGolds
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHResponseBeBankerPlayer(result)
    if (result == ProtocolResult.Success)
    then
    else
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("RequestBeBankerFailed"))
    end
end

---------------------------------------
function ControllerDeskH:s2cDesktopHResponseNotBeBankerPlayer(result)
    if (result == ProtocolResult.Success)
    then
    else
        ViewHelper.UiShowInfoFailedself(self.ViewMgr.LanMgr:getLanValue("RequestNotBeBankerFailed"))
    end
end

---------------------------------------
function ControllerDeskH:s2cDesktopHResponseSitdown(result)
    local min_golds = 0
    local desktop = self.ControllerMgr.ViewMgr:createView("DesktopH")
    if (desktop ~= nil)
    then
        min_golds = desktop.UiDesktopHBase:getSeatDownMinGolds()
    end

    if (result == ProtocolResult.Failed)
    then
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("SitDownFailedTips"))
    elseif (result == ProtocolResult.ChipNotEnough)
    then
        local tips = self.ViewMgr.LanMgr:getLanValue("Chip") ..
                self.ViewMgr.LanMgr:getLanValue("NotEnough") ..
                UiChipShowHelper:getGoldShowStr(min_golds, self.ControllerMgr.LanMgr.LanBase) ..
                self.ViewMgr.LanMgr:getLanValue("SitDownFailedTips")
        ViewHelper:UiShowInfoSuccess(tips)
    end
end

---------------------------------------
function ControllerDeskH:s2cDesktopHResponseStandup(result)
    if (result == ProtocolResult.Success)
    then
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("RequestLeaveSitSuccessTips"))
    else
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("RequestLeaveSitFaiedTips"))
    end
end

---------------------------------------
function ControllerDeskH:s2cDesktopHResponseGetStandPlayerList(list_stand)
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHGetStandPlayers")
    if (ev == nil)
    then
        ev = EvEntityDesktopHGetStandPlayers:new(nil)
    end
    ev.list_standplayer = list_stand
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHResponseSetCardsType(result)
    if (result == ProtocolResult.Success)
    then
        local msg = self.ViewMgr.LanMgr:getLanValue("ChangeCardTypeSuccessTips")
        ViewHelper:UiShowInfoSuccess(msg)
    else
        local msg = self.ViewMgr.LanMgr:getLanValue("ChangeCardTypeFaiedTips")
        ViewHelper:UiShowInfoSuccess(msg)
    end
end

---------------------------------------
function ControllerDeskH:s2cDesktopHResponseGetWinRewardPotInfo(win_rewardpot_info)
    ViewHelper:UiEndWaiting()
    local w_i = BDesktopHWinRewardPotInfo:new(nil)
    w_i:setData(win_rewardpot_info)
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHGetRewardPotInfo")
    if (ev == nil)
    then
        ev = EvEntityDesktopHGetRewardPotInfo:new(nil)
    end
    ev.total_info = w_i
    ev.reward_totalgolds = self.RewardPotGolds
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHResponseInitDailyBetReward(init_dailybet_reward)
    local i_r = BDesktopHDialyBetReward:new(nil)
    i_r:setData(init_dailybet_reward)
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityInitBetReward")
    if (ev == nil)
    then
        ev = EvEntityInitBetReward:new(nil)
    end
    ev.init_dailybet_reward = i_r
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:s2cDesktopHResponseGetDailyBetReward(result, reward_gold, bet_reward)
    if (result == ProtocolResult.Success)
    then
        ViewHelper:UiShowInfoSuccess(string.format(self.ViewMgr.LanMgr:getLanValue(
                "GetBetRewardSuccessTips"), UiChipShowHelper:getGoldShowStr(reward_gold, self.ControllerMgr.LanMgr.LanBase)))
    else
        ViewHelper:UiShowInfoFailed(self.ViewMgr.LanMgr:getLanValue("GetBetRewardFaiedTips"))
    end
end

---------------------------------------
function ControllerDeskH:isSeatPlayer(player_guid)
    local is_seatplayer = false
    for key, value in pairs(self.MapSeatPlayer) do
        --if (value == nil)
        --then
        --continue
        --end
        if (value.PlayerInfoCommon.PlayerGuid == player_guid)
        then
            is_seatplayer = true
            break
        end
    end

    return is_seatplayer
end

---------------------------------------
function ControllerDeskH:addDesktopMsg(from_etguid, from_name, sender_viplevel, chat_content)
    if (from_etguid == nil or from_etguid == "")
    then
        return
    end
    local chat_info = ChatTextInfo:new(nil)
    chat_info.chat_content = chat_content
    chat_info.sender_etguid = from_etguid
    chat_info.sender_name = from_name
    chat_info.sender_viplevel = sender_viplevel
    table.insert(self.ListDesktopChat, chat_info)

    if (#self.ListDesktopChat > 50)
    then
        table.remove(self.ListDesktopChat, 1)
    end
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityRecvChatFromDesktopH")
    if (ev == nil)
    then
        ev = EvEntityRecvChatFromDesktopH:new(nil)
    end
    ev.chat_info = chat_info
    self.ControllerMgr.ViewMgr:sendEv(ev)
end

---------------------------------------
function ControllerDeskH:receiveInvitePlayerEnterDesktop(desktop_guid, desktop_filter)
    self.ControllerMgr.RPC:RPC0(CommonMethodType.DesktopHRequestLeave)
    local t_encode = self.ControllerMgr.Listener.Json.encode(desktop_filter)
    local map_param = {}
    map_param[0] = desktop_guid
    map_param[1] = t_encode
    if (self.FTaskLeaveDesktopH == nil)
    then
        self.FTaskLeaveDesktopH = CS.Casinos.FTMgr.Instance:startTask(0)
    end
    if (self.FTaskerLeaveDesktopH ~= nil)
    then
        self.FTaskerLeaveDesktopH:cancelTask()
    end
    self.FTaskerLeaveDesktopH = CS.Casinos.FTMgr.Instance:whenAll(map_param,
            function(map_param)
                self:playerLeaveDesktopH(map_param)
            end
    , self.FTaskLeaveDesktopH)
end

---------------------------------------
function ControllerDeskH:canLeaveImmediately()
    local can_leave_immediately = false
    if (self.IsBankPlayer)
    then
        if (self.DesktopHState == _eDesktopHState.Rest)
        then
            can_leave_immediately = true
        end
    else
        if (self.TotalBetGolds == 0)
        then
            can_leave_immediately = true
        end
    end

    return can_leave_immediately
end

---------------------------------------
function ControllerDeskH:getSelfTotalBetGolds()
    return self.TotalBetGolds
end

---------------------------------------
function ControllerDeskH:clearDesktopHundred(need_createmainui)
    self.ListDesktopChat = {}
    self.FTaskLeaveDesktopH = nil
    self.MapBetPotSelfBetGolds = {}
    self.MapBetPotStandPlayerBetGolds = {}
    self.ListBeBankPlayer = {}
    self.MapSeatPlayer = {}
    self.BankPlayer = nil
    self.MapBetPotWinlooseRecord = {}
    self.TotalBetGolds = 0
    self.SeatIndex = 255
    self.IsBankPlayer = false
    self.DesktopHState = _eDesktopHState.Idle
    self.CurrentUnSendDesktopMsg = ""
    local ui_desktoph = self.ControllerMgr.ViewMgr:getView("DesktopH")
    self.ControllerMgr.ViewMgr:destroyView(ui_desktoph)
    if (need_createmainui)
    then
        self.ControllerPlayer:createMainUi()
        self.ControllerIM:setMainUiIMInfo()
    end

    self.MapBetRepeatInfo = {}
    if (self.DesktopHBase ~= nil)
    then
        self.DesktopHBase:onDestroy()
        self.DesktopHBase = nil
    end
    self.DesktopHGuid = ""
end

---------------------------------------
function ControllerDeskH:betGold(bet_betpot_index, bet_golds)
    local max_percent = self.DesktopHBase:getMaxCannotBetPecent()
    local total_chips = self.ControllerActor.PropGoldAcc:get()
    local already_bet_chips = 0
    if (self.MapBetPotSelfBetGolds[bet_betpot_index] ~= nil)
    then
        already_bet_chips = self.MapBetPotSelfBetGolds[bet_betpot_index]
    end
    local after_bet_chips = self.TotalBetGolds + bet_golds
    if (after_bet_chips * max_percent > total_chips + self.TotalBetGolds)
    then
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("BetLimitTips"))
        return false
    end
    self.TotalBetGolds = self.TotalBetGolds + bet_golds
    already_bet_chips = already_bet_chips + bet_golds

    self.MapBetPotSelfBetGolds[bet_betpot_index] = already_bet_chips
    self.MapCurrentRoundSelfBetInfo[bet_betpot_index] = already_bet_chips
    local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHBet")
    if (ev == nil)
    then
        ev = EvEntityDesktopHBet:new(nil)
    end
    ev.bet_golds = bet_golds
    ev.already_betgolds = already_bet_chips
    ev.bet_potindex = bet_betpot_index
    self.ControllerMgr.ViewMgr:sendEv(ev)
    return true
end

---------------------------------------
-- 更新可下注筹码，AccGold不够，某些筹码置灰
function ControllerDeskH:updateSuitBetOperateId()
    if (self.DesktopHBase ~= nil)
    then
        local map_canoperateid = self.MapCanOperateId
        local list_operatesid = self.DesktopHBase:getBetOperateId()
        for key, value in pairs(list_operatesid) do
            map_canoperateid[value] = false
        end

        local total_chips = self.ControllerActor.PropGoldAcc:get()
        local max_percent_gold = self.DesktopHBase:getMaxCannotBetPecent()
        local max_betchips = total_chips / max_percent_gold

        local operate_id = -1
        local map_changed_operate = {}
        for i = 1, #list_operatesid - 1 do
            local operatefloor = list_operatesid[i]
            local operateceiling = list_operatesid[i + 1]
            local can_operate = map_canoperateid[operatefloor]
            local operatefloor_gold = self.DesktopHBase:getOperateGold(operatefloor)
            local operateceiling_gold = self.DesktopHBase:getOperateGold(operateceiling)
            if (max_betchips >= operatefloor_gold)
            then
                if (max_betchips < operateceiling_gold)
                then
                    operate_id = operatefloor
                end

                map_canoperateid[operatefloor] = true
                if (can_operate == false)
                then
                    map_changed_operate[operatefloor] = true
                end
            else
                map_canoperateid[operatefloor] = false
                if (can_operate)
                then
                    map_changed_operate[operatefloor] = false
                end
            end
        end
        local max_operateid = list_operatesid[#list_operatesid]
        local max_can_operate = map_canoperateid[max_operateid]
        local max_operategold = self.DesktopHBase:getOperateGold(max_operateid)
        if (max_betchips >= max_operategold)
        then
            map_canoperateid[max_operateid] = true
            if (max_can_operate == false)
            then
                map_changed_operate[max_operateid] = true
            end

            operate_id = max_operateid
        else
            map_canoperateid[max_operateid] = false
            if (max_can_operate)
            then
                map_changed_operate[max_operateid] = false
            end
        end

        if (self.CurrentTbBetOperateId ~= -1)
        then
            local if_current_canoperate = map_canoperateid[self.CurrentTbBetOperateId]
            if (if_current_canoperate == false)
            then
                self.CurrentTbBetOperateId = operate_id
            end
        else
            CS.UnityEngine.PlayerPrefs.SetInt("CurrentTbBetOperateIdDesktopH", operate_id)
            self.CurrentTbBetOperateId = operate_id
        end
        if (LuaHelper:GetTableCount(map_changed_operate) > 0)
        then
            local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDesktopHBetOperateTypeChange")
            if (ev == nil)
            then
                ev = EvEntityDesktopHBetOperateTypeChange:new(nil)
            end
            ev.map_changeoperate = map_changed_operate
            self.ControllerMgr.ViewMgr:sendEv(ev)
        end
    end
end

---------------------------------------
function ControllerDeskH:playerLeaveDesktopH(map_param)
    local desktop_guid = tostring(map_param[0])
    local f = tostring(map_param[1])
    local t_decode = self.ControllerMgr.Listener.Json.decode(f)
    local desktop_filter = DesktopFilter:new(nil)
    desktop_filter:setData(t_decode)
    local controller_lobby = self.ControllerMgr:GetController("Lobby")
    controller_lobby:requestEnterDesktop(desktop_guid, false, 255, desktop_filter:getData4Pack())
    self.FTaskerLeaveDesktopH = nil
end

---------------------------------------
function ControllerDeskH:regDesktopHBaseFactory(desktoph_fac)
    self.MapDesktopHBaseFac[desktoph_fac:GetName()] = desktoph_fac
end

---------------------------------------
function ControllerDeskH:GetDesktopHBaseFactory(fac_name)
    return self.MapDesktopHBaseFac[fac_name]
end

---------------------------------------
function ControllerDeskH:regDesktopHCardTypeBaseFactory(desktoph_fac)
    self.MapDesktopHCardTypeBaseFac[desktoph_fac:GetName()] = desktoph_fac
end

---------------------------------------
function ControllerDeskH:GetDesktopHCardTypeBaseFactory(fac_name)
    return self.MapDesktopHCardTypeBaseFac[fac_name]
end

---------------------------------------
ControllerDeskHFactory = ControllerFactory:new()

---------------------------------------
function ControllerDeskHFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerName = "DeskH"
    return o
end

---------------------------------------
function ControllerDeskHFactory:createController(controller_mgr, controller_data, guid)
    local controller = ControllerDeskH:new(nil, controller_mgr, controller_data, guid)
    return controller
end