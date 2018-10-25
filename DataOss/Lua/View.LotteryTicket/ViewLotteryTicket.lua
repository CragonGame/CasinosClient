-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewLotteryTicket = ViewBase:new(nil)

---------------------------------------
function ViewLotteryTicket:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.LotteryTicketPackName = "LotteryTicket"
    if (self.Instance == nil) then
        self.ViewMgr = nil
        self.GoUi = nil
        self.ComUi = nil
        self.Panel = nil
        self.UILayer = nil
        self.InitDepth = nil
        self.ViewKey = nil
        self.Instance = o
    end
    return self.Instance
end

---------------------------------------
function ViewLotteryTicket:onCreate()
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketBetOperateTypeChange", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketCurrentBetOperateTypeChange", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketUpdateBetPotBetInfo", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketBet", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketBetState", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketGameEndState", self)
    self.ViewMgr:BindEvListener("EvEntityGetLotteryTicketDataSuccess", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketGameEndStateSimple", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketGetRewardPotInfo", self)
    self.ViewMgr:BindEvListener("EvEntityLotteryTicketUpdateTm", self)
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerLotteryTicket = self.ViewMgr.ControllerMgr:GetController("LotteryTicket")
    local fac = self.ControllerLotteryTicket:GetLotteryTicketBaseFactory(LotteryTicketFactoryName)
    self.ViewLotteryTicketBase = fac:CreateUiDesktopHBase(self)
    local co_bg = self.ComUi:GetChild("CommonMsgBgAndClose").asCom
    local btn_close = co_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:onClickBtnClose()
            end
    )
    local com_shade = self.ComUi:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickBtnClose()
            end
    )
    self.GTextLotteryTicketTips = self.ComUi:GetChild("Time").asTextField
    self.GLoaderLotteryTicketTips = self.ComUi:GetChild("LoaderTips").asLoader
    self.GListBetPot = self.ComUi:GetChild("ListBetPot").asList
    self.GListHistory = self.ComUi:GetChild("ListHistory").asList
    self.GBtnRules = self.ComUi:GetChild("BtnRules").asButton
    self.GBtnRules.onClick:Add(
            function()
                self:onClickRules()
            end
    )
    self.GBtnShowBetOperate = self.ComUi:GetChild("BtnRepeatBetEx").asCom
    self.GBtnShowBetOperate.onClick:Add(
            function()
                self:onClickBetOperate()
            end
    )
    self.GBtnRepeatBet = self.ComUi:GetChild("BtnRepeatBet").asCom
    self.GBtnRepeatBet.onClick:Add(
            function()
                self:onClickRepeatBet()
            end
    )
    self.GBtnRepeatBet.enabled = false
    self.ControllerRules = self.ComUi:GetController("ControllerRules")
    self.ControllerBetOperate = self.ComUi:GetController("ControllerBetOperate")
    self.GCoPotBetOperate = self.ComUi:GetChild("PotBetOperate").asCom
    local group_rules = self.ComUi:GetChild("GroupRules").asGroup
    local co_shade = self.ComUi:GetChildInGroup(group_rules, "CoShade").asCom
    co_shade.x = 0
    co_shade.onClick:Add(
            function()
                self:onClickShade()
            end
    )
    local co_headicon = self.ComUi:GetChild("CoHeadIcon").asCom
    self.ViewHeadIcon = ViewHeadIcon:new(nil, co_headicon)
    self.MapBetPot = {}
    self.MapOperate = {}
    self.ListLoaderCard = {}
    self.ViewLotteryTicketBase:findLotteryTicketCard(self.ListLoaderCard)
    self.GTextCurrentOperate = self.ComUi:GetChild("BetOperateValue").asTextField
    local co_rewardpot = self.ComUi:GetChild("CoRewardPot").asCom
    local btn_rewardpot = self.ComUi:GetChild("BtnRewardPotRecord").asButton
    self.ViewLotteryTicketRewardPot = ViewLotteryTicketRewardPot:new(nil, co_rewardpot, btn_rewardpot, self)
    self.GTextLastWinMaxPlayerNickName = self.ComUi:GetChild("WinMaxNickName").asTextField
    self.GTextLastWinMaxPlayerGolds = self.ComUi:GetChild("WinMaxGolds").asTextField
    self.GImageLastWinMaxPlayerGoldSign = self.ComUi:GetChild("WinMaxPlayerGoldSign").asImage
    self.GTextLastRoundBaoZiTm = co_rewardpot:GetChild("LastRoundBaoZiTm").asTextField
    self:switchControllerRules(false)
    self:createBetPot()
    self.ChipIconSolustion = self.ComUi:GetController("ChipIconSolustion")
    self.ChipIconSolustion.selectedIndex = ChipIconSolustion
end

---------------------------------------
function ViewLotteryTicket:onDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewLotteryTicket:onHandleEv(ev)
    if (ev.EventName == "EvEntityLotteryTicketBetOperateTypeChange") then
        for key, value in pairs(ev.map_changeoperate) do
            local item_operate = nil
            item_operate = self.MapOperate[key]
            if (item_operate ~= nil) then
                item_operate:setcanOperate(value)
            end
        end
        self:refreshBetOperate()
    elseif (ev.EventName == "EvEntityLotteryTicketCurrentBetOperateTypeChange") then
        self:refreshBetOperate()
    elseif (ev.EventName == "EvEntityLotteryTicketUpdateBetPotBetInfo") then
        local map_allbetpot = ev.map_allbetpot
        local map_self_betinfo = ev.map_self_betinfo
        if (map_allbetpot ~= nil) then
            for key, value in pairs(map_allbetpot) do
                local self_bet = 0
                if (map_self_betinfo[key] ~= nil) then
                    self_bet = map_self_betinfo[key]
                end
                local bet_pot = self.MapBetPot[key]
                bet_pot:setBetPotInfo(value, self_bet)
            end
        end
    elseif (ev.EventName == "EvEntityLotteryTicketBet") then
        self.MapBetPot[ev.bet_potindex]:setBetPotSelfChips(ev.already_bet_chips)
    elseif (ev.EventName == "EvEntityLotteryTicketBetState") then
        self:betState(ev.map_betrepeatinfo)
    elseif (ev.EventName == "EvEntityLotteryTicketGameEndState") then
        self:gameEnd(ev.gameend_detail, ev.me_wingold)
    elseif (ev.EventName == "EvEntityGetLotteryTicketDataSuccess") then
        local icon_name = ""
        if (ev.lotteryticket_data.State == LotteryTicketStateEnum.Bet) then
            local tips = string.format("%u", math.ceil(ev.lotteryticket_data.StateLeftTm)) .. self.ViewMgr.LanMgr:getLanValue("S")
            self.GTextLotteryTicketTips.visible = true
            self.GTextLotteryTicketTips.text = tips
            icon_name = "Beting"
        else
            self.GTextLotteryTicketTips.visible = false
            icon_name = "Ending"
        end
        local package_name = self.LotteryTicketPackName
        if (UseLan) then
            package_name = self.ViewMgr.LanMgr:getLanPackageName()
        end
        self.GLoaderLotteryTicketTips.icon = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(package_name, icon_name)
        self:initLotteryTicket(ev.lotteryticket_data)
    elseif (ev.EventName == "EvEntityLotteryTicketGameEndStateSimple") then
        self:setTips(true)
    elseif (ev.EventName == "EvEntityLotteryTicketGetRewardPotInfo") then
        self.ViewLotteryTicketRewardPot:setRewardPotInfo(ev.reward_totalgolds, ev.list_playerinfo)
    elseif (ev.EventName == "EvEntityLotteryTicketUpdateTm") then
        self:updateLotteryTickTm(ev.tm)
    end
end

---------------------------------------
function ViewLotteryTicket:initLotteryTicket(lotteryticket_data)
    self:createOpreate()
    self:updateHistory()
    self.ViewLotteryTicketRewardPot:setRewardGolds(lotteryticket_data.RewardPotGold)
    if (lotteryticket_data.State == LotteryTicketStateEnum.Bet) then
        self:refreshBetOperate()
    else
        self.GBtnRepeatBet.enabled = false
        self.GBtnShowBetOperate.enabled = false
        if (lotteryticket_data.ListCard ~= nil) then
            for i, v in pairs(lotteryticket_data.ListCard) do
                local card = self.ListLoaderCard[i]
                card:showCard(v)
            end
        else
            for key, value in pairs(self.ListLoaderCard) do
                value:resetCard()
            end
        end
        for key, value in pairs(self.MapBetPot) do
            value:hideBetPot()
        end
        local byte_index = self.ViewLotteryTicketBase:getBetPotIndex(lotteryticket_data.WinCardType)--self.ViewLotteryTicketBase:getBetPotIndex(lotteryticket_data.WinCardType)
        self.MapBetPot[byte_index]:isWin()
    end
    if (lotteryticket_data.MapBetPotBetInfo ~= nil) then
        local table = lotteryticket_data.MapBetPotBetInfo
        for key, value in pairs(table) do
            local self_bet = 0
            if (self.ControllerLotteryTicket.MapTotalBetGolds[key] ~= nil) then
                self_bet = self.ControllerLotteryTicket.MapTotalBetGolds[key]
            end
            local bet_pot = self.MapBetPot[key]
            bet_pot:setBetPotInfo(value, self_bet)
        end
    end
    self:setWinMaxPlayerInfo(lotteryticket_data.LastMaxWinner)
    self:setLastBaoZiTm(lotteryticket_data.LastBaoZiDt)
end

---------------------------------------
function ViewLotteryTicket:getCardType(list_card)
    return self.ViewLotteryTicketBase:getCardType(list_card)
end

---------------------------------------
function ViewLotteryTicket:updateLotteryTickTm(tm)
    local icon_name = ""
    if (tm > 0) then
        self.GTextLotteryTicketTips.visible = true
        self.GTextLotteryTicketTips.text = tostring(tm) .. self.ViewMgr.LanMgr:getLanValue("S")
        icon_name = "Beting"
    else
        self.GTextLotteryTicketTips.visible = false
        icon_name = "Ending"
    end

    local package_name = self.LotteryTicketPackName
    if (UseLan) then
        package_name = self.ViewMgr.LanMgr:getLanPackageName()
    end
    self.GLoaderLotteryTicketTips.icon = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(package_name, icon_name)
end

---------------------------------------
function ViewLotteryTicket:betState(map_betrepeatinfo)
    self.GBtnRepeatBet.enabled = LuaHelper:GetTableCount(map_betrepeatinfo) > 0
    self.GBtnShowBetOperate.enabled = true
    for key, value in pairs(self.ListLoaderCard) do
        value:resetCard()
    end
    for key, value in pairs(self.MapBetPot) do
        value:resetBetPot()
    end
end

---------------------------------------
function ViewLotteryTicket:gameEnd(gameend_detail, me_wingold)
    self.GBtnRepeatBet.enabled = false
    self.GBtnShowBetOperate.enabled = false
    self:setTips(true)
    self.ViewLotteryTicketRewardPot:setRewardGolds(gameend_detail.RewardPotGold)

    for i, v in pairs(gameend_detail.ListCard) do
        local card = self.ListLoaderCard[i]
        card:showCard(v)
    end

    for key, value in pairs(self.MapBetPot) do
        value:hideBetPot()
    end

    --local w_t = gameend_detail.WinCardType
    --self.ViewLotteryTicketBase:getBetPotIndex(gameend_detail.WinCardType)
    local byte_index = self.ViewLotteryTicketBase:getBetPotIndex(gameend_detail.WinCardType)
    self.MapBetPot[byte_index]:isWin()
    self:setWinMaxPlayerInfo(gameend_detail.LastMaxWinner)
    self:setLastBaoZiTm(gameend_detail.LastBaoZiDt)
    if (me_wingold > 0) then
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("Get") .. me_wingold .. self.ViewMgr.LanMgr:getLanValue("Chip"))
    end

    self:updateHistory()
end

---------------------------------------
function ViewLotteryTicket:createBetPot()
    for i = 5, 0, -1 do
        local key = i
        local tb_goldpercent = self.CasinosContext.TbDataMgrLua:GetData("LotteryTicketGoldPercent", i)
        local co_betpot = CS.FairyGUI.UIPackage.CreateObject(self.LotteryTicketPackName, "CoBetPot").asCom
        local bet_pot = ItemLotteryTicketBetPot:new(nil, co_betpot, tb_goldpercent, self)
        bet_pot:initBetPot(key)
        self.GListBetPot:AddChild(bet_pot.GCoBetPot)
        self.MapBetPot[key] = bet_pot
    end
end

---------------------------------------
function ViewLotteryTicket:updateHistory()
    local index = 1
    local list_history = self.ControllerLotteryTicket.ListLotteryTicketWinlooseResult
    self.GListHistory:RemoveChildrenToPool()
    for i = 1, #list_history do
        local co_history = self.GListHistory:AddItemFromPool().asCom
        local history = ItemLotteryTicketHistory:new(nil, self, co_history, index == 1, list_history[i])
        index = index + 1
    end
end

---------------------------------------
function ViewLotteryTicket:createOpreate()
    local co_operate = self.ComUi:GetChild("PotBetOperate").asCom
    local list_operate = co_operate:GetChild("list").asList
    local map_operate = self.CasinosContext.TbDataMgrLua:GetMapData("LotteryTicketBetOperate")
    for key, value in pairs(map_operate) do
        local operate = value
        local can_operate = self.ControllerLotteryTicket.MapCanOperateId[key]
        local is_current_operate = false
        if (self.ControllerLotteryTicket.CurrentTbBetOperateId == -1) then
            is_current_operate = false
            if (key == 1) then
                is_current_operate = true
            end
        else
            is_current_operate = false
            if (key == self.ControllerLotteryTicket.CurrentTbBetOperateId) then
                is_current_operate = true
            end
        end
        local item_operate = list_operate:AddItemFromPool().asButton
        local bet_operate = ItemLotteryTicketBetOperate:new(nil, item_operate, self)
        bet_operate:setOperateInfo(key, operate.OperateGolds, can_operate, is_current_operate)
        self.MapOperate[key] = bet_operate
        if (is_current_operate) then
            self.GTextCurrentOperate.text = UiChipShowHelper:getGoldShowStr2(operate.OperateGolds, self.ViewMgr.LanMgr.LanBase)
        end
    end
    self:switchControllerBetOperate(false)
end

---------------------------------------
function ViewLotteryTicket:setDesktopTips()
end

---------------------------------------
function ViewLotteryTicket:refreshBetOperate()
    for key, value in pairs(self.MapOperate) do
        value:setIsCurrentOperate(false)
    end

    local current_operate = nil
    if (self.MapOperate[self.ControllerLotteryTicket.CurrentTbBetOperateId] ~= nil) then
        current_operate = self.MapOperate[self.ControllerLotteryTicket.CurrentTbBetOperateId]
    end
    if (current_operate ~= nil) then
        current_operate:setIsCurrentOperate(true)
        self.GTextCurrentOperate.text = current_operate.GBtnBetOperate.text
        self.GBtnShowBetOperate.enabled = true
    else
        self.GBtnShowBetOperate.enabled = false
    end
    self:switchControllerBetOperate(false)
end

---------------------------------------
function ViewLotteryTicket:setWinMaxPlayerInfo(lastround_winmax_playerinfo)
    if (lastround_winmax_playerinfo ~= nil) then
        self.GImageLastWinMaxPlayerGoldSign.visible = true
        self.GTextLastWinMaxPlayerNickName.visible = true
        self.GTextLastWinMaxPlayerGolds.visible = true
        self.ViewHeadIcon:setLotteryWinMaxPlayerInfo(lastround_winmax_playerinfo)
        self.GTextLastWinMaxPlayerNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(lastround_winmax_playerinfo.Nickname, 15, 4)
        self.GTextLastWinMaxPlayerGolds.text = UiChipShowHelper:getGoldShowStr(lastround_winmax_playerinfo.WinGold, self.ViewMgr.LanMgr.LanBase)
    else
        self.ViewHeadIcon:hideIcon()
        self.GImageLastWinMaxPlayerGoldSign.visible = false
        self.GTextLastWinMaxPlayerNickName.visible = false
        self.GTextLastWinMaxPlayerGolds.visible = false
    end
end

---------------------------------------
function ViewLotteryTicket:setLastBaoZiTm(tm)
    if tm == nil then
        return
    end

    local d_tm = CS.System.DateTime.Parse(tm)
    if (CS.System.DateTime.MinValue ~= d_tm) then
        local last_baozi_date = CS.Casinos.LuaHelper.TimeDifferenceNow(d_tm)
        local day = self.ViewMgr.LanMgr:getLanValue("Day")
        local hours = self.ViewMgr.LanMgr:getLanValue("Hour")
        local minute = self.ViewMgr.LanMgr:getLanValue("Minute")
        self.GTextLastRoundBaoZiTm.text = last_baozi_date.Days .. day .. last_baozi_date.Hours .. hours .. last_baozi_date.Minutes .. minute
    end
end

---------------------------------------
function ViewLotteryTicket:switchControllerRules(show_rules)
    if (show_rules) then
        self.ControllerRules.selectedIndex = 0
    else
        self.ControllerRules.selectedIndex = 1
    end
end

---------------------------------------
function ViewLotteryTicket:switchControllerBetOperate(show_rules)
    if (show_rules) then
        self.ControllerBetOperate.selectedIndex = 1
    else
        self.ControllerBetOperate.selectedIndex = 0
    end
end

---------------------------------------
function ViewLotteryTicket:setTips(is_ending)
    if (is_ending) then
        self.GTextLotteryTicketTips.visible = false
        local icon_name = "Ending"
        self.GLoaderLotteryTicketTips.icon = CS.Casinos.UiHelperCasinos:FormatePackageImagePath(self.LotteryTicketPackName, icon_name)
    end
end

---------------------------------------
function ViewLotteryTicket:onClickRules()
    self:switchControllerRules(self.ControllerRules.selectedIndex == 1)
end

function ViewLotteryTicket:onClickBetOperate()
    self:switchControllerBetOperate(self.ControllerBetOperate.selectedIndex == 0)
end

---------------------------------------
function ViewLotteryTicket:onClickShade()
    self.ControllerRules.selectedIndex = 1
end

---------------------------------------
function ViewLotteryTicket:onClickRepeatBet()
    local ev = self.ViewMgr:GetEv("EvLotteryTicketRepeatBet")
    if (ev == nil) then
        ev = EvLotteryTicketRepeatBet:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self.GBtnRepeatBet.enabled = false
end

---------------------------------------
function ViewLotteryTicket:onClickBtnClose()
    local ev = self.ViewMgr:GetEv("EvUiClickLeaveLotteryTicket")
    if (ev == nil) then
        ev = EvUiClickLeaveLotteryTicket:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewLotteryTicketFactory = ViewFactory:new()

---------------------------------------
function ViewLotteryTicketFactory:new(o, ui_package_name, ui_component_name,
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

---------------------------------------
function ViewLotteryTicketFactory:CreateView()
    local view = ViewLotteryTicket:new(nil)
    return view
end