-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
-- 结算对话框中的5个Item中的一个
UiDesktopHDlgGameEndResultWinPlayerItem = {}

---------------------------------------
function UiDesktopHDlgGameEndResultWinPlayerItem:new(o, com, player_info, rank, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GCoPlayerInfo = com
    local co_headicon = o.GCoPlayerInfo:GetChild("CoHeadIcon").asCom
    o.UiHeadIcon = ViewHeadIcon:new(nil, co_headicon)
    o.UiHeadIcon:setPlayerInfo(player_info.icon, player_info.account_id, player_info.vip_level)
    o.GTextNickName = o.GCoPlayerInfo:GetChild("NickName").asTextField
    o.GTextGolds = o.GCoPlayerInfo:GetChild("WinGold").asTextField
    o.GTextRank = o.GCoPlayerInfo:GetChild("Rank").asTextField
    o.GTextNickName.text = CS.Casinos.UiHelper.addEllipsisToStr(player_info.nick_name, 15, 4)
    o.GTextGolds.text = UiChipShowHelper:getGoldShowStr(player_info.win_gold, view_mgr.LanMgr.LanBase, false)
    o.GTextRank.text = tostring(rank)
    return o
end

---------------------------------------
-- 结算对话框中的5个Item中的一个
UiDesktopHDlgGameEndResultPotItem = {}

---------------------------------------
function UiDesktopHDlgGameEndResultPotItem:new(o, ui_desktoph, com, list_card, is_win, pot_index)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Context = Context
    o.CasinosContext = CS.Casinos.CasinosContext.Instance
    o.GCom = com
    local c_name = ""
    for i, v in pairs(list_card) do
        c_name = "Card" .. tostring(i)
        local l_card = o.GCom:GetChild(c_name)
        if (l_card ~= nil) then
            local gloader_card = l_card.asLoader
            local card_name = ui_desktoph.UiDesktopHBase:getCardResName(v)
            gloader_card.icon = o.CasinosContext.PathMgr.DirAbCard .. string.lower(card_name) .. ".ab"
        end
    end

    o.GLoaderBankSign = o.GCom:GetChild("LoaderBankSign").asLoader
    local bank_sign = ""
    if (pot_index == 255) then
        local packageName = ui_desktoph:getDesktopBasePackageName()
        if (o.Context.Cfg.UseLan == true) then
            packageName = ui_desktoph.ViewMgr.LanMgr:getLanPackageName()
        end
        bank_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(packageName, "BankSign")
    elseif (pot_index == 0) then
        bank_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(ui_desktoph:getDesktopBasePackageName(), "Pot0Sign")
    elseif (pot_index == 1) then
        bank_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(ui_desktoph:getDesktopBasePackageName(), "Pot1Sign")
    elseif (pot_index == 2) then
        bank_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(ui_desktoph:getDesktopBasePackageName(), "Pot2Sign")
    elseif (pot_index == 3) then
        bank_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(ui_desktoph:getDesktopBasePackageName(), "Pot3Sign")
    end

    o.GLoaderBankSign.icon = bank_sign
    o.GLoaderWinLoose = o.GCom:GetChild("LoaderWinLoose").asLoader
    local win_loose = "LooseSign"
    if (is_win) then
        win_loose = "WinSign"
    end
    local win_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(ui_desktoph:getDesktopBasePackageName(), win_loose)
    if (o.Context.Cfg.UseLan == true) then
        local pack_name = ui_desktoph.ViewMgr.LanMgr:getLanPackageName()
        win_sign = CS.Casinos.UiHelperCasinos.FormatePackageImagePath(pack_name, win_loose)
    end
    o.GLoaderWinLoose.icon = win_sign

    return o
end

---------------------------------------
ViewDesktopHResult = ViewBase:new()

---------------------------------------
function ViewDesktopHResult:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    self.ViewMgr = nil
    self.GoUi = nil
    self.ComUi = nil
    self.Panel = nil
    self.UILayer = nil
    self.InitDepth = nil
    self.ViewKey = nil
    self.ViewDesktopH = nil
    self.GListWinPlayer = nil
    self.GListPotResult = nil
    self.GTextCloseTips = nil
    self.GTextBankName = nil
    self.GTextBanWin = nil
    self.GTextSelfWin = nil
    self.GTextField = nil
    self.LeftTm = 0
    self.AutoDestroyTm = 5
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.TimerUpdate = nil
    return o
end

---------------------------------------
function ViewDesktopHResult:OnCreate()
    ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("BureauSettlement"))
    self.ViewDesktopH = self.ViewMgr:GetView("DesktopH")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bg:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:_onClickBtnClose()
            end
    )
    self.GListWinPlayer = self.ComUi:GetChild("ListWinRank").asList
    self.GListPotResult = self.ComUi:GetChild("ListPotResult").asList
    self.GTextCloseTips = self.ComUi:GetChild("CloseTips").asTextField
    self.GTextBankName = self.ComUi:GetChild("BankName").asTextField
    self.GTextBanWin = self.ComUi:GetChild("BanWin").asTextField
    self.GTextSelfWin = self.ComUi:GetChild("SelfWin").asTextField
    local self_bet = self.ComUi:GetChild("SelfBet")
    if (self_bet ~= nil) then
        self.GTextSelfBet = self_bet.asTextField
    end

    self.LeftTm = self.AutoDestroyTm
    self:_refreshLeftTime()

    self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(100, self, self._timerUpdate)
end

---------------------------------------
function ViewDesktopHResult:OnDestroy()
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
end

---------------------------------------
function ViewDesktopHResult:SetGameEndResult(bank_name, self_wingolds, self_betgolds,
                                             map_betpot_info, bankerpot_info, list_gameend_win_player)
    self.GTextSelfWin.text = UiChipShowHelper:getGoldShowStr(self_wingolds,
            self.ViewMgr.LanMgr.LanBase, false)
    if (self.GTextSelfBet ~= nil) then
        self.GTextSelfBet.text = UiChipShowHelper:getGoldShowStr(self_betgolds, self.ViewMgr.LanMgr.LanBase, false)
    end
    if (list_gameend_win_player ~= nil) then
        local rank = 1
        for i, v in pairs(list_gameend_win_player) do
            local co_winplayer = self.GListWinPlayer:AddItemFromPool().asCom
            UiDesktopHDlgGameEndResultWinPlayerItem:new(nil, co_winplayer, v, rank, self.ViewMgr)
            rank = rank + 1
        end
    end

    self.GTextBankName.text = bank_name
    if (bankerpot_info ~= nil) then
        local bank_wingolds = bankerpot_info.stack_after - bankerpot_info.stack_before
        self.GTextBanWin.text = UiChipShowHelper:getGoldShowStr(bank_wingolds, self.ViewMgr.LanMgr.LanBase, false)
        local bank_iswin = bank_wingolds >= 0
        local co_pot_result = CS.FairyGUI.UIPackage.CreateObject(self.ViewDesktopH:getDesktopBasePackageName(), "CoPotResult").asCom
        local co_bank_result = (self.GListPotResult:AddChild(co_pot_result)).asCom
        UiDesktopHDlgGameEndResultPotItem:new(nil, self.ViewDesktopH, co_bank_result, bankerpot_info.list_card, bank_iswin, 255)
    end

    if (map_betpot_info ~= nil) then
        local t_map_betpot_info = map_betpot_info
        for i = 0, 3 do
            local r_s = t_map_betpot_info[i]
            local co_pot_result = CS.FairyGUI.UIPackage.CreateObject(self.ViewDesktopH:getDesktopBasePackageName(), "CoPotResult").asCom
            local co_bank_result = (self.GListPotResult:AddChild(co_pot_result)).asCom
            UiDesktopHDlgGameEndResultPotItem:new(nil, self.ViewDesktopH, co_bank_result, r_s.list_card, r_s.is_win, i)
        end
    end

    local sound = "deskhundred_loose"
    if (self_wingolds >= 0) then
        sound = "deskhundred_win"
    end
    self.CasinosContext:Play(sound, CS.Casinos._eSoundLayer.LayerNormal)
end

---------------------------------------
function ViewDesktopHResult:_timerUpdate(tm)
    self.LeftTm = self.LeftTm - tm
    if (self.LeftTm >= 0) then
        self:_refreshLeftTime()
    else
        self:_onClickBtnClose()
    end
end

---------------------------------------
function ViewDesktopHResult:_refreshLeftTime()
    self.GTextCloseTips.text = string.format(self.ViewMgr.LanMgr:getLanValue("CloseAuto"), tostring(math.ceil(self.LeftTm)))
end

---------------------------------------
function ViewDesktopHResult:_onClickBtnClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewDesktopHResultFactory = ViewFactory:new()

---------------------------------------
function ViewDesktopHResultFactory:new(o, ui_package_name, ui_component_name,
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
function ViewDesktopHResultFactory:CreateView()
    local view = ViewDesktopHResult:new(nil)
    return view
end