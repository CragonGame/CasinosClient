-- Copyright(c) Cragon. All rights reserved.

---------------------------------------
MethodTypeTexasDesktop = {
    [0] = "None", -- 无效
    None = 0,

    [1] = "PlayerPushStackRequest", -- 请求换钱
    PlayerPushStackRequest = 1,
    --[[[2] = "PlayerBuyAndSendItemRequest", -- 请求购买并赠送Item
    PlayerBuyAndSendItemRequest = 2,]]
    [3] = "PlayerAutoActionRequest", -- 请求一次性托管
    PlayerAutoActionRequest = 3,
    [4] = "PlayerCancelAutoActionRequest", -- 请求取消一次性和永久托管
    PlayerCancelAutoActionRequest = 4,
    [5] = "PlayerActionBetRequest", -- 玩家操作，押注
    PlayerActionBetRequest = 5,
    [6] = "PlayerActionFoldRequest", -- 玩家操作，盖牌
    PlayerActionFoldRequest = 6,
    [7] = "PlayerActionCheckRequest", -- 玩家操作，让牌
    PlayerActionCheckRequest = 7,
    [8] = "PlayerActionCallRequest", -- 玩家操作，跟注
    PlayerActionCallRequest = 8,
    [9] = "PlayerActionRaiseRequest", -- 玩家操作，加注
    PlayerActionRaiseRequest = 9,
    [10] = "PlayerActionReRaiseRequest", -- 玩家操作，再加注
    PlayerActionReRaiseRequest = 10,
    [11] = "PlayerActionAllInRequest", -- 玩家操作，全下
    PlayerActionAllInRequest = 11,
    PlayerShowCardRequest = 12, -- 玩家请求更新亮牌状态
    PlayerShowCardResponse = 13, -- 玩家响应更新亮牌状态

    [100] = "DesktopIdleNotify", -- 桌子空闲
    DesktopIdleNotify = 100,
    [101] = "DesktopPreFlopNotify", -- 第一轮下注
    DesktopPreFlopNotify = 101,
    [102] = "DesktopFlopNotify", -- 第二轮下注
    DesktopFlopNotify = 102,
    [103] = "DesktopTurnNotify", -- 第三轮下注
    DesktopTurnNotify = 103,
    [104] = "DesktopRiverNotify", -- 第四轮下注
    DesktopRiverNotify = 104,
    [105] = "DesktopShowdownNotify", -- 摊牌
    DesktopShowdownNotify = 105,
    [106] = "DesktopGameEndNotify", -- 牌局结束
    DesktopGameEndNotify = 106,
    [107] = "PlayerPushStackNotify", -- 请求换钱
    PlayerPushStackNotify = 107,
    --[108] = "PlayerBuyAndSendItemNotify", -- 玩家购买并赠送道具广播
    --PlayerBuyAndSendItemNotify = 108,
    [109] = "PlayerStateChangeNotify", -- 玩家状态改变
    PlayerStateChangeNotify = 109,
    [110] = "PlayerActionNotify", -- 玩家操作，包含状态改变
    PlayerActionNotify = 110,
    [111] = "PlayerGetTurnNotify", -- 玩家获得行动机会
    PlayerGetTurnNotify = 111,
    --[[[112] = "PlayerCurrentGiftChangeNotify", -- 玩家当前礼物更改
    PlayerCurrentGiftChangeNotify = 112,]]
    [113] = "PlayerPushStackResultNotify", -- 请求换钱
    PlayerPushStackResultNotify = 113,
    PlayerShowCardStateNotify = 114, -- 玩家亮牌状态广播

    TexasMTTUpdateRealtimeInfoNotify = 130, -- MTT，通知刷新赛况实时简要信息，全桌广播
    TexasMTTServerAutoActionStateChangeNotify = 131, -- MTT，服务端托管状态变更通知，仅本人收到
    TexasMTTUpdateProcessNotify = 132, -- MTT，进入前50%，30%，奖励圈，决赛桌，终极对决通知，全桌广播
    TexasMTTDesktopStartOrPause = 133, -- MTT，桌子暂停或启动。参数：BMatchTexasDesktopStartOrPauseNotify
    TexasMTTDesktoPlayerInfoUpdate = 134, -- MTT，桌子中玩家的赛事信息更新通知，全桌广播。参数：BMatchTexasPlayerInfo
}

TexasDesktopSeatNum = {
    Unlimited = 0,
    Five = 5,
    Six = 6,
    Nine = 9,
}

TexasDesktopGameSpeed = {
    Unlimited = 0,
    Normal = 1,
    Fast = 2,
    [0] = "Unlimited", -- 不限
    [1] = "Normal", -- 正常
    [2] = "Fast", -- 快速
}

TexasDesktopPlayerState = {
    Ob = 0,
    Wait4Next = 1,
    WaitWhile = 2,
    InGame = 3,
    HoldSeat = 4,
    [0] = "Ob", -- 观战
    [1] = "Wait4Next", -- 未打牌，等待下一局
    [2] = "WaitWhile", -- 未打牌，暂时离开
    [3] = "InGame", -- 打牌中
    [4] = "HoldSeat", -- 留座，为赛事桌使用，留座头像是灰态
}

TexasDesktopState = {
    Idle = 0,
    PreFlop = 1,
    Flop = 2,
    Turn = 3,
    River = 4,
    GameEnd = 5,
    [0] = "Idle",
    [1] = "PreFlop",
    [2] = "Flop",
    [3] = "Turn",
    [4] = "River",
    [5] = "GameEnd",
}

TexasPlayerShowCardState = {
    None = 0,
    First = 1,
    Second = 2,
    FirstAndSecond = 3,
}

DesktopStackChangeTypeTexas = {
    Bet = 0,
    Win = 1,
    ChargeForDeskUse = 2,
    PushStack = 3,
    [0] = "Bet", -- 下注的筹码
    [1] = "Win", -- 赢的筹码
    [2] = "ChargeForDeskUse", -- 台费
    [3] = "PushStack",
}

--DesktopTypeTexas = {
--    Classic = 0,
--    MustBet = 1,
--    [0] = "Classic", -- 经典
--    [1] = "MustBet", -- 必下
--}

BotGroupWinOrLoseStateTexas = {
    NeedWin = 0,
    NeedLose = 1,
    [0] = "NeedWin",
    [1] = "NeedLose",
}

RoundTypeTexas = {
    PreFlop = 0,
    Flop = 1,
    Turn = 2,
    River = 3,
    [0] = "PreFlop",
    [1] = "Flop",
    [2] = "Turn",
    [3] = "River",
}

PlayerActionTypeTexas = {
    None = 0,
    Bet = 1,
    Fold = 2,
    Check = 3,
    Call = 4,
    Raise = 5,
    ReRaise = 6,
    AllIn = 7,
    [0] = "None", -- 无操作
    [1] = "Bet", -- 押注
    [2] = "Fold", -- 盖牌
    [3] = "Check", -- 让牌
    [4] = "Call", -- 跟注
    [5] = "Raise", -- 加注
    [6] = "ReRaise", -- 再加注
    [7] = "AllIn", -- 全下
}

PlayerAutoActionTypeTexas = {
    None = 0,
    Fold = 1,
    CheckFold = 2,
    Check = 3,
    CallAny = 4,
    [0] = "None", -- 无操作
    [1] = "Fold", -- 盖牌
    [2] = "CheckFold", -- 让牌，盖牌
    [3] = "Check", -- 让牌
    [4] = "CallAny", -- 任意跟注
}

PlayerCanActionTypeTexas = {
    None = 0,
    Fold = 1,
    CheckFold = 2,
    Check = 3,
    Call = 4,
    Raise = 5,
    [0] = "None", -- 无操作
    [1] = "Fold", -- 盖牌
    [2] = "CheckFold", -- 让牌，盖牌
    [3] = "Check", -- 让牌
    [4] = "Call", -- 跟注
    [5] = "Raise", -- 加注
}

HandRankTypeTexas = {
    None = 0,
    HighCard = 1000,
    Pair = 2000,
    TwoPairs = 3000,
    ThreeOfAKind = 4000,
    Straight = 5000,
    Flush = 6000,
    FullHouse = 7000,
    FourOfAKind = 8000,
    StraightFlush = 9000,
    [0] = "None",
    [1000] = "HighCard",
    [2000] = "Pair",
    [3000] = "TwoPairs",
    [4000] = "ThreeOfAKind",
    [5000] = "Straight",
    [6000] = "Flush",
    [7000] = "FullHouse",
    [8000] = "FourOfAKind",
    [9000] = "StraightFlush",
}

HandRankTypeTexasH = {
    HighCard = 0,
    Pair = 1,
    TwoPairs = 2,
    ThreeOfAKind = 3,
    Straight = 4,
    Flush = 5,
    FullHouse = 6,
    FourOfAKind = 7,
    StraightFlush = 8,
    RoyalFlush = 9,
    [0] = "HighCard",
    [1] = "Pair",
    [2] = "TwoPairs",
    [3] = "ThreeOfAKind",
    [4] = "Straight",
    [5] = "Flush",
    [6] = "FullHouse",
    [7] = "FourOfAKind",
    [8] = "StraightFlush",
    [9] = "RoyalFlush",
}

---------------------------------------
MethodInfoTexasDesktop = {}

function MethodInfoTexasDesktop:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.id = 0
    o.data = nil

    return o
end

function MethodInfoTexasDesktop:getData4Pack()
    local t = {}
    table.insert(t, self.id)
    table.insert(t, self.data)
    return t
end

---------------------------------------
PlayerDataDesktopTexas = {}

function PlayerDataDesktopTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.PlayerGuid = nil
    o.NickName = nil
    o.AccountId = nil
    o.IconName = nil
    o.VIPLevel = 0
    o.Gender = true
    o.PlayerId = 0
    o.BotTbId = 0
    o.Stack = 0
    o.CurrentGiftItemData = nil
    o.SeatIndex = 255
    o.CurrentTotalBet = 0
    o.CurrentRoundBet = 0
    o.DesktopPlayerState = 0
    o.PlayerActionType = 0
    o.WaitWhileTime = 0.0
    return o
end

function PlayerDataDesktopTexas:setData(m_p, data)
    self.PlayerGuid = data.PlayerGuid
    self.NickName = data.NickName
    self.AccountId = data.AccountId
    self.IconName = data.IconName
    self.VIPLevel = data.VIPLevel
    self.Gender = data.Gender
    self.PlayerId = data.PlayerId
    local t_p_data = m_p.unpack(data.PlayerData)
    self.BotTbId = t_p_data[1]
    self.Stack = math.ceil(t_p_data[2])
    local i_data = t_p_data[3]
    local item_data = nil
    if (i_data ~= nil) then
        item_data = ItemData1:new(nil)
        item_data:setData(i_data)
    end
    self.CurrentGiftItemData = item_data
    self.SeatIndex = math.ceil(t_p_data[4])
    self.CurrentTotalBet = t_p_data[5]
    self.CurrentRoundBet = t_p_data[6]
    self.DesktopPlayerState = t_p_data[7]
    self.PlayerActionType = t_p_data[8]
    self.WaitWhileTime = t_p_data[9]
end

---------------------------------------
BDesktopSnapshotNormalTexas = {}

function BDesktopSnapshotNormalTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.DesktopTbId = 0
    o.Ante = 0
    return o
end

function BDesktopSnapshotNormalTexas:setData(data)
    self.DesktopTbId = data[1]
    self.Ante = data[2]
end

---------------------------------------
DesktopSnapshotDataTexas = {}

function DesktopSnapshotDataTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.desktop_guid = nil
    o.seat_num = 0
    o.game_speed = 0
    o.desktop_state = 0
    o.pot_total = 0
    o.list_pot = nil
    o.player_turn = nil
    o.list_showdown = nil
    o.list_winner = nil
    o.list_community_cards = nil
    o.dealer_seat_index = 0
    o.list_seat_player = nil
    o.list_actorinfo = nil
    o.list_showcard = nil
    o.showcard_state = 0
    o.my_hand_card = nil
    o.is_vip = false
    o.is_private = false
    o.desktop_action_waiting_tm = 0
    o.desktop_waitwhile_tm = 0
    o.normal_texas = nil
    o.match_texas = nil
    return o
end

function DesktopSnapshotDataTexas:setData(data)
    self.desktop_guid = data[1]
    self.seat_num = data[2]
    self.game_speed = data[3]
    self.desktop_state = data[4]
    self.pot_total = data[5]
    self.list_pot = data[6]
    local p_t1 = data[7]
    local p_t = PlayerTurnDataTexas:new(nil)
    p_t:setData(p_t1)
    self.player_turn = p_t
    local list_showdown1 = data[8]
    local list_showdown = {}
    for i, v in pairs(list_showdown1) do
        local card_data = PlayerHoleCardDataTexas:new(nil)
        card_data:setData(v)
        table.insert(list_showdown, card_data)
    end
    self.list_showdown = list_showdown
    local list_winner = {}
    for i, v in pairs(data[9]) do
        local win_data = PlayerWinnerDataTexas:new(nil)
        win_data:setData(v)
        table.insert(list_winner, win_data)
    end
    self.list_winner = list_winner
    self.list_community_cards = data[10]
    self.dealer_seat_index = data[11]
    self.list_seat_player = data[12]
    local list_actorinfo = {}
    for i, v in pairs(data[13]) do
        local p_data = PlayerDataDesktop:new(nil)
        p_data:setData(v)
        table.insert(list_actorinfo, p_data)
    end
    self.list_actorinfo = list_actorinfo
    local list_showc = {}
    for i, v in pairs(data[14]) do
        local show_data = TexasPlayerShowCardData:new(nil)
        show_data:setData(v)
        table.insert(list_showc, show_data)
    end
    self.list_showcard = list_showc
    self.showcard_state = data[15]
    self.my_hand_card = data[16]
    self.is_vip = data[17]
    self.is_private = data[18]
    self.desktop_action_waiting_tm = data[19]
    self.desktop_waitwhile_tm = data[20]
    local normal_t = data[21]
    local m_normal_t = nil
    if normal_t ~= nil then
        m_normal_t = BDesktopSnapshotNormalTexas:new(nil)
        m_normal_t:setData(normal_t)
    end
    self.normal_texas = m_normal_t
    local match_data = data[22]
    local m_data = nil
    if match_data ~= nil then
        m_data = BDesktopSnapshotMatchTexas:new(nil)
        m_data:setData(match_data)
    end
    self.match_texas = m_data
end

---------------------------------------
DesktopFilterTexas = {}

function DesktopFilterTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.seat_num = 0
    o.game_speed = 0
    o.desktop_tableid = 0
    o.is_vip = false
    o.is_seat_full = false
    --o.desktoptype_texas = 0
    o.is_private = false
    o.ante = 0
    return o
end

function DesktopFilterTexas:setData(data)
    self.seat_num = data[1]
    self.game_speed = data[2]
    self.desktop_tableid = data[3]
    self.is_vip = data[4]
    self.is_seat_full = data[5]
    --self.desktoptype_texas = data[6]
    self.is_private = data[6]
    self.ante = data[7]
end

function DesktopFilterTexas:getData4Pack()
    local p_d = {}
    table.insert(p_d, self.seat_num)
    table.insert(p_d, self.game_speed)
    table.insert(p_d, self.desktop_tableid)
    table.insert(p_d, self.is_vip)
    table.insert(p_d, self.is_seat_full)
    --table.insert(p_d, self.desktoptype_texas)
    table.insert(p_d, self.is_private)
    table.insert(p_d, self.ante)
    return p_d
end

---------------------------------------
DesktopInfoTexas = {}

function DesktopInfoTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.desktop_etguid = nil
    o.seat_num = 0
    o.game_speed = 0
    o.is_vip = false
    o.desktop_tableid = 0
    o.list_seat_player = nil
    o.seat_player_num = 0
    o.all_player_num = 0
    o.ante = 0
    --o.desktoptype_texas = 0
    return o
end

function DesktopInfoTexas:isFull()
    local seat_count = self.seat_num
    return seat_count == self.seat_player_num
end

---------------------------------------
PrivateDesktopCreateInfoTexas = {}

function PrivateDesktopCreateInfoTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.seat_num = 0
    o.game_speed = 0
    o.is_vip = false
    o.desktop_tableid = 0
    o.ante = 0
    --o.desktoptype_texas = 0
    return o
end

function PrivateDesktopCreateInfoTexas:getData4Pack()
    local p_d = {}
    table.insert(p_d, self.seat_num)
    table.insert(p_d, self.game_speed)
    table.insert(p_d, self.is_vip)
    table.insert(p_d, self.desktop_tableid)
    table.insert(p_d, self.ante)
    return p_d
end

---------------------------------------
PlayerModuleDataTexas = {}

function PlayerModuleDataTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.GameTotal = 0
    o.GameWin = 0
    return o
end

function PlayerModuleDataTexas:setData(data)
    self.GameTotal = data[1]
    self.GameWin = data[2]
end

---------------------------------------
TexasChipEnterPot = {}

function TexasChipEnterPot:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.list_seatchip_enter_pot = nil
    o.list_pot = nil
    o.pot_total = 0
    return o
end

function TexasChipEnterPot:setData(data)
    self.list_seatchip_enter_pot = data[1]
    self.list_pot = data[2]
    self.pot_total = data[3]
end

---------------------------------------
PlayerCanActionDataTexas = {}

function PlayerCanActionDataTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.can_action = 0
    o.chip = 0

    return o
end

function PlayerCanActionDataTexas:setData(data)
    self.can_action = data[1]
    self.chip = data[2]
end

---------------------------------------
PlayerHoleCardDataTexas = {}

function PlayerHoleCardDataTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.first_card = nil
    o.second_card = nil
    o.hand_rank_type = 0
    return o
end

function PlayerHoleCardDataTexas:setData(data)
    self.player_guid = data[1]
    self.first_card = data[2]
    self.second_card = data[3]
    self.hand_rank_type = data[4]
end

---------------------------------------
PlayerPreFlopDataTexas = {}

function PlayerPreFlopDataTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.stack = 0
    return o
end

function PlayerPreFlopDataTexas:setData(data)
    self.player_guid = data[1]
    self.stack = data[2]
end

---------------------------------------
PlayerTurnDataTexas = {}

function PlayerTurnDataTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.player_turn_lefttm = 0
    return o
end

function PlayerTurnDataTexas:setData(data)
    self.player_guid = data[1]
    self.player_turn_lefttm = data[2]
end

---------------------------------------
PlayerWinnerDataTexas = {}

function PlayerWinnerDataTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.win_chip = 0
    o.map_win_pot = nil
    o.stack = 0
    return o
end

function PlayerWinnerDataTexas:setData(data)
    self.player_guid = data[1]
    self.win_chip = data[2]
    self.map_win_pot = data[3]
    self.stack = data[4]
end

---------------------------------------
TexasPlayerGameEndAddData = {}

function TexasPlayerGameEndAddData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.add_exp = 0
    o.add_point = 0
    return o
end

function TexasPlayerGameEndAddData:setData(data)
    self.player_guid = data[1]
    self.add_exp = data[2]
    self.add_point = data[3]
end

---------------------------------------
TexasPlayerShowCardData = {}

function TexasPlayerShowCardData:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.first_card = nil
    o.second_card = nil
    return o
end

function TexasPlayerShowCardData:setData(data)
    self.player_guid = data[1]
    self.first_card = data[2]
    self.second_card = data[3]
end

---------------------------------------
TexasDesktopNotifyPlayerShowCard = {}

function TexasDesktopNotifyPlayerShowCard:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.list_showcard = nil
    return o
end

function TexasDesktopNotifyPlayerShowCard:setData(data)
    local l = {}
    for i, v in pairs(data[1]) do
        local d = TexasPlayerShowCardData:new(nil)
        d:setData(v)
        table.insert(l, d)
    end
    self.list_showcard = l
end

---------------------------------------
TexasDesktopNotifyIdle = {}

function TexasDesktopNotifyIdle:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.list_player_state = nil
    return o
end

function TexasDesktopNotifyIdle:setData(data)
    local l = data[1]
    local real_l = {}
    for i, v in pairs(l) do
        local p = PlayerStateDataTexas:new(nil)
        p:setData(v)
        table.insert(real_l, p)
    end
    self.list_player_state = real_l
end

---------------------------------------
DesktopNotifyPlayerActionTexas = {}

function DesktopNotifyPlayerActionTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.action = 0
    o.state = 0
    o.current_total_bet = 0
    o.current_round_bet = 0
    o.current_bet_chip = 0
    o.current_stack = 0
    o.pot_total = 0
    return o
end

function DesktopNotifyPlayerActionTexas:setData(data)
    self.player_guid = data[1]
    self.action = data[2]
    self.state = data[3]
    self.current_total_bet = data[4]
    self.current_round_bet = data[5]
    self.current_bet_chip = data[6]
    self.current_stack = data[7]
    self.pot_total = data[8]
end

---------------------------------------
DesktopNotifyPlayerGetTurnTexas = {}

function DesktopNotifyPlayerGetTurnTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_turn = nil
    return o
end

function DesktopNotifyPlayerGetTurnTexas:setData(data)
    local p_t1 = data[1]
    local p_t = PlayerTurnDataTexas:new(nil)
    p_t:setData(p_t1)
    self.player_turn = p_t
end

---------------------------------------
DesktopNotifyDesktopPreFlopTexas = {}

function DesktopNotifyDesktopPreFlopTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.dealer_seat_index = nil
    o.smallblind_guid = nil
    o.bigblind_guid = nil
    o.small_blind = 0
    o.big_blind = 0
    o.ante = 0
    o.list_player_preflop_data = nil
    o.first_card = nil
    o.second_card = nil
    o.pot_total = 0
    o.list_pot = nil
    o.list_player_state = nil
    return o
end

function DesktopNotifyDesktopPreFlopTexas:setData(data)
    self.dealer_seat_index = data[1]
    self.smallblind_guid = data[2]
    self.bigblind_guid = data[3]
    self.small_blind = data[4]
    self.big_blind = data[5]
    self.ante = data[6]
    local l = {}
    for i, v in pairs(data[7]) do
        local p = PlayerPreFlopDataTexas:new(nil)
        p:setData(v)
        table.insert(l, p)
    end
    self.list_player_preflop_data = l
    self.first_card = data[8]
    self.second_card = data[9]
    self.pot_total = data[10]
    self.list_pot = data[11]
    local real_l = {}
    for i, v in pairs(data[12]) do
        local p = PlayerStateDataTexas:new(nil)
        p:setData(v)
        table.insert(real_l, p)
    end
    self.list_player_state = real_l
end

---------------------------------------
DesktopNotifyDesktopFlopTexas = {}

function DesktopNotifyDesktopFlopTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.first_card = nil
    o.second_card = nil
    o.third_card = nil
    o.chip_enter_pot = nil
    return o
end

function DesktopNotifyDesktopFlopTexas:setData(data)
    self.first_card = data[1]
    self.second_card = data[2]
    self.third_card = data[3]
    local chip_enter = TexasChipEnterPot:new(nil)
    chip_enter:setData(data[4])
    self.chip_enter_pot = chip_enter
end

---------------------------------------
DesktopNotifyDesktopTurnTexas = {}

function DesktopNotifyDesktopTurnTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.card = nil
    o.chip_enter_pot = nil
    return o
end

function DesktopNotifyDesktopTurnTexas:setData(data)
    self.card = data[1]
    local chip_enter = TexasChipEnterPot:new(nil)
    chip_enter:setData(data[2])
    self.chip_enter_pot = chip_enter
end

---------------------------------------
DesktopNotifyDesktopRiverTexas = {}

function DesktopNotifyDesktopRiverTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.card = nil
    o.chip_enter_pot = nil
    return o
end

function DesktopNotifyDesktopRiverTexas:setData(data)
    self.card = data[1]
    local chip_enter = TexasChipEnterPot:new(nil)
    chip_enter:setData(data[2])
    self.chip_enter_pot = chip_enter
end

---------------------------------------
DesktopNotifyDesktopShowdownTexas = {}

function DesktopNotifyDesktopShowdownTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.list_playerholecard = nil
    o.list_carddata_left = nil
    o.chip_enter_pot = nil
    return o
end

function DesktopNotifyDesktopShowdownTexas:setData(data)
    local list_playerdata = {}
    for i, v in pairs(data[1]) do
        local card_data = PlayerHoleCardDataTexas:new(nil)
        card_data:setData(v)
        table.insert(list_playerdata, card_data)
    end
    self.list_playerholecard = list_playerdata
    self.list_carddata_left = data[2]
    local chip_enter = TexasChipEnterPot:new(nil)
    chip_enter:setData(data[3])
    self.chip_enter_pot = chip_enter
end

---------------------------------------
DesktopNotifyGameEndTexas = {}

function DesktopNotifyGameEndTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.list_winner = nil
    o.chip_enter_pot = nil
    o.rebegin_tm = 0.0
    o.list_allplayer_adddata = nil
    o.list_player_state = nil
    return o
end

function DesktopNotifyGameEndTexas:setData(data)
    local l = {}
    for i, v in pairs(data[1]) do
        local win_data = PlayerWinnerDataTexas:new(nil)
        win_data:setData(v)
        table.insert(l, win_data)
    end
    self.list_winner = l
    local c_e = data[2]
    if c_e ~= nil then
        local chip_enter = TexasChipEnterPot:new(nil)
        chip_enter:setData(c_e)
        self.chip_enter_pot = chip_enter
    end
    self.rebegin_tm = data[3]
    local l_adddata = {}
    for i, v in pairs(data[4]) do
        local add_data = TexasPlayerGameEndAddData:new(nil)
        add_data:setData(v)
        table.insert(l_adddata, add_data)
    end
    self.list_allplayer_adddata = l_adddata
    local real_l = {}
    for i, v in pairs(data[5]) do
        local p = PlayerStateDataTexas:new(nil)
        p:setData(v)
        table.insert(real_l, p)
    end
    self.list_player_state = real_l
end

---------------------------------------
DesktopNotifyPlayerPushStackTexas = {}

function DesktopNotifyPlayerPushStackTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.stack = 0
    return o
end

function DesktopNotifyPlayerPushStackTexas:setData(data)
    self.player_guid = data[1]
    self.stack = data[2]
end

---------------------------------------
PlayerStateDataTexas = {}

function PlayerStateDataTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.player_guid = nil
    o.state = 0
    o.action = 0
    return o
end

function PlayerStateDataTexas:setData(data)
    self.player_guid = data[1]
    self.state = data[2]
    self.action = data[3]
end

---------------------------------------
DesktopNotifyPlayerStateChangeTexas = {}

function DesktopNotifyPlayerStateChangeTexas:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    o.list_playerstate = nil
    return o
end

function DesktopNotifyPlayerStateChangeTexas:setData(data)
    local l = data[1]
    local real_l = {}
    for i, v in pairs(l) do
        local p = PlayerStateDataTexas:new(nil)
        p:setData(v)
        table.insert(real_l, p)
    end
    self.list_playerstate = real_l
end