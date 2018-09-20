TimingRewardType = {
    None = 0, --不在可领取时间段
    Midday = 1, --上午
    Evening = 2, --晚上
}

--在线奖励状态
OnlineRewardState = {
    CountDown = 0, --倒计时中
    Wait4GetReward = 1, --等待领取奖励中
}

--玩家在线状态，供客户端使用
PlayerOnlineState = {
    Offline = 0, -- 离线
    Online = 1, -- 在线
}

--举报类型
ReportPlayerType = {
    AbuseBehavior = 0, --辱骂他人
    GoldTransaction = 1, --非法交易
    PornIcon = 2, --色情头像
    Cheat = 3, --使用外挂
}

TimingRewardData = {}
function TimingRewardData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.Type = TimingRewardType.None
    o.Get = false
    o.RewardGold = 0

    return o
end

function TimingRewardData:setData(data)
    self.Type = data[1]
    self.Get = data[2]
    self.RewardGold = data[3]
end

-- 输光赠送Gold
LostAllSendGoldsInfo = {}
function LostAllSendGoldsInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.send_golds = 0
    o.total_golds = 0
    return o
end

function LostAllSendGoldsInfo:setData(data)
    self.send_golds = data[1]
    self.total_golds = data[2]
end

-- 举报玩家
ReportPlayer = {}
function ReportPlayer:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.report_type = nil
    o.player_guid = nil

    return o
end

function ReportPlayer:getData4Pack()
    local p_d = {}
    table.insert(p_d, self.report_type)
    table.insert(p_d, self.player_guid)

    return p_d
end

--银行操作通知
BankNotify = {}
function BankNotify:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.result = nil
    o.bank_golds = 0
    o.acc_golds = 0
    return o
end

function BankNotify:setData(data)
    self.result = data[1]
    self.bank_golds = data[2]
    self.acc_golds = data[3]
end
