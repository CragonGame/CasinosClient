-- 积分变更原因
PointChangeReason = {
	BuyItem = 0,
    ChangeByManager = 1
}

-- GoldAccChangeReason
GoldAccChangeReason = {
	None = 0,
    PlayerGiveOtherGold = 10,
    PlayerRecvGoldFromOther = 11,
    PlayerObtainRelief = 12, -- 玩家领取救济金
    BuyItem = 13,
    BuyGold = 14,
    SellItem = 15,
    BankDeposit = 16,
    BankWithdraw = 17,
    DailyReward = 18,
    OnlineReward = 19,
    TimingReward = 20,
    GrowReward = 21,
    AddByGoldPackage = 22,-- 金币袋中获得
    AddByBotGroup = 100,
    BotGiveGoldToGroup = 101,
    BotRecvGoldFromGroup = 102,
    BotHGetGoldFromGroup = 103,
    ManagerChangeGoldAcc = 200,
    ManagerChangeGoldBank = 201,
    DesktopSeatFee = 300,
    DesktopWin = 301,
    DesktopBet = 302,
    DesktopReplaceStack = 303,
    DesktopHBet = 400,
    DesktopHBetFailed = 401,
    DesktopHWin = 402,
    DesktopHLoose = 403,
    DesktopHBeBankerListAddItem = 404,-- 百人桌，本人添加到申请上庄列表
    DesktopHBeBankerListRemoveItem = 405,-- 百人桌，本人从申请上庄列表中移除
    DesktopHGetDialyBetReward = 406,-- 百人桌，每日下注奖励
    DesktopHLeave = 407,-- 百人桌，离开百人桌
    LotteryTicketBet = 500,
    LotteryTicketBetFailed = 501,
    LotteryTicketWin = 502,
    ForestPartyBet = 600,
    ForestPartyBetFailed = 601,
    ForestPartyWin = 602,
    ForestPartyBankerLose = 603,
    MatchTexasSignUp = 700,-- 扣除报名费+服务费
MatchTexasSignUpBack = 701,-- 返还报名费+服务费
}

DiamondChangeReason = {
    BuyItem = 0,
    SellItem = 1,
    BuyDiamonds = 2,
    ChangeByManager = 10,
    RecvFromMail = 20
}