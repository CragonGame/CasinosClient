ControllerActor = ControllerBase:new(nil)

function ControllerActor:new(o,controller_mgr,controller_data,guid)
	o = o or {}
	setmetatable(o,self)
	self.__index = self

	o.ControllerData = controller_data
	o.ControllerMgr = controller_mgr
	o.Guid = guid
	o.ViewMgr = ViewMgr:new(nil)
	local BotTbId = o.ControllerData["BotTbId"]
	o.PropBotTbId = Prop:new(tonumber(BotTbId))
	local AccountId = o.ControllerData["AccountId"]
	o.PropAccountId = Prop:new(AccountId)
	CS.Casinos.CasinosContext.Instance.AccountId = AccountId
	local ActorId = o.ControllerData["ActorId"]
	o.PropActorId = Prop:new(tonumber(ActorId))
	local Gender = o.ControllerData["Gender"]
	local gender = false
	if(string.lower(Gender) == "true")
	then
		gender = true
	end
	o.PropGender = Prop:new(gender)
	local NickName = o.ControllerData["NickName"]
	o.PropNickName = Prop:new(NickName)
	local Icon = o.ControllerData["Icon"]
	o.PropIcon = Prop:new(Icon)
	local GoldAcc = o.ControllerData["GoldAcc"]
	o.PropGoldAcc = Prop:new(tonumber(GoldAcc))
	local GoldBank = o.ControllerData["GoldBank"]
	o.PropGoldBank = Prop:new(tonumber(GoldBank))
	local Diamond = o.ControllerData["Diamond"]
	o.PropDiamond = Prop:new(tonumber(Diamond))
	local Level = o.ControllerData["Level"]
	o.PropLevel = Prop:new(tonumber(Level))
	local Experience = o.ControllerData["Experience"]
	o.PropExperience = Prop:new(tonumber(Experience))
	local LotteryDrawPoint = o.ControllerData["LotteryDrawPoint"]
	o.PropLotteryDrawPoint = Prop:new(tonumber(LotteryDrawPoint))
	local RechargePoint = o.ControllerData["RechargePoint"]
	o.PropRechargePoint = Prop:new(tonumber(RechargePoint))
	local IsFirstRecharge = o.ControllerData["IsFirstRecharge"]
	local is_first = false
	if(string.lower(IsFirstRecharge) == "true")
	then
		is_first = true
	end
	o.PropIsFirstRecharge = Prop:new(is_first)
	local IpAddress = o.ControllerData["IpAddress"]
	o.PropIpAddress = Prop:new(IpAddress)
	local IndividualSignature = o.ControllerData["IndividualSignature"]
	o.PropIndividualSignature = Prop:new(IndividualSignature)
	local ProfileSkinTbId = o.ControllerData["ProfileSkinTbId"]
	o.PropProfileSkinTbId = Prop:new(tonumber(ProfileSkinTbId))
	local JoinDataTime = o.ControllerData["JoinDataTime"]
	local datetime = CS.System.DateTime.Parse(JoinDataTime)
	o.PropJoinDateTime = Prop:new(datetime)
	local LoginDateTime = o.ControllerData["LoginDateTime"]
	o.PropLoginDateTime = Prop:new(LoginDateTime)
	local LogoutDateTime = o.ControllerData["LogoutDateTime"]
	o.PropLogoutDateTime = Prop:new(LogoutDateTime)
	local VIPLevel = o.ControllerData["VIPLevel"]
	o.PropVIPLevel = Prop:new(tonumber(VIPLevel))
	local VIPDataTime = o.ControllerData["VIPDataTime"]
	o.PropVIPDataTime = Prop:new(VIPDataTime)
	local ReliefCount = o.ControllerData["ReliefCount"]
	o.PropReliefCount = Prop:new(tonumber(ReliefCount))
	local ReliefDateTime = o.ControllerData["ReliefDateTime"]
	o.PropReliefDateTime = Prop:new(ReliefDateTime)
	--local EnableGrow = self.ControllerData["EnableGrow"]
	local enable_grow = false
	--if(string.lower(EnableGrow) == "true")
	--then
	--	enable_grow = true
	--end
	o.PropEnableGrow = Prop:new(enable_grow)
	local Point = o.ControllerData["Point"]
	o.PropPoint = Prop:new(tonumber(Point))
	local MasterPoint = o.ControllerData["MasterPoint"]
	o.MasterPoint = Prop:new(tonumber(MasterPoint))
	local wechat_openid = o.ControllerData["WeChatOpenId"]
	o.WeChatOpenId = Prop:new(wechat_openid)
	local wechat_name = o.ControllerData["WeChatName"]
	o.WeChatName = Prop:new(wechat_name)
	o.ControllerMgr = controller_mgr
	o.ControllerPlayer = o.ControllerMgr:GetController("ControllerPlayer")
	-- self.EffectMgr = CS.Casinos.EffectMgr()
	o.LastLevel = o.PropLevel
	Native.Instance:CreateShareUrlAndQRCode(ActorId)

	return o
end

function ControllerActor:onCreate()
	self.RPC = self.ControllerMgr.RPC
	self.MC = CommonMethodType
	self.CasinosContext = CS.Casinos.CasinosContext.Instance
	self.ControllerLogin = self.ViewMgr.ControllerMgr:GetController("Login")
	self.ControllerLogin.ControllerActor = self
	self.PropExperience.OnChanged = function()
		self:onPropExperienceChanged()
	end
	self.PropLevel.OnChanged = function()
		self:onPropLevelChanged()
	end
	self.PropGoldAcc.OnChanged = function()
		self:onPropGoldAccChanged()
	end
	self.PropPoint.OnChanged = function()
		self:onPropPointChanged()
	end
	self.PropGoldBank.OnChanged = function()
		self:onPropGoldBankChanged()
	end
	self.PropDiamond.OnChanged = function()
		self:onPropDiamondChanged()
	end
	self.PropIpAddress.OnChanged = function()
		self:onPropIpAddressChanged()
	end
	self.PropNickName.OnChanged = function()
		self:onPropNickNameChanged()
	end
	self.PropIndividualSignature.OnChanged = function()
		self:onPropIndividualSignatureChanged()
	end
	self.PropVIPLevel.OnChanged = function()
		self:onPropVipLevelChanged()
	end
	self.PropIsFirstRecharge.OnChanged = function()
		self:onPropIsFirstRecharge()
	end
	-- 玩家积分更新通知
	self.RPC:RegRpcMethod3(self.MC.PlayerPointUpdateNotify,function(change_reason,point,user_date)
		self:s2cPlayerPointUpdateNotify(change_reason,point,user_date)
	end)
	-- 玩家筹码变更通知
	self.RPC:RegRpcMethod3(self.MC.PlayerGoldAccUpdateNotify,function(change_reason,gold_acc,user_data)
		self:s2cPlayerGoldAccUpdateNotify(change_reason,gold_acc,user_data)
	end)
	-- 玩家钻石改变通知
	self.RPC:RegRpcMethod3(self.MC.PlayerDiamondUpdateNotify,function(change_reason,diamond,user_data)
		self:s2cPlayerDiamondUpdateNotify(change_reason,diamond,user_data)
	end)
	-- 玩家VIP改变通知
	self.RPC:RegRpcMethod1(self.MC.PlayerVipChangedNotify,function(vip_level)
		self:s2cPlayerVipChangedNotify(vip_level)
	end)
	-- 充值点数变更通知
	self.RPC:RegRpcMethod1(self.MC.PlayerRechargePointChangedNotify,function(rechargepoint_change)
		self:s2cPlayerRechargePointChangedNotify(rechargepoint_change)
	end)
	-- 首充变更通知
	self.RPC:RegRpcMethod0(self.MC.PlayerIsFirstRechargeChangedNotify,function()
		self:s2cPlayerIsFirstRechargeChangedNotify()
	end)
	-- 输光后送的筹码通知
	self.RPC:RegRpcMethod1(self.MC.PlayerLostAllSendChipsNotify,function(send_goldsinfo)
		self:s2cPlayerLostAllSendChipsNotify(send_goldsinfo)
	end)
	-- 玩家大师分更新通知
	--[[self.RPC:RegRpcMethod1(self.MC.PlayerMasterPointUpdateNotify,function(level_new)
		self:s2cPlayerLevelupNotify(level_new)
	end)]]
	self.ViewMgr:bindEvListener("EvBindWeChatSuccess",self)
	self.ViewMgr:bindEvListener("EvUnBindWeChatSuccess",self)
end

function ControllerActor:onDestroy()
	self.ViewMgr:unbindEvListener(self)
	self.ControllerLogin.ControllerActor = nil
end

function ControllerActor:onUpdate(tm)
end

function ControllerActor:onHandleEv(ev)
	if(ev.EventName == "EvBindWeChatSuccess")
	then
		if ev.IsSuccess then
			self.WeChatOpenId:set(ev.WeChatOpenId)
			self.WeChatName:set(ev.WeChatName)
		end
		elseif(ev.EventName == "EvUnBindWeChatSuccess")
	then
		if ev.IsSuccess then
			self.WeChatOpenId:set(nil)
			self.WeChatName:set(nil)
		end
	end
end

function ControllerActor:s2cPlayerGoldAccUpdateNotify(change_reason,gold_acc,user_data)
	if(change_reason == GoldAccChangeReason.AddByGoldPackage) 
	then
		ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("ScoreForGoldPackage"))
	end
	local gold = self.PropGoldAcc:get()
	local delta_gold = gold_acc - gold
	self.PropGoldAcc:set(gold_acc)
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityGoldChanged")
	if(ev == nil)
	then
		ev = EvEntityGoldChanged:new(nil)
	end
	ev.change_reason = change_reason
	ev.gold_acc = self.PropGoldAcc:get()
	ev.delta_gold = delta_gold
	ev.user_data = user_data
	self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerActor:s2cPlayerDiamondUpdateNotify(change_reason,diamond,user_data)
	print("s2cPlayerDiamondUpdateNotify" ..tostring(diamond))
	self.PropDiamond:set(diamond)
end

function ControllerActor:s2cPlayerVipChangedNotify(vip_level)
	self.PropVIPLevel:set(vip_level)
end

function ControllerActor:getNextVIPInfo(next_viplevel,next_needtotalexp,next_allexp)
	local level_cur = self.PropVIPLevel:get()
	local level_next = level_cur + 1
	local exp_cur = self.PropRechargePoint:get()
	local tb_actorlevel_next = self.CasinosContext.TbDataMgrLua:GetData("VIPLevel",level_next)
	if (tb_actorlevel_next == nil)
	then
		next_viplevel = -1
		next_needtotalexp = 0
		next_allexp = 0
		return next_viplevel,next_needtotalexp,next_allexp
	end

	local exp_total = tb_actorlevel_next.VIPPoint - exp_cur
	next_viplevel = level_next
	next_needtotalexp = exp_total
	next_allexp = tb_actorlevel_next.VIPPoint
	return next_viplevel,next_needtotalexp,next_allexp
end

function ControllerActor:s2cPlayerLostAllSendChipsNotify(send_goldsinfo)
	local data = LostAllSendGoldsInfo:new(nil)
	data:setData(send_goldsinfo)
	local tips = string.format(self.ViewMgr.LanMgr:getLanValue("LostAllGold"),tostring(data.send_golds),
			self.ViewMgr.LanMgr:getLanValue("Chip"))
	self.PropGoldAcc:set(data.total_golds)
end

function ControllerActor:s2cPlayerRechargePointChangedNotify(rechargepoint_change)
	self.PropRechargePoint:set(rechargepoint_change)
end

function ControllerActor:s2cPlayerIsFirstRechargeChangedNotify()
	self.PropIsFirstRecharge:set(false)
end

function ControllerActor:onPropExperienceChanged()

end

function ControllerActor:s2cPlayerPointUpdateNotify(change_reson,point,user_date)
	self.PropPoint:set(point)
end

function ControllerActor:onPropLevelChanged()
	if (self.LastLevel ~= self.PropLevel:get())
	then
		self.LastLevel = self.PropLevel:get()
		local msg = string.format(self.ViewMgr.LanMgr:getLanValue("LevelUpTips"), self.PropLevel:get())
		ViewHelper:UiShowInfoSuccess(msg)
	end
end

function ControllerActor:onPropGoldAccChanged()
end

function ControllerActor:onPropPointChanged()
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityPointChanged") 
	if(ev == nil)
	then
		ev = EvEntityPointChanged:new(nil)
	end
	self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerActor:onPropGoldBankChanged()
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityBankGoldChange")
	if(ev == nil)
	then
		ev = EvEntityBankGoldChange:new(nil)
	end
	ev.bank_gold = self.PropGoldBank:get()
	ev.gold_acc = self.PropGoldAcc:get()
	self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerActor:onPropDiamondChanged()
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityDiamondChanged")
	if(ev == nil)
	then
		ev = EvEntityDiamondChanged:new(nil)
	end
	self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerActor:onPropNickNameChanged()
	ViewHelper:UiEndWaiting()
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityPlayerInfoChanged")
	if(ev == nil)
	then
		ev = EvEntityPlayerInfoChanged:new(nil)
	end
	ev.controller_actor = self
	self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerActor:onPropIndividualSignatureChanged()
	ViewHelper:UiEndWaiting()
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityPlayerInfoChanged")
	if(ev == nil)
	then
		ev = EvEntityPlayerInfoChanged:new(nil)
	end
	ev.controller_actor = self
	self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerActor:onPropIpAddressChanged()
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityPlayerInfoChanged")
	if(ev == nil)
	then
		ev = EvEntityPlayerInfoChanged:new(nil)
	end
	ev.controller_actor = self
	self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerActor:onPropVipLevelChanged()
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityPlayerInfoChanged")
	if(ev == nil)
	then
		ev = EvEntityPlayerInfoChanged:new(nil)
	end
	ev.controller_actor = self
	self.ControllerMgr.ViewMgr:sendEv(ev)
end

function ControllerActor:onPropIsFirstRecharge()
	local ev = self.ControllerMgr.ViewMgr:getEv("EvEntityIsFirstRechargeChanged")
	if(ev == nil)
	then
		ev = EvEntityIsFirstRechargeChanged:new(nil)
	end
	self.ControllerMgr.ViewMgr:sendEv(ev)
end

ControllerActorFactory = ControllerFactory:new()

function ControllerActorFactory:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	self.ControllerName = "Actor"
	return o
end

function ControllerActorFactory:createController(controller_mgr,controller_data,guid)
	local controller = ControllerActor:new(nil,controller_mgr,controller_data,guid)
	return controller
end