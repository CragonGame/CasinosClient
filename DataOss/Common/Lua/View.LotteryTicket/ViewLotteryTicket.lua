-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewLotteryTicket = ViewBase:new(nil)

---------------------------------------
function ViewLotteryTicket:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    if (self.Instance == nil) then
        self.Context = Context
        self.CasinosContext = CS.Casinos.CasinosContext.Instance
        self.LotteryTicketPackName = "LotteryTicket"
        self.ViewMgr = nil
        self.GoUi = nil
        self.ComUi = nil
        self.Panel = nil
        self.UILayer = nil
        self.InitDepth = nil
        self.ViewKey = nil
        self.ControllerLotteryTicket = nil
        self.UiLotteryTicketFlow = nil
        self.UiLotteryTicketBase = nil
        self.Instance = o
    end
    return self.Instance
end

---------------------------------------
function ViewLotteryTicket:OnCreate()
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

    self.ControllerLotteryTicket = self.ViewMgr.ControllerMgr:GetController("LotteryTicket")
    local fac = self.ControllerLotteryTicket:GetLotteryTicketBaseFactory(self.Context.Cfg.LotteryTicketFactoryName)
    self.UiLotteryTicketBase = fac:CreateUiDesktopHBase(self)
    local co_bg = self.ComUi:GetChild("CommonMsgBgAndClose").asCom
    local btn_close = co_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local com_shade = self.ComUi:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    self.GTextLotteryTicketTips = self.ComUi:GetChild("Time").asTextField
    self.GLoaderLotteryTicketTips = self.ComUi:GetChild("LoaderTips").asLoader
    self.GListBetPot = self.ComUi:GetChild("ListBetPot").asList
    self.GListHistory = self.ComUi:GetChild("ListHistory").asList
    self.GBtnRules = self.ComUi:GetChild("BtnRules").asButton
    self.GBtnRules.onClick:Add(
            function()
                self:_onClickRules()
            end
    )
    self.GBtnShowBetOperate = self.ComUi:GetChild("BtnRepeatBetEx").asCom
    self.GBtnShowBetOperate.onClick:Add(
            function()
                self:_onClickBetOperate()
            end
    )
    self.GBtnRepeatBet = self.ComUi:GetChild("BtnRepeatBet").asCom
    self.GBtnRepeatBet.onClick:Add(
            function()
                self:_onClickRepeatBet()
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
                self:_onClickShade()
            end
    )
    local co_headicon = self.ComUi:GetChild("CoHeadIcon").asCom
    self.ViewHeadIcon = ViewHeadIcon:new(nil, co_headicon)
    self.MapBetPot = {}
    self.MapOperate = {}
    self.ListLoaderCard = {}
    self.UiLotteryTicketBase:FindLotteryTicketCard(self.ListLoaderCard)
    self.GTextCurrentOperate = self.ComUi:GetChild("BetOperateValue").asTextField
    local co_rewardpot = self.ComUi:GetChild("CoRewardPot").asCom
    local btn_rewardpot = self.ComUi:GetChild("BtnRewardPotRecord").asButton
    self.ViewLotteryTicketRewardPot = ViewLotteryTicketRewardPot:new(nil, co_rewardpot, btn_rewardpot, self)
    self.GTextLastWinMaxPlayerNickName = self.ComUi:GetChild("WinMaxNickName").asTextField
    self.GTextLastWinMaxPlayerGolds = self.ComUi:GetChild("WinMaxGolds").asTextField
    self.GImageLastWinMaxPlayerGoldSign = self.ComUi:GetChild("WinMaxPlayerGoldSign").asImage
    self.GTextLastRoundBaoZiTm = co_rewardpot:GetChild("LastRoundBaoZiTm").asTextField
    self:SwitchControllerRules(false)
    self:_createBetPot()
    self.ChipIconSolustion = self.ComUi:GetController("ChipIconSolustion")
    self.ChipIconSolustion.selectedIndex = self.Context.Cfg.ChipIconSolustion
    self.UiLotteryTicketFlow = UiLotteryTicketFlow:new(nil, self)
    self.UiLotteryTicketFlow:Create()
end

---------------------------------------
function ViewLotteryTicket:OnDestroy()
    self.UiLotteryTicketFlow:Destroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewLotteryTicket:OnHandleEv(ev)
    if (ev.EventName == "EvEntityLotteryTicketBetOperateTypeChange") then
        for key, value in pairs(ev.map_changeoperate) do
            local item_operate = nil
            item_operate = self.MapOperate[key]
            if (item_operate ~= nil) then
                item_operate:SetCanOperate(value)
            end
        end
        self:RefreshBetOperate()
    elseif (ev.EventName == "EvEntityLotteryTicketCurrentBetOperateTypeChange") then
        self:RefreshBetOperate()
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
                bet_pot:SetBetPotInfo(value, self_bet)
            end
        end
    elseif (ev.EventName == "EvEntityLotteryTicketBet") then
        self.MapBetPot[ev.bet_potindex]:SetBetPotSelfChips(ev.already_bet_chips)
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
        if (self.Context.Cfg.UseLan) then
            package_name = self.ViewMgr.LanMgr:getLanPackageName()
        end
        self.GLoaderLotteryTicketTips.icon = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(package_name, icon_name)
        self:InitLotteryTicketData(ev.lotteryticket_data)
    elseif (ev.EventName == "EvEntityLotteryTicketGameEndStateSimple") then
        self:SetTips(true)
    elseif (ev.EventName == "EvEntityLotteryTicketGetRewardPotInfo") then
        self.ViewLotteryTicketRewardPot:SetRewardPotInfo(ev.reward_totalgolds, ev.list_playerinfo)
    elseif (ev.EventName == "EvEntityLotteryTicketUpdateTm") then
        self:RefreshLotteryTickLeftTm(ev.tm)
    end
end

---------------------------------------
function ViewLotteryTicket:InitLotteryTicketData(lotteryticket_data)
    self:CreateOpreate()
    self:RefreshHistory()
    self.ViewLotteryTicketRewardPot:SetRewardGolds(lotteryticket_data.RewardPotGold)
    if (lotteryticket_data.State == LotteryTicketStateEnum.Bet) then
        self:RefreshBetOperate()
    else
        self.GBtnRepeatBet.enabled = false
        self.GBtnShowBetOperate.enabled = false
        if (lotteryticket_data.ListCard ~= nil) then
            for i, v in pairs(lotteryticket_data.ListCard) do
                local card = self.ListLoaderCard[i]
                card:ShowCard(v)
            end
        else
            for key, value in pairs(self.ListLoaderCard) do
                value:ResetCard()
            end
        end
        for key, value in pairs(self.MapBetPot) do
            value:HideBetPot()
        end
        local byte_index = self.UiLotteryTicketBase:GetBetPotIndex(lotteryticket_data.WinCardType)--self.UiLotteryTicketBase:GetBetPotIndex(lotteryticket_data.WinCardType)
        self.MapBetPot[byte_index]:IsWin()
    end
    if (lotteryticket_data.MapBetPotBetInfo ~= nil) then
        local table = lotteryticket_data.MapBetPotBetInfo
        for key, value in pairs(table) do
            local self_bet = 0
            if (self.ControllerLotteryTicket.MapTotalBetGolds[key] ~= nil) then
                self_bet = self.ControllerLotteryTicket.MapTotalBetGolds[key]
            end
            local bet_pot = self.MapBetPot[key]
            bet_pot:SetBetPotInfo(value, self_bet)
        end
    end
    self:SetLastMaxWinnerInfo(lotteryticket_data.LastMaxWinner)
    self:SetLastBaoZiTm(lotteryticket_data.LastBaoZiDt)
end

---------------------------------------
function ViewLotteryTicket:GetCardType(list_card)
    return self.UiLotteryTicketBase:GetCardType(list_card)
end

---------------------------------------
function ViewLotteryTicket:RefreshLotteryTickLeftTm(tm)
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
    if (self.Context.Cfg.UseLan) then
        package_name = self.ViewMgr.LanMgr:getLanPackageName()
    end
    self.GLoaderLotteryTicketTips.icon = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(package_name, icon_name)
end

---------------------------------------
function ViewLotteryTicket:betState(map_betrepeatinfo)
    self.GBtnRepeatBet.enabled = LuaHelper:GetTableCount(map_betrepeatinfo) > 0
    self.GBtnShowBetOperate.enabled = true
    for key, value in pairs(self.ListLoaderCard) do
        value:ResetCard()
    end
    for key, value in pairs(self.MapBetPot) do
        value:ResetBetPot()
    end
end

---------------------------------------
function ViewLotteryTicket:gameEnd(gameend_detail, me_wingold)
    self.GBtnRepeatBet.enabled = false
    self.GBtnShowBetOperate.enabled = false
    self:SetTips(true)
    self.ViewLotteryTicketRewardPot:SetRewardGolds(gameend_detail.RewardPotGold)

    for i, v in pairs(gameend_detail.ListCard) do
        local card = self.ListLoaderCard[i]
        card:ShowCard(v)
    end

    for key, value in pairs(self.MapBetPot) do
        value:HideBetPot()
    end

    --local w_t = gameend_detail.WinCardType
    --self.UiLotteryTicketBase:GetBetPotIndex(gameend_detail.WinCardType)
    local byte_index = self.UiLotteryTicketBase:GetBetPotIndex(gameend_detail.WinCardType)
    self.MapBetPot[byte_index]:IsWin()
    self:SetLastMaxWinnerInfo(gameend_detail.LastMaxWinner)
    self:SetLastBaoZiTm(gameend_detail.LastBaoZiDt)
    if (me_wingold > 0) then
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("Get") .. me_wingold .. self.ViewMgr.LanMgr:getLanValue("Chip"))
    end

    self:RefreshHistory()
end

---------------------------------------
function ViewLotteryTicket:_createBetPot()
    for i = 5, 0, -1 do
        local key = i
        local tb_goldpercent = self.CasinosContext.TbDataMgrLua:GetData("LotteryTicketGoldPercent", i)
        local co_betpot = CS.FairyGUI.UIPackage.CreateObject(self.LotteryTicketPackName, "CoBetPot").asCom
        local bet_pot = UiLotteryTicketBetPotItem:new(nil, co_betpot, tb_goldpercent, self)
        bet_pot:InitBetPot(key)
        self.GListBetPot:AddChild(bet_pot.GCoBetPot)
        self.MapBetPot[key] = bet_pot
    end
end

---------------------------------------
function ViewLotteryTicket:RefreshHistory()
    local index = 1
    local list_history = self.ControllerLotteryTicket.ListLotteryTicketWinlooseResult
    self.GListHistory:RemoveChildrenToPool()
    for i = 1, #list_history do
        local co_history = self.GListHistory:AddItemFromPool().asCom
        --local history = ItemLotteryTicketHistory:new(nil, self, co_history, index == 1, list_history[i])
        local rank_type = list_history[i]
        local gloader_history = co_history:GetChild("LoaderHistory").asLoader
        local gimage_newsign = co_history:GetChild("NewSign").asImage
        local type_name = self.UiLotteryTicketBase:GetCardTypeName(rank_type)
        local package_name = self.LotteryTicketPackName
        if (self.Context.Cfg.UseLan == true) then
            package_name = self.ViewMgr.LanMgr:getLanPackageName()
        end
        gloader_history.icon = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(package_name, type_name)
        gimage_newsign.visible = index == 1

        index = index + 1
    end
end

---------------------------------------
function ViewLotteryTicket:CreateOpreate()
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
        local bet_operate = UiLotteryTicketBetOperateItem:new(nil, item_operate, self)
        bet_operate:SetOperateInfo(key, operate.OperateGolds, can_operate, is_current_operate)
        self.MapOperate[key] = bet_operate
        if (is_current_operate) then
            self.GTextCurrentOperate.text = UiChipShowHelper:getGoldShowStr2(operate.OperateGolds, self.ViewMgr.LanMgr.LanBase)
        end
    end
    self:SwitchControllerBetOperate(false)
end

---------------------------------------
function ViewLotteryTicket:RefreshBetOperate()
    for key, value in pairs(self.MapOperate) do
        value:SetIsCurrentOperate(false)
    end

    local current_operate = nil
    if (self.MapOperate[self.ControllerLotteryTicket.CurrentTbBetOperateId] ~= nil) then
        current_operate = self.MapOperate[self.ControllerLotteryTicket.CurrentTbBetOperateId]
    end
    if (current_operate ~= nil) then
        current_operate:SetIsCurrentOperate(true)
        self.GTextCurrentOperate.text = current_operate.GBtnBetOperate.text
        self.GBtnShowBetOperate.enabled = true
    else
        self.GBtnShowBetOperate.enabled = false
    end
    self:SwitchControllerBetOperate(false)
end

---------------------------------------
function ViewLotteryTicket:SetLastMaxWinnerInfo(lastround_winmax_playerinfo)
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
function ViewLotteryTicket:SetLastBaoZiTm(tm)
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
function ViewLotteryTicket:SwitchControllerRules(show_rules)
    if (show_rules) then
        self.ControllerRules.selectedIndex = 0
    else
        self.ControllerRules.selectedIndex = 1
    end
end

---------------------------------------
function ViewLotteryTicket:SwitchControllerBetOperate(show_rules)
    if (show_rules) then
        self.ControllerBetOperate.selectedIndex = 1
    else
        self.ControllerBetOperate.selectedIndex = 0
    end
end

---------------------------------------
function ViewLotteryTicket:SetTips(is_ending)
    if (is_ending) then
        self.GTextLotteryTicketTips.visible = false
        local icon_name = "Ending"
        self.GLoaderLotteryTicketTips.icon = CS.Casinos.UiHelperCasinos:FormatePackageImagePath(self.LotteryTicketPackName, icon_name)
    end
end

---------------------------------------
function ViewLotteryTicket:_onClickRules()
    self:SwitchControllerRules(self.ControllerRules.selectedIndex == 1)
end

function ViewLotteryTicket:_onClickBetOperate()
    self:SwitchControllerBetOperate(self.ControllerBetOperate.selectedIndex == 0)
end

---------------------------------------
function ViewLotteryTicket:_onClickShade()
    self.ControllerRules.selectedIndex = 1
end

---------------------------------------
function ViewLotteryTicket:_onClickRepeatBet()
    local ev = self.ViewMgr:GetEv("EvLotteryTicketRepeatBet")
    if (ev == nil) then
        ev = EvLotteryTicketRepeatBet:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self.GBtnRepeatBet.enabled = false
end

---------------------------------------
function ViewLotteryTicket:_onClickBtnClose()
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