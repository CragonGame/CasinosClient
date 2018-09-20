// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BestHandGFlower : IComparable<BestHandGFlower>
    {
        //---------------------------------------------------------------------
        // When comparing or ranking cards, the suit doesn't matter
        public List<CardData> Cards { get; private set; }
        public List<CardData> RankTypeCards { get; private set; }
        public HandRankTypeGFlower RankType { get; private set; }
        const int HandCardLength = 3;

        //---------------------------------------------------------------------
        internal BestHandGFlower(HandRankTypeGFlower rankType, ICollection<CardData> cards)
        {
            if (cards.Count != HandCardLength)
            {
                //throw new ArgumentException("Cards collection should contains exactly 5 elements", nameof(cards));
            }

            this.Cards = cards.ToList();
            this.RankType = rankType;
        }

        //---------------------------------------------------------------------
        internal BestHandGFlower(HandRankTypeGFlower rankType, ICollection<CardData> cards, ICollection<CardData> rankTypeCards)
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
        public int CompareTo(BestHandGFlower other)
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
                case HandRankTypeGFlower.HighCard:
                    return CompareTwoHandsWithHighCard(this.Cards, other.Cards);
                case HandRankTypeGFlower.Pair:
                    return CompareTwoHandsWithPair(this.Cards, other.Cards);
                case HandRankTypeGFlower.Straight:
                    return CompareTwoHandsWithStraight(this.Cards, other.Cards);
                case HandRankTypeGFlower.Flush:
                    return CompareTwoHandsWithHighCard(this.Cards, other.Cards);
                case HandRankTypeGFlower.StraightFlush:
                    return CompareTwoHandsWithStraight(this.Cards, other.Cards);
                case HandRankTypeGFlower.BaoZi:
                    return CompareTwoHandsWithBaoZi(this.Cards, other.Cards);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //---------------------------------------------------------------------
        private static int CompareTwoHandsWithHighCard(
            ICollection<CardData> firstHand,
            ICollection<CardData> secondHand)
        {
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
            var firstPairCard = firstHand.GroupBy(x => x.type).First(x => x.Count() >= 2);
            var secondPairCard = secondHand.GroupBy(x => x.type).First(x => x.Count() >= 2);

            if (firstPairCard.Key > secondPairCard.Key)
            {
                return 1;
            }

            if (firstPairCard.Key < secondPairCard.Key)
            {
                return -1;
            }

            // Equal pair => compare high card
            return CompareTwoHandsWithHighCard(firstHand, secondHand);
        }

        //---------------------------------------------------------------------
        private static int CompareTwoHandsWithBaoZi(
            ICollection<CardData> firstHand,
            ICollection<CardData> secondHand)
        {
            var firstThreeOfAKindCard = firstHand.GroupBy(x => x.type).Where(x => x.Count() == 3).OrderByDescending(x => x.Key).FirstOrDefault();
            var secondThreeOfAKindCard = secondHand.GroupBy(x => x.type).Where(x => x.Count() == 3).OrderByDescending(x => x.Key).FirstOrDefault();
            if (firstThreeOfAKindCard.Key > secondThreeOfAKindCard.Key)
            {
                return 1;
            }

            if (secondThreeOfAKindCard.Key > firstThreeOfAKindCard.Key)
            {
                return -1;
            }

            // Equal triples => compare high card
            return CompareTwoHandsWithHighCard(firstHand, secondHand);
        }

        //---------------------------------------------------------------------
        private static int CompareTwoHandsWithStraight(
            ICollection<CardData> firstHand,
            ICollection<CardData> secondHand)
        {
            var firstBiggestCard = firstHand.Max();
            var firstBiggetsCardType = firstBiggestCard.type;
            var firstHandType = firstHand.Select(x => x.type);
            if (firstBiggetsCardType == (byte)CardType.Ace && firstHandType.Contains((byte)CardType.Three))
            {
                firstBiggetsCardType = (byte)CardType.Three;
            }

            var secondBiggestCard = secondHand.Max();
            var secondBiggestCartType = secondBiggestCard.type;
            var secondHandType = secondHand.Select(x => x.type);
            if (secondBiggestCartType == (byte)CardType.Ace && secondHandType.Contains((byte)CardType.Three))
            {
                secondBiggestCartType = (byte)CardType.Three;
            }

            return firstBiggetsCardType.CompareTo(secondBiggestCartType);
        }
    }
}
