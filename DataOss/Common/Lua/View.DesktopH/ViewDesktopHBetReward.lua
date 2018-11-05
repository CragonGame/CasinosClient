-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
-- 代表一个宝箱
UiDesktopHBetRewardItem = ViewBase:new()

---------------------------------------
function UiDesktopHBetRewardItem:new(o, bet_reward, co_betreward, betreward_tbid, state, view_mgr)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.ViewMgr = view_mgr
    o.BetReward = bet_reward
    o.GCoBetReward = co_betreward
    o.BetRewardTbId = betreward_tbid
    o.DesktopHDialyGetBetRewardState = state
    o.GTextBetRewardValue = o.GCoBetReward:GetChild("RewardValue").asTextField
    o.GTextBetValue = o.GCoBetReward:GetChild("BetValue").asTextField
    local tb_reward = o.ViewMgr.TbDataMgr:GetData("DesktopHBetReward", betreward_tbid)
    o.GTextBetRewardValue.text = UiChipShowHelper:getGoldShowStr(tb_reward.BetRewardValue, o.ViewMgr.LanMgr.LanBase)
    o.GTextBetValue.text = UiChipShowHelper:getGoldShowStr(tb_reward.BetValue, o.ViewMgr.LanMgr.LanBase)
    o.GCoBetReward.onClick:Add(
            function()
                o:_onClickBetReward()
            end
    )
    return o
end

---------------------------------------
function UiDesktopHBetRewardItem:_onClickBetReward()
    local tips = ""
    local tb_datamgr = self.ViewMgr.TbDataMgr
    if (self.DesktopHDialyGetBetRewardState == DesktopHDialyGetBetRewardState.Get) then
        tips = self.ViewMgr.LanMgr:getLanValue("HasBeenReceivedTreasure")
    else
        local reward_gold = 0
        local t_desktophbetreward = tb_datamgr:GetMapData("DesktopHBetReward")
        for k, v in pairs(t_desktophbetreward) do
            local state = self.BetReward.BDesktopHDialyBetReward.MapGetRewardState[k]
            if (state ~= DesktopHDialyGetBetRewardState.Get) then
                local tb_data = v
                if (k <= self.BetRewardTbId) then
                    reward_gold = reward_gold + tb_data.BetRewardValue
                end
            end
        end

        local tb_reward = tb_datamgr:GetData("DesktopHBetReward", self.BetRewardTbId)
        tips = string.format(self.ViewMgr.LanMgr:getLanValue("BetRechCanCollectBox"),
                UiChipShowHelper:getGoldShowStr(tb_reward.BetValue, self.ViewMgr.LanMgr.LanBase),
                UiChipShowHelper:getGoldShowStr(reward_gold, self.ViewMgr.LanMgr.LanBase))

        local ev = self.ViewMgr:GetEv("EvDesktopHGetBetReward")
        if (ev == nil) then
            ev = EvDesktopHGetBetReward:new(nil)
        end
        self.ViewMgr:SendEv(ev)
    end

    ViewHelper:UiShowInfoSuccess(tips)
end

---------------------------------------
-- 下注奖励对话框
ViewDesktopHBetReward = ViewBase:new()

---------------------------------------
function ViewDesktopHBetReward:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    o.ViewMgr = nil
    o.GoUi = nil
    o.ComUi = nil
    o.Panel = nil
    o.UILayer = nil
    o.InitDepth = nil
    o.ViewKey = nil
    o.BDesktopHDialyBetReward = nil
    o.GBtnGetAllReward = nil
    o.GTextBetTotal = nil
    o.GProBet = nil
    o.ViewDesktopH = nil
    self.BetRewardTitle = "ComReward"
    return o
end

---------------------------------------
function ViewDesktopHBetReward:OnCreate()
	ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("BetReward"))
    self.ViewMgr:BindEvListener("EvEntityInitBetReward",self)
    self.ViewDesktopH = self.ViewMgr:GetView("DesktopH")
    local co_history_close = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_history_close = co_history_close:GetChild("BtnClose").asButton
    btn_history_close.onClick:Add(
            function()
                self:_onClickBtnHelpClose()
            end
    )
	local com_shade = co_history_close:GetChild("ComShade").asCom
	com_shade.onClick:Add(
		function()
			self:_onClickBtnHelpClose()
		end
	)
    self.GBtnGetAllReward = self.ComUi:GetChild("Lan_Btn_OneKeyCollection").asButton
    self.GBtnGetAllReward.onClick:Add(
            function()
                self:_onClickGetAllBetReward()
            end
    )
    self.GTextBetTotal = self.ComUi:GetChild("BetTotal").asTextField
    self.GProBet = self.ComUi:GetChild("ProBet").asProgress
end

---------------------------------------
function ViewDesktopHBetReward:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ViewDesktopHBetReward:OnHandleEv(ev)
    if(ev ~= nil) then
        if(ev.EventName == "EvEntityInitBetReward") then
            self.BDesktopHDialyBetReward = ev.init_dailybet_reward
            local current_betgold = self.BDesktopHDialyBetReward.TotalBetGold
            self.GTextBetTotal.text = UiChipShowHelper:getGoldShowStr(current_betgold,
                    self.ViewMgr.LanMgr.LanBase)

            local can_get = false
            local t_MapGetRewardState = self.BDesktopHDialyBetReward.MapGetRewardState
            for i, v in pairs(t_MapGetRewardState) do
                if (v == DesktopHDialyGetBetRewardState.NotGet) then
                    can_get = true
                end
                local co_betreward = self.ComUi:GetChild(self.BetRewardTitle .. i).asCom
                UiDesktopHBetRewardItem:new(nil,self, co_betreward, i, v,self.ViewMgr)
            end

            self.GBtnGetAllReward.enabled = can_get
            local l = self.ViewMgr.TbDataMgr:GetMapData("DesktopHBetReward")
            local tb_betreward_k,tb_betreward_v = LuaHelper:GetTableFirstEle(l)

            for k,v in pairs(l) do
                local tb_betreward_value = tb_betreward_v.BetValue
                if (current_betgold > tb_betreward_value and
                        v.BetValue < current_betgold) then
                    tb_betreward_v = v
                else
                    if (v.BetValue > current_betgold) then
                        break
                    end
                end
            end

            local tb_maxbetreward = l[#l]
            local tb_next_betreward = nil
            if (tb_betreward_v.Id < tb_maxbetreward.Id) then
                tb_next_betreward = l[tb_betreward_v.Id]
            end

            if (tb_next_betreward == nil) then
                print("tb_maxbetreward.BetProgressValue1             "..tb_maxbetreward.BetProgressValue)
                self.GProBet.value = tb_maxbetreward.BetProgressValue
            else
                local left_betgold = current_betgold - tb_betreward_v.BetValue
                if (left_betgold < 0) then
                    left_betgold = 0
                end

                if (left_betgold == 0) then
                    print("tb_maxbetreward.BetProgressValue2             "..tb_maxbetreward.BetProgressValue)
                    self.GProBet.value = tb_betreward_v.BetProgressValue
                else
                    local bet_values = tb_next_betreward.BetValue - tb_betreward_v.BetValue
                    local bet_pro = tb_next_betreward.BetProgressValue - tb_betreward_v.BetProgressValue
                    local betpro_value = tb_betreward_v.BetProgressValue
                    betpro_value = betpro_value + ((bet_pro / bet_values) * left_betgold)
                    print("tb_maxbetreward.BetProgressValue3             "..betpro_value)
                    self.GProBet.value = betpro_value
                end
            end
        end
    end
end

---------------------------------------
function ViewDesktopHBetReward:_onClickGetAllBetReward()
    local ev = self.ViewMgr:GetEv("EvDesktopHGetBetReward")
    if(ev == nil) then
        ev = EvDesktopHGetBetReward:new(nil)
    end
    ev.factory_name = self.ViewDesktopH.FactoryName
    self.ViewMgr:SendEv(ev)
    self.GBtnGetAllReward.enabled = false
end

---------------------------------------
function ViewDesktopHBetReward:_onClickBtnHelpClose()
    self.ViewMgr:DestroyView(self)
end

---------------------------------------
ViewDesktopHBetRewardFactory = ViewFactory:new()

---------------------------------------
function ViewDesktopHBetRewardFactory:new(o,ui_package_name,ui_component_name,
                                          ui_layer,is_single,fit_screen)
    o = o or {}
    setmetatable(o,self)
    self.__index = self
    self.PackageName = ui_package_name
    self.ComponentName = ui_component_name
    self.UILayer = ui_layer
    self.IsSingle = is_single
    self.FitScreen = fit_screen
    return o
end

---------------------------------------
function ViewDesktopHBetRewardFactory:CreateView()
    local view = ViewDesktopHBetReward:new(nil)
    return view
end