ViewForestPartyMenu = ViewBase:new()

function ViewForestPartyMenu:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
    if(self.Instance==nil)
	then
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

function ViewForestPartyMenu:onCreate()
	local com_shade = self.ComUi:GetChild("ComShade").asCom
    com_shade.onClick:Add(
		function()
			self:onClickComShade()
		end
	)
    local com_menuEx = self.ComUi:GetChild("CoMenuEx").asCom
    local btn_return = com_menuEx:GetChild("BtnReturn").asButton
    btn_return.onClick:Add(
		function()
			self:onClickBtnReturn()
		end
	)
    local btn_rule = com_menuEx:GetChild("BtnRule").asButton
    btn_rule.onClick:Add(
		function()
			self:onClickBtnRule()
		end
	)
    self.TransitionMenu = self.ComUi:GetTransition("TransitionMenu")
    self.TransitionMenu:Play()
end

function ViewForestPartyMenu:onClickBtnRule()
	self.ViewMgr.createView("ForestPartyRule")
	self.TransitionMenu:PlayReverse(
		function()
			self.ViewMgr.destroyView(self)
		end
	)
end

function ViewForestPartyMenu:onClickBtnReturn()
	self.TransitionMenu:PlayReverse(
		function()
			self:leave()
		end
	)
end

function ViewForestPartyMenu:onClickComShade()
	self.TransitionMenu.PlayReverse(
		self.ViewMgr.destroyView(self)
	)
end

function ViewForestPartyMenu:leave()
	self.ViewMgr.destroyView(self)
	local ev = self.ViewMgr:getEv("EvUiRequestLeaveForestParty")
	if(ev == nil)
	then
		ev = EvUiRequestLeaveForestParty:new(nil)
	end
	self.ViewMgr:sendEv(ev)
end
