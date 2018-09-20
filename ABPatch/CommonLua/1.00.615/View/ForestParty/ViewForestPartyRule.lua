ViewForestPartyRule = ViewBase:new()

function ViewForestPartyRule:new(o)
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

function ViewForestPartyRule:onCreate()
	self.GTextMaxBet = self.ComUi:GetChild("TextMaxBet").asTextField
    local com_bgAndClose = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_close = com_bgAndClose:GetChild("BtnClose").asButton
    btn_close.onClick:Add(
		function()
			self:onClickBtnClose()
		end
	)
    self.GTextMaxBet.text = CS.Casinos.CasinosContext.Instance.UiChipShowHelper:getGoldShowStr(ControllerForestParty.TbdataDeskTop.BetLimitMax, CS.Casinos.CasinosContext.Instance.LanMgr.LanBase, true, 0)
end

function ViewForestPartyRule:onClickBtnClose()
	self.ViewMgr.destroyView(self)
end