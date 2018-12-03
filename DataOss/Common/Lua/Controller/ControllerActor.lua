-- Copyright(c) Cragon. All rights reserved.
-- 无View

---------------------------------------
Prop = {}

function Prop:new(v)
    o = {}
    setmetatable(o, self)
    self.__index = self
    o.Value = v
    o.OnChanged = nil
    return o
end

function Prop:get()
    return self.Value
end

function Prop:set(value)
    self.Value = value
    if (self.OnChanged ~= nil) then
        self.OnChanged()
    end
end

---------------------------------------
ControllerActor = class(ControllerBase)

---------------------------------------
function ControllerActor:ctor(this, controller_data, controller_name)
    local BotTbId = self.ControllerData["BotTbId"]
    self.PropBotTbId = Prop:new(tonumber(BotTbId))
    local AccountId = self.ControllerData["AccountId"]
    self.PropAccountId = Prop:new(AccountId)
    local ActorId = self.ControllerData["ActorId"]
    self.PropActorId = Prop:new(tonumber(ActorId))
    local Gender = self.ControllerData["Gender"]
    local gender = false
    if (string.lower(Gender) == "true") then
        gender = true
    end
    self.PropGender = Prop:new(gender)
    local NickName = self.ControllerData["NickName"]
    self.PropNickName = Prop:new(NickName)
    local Icon = self.ControllerData["Icon"]
    self.PropIcon = Prop:new(Icon)
    local GoldAcc = self.ControllerData["GoldAcc"]
    self.PropGoldAcc = Prop:new(tonumber(GoldAcc))
    local GoldBank = self.ControllerData["GoldBank"]
    self.PropGoldBank = Prop:new(tonumber(GoldBank))
    local Diamond = self.ControllerData["Diamond"]
    self.PropDiamond = Prop:new(tonumber(Diamond))
    local Level = self.ControllerData["Level"]
    self.PropLevel = Prop:new(tonumber(Level))
    local Experience = self.ControllerData["Experience"]
    self.PropExperience = Prop:new(tonumber(Experience))
    local LotteryDrawPoint = self.ControllerData["LotteryDrawPoint"]
    self.PropLotteryDrawPoint = Prop:new(tonumber(LotteryDrawPoint))
    local RechargePoint = self.ControllerData["RechargePoint"]
    self.PropRechargePoint = Prop:new(tonumber(RechargePoint))
    local IsFirstRecharge = self.ControllerData["IsFirstRecharge"]
    local is_first = false
    if (string.lower(IsFirstRecharge) == "true") then
        is_first = true
    end
    self.PropIsFirstRecharge = Prop:new(is_first)
    local IpAddress = self.ControllerData["IpAddress"]
    self.PropIpAddress = Prop:new(IpAddress)
    local IndividualSignature = self.ControllerData["IndividualSignature"]
    self.PropIndividualSignature = Prop:new(IndividualSignature)
    local ProfileSkinTbId = self.ControllerData["ProfileSkinTbId"]
    self.PropProfileSkinTbId = Prop:new(tonumber(ProfileSkinTbId))
    local JoinDataTime = self.ControllerData["JoinDataTime"]
    local datetime = CS.System.DateTime.Parse(JoinDataTime)
    self.PropJoinDateTime = Prop:new(datetime)
    local LoginDateTime = self.ControllerData["LoginDateTime"]
    self.PropLoginDateTime = Prop:new(LoginDateTime)
    local LogoutDateTime = self.ControllerData["LogoutDateTime"]
    self.PropLogoutDateTime = Prop:new(LogoutDateTime)
    local VIPLevel = self.ControllerData["VIPLevel"]
    self.PropVIPLevel = Prop:new(tonumber(VIPLevel))
    local VIPDataTime = self.ControllerData["VIPDataTime"]
    self.PropVIPDataTime = Prop:new(VIPDataTime)
    local ReliefCount = self.ControllerData["ReliefCount"]
    self.PropReliefCount = Prop:new(tonumber(ReliefCount))
    local ReliefDateTime = self.ControllerData["ReliefDateTime"]
    self.PropReliefDateTime = Prop:new(ReliefDateTime)
    --local EnableGrow = self.ControllerData["EnableGrow"]
    --local enable_grow = false
    --if(string.lower(EnableGrow) == "true") then
    --	enable_grow = true
    --end
    self.PropEnableGrow = Prop:new(false)
    local Point = self.ControllerData["Point"]
    self.PropPoint = Prop:new(tonumber(Point))
    local MasterPoint = self.ControllerData["MasterPoint"]
    self.MasterPoint = Prop:new(tonumber(MasterPoint))
    self.LastLevel = self.PropLevel

    self.ControllerPlayer = self.ControllerMgr:GetController("ControllerPlayer")
    Native:CreateShareUrlAndQRCode(ActorId)
end

---------------------------------------
function ControllerActor:OnCreate()
    self.Rpc = self.ControllerMgr.Rpc
    self.MC = CommonMethodType
    self.CasinosContext = CS.Casinos.CasinosContext.Instance
    self.ControllerLogin = self.ControllerMgr:GetController("Login")
    local attach_wechat = self.ControllerLogin.ClientEnterWorldNotify.attach_wechat
    local wechat_openid = nil
    local wechat_name = nil
    if attach_wechat ~= nil then
        wechat_openid = attach_wechat.open_id
        wechat_name = attach_wechat.nick_name
        --print("wechat_openid   " .. wechat_openid)
    else
        --print("attach_wechat  is null")
    end
    self.WeChatOpenId = Prop:new(wechat_openid)
    self.WeChatName = Prop:new(wechat_name)
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
    self.Rpc:RegRpcMethod3(self.MC.PlayerPointUpdateNotify, function(change_reason, point, user_date)
        self:s2cPlayerPointUpdateNotify(change_reason, point, user_date)
    end)
    -- 玩家筹码变更通知
    self.Rpc:RegRpcMethod3(self.MC.PlayerGoldAccUpdateNotify, function(change_reason, gold_acc, user_data)
        self:s2cPlayerGoldAccUpdateNotify(change_reason, gold_acc, user_data)
    end)
    -- 玩家钻石改变通知
    self.Rpc:RegRpcMethod3(self.MC.PlayerDiamondUpdateNotify, function(change_reason, diamond, user_data)
        self:s2cPlayerDiamondUpdateNotify(change_reason, diamond, user_data)
    end)
    -- 玩家VIP改变通知
    self.Rpc:RegRpcMethod1(self.MC.PlayerVipChangedNotify, function(vip_level)
        self:s2cPlayerVipChangedNotify(vip_level)
    end)
    -- 充值点数变更通知
    self.Rpc:RegRpcMethod1(self.MC.PlayerRechargePointChangedNotify, function(rechargepoint_change)
        self:s2cPlayerRechargePointChangedNotify(rechargepoint_change)
    end)
    -- 首充变更通知
    self.Rpc:RegRpcMethod0(self.MC.PlayerIsFirstRechargeChangedNotify, function()
        self:s2cPlayerIsFirstRechargeChangedNotify()
    end)
    -- 输光后送的筹码通知
    self.Rpc:RegRpcMethod1(self.MC.PlayerLostAllSendChipsNotify, function(send_goldsinfo)
        self:s2cPlayerLostAllSendChipsNotify(send_goldsinfo)
    end)
    -- 玩家大师分更新通知
    --[[self.Rpc:RegRpcMethod1(self.MC.PlayerMasterPointUpdateNotify,function(level_new)
        self:s2cPlayerLevelupNotify(level_new)
    end)]]
    self.ViewMgr:BindEvListener("EvBindWeChatSuccess", self)
    self.ViewMgr:BindEvListener("EvUnBindWeChatSuccess", self)
end

---------------------------------------
function ControllerActor:OnDestroy()
    self.ViewMgr:UnbindEvListener(self)
end

---------------------------------------
function ControllerActor:OnHandleEv(ev)
    if (ev.EventName == "EvBindWeChatSuccess") then
        if ev.IsSuccess then
            self.WeChatOpenId:set(ev.WeChatOpenId)
            self.WeChatName:set(ev.WeChatName)
        end
    elseif (ev.EventName == "EvUnBindWeChatSuccess") then
        if ev.IsSuccess then
            self.WeChatOpenId:set(nil)
            self.WeChatName:set(nil)
        end
    end
end

---------------------------------------
function ControllerActor:s2cPlayerGoldAccUpdateNotify(change_reason, gold_acc, user_data)
    if (change_reason == GoldAccChangeReason.AddByGoldPackage) then
        ViewHelper:UiShowInfoSuccess(self.ViewMgr.LanMgr:getLanValue("ScoreForGoldPackage"))
    end
    local gold = self.PropGoldAcc:get()
    local delta_gold = gold_acc - gold
    self.PropGoldAcc:set(gold_acc)
    local ev = self.ViewMgr:GetEv("EvEntityGoldChanged")
    if (ev == nil) then
        ev = EvEntityGoldChanged:new(nil)
    end
    ev.change_reason = change_reason
    ev.gold_acc = self.PropGoldAcc:get()
    ev.delta_gold = delta_gold
    ev.user_data = user_data
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerActor:s2cPlayerDiamondUpdateNotify(change_reason, diamond, user_data)
    print("s2cPlayerDiamondUpdateNotify" .. tostring(diamond))
    self.PropDiamond:set(diamond)
end

---------------------------------------
function ControllerActor:s2cPlayerVipChangedNotify(vip_level)
    self.PropVIPLevel:set(vip_level)
end

---------------------------------------
function ControllerActor:getNextVIPInfo(next_viplevel, next_needtotalexp, next_allexp)
    local level_cur = self.PropVIPLevel:get()
    local level_next = level_cur + 1
    local exp_cur = self.PropRechargePoint:get()
    local tb_actorlevel_next = self.CasinosContext.TbDataMgrLua:GetData("VIPLevel", level_next)
    if (tb_actorlevel_next == nil)
    then
        next_viplevel = -1
        next_needtotalexp = 0
        next_allexp = 0
        return next_viplevel, next_needtotalexp, next_allexp
    end

    local exp_total = tb_actorlevel_next.VIPPoint - exp_cur
    next_viplevel = level_next
    next_needtotalexp = exp_total
    next_allexp = tb_actorlevel_next.VIPPoint
    return next_viplevel, next_needtotalexp, next_allexp
end

---------------------------------------
function ControllerActor:s2cPlayerLostAllSendChipsNotify(send_goldsinfo)
    local data = LostAllSendGoldsInfo:new(nil)
    data:setData(send_goldsinfo)
    --local tips = string.format(self.ViewMgr.LanMgr:getLanValue("LostAllGold"), tostring(data.send_golds), self.ViewMgr.LanMgr:getLanValue("Chip"))
    self.PropGoldAcc:set(data.total_golds)
end

---------------------------------------
function ControllerActor:s2cPlayerRechargePointChangedNotify(rechargepoint_change)
    self.PropRechargePoint:set(rechargepoint_change)
end

---------------------------------------
function ControllerActor:s2cPlayerIsFirstRechargeChangedNotify()
    self.PropIsFirstRecharge:set(false)
end

---------------------------------------
function ControllerActor:onPropExperienceChanged()
end

---------------------------------------
function ControllerActor:s2cPlayerPointUpdateNotify(change_reson, point, user_date)
    self.PropPoint:set(point)
end

---------------------------------------
function ControllerActor:onPropLevelChanged()
    if (self.LastLevel ~= self.PropLevel:get()) then
        self.LastLevel = self.PropLevel:get()
        local msg = string.format(self.ViewMgr.LanMgr:getLanValue("LevelUpTips"), self.PropLevel:get())
        ViewHelper:UiShowInfoSuccess(msg)
    end
end

---------------------------------------
function ControllerActor:onPropGoldAccChanged()
end

---------------------------------------
function ControllerActor:onPropPointChanged()
    local ev = self.ViewMgr:GetEv("EvEntityPointChanged")
    if (ev == nil) then
        ev = EvEntityPointChanged:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerActor:onPropGoldBankChanged()
    local ev = self.ViewMgr:GetEv("EvEntityBankGoldChange")
    if (ev == nil) then
        ev = EvEntityBankGoldChange:new(nil)
    end
    ev.bank_gold = self.PropGoldBank:get()
    ev.gold_acc = self.PropGoldAcc:get()
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerActor:onPropDiamondChanged()
    local ev = self.ViewMgr:GetEv("EvEntityDiamondChanged")
    if (ev == nil) then
        ev = EvEntityDiamondChanged:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerActor:onPropNickNameChanged()
    ViewHelper:UiEndWaiting()
    local ev = self.ViewMgr:GetEv("EvEntityPlayerInfoChanged")
    if (ev == nil) then
        ev = EvEntityPlayerInfoChanged:new(nil)
    end
    ev.controller_actor = self
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerActor:onPropIndividualSignatureChanged()
    ViewHelper:UiEndWaiting()
    local ev = self.ViewMgr:GetEv("EvEntityPlayerInfoChanged")
    if (ev == nil) then
        ev = EvEntityPlayerInfoChanged:new(nil)
    end
    ev.controller_actor = self
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerActor:onPropIpAddressChanged()
    local ev = self.ViewMgr:GetEv("EvEntityPlayerInfoChanged")
    if (ev == nil) then
        ev = EvEntityPlayerInfoChanged:new(nil)
    end
    ev.controller_actor = self
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerActor:onPropVipLevelChanged()
    local ev = self.ViewMgr:GetEv("EvEntityPlayerInfoChanged")
    if (ev == nil) then
        ev = EvEntityPlayerInfoChanged:new(nil)
    end
    ev.controller_actor = self
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
function ControllerActor:onPropIsFirstRecharge()
    local ev = self.ViewMgr:GetEv("EvEntityIsFirstRechargeChanged")
    if (ev == nil) then
        ev = EvEntityIsFirstRechargeChanged:new(nil)
    end
    self.ViewMgr:SendEv(ev)
end

---------------------------------------
ControllerActorFactory = class(ControllerFactory)

function ControllerActorFactory:GetName()
    return 'Actor'
end

---------------------------------------
function ControllerActorFactory:CreateController(controller_data)
    local ctrl_name = self:GetName()
    local ctrl = ControllerActor:new(controller_data, ctrl_name)
    return ctrl
end