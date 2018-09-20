ViewRechargeFirst = ViewBase:new()

function ViewRechargeFirst:new(o)
    o = o or {}
    setmetatable(o,self)
    self.__index = self	
	self.ViewMgr = nil
	self.GoUi = nil
	self.ComUi = nil
	self.Panel = nil
	self.UILayer = nil
	self.InitDepth = nil
	self.ViewKey = nil

    return o
end

function ViewRechargeFirst:onCreate()
	ViewHelper:PopUi(self.ComUi)
	local btn_comfirm = self.ComUi:GetChild("Confirm").asButton
    btn_comfirm.onClick:Add(
		function()
			self:onClickCharge()
		end
	)
    local common_bgandreturn = self.ComUi:GetChild("ComBgAndClose").asCom
    local btn_return = common_bgandreturn:GetChild("BtnClose").asButton
    btn_return.onClick:Add(
		function()
			self:onClickBtnReturn()
		end
	)
	local com_shade = common_bgandreturn:GetChild("ComShade").asCom
	com_shade.onClick:Add(
		function()
			self:onClickBtnReturn()
		end
	)
end

function ViewRechargeFirst:onClickBtnReturn()
	self.ViewMgr:destroyView(self)
end

function ViewRechargeFirst:onClickCharge()
	local ev = self.ViewMgr:getEv("EvUiRequestFirstRecharge")
	if(ev == nil)
	then
		ev = EvUiRequestFirstRecharge:new(nil)
	end
	self.ViewMgr:sendEv(ev)
    self:onClickBtnReturn()
end
				

			

ViewRechargeFirstFactory = ViewFactory:new()

function ViewRechargeFirstFactory:new(o,ui_package_name,ui_component_name,
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

function ViewRechargeFirstFactory:createView()	
	local view = ViewRechargeFirst:new(nil)	
	return view
end