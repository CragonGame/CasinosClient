-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
DesktopHChair = {}

---------------------------------------
function DesktopHChair:new(o, ui_desktoph, co_chair, chair_index, is_left)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Context = Context
    o.ViewDesktopH = ui_desktoph
    o.ChairIndex = chair_index
    o.IsLeft = is_left
    o.GCoChair = co_chair
    o.MapWinLooseInfo = {}
    o.MapWinUiGolds = {}
    o.MapFTaskerGetWinGold = {}
    o.GCoChair.onClick:Add(
            function()
                o:_onClick()
            end
    )
    o.ItemDesktopHSeat = ItemDesktopHSeat:new(nil, o.GCoChair, o.ViewDesktopH.ViewMgr)
    o.GCoChatParent = o.GCoChair:GetChild("CoChatParent").asCom
    o.SeatPlayerInfoHundred = nil
    o.ItemChatDesktop = nil
    o.ViewMgr = ViewMgr:new(nil)
    return o
end

---------------------------------------
function DesktopHChair:Destroy()
    for k, v in pairs(self.MapWinUiGolds) do
        for g_k, g_v in pairs(v) do
            self.ViewDesktopH.DesktopHGoldPool:goldHEnPool(g_v)
        end
    end

    self.MapWinUiGolds = {}
    self.MapWinUiGolds = nil
    self:_cancelTask()
    self:_destroyUiHead()
end

---------------------------------------
function DesktopHChair:update(tm)
    if (self.ItemChatDesktop ~= nil) then
        self.ItemChatDesktop:update(tm)
    end
end

---------------------------------------
function DesktopHChair:playerChat(chat_info)
    if (self.SeatPlayerInfoHundred == nil) then
        return
    end

    if (chat_info.sender_etguid == self.SeatPlayerInfoHundred.PlayerInfoCommon.PlayerGuid) then
        local sorting_order = self.GCoChatParent.sortingOrder + self.ViewDesktopH.DesktopHGoldPool:getMaxGoldSortOrder()
        if (self.ItemChatDesktop == nil) then
            local co_chatname = "CoChatRight"
            if (self.IsLeft) then
                co_chatname = "CoChatLeft"
            end

            local pos = self.GCoChair:TransformPoint(self.GCoChatParent.xy, self.ViewDesktopH.UiDesktopChatParent.ComUi)
            self.ItemChatDesktop = self.ViewDesktopH.UiDesktopChatParent:addChat(co_chatname, self.ViewDesktopH.ComUi, pos)
            self.ItemChatDesktop:setChatTextAndSortingOrder(chat_info, sorting_order)
        else
            self.ItemChatDesktop:setChatTextAndSortingOrder(chat_info, sorting_order)
        end
    end
end

---------------------------------------
function DesktopHChair:playerSeatDown(player_info)
    for k, v in pairs(self.MapWinUiGolds) do
        for g_k, g_v in pairs(v) do
            self.ViewDesktopH.DesktopHGoldPool:goldHEnPool(g_v)
        end
    end

    self.MapWinUiGolds = {}
    self.MapWinLooseInfo = {}

    local player_changed = false
    if (player_info ~= nil) then
        if (self.SeatPlayerInfoHundred == nil) then
            player_changed = true
        else
            if (self.SeatPlayerInfoHundred.PlayerInfoCommon.PlayerGuid ~= player_info.PlayerInfoCommon.PlayerGuid) then
                player_changed = true
            end
        end
    end

    self.SeatPlayerInfoHundred = player_info

    if (self.SeatPlayerInfoHundred ~= nil) then
        if (self.ItemDesktopHSeat == nil) then
            self.ItemDesktopHSeat = ItemDesktopHSeat:new(nil, self.GCoChair)
        end

        self.ItemDesktopHSeat:setSeatPlayerData(self.SeatPlayerInfoHundred, self.ChairIndex, player_changed)
    else
        self:_destroyUiHead()
        if (self.ItemChatDesktop ~= nil) then
            self.ItemChatDesktop.GCoChat.visible = false
        end
    end
end

---------------------------------------
function DesktopHChair:updatePlayerGolds(golds)
    if (self.SeatPlayerInfoHundred ~= nil) then
        self.SeatPlayerInfoHundred.Gold = golds
    end

    if (self.ItemDesktopHSeat ~= nil) then
        self.ItemDesktopHSeat:updatePlayerGolds(golds)
    end
end

---------------------------------------
function DesktopHChair:betGolds(current_bet_operate, bet_potindex, gold_value)
    local bet_pot = self.ViewDesktopH:getDesktopHBetPot(bet_potindex)
    local from = self:getChairCenterPos()
    if (current_bet_operate ~= -1) then
        if (bet_pot ~= nil) then
            bet_pot:betGolds(from, gold_value)
        end
    else
        bet_pot:betGolds(from, gold_value)
    end

    self:_playBetGoldsAni()
end

---------------------------------------
function DesktopHChair:setSeatPlayerResultInfo(betpot_index, winloose_info)
    self.MapWinLooseInfo[betpot_index] = winloose_info
end

---------------------------------------
function DesktopHChair:showWinGoldsAni(pot_index, from)
    local winloose_info = self.MapWinLooseInfo[pot_index]
    if (winloose_info ~= nil) then
        if (winloose_info.winloose_gold <= 0) then
            return
        end

        local bet_pot = self.ViewDesktopH:getDesktopHBetPot(pot_index)
        local list_chips = self.MapWinUiGolds[pot_index]
        if (list_chips == nil) then
            list_chips = {}
        end

        self.ViewDesktopH:createGolds(list_chips, nil, winloose_info.winloose_gold, bet_pot, 9)
        self.MapWinUiGolds[pot_index] = list_chips

        local delay_tm = 0.0
        local delay_t = self.ViewDesktopH:getMoveIntervalTm(#list_chips)
        for k, v in pairs(list_chips) do
            local to = bet_pot:getRandomChipPos()
            v:initMove(from, to, DesktopHUiGold.MOVE_CHIP_TM, DesktopHUiGold.MOVE_SOUND, nil, nil, false, delay_tm, true)
            delay_tm = delay_tm + delay_t
        end

        local map_param = {}
        map_param[0] = winloose_info.winloose_gold
        map_param[1] = pot_index
        local t = CS.Casinos.FTMgr.Instance:startTask(DesktopHBetPot.GivePlayerAniTm - DesktopHBetPot.WinShowAniTm)
        local tasker = CS.Casinos.FTMgr.Instance:whenAll(map_param,
                function(map_param)
                    self:_playWinGoldAni(map_param)
                end,
                t)
        self.MapFTaskerGetWinGold[pot_index] = tasker
    end
end

---------------------------------------
function DesktopHChair:Reset()
    self.MapWinLooseInfo = {}
    for k, v in pairs(self.MapWinUiGolds) do
        for g_k, g_v in pairs(v) do
            self.ViewDesktopH.DesktopHGoldPool:goldHEnPool(g_v)
        end
    end
    self:_cancelTask()
    self.MapWinUiGolds = {}
end

---------------------------------------
function DesktopHChair:sendMagicExp(sender_guid, exp_tbid)
    local tb_magicexp = self.Context.TbDataMgr:GetData("UnitMagicExpression", exp_tbid)
    if (tb_magicexp == nil) then
        return
    end

    local from_pos = CS.Casinos.LuaHelper.GetVector2(0, 0)
    if (self.ViewDesktopH.DesktopHBankPlayer.BankPlayerDataDesktopH.PlayerInfoCommon.PlayerGuid == sender_guid) then
        from_pos = self.ViewDesktopH.DesktopHBankPlayer:getBankPlayerCenterPos()
    else
        local chair = self.ViewDesktopH:getDesktopHChairByGuid(sender_guid)
        if (chair ~= nil) then
            from_pos = chair:getChairCenterPos()
        else
            from_pos = self.ViewDesktopH.DesktopHStandPlayer:getStandPlayerCenterPos()
        end
    end

    local to_pos = self:getChairCenterPos()
    local ui_pool = self.ViewMgr:GetView("Pool")
    local item_magicsender = ui_pool:getMagicExpSender()
    self.ViewDesktopH.ComUi:AddChild(item_magicsender.GCoMagicExpSender)
    item_magicsender:sendMagicExp(from_pos, to_pos, exp_tbid)
end

---------------------------------------
function DesktopHChair:getChairCenterPos()
    local pos = self.GCoChair.xy
    pos.x = pos.x + self.GCoChair.width * self.GCoChair.scaleX / 2
    pos.y = pos.y + self.GCoChair.height * self.GCoChair.scaleY / 2
    return pos
end

---------------------------------------
function DesktopHChair:_playWinGoldAni(map_param)
    local pot_index = map_param[1]
    local list_gold = self.MapWinUiGolds[pot_index]
    if (list_gold ~= nil) then
        local to = self:getChairCenterPos()
        local delay_tm = 0.0
        local delay_t = self.ViewDesktopH:getMoveIntervalTm(#list_gold)
        for k, v in pairs(list_gold) do
            v:initMove(v.GCoGold.xy, to,
                    DesktopHUiGold.MOVE_CHIP_TM, DesktopHUiGold.MOVE_SOUND, nil, nil, true, delay_tm, false)
            delay_tm = delay_tm + delay_t
        end

        self.MapWinUiGolds[pot_index] = nil
        self.MapWinLooseInfo[pot_index] = nil
    end
end

---------------------------------------
function DesktopHChair:_cancelTask()
    for k, v in pairs(self.MapFTaskerGetWinGold) do
        if (v ~= nil) then
            v:cancelTask()
        end
    end
    self.MapFTaskerGetWinGold = {}
end

---------------------------------------
function DesktopHChair:_destroyUiHead()
    if (self.ItemDesktopHSeat ~= nil) then
        self.ItemDesktopHSeat:setSeatPlayerData(nil, 255, true)
    end
end

---------------------------------------
function DesktopHChair:_playBetGoldsAni()
    if (self.ViewDesktopH.FactoryName == "ZhongFB") then
        local chair_y = self.GCoChair.y
        if (CS.FairyGUI.GTween.IsTweening(self.GCoChair) == false) then
            self.GCoChair:TweenMoveY(chair_y + self.ViewDesktopH.BetAniX, 0.1):OnComplete(
                    function()
                        self.GCoChair:TweenMoveY(chair_y, 0.1)
                    end
            )
        end
    else
        local move_x = self.ViewDesktopH.BetAniX
        if (self.IsLeft == false) then
            move_x = -self.ViewDesktopH.BetAniX
        end

        local chair_x = self.GCoChair.x
        if (CS.FairyGUI.GTween.IsTweening(self.GCoChair) == false) then
            self.GCoChair:TweenMoveX(chair_x + move_x, 0.1):OnComplete(
                    function()
                        self.GCoChair:TweenMoveX(chair_x, 0.1)
                    end
            )
        end
    end
end

---------------------------------------
function DesktopHChair:_playerInfo(player_info, head_icon)
    self.ItemDesktopHSeat:updatePlayerIcon(head_icon)
end

---------------------------------------
function DesktopHChair:_onClick()
    if (self.SeatPlayerInfoHundred == nil) then
        if (self.ViewDesktopH.ControllerDesktopH.IsBankPlayer) then
            ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("BankerSit"))
            return
        end

        local ev = self.ViewMgr:GetEv("EvUiDesktopHSeatDown")
        if (ev == nil) then
            ev = EvUiDesktopHSeatDown:new(nil)
        end
        ev.seat_index = self.ChairIndex
        ev.min_golds = self.ViewDesktopH.UiDesktopHBase:getSeatDownMinGolds()
        self.ViewMgr:SendEv(ev)
    else
        local ui_profileother = self.ViewMgr:CreateView("PlayerProfile")
        ui_profileother:setPlayerGuid(CS.Casinos._ePlayerProfileType.DesktopH, self.SeatPlayerInfoHundred.PlayerInfoCommon.PlayerGuid,
                function(player_info, head_icon)
                    self:_playerInfo(player_info, head_icon)
                end
        )
    end
end