MatchTexasProcessType = {
    Fifty = 0, -- 50%
    Thirty = 1, -- 30%
    InTheReward = 2, -- 奖励圈
    FinalTable = 3, -- 决赛桌
    OneVSOne = 4, -- 终极对决
}

MatchTexasIconType = {
    Normal = 0,
-- 红包赛
-- 狂欢赛
-- 深筹赛
-- iPhoneX赛
}

MatchTexasScopeType = {
    Public = 0, -- 公开的赛事
    Club = 1, -- 俱乐部创建的赛事
    Player = 2, -- 玩家私人创建的赛事
}

MatchTexasRaiseBlindType = {
    Blinds1 = 0,
    BlindsFast = 1,
    BlindsNormal = 2,
    BlindsSlow = 3,
}

--DesktopNotifyMTTPlayerRebuyOrAddon = {} -- 比赛桌，玩家重购或增购广播信息
--function DesktopNotifyMTTPlayerRebuyOrAddon:new(o)
--    o = o or {}
--    setmetatable(o, self)
--    self.__index = self
--    o.PlayerGuid = nil --玩家Guid
--    o.RebuyNum = 0 --已重购次数，已用掉次数，非剩余次数
--    o.AddonNum = 0 --已增购次数，已用掉次数，非剩余次数
--    o.CurrentScore = 0 --购买后当前记分牌
--
--    return o
--end
--
--function DesktopNotifyMTTPlayerRebuyOrAddon:setData(data)
--    self.PlayerGuid = data[1]
--    self.RebuyNum = tonumbler(data[2])
--    self.AddonNum = tonumbler(data[3])
--    self.CurrentScore = data[4]
--end
--
--DesktopNotifyMTTUpdateProcess = {} -- 比赛桌，进度推送广播信息
--function DesktopNotifyMTTUpdateProcess:new(o)
--    o = o or {}
--    setmetatable(o, self)
--    self.__index = self
--    o.ProcessType = 0
--
--    return o
--end
--
--function DesktopNotifyMTTUpdateProcess:setData(data)
--    self.ProcessType = data[1]
--end

BMatchTexasPlayerFinishedNotify = {} -- 比赛桌，本人比赛结束信息
function BMatchTexasPlayerFinishedNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Result = 0
    o.MatchGuid = nil
    o.MatchName = nil
    o.DesktopGuid = nil
    o.Ranking = -1-- 本人排名，小于0表示没有产生排名
    o.RewardGold = -1  -- 本人获得的金币奖励，小于等于0则不提示
    o.RewardDiamond = -1 -- 本人获得的钻石奖励，小于等于0则不提示
    o.ListRewardItemId = nil -- 本人获得的道具奖励，可以为null

    return o
end

function BMatchTexasPlayerFinishedNotify:setData(data)
    self.Result = data[1]
    self.MatchGuid = data[2]
    self.MatchName = data[3]
    self.DesktopGuid = data[4]
    self.Ranking = data[5]
    self.RewardGold = data[6]
    self.RewardDiamond = data[7]
    self.ListRewardItemId = data[8]
end

--获取赛事列表响应
BMatchTexasGetListResponse = {}
function BMatchTexasGetListResponse:new(o)
	o = o or {}
	setmetatable(o, self)
	self.__index = self
	o.ListMatchTexasInfo = nil
	o.ListMyMatch = nil

	return o
end

function BMatchTexasGetListResponse:setData(data)
	self.ListMatchTexasInfo = {}
	if(data[1] ~= nil and #data[1] > 0)
	then
		for i = 1 ,#data[1] do
			local temp_matchInfo = BMatchTexasInfo:new(nil)
			temp_matchInfo:setData(data[1][i])
			data[1][i] = temp_matchInfo
		end
	end

	self.ListMatchTexasInfo = data[1]
	self.ListMyMatch = data[2]
end

BMatchTexasPlayerInfo = {}
function BMatchTexasPlayerInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.PlayerGuid = "" --玩家Guid
    o.Score = 0 --记分牌
    o.RebuyCount = 0 --已重购次数
    o.AddonCount = 0 --已增购信息次数

    return o
end

function BMatchTexasPlayerInfo:setData(data)
    self.PlayerGuid = data[1]
    self.Score = data[2]
    self.RebuyCount = data[3]
    self.AddonCount = data[4]
end

BMatchTexasPlayerRankingInfo = {} -- 赛事中排名表中的玩家信息
function BMatchTexasPlayerRankingInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.PlayerGuid = "" --玩家Guid
    o.NickName = "" -- 昵称
    o.AccId = "" -- 用于玩家头像显示
	o.Icon = "" --用于玩家头像显示
    o.Ranking = 0 --名次
    o.Score = 0 --记分牌

    return o
end

function BMatchTexasPlayerRankingInfo:setData(data)
    self.PlayerGuid = data[1]
    self.NickName = data[2]
    self.AccId = data[3]
	self.Icon = data[4]
    self.Ranking = data[5]
    self.Score = data[6]
end

BMatchTexasPlayerNumUpdate = {}
function BMatchTexasPlayerNumUpdate:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Guid = nil --赛事Guid
    o.PlayerNum = 0 --参赛人数
    return o
end

function BMatchTexasPlayerNumUpdate:setData(data)
    self.Guid = data[1]
    self.PlayerNum = data[2]
end

BMatchTexasInfo = {}--赛事信息
function BMatchTexasInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Guid = nil --赛事Guid
    o.UniqId = 0 -- 唯一Id
    o.TemplateId = 0 -- 模板Id
    o.IconType = nil --赛事图标类型
    o.ScopeType = nil --赛事开放范围
    o.Name = nil --赛事名字
    o.Discribe = nil --赛事描述
    o.DtMatchOpen = nil --比赛开放时间
    o.DtSignup = nil --报名时间
    o.DtMatchBegin = nil --比赛开始时间
    o.DtSignupClose = nil --报名截止时间
    o.PlayerNum = 0 --当前参赛人数
    o.InitScore = 0 --初始记分牌
    o.SignupCostOneOrTwo = 0 -- 报名消耗道具和报名费的关系，0表示或（二选一），1表示与（同时需要）
    o.SignupItemId = 0 -- 报名消耗ItemId
    o.SignupFee = 0 --报名费
    o.ServiceFee = 0 --服务费
    o.CanRebuyCount = 0 --可重购次数
    o.CanAddonCount = 0 --可增购次数
    o.IsSnowballReward = nil --是否是滚雪球奖励
    o.TotalRewardGold = 0 --总金币奖励
    o.SeatNum = nil --座位数
    o.RaiseBlindTbInfo = nil --升盲表静态信息

    return o
end

function BMatchTexasInfo:setData(data)
    self.Guid = data[1]
    self.UniqId = data[2]
    self.TemplateId = data[3]
    self.IconType = data[4]
    self.ScopeType = data[5]
    self.Name = data[6]
    self.Discribe = data[7]
    self.DtMatchOpen = CS.System.DateTime.Parse(data[8]):ToLocalTime()
    self.DtSignup = CS.System.DateTime.Parse(data[9]):ToLocalTime()
    self.DtMatchBegin = CS.System.DateTime.Parse(data[10]):ToLocalTime()
    self.DtSignupClose = CS.System.DateTime.Parse(data[11]):ToLocalTime()
    self.PlayerNum = data[12]
	self.InitScore = data[13]
self.SignupCostOneOrTwo = data[14]
    self.SignupItemId = data[15]
    self.SignupFee = data[16]
    self.ServiceFee = data[17]
    self.CanRebuyCount = data[18]
    self.CanAddonCount = data[19]
    self.IsSnowballReward = data[20]
    self.TotalRewardGold = data[21]
    self.SeatNum = data[22]
	if(data[23] ~= nil)
	then
		local raiseBlindInfo = BMatchTexasRaiseBlindTbInfo:new(nil)
		raiseBlindInfo:setData(data[23])
		self.RaiseBlindTbInfo = raiseBlindInfo
	end
end

BMatchTexasMoreInfo = {}--赛事详细信息，点击赛事列表Item后，弹出的赛事详细信息对话框
function BMatchTexasMoreInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.Info = nil --赛事信息
    o.PlayerNumMin = 0 --参赛人数，最少
    o.PlayerNumMax = 0  --参赛人数，最多
    o.InvitationCode = nil  --邀请码
    o.CreatePlayerGuid = nil  --赛事创建者Guid
    o.CreatePlayerNickName = nil  --赛事创建者昵称
    o.CreatePlayerAccId = nil  --赛事创建者AccId，用于显示头像
    o.RealtimeMoreInfo = nil  --实时详细信息
	o.Reward = nil  --奖励信息
    return o
end

function BMatchTexasMoreInfo:setData(data)
    if data[1] ~= nil 
	then
        local match_info = BMatchTexasInfo:new(nil)
        match_info:setData(data[1])
		self.Info = match_info
    end
	self.PlayerNumMin = data[2] 
    self.PlayerNumMax = data[3]  
    self.InvitationCode = data[4] 
    self.CreatePlayerGuid = data[5]  
    self.CreatePlayerNickName = data[6]
    self.CreatePlayerAccId = data[7]
	if(data[8] ~= nil)
	then
		local realtimeMoreInfo = BMatchTexasRealtimeMoreInfo:new(nil)
		realtimeMoreInfo:setData(data[8])
		self.RealtimeMoreInfo = realtimeMoreInfo
	end
	local reward = BMatchTexasReward:new(nil)
	reward:setData(data[9])
	self.Reward = reward
end

-- 比赛奖励信息       
BMatchTexasReward = {}

function BMatchTexasReward:new(o)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.SnowballTotalReward = nil
	o.SnowballTotalRewardCurrent = nil
	o.ListReward = {}
	return o
end

function BMatchTexasReward:setData(data)
	self.SnowballTotalReward = data[1]
	self.SnowballTotalRewardCurrent = data[2]
	if(#data[3] > 0)
	then
		for i = 1,#data[3] do
			local temp = BMatchTexasRewardItem:new(nil)
			temp:setData(data[3][i])
			if(temp.RankingBegin ~= 0)
			then
				table.insert(self.ListReward,temp)
			end
		end
	end
end

BMatchTexasRewardItem = {}
function BMatchTexasRewardItem:new(o)	
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.RankingBegin = 0 --从1开始，<=0都是无效排名，丢弃
	o.RankingEnd = 0 --End>=Begin
	o.Gold = 0 --单人奖励值。如果RankingBegin=1，RankingEnd=10，Value=1w，则该项总奖励为10w
	o.RedEnvelopes = 0 --红包，单位：分
	o.ItemId = 0 --奖励道具Id
	return o
end

function BMatchTexasRewardItem:setData(data)
	self.RankingBegin = data[1]
	self.RankingEnd = data[2]
	self.Gold = data[3]
	self.RedEnvelopes = data[4]
	self.ItemId = data[5]
end


BMatchTexasRealtimeInfo = {}--升盲表实时信息
function BMatchTexasRealtimeInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.MyRanking = 0 -- 本人排名
    o.PlayerLeftNum = 0 -- 当前剩余参赛人数
    o.AverageScore = 0 --均筹（人均记分牌）
    o.PlayerNumReward = 0 -- 奖励圈人数
    o.TotalRewardGold = 0 -- 总奖励金币
    o.CurrentRaiseBlindTbId = 0 -- 当前升盲表TbId
    o.RaiseBlindLeftSecond = 0 -- 升盲倒计时（秒数）
    o.RaiseBlindIdNext = 0 -- 接下来一句使用的升盲表Id，RaiseBlindId！=RaiseBlindIdNext则下局升盲

    --o.CurrentRaiseBlindTbId = 0 --升盲表当前TbId

    --o.CurrentRaiseBlindSb = 0 -- 升盲表当前Sb
    --o.CurrentRaiseBlindBb = 0 --升盲表当前BB
    --o.CurrentRaiseBlindAnte = 0 --升盲表当前Ante
    --o.NextRaiseBlindTbId = 0 --升盲表下一个TbId
    --o.NextRaiseBlindSb = 0 --升盲表下一个Sb
    --o.NextRaiseBlindBb = 0 -- 升盲表下一个BB
    --o.NextRaiseBlindAnte = 0 -- 升盲表下一个Ante

    return o
end

function BMatchTexasRealtimeInfo:setData(data)
    self.MyRanking = data[1]
    self.PlayerLeftNum = data[2]
    self.AverageScore = data[3]
    self.PlayerNumReward = data[4]
    self.TotalRewardGold = data[5]
    self.CurrentRaiseBlindTbId = data[6]
    self.RaiseBlindLeftSecond = data[7]
    self.RaiseBlindIdNext= data[8]

    --self.CurrentRaiseBlindTbId = data[7]
    --self.CurrentRaiseBlindSb = data[8]
    --self.CurrentRaiseBlindBb = data[9]
    --self.CurrentRaiseBlindAnte = data[10]
    --self.NextRaiseBlindTbId = data[11]
    --self.NextRaiseBlindSb = data[12]
    --self.NextRaiseBlindBb = data[13]
    --self.NextRaiseBlindAnte = data[14]
end

BMatchTexasRaiseBlindTbInfo = {}--升盲表静态信息
function BMatchTexasRaiseBlindTbInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    --o.RaiseBlindTbName = nil -- 升盲表名
    o.BlindType = 0
    o.BeginId = 0 -- 起始Id，默认为升盲表第一条的Id 如果小于第一条 就等于第一条
    o.EndId = 0 --结束Id，默认为升盲表最后一条的Id  如果大于最后一条 就等于最后一条
    o.ListRaiseBlindTbIdCanRebuy = nil -- 可以重购的盲注级别列表，如不可重购，则该值为null
    o.ListRaiseBlindTbIdCanAddon = nil -- 可以增购的盲注级别列表，如不可增购，则该值为null
    o.RebuyGold = 0 -- 重购消耗的金币
    o.RebuyScore = 0 -- 重购购买的记分牌
    o.AddonGold = 0 -- 增购消耗的金币
    o.AddonScore = 0 -- 增购购买的记分牌
    o.RaiseBlindTmSpan = 0 --涨盲时间，单位：秒
    return o
end

function BMatchTexasRaiseBlindTbInfo:setData(data)
    self.BlindType = data[1]
	if(data[2] < 1)
	then
		data[2] = 1
	end
	self.BeginId = data[2]
	local raise_blindTable = TbDataHelper:GetAllTexasRaiseBlindsByType(self.BlindType)
	if(data[3] > #raise_blindTable)
	then
		data[3] = #raise_blindTable
	end
    self.EndId = data[3]
    self.ListRaiseBlindTbIdCanRebuy = data[4]
    self.ListRaiseBlindTbIdCanAddon = data[5]
    self.RebuyGold = data[6]
    self.RebuyScore = data[7]
    self.AddonGold = data[8]
    self.AddonScore = data[9]
	self.RaiseBlindTmSpan = data[10]
end

BMatchTexasRealtimeMoreInfo = {}--赛事实时详细信息
function BMatchTexasRealtimeMoreInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Info = nil -- 右上角实时简要信息
    o.ListPlayerRanking = nil -- 玩家排名表

    return o
end

function BMatchTexasRealtimeMoreInfo:setData(data)
    local info = data[1]
    local real_info = nil
    if info ~= nil then
        real_info = BMatchTexasRealtimeInfo:new(nil)
        real_info:setData(info)
    end
    self.Info = real_info

    local ranking = data[2]
    local list_ranking = {}
    if ranking ~= nil then
        for i, v in pairs(ranking) do
            local ranking_info = BMatchTexasPlayerRankingInfo:new(nil)
            ranking_info:setData(v)
            table.insert(list_ranking, ranking_info)
        end
    end
    self.ListPlayerRanking = list_ranking
end

BDesktopSnapshotMatchTexasMyInfo = {}
function BDesktopSnapshotMatchTexasMyInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.RebuyNum = 0 --已重购次数，用掉的次数，非剩余次数
    o.AddonNum = 0 --已增购次数，用掉的次数，非剩余次数
    o.GameOverCountDownLeftSec = -1 -- 输光可重购时，比赛结束剩余倒计时时间,小于0则不处于该状态
    o.ServerAutoAction = false -- 是否处于服务器托管操作状态

    return o
end

function BDesktopSnapshotMatchTexasMyInfo:setData(data)
    self.RebuyNum = data[1]
    self.AddonNum = data[2]
    self.GameOverCountDownLeftSec = data[3]
    self.ServerAutoAction = data[4]
end

BDesktopSnapshotMatchTexas = {}--比赛桌相对普通桌快照中的额外信息
function BDesktopSnapshotMatchTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.RealtimeInfo = nil -- 右上角实时简要信息
    o.RaiseBlindTbInfo = nil -- 升盲表静态信息
    o.MatchGuid = nil
    o.MatchName = nil -- 赛事名称
    o.IsSnowballReward = false --是否是滚雪球奖励
    o.Pause = false -- 是否处于暂停态
    o.PauseCountDownLeftSec = -1 -- 暂停态剩余倒计时秒数，小于0则不处于该状态
    o.MyInfo = nil --本人信息

    --o.RebuyNum = 0 --已重购次数，用掉的次数，非剩余次数
    --o.AddonNum = 0 --已增购次数，用掉的次数，非剩余次数
    --o.CanRebuyCount = 0 --可重购次数
    --o.CanAddonCount = 0 --可增购次数
    --o.ServerAutoAction = false -- 是否处于服务器托管操作状态
    --o.GameOverCountDownLeftSec = -1 -- 输光可重购时，比赛结束剩余倒计时时间,小于0则不处于该状态
    --o.Wait4GetDesktopCountDownLeftSec = -1 -- 等待分桌剩余倒计时秒数，小于0则不处于该状态

    return o
end

function BDesktopSnapshotMatchTexas:setData(data)
    local real_info = data[1]
    local realtime_info = nil
    if real_info ~= nil then
        realtime_info = BMatchTexasRealtimeInfo:new(nil)
        realtime_info:setData(real_info)
    end
    self.RealtimeInfo = realtime_info
    local blind_tbinfo = data[2]
    local raise_blindtbinfo = nil
    if blind_tbinfo ~= nil then
        raise_blindtbinfo = BMatchTexasRaiseBlindTbInfo:new(nil)
        raise_blindtbinfo:setData(blind_tbinfo)
    end
    self.RaiseBlindTbInfo = raise_blindtbinfo
    self.MatchGuid = data[3]
    self.MatchName = data[4]
    self.IsSnowballReward = data[5]
    self.Pause = data[6]
    self.PauseCountDownLeftSec = data[7]
    local my_info = data[8]
    local m_info = nil
    if my_info ~= nil then
        m_info = BDesktopSnapshotMatchTexasMyInfo:new(nil)
        m_info:setData(my_info)
    end
    self.MyInfo = m_info
end

BMatchTexasCreateInfo = {}--赛事创建信息

function BMatchTexasCreateInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.IconType = nil --赛事图标类型
    o.ScopeType = nil --赛事开放范围
    o.Name = nil --赛事名字
    o.Discribe = nil --赛事描述
    o.DtMatchOpen = nil --比赛开放时间
    o.DtSignup = nil --报名时间
    o.DtMatchBegin = nil --比赛开始时间
    o.DtSignupClose = nil --报名截止时间
    o.PlayerNumMin = nil --参赛人数，最少
    o.PlayerNumMax = nil --参赛人数，最多
    o.SignupFee = nil --报名费
    o.ServiceFee = nil --服务费
    o.CanRebuyCount = nil --可重购次数
    o.CanAddonCount = nil --可增购次数
    o.InitScore = nil --初始记分牌
    o.SeatNum = nil --座位数
    o.CreatePlayerGuid = nil --创建赛事玩家Guid，公共赛事该Guid为空
    o.RaiseBlindTbInfo = nil --升盲表静态信息

    return o
end

function BMatchTexasCreateInfo:getData4Pack()
    local p_d = {}
    table.insert(p_d, self.IconType)
    table.insert(p_d, self.ScopeType)
    table.insert(p_d, self.Name)
    table.insert(p_d, self.Discribe)
    table.insert(p_d, self.DtMatchOpen)
    table.insert(p_d, self.DtSignup)
    table.insert(p_d, self.DtMatchBegin)
    table.insert(p_d, self.DtSignupClose)
    table.insert(p_d, self.PlayerNumMin)
    table.insert(p_d, self.PlayerNumMax)
    table.insert(p_d, self.SignupFee)
    table.insert(p_d, self.ServiceFee)
    table.insert(p_d, self.CanRebuyCount)
    table.insert(p_d, self.CanAddonCount)
    table.insert(p_d, self.InitScore)
    table.insert(p_d, self.SeatNum)
    table.insert(p_d, self.CreatePlayerGuid)
    table.insert(p_d, self.RaiseBlindTbInfo:getData4Pack())
    return p_d
end

-- 赛事开始通知
BMatchTexasStartNotify = {}

function BMatchTexasStartNotify:new(o)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.MatchGuid = nil
    o.MatchName = nil
    o.DtMatchBegin = nil --比赛开始时间

	return o
end

function BMatchTexasStartNotify:setData(data)
	self.MatchGuid = data[1]
    self.MatchName = data[2]
    self.DtMatchBegin = CS.System.DateTime.Parse(data[3]):ToLocalTime()
end

-- 赛事解散通知
BMatchTexasPlayerGameEndNotify = {}

function BMatchTexasPlayerGameEndNotify:new(o)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.Result = nil
	o.MatchGuid = nil
    o.MatchName = nil
    o.MatchTemplateId = 0
    o.SignupFee = 0
    o.ServiceFee = 0
    o.ItemId = 0
	return o
end

function BMatchTexasPlayerGameEndNotify:setData(data)
	self.Result = data[1]
	self.MatchGuid = data[2]
    self.MatchName = data[3]
    self.MatchTemplateId = data[4]
    self.SignupFee = data[5]
    self.ServiceFee = data[6]
    self.ItemId = data[7]
end

--报名结果响应
BMatchTexasSignUpResponse = {}

function BMatchTexasSignUpResponse:new(o)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.Result = nil
    o.MatchGuid = nil
    o.ItemTbId = nil
    o.SignupFee = 0
    o.ServiceFee = 0

	return o
end

function BMatchTexasSignUpResponse:setData(data)
	self.Result = data[1]
    self.MatchGuid = data[2]
    self.ItemTbId = data[3]
    self.SignupFee = data[4]
    self.ServiceFee = data[5]
end

--取消报名结果响应
BMatchTexasCancelSignUpResponse = {}

function BMatchTexasCancelSignUpResponse:new(o)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Result = nil
    o.MatchGuid = nil
    o.ItemTbId = nil
    o.SignupFee = 0
    o.ServiceFee = 0
    o.MatchName = nil

	return o
end

function BMatchTexasCancelSignUpResponse:setData(data)
	self.Result = data[1]
    self.MatchGuid = data[2]
    self.ItemTbId = data[3]
    self.SignupFee = data[4]
    self.ServiceFee = data[5]
    self.MatchName = data[6]
end

--进入比赛结果响应
BMatchTexasEnterResponse = {}
function BMatchTexasEnterResponse:new(o)
	o = o or {}
    setmetatable(o, self)
    self.__index = self
	o.Result = nil
    o.MatchGuid = nil

	return  o
end

function BMatchTexasEnterResponse:setData(data)
	self.Result = data[1]
    self.MatchGuid = data[2]
end

--重购结果响应
BMatchTexasRebuyResponse = {}
function BMatchTexasRebuyResponse:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Result = nil
    o.MatchGuid = nil
    --o.RebuyNum = 0 -- 已重购次数，已用掉次数，非剩余次数
    --o.CurrentScore = 0 -- 购买后当前记分牌

    return  o
end

function BMatchTexasRebuyResponse:setData(data)
    self.Result = data[1]
    self.MatchGuid = data[2]
    --self.RebuyNum = data[3]
    --self.CurrentScore = data[4]
end

--增购结果响应
BMatchTexasAddonResponse = {}
function BMatchTexasAddonResponse:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Result = nil
    o.MatchGuid = nil
    --o.AddonNum = 0 -- 已增购次数，已用掉次数，非剩余次数
    --o.CurrentScore = 0 -- 购买后当前记分牌

    return  o
end

function BMatchTexasAddonResponse:setData(data)
    self.Result = data[1]
    self.MatchGuid = data[2]
    --self.AddonNum = data[3]
    --self.CurrentScore = data[4]
end

-- 比赛桌，暂停或开始通知
BMatchTexasDesktopStartOrPauseNotify = {}
function BMatchTexasDesktopStartOrPauseNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Pause = true
    o.PauseCountdownLeftSec = 0 -- 暂停剩余秒数

    return  o
end

function BMatchTexasDesktopStartOrPauseNotify:setData(data)
    self.Pause = data[1]
    self.PauseCountdownLeftSec = data[2]
end