ViewCreateMatch = ViewBase:new()

function ViewCreateMatch:new(o)
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
	self.MinSignupNum = 6
	self.MaxSignupNum = 2000
	self.DefaltMatchExplain = "赛事说明(可不填)"

    return o
end

function ViewCreateMatch:onCreate()
	self.ControllerMtt = self.ViewMgr.ControllerMgr:GetController("Mtt")
	local btn_return = self.ComUi:GetChild("BtnReturn").asButton
	btn_return.onClick:Add(
		function()
			self:onClickBtnReturn()
		end
	)
	local btn_create = self.ComUi:GetChild("BtnCreateMatch").asButton
	btn_create.onClick:Add(
		function()
			self:onClickBtnCreate()
		end
	)
	local com_setType = self.ComUi:GetChild("ComSetType").asCom
	local btn_tabSet = com_setType:GetChild("BtnSet").asButton
	btn_tabSet.onClick:Add(
		function()
			self:onClickBtnTabSet()
		end
	)
	self.GTextMatchExpectTime = self.ComUi:GetChild("TextMatchExpectTime").asTextField
	self.GControllerTabSet = com_setType:GetController("ControllerTabSet")
	self.GTextStartScore1 = com_setType:GetChild("TextStartScore").asTextField
	self.GTextStartBlind1 = com_setType:GetChild("TextStartBlind").asTextField
	self.GTextRaiseBlindTime1 = com_setType:GetChild("TextRaiseBlindTime").asTextField
	self.GControllerSetType = self.ComUi:GetController("ControllerSetType")
	local com_basicSet = self.ComUi:GetChild("ComBasicsSet").asCom
	self.GTextInputMatchName = com_basicSet:GetChild("TextInputMatchName").asTextField
	self.GTextInputMatchExplain = com_basicSet:GetChild("TextInputMatchExplain").asTextField
	local btn_setMatchBeginTime = com_basicSet:GetChild("BtnSetMatchTime").asButton
	btn_setMatchBeginTime.onClick:Add(
		function()
			self:onClickBtnSetMatchBeginTime()
		end
	)
	self.GTextMatchBeginTime = btn_setMatchBeginTime:GetChild("TextTime").asTextField
	local com_blindTable = com_basicSet:GetChild("ComBlindTable").asCom
	local btn_blindTable = com_blindTable:GetChild("BtnBlindTable").asButton
	btn_blindTable.onClick:Add(
		function()
			self:onClickBtnBlindTable()
		end
	)
	local btn_help = com_blindTable:GetChild("BtnHelp").asButton
	btn_help.onClick:Add(
		function()
			self:onClickBtnBlindTable()
		end
	)
	local com_matchSpeedType = com_basicSet:GetChild("ComMatchSpeedType").asCom
	self.GControllerMatchSpeed = com_matchSpeedType:GetController("ControllerMatchSpeed")
	local btn_fastMatch = com_matchSpeedType:GetChild("BtnFastMatch").asButton
	btn_fastMatch.onClick:Add(
		function(a)
			self:onClickBtnSpeedMatch(1)
		end
	)
	local btn_normalMatch = com_matchSpeedType:GetChild("BtnNormalMatch").asButton
	btn_normalMatch.onClick:Add(
		function(a)
			self:onClickBtnSpeedMatch(2)
		end
	)
	local btn_slowMatch = com_matchSpeedType:GetChild("BtnSlowMatch").asButton
	btn_slowMatch.onClick:Add(
		function(a)
			self:onClickBtnSpeedMatch(3)
		end
	)
	self.GTextMaxApplyNum = com_basicSet:GetChild("TextMaxApplyNum").asTextField
	-- 参赛人数滑动条
	self.GSliderMaxApplyNum = com_basicSet:GetChild("SliderMaxApplyNumber").asSlider
	self.GSliderMaxApplyNumExtand = ExpandSlider:new(nil,self.GSliderMaxApplyNum)
	self.GSliderMaxApplyNum.onChanged:Add(
		function()
			self:onSliderMaxApplyNumChanged()
		end
	)
	self.GSliderMaxApplyNum.onGripTouchEnd:Add(
		function()
			self.GSliderMaxApplyNumExtand:onGripTouchEnd()
		end
	)
	local com_highSet = self.ComUi:GetChild("ComHighSet").asCom
	-- 起始记分牌
	self.GTextStartScore = com_highSet:GetChild("TextStartScore").asTextField
	self.GSliderStartScore = com_highSet:GetChild("SliderStartScore").asSlider
	self.GSliderStartScoreExtand = ExpandSlider:new(nil,self.GSliderStartScore)
	self.GSliderStartScore.onChanged:Add(
		function()
			self:onSliderStartScoreChanged()
		end
	)
	self.GSliderStartScore.onGripTouchEnd:Add(
		function()
			self.GSliderStartScoreExtand:onGripTouchEnd()
		end
	)
	-- 涨盲时间
	self.GTextRaiseBlindTime = com_highSet:GetChild("TextRaiseBlindTime").asTextField
	self.GSliderRaiseBlindTime = com_highSet:GetChild("SliderRaiseBlindTime").asSlider
	self.GSliderRaiseBlindTimeExtand = ExpandSlider:new(nil,self.GSliderRaiseBlindTime)
	self.GSliderRaiseBlindTime.onChanged:Add(
			function()
				self:onSliderRaiseBlindTimeChanged()
			end
	)
	self.GSliderRaiseBlindTime.onGripTouchEnd:Add(
			function()
				self.GSliderRaiseBlindTimeExtand:onGripTouchEnd()
			end
	)
	-- 起始盲注
	self.GTextStartBlind = com_highSet:GetChild("TextStartBlind").asTextField
	self.GSliderStartBlind = com_highSet:GetChild("SliderStartBlind").asSlider
	self.GSliderStartBlindExtand = ExpandSlider:new(nil,self.GSliderStartBlind)
	self.GSliderStartBlind.onChanged:Add(
		function()
			self:onSliderStartBlindChanged()
		end
	)
	-- 截止报名级别
	self.GTextStopSignupLevel = com_highSet:GetChild("TextStopSignupLevel").asTextField
	self.GSliderStopSignupLevel = com_highSet:GetChild("SliderStopSignupLevel").asSlider
	self.GSliderStopSignupLevelExtand = ExpandSlider:new(nil,self.GSliderStopSignupLevel)
	self.GSliderStopSignupLevel.onChanged:Add(
			function()
				self:onSliderStopSignupLevelChanged()
			end
	)
	self.GSliderStopSignupLevel.onGripTouchEnd:Add(
			function()
				self.GSliderStopSignupLevelExtand:onGripTouchEnd()
			end
	)
	-- 重购次数
	self.GTextRebuyTimes = com_highSet:GetChild("TextRebuyTimes").asTextField
	self.GSliderRebuyTimes = com_highSet:GetChild("SliderRebuyTimes").asSlider
	self.GSliderRebuyTimesExtand = ExpandSlider:new(nil,self.GSliderRebuyTimes)
	self.GSliderRebuyTimes.onChanged:Add(
			function()
				self:onSliderRebuyTimesChanged()
			end
	)
	self.GSliderRebuyTimes.onGripTouchEnd:Add(
			function()
				self.GSliderRebuyTimesExtand:onGripTouchEnd()
			end
	)
	-- 增购倍数
	self.GGroupAddon = com_highSet:GetChild("GroupAddon").asGroup
	self.GTextAddonNum = com_highSet:GetChild("TextAddonNum").asTextField
	self.GSliderAddonNum = com_highSet:GetChild("SliderAddonNum").asSlider
	self.GSliderAddonNumExtand = ExpandSlider:new(nil,self.GSliderAddonNum)
	self.GSliderAddonNum.onChanged:Add(
			function()
				self:onSliderAddonNumChanged()
			end
	)
	self.GSliderAddonNum.onGripTouchEnd:Add(
			function()
				self.GSliderAddonNumExtand:onGripTouchEnd()
			end
	)
	local btn_allowAddon = com_highSet:GetChild("BtnAllowAddon").asButton
	btn_allowAddon.onClick:Add(
		function()
			self:onClickBtnAllowAddon()
		end
	)
	local com_tablePlayerNum = com_highSet:GetChild("ComTablePlayerNum").asCom
	self.GControllerTablePlayerNum = com_tablePlayerNum:GetController("ControllerTablePlayerNum")
	local btn_sixNum = com_tablePlayerNum:GetChild("BtnSixNum").asButton
	btn_sixNum.onClick:Add(
		function(a)
			self:onClickBtnTableNum(6)
		end
	)
	local btn_nineNum = com_tablePlayerNum:GetChild("BtnNineNum").asButton
	btn_nineNum.onClick:Add(
		function(a)
			self:onClickBtnTableNum(9)
		end
	)
	self.GComSetMatchBeginTime = self.ComUi:GetChild("ComChooseTime").asCom
	self.GTextInputMonth = self.GComSetMatchBeginTime:GetChild("TextInputMonth").asTextField
	self.GTextInputDay = self.GComSetMatchBeginTime:GetChild("TextInputDay").asTextField
	self.GTextInputHour = self.GComSetMatchBeginTime:GetChild("TextInputHour").asTextField
	self.GTextInputMinute = self.GComSetMatchBeginTime:GetChild("TextInputMinute").asTextField
	local btn_close = self.GComSetMatchBeginTime:GetChild("BtnClose").asButton
	btn_close.onClick:Add(
		function()
			self.GComSetMatchBeginTime.visible = false
		end
	)
	local btn_ensuretime = self.GComSetMatchBeginTime:GetChild("BtnEnsure").asButton
	btn_ensuretime.onClick:Add(
		function()
			self:onClickBtnEnsureTime()
		end
	)
	self.HasSetContent = false
	self.IsHighSet = false
	self:init()
end

function ViewCreateMatch:init()
	local temp = {6,9,12,18,24,27,30,36,42,48,54,60,63,66,72,78,81,84,90,96,100,200,300,400,500,1000,1500,2000}
	self.GSliderMaxApplyNumExtand:setList(temp)
	temp = {1000,2000,3000,4000,5000,6000,8000,10000,15000,20000,25000,30000,40000,50000}
	self.GSliderStartScoreExtand:setList(temp)
	temp = {1,2,3,4,5,6,7,8,9,10,11,12,15,20,25,30}
	self.GSliderRaiseBlindTimeExtand:setList(temp)
	temp = {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15}
	self.GSliderStopSignupLevelExtand:setList(temp)
	temp = {0,1,2,3,4,5,6,7,8,9}
	self.GSliderRebuyTimesExtand:setList(temp)
	temp = {1,1.5,2,3}
	self.GSliderAddonNumExtand:setList(temp)
	self.StartScore = 0
	self.AllowAddon = false
	self.BlindTableName = nil
	self.MaxApplyNum = 0
	self.StopSignupLevel = 0
	self.AddonNum = 0
	self.TableNumType = nil
	self.RebuyTimes = 0
	self.StartBlind = 0
	self.StartBlindId = 0
	self.RaiseBlindTime = 0
	self:onClickBtnSpeedMatch(3)
	self.GSliderStopSignupLevelExtand:setValue(6)
	self:onSliderStopSignupLevelChanged()
	self.GSliderAddonNumExtand:setValue(1)
	self:onSliderAddonNumChanged()
	self:onClickBtnTableNum(6)
	self.GSliderRebuyTimesExtand:setValue(0)
	self:onSliderRebuyTimesChanged()
	self.GSliderMaxApplyNumExtand:setValue(100)
	self:onSliderMaxApplyNumChanged()
end

function ViewCreateMatch:onClickBtnReturn()
	if(self.GControllerSetType.selectedIndex == 1)
	then
		self.GControllerSetType:SetSelectedIndex(0)
		self.GControllerTabSet:SetSelectedIndex(0)
		return
	end
	if(self.HasSetContent)
	then
		local msg_box = self.ViewMgr:createView("MsgBox")
		msg_box:useTwoBtn2("","返回将不会保存设置内容。确定返回吗？",
				function()
					self.ViewMgr:destroyView(self)
				end,
				function()
					self.ViewMgr:destroyView(msg_box)
				end
		)
	else
		self.ViewMgr:destroyView(self)
	end
end

function ViewCreateMatch:onClickBtnTabSet()
	if(self.GControllerSetType.selectedIndex == 0)
	then
		self.GControllerSetType:SetSelectedIndex(1)
		self.GControllerTabSet:SetSelectedIndex(1)
	else
		self.GControllerSetType:SetSelectedIndex(0)
		self.GControllerTabSet:SetSelectedIndex(0)
	end
end

function ViewCreateMatch:onSliderStartScoreChanged()
	local value =  self.GSliderStartScoreExtand:onSliderChange()
	if(value ~= nil)
	then
		self.StartScore = value
		self.GTextStartScore.text = UiChipShowHelper:getGoldShowStr3(value) .. " " .. math.floor(value/self.StartBlind).. "BB"
		self.GTextStartScore1.text = UiChipShowHelper:getGoldShowStr3(value)
		local tb_mgr = CS.Casinos.CasinosContext.Instance.TbDataMgrLua
		local list_data = tb_mgr:GetMapData(self.BlindTableName)
		local temp = self.StartScore/10
		local temp1 = {}
		for i = 1,#list_data do
			temp1[i] = list_data[i].BlindsBig
			if(temp == list_data[i].BlindsBig)
			then
				break
			elseif(list_data[i].BlindsBig > temp)
			then
				temp1[i] = nil
				break
			end
		end
		self.GSliderStartBlindExtand:setList(temp1)
		self.GSliderStartBlindExtand:setValue(temp1[1])
		self:onSliderStartBlindChanged()
	end
end

function ViewCreateMatch:onSliderRaiseBlindTimeChanged()
	local value = self.GSliderRaiseBlindTimeExtand:onSliderChange()
	if(value ~= nil)
	then
		self.GTextRaiseBlindTime.text = tostring(value) .. "分钟"
		self.GTextRaiseBlindTime1.text = tostring(value) .. "分钟"
		self.RaiseBlindTime = value * 60
	end
end

function ViewCreateMatch:onSliderStartBlindChanged()
	local value = self.GSliderStartBlindExtand:onSliderChange()
	if(value ~= nil)
	then
		local temp = tostring(math.floor(value/2)) .. "/".. tostring(value)
		self.GTextStartBlind.text = temp
		self.GTextStartBlind1.text = temp
		local tb_mgr = CS.Casinos.CasinosContext.Instance.TbDataMgrLua
		local list_data = tb_mgr:GetMapData(self.BlindTableName)
		for i = 1,#list_data do
			if (list_data[i].BlindsBig == value) then
				self.StartBlind = list_data[i].BlindsBig
				self.StartBlindId = i
			end
		end
		self.GTextStartScore.text = UiChipShowHelper:getGoldShowStr3(self.StartScore) .. " " .. math.floor(self.StartScore/self.StartBlind)
		.. "BB"
	end
end

function ViewCreateMatch:onSliderStopSignupLevelChanged()
	local value = self.GSliderStopSignupLevelExtand:onSliderChange()
	if(value ~= nil)
	then
		self.GTextStopSignupLevel.text = tostring(value)
		self.StopSignupLevel = value
	end
end

function ViewCreateMatch:onSliderRebuyTimesChanged()
	local value = self.GSliderRebuyTimesExtand:onSliderChange()
	if(value ~= nil)
	then
		self.GTextRebuyTimes.text = tostring(value) .. "次"
		self.RebuyTimes = value
	end
end

function ViewCreateMatch:onSliderAddonNumChanged()
	local value = self.GSliderAddonNumExtand:onSliderChange()
	if(value ~= nil)
	then
		self.GTextAddonNum.text = tostring(value) .. "倍"
		self.AddonNum = value * self.StartScore
	end
end

function ViewCreateMatch:onClickBtnAllowAddon()
	if(self.AllowAddon)
	then
		self.AllowAddon = false
	else
		self.AllowAddon = true
	end
	self.GGroupAddon.visible = self.AllowAddon
end

function ViewCreateMatch:onClickBtnTableNum(num)
	if(num == 6)
	then
		self.GControllerTablePlayerNum:SetSelectedIndex(0)
		self.TableNumType = TexasDesktopSeatNum.Six
	elseif(num == 9)
	then
		self.GControllerTablePlayerNum:SetSelectedIndex(1)
		self.TableNumType = TexasDesktopSeatNum.Nine
	end
end

function ViewCreateMatch:onClickBtnSetMatchBeginTime()
	self.GComSetMatchBeginTime.visible = true
end

function ViewCreateMatch:onClickBtnCreate()
	local nowTm = CS.System.DateTime.Now
	if(self.MatchBeginTime < nowTm)
	then
		ViewHelper:UiShowInfoSuccess("请输入正确的时间")
		return
	end
	local blind_info = BMatchTexasRaiseBlindTbInfo:new(nil)
	blind_info.RaiseBlindTbName = self.BlindTableName
	local tb_mgr = CS.Casinos.CasinosContext.Instance.TbDataMgrLua
	local blind_table = tb_mgr:GetMapData(self.BlindTableName)
	blind_info.BeginId = self.StartBlindId
	blind_info.EndId = #blind_table
	local temp = {}
	for i = self.StartBlindId,#blind_table do
		table.insert(temp,i)
	end
	blind_info.ListRaiseBlindTbIdCanRebuy = temp
	blind_info.ListRaiseBlindTbIdCanAddon = temp
	blind_info.RebuyGold = self.StartScore
	blind_info.RebuyScore = self.StartScore
	blind_info.AddonGold = self.AddonNum
	blind_info.AddonScore = self.AddonNum
	blind_info.RaiseBlindTmSpan = self.RaiseBlindTime

	local create_info = BMatchTexasCreateInfo:new(nil)
	create_info.IconType = nil --赛事图标类型
	create_info.ScopeType = MatchTexasScopeType.Player --赛事开放范围
	create_info.Name = self.GTextInputMatchName --赛事名字
	create_info.Discribe = self.GTextInputMatchExplain --赛事描述
	create_info.DtMatchOpen = nowTm --比赛开放时间
	create_info.DtSignup = nowTm --报名时间
	create_info.DtMatchBegin = self.MatchBeginTime --比赛开始时间
	create_info.DtSignupClose = self.MatchBeginTime:AddSeconds(self.RaiseBlindTime * (self.StopSignupLevel - 1))
	create_info.PlayerNumMin = 6
	create_info.PlayerNumMax = self.MaxApplyNum
	create_info.SignupFee = 0
	create_info.ServiceFee = 0
	create_info.CanRebuyCount = self.RebuyTimes
	if(self.AllowAddon)
	then
		create_info.CanAddonCount = 1
	else
		create_info.CanAddonCount = 0
	end
	create_info.InitScore = self.StartScore --初始记分牌
	create_info.SeatNum = self.TableNumType --座位数
	create_info.CreatePlayerGuid = self.ControllerMtt.Guid --创建赛事玩家Guid，公共赛事该Guid为空
	create_info.RaiseBlindTbInfo = blind_info --升盲表静态信息
	local ev = self.ViewMgr:getEv("EvUiRequestCreatePrivateMatch")
	if ev == nil then
		ev = EvUiRequestCreatePrivateMatch:new(nil)
	end
	ev.MatchCreateInfo = create_info
	self.ViewMgr:sendEv(ev)
end

function ViewCreateMatch:onClickBtnBlindTable()
	self.ViewMgr:createView("BlindTable")
end

function ViewCreateMatch:onClickBtnSpeedMatch(speed_type)
	if(speed_type == 1)
	then
		self.GControllerMatchSpeed:SetSelectedIndex(0)
		self.BlindTableName = "TexasRaiseBlindsFast"
		-- 起始记分牌 5000 涨盲时间 2min
		self.GSliderStartScoreExtand:setValue(5000)
		self.GSliderRaiseBlindTimeExtand:setValue(2)
	elseif(speed_type == 2)
	then
		self.GControllerMatchSpeed:SetSelectedIndex(1)
		self.BlindTableName = "TexasRaiseBlindsNormal"
		-- 起始记分牌 10000 涨盲时间 3min
		self.GSliderStartScoreExtand:setValue(10000)
		self.GSliderRaiseBlindTimeExtand:setValue(3)
	elseif(speed_type == 3)
	then
		self.GControllerMatchSpeed:SetSelectedIndex(2)
		self.BlindTableName = "TexasRaiseBlindsSlow"
		-- 起始记分牌 15000 涨盲时间 5min
		self.GSliderStartScoreExtand:setValue(15000)
		self.GSliderRaiseBlindTimeExtand:setValue(5)
	end
	self:onSliderStartScoreChanged()
	self:onSliderRaiseBlindTimeChanged()
end

function ViewCreateMatch:onSliderMaxApplyNumChanged()
	local value = self.GSliderMaxApplyNumExtand:onSliderChange()
	if value ~= nil then
		self.GTextMaxApplyNum.text = tostring(value)
		self.MaxApplyNum = value
	end
end

function ViewCreateMatch:onClickBtnEnsureTime()
	local month = tonumber(self.GTextInputMonth.text)
	local day = tonumber(self.GTextInputDay.text)
	local hour = tonumber(self.GTextInputHour.text)
	local minute = tonumber(self.GTextInputMinute.text)
	local result =  self:DataIsLegal(month,day,hour,minute)
	if(result == false)
	then
		ViewHelper:UiShowInfoSuccess("请输入正确的时间")
	else
		local match_begintime = CS.System.DateTime(2018,month,day,hour,minute,0)
		local now_time = CS.System.DateTime.Now
		if(match_begintime < now_time)
		then
			ViewHelper:UiShowInfoSuccess("请输入正确的时间")
			return
		end
		self.MatchBeginTime  = match_begintime
		self.GTextMatchBeginTime.text = "2018/"..string.format("%02s",month) .. "/" ..string.format("%02s",day)
										.." " .. string.format("%02s",hour) .. ":" .. string.format("%02s",minute)
		self.GComSetMatchBeginTime.visible = false
	end
end

function ViewCreateMatch:DataIsLegal(month,day,hour,minute)
	if(month == nil or day == nil or hour == nil or minute == nil)
	then
		return false
	end
	if(month <= 0 or day <= 0 or hour < 0 or minute < 0)
	then
		return false
	end
	if(month >12)
	then
		return false
	end
	if(month == 1 or month == 3 or month == 5 or month == 7 or month == 8 or month == 10 or month == 12)
	then
		if(day >31)
		then
			return false
		end
	end
	if(month == 4 or month == 6 or month == 9)
	then
		return false
	end
	if(month == 2)
	then
		if(day > 28)
		then
			return false
		end
	end
	if(hour > 23)
	then
		return false
	end
	if(minute > 59)
	then
		return false
	end
	return true
end


ExpandSlider = {}
function ExpandSlider:new(o,gSlider)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.GSlider = gSlider
	o.GTextTitle = text_title
	o.ListPoint = {}
	return o
end

function ExpandSlider:setList(list_value)
	local point_num = #list_value
	local length = math.floor(100/(#list_value - 1))
	for i = 1,#list_value do
		local point_view = (i - 1) * length
		if i == #list_value then
			point_view = 100
		end
		local start_value = point_view - length/2
		local end_value = point_view + length/2
		if start_value < 0 then
			start_value = 0
		elseif(end_value > 100)
		then
			end_value = 100
		end
		local point = ExpandSliderPoint:new(nil,point_view,start_value,end_value,list_value[i])
		self.ListPoint[i] = point
	end
end

function ExpandSlider:setValue(value)
	for i = 1,#self.ListPoint do
		if(self.ListPoint[i].Value == value)
		then
			self.GSlider.value = self.ListPoint[i].SliderShowValue
		end
	end
end

function ExpandSlider:onSliderChange()
	local point = self.ListPoint[self:findCurrentPointIndex()]
	if point == nil then
		return
	end
	return point.Value
end

function ExpandSlider:onGripTouchEnd()
	local point = self.ListPoint[self:findCurrentPointIndex()]
	if point == nil then
		return
	end
	self.GSlider.value = point.SliderShowValue
end

function ExpandSlider:findCurrentPointIndex()
	local slider_Value = self.GSlider.value
	for i = 1,#self.ListPoint do
		local point = self.ListPoint[i]
		if(i == 1)
		then
			if slider_Value >= point.SliderStartValue and slider_Value <= point.SliderEndValue
			then
				return i
			end
		else
			if slider_Value > point.SliderStartValue and slider_Value <= point.SliderEndValue
			then
				return i
			end
		end
	end
end

ExpandSliderPoint = {}
function ExpandSliderPoint:new(o,slider_value,start_value,end_value,value)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.SliderShowValue = slider_value
	o.SliderStartValue = start_value
	o.SliderEndValue = end_value
	o.Value = value
	return o
end

ViewCreateMatchFactory = ViewFactory:new()

function ViewCreateMatchFactory:new(o,ui_package_name,ui_component_name,
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

function ViewCreateMatchFactory:createView()	
	local view = ViewCreateMatch:new(nil)	
	return view
end