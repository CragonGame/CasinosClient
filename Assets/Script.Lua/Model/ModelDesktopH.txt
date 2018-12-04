-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
_eDesktopHState = {
    Idle = 0,
    Ready = 1,
    Bet = 2,
    GameEnd = 3,
    Rest = 4,
}

_eDesktopHPlayerType = {
    BankPlayer = 1,
    SeatPlayer = 2,
}

DesktopHDialyGetBetRewardState = {
    CanntGet = 0, -- 不能领取
    NotGet = 1, -- 可领未领
    Get = 2, -- 已领
}

---------------------------------------
BDesktopHDialyBetReward = {}

function BDesktopHDialyBetReward:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.FactoryName = nil
    o.BetDt = nil
    o.TotalBetGold = 0
    o.MapGetRewardState = nil
    return o
end

function BDesktopHDialyBetReward:setData(data)
    self.FactoryName = data[1]
    self.BetDt = data[2]
    self.TotalBetGold = data[3]
    self.MapGetRewardState = data[4]
end

function BDesktopHDialyBetReward:getData4Pack()
    local t = {}
    table.insert(t, self.FactoryName)
    table.insert(t, self.BetDt)
    table.insert(t, self.TotalBetGold)
    table.insert(t, self.MapGetRewardState)
    return t
end

---------------------------------------
BDesktopHPlayerBetInfo = {}

function BDesktopHPlayerBetInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.PlayerGuid = nil
    o.BetGold = 0
    o.BetPotIndex = 0
    return o
end

function BDesktopHPlayerBetInfo:setData(data)
    self.PlayerGuid = data[1]
    self.BetGold = data[2]
    self.BetPotIndex = data[3]
end

function BDesktopHPlayerBetInfo:getData4Pack()
    local p_d = {}
    table.insert(p_d, self.PlayerGuid)
    table.insert(p_d, self.BetGold)
    table.insert(p_d, self.BetPotIndex)
    return p_d
end

---------------------------------------
BDesktopHNotifySeatPlayerBet = {}

function BDesktopHNotifySeatPlayerBet:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.map_betinfo = nil
    return o
end

function BDesktopHNotifySeatPlayerBet:setData(data)
    self.player_guid = data[1]
    self.map_betinfo = data[2]
end

function BDesktopHNotifySeatPlayerBet:getData4Pack()
    local p_d = {}
    table.insert(p_d, self.player_guid)
    table.insert(p_d, self.map_betinfo)
    return p_d
end

---------------------------------------
BDesktopHNotifyBetDeltaInfo = {}

function BDesktopHNotifyBetDeltaInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.map_betpot_betdeltainfo = nil
    o.map_standplayer_betdeltainfo = nil
    o.list_seatplayer_betdeltainfo = nil
    return o
end

function BDesktopHNotifyBetDeltaInfo:setData(data)
    self.map_betpot_betdeltainfo = data[1]
    self.map_standplayer_betdeltainfo = data[2]
    local l = data[3]
    if l ~= nil then
        local t_l = {}
        for i, v in pairs(l) do
            local s_b = BDesktopHNotifySeatPlayerBet:new(nil)
            s_b:setData(v)
            table.insert(t_l, s_b)
        end
        self.list_seatplayer_betdeltainfo = t_l
    end
end

function BDesktopHNotifyBetDeltaInfo:getData4Pack()
    local p_d = {}
    table.insert(p_d, self.map_betpot_betdeltainfo)
    table.insert(p_d, self.map_standplayer_betdeltainfo)
    table.insert(p_d, self.list_seatplayer_betdeltainfo)
    return p_d
end

---------------------------------------
BDesktopHNotifyGameEndBetPot = {}

function BDesktopHNotifyGameEndBetPot:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.list_card = nil
    o.card_type = 0
    o.is_win = false
    o.bet_gold = 0
    o.winloose_gold = 0 -- 该下注池输赢总金币，不含开奖池所得金币
    o.gold_percent = 0
    return o
end

function BDesktopHNotifyGameEndBetPot:setData(data)
    local l = data[1]
    if l ~= nil then
        local l_t = {}
        for i, v in pairs(l) do
            local c_d = CardData:new(nil)
            c_d:setData(v)
            table.insert(l_t, c_d)
        end
        self.list_card = l_t
    end
    self.card_type = data[2]
    self.is_win = data[3]
    self.bet_gold = data[4]
    self.winloose_gold = data[5]
    self.gold_percent = data[6]
end

---------------------------------------
BDesktopHNotifyGameEndRewardPot = {}

function BDesktopHNotifyGameEndRewardPot:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.gold_before = 0
    o.gold_after = 0 -- 包含奖池抽水+开奖之后的最终值，gold_after=gold_before+map_pumping_gold.Sum-map_win_rewardpot_gold.Sum
    o.map_pumping_gold = nil -- key=pot index（banker pot=255），value仅仅是奖池抽水，不包含系统抽水
    o.map_win_rewardpot_gold = nil -- key=pot index（banker pot=255），奖池开奖，如果开奖，则map中Count>0
    return o
end

function BDesktopHNotifyGameEndRewardPot:setData(data)
    self.gold_before = data[1]
    self.gold_after = data[2]
    self.map_pumping_gold = data[3]
    self.map_win_rewardpot_gold = data[4]
end

---------------------------------------
-- 百人桌结算阶段庄家池信息，判定庄家输赢的方法是看stack_after-stack_before是正是负
BDesktopHNotifyGameEndBankerPot = {}

function BDesktopHNotifyGameEndBankerPot:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.list_card = nil
    o.card_type = 0
    o.stack_before = 0 -- 上次庄家拥有金币
    o.stack_after = 0 -- 所有下注池和庄家池结算并经过系统，奖池抽水后的金币值，最终Stack
    o.winloose_gold = 0 -- 本局除开奖池以外输赢的值，winloose_gold=stack_after-win_rewardpot_gold-stack_before
    o.win_rewardpot_gold = 0 -- 庄家池开奖Gold
    return o
end

function BDesktopHNotifyGameEndBankerPot:setData(data)
    local l = data[1]
    if l ~= nil then
        local l_t = {}
        for i, v in pairs(l) do
            local c_d = CardData:new(nil)
            c_d:setData(v)
            table.insert(l_t, c_d)
        end
        self.list_card = l_t
    end
    self.card_type = data[2]
    self.stack_before = data[3]
    self.stack_after = data[4]
    self.winloose_gold = data[5]
    self.win_rewardpot_gold = data[6]
end

---------------------------------------
BDesktopHNotifyGameEndBetPotPlayerWinLooseInfo = {} -- DesktopH单个下注池中玩家输赢

function BDesktopHNotifyGameEndBetPotPlayerWinLooseInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.is_win = false
    o.betpot_index = 0
    o.bet_gold = 0
    o.winloose_gold = 0 -- 永远正值，需要和is_win共同判断。不包含经过奖池开奖后的结果
    o.winloose_gold_percent = 0 -- 输赢倍率
    o.sys_pumping = 0 -- 系统抽水
    o.rewardpot_pumping = 0 -- 奖池抽水
    o.win_rewardpot_gold = 0 -- 赢取奖池Gold
    return o
end

function BDesktopHNotifyGameEndBetPotPlayerWinLooseInfo:setData(data)
    self.player_guid = data[1]
    self.is_win = data[2]
    self.betpot_index = data[3]
    self.bet_gold = data[4]
    self.winloose_gold = data[5]
    self.winloose_gold_percent = data[6]
    self.sys_pumping = data[7]
    self.rewardpot_pumping = data[8]
    self.win_rewardpot_gold = data[9]
end

---------------------------------------
BDesktopHGameResult = {}

function BDesktopHGameResult:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.desktoph_guid = nil
    o.factory_name = nil
    o.play_guid = nil
    o.left_tm = 0
    o.map_betpot_info = nil -- 闲家池信息
    o.bankerpot_info = nil -- 庄家池信息
    o.rewardpot_info = nil -- 奖池信息
    o.map_seatplayer_winloose_info = nil
    o.map_standplayer_gold = nil -- 无座玩家输赢信息，包含本人，所有无座玩家累加后的winloose_gold
    o.ListGameEndWinPlayer = nil -- 本局最大赢家列表，最多5个玩家
    o.banker_guid = nil -- 庄家Guid
    o.win_rewardpot_index = 0 -- 如果开奖，则开奖的下注池索引
    o.win_rewardpot_info = nil -- 奖池快照数据
    o.win_rewardpot_goldtotal = 0;-- 本局最大牌型如果可以开奖池，那么计算开奖池的百分比可以得到的开奖金币（Client无需使用）
    o.win_reward_max_multiply = 0;-- 本局最大牌型如果可以开奖池，那么对应的玩家可以得到的倍率上限（Client无需使用）
    o.premature_termination = false -- 是否提前结束下注
    return o
end

function BDesktopHGameResult:setData(data)
    self.desktoph_guid = data[1]
    self.factory_name = data[2]
    self.play_guid = data[3]
    self.left_tm = data[4]
    local m_b_i = data[5]
    if m_b_i ~= nil then
        local t_mbi = {}
        for i, v in pairs(m_b_i) do
            local g = BDesktopHNotifyGameEndBetPot:new(nil)
            g:setData(v)
            t_mbi[i] = g
        end
        self.map_betpot_info = t_mbi
    end
    local b_i = data[6]
    if b_i ~= nil then
        local b_p = BDesktopHNotifyGameEndBankerPot:new(nil)
        b_p:setData(b_i)
        self.bankerpot_info = b_p
    end
    local r_i = data[7]
    if r_i ~= nil then
        local b_p = BDesktopHNotifyGameEndRewardPot:new(nil)
        b_p:setData(r_i)
        self.rewardpot_info = b_p
    end
    local m_s_w_i = data[8]
    if m_s_w_i ~= nil then
        local t = {}
        for i, v in pairs(m_s_w_i) do
            if v ~= nil then
                local v_t = {}
                for v_i, v_v in pairs(v) do
                    local b_winloose = BDesktopHNotifyGameEndBetPotPlayerWinLooseInfo:new(nil)
                    b_winloose:setData(v_v)
                    table.insert(v_t, b_winloose)
                end
                t[i] = v_t
            end
        end
        self.map_seatplayer_winloose_info = t
    end
    self.map_standplayer_gold = data[9]
    local l_p = data[10]
    if l_p ~= nil then
        local t = {}
        for i, v in pairs(l_p) do
            local b_p = BDesktopHGameEndWinPlayer:new(nil)
            b_p:setData(v)
            table.insert(t, b_p)
        end
        self.ListGameEndWinPlayer = t
    end
    self.banker_guid = data[11]
    self.win_rewardpot_index = data[12]
    local w_i = data[13]
    if w_i ~= nil then
        local p_i = BDesktopHWinRewardPotInfo:new(nil)
        p_i:setData(w_i)
        o.win_rewardpot_info = p_i
    end
    self.win_rewardpot_goldtotal = data[14]
    self.win_reward_max_multiply = data[15]
    self.premature_termination = data[16]
end

---------------------------------------
BDesktopHData = {}

function BDesktopHData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.desktoph_guid = nil
    o.factory_name = nil
    o.play_guid = nil
    o.banker_player = nil -- 庄家
    o.list_bebanker = nil -- 申请上庄列表
    o.max_total_bet_gold = 0 -- 最大总下注额，由庄家Stack决定
    o.map_seatplayer = nil
    o.map_betpot_betinfo = nil
    o.game_result = nil
    o.state = 0
    o.left_tm = 0
    o.reward_pot = 0 -- 奖池当前最新值
    o.map_betpot_winloose_record = nil -- 最近输赢历史记录
    return o
end

function BDesktopHData:setData(data)
    self.desktoph_guid = data[1]
    self.factory_name = data[2]
    self.play_guid = data[3]
    local b_p = data[4]
    if b_p ~= nil then
        local p = PlayerDataDesktopH:new(nil)
        p:setData(b_p)
        self.banker_player = p
    end
    local l_p = data[5]
    if l_p ~= nil then
        local t_lp = {}
        for i, v in pairs(l_p) do
            local p = PlayerDataDesktopH:new(nil)
            p:setData(v)
            table.insert(t_lp, p)
        end
        self.list_bebanker = t_lp
    end
    self.max_total_bet_gold = data[6]
    local m_sp = data[7]
    if m_sp ~= nil then
        local t_sp = {}
        for i, v in pairs(m_sp) do
            local p = PlayerDataDesktopH:new(nil)
            p:setData(v)
            t_sp[i] = p
        end
        self.map_seatplayer = t_sp
    end
    self.map_betpot_betinfo = data[8]
    local g_r = data[9]
    if g_r ~= nil then
        local d_r = BDesktopHGameResult:new(nil)
        d_r:setData(g_r)
        self.game_result = d_r
    end
    self.state = data[10]
    self.left_tm = data[11]
    self.reward_pot = data[12]
    self.map_betpot_winloose_record = data[13]
end

---------------------------------------
BDesktopHWinRewardPotPlayer = {} -- DesktopH奖池开奖玩家信息

function BDesktopHWinRewardPotPlayer:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.account_id = nil
    o.nick_name = nil
    o.icon = nil
    o.vip_level = 0
    o.win_gold = 0
    return o
end

function BDesktopHWinRewardPotPlayer:setData(data)
    self.player_guid = data[1]
    self.account_id = data[2]
    self.nick_name = data[3]
    self.icon = data[4]
    self.vip_level = data[5]
    self.win_gold = data[6]
end

---------------------------------------
BDesktopHWinRewardPotInfo = {} -- DesktopH奖池开奖信息

function BDesktopHWinRewardPotInfo:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.list_card = nil
    o.win_rewardpot_gold = 0 -- 本次开奖池所开出的Delta Gold，是正值
    o.date_time = nil
    o.list_playerinfo = nil
    return o
end

function BDesktopHWinRewardPotInfo:setData(data)
    local l_c = data[1]
    if l_c ~= nil then
        local t_c = {}
        for i, v in pairs(l_c) do
            local c_d = CardData:new(nil)
            c_d:setData(v)
            table.insert(t_c, c_d)
        end
        self.list_card = t_c
    end
    self.win_rewardpot_gold = data[2]
    self.date_time = data[3]
    local l_p = data[4]
    if l_p ~= nil then
        local t_p = {}
        for i, v in pairs(l_p) do
            local d_p = BDesktopHWinRewardPotPlayer:new(nil)
            d_p:setData(v)
            table.insert(t_p, d_p)
        end
        self.list_playerinfo = t_p
    end
end

---------------------------------------
BDesktopHGoldPercentConfig = {}

function BDesktopHGoldPercentConfig:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o._id = nil -- 默认标识
    o.HandRankTypeH = 0 -- 类型
    o.GoldPercent = 0 -- 倍数
    o.WinRewardPotPercent = 0 -- 赢取奖池百分比
    o.FactoryName = nil
    return o
end

function BDesktopHGoldPercentConfig:setData(data)
    self._id = data[1]
    self.HandRankTypeH = data[2]
    self.GoldPercent = data[3]
    self.WinRewardPotPercent = data[4]
    self.FactoryName = data[5]
end

---------------------------------------
BDesktopHGameEndWinPlayer = {}

function BDesktopHGameEndWinPlayer:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.account_id = nil
    o.nick_name = nil
    o.icon = nil
    o.vip_level = 0
    o.win_gold = 0 -- 包含开奖池后的输赢Delta Gold
    return o
end

function BDesktopHGameEndWinPlayer:setData(data)
    self.player_guid = data[1]
    self.account_id = data[2]
    self.nick_name = data[3]
    self.icon = data[4]
    self.vip_level = data[5]
    self.win_gold = data[6]
end

function BDesktopHGameEndWinPlayer:getData4Pack()
    local t = {}
    table.insert(t, self.player_guid)
    table.insert(t, self.account_id)
    table.insert(t, self.nick_name)
    table.insert(t, self.icon)
    table.insert(t, self.vip_level)
    table.insert(t, self.win_gold)
    return t
end

---------------------------------------
PlayerDataDesktopH = {}

function PlayerDataDesktopH:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.PlayerInfoCommon = nil
    o.GiftTbId = 0
    o.BotTbId = 0
    o.Gold = 0
    return o
end

function PlayerDataDesktopH:setData(data)
    local p_c = data[1]
    if p_c ~= nil then
        local p = PlayerInfoCommon:new(nil)
        p:setData(p_c)
        self.PlayerInfoCommon = p
    end
    self.GiftTbId = data[2]
    self.BotTbId = data[3]
    self.Gold = data[4]
end