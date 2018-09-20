// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BestHandNiuNiu : IComparable<BestHandNiuNiu>
    {
        //---------------------------------------------------------------------
        // When comparing or ranking cards, the suit doesn't matter
        public List<CardData> Cards { get; private set; }
        public List<CardData> RankTypeCards { get; private set; }
        public HandRankTypeNiuNiu RankType { get; private set; }

        //---------------------------------------------------------------------
        internal BestHandNiuNiu(HandRankTypeNiuNiu rankType, ICollection<CardData> cards)
        {
            if (cards.Count != 5)
            {
                //throw new ArgumentException("Cards collection should contains exactly 5 elements", nameof(cards));
            }

            this.Cards = cards.ToList();
            this.RankType = rankType;
        }

        //---------------------------------------------------------------------
        internal BestHandNiuNiu(HandRankTypeNiuNiu rankType, ICollection<CardData> cards, ICollection<CardData> rankTypeCards)
        {
            if (cards.Count != 5)
            {
                //throw new ArgumentException("Cards collection should contains exactly 5 elements", nameof(cards));
            }

            this.Cards = cards.ToList();
            this.RankTypeCards = rankTypeCards.ToList();
            this.RankType = rankType;
        }

        //---------------------------------------------------------------------
        public int CompareTo(BestHandNiuNiu other)
        {
            if (this.RankType > other.RankType)
            {
                return 1;
            }

            if (this.RankType < other.RankType)
            {
                return -1;
            }

            switch (this.RankType)
            {
                case HandRankTypeNiuNiu.WuXiaoNiu:
                case HandRankTypeNiuNiu.NiuNiu:
                case HandRankTypeNiuNiu.JinNiu:
                //case HandRankTypeNiuNiu.YinNiu:
                case HandRankTypeNiuNiu.Niu9:
                case HandRankTypeNiuNiu.Niu8:
                case HandRankTypeNiuNiu.Niu7:
                case HandRankTypeNiuNiu.Niu6:
                case HandRankTypeNiuNiu.Niu5:
                case HandRankTypeNiuNiu.Niu4:
                case HandRankTypeNiuNiu.Niu3:
                case HandRankTypeNiuNiu.Niu2:
                case HandRankTypeNiuNiu.Niu1:
                case HandRankTypeNiuNiu.HighCard:
                    return CompareTwoHandsWithHighCard(this.Cards, other.Cards);
                case HandRankTypeNiuNiu.SiZha:
                    return CompareTwoHandsWithFourOfAKind(this.Cards, other.Cards);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //---------------------------------------------------------------------
        private static int CompareTwoHandsWithHighCard(
            ICollection<CardData> firstHand,
            ICollection<CardData> secondHand)
        {
            var first_sorted_card = firstHand.OrderByDescending(x => x.type).First();
            var second_sorted_card = secondHand.OrderByDescending(x => x.type).First();
            byte first_card_type = first_sorted_card.type;
            if (first_card_type == 14)
            {
                first_card_type = 1;
            }
            byte second_card_type = second_sorted_card.type;
            if (second_card_type == 14)
            {
                second_card_type = 1;
            }
            if (first_card_type > second_card_type)
            {
                return 1;
            }

            if (first_card_type < second_card_type)
            {
                return -1;
            }

            if (first_sorted_card.suit > second_sorted_card.suit)
            {
                return 1;
            }

            if (first_sorted_card.suit < second_sorted_card.suit)
            {
                return -1;
            }

            return 0;
        }

        //---------------------------------------------------------------------
        private static int CompareTwoHandsWithFourOfAKind(
            ICollection<CardData> firstHand,
            ICollection<CardData> secondHand)
        {
            var firstFourOfAKingCard = firstHand.GroupBy(x => x.type).First(x => x.Count() == 4);
            var secondFourOfAKindCard = secondHand.GroupBy(x => x.type).First(x => x.Count() == 4);

            if (firstFourOfAKingCard.Key > secondFourOfAKindCard.Key)
            {
                return 1;
            }

            if (firstFourOfAKingCard.Key < secondFourOfAKindCard.Key)
            {
                return -1;
            }

            // Equal pair => compare high card
            return CompareTwoHandsWithHighCard(firstHand, secondHand);
        }
    }
}
