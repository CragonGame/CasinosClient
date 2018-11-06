-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ControllerLotteryTicket = ControllerBase:new(nil)

---------------------------------------
function ControllerLotteryTicket:new(o, controller_mgr, controller_data, guid)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Context = Context
    self.ControllerData = controller_data
    self.ControllerMgr = controller_mgr
    self.Guid = guid
    self.ViewMgr = ViewMgr:new(nil)
    self.QUE_BETPOT_WINLOOSE_RESULT_COUNT = 10
    self.BetStateTm = 0
    self.UpdateUiTime = 1
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.TimerUpdate = nil
    return o
end

---------------------------------------
function ControllerLotteryTicket:OnCreate()
    self.ViewMgr:BindEvListener("EvUiClickLeaveLotteryTicket", self)
    self.ViewMgr:BindEvListener("EvLotteryTicketClickBetOperateType", self)
    self.ViewMgr:BindEvListener("EvEntityRequestGetLotteryTicketData", self)
    self.ViewMgr:BindEvListener("EvLotteryTicketClickRewardPotBtn", self)
    self.ViewMgr:BindEvListener("EvLotteryTicketBet", self)
    self.ViewMgr:BindEvListener("EvLotteryTicketRepeatBet", self)
    self.ViewMgr:BindEvListener("EvEntityGoldChanged", self)
    self.ControllerActor = self.ControllerMgr:GetController("Actor")
    self.ListLotteryTicketWinlooseResult = {}
    self.MapCanOperateId = {}
    self.MapCurrentRoundBetInfo = {}
    self.MapBetRepeatInfo = {}
    self.MapTotalBetGolds = {}
    local operate_id = -1
    if (CS.UnityEngine.PlayerPrefs.HasKey("CurrentTbBetOperateIdLottery")) then
        operate_id = CS.UnityEngine.PlayerPrefs.GetInt("CurrentTbBetOperateIdLottery")
    end
    self.CurrentTbBetOperateId = operate_id
    self.BetStateTm = 0
    self.UpdateUiTm = 0
    self.MapLotteryTicketBaseFac = {}

    self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(100, self, self._timerUpdate)

    self:regLotteryTicketBaseFactory(UiLotteryTicketTexasFactory:new(nil))
    local rpc = self.ControllerMgr.RPC
    local m_c = CommonMethodType
    rpc:RegRpcMethod2(m_c.LotteryTicketNotifyInit, function(state, tm)
        self:OnLotteryTicketNotifyInit(state, tm)
    end)
    rpc:RegRpcMethod0(m_c.LotteryTicketNotifyClose, function()
        self:OnLotteryTicketNotifyClose()
    end)
    rpc:RegRpcMethod1(m_c.LotteryTicketNotifyBet, function(tm)
        self:OnLotteryTicketNotifyBet(tm)
    end)
    rpc:RegRpcMethod2(m_c.LotteryTicketNotifyGameEndDetail, function(gameend_detail, me_win_gold)
        self:OnLotteryTicketNotifyGameEndDetail(gameend_detail, me_win_gold)
    end)
    rpc:RegRpcMethod0(m_c.LotteryTicketNotifyGameEndSimple, function()
        self:OnLotteryTicketNotifyGameEndSimple()
    end)
    rpc:RegRpcMethod2(m_c.LotteryTicketNotifyUpdateBetInfo, function(map_allbetpot, map_self_betinfo)
        self:OnLotteryTicketNotifyUpdateBetInfo(map_allbetpot, map_self_betinfo)
    end)
    rpc:RegRpcMethod2(m_c.LotteryTicketResponseSnapshot, function(lotteryticket_data, my_betinfo)
        self:OnLotteryTicketResponseSnapshot(lotteryticket_data, my_betinfo)
    end)
    rpc:RegRpcMethod1(m_c.LotteryTicketResponseGetRewardPotPlayerInfo, function(list_playerinfo)
        self:OnLotteryTicketResponseGetRewardPotPlayerInfo(list_playerinfo)
    end)
    rpc:RegRpcMethod1(m_c.LotteryTicketResponseSetCardsType, function(result)
        self:OnLotteryTicketResponseSetCardsType(result)
    end)
end

---------------------------------------
function ControllerLotteryTicket:OnDestroy()
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ControllerLotteryTicket:OnHandleEv(ev)
    if (ev.EventName == "EvUiClickLeaveLotteryTicket")
    then
        self:RequestChangeLotteryTicketPlayerState2Simple()
    elseif (ev.EventName == "EvLotteryTicketClickBetOperateType")
    then
        CS.UnityEngine.PlayerPrefs.SetInt("CurrentTbBetOperateIdLottery", ev.tb_bet_operateid)
        self.CurrentTbBetOperateId = ev.tb_bet_operateid
        local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityLotteryTicketCurrentBetOperateTypeChange")
        if (ev == nil)
        then
            ev = EvEntityLotteryTicketCurrentBetOperateTypeChange:new(nil)
        end
        self.ControllerMgr.ViewMgr:SendEv(ev)
    elseif (ev.EventName == "EvEntityRequestGetLotteryTicketData")
    then
        self.ControllerMgr.RPC:RPC0(CommonMethodType.LotteryTicketSnapshot)
    elseif (ev.EventName == "EvLotteryTicketClickRewardPotBtn")
    then
        self.ControllerMgr.RPC:RPC0(CommonMethodType.LotteryTicketGetRewardPotPlayerInfo)
    elseif (ev.EventName == "EvLotteryTicketBet")
    then
        self:RequestBet(ev.bet_betpot_index)
    elseif (ev.EventName == "EvLotteryTicketRepeatBet")
    then
        self.ControllerMgr.RPC:RPC1(CommonMethodType.LotteryTicketRequestBetRepeat, self.MapBetRepeatInfo)
    elseif (ev.EventName == "EvEntityGoldChanged")
    then
        self:updateSuitBetOperateId()
    end
end

---------------------------------------
function ControllerLotteryTicket:_timerUpdate(tm)
    if (self.BetStateTm > 0) then
        self.BetStateTm = self.BetStateTm - tm
        if (self.BetStateTm < 0) then
            self.BetStateTm = 0
        end
    end

    self.UpdateUiTm = self.UpdateUiTm + tm
    if (self.UpdateUiTm >= self.UpdateUiTime) then
        self.UpdateUiTm = 0
        local tm1 = math.ceil(self.BetStateTm)
        local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityLotteryTicketUpdateTm")
        if (ev == nil) then
            ev = EvEntityLotteryTicketUpdateTm:new(nil)
        end
        ev.tm = tm1
        self.ControllerMgr.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
function ControllerLotteryTicket:OnLotteryTicketNotifyInit(state, tm)
    self.LotteryTicketState = state
    self.BetStateTm = tm
    self.MapCurrentRoundBetInfo = {}
    self.MapTotalBetGolds = {}
    self.MapBetRepeatInfo = {}
    self.TotalBetGolds = 0
end

---------------------------------------
function ControllerLotteryTicket:OnLotteryTicketNotifyClose()
    self.LotteryTicketState = LotteryTicketStateEnum.Close
    self.MapCurrentRoundBetInfo = {}
    self.MapTotalBetGolds = {}
    self.MapBetRepeatInfo = {}
    self.TotalBetGolds = 0
end

---------------------------------------
function ControllerLotteryTicket:OnLotteryTicketNotifyBet(tm)
    self.LotteryTicketState = LotteryTicketStateEnum.Bet
    self.MapTotalBetGolds = {}
    self.BetStateTm = tm
    self.TotalBetGolds = 0
    self.MapBetRepeatInfo = {}
    for key, value in pairs(self.MapCurrentRoundBetInfo) do
        self.MapBetRepeatInfo[tostring(key)] = value
    end
    self.MapCurrentRoundBetInfo = {}
    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityLotteryTicketBetState")
    if (ev == nil)
    then
        ev = EvEntityLotteryTicketBetState:new(nil)
    end
    ev.map_betrepeatinfo = self.MapBetRepeatInfo
    self.ControllerMgr.ViewMgr:SendEv(ev)
end

---------------------------------------
-- 游戏详细结算
function ControllerLotteryTicket:OnLotteryTicketNotifyGameEndDetail(gameend_detail, me_win_gold)
    self.BetStateTm = 0
    self.MapTotalBetGolds = {}
    self.TotalBetGolds = 0
    self.LotteryTicketState = LotteryTicketStateEnum.GameEnd
    local g_d = BLotteryTicketGameEndDetail:new(nil)
    g_d:setData(gameend_detail)
    local view_lotteryticket = self.ControllerMgr.ViewMgr:GetView("LotteryTicket")
    if view_lotteryticket ~= nil then
        local l_c = {}
        for i, v in pairs(g_d.ListCard) do
            local card = CS.Casinos.Card(v.suit, v.type)
            table.insert(l_c, card)
        end
        local card_type = view_lotteryticket:GetCardType(l_c)
        table.insert(self.ListLotteryTicketWinlooseResult, 1, card_type)
    end

    local l = #self.ListLotteryTicketWinlooseResult
    if (l > self.QUE_BETPOT_WINLOOSE_RESULT_COUNT) then
        table.remove(self.ListLotteryTicketWinlooseResult, l)
    end

    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityLotteryTicketGameEndState")
    if (ev == nil) then
        ev = EvEntityLotteryTicketGameEndState:new(nil)
    end
    ev.gameend_detail = g_d
    ev.me_wingold = me_win_gold
    self.ControllerMgr.ViewMgr:SendEv(ev)
    self.RewardPotGolds = g_d.RewardPotGold
end

---------------------------------------
function ControllerLotteryTicket:OnLotteryTicketNotifyGameEndSimple()
    self.BetStateTm = 0
    self.MapTotalBetGolds = {}
    self.TotalBetGolds = 0
    if (self.LotteryTicketState == LotteryTicketStateEnum.Bet)
    then
        self.LotteryTicketState = LotteryTicketStateEnum.GameEnd
        local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityLotteryTicketGameEndStateSimple")
        if (ev == nil)
        then
            ev = EvEntityLotteryTicketGameEndStateSimple:new(nil)
        end
        self.ControllerMgr.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
function ControllerLotteryTicket:OnLotteryTicketNotifyUpdateBetInfo(map_allbetpot, map_self_betinfo)
    for key, value in pairs(map_self_betinfo) do
        self.MapTotalBetGolds[key] = value
        self.MapCurrentRoundBetInfo[key] = value
        self.TotalBetGolds = self.TotalBetGolds + value
    end
    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityLotteryTicketUpdateBetPotBetInfo")
    if (ev == nil)
    then
        ev = EvEntityLotteryTicketUpdateBetPotBetInfo:new(nil)
    end
    ev.map_allbetpot = map_allbetpot
    ev.map_self_betinfo = map_self_betinfo
    self.ControllerMgr.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerLotteryTicket:OnLotteryTicketResponseSnapshot(lotteryticket_data, my_betinfo)
    local l_d = BLotteryTicketData:new(nil)
    l_d:setData(lotteryticket_data)
    self:updateSuitBetOperateId()
    self.LotteryTicketState = l_d.State
    if (l_d.ListHistory ~= nil)
    then
        self.ListLotteryTicketWinlooseResult = l_d.ListHistory
    end

    for key, value in pairs(my_betinfo) do
        self.MapTotalBetGolds[key] = value
        self.MapCurrentRoundBetInfo[key] = value
        self.TotalBetGolds = self.TotalBetGolds + value
    end
    self.RewardPotGolds = l_d.RewardPotGold
    if (self.LotteryTicketState == LotteryTicketStateEnum.Bet)
    then
        self.BetStateTm = l_d.StateLeftTm
    else
        self.BetStateTm = 0
    end
    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityGetLotteryTicketDataSuccess")
    if (ev == nil)
    then
        ev = EvEntityGetLotteryTicketDataSuccess:new(nil)
    end
    ev.lotteryticket_data = l_d
    self.ControllerMgr.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerLotteryTicket:OnLotteryTicketResponseGetRewardPotPlayerInfo(list_playerinfo)
    ViewHelper:UiEndWaiting()
    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityLotteryTicketGetRewardPotInfo")
    if (ev == nil)
    then
        ev = EvEntityLotteryTicketGetRewardPotInfo:new(nil)
    end
    local t_p = nil
    if list_playerinfo ~= nil then
        t_p = {}
        for i, v in pairs(list_playerinfo) do
            local l_p = BLotteryTicketPlayerInfo:new(nil)
            l_p:setData(v)
            table.insert(t_p, l_p)
        end
    end
    ev.list_playerinfo = t_p
    ev.reward_totalgolds = self.RewardPotGolds
    self.ControllerMgr.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerLotteryTicket:OnLotteryTicketResponseSetCardsType(result)
end

---------------------------------------
function ControllerLotteryTicket:RequestChangeLotteryTicketPlayerState2Simple()
    self.ControllerMgr.RPC:RPC0(CommonMethodType.LotteryTicketChangePlayerState2Simple)
    self:ClearLotteryTicketundred()
end

---------------------------------------
function ControllerLotteryTicket:updateSuitBetOperateId()
    local map_tblotteryticket_operate = self.ControllerMgr.TbDataMgr:GetMapData("LotteryTicketBetOperate")
    if (map_tblotteryticket_operate == nil)
    then
        return
    end
    local list_operates = map_tblotteryticket_operate
    for key, value in pairs(list_operates) do
        self.MapCanOperateId[key] = false
    end

    local total_chips = self.ControllerActor.PropGoldAcc:get()
    local max_betchips = total_chips

    local operate_id = -1
    local map_changed_operate = {}
    for i = 1, #list_operates - 1 do
        local operatefloor = list_operates[i]
        local operateceiling = list_operates[i + 1]
        local can_operate = self.MapCanOperateId[operatefloor.Id]
        if (max_betchips >= operatefloor.OperateGolds)
        then
            if (max_betchips < operateceiling.OperateGolds)
            then
                operate_id = operatefloor.Id
            end

            self.MapCanOperateId[operatefloor.Id] = true
            if (can_operate == false)
            then
                map_changed_operate[operatefloor.Id] = true
            end
        else
            self.MapCanOperateId[operatefloor.Id] = false
            if (can_operate)
            then
                map_changed_operate[operatefloor.Id] = false
            end
        end
    end

    local max_operate = list_operates[#list_operates]
    local max_can_operate = self.MapCanOperateId[max_operate.Id]
    if (max_betchips >= max_operate.OperateGolds)
    then
        self.MapCanOperateId[max_operate.Id] = true
        if (max_can_operate == false)
        then
            map_changed_operate[max_operate.Id] = true
        end
        operate_id = max_operate.Id
    else
        self.MapCanOperateId[max_operate.Id] = false
        if (max_can_operate)
        then
            map_changed_operate[max_operate.Id] = false
        end
    end

    local current_operate_change = false
    if (self.CurrentTbBetOperateId ~= -1)
    then
        local if_current_canoperate = self.MapCanOperateId[self.CurrentTbBetOperateId]
        if (if_current_canoperate == false)
        then
            self.CurrentTbBetOperateId = operate_id
            current_operate_change = true
        end
    else
        CS.UnityEngine.PlayerPrefs.SetInt("CurrentTbBetOperateIdLottery", operate_id)
        self.CurrentTbBetOperateId = operate_id
    end

    if (LuaHelper:GetTableCount(map_changed_operate) > 0 or current_operate_change)
    then
        local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityLotteryTicketBetOperateTypeChange")
        if (ev == nil)
        then
            ev = EvEntityLotteryTicketBetOperateTypeChange:new(nil)
        end
        ev.map_changeoperate = map_changed_operate
        self.ControllerMgr.ViewMgr:SendEv(ev)
    end
end

---------------------------------------
function ControllerLotteryTicket:ClearLotteryTicketundred()
    self.ListLotteryTicketWinlooseResult = {}
    self.MapCurrentRoundBetInfo = {}
    self.MapBetRepeatInfo = {}
    self.MapTotalBetGolds = {}
    self.LotteryTicketState = LotteryTicketStateEnum.Close
    self.TotalBetGolds = 0
end

---------------------------------------
function ControllerLotteryTicket:RequestBet(bet_betpot_index)
    if (self.LotteryTicketState ~= LotteryTicketStateEnum.Bet)
    then
        return
    end

    if (self.CurrentTbBetOperateId == -1)
    then
        local tips = self.ViewMgr.LanMgr:getLanValue("Chip") ..
                self.ControllerMgr.LanMgr:getLanValue("NotEnough")
        ViewHelper:UiShowInfoFailed(tips)
        return
    end

    local tb_hundredoperategolds = self.ControllerMgr.TbDataMgr:GetData("LotteryTicketBetOperate", self.CurrentTbBetOperateId)
    local bet_golds = tb_hundredoperategolds.OperateGolds
    local total_golds = self.ControllerActor.PropGoldAcc:get()
    local already_bet_golds = 0
    for key, value in pairs(self.MapTotalBetGolds) do
        if (key == bet_betpot_index)
        then
            already_bet_golds = value
            break
        end
    end
    local after_bet_golds = already_bet_golds + bet_golds
    if (after_bet_golds > total_golds + self.TotalBetGolds)
    then
        return
    end

    self.TotalBetGolds = self.TotalBetGolds + bet_golds
    already_bet_golds = already_bet_golds + bet_golds
    self.MapTotalBetGolds[bet_betpot_index] = already_bet_golds
    self.MapCurrentRoundBetInfo[bet_betpot_index] = already_bet_golds

    local ev = self.ControllerMgr.ViewMgr:GetEv("EvEntityLotteryTicketBet")
    if (ev == nil)
    then
        ev = EvEntityLotteryTicketBet:new(nil)
    end
    ev.already_bet_chips = already_bet_golds
    ev.bet_potindex = bet_betpot_index
    self.ControllerMgr.ViewMgr:SendEv(ev)
    self.ControllerMgr.RPC:RPC2(CommonMethodType.LotteryTicketRequestBet, bet_betpot_index, bet_golds)
end

---------------------------------------
function ControllerLotteryTicket:RequestSetCardType(card_type)
    self.ControllerMgr.RPC:RPC1(CommonMethodType.LotteryTicketSetCardsType, card_type)
end

---------------------------------------
function ControllerLotteryTicket:regLotteryTicketBaseFactory(l_fac)
    self.MapLotteryTicketBaseFac[l_fac:GetName()] = l_fac
end

---------------------------------------
function ControllerLotteryTicket:GetLotteryTicketBaseFactory(fac_name)
    return self.MapLotteryTicketBaseFac[fac_name]
end

---------------------------------------
ControllerLotteryTicketFactory = ControllerFactory:new()

---------------------------------------
function ControllerLotteryTicketFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ControllerName = "LotteryTicket"
    return o
end

---------------------------------------
function ControllerLotteryTicketFactory:createController(controller_mgr, controller_data, guid)
    local controller = ControllerLotteryTicket:new(nil, controller_mgr, controller_data, guid)
    return controller
end