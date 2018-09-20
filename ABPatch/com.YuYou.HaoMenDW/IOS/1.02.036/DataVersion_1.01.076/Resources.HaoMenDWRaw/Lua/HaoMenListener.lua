HaoMenListener = {
	Instance = nil,
	MainC = nil,
	ControllerMgr = nil,
	ViewMgr = nil,
	EventSys = nil,
}

function HaoMenListener:new(o)
	 o = o or {}  
    setmetatable(o,self)  
    self.__index = self	
    if(self.Instance==nil)
	then
		self.Instance = o		
	end
    return self.Instance
end

function HaoMenListener:onCreate()
	print("HaoMenListener:onCreate")
	self:_regLuaFilePath()

	self.MainC = MainC:new(nil)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("EventSys")	
	self.EventSys = EventSys:new(nil)
	self.EventSys:onCreate()
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ViewMgr")
	self.ViewMgr = ViewMgr:new(nil)	
	self.ViewMgr:onCreate("Resources.HaoMenDW/Ui/", "Resources.HaoMenDWRaw/")
	self:_regView()	

	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("SessionMgr")
	SessionMgr:new(nil)

	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerMgr")
	self.ControllerMgr = ControllerMgr:new(nil)
	self.ControllerMgr:OnCreate()
	self:_regController()
	self:_addUiPackage()
end

function HaoMenListener:onDestroy()
	if(self.ViewMgr ~= nil)
	then
		self.ViewMgr:onDestroy()
	end

	if(self.ControllerMgr ~= nil)
	then
		self.ControllerMgr:OnDestroy()
	end
end

function HaoMenListener:onUpdate(tm)		
	if(self.ViewMgr ~= nil)
	then
		self.ViewMgr:onUpdate(tm)
	end

	if(self.ControllerMgr ~= nil)
	then
		self.ControllerMgr:OnUpdate(tm)
	end
end

function HaoMenListener:CreateController(table_data)
	
end

function HaoMenListener:_regLuaFilePath()
	local casinos_context = CS.Casinos.CasinosContext.Instance	
	casinos_context:regLuaFilePath("Controller/", "ControllerMgr","ControllerBase","ControllerFactory",
		"ControllerActivity","ControllerActor","ControllerBag","ControllerDesk","ControllerDeskH","ControllerGrow",
		"ControllerIM","ControllerLobby","ControllerLotteryTicket","ControllerMarquee","ControllerPlayer",
		"ControllerRanking","ControllerTrade","ControllerLogin")
	casinos_context:regLuaFilePath("Model/", "ModelBase","ModelReaderBase")
	casinos_context:regLuaFilePath("View/", "ViewMgr","ViewFactory","ViewBase","ViewLoading","ViewMsgBox","UiSecurityCode")
	casinos_context:regLuaFilePath("Event/", "EventSys","EventBase","EventView")
	casinos_context:regLuaFilePath("Helper/", "CommonLuaLoader","LuaHelper","ProjectDataLoader","WWWLoader")
	casinos_context:regLuaFilePath("Resources.HaoMenDWRaw/Lua/View/", "ViewLogin","ViewRegister")
	-- casinos_context:regLuaFilePath("Resources.HaoMenDW/Lua/Event/", "HaoMenEventController","HaoMenEventView")
end

function HaoMenListener:_regView()
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ViewLoading")
	local view_loading_fac = ViewLoadingFactory:new(nil,"Loading","Loading","Loading",true,CS.FairyGUI.FitScreen.FitSize)
	self.ViewMgr:regView("Loading",view_loading_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ViewMsgBox")
	local view_msgbox_fac = ViewMsgBoxFactory:new(nil,"Common","MsgBox","Waiting",true,CS.FairyGUI.FitScreen.FitSize)
	self.ViewMgr:regView("MsgBox",view_msgbox_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ViewLogin")
	local view_login_fac = ViewLoginFactory:new(nil,"Login","Login","Background",true,CS.FairyGUI.FitScreen.FitSize)
	self.ViewMgr:regView("Login",view_login_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ViewRegister")
	local view_register_fac = ViewRegisterFactory:new(nil,"Register","Register","MessgeBox",true,CS.FairyGUI.FitScreen.FitSize)
	self.ViewMgr:regView("Register",view_register_fac)
end

function HaoMenListener:_regController()
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerLogin")
	local con_login_fac = ControllerLoginFactory:new(nil)
	self.ControllerMgr:RegController("Login",con_login_fac)
	--[[CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerActivity")
	local con_activity_fac = ControllerActivityFactory:new(nil)
	self.ControllerMgr:RegController("Activity",con_activity_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerActor")
	local con_actor_fac = ControllerActorFactory:new(nil)
	self.ControllerMgr:RegController("Actor",con_actor_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerBag")
	local con_bag_fac = ControllerBagFactory:new(nil)
	self.ControllerMgr:RegController("Bag",con_bag_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerDesk")
	local con_desk_fac = ControllerDeskFactory:new(nil)
	self.ControllerMgr:RegController("Desk",con_desk_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerDeskH")
	local con_deskh_fac = ControllerDeskHFactory:new(nil)
	self.ControllerMgr:RegController("DeskH",con_deskh_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerGrow")
	local con_grow_fac = ControllerGrowFactory:new(nil)
	self.ControllerMgr:RegController("Grow",con_grow_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerIM")
	local con_im_fac = ControllerIMFactory:new(nil)
	self.ControllerMgr:RegController("IM",con_im_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerLobby")
	local con_lobby_fac = ControllerLobbyFactory:new(nil)
	self.ControllerMgr:RegController("Lobby",con_lobby_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerLotteryTicket")
	local con_lottery_fac = ControllerLotteryFactory:new(nil)
	self.ControllerMgr:RegController("Lottery",con_lottery_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerMarquee")
	local con_marquee_fac = ControllerMarqueeFactory:new(nil)
	self.ControllerMgr:RegController("Marquee",con_marquee_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerPlayer")
	local con_player_fac = ControllerPlayerFactory:new(nil)
	self.ControllerMgr:RegController("Player",con_player_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerRanking")
	local con_ranking_fac = ControllerRankingFactory:new(nil)
	self.ControllerMgr:RegController("Ranking",con_ranking_fac)
	CS.Casinos.CasinosContext.Instance.CasinosLua:doString("ControllerTrade")
	local con_trade_fac = ControllerTradeFactory:new(nil)
	self.ControllerMgr:RegController("Trade",con_trade_fac)--]]
end

function HaoMenListener:_addUiPackage()	
	local casinos_context = CS.Casinos.CasinosContext.Instance
	local s = self.ViewMgr:getUiPackagePath("ActivityCenter")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("Login")
    casinos_context.Listener:AddUiPackage(s)
	s = self.ViewMgr:getUiPackagePath("Common")
    casinos_context.Listener:AddUiPackage(s)	
	s = self.ViewMgr:getUiPackagePath("Loading")
    casinos_context.Listener:AddUiPackage(s)	
	s = self.ViewMgr:getUiPackagePath("Pool")
    casinos_context.Listener:AddUiPackage(s)	
    s = self.ViewMgr:getUiPackagePath("Main")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("MailDetail")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("Chat")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DailyReward")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopH")
    casinos_context.Listener:AddUiPackage(s)
	s = self.ViewMgr:getUiPackagePath("DesktopChatParent")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHBetReward")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHGFlower")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHNiuNiu")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHZhongFB")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHBankPlayerList")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHCardType")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHHistory")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHRewardPot")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHMenu")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHHelp")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHResult")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHSetCardType")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHTongSha")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("DesktopHTongPei")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("QuitOrBack")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("LotteryTicket")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("Notice")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("ShootingText")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("GoldTree")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("PlayerInfo")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("PlayerProfile")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("PlayerProfileSmall")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("RechargeFirst")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("Ranking")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("ResetPwd")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("Register")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("Task")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("TakePhoto")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("Set")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("Shop")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("Warning")
    casinos_context.Listener:AddUiPackage(s)
    s = self.ViewMgr:getUiPackagePath("SafeBox")
	casinos_context.Listener:AddUiPackage(s)
end