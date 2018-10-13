// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DeckNiuNiu : IDeck
    {
        //---------------------------------------------------------------------
        public static readonly IList<Card> AllCards;
        private static readonly IEnumerable<CardTypeNiuNiu> AllCardTypes = new List<CardTypeNiuNiu>
                                                                         {
                                                                             CardTypeNiuNiu.Ace,
                                                                             CardTypeNiuNiu.Two,
                                                                             CardTypeNiuNiu.Three,
                                                                             CardTypeNiuNiu.Four,
                                                                             CardTypeNiuNiu.Five,
                                                                             CardTypeNiuNiu.Six,
                                                                             CardTypeNiuNiu.Seven,
                                                                             CardTypeNiuNiu.Eight,
                                                                             CardTypeNiuNiu.Nine,
                                                                             CardTypeNiuNiu.Ten,
                                                                             CardTypeNiuNiu.Jack,
                                                                             CardTypeNiuNiu.Queen,
                                                                             CardTypeNiuNiu.King,
                                                                         };
        private static readonly IEnumerable<CardSuit> AllCardSuits = new List<CardSuit>
                                                                         {
                                                                             CardSuit.Club,
                                                                             CardSuit.Diamond,
                                                                             CardSuit.Heart,
                                                                             CardSuit.Spade
                                                                         };
        private Dictionary<CardTypeNiuNiu, List<Card>> mapSameTypeCard;
        private Dictionary<CardSuit, List<Card>> mapSameSuitCard;
        private IList<Card> listOfCards;
        private Random Random;
        private Random RandomEx;
        const int NormalCardsCount = 5;

        //---------------------------------------------------------------------
        static DeckNiuNiu()
        {
            var cards = new List<Card>();
            foreach (var cardSuit in AllCardSuits)
            {
                foreach (var cardType in AllCardTypes)
                {
                    cards.Add(new Card((byte)cardSuit, (byte)cardType));
                }
            }

            AllCards = cards.AsReadOnly();
        }

        //---------------------------------------------------------------------
        public DeckNiuNiu(Random rd1, Random rd2)
        {
            Random = rd1;
            RandomEx = rd2;

            Shuffle();
        }

        //---------------------------------------------------------------------
        public void Shuffle()
        {
            this.listOfCards = AllCards.Shuffle(Random).ToList();

            mapSameTypeCard = new Dictionary<CardTypeNiuNiu, List<Card>>();
            mapSameSuitCard = new Dictionary<CardSuit, List<Card>>();
            foreach (var i in listOfCards)
            {
                CardTypeNiuNiu card_type = (CardTypeNiuNiu)i.Type;
                List<Card> list_sametypecard = null;
                mapSameTypeCard.TryGetValue(card_type, out list_sametypecard);
                if (list_sametypecard == null)
                {
                    list_sametypecard = new List<Card>();
                    mapSameTypeCard[card_type] = list_sametypecard;
                }

                list_sametypecard.Add(i);

                CardSuit card_suit = (CardSuit)i.Suit;
                List<Card> list_samesuitcard = null;
                mapSameSuitCard.TryGetValue(card_suit, out list_samesuitcard);
                if (list_samesuitcard == null)
                {
                    list_samesuitcard = new List<Card>();
                    mapSameSuitCard[card_suit] = list_samesuitcard;
                }

                list_samesuitcard.Add(i);
            }
        }

        //---------------------------------------------------------------------
        public Card GetNextCard()
        {
            if (this.listOfCards.Count == 0)
            {
                return null;
            }

            var card = this.listOfCards[0];
            _removeCard(card);

            return card;
        }

        //---------------------------------------------------------------------
        public void GiveBackCard(List<Card> list_card)
        {
            _giveBackCard(list_card);
        }

        //---------------------------------------------------------------------
        public void GiveBackCard(Card card)
        {
            _giveBackCard(card);
        }

        //---------------------------------------------------------------------
        public List<Card> GetCardsWithHundredHandRankType(HandRankTypeNiuNiu hand_ranktype)
        {
            List<Card> list_cards = new List<Card>();
            switch (hand_ranktype)
            {
                case HandRankTypeNiuNiu.HighCard:
                    list_cards = _getHighCard();
                    break;
                case HandRankTypeNiuNiu.Niu1:
                case HandRankTypeNiuNiu.Niu2:
                case HandRankTypeNiuNiu.Niu3:
                case HandRankTypeNiuNiu.Niu4:
                case HandRankTypeNiuNiu.Niu5:
                case HandRankTypeNiuNiu.Niu6:
                case HandRankTypeNiuNiu.Niu7:
                case HandRankTypeNiuNiu.Niu8:
                case HandRankTypeNiuNiu.Niu9:
                case HandRankTypeNiuNiu.NiuNiu:
                    list_cards = _getNiun((byte)hand_ranktype);
                    break;
                case HandRankTypeNiuNiu.JinNiu:
                    list_cards = _getJinNiu();
                    break;
                case HandRankTypeNiuNiu.SiZha:
                    list_cards = _getFourOfAKind();
                    break;
                case HandRankTypeNiuNiu.WuXiaoNiu:
                    list_cards = _getWuXiaoNiu();
                    break;
                default:
                    break;
            }

            return list_cards;
        }

        //---------------------------------------------------------------------
        List<Card> _getHighCard()
        {
            List<Card> list_cards = new List<Card>();

            for (int i = 0; i < NormalCardsCount; i++)
            {
                var card = GetNextCard();
                list_cards.Add(card);
            }

            var best_hand = CardTypeHelperNiuNiu.GetBestHand(list_cards);
            if (best_hand.RankType != HandRankTypeNiuNiu.HighCard)
            {
                foreach (var i in list_cards)
                {
                    _giveBackCard(i);
                }

                list_cards = _getHighCard();
            }
            
            return list_cards;
        }

        //---------------------------------------------------------------------
        List<Card> _getNiun(byte n)
        {
            if (this.listOfCards.Count < NormalCardsCount)
            {
                return null;
            }

            var card_first = GetNextCard();
            var card_second = GetNextCard();

            byte card_first_type_value = (byte)(card_first.Type >= 10 ? 10 : card_first.Type);
            byte card_second_type_value = (byte)(card_second.Type >= 10 ? 10 : card_second.Type);
            long total_type_value = card_first_type_value + card_second_type_value;
            byte third_card_type = (byte)(10 - total_type_value % 10);
            Card card_third = null;
            if (third_card_type != 10)
            {
                card_third = _getSameTypeCard((CardTypeNiuNiu)third_card_type);
            }
            else
            {
                card_third = _getSameTypeCards(CardTypeNiuNiu.Ten, CardTypeNiuNiu.Jack, CardTypeNiuNiu.Queen, CardTypeNiuNiu.King);
            }

            if (card_third == null)
            {
                _giveBackCard(card_first);
                _giveBackCard(card_second);
                return null;
            }

            List<Card> list_card = new List<Card>();
            var card_fourth = GetNextCard();
            if (card_fourth.Type >= 10)
            {
                _giveBackCard(card_first);
                _giveBackCard(card_second);
                _giveBackCard(card_third);
                _giveBackCard(card_fourth);
                list_card = _getNiun(n);
            }
            else
            {
                byte card_fourth_type_value = (byte)(card_fourth.Type >= 10 ? 10 : card_fourth.Type);
                byte last_cardtype = (byte)((10 + n - card_fourth_type_value) % 10);
                var last_card = _getSameTypeCard((CardTypeNiuNiu)last_cardtype);
                if (last_card == null)
                {
                    _giveBackCard(card_first);
                    _giveBackCard(card_second);
                    _giveBackCard(card_third);
                    _giveBackCard(card_fourth);
                    list_card = _getNiun(n);
                }
                else
                {
                    list_card.Add(card_first);
                    list_card.Add(card_second);
                    list_card.Add(card_third);
                    list_card.Add(card_fourth);
                    list_card.Add(last_card);
                }
            }

            return list_card;
        }

        //---------------------------------------------------------------------
        List<Card> _getJinNiu()
        {
            var need_cards = mapSameTypeCard.Where(x => x.Key > CardTypeNiuNiu.Ten);
            if (need_cards == null || need_cards.Count() == 0)
            {
                return null;
            }

            List<Card> list_card = new List<Card>();
            foreach (var i in need_cards)
            {
                list_card.AddRange(i.Value);
            }

            if (list_card.Count < 5)
            {
                return null;
            }

            list_card = list_card.Shuffle(RandomEx).ToList();
            list_card = list_card.GetRange(0, 5);
            var hand_rank = CardTypeHelperNiuNiu.GetHandRank(list_card);
            if (hand_rank == HandRankTypeNiuNiu.SiZha)
            {
                _giveBackCard(list_card);
                list_card = _getJinNiu();
            }

            foreach (var i in list_card)
            {
                _removeCard(i);
            }

            return list_card;
        }

        //---------------------------------------------------------------------
        List<Card> _getFourOfAKind()
        {
            bool have_sametypecard = _ifHaveSameTypeCard();
            if (!have_sametypecard)
            {
                return null;
            }

            var sametype_card = mapSameTypeCard.Where(x => x.Value.Count == 4 && x.Key != CardTypeNiuNiu.Ace);
            if (sametype_card == null || (sametype_card != null && sametype_card.Count() == 0))
            {
                return null;
            }

            var list_type_cards = sametype_card.Select(x => x.Value).ToList();
            var index = RandomEx.Next(0, list_type_cards.Count);
            var list_final_cards = list_type_cards[index];
            var list_four_cards = list_final_cards.GetRange(0, 4);
            foreach (var i in list_four_cards)
            {
                _removeCard(i);
            }
            Card fourofakind_card = list_four_cards[0];
            var except_type = (CardTypeNiuNiu)fourofakind_card.Type;
            Card card_secondtype = null;
            if (fourofakind_card.Type == 1)
            {
                card_secondtype = _getNotSameTypeCard(1, except_type, CardTypeNiuNiu.Two, CardTypeNiuNiu.Three, CardTypeNiuNiu.Four);
            }
            else if (fourofakind_card.Type == 2)
            {
                card_secondtype = _getNotSameTypeCard(1, except_type, CardTypeNiuNiu.Ace);
            }
            else if (fourofakind_card.Type == 3)
            {
                card_secondtype = _getNotSameTypeCard(1, except_type, CardTypeNiuNiu.Ace);
            }
            else
            {
                card_secondtype = _getNotSameTypeCard(1, except_type);
            }

            if (card_secondtype == null)
            {
                _giveBackCard(list_four_cards);
                return null;
            }
            list_four_cards.Add(card_secondtype);            

            return list_four_cards;
        }

        //---------------------------------------------------------------------
        List<Card> _getWuXiaoNiu()
        {
            var need_cards = mapSameTypeCard.Where(x => x.Key <= CardTypeNiuNiu.Four);
            if (need_cards == null || need_cards.Count() == 0)
            {
                return null;
            }

            List<Card> list_card = new List<Card>();
            foreach (var i in need_cards)
            {
                list_card.AddRange(i.Value);
            }

            if (list_card.Count < 5)
            {
                return null;
            }

            list_card = list_card.Shuffle(RandomEx).ToList();
            var list_take = list_card.GetRange(0, 5);
            var all_cards_sum_value = list_take.Sum(x => x.Type);
            if (all_cards_sum_value > 10)
            {
                _giveBackCard(list_take);
                list_take = _getWuXiaoNiu();
            }

            foreach (var i in list_take)
            {
                _removeCard(i);
            }

            return list_take;
        }

        //---------------------------------------------------------------------
        Card _getSameTypeCard(CardTypeNiuNiu card_type)
        {
            Card card = null;
            List<Card> list_sametypecard = null;
            mapSameTypeCard.TryGetValue(card_type, out list_sametypecard);
            if (list_sametypecard != null && list_sametypecard.Count > 0)
            {
                int index = RandomEx.Next(0, list_sametypecard.Count);
                card = list_sametypecard[index];
                _removeCard(card);
            }

            return card;
        }

        //---------------------------------------------------------------------
        Card _getSameTypeCards(params CardTypeNiuNiu[] card_type)
        {
            if (card_type == null || card_type.Count() == 0)
            {
                return null;
            }

            Card card = null;
            foreach (var i in card_type)
            {
                card = _getSameTypeCard(i);
                if (card != null)
                {
                    break;
                }
            }

            return card;
        }

        //---------------------------------------------------------------------
        Card _getNotSameTypeCard(int sametype_card_mincount, params CardTypeNiuNiu[] card_type)
        {
            Card card = null;
            List<Card> list_sametypecard = null;

            var list_cards = mapSameTypeCard.Where(x => (!card_type.Any(y => y == x.Key)) &&
                   x.Value.Count >= sametype_card_mincount).Select(x => x.Value).ToList();
            if (list_cards != null && list_cards.Count > 0)
            {
                int index = RandomEx.Next(0, list_cards.Count);
                list_sametypecard = list_cards[index];
            }

            if (list_sametypecard != null)
            {
                int index = RandomEx.Next(0, list_sametypecard.Count);
                card = list_sametypecard[index];
                _removeCard(card);
            }

            return card;
        }

        //---------------------------------------------------------------------
        Card _getNextOrLastCardTypeCard(CardTypeNiuNiu card_type, CardSuit card_suit, bool is_same_suit, bool is_next)
        {
            Card card = null;
            CardTypeNiuNiu need_cardtype = CardTypeNiuNiu.Two;
            if (is_next)
            {
                int next_cardtype = ((int)card_type) + 1;
                if (next_cardtype == 15)
                {
                    need_cardtype = CardTypeNiuNiu.Two;
                }
                else
                {
                    need_cardtype = (CardTypeNiuNiu)next_cardtype;
                }
            }
            else
            {
                int last_cardtype = ((int)card_type) - 1;
                if (last_cardtype == 1)
                {
                    need_cardtype = CardTypeNiuNiu.Ace;
                }
                else
                {
                    need_cardtype = (CardTypeNiuNiu)last_cardtype;
                }
            }

            if (is_same_suit)
            {
                List<Card> list_same_suitcards = null;
                if (mapSameSuitCard.TryGetValue(card_suit, out list_same_suitcards))
                {
                    var list_suit_card = list_same_suitcards.Where(x => (CardTypeNiuNiu)x.Type == need_cardtype);
                    if (list_suit_card != null && list_suit_card.Count() > 0)
                    {
                        card = list_suit_card.First();
                        _removeCard(card);
                    }
                }
            }
            else
            {
                card = _getSameTypeCard(need_cardtype);
            }

            return card;
        }

        //---------------------------------------------------------------------
        Card _getSameSuitCard(CardSuit card_suit)
        {
            Card card = null;
            List<Card> list_samesuitcard = null;
            mapSameSuitCard.TryGetValue(card_suit, out list_samesuitcard);
            if (list_samesuitcard != null && list_samesuitcard.Count > 0)
            {
                int index = RandomEx.Next(0, list_samesuitcard.Count);
                card = list_samesuitcard[index];
                _removeCard(card);
            }

            return card;
        }

        //---------------------------------------------------------------------
        Card _getNotSameSuitCard(int samesuit_card_mincount, params CardSuit[] card_suit)
        {
            Card card = null;
            List<Card> list_samesuitcard = null;

            var list_cards = mapSameSuitCard.Where(x => (!card_suit.Any(y => y == x.Key)) &&
                     x.Value.Count >= samesuit_card_mincount).Select(x => x.Value).ToList();
            if (list_cards != null && list_cards.Count > 0)
            {
                int index = RandomEx.Next(0, list_cards.Count);
                list_samesuitcard = list_cards[index];
            }

            if (list_samesuitcard != null)
            {
                int index = RandomEx.Next(0, list_samesuitcard.Count);
                card = list_samesuitcard[index];
                _removeCard(card);
            }

            return card;
        }

        //---------------------------------------------------------------------
        void _giveBackCard(Card card)
        {
            if (!this.listOfCards.Contains(card))
            {
                this.listOfCards.Add(card);
            }

            List<Card> list_sametypecard = null;
            mapSameTypeCard.TryGetValue((CardTypeNiuNiu)card.Type, out list_sametypecard);
            if (list_sametypecard != null && !list_sametypecard.Contains(card))
            {
                list_sametypecard.Add(card);
            }

            List<Card> list_samesuitcard = null;
            mapSameSuitCard.TryGetValue((CardSuit)card.Suit, out list_samesuitcard);
            if (list_samesuitcard != null && !list_samesuitcard.Contains(card))
            {
                list_samesuitcard.Add(card);
            }
        }

        //---------------------------------------------------------------------
        void _removeCard(Card card)
        {
            this.listOfCards.Remove(card);

            List<Card> list_sametypecard = null;
            mapSameTypeCard.TryGetValue((CardTypeNiuNiu)card.Type, out list_sametypecard);
            if (list_sametypecard != null)
            {
                list_sametypecard.Remove(card);
            }

            List<Card> list_samesuitcard = null;
            mapSameSuitCard.TryGetValue((CardSuit)card.Suit, out list_samesuitcard);
            if (list_samesuitcard != null)
            {
                list_samesuitcard.Remove(card);
            }
        }

        //---------------------------------------------------------------------
        bool _ifHaveSameTypeCard()
        {
            bool have_sametype_card = false;

            foreach (var i in mapSameTypeCard)
            {
                if (i.Value.Count > 2)
                {
                    have_sametype_card = true;
                    break;
                }
            }

            return have_sametype_card;
        }

        //---------------------------------------------------------------------
        bool _ifHaveSameSuitCard()
        {
            bool have_samesuit_card = false;

            foreach (var i in mapSameSuitCard)
            {
                if (i.Value.Count > 2)
                {
                    have_samesuit_card = true;
                    break;
                }
            }

            return have_samesuit_card;
        }

        //---------------------------------------------------------------------
        void _giveBackCard(List<Card> list_card)
        {
            foreach (var i in list_card)
            {
                _giveBackCard(i);
            }

            list_card.Clear();
        }
    }
}
