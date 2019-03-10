// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BestHandZhongFB : IComparable<BestHandZhongFB>
    {
        //---------------------------------------------------------------------
        // When comparing or ranking cards, the suit doesn't matter
        public List<CardData> Cards { get; private set; }
        public List<CardData> RankTypeCards { get; private set; }
        public HandRankTypeZhongFB RankType { get; private set; }
        const int HandCardLength = 2;

        //---------------------------------------------------------------------
        internal BestHandZhongFB(HandRankTypeZhongFB rankType, ICollection<CardData> cards)
        {
            if (cards.Count != HandCardLength)
            {
                //throw new ArgumentException("Cards collection should contains exactly 5 elements", nameof(cards));
            }

            this.Cards = cards.ToList();
            this.RankType = rankType;
        }

        //---------------------------------------------------------------------
        internal BestHandZhongFB(HandRankTypeZhongFB rankType, ICollection<CardData> cards, ICollection<CardData> rankTypeCards)
        {
            if (cards.Count != HandCardLength)
            {
                //throw new ArgumentException("Cards collection should contains exactly 5 elements", nameof(cards));
            }

            this.Cards = cards.ToList();
            this.RankTypeCards = rankTypeCards.ToList();
            this.RankType = rankType;
        }

        //---------------------------------------------------------------------
        public int CompareTo(BestHandZhongFB other)
        {
            if (this.RankType > other.RankType)
            {
                return 1;
            }

            if (this.RankType < other.RankType)
            {
                return -1;
            }

            if (this.RankType <= HandRankTypeZhongFB.Dian9)
            {
                return CompareTwoHandsWithHighCard(this.Cards, other.Cards);
            }

            switch (this.RankType)
            {
                case HandRankTypeZhongFB.BaoZi:
                case HandRankTypeZhongFB.BaoZiBai:
                case HandRankTypeZhongFB.BaoZiZhong:
                case HandRankTypeZhongFB.BaoZiFa:
                    return CompareTwoHandsWithPair(this.Cards, other.Cards);
                case HandRankTypeZhongFB.TianGang:
                    return CompareTwoHandsWithHighCard(this.Cards, other.Cards);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //---------------------------------------------------------------------
        private static int CompareTwoHandsWithHighCard(
            ICollection<CardData> firstHand,
            ICollection<CardData> secondHand)
        {
            int first_hand_value = 0;
            foreach (var i in firstHand)
            {
                MaJiangSuit mj_suit = (MaJiangSuit)i.suit;
                if (mj_suit == MaJiangSuit.Tong)
                {
                    first_hand_value += i.type;
                }
            }
            first_hand_value = first_hand_value % 10;

            int second_hand_value = 0;
            foreach (var i in secondHand)
            {
                MaJiangSuit mj_suit = (MaJiangSuit)i.suit;
                if (mj_suit == MaJiangSuit.Tong)
                {
                    second_hand_value += i.type;
                }
            }
            second_hand_value = second_hand_value % 10;

            if (first_hand_value > second_hand_value)
            {
                return 1;
            }

            if (first_hand_value < second_hand_value)
            {
                return -1;
            }

            var firstSorted = firstHand.OrderByDescending(x => x.type).ToList();
            var secondSorted = secondHand.OrderByDescending(x => x.type).ToList();
            var cardsToCompare = Math.Min(firstHand.Count, secondHand.Count);
            for (var i = 0; i < cardsToCompare; i++)
            {
                if (firstSorted[i].type > secondSorted[i].type)
                {
                    return 1;
                }

                if (firstSorted[i].type < secondSorted[i].type)
                {
                    return -1;
                }
            }

            return 0;
        }

        //---------------------------------------------------------------------
        private static int CompareTwoHandsWithPair(
            ICollection<CardData> firstHand,
            ICollection<CardData> secondHand)
        {
            var firstPairCard = firstHand.First();
            var secondPairCard = secondHand.First();

            if (firstPairCard.suit > secondPairCard.suit)
            {
                return 1;
            }

            if (firstPairCard.suit < secondPairCard.suit)
            {
                return -1;
            }

            if (firstPairCard.type > secondPairCard.type)
            {
                return 1;
            }

            if (firstPairCard.type < secondPairCard.type)
            {
                return -1;
            }

            return 0;
        }
    }
}
