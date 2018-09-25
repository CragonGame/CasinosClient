-- Copyright(c) Cragon. All rights reserved.

DesktopTexasClassic = DesktopTypeBase:new(nil)

function DesktopTexasClassic:new(o, desktop_base, co_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ControllerPlayer = co_mgr:GetController("Player")
    o.ControllerActor = co_mgr:GetController("Actor")
    o.ControllerLobby = co_mgr:GetController("Lobby")
    o.ControllerDesktop = co_mgr:GetController("Desk")
    o.DesktopBase = desktop_base
    o.Name = "TexasClassic"

    return o
end

function DesktopTexasClassic:onDestroy(need_createmainui)
    local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
    if (need_createmainui)
    then
        local ui_classicmodel = view_mgr:getView("ClassicModel")
        if (ui_classicmodel == nil)
        then
            ui_classicmodel = view_mgr:createView("ClassicModel")
            ui_classicmodel:setLobbyModel()
        end

        CS.Casinos.CasinosContext.Instance:Play("MainBg1", CS.Casinos._eSoundLayer.Background)
    end
end

function DesktopTexasClassic:onUpdate(elapsed_tm)
end

function DesktopTexasClassic:onHandleEv(ev)
    if (ev ~= nil)
    then
        if (ev.EventName == "EvUiClickSeat")
        then
            local enough = TexasHelper:enoughChip4DesktopBetMin(self.ControllerDesktop.ControllerMgr.TbDataMgr, self.ControllerActor.PropGoldAcc:get(),
                    self.NormalTexas.DesktopTbId)
            if (enough == false)
            then
                local msg_box = self.ControllerDesktop.ControllerMgr.ViewMgr:createView("MsgBox")
                local tips = self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr:getLanValue("ChipNotEnoughTips")
                tips = string.format(tips, UiChipShowHelper:getGoldShowStr(self.BetMin, self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr.LanBase))
                local title = self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr:getLanValue("ChipNotEnough")
                msg_box:showMsgBox1(title, tips,
                        function(bo)
                            if (bo)
                            then
                                local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
                                local ev1 = view_mgr:getEv("EvUiClickShop")
                                if (ev1 == nil)
                                then
                                    ev1 = EvUiClickShop:new(nil)
                                end
                                view_mgr:sendEv(ev1)
                            end
                        end
                )
            else
                local index_ = ev.seat_index
                self.DesktopBase:createBetGame(
                        function(ok, chip)
                            if (ok == false)
                            then
                                return
                            end

                            local real_index = index_
                            if (self.DesktopBase.MeP ~= nil)
                            then
                                if (self.DesktopBase.MeP.UiMiddleSeatIndexRealIndex ~= 255)
                                then
                                    real_index = (real_index - math.floor(self.DesktopBase.SeatNum / 2) + self.DesktopBase.MeP.UiMiddleSeatIndexRealIndex)
                                    if (real_index < 0)
                                    then
                                        real_index = real_index + self.DesktopBase.SeatNum
                                    end
                                end
                            end

                            local seatdown_info = DesktopSitdownRequest:new(nil)
                            seatdown_info.player_guid = self.DesktopBase.MeP.Guid
                            seatdown_info.seat_index = math.ceil(real_index)
                            seatdown_info.user_data1 = tostring(chip)
                            seatdown_info.user_data2 = ""
                            self.DesktopBase:requestPlayerSitdown(seatdown_info)
                        end, self.BetMin, self.BetMax
                )
            end
        elseif (ev.EventName == "EvUiClickPlayerReturn")
        then
            if (self.DesktopBase.MeP.PlayerDataDesktop.Stack < self.BetMin)
            then
                local enough = TexasHelper:enoughChip4DesktopBetMin(self.ControllerDesktop.ControllerMgr.TbDataMgr,
                        self.ControllerActor.PropGoldAcc:get(), self.NormalTexas.DesktopTbId)

                if (enough == false)
                then
                    local msg_box = self.ControllerDesktop.ControllerMgr.ViewMgr:createView("MsgBox")
                    local tips = self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr:getLanValue("ChipNotEnoughTips")
                    tips = string.format(tips, UiChipShowHelper:getGoldShowStr(self.BetMin, self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr.LanBase))
                    local title = self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr:getLanValue("ChipNotEnough")
                    msg_box:showMsgBox1(title, tips,
                            function(bo)
                                if (bo)
                                then
                                    local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
                                    local ev = view_mgr:getEv("EvUiClickShop")
                                    if (ev == nil)
                                    then
                                        ev = EvUiClickShop:new(nil)
                                    end
                                    view_mgr:sendEv(ev)
                                end
                            end
                    )
                else
                    self.DesktopBase:createBetGame(
                            function(ok, chip)
                                if (ok == false)
                                then
                                    self.DesktopBase:requestPlayerOb()
                                    return
                                else
                                    self.DesktopBase:requestPlayerPushStack(chip)
                                end
                            end, self.BetMin, self.BetMax
                    )
                end
            else
                self.DesktopBase:requestPlayerReturn(self.DesktopBase.MeP.PlayerDataDesktop.Stack)
            end
        elseif (ev.EventName == "EvUiCreateExchangeChip")
        then
            local enough = TexasHelper:enoughChip4DesktopBetMin(self.ControllerDesktop.ControllerMgr.TbDataMgr, self.ControllerActor.PropGoldAcc:get(),
                    self.NormalTexas.DesktopTbId)

            local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
            if (enough == false)
            then
                local msg_box = view_mgr:createView("MsgBox")
                local tips = self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr:getLanValue("ChipNotEnoughTips")
                tips = string.format(tips, UiChipShowHelper:getGoldShowStr(self.BetMin, self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr.LanBase))
                local title = self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr:getLanValue("ChipNotEnough")
                msg_box:showMsgBox1(title, tips,
                        function(bo)
                            if (bo)
                            then
                                local view_mgr = self.ControllerDesktop.ControllerMgr.ViewMgr
                                local ev = view_mgr:getEv("EvUiClickShop")
                                if (ev == nil)
                                then
                                    ev = EvUiClickShop:new(nil)
                                end
                                view_mgr:sendEv(ev)
                            end
                        end
                )
            else
                local chip_transaction = view_mgr:createView("ChipOperate")
                local chips = self.BetMax
                chip_transaction:setChipsInfo(self.ControllerActor.PropGoldAcc:get(), chips
                , self.BetMin, CS.Casinos._eChipOperateType.Exchange, nil,
                        function(ok, chip)
                            if (ok == false)
                            then
                                return
                            end
                            self.DesktopBase:requestPlayerPushStack(chip)
                        end
                )
            end
        elseif (ev.EventName == "EvUiInviteFriendPlayTogether")
        then
            local self_desktop_etguid = self.DesktopBase.DesktopGuid

            if (CS.System.String.IsNullOrEmpty(self_desktop_etguid) == false)
            then
                if (self_desktop_etguid == ev.friend_desktopguid)
                then
                    ViewHelper:UiShowInfoSuccess(self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr:getLanValue("SameTableTips"))
                else
                    local desktop_filter_texas = DesktopFilterTexas:new(nil)
                    desktop_filter_texas.desktop_tableid = self.NormalTexas.DesktopTbId
                    desktop_filter_texas.game_speed = self.DesktopBase.GameSpeed
                    desktop_filter_texas.is_seat_full = false
                    desktop_filter_texas.is_vip = self.DesktopBase.IsVIP
                    local seat_n = TexasDesktopSeatNum.Nine
                    if (self.DesktopBase.SeatNum == 5)
                    then
                        seat_n = TexasDesktopSeatNum.Five
                    end
                    desktop_filter_texas.seat_num = seat_n

                    local desktop_filter = DesktopFilter:new(nil)
                    desktop_filter.FactoryName = "Texas"
                    desktop_filter.IncludeFull = false
                    desktop_filter.FilterData = self.ControllerDesktop.ControllerMgr:packData(desktop_filter_texas:getData4Pack())

                    self.ControllerDesktop:requestInvitePlayerEnterDesktop(
                            ev.friend_guid, self.DesktopBase.DesktopGuid, desktop_filter:getData4Pack(), self.DesktopBase:getPlayerCount())
                end
            end
        end
    end
end

function DesktopTexasClassic:SetDesktopSnapshotData(snapshot_data)
    self.NormalTexas = snapshot_data.normal_texas
    local tbdata_desktopinfo = self.ControllerPlayer.ControllerMgr.TbDataMgr:GetData("DesktopInfoTexas", snapshot_data.normal_texas.DesktopTbId)
    self.BetMin = tbdata_desktopinfo.BetMin
    self.BetMax = tbdata_desktopinfo.BetMax
    self.SmallBlind = tbdata_desktopinfo.SmallBlind
    self.BigBlind = tbdata_desktopinfo.BigBlind
    self.DesktopFee = tbdata_desktopinfo.DesktopFee
    self.DesktopTips = string.format(tbdata_desktopinfo.Tips, snapshot_data.seat_num)
    self.ServerAutoAction = false
end

function DesktopTexasClassic:DesktopUser(method_id, method_data)
    if (method_id == MethodTypeTexasDesktop.PlayerPushStackNotify)
    then
        if (CS.System.String.IsNullOrEmpty(self.DesktopBase.DesktopGuid))
        then
            return
        end
        local data1 = self.ControllerDesktop.ControllerMgr:unpackData(method_data)
        local data = DesktopNotifyPlayerPushStackTexas:new(nil)
        data:setData(data1)
        local player_texas = self.DesktopBase.MapPlayerTexas[data.player_guid]
        if (player_texas ~= nil)
        then
            player_texas:pushStack(data.stack)
        end
    elseif (method_id == MethodTypeTexasDesktop.PlayerPushStackResultNotify)
    then
        local result = self.ControllerDesktop.ControllerMgr:unpackData(method_data)
        if (result == ProtocolResult.Success)
        then
            ViewHelper:UiShowInfoSuccess(self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr:getLanValue("ExchangeMoneySuccess"))
        elseif (result == ProtocolResult.GiveChipNotEnoughChip)
        then
            ViewHelper:UiShowInfoFailed(self.ControllerDesktop.ControllerMgr.ViewMgr.LanMgr:getLanValue("ExchangeMoneyFailed"))
        end
    end
end

function DesktopTexasClassic:preflopBegin()

end

function DesktopTexasClassic:requestPlayerReturn(chip)
    self.ControllerDesktop:RequestPlayerReturn(chip)
end

function DesktopTexasClassic:_userRequest(method_info)
    self.ControllerDesktop:UserRequest("Texas", method_info:getData4Pack())
end

function DesktopTexasClassic:checkMeStack(stack)
    if (stack <= self.DesktopFee)
    then
        local player_left_golds = self.DesktopBase.ControllerActor.PropGoldAcc:get()
        if (player_left_golds < self.BetMin)
        then
            -- 筹码（账户）不够
            local msg_box = self.DesktopBase.ControllerPlayer.ControllerMgr.ViewMgr:createView("MsgBox")
            local tips = self.DesktopBase.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("ChipNotEnoughTips")
            tips = string.format(tips, UiChipShowHelper:getGoldShowStr( self.BetMin, self.DesktopBase.ControllerDesktop.ControllerMgr.LanMgr.LanBase))
            local title = self.DesktopBase.ControllerDesktop.ControllerMgr.LanMgr:getLanValue("ChipNotEnough")
            msg_box:showMsgBox1(title, tips,
                    function(bo)
                        if (bo)
                        then
                            local view_mgr = self.DesktopBase.ControllerPlayer.ControllerMgr.ViewMgr
                            local ev = view_mgr:getEv("EvUiClickShop")
                            if (ev == nil)
                            then
                                ev = EvUiClickShop:new(nil)
                            end
                            view_mgr:sendEv(ev)
                        end
                    end
            )
        else
            -- 筹码（账户）足够
            self.DesktopBase:createBetGame(
                    function(ok, chip)
                        if (ok == false)
                        then
                            self.DesktopBase:requestPlayerOb()
                            return
                        else
                            self.DesktopBase:requestPlayerPushStack(chip)
                        end
                    end, self.BetMin, self.BetMax
            )
        end
    end
end

function DesktopTexasClassic:getFastBetKey()
    local desktop_id = self.NormalTexas.DesktopTbId
    local t_preflop = {}
    table.insert(t_preflop, desktop_id)
    table.insert(t_preflop, DesktopType.Classic)
    table.insert(t_preflop, "true")
    local t_not_preflop = {}
    table.insert(t_not_preflop, desktop_id)
    table.insert(t_not_preflop, DesktopType.Classic)
    table.insert(t_not_preflop, "false")

    return table.concat(t_preflop),table.concat(t_not_preflop)
end

DesktopTexasClassicFactory = DesktopTypeBaseFactory:new(nil)

function DesktopTexasClassicFactory:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self

    return o
end

function DesktopTexasClassicFactory:GetName()
    return "TexasClassic"
end

function DesktopTexasClassicFactory:CreateDesktopType(desktop_base, co_mgr)
    local l = DesktopTexasClassic:new(nil, desktop_base, co_mgr)
    return l
end