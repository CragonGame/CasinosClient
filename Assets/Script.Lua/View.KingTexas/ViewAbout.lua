-- Copyright(c) Cragon. All rights reserved.

ViewAbout = ViewBase:new()

function ViewAbout:new(o)
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

function ViewAbout:onCreate()
	print("ViewAbout")
	ViewHelper:PopUi(self.ComUi,self.ViewMgr.LanMgr:getLanValue("About"))
	self.ControllerAbout = self.ComUi:GetController("ControllerAbout")
	local com_linkAbout = self.ComUi:GetChild("ComAboutLink")
	com_linkAbout.onClick:Add(
		function()
			self:onClickAbout()
		end
	)
	local com_linkPrivacy = self.ComUi:GetChild("ComPrivacyLink")
	com_linkPrivacy.onClick:Add(
		function() 
			self:onClickPrivacy()
		end
	)
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
	
	--self:Test()
end

function ViewAbout:Test()

end

function ViewAbout:onClickBtnClose()
	self.ViewMgr:destroyView(self)
end

function ViewAbout:onClickAbout()
	self.ControllerAbout:SetSelectedIndex(0)
end

function ViewAbout:onClickPrivacy()
	self.ControllerAbout:SetSelectedIndex(1)
end



ViewAboutFactory = ViewFactory:new()

function ViewAboutFactory:new(o,ui_package_name,ui_component_name,
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

function ViewAboutFactory:createView()	
	local view = ViewAbout:new(nil)	
	return view
end