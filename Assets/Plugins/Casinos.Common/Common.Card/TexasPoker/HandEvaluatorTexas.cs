// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IHandEvaluator
    {
        BestHandTexas GetBestHand(IEnumerable<Card> cards);
    }

    // For performance considerations this class is not implemented using Chain of Responsibility
    public class HandEvaluatorTexas : IHandEvaluator
    {
        //---------------------------------------------------------------------
        private const int ComparableCards = 5;
        private int CardTypeCounts;
        private int CardSuitCousts;

        //---------------------------------------------------------------------        
        public HandEvaluatorTexas()
        {
            CardTypeCounts = (int)CardType.Ace + 1;
            CardSuitCousts = (int)CardSuit.Spade + 1;
        }

        //---------------------------------------------------------------------
        // <summary>
        // Finds the best possible hand given a player's cards and all revealed comunity cards.
        // </summary>
        // <param name="cards">A player's cards + all revealed comunity cards (at lesat 5 in total)</param>
        // <returns>Returns an object of type BestHand</returns>
        public BestHandTexas GetBestHand(IEnumerable<Card> cards)
        {
            var cardSuitCounts = new int[CardSuitCousts];
            var cardTypeMap = new Dictionary<CardType, List<Card>>();
            foreach (var card in cards)
            {
                cardSuitCounts[(int)card.Suit]++;
                //cardTypeCounts[(int)card.Type]++;
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

            // Flushes
            if (cardSuitCounts.Any(x => x >= ComparableCards))
            {
                // Straight flush
                var straightFlushCards = this.GetStraightFlushCards(cardSuitCounts, cards);
                if (straightFlushCards.Count > 0)
                {
                    return new BestHandTexas(HandRankTypeTexas.StraightFlush, straightFlushCards, straightFlushCards);
                }

                // Flush - it is not possible to have Flush and either Four of a kind or Full house at the same time
                for (var i = 0; i < cardSuitCounts.Length; i++)
                {
                    if (cardSuitCounts[i] >= ComparableCards)
                    {
                        var flushCards =
                            cards.Where(x => x.Suit == i)
                                .Select(x => x.GetCardData())
                                .OrderByDescending(x => x.type)
                                .Take(ComparableCards)
                                .ToList();
                        return new BestHandTexas(HandRankTypeTexas.Flush, flushCards, flushCards);
                    }
                }
            }


            // Four of a kind            
            if (cardTypeMap.Any(x => x.Value.Count == 4))
            {
                var bestFourOfAKind = this.GetTypesWithNCards(cardTypeMap, 4).ToList();
                var bestCards = new List<CardData>();
                bestCards.AddRange(bestFourOfAKind);
                bestCards.Add(cards.Where(x => !bestFourOfAKind.Exists((CardData best_hand) => { return best_hand.type == x.Type; })).Max(x => x.GetCardData()));

                return new BestHandTexas(HandRankTypeTexas.FourOfAKind, bestCards, bestFourOfAKind);
            }

            // Full
            var pairTypes = this.GetTypesWithNCards(cardTypeMap, 2);
            var threeOfAKindTypes = this.GetTypesWithNCards(cardTypeMap, 3);
            if ((pairTypes.Count > 0 && threeOfAKindTypes.Count > 0) || threeOfAKindTypes.Count > 3)
            {
                var bestCards = new List<CardData>();
                for (var i = 0; i < 3; i++)
                {
                    bestCards.Add(threeOfAKindTypes[i]);
                }

                if (threeOfAKindTypes.Count > 3)
                {
                    for (var i = 3; i < 5; i++)
                    {
                        bestCards.Add(threeOfAKindTypes[i]);
                    }
                }

                if (pairTypes.Count > 0)
                {
                    for (var i = 0; i < 2; i++)
                    {
                        bestCards.Add(pairTypes[i]);
                    }
                }

                return new BestHandTexas(HandRankTypeTexas.FullHouse, bestCards, bestCards);
            }

            // Straight            
            var straightCardsMap = this.GetStraightCards(cardTypeMap);
            if (straightCardsMap != null)
            {
                var straightCardsList = new List<CardData>();

                foreach (var bestCard in straightCardsMap)
                {
                    straightCardsList.Add(bestCard.Value[0].GetCardData());
                }

                return new BestHandTexas(HandRankTypeTexas.Straight, straightCardsList, straightCardsList);
            }

            // 3 of a kind
            if (threeOfAKindTypes.Count > 0)
            {
                var bestThreeOfAKindType = threeOfAKindTypes[0];
                var bestCards =
                    cards.Where(x => x.Type != bestThreeOfAKindType.type)
                        .Select(x => x.GetCardData())
                        .OrderByDescending(x => x.type)
                        .Take(ComparableCards - 3).ToList();
                bestCards.AddRange(threeOfAKindTypes);

                return new BestHandTexas(HandRankTypeTexas.ThreeOfAKind, bestCards, threeOfAKindTypes);
            }

            // Two pairs
            if (pairTypes.Count >= 4)
            {
                var bestCards = new List<CardData>();
                var bestCardsMiddle = new List<CardData>();
                for (int i = 0; i < 4; i++)
                {
                    bestCardsMiddle.Add(pairTypes[i]);
                }
                bestCards.AddRange(bestCardsMiddle);
                bestCards.AddRange(cards.Where(x => !bestCardsMiddle.Exists((CardData best_pair) => { return x.GetCardData().type == best_pair.type; })).
                    OrderByDescending(x => x.Type).Select(x => x.GetCardData()).Take(1).ToList());

                return new BestHandTexas(HandRankTypeTexas.TwoPairs, bestCards, bestCardsMiddle);
            }

            // Pair
            if (pairTypes.Count == 2)
            {
                var bestCards = new List<CardData>();
                bestCards.AddRange(pairTypes);
                bestCards.AddRange(cards.Where(x => x.Type != pairTypes[0].type).
                    OrderByDescending(x => x.Type).Select(x => x.GetCardData()).Take(3).ToList());

                return new BestHandTexas(HandRankTypeTexas.Pair, bestCards, pairTypes);
            }
            else
            {
                int cards_count = cards.Count();
                int take_count = (cards_count >= 5) ? 5 : cards_count;

                var bestCards = cards.Select(x => x.GetCardData()).OrderByDescending(x => x.type).Take(take_count).ToList();

                return new BestHandTexas(HandRankTypeTexas.HighCard, bestCards, bestCards);
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
        private ICollection<CardData> GetStraightFlushCards(int[] cardSuitCounts, IEnumerable<Card> cards)
        {
            var straightFlushCardTypes = new List<CardData>();
            for (var i = 0; i < cardSuitCounts.Length; i++)
            {
                if (cardSuitCounts[i] < ComparableCards)
                {
                    continue;
                }

                var cardTypeCountsMap = new Dictionary<CardType, List<Card>>();
                List<Card> listSameSuitCard = null;

                foreach (var card in cards)
                {
                    if (card.Suit == i)
                    {
                        CardType card_type = (CardType)card.Type;
                        cardTypeCountsMap.TryGetValue(card_type, out listSameSuitCard);
                        if (listSameSuitCard == null)
                        {
                            listSameSuitCard = new List<Card>();
                        }
                        listSameSuitCard.Add(card);
                        cardTypeCountsMap[card_type] = listSameSuitCard;
                    }
                }

                var bestStraight = this.GetStraightCards(cardTypeCountsMap);
                if (bestStraight != null)
                {
                    foreach (var bestCard in bestStraight)
                    {
                        straightFlushCardTypes.Add(bestCard.Value[0].GetCardData());
                    }
                }
            }

            return straightFlushCardTypes;
        }

        //---------------------------------------------------------------------
        private Dictionary<CardType, List<Card>> GetStraightCards(Dictionary<CardType, List<Card>> cardTypeCountsMap)
        {
            var lastCardType = CardTypeCounts;
            var straightLength = 0;
            for (var i = lastCardType - 1; i >= 1; i--)
            {
                bool has_ace = false;
                if (i == 1)
                {
                    List<Card> list_ace = null;
                    cardTypeCountsMap.TryGetValue(CardType.Ace, out list_ace);

                    if (list_ace != null && list_ace.Count > 0)
                    {
                        has_ace = true;
                    }
                }

                var hasCardsOfType = cardTypeCountsMap.ContainsKey((CardType)i) || has_ace;

                if (hasCardsOfType && i == lastCardType - 1)
                {
                    straightLength++;
                    if (straightLength == ComparableCards)
                    {
                        var bestStraight = new Dictionary<CardType, List<Card>>();
                        for (var j = i; j <= i + ComparableCards - 1; j++)
                        {
                            List<Card> listCard = null;

                            cardTypeCountsMap.TryGetValue((CardType)j, out listCard);
                            if (j == 1)
                            {
                                cardTypeCountsMap.TryGetValue(CardType.Ace, out listCard);
                                if (listCard != null)
                                {
                                    bestStraight[CardType.Ace] = listCard;
                                }
                            }
                            else
                            {
                                if (listCard != null)
                                {
                                    bestStraight[(CardType)j] = listCard;
                                }
                            }
                        }

                        return bestStraight;
                    }
                }
                else
                {
                    straightLength = 0;
                }

                lastCardType = i;
            }

            return null;
        }
    }
}