// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DeckGFlower : IDeck
    {
        //---------------------------------------------------------------------
        public static readonly IList<Card> AllCards;
        private static readonly IEnumerable<CardType> AllCardTypes = new List<CardType>
                                                                         {
                                                                             CardType.Two,
                                                                             CardType.Three,
                                                                             CardType.Four,
                                                                             CardType.Five,
                                                                             CardType.Six,
                                                                             CardType.Seven,
                                                                             CardType.Eight,
                                                                             CardType.Nine,
                                                                             CardType.Ten,
                                                                             CardType.Jack,
                                                                             CardType.Queen,
                                                                             CardType.King,
                                                                             CardType.Ace
                                                                         };
        private static readonly IEnumerable<CardSuit> AllCardSuits = new List<CardSuit>
                                                                         {
                                                                             CardSuit.Club,
                                                                             CardSuit.Diamond,
                                                                             CardSuit.Heart,
                                                                             CardSuit.Spade
                                                                         };
        private Dictionary<CardType, List<Card>> mapSameTypeCard;
        private Dictionary<CardSuit, List<Card>> mapSameSuitCard;
        private IList<Card> listOfCards;
        private Random Random;
        private Random RandomEx;
        public const int NormalCardsCount = 3;

        //---------------------------------------------------------------------
        static DeckGFlower()
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
        public DeckGFlower(Random rd, Random rd2)
        {
            Random = rd;
            RandomEx = rd2;

            Shuffle();
        }

        //---------------------------------------------------------------------
        public void Shuffle()
        {
            this.listOfCards = AllCards.Shuffle(Random).ToList();

            mapSameTypeCard = new Dictionary<CardType, List<Card>>();
            mapSameSuitCard = new Dictionary<CardSuit, List<Card>>();
            foreach (var i in listOfCards)
            {
                List<Card> list_sametypecard = null;
                CardType card_type = (CardType)i.Type;
                mapSameTypeCard.TryGetValue(card_type, out list_sametypecard);
                if (list_sametypecard == null)
                {
                    list_sametypecard = new List<Card>();
                    mapSameTypeCard[card_type] = list_sametypecard;
                }

                list_sametypecard.Add(i);

                List<Card> list_samesuitcard = null;
                CardSuit card_suit = (CardSuit)i.Suit;
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
        public List<Card> GetCardsWithHundredHandRankType(HandRankTypeGFlowerH hand_ranktype)
        {
            List<Card> list_cards = new List<Card>();
            switch (hand_ranktype)
            {
                case HandRankTypeGFlowerH.HighCard:
                    list_cards = _getHighCard();
                    break;
                case HandRankTypeGFlowerH.Pair:
                    list_cards = _getPair();
                    break;
                case HandRankTypeGFlowerH.Straight:
                    list_cards = _getStraight();
                    break;
                case HandRankTypeGFlowerH.Flush:
                    list_cards = _getFlush();
                    break;
                case HandRankTypeGFlowerH.StraightFlush:
                    list_cards = _getStraightFlush();
                    break;
                case HandRankTypeGFlowerH.BaoZi:
                    list_cards = _getThreeOfAKind(CardType.Ace);
                    break;
                case HandRankTypeGFlowerH.RoyalBaoZi:
                    list_cards = _getThreeOfAKind(CardType.Ace, false);
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
                if (card == null)
                {
                    return null;
                }

                list_cards.Add(card);
            }

            var rank_type = CardTypeHelperGFlower.GetHandRankHGFlower(list_cards);
            if (rank_type != HandRankTypeGFlowerH.HighCard)
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
        List<Card> _getPair()
        {
            List<Card> list_cards = _getPairOnlyTwoCardsExceptType(null);
            if (list_cards == null)
            {
                return null;
            }

            Card pair_card = list_cards[0];
            CardType card_type = (CardType)pair_card.Type;
            var card_secondtype = _getNotSameTypeCard(1, card_type);
            if (card_secondtype == null)
            {
                _giveBackCard(list_cards);
                return null;
            }
            list_cards.Add(card_secondtype);

            return list_cards;
        }

        //---------------------------------------------------------------------
        List<Card> _getThreeOfAKind(CardType card_type, bool is_random = true)
        {
            bool have_sametypecard = _ifHaveSameTypeCard();
            if (!have_sametypecard)
            {
                return null;
            }

            List<Card> list_threeofakind = null;
            if (!is_random)
            {
                List<Card> list_card = null;
                mapSameTypeCard.TryGetValue(card_type, out list_card);

                if (list_card != null && list_card.Count > 2)
                {
                    list_threeofakind = list_card.GetRange(0, 3);
                    foreach (var i in list_threeofakind)
                    {
                        _removeCard(i);
                    }

                    return list_threeofakind;
                }
            }
            else
            {
                var sametype_card = mapSameTypeCard.Where(x => x.Value.Count > 2);
                if (sametype_card == null || (sametype_card != null && sametype_card.Count() == 0))
                {
                    return null;
                }

                var list_type_cards = sametype_card.Select(x => x.Value).ToList();
                var index = RandomEx.Next(0, list_type_cards.Count);
                var list_final_cards = list_type_cards[index];
                list_threeofakind = list_final_cards.GetRange(0, 3);
                foreach (var i in list_threeofakind)
                {
                    _removeCard(i);
                }
            }

            return list_threeofakind;
        }

        //---------------------------------------------------------------------
        List<Card> _getStraight()
        {
            List<Card> list_card = new List<Card>();
            var card = GetNextCard();
            if (card == null)
            {
                return null;
            }

            list_card.Add(card);

            CardType card_type = (CardType)card.Type;
            CardSuit card_suit = (CardSuit)card.Suit;
            if ((card.Type >= ((int)CardType.Jack)) && (card.Type < ((int)CardType.Ace)))
            {
                _getStraightEx(list_card, card_type, card_suit, false, false);
            }
            else if ((CardType)card.Type == CardType.Ace)
            {
                _getStraightEx(list_card, card_type, card_suit, false, false);
                if (list_card.Count <= 1)
                {
                    _getStraightEx(list_card, card_type, card_suit, false, true);
                }
            }
            else
            {
                _getStraightEx(list_card, card_type, card_suit, false, true);
            }

            if (list_card.Count == 0)
            {
                list_card = _getStraight();
            }

            return list_card;
        }

        //---------------------------------------------------------------------
        List<Card> _getFlush()
        {
            bool if_havesame_suit = _ifHaveSameSuitCard();
            if (!if_havesame_suit)
            {
                return null;
            }

            List<Card> list_card = new List<Card>();
            var card = GetNextCard();
            if (card == null)
            {
                return null;
            }

            list_card.Add(card);
            CardType first_cardtype_next = _getNextCardType((CardType)card.Type, true);
            CardType first_cardtype_last = _getNextCardType((CardType)card.Type, false);
            CardSuit card_suit = (CardSuit)card.Suit;
            var card_second = _getSameSuitNotSameTypeCard(card_suit, first_cardtype_next, first_cardtype_last);
            if (card_second == null)
            {
                _giveBackCard(list_card);
                return null;
            }
            list_card.Add(card_second);

            CardType second_cardtype_next = _getNextCardType((CardType)card_second.Type, true);
            CardType second_cardtype_last = _getNextCardType((CardType)card_second.Type, false);
            var card_third = _getSameSuitNotSameTypeCard(card_suit, second_cardtype_next, second_cardtype_last);
            if (card_third == null)
            {
                _giveBackCard(list_card);
                return null;
            }
            list_card.Add(card_third);

            return list_card;
        }

        //---------------------------------------------------------------------
        List<Card> _getStraightFlush()
        {
            List<Card> list_card = new List<Card>();
            var card = GetNextCard();
            if (card == null)
            {
                return null;
            }

            list_card.Add(card);
            CardType card_type = (CardType)card.Type;
            CardSuit card_suit = (CardSuit)card.Suit;
            if ((card.Type >= ((int)CardType.Jack)) && (card.Type < ((int)CardType.Ace)))
            {
                _getStraightEx(list_card, card_type, card_suit, true, false);
            }
            else if (card_type == CardType.Ace)
            {
                _getStraightEx(list_card, card_type, card_suit, true, true);
            }
            else
            {
                _getStraightEx(list_card, card_type, card_suit, true, true);
            }

            if (list_card.Count == 0)
            {
                list_card = _getStraightFlush();
            }

            return list_card;
        }

        //---------------------------------------------------------------------
        Card _getSameTypeCard(CardType card_type)
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
        Card _getNotSameTypeCard(int sametype_card_mincount, params CardType[] card_type)
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
        Card _getNextOrLastCardTypeCard(CardType card_type, CardSuit card_suit, bool is_same_suit, bool is_next)
        {
            Card card = null;
            CardType need_cardtype = _getNextCardType(card_type, is_next);

            if (is_same_suit)
            {
                List<Card> list_same_suitcards = null;
                if (mapSameSuitCard.TryGetValue(card_suit, out list_same_suitcards))
                {
                    byte need_card_typeex = (byte)need_cardtype;
                    var list_suit_card = list_same_suitcards.Where(x => x.Type == need_card_typeex);
                    if (list_suit_card != null && list_suit_card.Count() > 0)
                    {
                        card = list_suit_card.First();
                        _removeCard(card);
                    }
                }
            }
            else
            {
                card = _getSameTypeNotSameSuitCard(need_cardtype, card_suit);
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
        Card _getSameSuitNotSameTypeCard(CardSuit card_suit, params CardType[] card_type)
        {
            Card card = null;
            List<Card> list_samesuitcard = null;
            mapSameSuitCard.TryGetValue(card_suit, out list_samesuitcard);
            if (list_samesuitcard != null && list_samesuitcard.Count > 0)
            {
                if (card_type.Length > 0)
                {
                    list_samesuitcard = list_samesuitcard.Where(x => card_type.Where(y => (byte)y == x.Type).Count() == 0).ToList();
                }

                int index = RandomEx.Next(0, list_samesuitcard.Count);
                card = list_samesuitcard[index];
                _removeCard(card);
            }

            return card;
        }

        //---------------------------------------------------------------------
        Card _getSameTypeNotSameSuitCard(CardType card_type, CardSuit card_suit)
        {
            Card card = null;
            List<Card> list_sametypecard = null;
            mapSameTypeCard.TryGetValue(card_type, out list_sametypecard);
            if (list_sametypecard != null && list_sametypecard.Count > 0)
            {
                list_sametypecard = list_sametypecard.Where(x => x.Suit != (byte)card_suit).ToList();
                int index = RandomEx.Next(0, list_sametypecard.Count);
                card = list_sametypecard[index];
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
            mapSameTypeCard.TryGetValue((CardType)card.Type, out list_sametypecard);
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
            mapSameTypeCard.TryGetValue((CardType)card.Type, out list_sametypecard);
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
        void _getStraightEx(List<Card> list_card, CardType card_type, CardSuit card_suit, bool get_same_suit, bool get_next_card)
        {
            var card_next = _getNextOrLastCardTypeCard(card_type, card_suit, get_same_suit, get_next_card);
            if (card_next == null)
            {
                _giveBackCard(list_card);
                return;
            }
            list_card.Add(card_next);

            var card_third = _getNextOrLastCardTypeCard((CardType)card_next.Type, card_suit, get_same_suit, get_next_card);
            if (card_third == null)
            {
                _giveBackCard(list_card);
                return;
            }
            list_card.Add(card_third);
        }

        //---------------------------------------------------------------------
        List<Card> _getPairOnlyTwoCardsExceptType(Card except_card)
        {
            bool have_sametypecard = _ifHaveSameTypeCard();
            if (!have_sametypecard)
            {
                return null;
            }

            List<Card> list_cards = new List<Card>();
            Card card = null;
            if (except_card == null)
            {
                card = GetNextCard();
                if (card == null)
                {
                    return null;
                }
            }
            else
            {
                card = _getNotSameTypeCard(2, (CardType)except_card.Type);
            }

            var same_typecard = _getSameTypeCard((CardType)card.Type);
            if (same_typecard == null)
            {
                _giveBackCard(card);
                list_cards = _getPairOnlyTwoCardsExceptType(except_card);
            }
            else
            {
                list_cards.Add(card);
                list_cards.Add(same_typecard);
            }

            return list_cards;
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

        //---------------------------------------------------------------------
        CardType _getNextCardType(CardType card_type, bool is_next)
        {
            CardType card_typenext = CardType.Ace;
            if (is_next)
            {
                byte card_n = (byte)((byte)card_type + 1);
                if (card_n == 15)
                {
                    card_n = 2;
                }
                card_typenext = (CardType)card_n;
            }
            else
            {
                byte card_n = (byte)((byte)card_type - 1);
                if (card_n == 1)
                {
                    card_n = 14;
                }
                card_typenext = (CardType)card_n;
            }

            return card_typenext;
        }
    }
}
