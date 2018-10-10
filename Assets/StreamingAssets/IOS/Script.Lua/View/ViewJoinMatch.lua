-- Copyright(c) Cragon. All rights reserved.
-- 加入玩家创建的赛事

ViewJoinMatch = ViewBase:new()

function ViewJoinMatch:new(o)
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

function ViewJoinMatch:onCreate()
	ViewHelper:PopUi(self.ComUi)
	local com_bg = self.ComUi:GetChild("ComBgAndClose").asCom
	local com_shade = com_bg:GetChild("ComShade").asCom
	com_shade.onClick:Add(
			function()
				self:onClickBtnClose()
			end
	)
	local btn_close = self.ComUi:GetChild("BtnClose").asButton
	btn_close.onClick:Add(
		function()
			self:onClickBtnClose()
		end
	)
	local btn_delete = self.ComUi:GetChild("BtnDelete").asButton
	btn_delete.onClick:Add(
		function()
			self:onClickBtnDelete()
 		end
	)
	for i = 0,9 do
		local btn_num = self.ComUi:GetChild("BtnNum".. i).asButton
		btn_num.onClick:Add(
			function(a)
				self:onClickBtnNum(i)
			end
		)
	end
	self.GTextPassword = self.ComUi:GetChild("TextPassword").asTextField
	self.Password = ""
end

function ViewJoinMatch:onClickBtnDelete()
	if(#self.Password == 0)
	then
		return
	else
        self.Password = string.sub(self.Password,1,#self.Password - 1)
        self:onPassWordChanged()
	end
end

function ViewJoinMatch:onClickBtnClose()
	self.ViewMgr:destroyView(self)
end

function ViewJoinMatch:onClickBtnNum(num)
	self.Password = self.Password .. num
	if(#self.Password == 6)
	then
		local ev = self.ViewMgr:getEv("EvUiRequestGetMatchDetailedInfoByInvitation")
		if(ev == nil)
		then
			ev = EvUiRequestGetMatchDetailedInfoByInvitation:new(nil)
		end
		ev.InvitationCode = tonumber(self.Password)
		self.ViewMgr:sendEv(ev)
		self.Password = ""
	end
	self:onPassWordChanged()
end

function ViewJoinMatch:onPassWordChanged()
	--local str = string.format("%- 6s",self.Password)
    local temp = ""
    for i = 1,6 - #self.Password do
        temp = temp .. "-"
    end
    self.GTextPassword.text = self.Password..temp
end

ViewJoinMatchFactory = ViewFactory:new()

function ViewJoinMatchFactory:new(o,ui_package_name,ui_component_name,
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

function ViewJoinMatchFactory:createView()
	local view = ViewJoinMatch:new(nil)
	return view
end