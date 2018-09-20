LotteryTicketStateEnum = {
    Close = 0, -- 关闭
    Bet = 1, -- 下注
    GameEnd = 2, -- 游戏结束
}

BLotteryTicketPlayerInfo = {}
function BLotteryTicketPlayerInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.PlayerGuid = nil
    o.PlayerId = nil
    o.Nickname = nil
    o.Icon = nil
    o.VIPLevel = 0
    o.Dt = nil
    o.WinGold = 0 -- 赢的总金币，包括本钱（待确认）

    return o
end

function BLotteryTicketPlayerInfo:setData(data)
    self.PlayerGuid = data[1]
    self.PlayerId = data[2]
    self.Nickname = data[3]
    self.Icon = data[4]
    self.VIPLevel = data[5]
    self.Dt = data[6]
    self.WinGold = data[7]
end

function BLotteryTicketPlayerInfo:getData4Pack()
    local temp = {}
    table.insert(temp, self.PlayerGuid)
    table.insert(temp, self.PlayerId)
    table.insert(temp, self.Nickname)
    table.insert(temp, self.Icon)
    table.insert(temp, self.VIPLevel)
    table.insert(temp, self.Dt)
    table.insert(temp, self.WinGold)

    return temp
end

BLotteryTicketGameEndDetail = {}
function BLotteryTicketGameEndDetail:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.LotteryTicketPlayGuid = nil -- 每局Guid
    o.ListCard = nil -- 3张牌
    o.WinCardType = 0 -- 3张牌的对应牌型  (实际是BetPotIndex)
    o.RewardPotGold = 0 -- 奖池中当前金币总额
    o.LastBaoZiDt = nil
    o.LastMaxWinner = nil -- 本轮最大赢家

    return o
end

function BLotteryTicketGameEndDetail:setData(data)
    self.LotteryTicketPlayGuid = data[1]
    local l_c = data[2]
    if l_c ~= nil then
        local t_c = {}
        for i, v in pairs(l_c) do
            local c_d = CardData:new(nil)
            c_d:setData(v)
            table.insert(t_c,c_d)
        end
        self.ListCard = t_c
    end
    self.WinCardType = data[3]
    self.RewardPotGold = data[4]
    self.LastBaoZiDt = data[5]
    local a = data[6]
    if a ~= nil then
        local a_t = BLotteryTicketPlayerInfo:new(nil)
        a_t:setData(a)
        self.LastMaxWinner = a_t
    end
end

function BLotteryTicketGameEndDetail:getData4Pack()
    local temp = {}
    table.insert(temp, self.LotteryTicketPlayGuid)
    table.insert(temp, self.ListCard)
    table.insert(temp, self.WinCardType)
    table.insert(temp, self.RewardPotGold)
    table.insert(temp, self.LastBaoZiDt)
    if self.LastMaxWinner ~= nil then
        table.insert(temp, self.LastMaxWinner:getData4Pack())
    end

    return temp
end

BLotteryTicketPlayerBetInfo = {}
function BLotteryTicketPlayerBetInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.PlayerGuid = nil
    o.BetGold = 0
    o.BetPotIndex = 0

    return o
end

function BLotteryTicketPlayerBetInfo:setData(data)
    self.PlayerGuid = data[1]
    self.BetGold = data[2]
    self.BetPotIndex = data[3]
end

function BLotteryTicketPlayerBetInfo:getData4Pack()
    local temp = {}
    table.insert(temp, self.PlayerGuid)
    table.insert(temp, self.BetGold)
    table.insert(temp, self.BetPotIndex)

    return temp
end

BLotteryTicketData = {}
function BLotteryTicketData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.State = 0
    o.StateLeftTm = 0
    o.MapBetPotBetInfo = nil
    o.RewardPotGold = 0 -- 奖池金币
    o.LastBaoZiDt = nil
    o.ListHistory = nil -- 牌型历史记录
    o.ListCard = nil -- 牌信息
    o.WinCardType = 0 -- 牌型信息
    o.LastMaxWinner = nil -- 上轮最大赢家

    return o
end

function BLotteryTicketData:setData(data)
    self.State = data[1]
    self.StateLeftTm = data[2]
    self.MapBetPotBetInfo = data[3]
    self.RewardPotGold = data[4]
    self.LastBaoZiDt = data[5]
    self.ListHistory = data[6]
    local l_c = data[7]
    if l_c ~= nil then
        local t_c = {}
        for i, v in pairs(l_c) do
            local c_d = CardData:new(nil)
            c_d:setData(v)
            table.insert(t_c,c_d)
        end
        self.ListCard = t_c
    end
    self.WinCardType = data[8]
    local a = data[9]
    if a ~= nil then
        local a_t = BLotteryTicketPlayerInfo:new(nil)
        a_t:setData(a)
        self.LastMaxWinner = a_t
    end
end

function BLotteryTicketData:getData4Pack()
    local temp = {}
    table.insert(temp, self.State)
    table.insert(temp, self.StateLeftTm)
    table.insert(temp, self.MapBetPotBetInfo)
    table.insert(temp, self.RewardPotGold)
    table.insert(temp, self.LastBaoZiDt)
    table.insert(temp, self.ListHistory)
    table.insert(temp, self.ListCard)
    table.insert(temp, self.WinCardType)
    if self.LastMaxWinner ~= nil then
        table.insert(temp, self.LastMaxWinner:getData4Pack())
    end

    return temp
end