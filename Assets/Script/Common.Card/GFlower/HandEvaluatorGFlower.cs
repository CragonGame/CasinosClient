// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IHandEvaluatorGFlower
    {
        BestHandGFlower GetBestHand(IEnumerable<Card> cards);
    }

    // For performance considerations this class is not implemented using Chain of Responsibility
    public class HandEvaluatorGFlower : IHandEvaluatorGFlower
    {
        //---------------------------------------------------------------------
        private const int ComparableCards = 3;
        private int CardSuitCousts;

        //---------------------------------------------------------------------        
        public HandEvaluatorGFlower()
        {
            CardSuitCousts = (int)CardSuit.Spade + 1;
        }

        //---------------------------------------------------------------------
        // <summary>
        // Finds the best possible hand given a player's cards and all revealed comunity cards.
        // </summary>
        // <param name="cards">A player's cards + all revealed comunity cards (at lesat 5 in total)</param>
        // <returns>Returns an object of type BestHandGFlower</returns>
        public BestHandGFlower GetBestHand(IEnumerable<Card> cards)
        {
            var cardSuitCounts = new int[CardSuitCousts];
            var cardTypeMap = new Dictionary<CardType, List<Card>>();
            foreach (var card in cards)
            {
                cardSuitCounts[(int)card.Suit]++;
                CardType card_type = (CardType)card.Type;
                List<Card> listTypeCard = null;
                cardTypeMap.TryGetValue(card_type, out listTypeCard);
                if (listTypeCard == null)
                {
                    listTypeCard = new List<Card>();
                }

                listTypeCard.Add(card);
                cardTypeMap[card_type] = listTypeCard;
            }

            List<CardData> list_card = cards.OrderByDescending(x => x.Type).Select(x => x.GetCardData()).ToList();
            // Flushes
            if (cardSuitCounts.Any(x => x >= ComparableCards))
            {
                // Straight flush
                bool is_straight = this.IsStraightCards(cards.ToList(), cardTypeMap);
                if (is_straight)
                {
                    return new BestHandGFlower(HandRankTypeGFlower.StraightFlush, list_card, list_card);
                }

                return new BestHandGFlower(HandRankTypeGFlower.Flush, list_card, list_card);
            }

            {
                // Straight            
                bool is_straight = this.IsStraightCards(cards.ToList(), cardTypeMap);
                if (is_straight)
                {
                    return new BestHandGFlower(HandRankTypeGFlower.Straight, list_card, list_card);
                }
            }

            // 3 of a kind
            var threeOfAKindTypes = this.GetTypesWithNCards(cardTypeMap, 3);
            if (threeOfAKindTypes.Count > 0)
            {
                var bestThreeOfAKindType = threeOfAKindTypes[0];
                var bestCards =
                    cards.Where(x => x.Type != bestThreeOfAKindType.type)
                        .Select(x => x.GetCardData())
                        .OrderByDescending(x => x.type)
                        .Take(ComparableCards - 3).ToList();
                bestCards.AddRange(threeOfAKindTypes);
                HandRankTypeGFlower type = HandRankTypeGFlower.BaoZi;
                //if (bestThreeOfAKindType.type == (byte)CardType.Ace)
                //{
                //    type = HandRankTypeGFlowerH.RoyalBaoZi;
                //}

                return new BestHandGFlower(type, bestCards, threeOfAKindTypes);
            }

            // Pair
            var pairTypes = this.GetTypesWithNCards(cardTypeMap, 2);
            if (pairTypes.Count == 2)
            {
                var bestCards = new List<CardData>();
                bestCards.AddRange(pairTypes);
                bestCards.AddRange(cards.Where(x => x.Type != pairTypes[0].type).
                    OrderByDescending(x => x.Type).Select(x => x.GetCardData()).Take(3).ToList());

                return new BestHandGFlower(HandRankTypeGFlower.Pair, bestCards, pairTypes);
            }
            else
            {
                // High card
                var bestCards = cards.Select(x => x.GetCardData()).OrderByDescending(x => x.type).Take(3).ToList();

                return new BestHandGFlower(HandRankTypeGFlower.HighCard, bestCards, bestCards);
            }
        }

        //---------------------------------------------------------------------        
        private IList<CardData> GetTypesWithNCards(Dictionary<CardType, List<Card>> mapCardType, int n)
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

            return pairs;
        }

        //---------------------------------------------------------------------
        private bool IsStraightCards(List<Card> list_card, Dictionary<CardType, List<Card>> cardTypeCountsMap)
        {
            list_card.Sort((x, y) =>
            {
                return -x.Type.CompareTo(y.Type);
            });

            bool is_straight = true;
            byte compare_type = list_card[0].Type;
            for (int i = 1; i < list_card.Count; i++)
            {
                Card card = list_card[i];
                if (compare_type - card.Type == 1)
                {
                    compare_type = card.Type;
                }
                else
                {
                    is_straight = false;
                }
            }

            bool has_ace = false;
            List<Card> list_ace = null;
            cardTypeCountsMap.TryGetValue(CardType.Ace, out list_ace);
            if (list_ace != null && list_ace.Count > 0)
            {
                has_ace = true;
            }

            if (!is_straight && has_ace)
            {
                compare_type = 1;
                is_straight = true;
                for (int i = list_card.Count - 1; i >= 1; i--)
                {
                    Card card = list_card[i];
                    if (card.Type - compare_type == 1)
                    {
                        compare_type = card.Type;
                    }
                    else
                    {
                        is_straight = false;
                    }
                }
            }

            return is_straight;
        }
    }
}
