// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IHandEvaluatorZhongFB
    {
        BestHandZhongFB GetBestHand(IEnumerable<Card> cards);
    }

    // For performance considerations this class is not implemented using Chain of Responsibility
    public class HandEvaluatorZhongFB : IHandEvaluatorZhongFB
    {
        //---------------------------------------------------------------------
        private const int ComparableCards = 2;

        //---------------------------------------------------------------------        
        public HandEvaluatorZhongFB()
        {
        }

        //---------------------------------------------------------------------
        // <summary>
        // Finds the best possible hand given a player's cards and all revealed comunity cards.
        // </summary>
        // <param name="cards">A player's cards + all revealed comunity cards (at lesat 5 in total)</param>
        // <returns>Returns an object of type BestHandZhongFB</returns>
        public BestHandZhongFB GetBestHand(IEnumerable<Card> cards)
        {
            var cardTypeMap = new Dictionary<MaJiangType, List<Card>>();
            foreach (var card in cards)
            {
                List<Card> listTypeCard = null;
                cardTypeMap.TryGetValue((MaJiangType)card.Type, out listTypeCard);
                if (listTypeCard == null)
                {
                    listTypeCard = new List<Card>();
                }

                listTypeCard.Add(card);
                cardTypeMap[(MaJiangType)card.Type] = listTypeCard;
            }

            var pairTypes = this.GetTypesWithNCards(cardTypeMap, 2);
            // Pair
            if (pairTypes.Count == 2)
            {
                var bestCards = new List<CardData>();
                bestCards.AddRange(pairTypes);
                CardData card_data_first = bestCards[0];
                HandRankTypeZhongFB rank_type = HandRankTypeZhongFB.BaoZi;
                if (card_data_first.suit == (byte)MaJiangSuit.Bai)
                {
                    rank_type = HandRankTypeZhongFB.BaoZiBai;
                }
                else if (card_data_first.suit == (byte)MaJiangSuit.Zhong)
                {
                    rank_type = HandRankTypeZhongFB.BaoZiZhong;
                }
                else if (card_data_first.suit == (byte)MaJiangSuit.Fa)
                {
                    rank_type = HandRankTypeZhongFB.BaoZiFa;
                }
                return new BestHandZhongFB(rank_type, bestCards, pairTypes);
            }
            else
            {
                // High card          
                int cards_count = cards.Count();
                int take_count = (cards_count >= ComparableCards) ? ComparableCards : cards_count;

                var bestCards = cards.Select(x => x.GetCardData()).OrderByDescending(x => x.type).Take(take_count).ToList();
                bool exists_2_bing = bestCards.Exists(x => x.type == (byte)MaJiangType.Two && x.suit == (byte)MaJiangSuit.Tong);
                bool exists_8_bing = bestCards.Exists(x => x.type == (byte)MaJiangType.Eight && x.suit == (byte)MaJiangSuit.Tong);

                HandRankTypeZhongFB rank_type = HandRankTypeZhongFB.Dian0;
                if (exists_2_bing && exists_8_bing)
                {
                    rank_type = HandRankTypeZhongFB.TianGang;
                }
                else
                {
                    var hand_value = 0;
                    foreach (var i in cards)
                    {
                        MaJiangSuit mj_suit = (MaJiangSuit)i.Suit;
                        if (mj_suit == MaJiangSuit.Tong)
                        {
                            hand_value += i.Type;
                        }
                    }

                    var dian = hand_value % 10;
                    foreach (var i in Enum.GetValues(typeof(HandRankTypeZhongFB)))
                    {
                        HandRankTypeZhongFB t = (HandRankTypeZhongFB)i;
                        if ((int)t == dian)
                        {
                            rank_type = t;
                            break;
                        }
                    }
                }

                return new BestHandZhongFB(rank_type, bestCards, bestCards);
            }
        }

        //---------------------------------------------------------------------
        //private IList<CardData> GetTypesWithNCards(int[] cardTypeCounts, int n) Old style of writing!
        private IList<CardData> GetTypesWithNCards(Dictionary<MaJiangType, List<Card>> mapCardType, int n)
        {
            var pairs = new List<CardData>();
            foreach (var i in mapCardType)
            {
                if (i.Value.Count == n)
                {
                    var nCardsList = i.Value.Select(x => x.GetCardData());
                    pairs.AddRange(nCardsList);
                }
            }

            pairs = pairs.OrderByDescending(x => x.type).ToList();
            //for (var i = cardTypeCounts.Length - 1; i >= 0; i--)
            //{
            //    if (cardTypeCounts[i] == n)
            //    {
            //        pairs.Add((MaJiangType)i);
            //    }
            //}

            return pairs;
        }
    }
}
