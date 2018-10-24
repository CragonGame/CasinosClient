-- Copyright(c) Cragon. All rights reserved.

RankingListType = {
    Chip = 0, --筹码
    Gold = 1, --金币
    Level = 2, --等级
    Gift = 3, --礼物
    WinGold = 4, --豪胜
    RedEnvelopes = 5, --红包
}

-- 筹码榜数据
RankingGold = {}
function RankingGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.nick_name = nil
    o.account_id = nil
    o.gold = 0
    o.icon_name = nil
    return o
end

function RankingGold:setData(data)
    self.player_guid = data[1]
    self.nick_name = data[2]
    self.account_id = data[3]
    self.gold = data[4]
    self.icon_name = data[5]
end

-- 金币榜数据
RankingDiamond = {}
function RankingDiamond:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.nick_name = nil
    o.account_id = nil
    o.diamond = 0
    o.icon_name = nil
    return o
end

function RankingDiamond:setData(data)
    self.player_guid = data[1]
    self.nick_name = data[2]
    self.account_id = data[3]
    self.diamond = data[4]
    self.icon_name = data[5]
end

-- 等级数据
RankingLevel = {}
function RankingLevel:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.nick_name = nil
    o.account_id = nil
    o.player_level = 0
    o.icon_name = nil
    return o
end

function RankingLevel:setData(data)
    self.player_guid = data[1]
    self.nick_name = data[2]
    self.account_id = data[3]
    self.player_level = data[4]
    self.icon_name = data[5]
end

-- 礼物榜数据
RankingGift = {}
function RankingGift:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.nick_name = nil
    o.account_id = nil
    o.gift_value = 0
    o.icon_name = nil
    return o
end

function RankingGift:setData(data)
    self.player_guid = data[1]
    self.nick_name = data[2]
    self.account_id = data[3]
    self.gift_value = data[4]
    self.icon_name = data[5]
end


-- 赢家榜数据
RankingWinGold = {}
function RankingWinGold:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.nick_name = nil
    o.account_id = nil
    o.win_gold = 0
    o.icon_name = nil
    return o
end

function RankingWinGold:setData(data)
    self.player_guid = data[1]
    self.nick_name = data[2]
    self.account_id = data[3]
    self.win_gold = data[4]
    self.icon_name = data[5]
end

RankingRedEnvelopes = {}
function RankingRedEnvelopes:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.nick_name = nil
    o.account_id = nil
    o.win_redenvelope = 0
    o.icon_name = nil
    return o
end

function RankingRedEnvelopes:setData(data)
    self.player_guid = data[1]
    self.nick_name = data[2]
    self.account_id = data[3]
    self.win_redenvelope = data[4]
    self.icon_name = data[5]
end