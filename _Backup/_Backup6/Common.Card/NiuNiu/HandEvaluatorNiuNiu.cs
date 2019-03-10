// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IHandEvaluatorNiuNiu
    {
        BestHandNiuNiu GetBestHand(IEnumerable<Card> cards);
    }

    // For performance considerations this class is not implemented using Chain of Responsibility
    public class HandEvaluatorNiuNiu : IHandEvaluatorNiuNiu
    {
        //---------------------------------------------------------------------
        private const int ComparableCards = 5;

        //---------------------------------------------------------------------        
        public HandEvaluatorNiuNiu()
        {
        }

        //---------------------------------------------------------------------
        // <summary>
        // Finds the best possible hand given a player's cards and all revealed comunity cards.
        // </summary>
        // <param name="cards">A player's cards + all revealed comunity cards (at lesat 5 in total)</param>
        // <returns>Returns an object of type BestHand</returns>
        public BestHandNiuNiu GetBestHand(IEnumerable<Card> cards)
        {
            var all_cards_sum_value = cards.Sum(x => x.Type > 10 ? 10 : x.Type);
            if (all_cards_sum_value <= 10 && !cards.Any(x => x.Type >= 5))
            {
                var card_datas = cards.Select(x => x.GetCardData()).ToList();
                return new BestHandNiuNiu(HandRankTypeNiuNiu.WuXiaoNiu, card_datas);
            }

            var cardTypeMap = new Dictionary<CardTypeNiuNiu, List<Card>>();
            foreach (var card in cards)
            {
                List<Card> listTypeCard = null;
                cardTypeMap.TryGetValue((CardTypeNiuNiu)card.Type, out listTypeCard);
                if (listTypeCard == null)
                {
                    listTypeCard = new List<Card>();
                }

                listTypeCard.Add(card);
                cardTypeMap[(CardTypeNiuNiu)card.Type] = listTypeCard;
            }

            // Four of a kind            
            if (cardTypeMap.Any(x => x.Value.Count == 4))
            {
                var bestFourOfAKind = this.GetTypesWithNCards(cardTypeMap, 4).ToList();
                var best_cards = new List<CardData>();
                best_cards.AddRange(bestFourOfAKind);
                best_cards.Add(cards.Where(x => !bestFourOfAKind.Exists((CardData best_hand) => { return best_hand.type == x.Type; })).Max(x => x.GetCardData()));

                return new BestHandNiuNiu(HandRankTypeNiuNiu.SiZha, best_cards, bestFourOfAKind);
            }

            if (!cards.Any(x => x.Type <= 10))
            {
                HandRankTypeNiuNiu rank_type = HandRankTypeNiuNiu.JinNiu;
                //if (cards.Any(x => x.Type == 10))
                //{
                //    rank_type = HandRankTypeNiuNiu.YinNiu;
                //}

                var best_cards = cards.Select(x => x.GetCardData()).OrderByDescending(x => x.type).ToList();
                return new BestHandNiuNiu(rank_type, best_cards);
            }

            var have_niu = false;
            var cards_ex = cards.ToList();
            for (var i = 0; i <= 2; i++)
            {
                for (var j = i + 1; j <= 3; j++)
                {
                    for (var k = j + 1; k <= 4; k++)
                    {
                        var card_i_type = cards_ex[i].Type;
                        if (card_i_type > 10)
                        {
                            card_i_type = 10;
                        }

                        var card_j_type = cards_ex[j].Type;
                        if (card_j_type > 10)
                        {
                            card_j_type = 10;
                        }

                        var card_k_type = cards_ex[k].Type;
                        if (card_k_type > 10)
                        {
                            card_k_type = 10;
                        }

                        if ((card_i_type + card_j_type + card_k_type) % 10 == 0)
                        {
                            have_niu = true;
                        }
                    }
                }
            }

            if (have_niu)
            {
                var niu_num = all_cards_sum_value % 10;

                HandRankTypeNiuNiu rank_type = HandRankTypeNiuNiu.Niu1;
                switch (niu_num)
                {
                    case 0:
                        rank_type = HandRankTypeNiuNiu.NiuNiu;
                        break;
                    case 1:
                        rank_type = HandRankTypeNiuNiu.Niu1;
                        break;
                    case 2:
                        rank_type = HandRankTypeNiuNiu.Niu2;
                        break;
                    case 3:
                        rank_type = HandRankTypeNiuNiu.Niu3;
                        break;
                    case 4:
                        rank_type = HandRankTypeNiuNiu.Niu4;
                        break;
                    case 5:
                        rank_type = HandRankTypeNiuNiu.Niu5;
                        break;
                    case 6:
                        rank_type = HandRankTypeNiuNiu.Niu6;
                        break;
                    case 7:
                        rank_type = HandRankTypeNiuNiu.Niu7;
                        break;
                    case 8:
                        rank_type = HandRankTypeNiuNiu.Niu8;
                        break;
                    case 9:
                        rank_type = HandRankTypeNiuNiu.Niu9;
                        break;
                    default:
                        break;
                }

                var best_cards = cards.Select(x => x.GetCardData()).OrderByDescending(x => x.type).ToList();
                return new BestHandNiuNiu(rank_type, best_cards);
            }

            var bestCards = cards.Select(x => x.GetCardData()).OrderByDescending(x => x.type).ToList();
            return new BestHandNiuNiu(HandRankTypeNiuNiu.HighCard, bestCards, bestCards);
        }

        //---------------------------------------------------------------------       
        private IList<CardData> GetTypesWithNCards(Dictionary<CardTypeNiuNiu, List<Card>> mapCardType, int n)
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
    }
}
