ItemMatchInfo = {}

function ItemMatchInfo:new(o,com,match_info,self_join,view_mgr)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	o.MatchInfo = match_info
	o.Com = com
	local loadermatchtype = com:GetChild("LoaderMatchType").asLoader
	loadermatchtype.url = CS.FairyGUI.UIPackage.GetItemURL("MatchLobby","Normal")
	local text_matchtitle = com:GetChild("TextMatchTitle").asTextField
	text_matchtitle.text = match_info.Name
	local com_rebuy = com:GetChild("ComRebuy").asCom
	com_rebuy.visible = match_info.CanRebuyCount > 0
	local com_addon = com:GetChild("ComAddon").asCom
	com_addon.visible = match_info.CanAddonCount > 0
	local com_snowball = com:GetChild("ComSnowBall").asCom
	com_snowball.visible = match_info.IsSnowballReward
	local text_reward = com:GetChild("TextMatchReward").asTextField
	text_reward.text = UiChipShowHelper:getGoldShowStr2(match_info.TotalRewardGold,view_mgr.LanMgr.LanBase,nil,0)
	local text_siginupfee = com:GetChild("TextApplicationFee").asTextField
	if(match_info.SignupFee == 0)
	then
		text_siginupfee.text = "[size=20]免费[size]"
	else
		text_siginupfee.text = tostring(match_info.SignupFee) .. "+" tostring(match_info.ServiceFee)
	end
	o.GBtnSignup = com:GetChild("BtnSignIn").asButton
	o.GBtnSignup.onClick:Clear()
	o.GBtnSignup.onClick:Add(
		function()
			o:onClickBtnSign()
		end
	)
	local btn_self = com:GetChild("BtnSelf").asButton
	btn_self.onClick:Clear()
	btn_self.onClick:Add(
		function()
			o:onClickBtnSelf()
		end
	)
	o.GControllerBtnSigninState = o.GBtnSignup:GetController("ControllerState")
	o.GControllerBtnSigninState.onChanged:Add(
		function()
			o:onGControllerBtnSigninStateChange()
		end
	)
	o.GTextBtnSignTitle = o.GBtnSignup:GetChild("TextTitle").asTextField
	o.GControllerMatchState = com:GetController("ControllerMatchState")
	o.GComMatchStartTime1 = com:GetChild("ComMatchStartTime1").asCom
	o.GComMatchStartTime2 = com:GetChild("ComMatchStartTime2").asCom
	o.GTextDay = o.GComMatchStartTime1:GetChild("TextDay").asTextField
	o.GTextTime = o.GComMatchStartTime1:GetChild("TextTime").asTextField
	o.GTextMatchTips = o.GComMatchStartTime2:GetChild("TextTips").asTextField
	o.GTextTimeM = o.GComMatchStartTime2:GetChild("TextTimeM").asTextField
	o.GTextTimeS = o.GComMatchStartTime2:GetChild("TextTimeS").asTextField
	o.GTextPlayerNum = com:GetChild("TextPlayerNumber").asTextField
	o.GTextPlayerNum.text = tostring(match_info.PlayerNum)
	o.DtSignup = match_info.DtSignup
	o.DtMatchBegin = match_info.DtMatchBegin
	o.DtSignupClose = match_info.DtSignupClose
	o.IsSelfJoin = self_join
	o.ViewMgr = view_mgr

	return o
end

function ItemMatchInfo:UpdatePlayerNum(num)
	self.GTextPlayerNum.text = tostring(num)
end

function ItemMatchInfo:UpdateState(nowtm)
	if(nowtm < self.DtSignup) --暂未开放 无法报名
	then
		self.GControllerBtnSigninState.selectedIndex = 3
		self.GControllerMatchState.selectedIndex = 0
		local day = self.DtSignup.Day - nowtm.Day
		if(day == 1)
		then
			self.GTextDay.text = "明天"
		elseif(day == 2)
		then
			self.GTextDay.text = "后天"
		elseif(day > 2)
		then
			self.GTextDay.text = string.format("%02s",self.DtSignup.Month) .. "月" ..tostring(self.DtSignup.Day) .. "日"
		end
		self.GTextTime.text = string.format("%02s",self.DtMatchBegin.Hour) .. ":" .. string.format("%02s",self.DtMatchBegin.Minute)
		self.GTextBtnSignTitle.text = "[size=20][color=#4C87D9]暂未开放[color][size]"
	elseif(nowtm >= self.DtSignup and nowtm < self.DtMatchBegin)--已经开放 尚未开始
	then
		self.GTextBtnSignTitle.text = "报名"
		local time = self.DtMatchBegin - nowtm
		local day = self.DtMatchBegin.Day - nowtm.Day
		if(self.IsSelfJoin)
		then
			if(time.Minutes < 1)
			then
				self.GControllerBtnSigninState.selectedIndex = 1
				self.GTextBtnSignTitle.text = "进入"
			else
				self.GControllerBtnSigninState.selectedIndex = 3
				self.GTextBtnSignTitle.text = "[size=20][color=#4C87D9]等待开始[color][size]"
			end
		else
			self.GControllerBtnSigninState.selectedIndex = 0
		end
		if(time.Minutes > 9)
		then
			self.GControllerMatchState.selectedIndex = 0
			if(day == 0)
			then
				self.GTextDay.text = "今天"
			elseif(day == 1)
			then
				self.GTextDay.text = "明天"
			elseif(day == 2)
			then
				self.GTextDay.text = "后天"
			elseif(day > 2)
			then
				self.GTextDay.text = string.format("%02s",self.DtMatchBegin.Month) .. "月" ..tostring(self.DtMatchBegin.Day) .. "日"
			end
			self.GTextTime.text = string.format("%02s",self.DtMatchBegin.Hour) .. ":" .. string.format("%02s",self.DtMatchBegin.Minute)
		else
			self.GControllerMatchState.selectedIndex = 1
			self.GTextMatchTips.text = "即将开始"
			self.GTextTimeM.text = string.format("%02s",time.Minutes)
			self.GTextTimeS.text = string.format("%02s",time.Seconds)
		end
	elseif(nowtm >= self.DtMatchBegin and nowtm < self.DtSignupClose)--已经开始 尚未截至报名
	then
		if(self.IsSelfJoin)
		then
			self.GControllerMatchState.selectedIndex = 2 --正在进行
			self.GControllerBtnSigninState.selectedIndex = 1
			self.GTextBtnSignTitle.text = "进入"
		else
			self.GControllerMatchState.selectedIndex = 1
			self.GTextMatchTips = "报名截止"
			self.GControllerBtnSigninState.selectedIndex = 2
			self.GTextBtnSignTitle.text = "延迟报名"
			local left_time = self.DtSignupClose - nowtm
			self.GTextTimeM.text = string.format("%02s",left_time.Minutes)
			self.GTextTimeS.text = string.format("%02s",left_time.Seconds)
		end
	elseif(nowtm >= self.DtSignupClose)
	then
		if(self.IsSelfJoin)
		then
			self.GControllerMatchState.selectedIndex = 2--正在进行
			self.GControllerBtnSigninState.selectedIndex = 1
			self.GTextBtnSignTitle.text = "进入"
		else
			local match_lobby = self.ViewMgr:getView("MatchLobby")
			match_lobby:RemoveMatchItem(self)
		end
	end
end

function ItemMatchInfo:onClickBtnSign()
	if(self.GControllerBtnSigninState.selectedIndex == 0)
	then
		local msg_box = self.ViewMgr:createView("MsgBox")
		local content = {}
		content[1] = "您确认报名比赛？\n[size=17](消耗"
		content[2] = UiChipShowHelper:getGoldShowStr3(self.MatchInfo.SignupFee)
		content[3] = "+"
		content[4] = UiChipShowHelper:getGoldShowStr3(self.MatchInfo.ServiceFee)
		content[5] = "德州币)[/size]"
		msg_box:useTwoBtn("",table.concat(content),
			function()
				local ev = self.ViewMgr:getEv("EvUiRequestSignUpMatch")
				if(ev == nil)
				then
					ev = EvUiRequestSignUpMatch:new(nil)
				end
				ev.MatchGuid = self.MatchInfo.Guid
				self.ViewMgr:sendEv(ev)
				self.ViewMgr:destroyView(msg_box)
			end,
			function()
				self.ViewMgr:destroyView(msg_box)
			end
		)
	elseif(self.GControllerBtnSigninState.selectedIndex == 1)
	then
		local ev = self.ViewMgr:getEv("EvUiRequestEnterMatch")
		if(ev == nil)
		then
			ev = EvUiRequestEnterMatch:new(nil)
		end
		ev.MatchGuid = self.MatchInfo.Guid
		self.ViewMgr:sendEv(ev)
	elseif(self.GControllerBtnSigninState.selectedIndex == 2)
	then
		local ev = self.ViewMgr:getEv("EvUiRequestSignUpMatch")
		if(ev == nil)
		then
			ev = EvUiRequestSignUpMatch:new(nil)
		end
		ev.MatchGuid = self.MatchInfo.Guid
		self.ViewMgr:sendEv(ev)
	end
end

function ItemMatchInfo:onClickBtnSelf()
	local view_matchInfo = self.ViewMgr:createView("MatchInfo")
	view_matchInfo:Init(self.MatchInfo.Guid,false,self.IsSelfJoin)
end

function ItemMatchInfo:onGControllerBtnSigninStateChange()
	if(self.GControllerBtnSigninState.selectedIndex == 3)
	then
		self.GBtnSignup.enabled = false
	else
		self.GBtnSignup.enabled = true
	end
end