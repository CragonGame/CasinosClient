// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;

    // Class containing helper methods for evaluating and comparing player's hands.
    public static class CardTypeHelperTexas
    {
        //---------------------------------------------------------------------
        private static readonly IHandEvaluator HandEvaluator = new HandEvaluatorTexas();

        //---------------------------------------------------------------------
        // Finds the best possible hand given a player's cards and all revealed comunity cards.
        // <param name="cards">A player's cards + all revealed comunity cards</param>
        // <returns>Returns value of HandRankType. For example Straight, Flush, etc</returns>
        public static HandRankTypeTexas GetHandRank(ICollection<Card> cards)
        {
            return HandEvaluator.GetBestHand(cards).RankType;
        }

        //---------------------------------------------------------------------
        public static BestHandTexas GetBestHand(ICollection<Card> cards)
        {
            return HandEvaluator.GetBestHand(cards);
        }

        //---------------------------------------------------------------------
        // 百人桌牌型
        public static HandRankTypeTexasH GetHandRankHTexas(List<Card> cards)
        {
            HandRankTypeTexasH rank_type = HandRankTypeTexasH.HighCard;
            var hand_type = HandEvaluator.GetBestHand(cards).RankType;
            if (hand_type == HandRankTypeTexas.StraightFlush)
            {
                cards.Sort((card1, card2) =>
                {
                    return -((int)card1.Type).CompareTo((int)card2.Type);
                });

                if (cards[0].Type == (byte)CardType.Ace && cards[1].Type == (byte)CardType.King)
                {
                    rank_type = HandRankTypeTexasH.RoyalFlush;
                }
                else
                {
                    rank_type = HandRankTypeTexasH.StraightFlush;
                }
            }
            else
            {
                foreach (HandRankTypeTexasH i in Enum.GetValues(typeof(HandRankTypeTexasH)))
                {
                    HandRankTypeTexasH type = i;
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
