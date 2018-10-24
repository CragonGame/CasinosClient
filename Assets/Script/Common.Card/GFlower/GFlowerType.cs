// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    //-------------------------------------------------------------------------
    public enum HandRankTypeGFlowerH
    {
        HighCard = 0,
        Pair,
        Straight,
        Flush,
        StraightFlush,
        BaoZi,
        RoyalBaoZi,
    }

    //-------------------------------------------------------------------------
    public enum HandRankTypeGFlower
    {
        HighCard = 0,
        Pair,
        Straight,
        Flush,
        StraightFlush,
        BaoZi,
    }

    //-------------------------------------------------------------------------
    // ²Ù×÷
    public enum PlayerActionTypeGFlower
    {
        None = 0,// ÎÞ²Ù×÷   
        Bet = 1,// ÏÂµ××¢
        Fold = 2,// ÆúÅÆ
        SeeCard = 3, // ¿´ÅÆ
        Call = 4,// ¸ú×¢
        Raise = 5,// ¼Ó×¢   
        FireRaise = 6,// »ðÆ´¼Ó×¢
        PKCard = 7// ±ÈÅÆ        
    }
}