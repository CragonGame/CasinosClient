-- Copyright(c) Cragon. All rights reserved.
-- 已废弃

ItemMainOperate = {}

function ItemMainOperate:new(o,com_operate)
	o = o or {}
    setmetatable(o,self)
    self.__index = self
	self.GButton = com_operate
    self.GLoaderBg = com_operate:GetChild("LoaderBg").asLoader
    self.GLoaderIcon = com_operate:GetChild("LoaderIcon").asLoader
    self.GTextTitle = com_operate:GetChild("title").asTextField
	return o
end

function ItemMainOperate:setCurrentOperateInfo(main_operate)
	local icon_bg = ""
    local icon_name = main_operate:ToString()
    local main_operate_name = ""
	if(main_operate == CS.Casinos._eMainOperate.MustBet)
	then
		icon_bg = "BtnBgRed"
        icon_name = "Championship"
        main_operate_name = "必下模式"
        if (UseLan) then
			main_operate_name = CS.Casinos.CasinosContext.Instance.LanMgr:getLanValue("MustBetModel")
		end
        self.GButton.onClick:Add(
			function()
				self:onClickMustBet()
			end
		)
	elseif(main_operate == CS.Casinos._eMainOperate.Login) then
		icon_bg = "BtnBgPurple"
        main_operate_name = "登录"
        if (UseLan) then
			main_operate_name = CS.Casinos.CasinosContext.Instance.LanMgr:getLanValue("Login")
		end
        if (CS.Casinos.CasinosContext.Instance.CoApp.IsVisiter == false) then
			main_operate_name = "注销"
            if (UseLan) then
				main_operate_name = CS.Casinos.CasinosContext.Instance.LanMgr:getLanValue("Logout");
			end
		end
        self.GButton.onClick:Add(
			function()
				self:onClickLogin()
			end
		)
	elseif(main_operate == CS.Casinos._eMainOperate.Ranking) then
		icon_bg = "BtnBgRed"
        main_operate_name = "排行"
        if (UseLan) then
			main_operate_name = CS.Casinos.CasinosContext.Instance.LanMgr:getLanValue("Ranking")
		end
        self.GButton.onClick:Add(
			function()
				self:onClickRanking()
			end
		)
	elseif(main_operate == CS.Casinos._eMainOperate.Invite) then
		icon_bg = "BtnBgOrange"
        main_operate_name = "邀请"
        if (UseLan) then
			main_operate_name = CS.Casinos.CasinosContext.Instance.LanMgr.getLanValue("Invitation")
		end
        self.GButton.onClick:Add(
			function()
				self:onClickInvite()
			end
		)
	elseif(main_operate == CS.Casinos._eMainOperate.Shop) then
		icon_bg = "BtnBgPurple"
        main_operate_name = "商店"
        if (UseLan) then
			main_operate_name = CS.Casinos.CasinosContext.Instance.LanMgr:getLanValue("Shop")
		end
        self.GButton.onClick:Add(
			function()
				self:onClickShop()
			end
		)
	elseif(main_operate == CS.Casinos._eMainOperate.Vip) then
		icon_bg = "BtnBgRed"
        main_operate_name = "贵宾"
        if (UseLan) then
			main_operate_name = CS.Casinos.CasinosContext.Instance.LanMgr:getLanValue("Vip")
		end
        self.GButton.onClick:Add(
			function()
				self:onClickVip()
			end
		)
	end
    self.GLoaderIcon.url = CS.FairyGUI.UIPackage.GetItemURL("Main", icon_name)
    self.GLoaderBg.url = CS.FairyGUI.UIPackage.GetItemURL("Main", icon_bg)
    self.GTextTitle.text = main_operate_name
end

function ItemMainOperate:onClickMustBet()
	local view_mgr = ViewMgr:new(nil)
	local view_lobby = view_mgr.createView("Lobby")
    view_lobby:setLobbyModel()
end

function ItemMainOperate:onClickLogin()
	local view_mgr = ViewMgr:new(nil)
	local ev = view_mgr:getEv("EvUiClickLogin")
	if(ev == nil)
	then
		ev = EvUiClickLogin:new(nil)
	end
	view_mgr:sendEv(ev)
end

function ItemMainOperate:onClickRanking()
	local view_mgr = ViewMgr:new(nil) 
	local ev = view_mgr:getEv("EvUiClickHelp")
	if(ev == nil)
	then
		ev = EvUiClickHelp:new(nil)
	end
	view_mgr:sendEv(ev)
    ControllerRanking:createRankingUi()
end

function ItemMainOperate:onClickInvite()
	local view_mgr = ViewMgr:new(nil) 
	local ev = view_mgr:getEv("EvUiClickInviteFriend")
	if(ev == nil)
	then
		ev = EvUiClickInviteFriend:new(nil)
	end
	view_mgr:sendEv(ev)
end

function ItemMainOperate:onClickShop()
	local view_mgr = ViewMgr:new(nil) 
	local ev = view_mgr:getEv("EvUiClickShop")
	if(ev == nil)
	then
		ev = EvUiClickShop:new(nil)
	end
	view_mgr:sendEv(ev)
end

function ItemMainOperate:onClickVip()
	local view_mgr = ViewMgr:new(nil) 
	local ev = view_mgr:getEv("EvUiClickVip")
	if(ev == nil)
	then
		ev = EvUiClickVip:new(nil)
	end
	view_mgr:sendEv(ev)
end