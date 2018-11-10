-- Copyright(c) Cragon. All rights reserved.
-- 管理本人信息，左下角头像；如果本人是有座玩家或庄家，同时还刷新本人座位或庄家头像相关信息

---------------------------------------
UiDesktopHMe = {}

---------------------------------------
function UiDesktopHMe:new(co_icon, self_name, self_chips, view_desktoph)
    local o = {}
    setmetatable(o, { __index = self })
    --self.__index = self
    --setmetatable(o, self)
    o.UiHeadIcon = ViewHeadIcon:new(nil, co_icon)
    o.SelfName = self_name
    o.SelfGolds = self_chips
    o.ViewDesktopH = view_desktoph
    o.ViewMgr = o.ViewDesktopH.ViewMgr
    o.MapWinLooseInfo = {}
    o.MapWinUiGolds = {}
    o.MapFTaskerGetWinGold = {}
    o.MapFTaskerSetGold = {}
    o.GoldController = GoldController:new(nil,
            function(gold)
                o:_setGold1(gold)
            end)
    return o
end

---------------------------------------
function UiDesktopHMe:initSelfInfo(is_init)
    for key, value in pairs(self.MapWinUiGolds) do
        for key1, value1 in pairs(value) do
            self.ViewDesktopH.UiDesktopHGoldPool:goldHEnPool(value1)
        end
    end

    if (is_init) then
        local icon_resource_name = ""
        local item_ico, icon_resource_name = CS.Casinos.HeadIconMgr:getIconName(true, self.ViewDesktopH.ControllerActor.PropAccountId:get(), icon_resource_name)

        self.UiHeadIcon:SetPlayerInfo(self.ViewDesktopH.ControllerActor.PropIcon:get(),
                self.ViewDesktopH.ControllerActor.PropAccountId:get(), self.ViewDesktopH.ControllerActor.PropVIPLevel:get())
    end

    self.SelfName.text = CS.Casinos.UiHelper.addEllipsisToStr(self.ViewDesktopH.ControllerActor.PropNickName:get(), 21, 6)
    self.GoldController:refreshGold(self.ViewDesktopH.ControllerActor.PropGoldAcc:get())
end

---------------------------------------
function UiDesktopHMe:Destroy()
    for key, value in pairs(self.MapWinUiGolds) do
        for key1, value1 in pairs(value) do
            self.ViewDesktopH.UiDesktopHGoldPool:goldHEnPool(value1)
        end
    end
    self.MapWinUiGolds = {}
    self:_cancelTask()
end

---------------------------------------
function UiDesktopHMe:setGoldChanged(change_reason, delta_gold, user_data)
    self.GoldController:goldChange(change_reason, delta_gold, user_data)
end

---------------------------------------
function UiDesktopHMe:addDeltaGold(change_reason)
    self.GoldController:addDeltaGold(change_reason)
end

---------------------------------------
function UiDesktopHMe:setBetSelfChipsToPot(bet_potindex, betchips)
    local bet_pot = self.ViewDesktopH:getDesktopHBetPot(bet_potindex)

    local operate_golds = -1
    for key, value in pairs(self.ViewDesktopH.UiDesktopHBase:getGoldOperateList()) do
        if (value == betchips) then
            operate_golds = value
            break
        end
    end

    local seat_index = self.ViewDesktopH.ControllerDesktopH.SeatIndex
    if (seat_index == 255) then
        if (operate_golds ~= -1) then
            local from = self:_getSelfIconCenterPos()
            bet_pot:BetGold(from, operate_golds)
            local self_y = self.UiHeadIcon.GCoHeadIcon.y
            if (CS.FairyGUI.GTween.IsTweening(self.UiHeadIcon.GCoHeadIcon) == false) then
                self.UiHeadIcon.GCoHeadIcon:TweenMoveY(self_y - self.ViewDesktopH.BetAniX, 0.1):OnComplete(
                        function()
                            self.UiHeadIcon.GCoHeadIcon:TweenMoveY(self_y, 0.1)
                        end
                )
            end
        else
            local from = self:_getSelfIconCenterPos()
            bet_pot:BetGold(from, betchips)
        end
    else
        local self_chair = self.ViewDesktopH:getDesktopHChairByIndex(seat_index)
        self_chair:BetGold(operate_golds, bet_potindex, betchips)
    end
end

---------------------------------------
function UiDesktopHMe:setPlayerSelfResultInfo(betpot_index, self_result_info)
    self.MapWinLooseInfo[betpot_index] = self_result_info
end

---------------------------------------
function UiDesktopHMe:betState()
    self.MapWinLooseInfo = {}
end

---------------------------------------
function UiDesktopHMe:showGameResult()
    if (self.ViewDesktopH.DesktopHGameResult == nil) then
        return
    end

    local win_golds = 0
    for i, v in pairs(self.MapWinLooseInfo) do
        if (v.is_win) then
            win_golds = win_golds + v.winloose_gold
            win_golds = win_golds + v.win_rewardpot_gold
        else
            win_golds = win_golds - v.winloose_gold
        end
    end

    local ui_result = self.ViewMgr:CreateView("DesktopHResult")
    local self_betgolds = self.ViewDesktopH.ControllerDesktopH:getSelfTotalBetGolds()
    ui_result:SetGameEndResult(self.ViewDesktopH.UiDesktopHBanker.BankPlayerDataDesktopH.PlayerInfoCommon.NickName,
            win_golds, self_betgolds, self.ViewDesktopH.DesktopHGameResult.map_betpot_info, self.ViewDesktopH.DesktopHGameResult.bankerpot_info,
            self.ViewDesktopH.DesktopHGameResult.ListGameEndWinPlayer)
end

---------------------------------------
function UiDesktopHMe:showWinGoldsAni(pot_index, from)
    local winloose_info = self.MapWinLooseInfo[pot_index]
    if (winloose_info ~= nil) then
        if (winloose_info.winloose_gold <= 0) then
            return
        end

        local bet_pot = self.ViewDesktopH:getDesktopHBetPot(pot_index)
        local list_golds = {}
        self.ViewDesktopH:CreateGolds(list_golds, nil, winloose_info.winloose_gold, bet_pot, 10)
        self.MapWinUiGolds[pot_index] = list_golds

        local delay_tm = 0.0
        local delay_t = self.ViewDesktopH:GetMoveIntervalTm(#list_golds)
        for key, value in pairs(list_golds) do
            local to = bet_pot:getRandomChipPos()
            value:InitMove(from, to, UiDesktopHGold.MOVE_CHIP_TM, UiDesktopHGold.MOVE_SOUND, nil, nil, false, delay_tm, true)
            delay_tm = delay_tm + delay_t
        end

        local map_param = CS.Casinos.LuaHelper.GetNewByteObjMap()
        map_param:Add(0, winloose_info.winloose_gold)
        map_param:Add(1, pot_index)
        local t = CS.Casinos.FTMgr.Instance:startTask(1.2) -- UiDesktopHBetPot.GivePlayerAniTm - UiDesktopHBetPot.WinShowAniTm
        local tasker = CS.Casinos.FTMgr.Instance:whenAll(map_param,
                function(map_param)
                    self:_playWinGoldAni(map_param)
                end, t)
        self.MapFTaskerGetWinGold[pot_index] = tasker
    end
end

---------------------------------------
function UiDesktopHMe:Reset()
    self.MapWinLooseInfo = {}
    for key, value in pairs(self.MapWinUiGolds) do
        for key1, value1 in pairs(value) do
            self.ViewDesktopH.UiDesktopHGoldPool:goldHEnPool(value1)
        end
    end
    --[[foreach (local i in self.MapWinUiGolds)            
        foreach (local gold in i.Value)                
            self.ViewDesktopH.UiDesktopHGoldPool:goldHEnPool(gold)
        end
    end]]

    self:_cancelTask()
    self.MapWinUiGolds = {}
    self.GoldController:refreshGold(self.ViewDesktopH.ControllerActor.PropGoldAcc:get())
end

---------------------------------------
function UiDesktopHMe:_cancelTask()
    for key, value in pairs(self.MapFTaskerGetWinGold) do
        if (value ~= nil) then
            value:cancelTask()
        end
    end
    --[[foreach (local i in self.MapFTaskerGetWinGold)
        if (i.Value ~= nil)
        then
            i.Value.cancelTask()
        end
    end]]
    self.MapFTaskerGetWinGold = {}
    for key, value in pairs(self.MapFTaskerSetGold) do
        if (value ~= nil) then
            value:cancelTask()
        end
    end
    --[[foreach (local i in self.MapFTaskerSetGold)
        if (i.Value ~= nil)
        then
            i.Value.cancelTask()
        end
    end]]
    self.MapFTaskerSetGold = {}
end

---------------------------------------
function UiDesktopHMe:_getSelfIconCenterPos()
    local pos = self.UiHeadIcon.GCoHeadIcon.xy
    local x = pos.x
    x = x + self.UiHeadIcon.GCoHeadIcon.width * self.UiHeadIcon.GCoHeadIcon.scaleX / 2
    pos.x = x
    local y = pos.y
    y = y + self.UiHeadIcon.GCoHeadIcon.height * self.UiHeadIcon.GCoHeadIcon.scaleY / 2
    pos.y = y
    return pos
end

---------------------------------------
function UiDesktopHMe:_playWinGoldAni(map_param)
    local pot_index = map_param[1]
    local list_gold = self.MapWinUiGolds[pot_index]
    if (list_gold ~= nil) then
        local to = nil
        local seat_index = self.ViewDesktopH.ControllerDesktopH.SeatIndex
        if (seat_index == 255) then
            to = self:_getSelfIconCenterPos()
        else
            local self_chair = self.ViewDesktopH:getDesktopHChairByIndex(seat_index)
            to = self_chair:getChairCenterPos()
        end

        local delay_tm = 0.0
        local delay_t = self.ViewDesktopH:GetMoveIntervalTm(#list_gold)
        for i, v in pairs(list_gold) do
            v:InitMove(v.GCoGold.xy, to,
                    UiDesktopHGold.MOVE_CHIP_TM, UiDesktopHGold.MOVE_SOUND, nil, nil, true, delay_tm, false)
            delay_tm = delay_tm + delay_t
        end

        self.MapWinUiGolds[pot_index] = nil

        local t = CS.Casinos.FTMgr.Instance:startTask(UiDesktopHGold.MAX_CHIP_MOVE_TM)
        local tasker = CS.Casinos.FTMgr.Instance:whenAll(map_param,
                function(map_param)
                    self:_setGold(map_param)
                end, t)
        self.MapFTaskerSetGold[pot_index] = tasker
    end
end

---------------------------------------
function UiDesktopHMe:_setGold(map_param)
    self.GoldController:addDeltaGold(GoldAccChangeReason.DesktopHWin)
end

---------------------------------------
function UiDesktopHMe:_setGold1(gold)
    self.SelfGolds.text = UiChipShowHelper:getGoldShowStr(gold, self.ViewMgr.LanMgr.LanBase, true, 2)
end