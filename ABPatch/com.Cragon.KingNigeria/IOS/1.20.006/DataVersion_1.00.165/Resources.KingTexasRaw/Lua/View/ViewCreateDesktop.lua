ViewCreateDesktop = ViewBase:new()

function ViewCreateDesktop:new(o)
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

function ViewCreateDesktop:onCreate()
	ViewHelper:PopUi(self.ComUi,"正在创建私人牌桌")
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	local com = self.ComUi:GetChild("ComBgAndClose").asCom
    local com_shade = com:GetChild("ComShade").asCom
    com_shade.onClick:Add(
		function()
			self:onClickClose()
		end
	)
    local btn_create = self.ComUi:GetChild("BtnCreate").asButton
    btn_create.onClick:Add(
		function()
			self:onClickBtnCreate()
		end
	)
    self.mTextBet = self.ComUi:GetChild("TextBet").asTextField
    self.mTextStake = self.ComUi:GetChild("TextStake").asTextField
    self.mTextSpeed = self.ComUi:GetChild("TextSpeed").asTextField
end

function ViewCreateDesktop:setCreateInfo(desk_topinfo_id,map_params)
	self.mDeskTopId = desk_topinfo_id
    self.mSeatNum = map_params[0]
    self.mIsNormalSpeed = map_params[1]
	local tb_deskinfo = self.CasinosContext.TbDataMgrLua:GetData("DesktopInfoTexas",desk_topinfo_id)
	local tips = self.ViewMgr.LanMgr:getLanValue("��ע", "Bet")
	local	bet_str = self.CasinosContext:AppendStrWithSB(tips, ": ", UiChipShowHelper:getGoldShowStr(tb_deskinfo.SmallBlind, self.ViewMgr.LanMgr.LanBase)
	, "/", UiChipShowHelper:getGoldShowStr(tb_deskinfo.BigBlind, self.ViewMgr.LanMgr.LanBase))
    self.mTextBet.text = bet_str
    local seat_numstr = ""
	if(self.mSeatNum == TexasDesktopSeatNum.Unlimited)
	then
		seat_numstr = self.ViewMgr.LanMgr:getLanValue("����", "Unlimited")
	elseif(self.mSeatNum == TexasDesktopSeatNum.Nine)
	then
		seat_numstr = "9"
	elseif(self.mSeatNum == TexasDesktopSeatNum.Five)
	then
		seat_numstr = "5"
	end
    self.mTextStake.text = self.CasinosContext:AppendStrWithSB(self.ViewMgr.LanMgr:getLanValue("��λ", "Seat"), ": ", seat_numstr)
end
	
function ViewCreateDesktop:onClickClose()
	self.ViewMgr:destroyView(self)
end

function ViewCreateDesktop:onClickBtnCreate()
	ViewHelper:UiBeginWaiting()
    local create_infotexas = PrivateDesktopCreateInfoTexas:new(nil)
    create_infotexas.desktop_tableid = self.mDeskTopId
    create_infotexas.seat_num = self.mSeatNum
    create_infotexas.speed =self.mIsNormalSpeed
	create_infotexas.is_vip = false
    local create_info = PrivateDesktopCreateInfo:new(nil)
    create_info.FactoryName = "Texas"
    create_info.Data =  self.ViewMgr:packData(create_infotexas:getData4Pack()) -- CS.Casinos.LuaHelper.ProtobufSerializePrivateDesktopCreateInfoTexas(create_infotexas)
    local ev = self.ViewMgr:getEv("EvUiClickCreateDeskTop")
	if(ev == nil)
	then
		ev = EvUiClickCreateDeskTop:new(nil)
	end
	ev.create_info = create_info
	self.ViewMgr:sendEv(ev)
    self.ViewMgr:destroyView(self)
end
			


ViewCreateDesktopFactory = ViewFactory:new()

function ViewCreateDesktopFactory:new(o,ui_package_name,ui_component_name,
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

function ViewCreateDesktopFactory:createView()	
	local view = ViewCreateDesktop:new(nil)	
	return view
end