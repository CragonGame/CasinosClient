-- Copyright(c) Cragon. All rights reserved.

ViewDesktopHHistory = ViewBase:new()

function ViewDesktopHHistory:new(o)
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
	o.GListHistory = nil
	o.ViewDesktopH = nil

	return o
end

function ViewDesktopHHistory:onCreate()
	ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("DesktopHHistory"))
	self.ViewMgr:BindEvListener("EvEntityDesktopHGameEndState",self)
	local co_history_close = self.ComUi:GetChild("ComBgAndClose").asCom
	local btn_history_close = co_history_close:GetChild("BtnClose").asButton
	btn_history_close.onClick:Add(
		function()
			self:_onClickBtnHistoryClose()
		end
	)
	local com_shade = co_history_close:GetChild("ComShade").asCom
	com_shade.onClick:Add(
		function()
			self:_onClickBtnHistoryClose()
		end
	)
	self.GListHistory = self.ComUi:GetChild("ListHistroy").asList
	self.ViewDesktopH = self.ViewMgr:GetView("DesktopH")
end

function ViewDesktopHHistory:onDestroy()
	self.ViewMgr:UnbindEvListener(self)
end

function ViewDesktopHHistory:onHandleEv(ev)
	if(ev ~= nil)
	then
		if(ev.EventName == "EvEntityDesktopHGameEndState")
		then
			self:setHistory(self.ViewDesktopH.ControllerDesktopH.MapBetPotWinlooseRecord)
		end
	end
end

function ViewDesktopHHistory:setHistory(map_history)
	self.GListHistory:RemoveChildrenToPool()
	for i = 0, #map_history do
		local co_itemhistory = self.GListHistory:AddItemFromPool().asCom
		ItemDesktopHHistroy:new(nil,co_itemhistory, self.ViewDesktopH, i, map_history[i])
	end
end

function ViewDesktopHHistory:_onClickBtnHistoryClose()
	self.ViewMgr:DestroyView(self)
end



ViewDesktopHHistoryFactory = ViewFactory:new()

function ViewDesktopHHistoryFactory:new(o,ui_package_name,ui_component_name,
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

function ViewDesktopHHistoryFactory:CreateView()
	local view = ViewDesktopHHistory:new(nil)
	return view
end