-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewDesktopHResult = ViewBase:new()

---------------------------------------
function ViewDesktopHResult:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    o.ViewDesktopH = nil
    o.GListWinPlayer = nil
    o.GListPotResult = nil
    o.GTextCloseTips = nil
    o.GTextBankName = nil
    o.GTextBanWin = nil
    o.GTextSelfWin = nil
    o.GTextField = nil
    o.Tm = 0
    o.ShowTm = 0
    o.AutoDestroyTm = 5
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
                self:onClickBtnClose()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickBtnClose()
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

    self.Tm = self.AutoDestroyTm
    self.ShowTm = self.AutoDestroyTm
    self:_setCloseTime()

    self.TimerUpdate = self.CasinosContext.TimerShaft:RegisterTimer(100, self, self._timerUpdate)
end

---------------------------------------
function ViewDesktopHResult:setGameResult(bank_name, self_wingolds, self_betgolds,
                                          map_betpot_info, bankerpot_info, list_gameend_win_player)
    self.GTextSelfWin.text = UiChipShowHelper:getGoldShowStr(self_wingolds,
            self.ViewMgr.LanMgr.LanBase, false)
    if (self.GTextSelfBet ~= nil) then
        self.GTextSelfBet.text = UiChipShowHelper:getGoldShowStr(self_betgolds,
                self.ViewMgr.LanMgr.LanBase, false)
    end
    if (list_gameend_win_player ~= nil) then
        local rank = 1
        for i, v in pairs(list_gameend_win_player) do
            local co_winplayer = self.GListWinPlayer:AddItemFromPool().asCom
            ItemDesktopHGameEndWinPlayer:new(nil, co_winplayer, v, rank, self.ViewMgr)
            rank = rank + 1
        end
    end

    self.GTextBankName.text = bank_name
    if (bankerpot_info ~= nil) then
        local bank_wingolds = bankerpot_info.stack_after - bankerpot_info.stack_before
        self.GTextBanWin.text = UiChipShowHelper:getGoldShowStr(bank_wingolds, self.ViewMgr.LanMgr.LanBase, false)
        local bank_iswin = bank_wingolds >= 0
        local co_pot_result = CS.FairyGUI.UIPackage.CreateObject(self.ViewDesktopH:getDesktopBasePackageName(),
                "CoPotResult")  .asCom
        local co_bank_result = (self.GListPotResult:AddChild(co_pot_result)).asCom
        ItemDesktopHGameEndPotResult:new(nil, self.ViewDesktopH, co_bank_result, bankerpot_info.list_card, bank_iswin, 255)
    end

    if (map_betpot_info ~= nil) then
        local t_map_betpot_info = map_betpot_info
        for i = 0, 3 do
            local r_s = t_map_betpot_info[i]
            local co_pot_result = CS.FairyGUI.UIPackage.CreateObject(self.ViewDesktopH:getDesktopBasePackageName(),
                    "CoPotResult")  .asCom
            local co_bank_result = (self.GListPotResult:AddChild(co_pot_result)).asCom
            ItemDesktopHGameEndPotResult:new(nil, self.ViewDesktopH, co_bank_result, r_s.list_card, r_s.is_win, i)
        end
    end

    local sound = "deskhundred_loose"
    if (self_wingolds >= 0) then
        sound = "deskhundred_win"
    end
    CS.Casinos.CasinosContext.Instance:Play(sound, CS.Casinos._eSoundLayer.LayerNormal)
end

---------------------------------------
function ViewDesktopHResult:_timerUpdate(tm)
    self.Tm = self.Tm - tm
    if (self.Tm <= self.ShowTm - 1) then
        self.ShowTm = self.Tm
        self:_setCloseTime()
    end
    if (self.Tm <= 0) then
        self:onClickBtnClose()
    end
end

---------------------------------------
function ViewDesktopHResult:_setCloseTime()
    self.GTextCloseTips.text = string.format(self.ViewMgr.LanMgr:getLanValue("CloseAuto"), tostring(math.ceil(self.ShowTm)))
end

---------------------------------------
function ViewDesktopHResult:onClickBtnClose()
    if (self.TimerUpdate ~= nil) then
        self.TimerUpdate:Close()
        self.TimerUpdate = nil
    end
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