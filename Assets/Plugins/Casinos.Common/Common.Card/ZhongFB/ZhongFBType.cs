// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    //-------------------------------------------------------------------------
    public enum MaJiangType
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        DongFeng = 10,
        NanFeng = 11,
        XiFeng = 12,
        BeiFeng = 13,
        Bai = 14,
        Zhong = 15,
        Fa = 16,
    }

    //-------------------------------------------------------------------------
    public enum MaJiangSuit : byte
    {
        Wan = 0,// 万
        Tong = 1,// 饼
        Tiao = 2,// 条
        DongFeng = 3,// 东风
        NanFeng = 4,// 南风
        XiFeng = 5,// 西风
        BeiFeng = 6,// 北风            
        Bai = 7,// 白板
        Zhong = 8,// 红中    
        Fa = 9,// 发财
    }

    //-------------------------------------------------------------------------
    public enum HandRankTypeZhongFB
    {
        Dian0 = 0,
        Dian1,
        Dian2,
        Dian3,
        Dian4,
        Dian5,
        Dian6,
        Dian7,
        Dian8,
        Dian9,
        BaoZi,
        BaoZiBai,
        BaoZiZhong,
        BaoZiFa,
        TianGang,
    }
}