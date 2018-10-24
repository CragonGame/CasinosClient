// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;

    // Class containing helper methods for evaluating and comparing player's hands.
    public static class CardTypeHelperGFlower
    {
        //---------------------------------------------------------------------
        private static readonly IHandEvaluatorGFlower HandEvaluator = new HandEvaluatorGFlower();

        //---------------------------------------------------------------------
        // Finds the best possible hand given a player's cards and all revealed comunity cards.
        // <param name="cards">A player's cards + all revealed comunity cards</param>
        // <returns>Returns value of HandRankType. For example Straight, Flush, etc</returns>
        public static HandRankTypeGFlower GetHandRank(ICollection<Card> cards)
        {
            return HandEvaluator.GetBestHand(cards).RankType;
        }

        //---------------------------------------------------------------------
        public static BestHandGFlower GetBestHand(ICollection<Card> cards)
        {
            return HandEvaluator.GetBestHand(cards);
        }

        //---------------------------------------------------------------------
        // 百人桌牌型
        public static HandRankTypeGFlowerH GetHandRankHGFlower(List<Card> cards)
        {
            HandRankTypeGFlowerH rank_type = HandRankTypeGFlowerH.HighCard;
            var hand_type = HandEvaluator.GetBestHand(cards).RankType;
            if (hand_type == HandRankTypeGFlower.BaoZi)
            {
                var card = cards[0];
                if (card.Type == (byte)CardType.Ace)
                {
                    rank_type = HandRankTypeGFlowerH.RoyalBaoZi;
                }
                else
                {
                    rank_type = HandRankTypeGFlowerH.BaoZi;
                }
            }
            else
            {
                foreach (HandRankTypeGFlowerH i in Enum.GetValues(typeof(HandRankTypeGFlowerH)))
                {
                    HandRankTypeGFlowerH type = i;
                    if (type.ToString().Equals(hand_type.ToString()))
                    {
                        rank_type = type;
                        break;
                    }
                }
            }

            return rank_type;
        }

        //---------------------------------------------------------------------
        // Compares the cards of two opponents to see which one can make the strongest hand with the community cards.
        // At least 5 cards are needed. Can be used during and after the Flop round.
        // <param name="firstPlayerCards">First player cards + all revealed comunity cards</param>
        // <param name="secondPlayerCards">Second player cards + all revealed comunity cards</param>
        // <returns>Comparison result as int</returns>
        public static int CompareCards(IEnumerable<Card> firstPlayerCards, IEnumerable<Card> secondPlayerCards)
        {
            var firstPlayerBestHand = HandEvaluator.GetBestHand(firstPlayerCards);
            var secondPlayerBestHand = HandEvaluator.GetBestHand(secondPlayerCards);
            return firstPlayerBestHand.CompareTo(secondPlayerBestHand);
        }
    }
}
