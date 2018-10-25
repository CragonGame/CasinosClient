-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
ViewRanking = ViewBase:new()

---------------------------------------
function ViewRanking:new(o)
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
    self.CoPlayer = nil
    self.ChampionSignName = "Rank1"
    self.SecondPlaceSignName = "Rank2"
    self.ThirdPlaceSignName = "Rank3"
    return o
end

---------------------------------------
function ViewRanking:OnCreate()
    ViewHelper:PopUi(self.ComUi, self.ViewMgr.LanMgr:getLanValue("Ranking"))
    self.ControllerRanking = self.ViewMgr.ControllerMgr:GetController("Ranking")
    self.Controller = self.ComUi:GetController("ControllerRanking")
    local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_return = com_bg:GetChild("BtnClose").asButton
    btn_return.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )
    local com_shade = com_bg:GetChild("ComShade").asCom
    com_shade.onClick:Add(
            function()
                self:onClickBtnReturn()
            end
    )
    local btn_tabDiamond = self.ComUi:GetChild("BtnTabDiamond").asButton
    btn_tabDiamond.onClick:Add(
            function()
                self:onClickBtnDiamond()
            end
    )
    local btn_tabCold = self.ComUi:GetChild("BtnTabGold").asButton
    btn_tabCold.onClick:Add(
            function()
                self:onClickBtnGold()
            end
    )
    local btn_tabred = self.ComUi:GetChild("BtnTabRed").asButton
    btn_tabred.onClick:Add(
            function()
                self:onClickBtnRed()
            end
    )

    self.GListRanking = self.ComUi:GetChild("RankingList").asList
    local ev = self.ViewMgr:GetEv("EvUiGetRankingGold")
    if (ev == nil)
    then
        ev = EvUiGetRankingGold:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self.RankingListType = RankingListType.Chip
    self.GListRanking.itemRenderer = function(a, b)
        self:RenderListItem(a, b)
    end
    self.GListRanking:SetVirtual()
    self.ViewMgr:BindEvListener("EvEntityGetRankingDiamond", self)
    self.ViewMgr:BindEvListener("EvEntityGetRankingGold", self)
    self.ViewMgr:BindEvListener("EvEntityGetRankingRedEnvelopes", self)
    --self.ViewMgr:BindEvListener("EvEntityGetRankingWinGold",self)
end

---------------------------------------
function ViewRanking:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewRanking:OnHandleEv(ev)
    if (ev ~= nil) then
        if (ev.EventName == "EvEntityGetRankingDiamond") then
            self.GListRanking.numItems = #self.ControllerRanking.ListRankingDiamond
        elseif (ev.EventName == "EvEntityGetRankingGold") then
            self.GListRanking.numItems = #self.ControllerRanking.ListRankingGold
        elseif (ev.EventName == "EvEntityGetRankingRedEnvelopes") then
            self.GListRanking.numItems = #self.ControllerRanking.ListRankingRedEnvelopes
            --elseif(ev.EventName == "EvEntityGetRankingWinGold")
            --then
            --   self.GListRanking.numItems = #self.ControllerRanking.ListRankingWinGold
        end
    end
end

---------------------------------------
function ViewRanking:RenderListItem(index, obj)
    local com = CS.Casinos.LuaHelper.GObjectCastToGCom(obj)
    local item = ItemRank:new(nil, com)
    if (self.RankingListType == RankingListType.Chip) then
        if (self.ControllerRanking.ListRankingGold ~= nil) then
            if (#self.ControllerRanking.ListRankingGold > index) then
                local rank_info = self.ControllerRanking.ListRankingGold[index + 1]
                item:setRankInfo(self.ViewMgr, rank_info.player_guid, rank_info.nick_name, rank_info.icon_name,
                        rank_info.account_id, rank_info.gold, 0, index, true, nil)
            end
        end
    elseif (self.RankingListType == RankingListType.Gold) then
        if (self.ControllerRanking.ListRankingDiamond ~= nil) then
            if (#self.ControllerRanking.ListRankingDiamond > index) then
                local rank_info = self.ControllerRanking.ListRankingDiamond[index + 1]
                item:setRankInfo(self.ViewMgr, rank_info.player_guid, rank_info.nick_name, rank_info.icon_name,
                        rank_info.account_id, rank_info.diamond, 0, index, false, nil)
            end
        end
    elseif (self.RankingListType == RankingListType.Level) then
        if (self.ControllerRanking.ListRankingLevel ~= nil) then
            if (#self.ControllerRanking.ListRankingLevel > index) then
                local rank_info = self.ControllerRanking.ListRankingLevel[index + 1]
                item:setRankInfo(self.ViewMgr, rank_info.player_guid, rank_info.nick_name, rank_info.icon_name,
                        rank_info.account_id, rank_info.player_level, 0, index, true, nil)
            end
        end
    elseif (self.RankingListType == RankingListType.Gift) then
    elseif (self.RankingListType == RankingListType.WinGold) then
        if (self.ControllerRanking.ListRankingWinGold ~= nil) then
            if (#self.ControllerRanking.ListRankingWinGold > index) then
                local rank_info = self.ControllerRanking.ListRankingWinGold[index + 1]
                item:setRankInfo(self.ViewMgr, rank_info.player_guid, rank_info.nick_name, rank_info.icon_name,
                        rank_info.account_id, rank_info.win_gold, 0, index, true, nil)
            end
        end
    else
        if (self.ControllerRanking.ListRankingRedEnvelopes ~= nil) then
            if (#self.ControllerRanking.ListRankingRedEnvelopes > index) then
                local rank_info = self.ControllerRanking.ListRankingRedEnvelopes[index + 1]
                item:setRankInfo(self.ViewMgr, rank_info.player_guid, rank_info.nick_name, rank_info.icon_name,
                        rank_info.account_id, rank_info.win_redenvelope / 100, 0, index, true, self.ViewMgr.LanMgr:getLanValue("Yuan"))
            end
        end
    end
end

---------------------------------------
function ViewRanking:onClickBtnReturn()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
function ViewRanking:onClickBtnDiamond()
    local ev = self.ViewMgr:GetEv("EvUiGetRankingDiamond")
    if (ev == nil) then
        ev = EvUiGetRankingDiamond:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self.Controller:SetSelectedPage("Diamond")
    self.RankingListType = RankingListType.Gold
end

---------------------------------------
function ViewRanking:onClickBtnGold()
    local ev = self.ViewMgr:GetEv("EvUiGetRankingGold")
    if (ev == nil) then
        ev = EvUiGetRankingGold:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self.Controller:SetSelectedPage("Coin")
    self.RankingListType = RankingListType.Chip
end

---------------------------------------
function ViewRanking:onClickBtnWin()
    local ev = self.ViewMgr:GetEv("EvUiGetRankingWinGold")
    if (ev == nil) then
        ev = EvUiGetRankingWinGold:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self.Controller:SetSelectedPage("Win")
    self.RankingListType = RankingListType.WinGold
end

---------------------------------------
function ViewRanking:onClickBtnRed()
    local ev = self.ViewMgr:GetEv("EvUiGetRankingRedEnvelopes")
    if (ev == nil) then
        ev = EvUiGetRankingRedEnvelopes:new(nil)
    end
    self.ViewMgr:SendEv(ev)
    self.Controller:SetSelectedPage("Win")
    self.RankingListType = RankingListType.RedEnvelopes
end

---------------------------------------
ViewRankingFactory = ViewFactory:new()

---------------------------------------
function ViewRankingFactory:new(o, ui_package_name, ui_component_name,
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
function ViewRankingFactory:CreateView()
    local view = ViewRanking:new(nil)
    return view
end