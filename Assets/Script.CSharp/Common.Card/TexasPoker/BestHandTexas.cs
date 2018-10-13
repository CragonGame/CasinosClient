// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BestHandTexas : IComparable<BestHandTexas>
    {
        //---------------------------------------------------------------------
        // When comparing or ranking cards, the suit doesn't matter
        public List<CardData> Cards { get; private set; }
        public List<CardData> RankTypeCards { get; private set; }
        public HandRankTypeTexas RankType { get; private set; }

        //---------------------------------------------------------------------
        internal BestHandTexas(HandRankTypeTexas rankType, ICollection<CardData> cards)
        {
            if (cards.Count != 5)
            {
                //throw new ArgumentException("Cards collection should contains exactly 5 elements", nameof(cards));
            }

            this.Cards = cards.ToList();
            this.RankType = rankType;
        }

        //---------------------------------------------------------------------
        internal BestHandTexas(HandRankTypeTexas rankType, ICollection<CardData> cards, ICollection<CardData> rankTypeCards)
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
        public int CompareTo(BestHandTexas other)
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
                case HandRankTypeTexas.HighCard:
                    return CompareTwoHandsWithHighCard(this.Cards, other.Cards);
                case HandRankTypeTexas.Pair:
                    return CompareTwoHandsWithPair(this.Cards, other.Cards);
                case HandRankTypeTexas.TwoPairs:
                    return CompareTwoHandsWithTwoPairs(this.Cards, other.Cards);
                case HandRankTypeTexas.ThreeOfAKind:
                    return CompareTwoHandsWithThreeOfAKind(this.Cards, other.Cards);
                case HandRankTypeTexas.Straight:
                    return CompareTwoHandsWithStraight(this.Cards, other.Cards);
                case HandRankTypeTexas.Flush:
                    return CompareTwoHandsWithHighCard(this.Cards, other.Cards);
                case HandRankTypeTexas.FullHouse:
                    return CompareTwoHandsWithFullHouse(this.Cards, other.Cards);
                case HandRankTypeTexas.FourOfAKind:
                    return CompareTwoHandsWithFourOfAKind(this.Cards, other.Cards);
                case HandRankTypeTexas.StraightFlush:
                    return CompareTwoHandsWithStraight(this.Cards, other.Cards);
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
        private static int CompareTwoHandsWithTwoPairs(
            ICollection<CardData> firstHand,
            ICollection<CardData> secondHand)
        {
            var firstPairCard = firstHand.GroupBy(x => x.type).Where(x => x.Count() == 2).OrderByDescending(x => x.Key).ToList();
            var secondPairCard = secondHand.GroupBy(x => x.type).Where(x => x.Count() == 2).OrderByDescending(x => x.Key).ToList();

            for (int i = 0; i < firstPairCard.Count; i++)
            {
                if ((int)firstPairCard[i].Key > (int)secondPairCard[i].Key)
                {
                    return 1;
                }

                if ((int)secondPairCard[i].Key > (int)firstPairCard[i].Key)
                {
                    return -1;
                }
            }

            // Equal pairs => compare high card
            return CompareTwoHandsWithHighCard(firstHand, secondHand);
        }

        //---------------------------------------------------------------------
        private static int CompareTwoHandsWithThreeOfAKind(
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
            if (firstBiggetsCardType == (byte)CardType.Ace && firstHandType.Contains((byte)CardType.Five))
            {
                firstBiggetsCardType = (byte)CardType.Five;
            }

            var secondBiggestCard = secondHand.Max();
            var secondBiggestCartType = secondBiggestCard.type;
            var secondHandType = secondHand.Select(x => x.type);
            if (secondBiggestCartType == (byte)CardType.Ace && secondHandType.Contains((byte)CardType.Five))
            {
                secondBiggestCartType = (byte)CardType.Five;
            }

            return firstBiggetsCardType.CompareTo(secondBiggestCartType);
        }

        //---------------------------------------------------------------------
        private static int CompareTwoHandsWithFullHouse(
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

            var firstPairType = firstHand.GroupBy(x => x.type).First(x => x.Count() == 2);
            var secondPairType = secondHand.GroupBy(x => x.type).First(x => x.Count() == 2);
            return firstPairType.Key.CompareTo(secondPairType.Key);
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
