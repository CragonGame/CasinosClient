ViewDesktopHRewardPot = ViewBase:new()

function ViewDesktopHRewardPot:new(o)
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

    return o
end

function ViewDesktopHRewardPot:onCreate()
	ViewHelper:PopUi(self.ComUi)
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.ViewDesktopH = self.ViewMgr:getView("DesktopH")
    local co_rewardpot_close = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_rewardpot_close = co_rewardpot_close:GetChild("BtnClose").asButton
	btn_rewardpot_close.onClick:Add(
		function()
			self:onClickBtnRewardPotClose()
		end
	)
	local com_shade = co_rewardpot_close:GetChild("ComShade").asCom
	com_shade.onClick:Add(
		function()
			self:onClickBtnRewardPotClose()
		end
	)
	self.GTextRewardPot = self.ComUi:GetChild("RewardPot").asTextField
	local btn_tabPlayerInfo = self.ComUi:GetChild("BtnTabPlayerInfo").asButton
	btn_tabPlayerInfo.onClick:Add(
		function()
			self:onClickCoPlayerInfo()
		end
	)
	local btn_tabRewardPot = self.ComUi:GetChild("BtnTabRewardPot").asButton
	btn_tabRewardPot.onClick:Add(
		function()
			self:onClickCoRewardPot()
		end
	)
	self.GTextTitleLeft = self.ComUi:GetChild("TextTitleLeft").asTextField
	self.GTextTitleRight = self.ComUi:GetChild("TextTitleRight").asTextField
    self.ControllerRewardPot = self.ComUi:GetController("ControllerRewardPot")
    self.ControllerRewardPot.selectedIndex = 0
	self:changeTab()
    self.ListPlayerInfo = {}
	self.ViewMgr:bindEvListener("EvEntityDesktopHGetRewardPotInfo",self)

end

function ViewDesktopHRewardPot:onDestroy()
	self.ViewMgr:unbindEvListener(self)
end

function ViewDesktopHRewardPot:onHandleEv(ev)
	if(ev ~= nil)
	then
		if(ev.EventName == "EvEntityDesktopHGetRewardPotInfo")
		then
			self:setRewardPotInfo(ev.reward_totalgolds, ev.total_info)
		end
	end
end

function ViewDesktopHRewardPot:setRewardPotInfo(total_golds,total_info)
    local glist_playerinfo = self.ComUi:GetChild("ListPlayerInfo").asList
    glist_playerinfo:SetVirtual()
    glist_playerinfo.itemRenderer = function(a,b)
        self:RenderListItem(a,b)
    end
    self.GTextRewardPot.text = UiChipShowHelper:getGoldShowStr(total_golds, self.ViewMgr.LanMgr.LanBase, false)
    if (total_info.list_card ~= nil)
    then
        for i = 0,total_info.list_card.Count - 1 do
            local loader = self.ComUi:GetChild("Card" .. i).asLoader
            local res = self.ViewDesktopH.UiDesktopHBase:getCardResName(total_info.list_card[i])
            loader.icon = self.CasinosContext.PathMgr:combinePersistentDataPath
            (CS.Casinos.UiHelperCasinos:getABCardResourceTitlePath() .. string.lower(res) .. ".ab")
        end
    end
    if (total_info.list_playerinfo ~= nil)
    then
        for i = 0,total_info.list_playerinfo.Count - 1 do
            table.insert(self.ListPlayerInfo,total_info.list_playerinfo[i])
        end
        table.sort(self.ListPlayerInfo,
                function(a,b)
                    return a.win_gold > b.win_gold
                end
        )
        glist_playerinfo.numItems = #self.ListPlayerInfo
    end
    local win_totalgold = self.ComUi:GetChild("WinTotalGold").asTextField
    win_totalgold.text = UiChipShowHelper:getGoldShowStr(total_info.win_rewardpot_gold, self.ViewMgr.LanMgr.LanBase, false)
    local tm = self.ComUi:GetChild("WinTime").asTextField
    tm.text = CS.Casinos.UiHelper.getLocalTmToString(total_info.date_time)
    end

function ViewDesktopHRewardPot:RenderListItem(index,obj)
	local com = CS.Casinos.LuaHelper.GObjectCastToGCom(obj)
	local item = ItemDesktopHRewardPotPlayerInfo:new(nil,com)
    if (#self.ListPlayerInfo> index)
	then
		local player_info = self.ListPlayerInfo[index + 1]
        item:setDesktopHRewardPotPlayerInfo(player_info)
	end
end

function ViewDesktopHRewardPot:onClickCoPlayerInfo()
	self.ControllerRewardPot.selectedIndex = 1
	self:changeTab()
end

function ViewDesktopHRewardPot:onClickCoRewardPot()
	self.ControllerRewardPot.selectedIndex = 0
	self:changeTab()
end

function ViewDesktopHRewardPot:onClickBtnRewardPotClose()
	self.ViewMgr:destroyView(self)
end
	
function ViewDesktopHRewardPot:changeTab()
	if(self.ControllerRewardPot.selectedIndex == 0)
	then
		ViewHelper:SetUiTitle(self.GTextTitleLeft,"奖池信息")
		self.GTextTitleRight.text = "中奖纪录"
	else
		ViewHelper:SetUiTitle(self.GTextTitleRight,"中奖纪录")
		self.GTextTitleLeft.text = "奖池信息"
	end
end
			

ViewDesktopHRewardPotFactory = ViewFactory:new()

function ViewDesktopHRewardPotFactory:new(o,ui_package_name,ui_component_name,
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

function ViewDesktopHRewardPotFactory:createView()	
	local view = ViewDesktopHRewardPot:new(nil)	
	return view
end