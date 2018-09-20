ViewApplySucceed = ViewBase:new()

function ViewApplySucceed:new(o)
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
	o.Instance = o

    return o
end

function ViewApplySucceed:onCreate()	
	ViewHelper:PopUi(self.ComUi,"报名成功")
	local com_shade = self.ComUi:GetChild("ComBgAndClose").asCom
	com_shade.onClick:Add(
		function()
			self:close()
		end
	)
	local btn_ensure = self.ComUi:GetChild("BtnEnsure").asButton
	btn_ensure.onClick:Add(
		function()
			self:close()
		end
	)
	self.GTextMatchName = self.ComUi:GetChild("TextMatchName").asTextField
	self.GTextEntryFee = self.ComUi:GetChild("TextEntryFee").asTextField
	self.GTextMatchBeginTime = self.ComUi:GetChild("TextMatchBeginTime").asTextField
end

function ViewApplySucceed:SetMatchInfo(matchInfo)
	self.GTextMatchName.text = matchInfo.Name
	local text_SignUpfee = nil
	if(matchInfo.SignupFee == 0)
	then
		text_SignUpfee = "免费"
	else
		text_SignUpfee = tostring(matchInfo.SignupFee) .. "+" .. tostring(matchInfo.ServiceFee)
	end
	self.GTextEntryFee.text = text_SignUpfee
	local nowtm = CS.System.DateTime.Now
	local match_beginTm = matchInfo.DtMatchBegin
	local text_day = nil
	local day = match_beginTm.Day - nowtm.Day
	if(day == 0)
	then
		text_day = "今天"
	elseif(day == 1)
	then
		text_day = "明天"
	elseif(day == 2)
	then
		text_day = "后天"
	elseif(day > 2)
	then
		text_day = string.format("%02s",match_beginTm.Month) .. "月" ..tostring(match_beginTm.Day) .. "日"
	end
	local text_time = string.format("%02s",match_beginTm.Hour) .. ":" .. string.format("%02s",match_beginTm.Minute)
	self.GTextMatchBeginTime.text = text_day .. text_time
end

function ViewApplySucceed:close()
	self.ViewMgr:destroyView(self)
	local ev = self.ViewMgr:getEv("EvUiRequestPublicMatchList")
	if(ev == nil)
	then
		ev = EvUiRequestPublicMatchList:new(nil)
	end
	self.ViewMgr:sendEv(ev)
end

ViewApplySucceedFactory = ViewFactory:new()

function ViewApplySucceedFactory:new(o,ui_package_name,ui_component_name,
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

function ViewApplySucceedFactory:createView()	
	local view = ViewApplySucceed:new(nil)	
	return view
end