// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    //-------------------------------------------------------------------------
    public enum RoundTypeTexas
    {
        PreFlop = 0,
        Flop = 1,
        Turn = 2,
        River = 3
    }

    //-------------------------------------------------------------------------
    // 操作
    public enum PlayerActionTypeTexas
    {
        None = 0,// 无操作
        Bet = 1,// 押注
        Fold = 2,// 盖牌
        Check = 3,// 让牌
        Call = 4,// 跟注
        Raise = 5,// 加注
        ReRaise = 6,// 再加注
        AllIn = 7// 全下
    }

    //-------------------------------------------------------------------------
    // 托管操作
    public enum PlayerAutoActionTypeTexas
    {
        None = 0,// 无操作
        Fold = 1,// 盖牌
        CheckFold = 2,// 让牌，盖牌
        Check = 3,// 让牌
        CallAny = 4,// 任意跟注
    }

    //-------------------------------------------------------------------------
    // 可进行操作
    public enum PlayerCanActionTypeTexas
    {
        None = 0,// 无操作
        Fold = 1,// 盖牌
        CheckFold = 2,// 让牌，盖牌
        Check = 3,// 让牌
        Call = 4,// 跟注
        Raise = 5,// 加注
    }

    //-------------------------------------------------------------------------
    public enum HandRankTypeTexas
    {
        None = 0,
        HighCard = 1000,
        Pair = 2000,
        TwoPairs = 3000,
        ThreeOfAKind = 4000,
        Straight = 5000,
        Flush = 6000,
        FullHouse = 7000,
        FourOfAKind = 8000,
        StraightFlush = 9000
    }

    //-------------------------------------------------------------------------
    public enum HandRankTypeTexasH
    {
        HighCard = 0,
        Pair,
        TwoPairs,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush,
    }
}
