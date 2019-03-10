// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DeckTexas : IDeck
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
        const int NormalCardsCount = 5;

        //---------------------------------------------------------------------
        static DeckTexas()
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
        public DeckTexas(Random rd, Random rd2)
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
                CardType card_type = (CardType)i.Type;
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
        public List<Card> GetCardsWithHundredHandRankType(HandRankTypeTexasH hand_ranktype)
        {
            List<Card> list_cards = new List<Card>();
            switch (hand_ranktype)
            {
                case HandRankTypeTexasH.HighCard:
                    list_cards = _getHighCard();
                    break;
                case HandRankTypeTexasH.Pair:
                    list_cards = _getPair();
                    break;
                case HandRankTypeTexasH.TwoPairs:
                    list_cards = _getTwoPair();
                    break;
                case HandRankTypeTexasH.ThreeOfAKind:
                    list_cards = _getThreeOfAKind(false);
                    break;
                case HandRankTypeTexasH.Straight:
                    list_cards = _getStraight();
                    break;
                case HandRankTypeTexasH.Flush:
                    list_cards = _getFlush();
                    break;
                case HandRankTypeTexasH.FullHouse:
                    list_cards = _getFullHouse();
                    break;
                case HandRankTypeTexasH.FourOfAKind:
                    list_cards = _getFourOfAKind();
                    break;
                case HandRankTypeTexasH.StraightFlush:
                    list_cards = _getStraightFlush();
                    break;
                case HandRankTypeTexasH.RoyalFlush:
                    list_cards = _getRoyalFlush();
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

            var hand_rank = CardTypeHelperTexas.GetHandRankHTexas(list_cards);
            if (hand_rank != HandRankTypeTexasH.HighCard)
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

            CardType card_type_sec = (CardType)card_secondtype.Type;
            var card_thirdtype = _getNotSameTypeCard(1, card_type, card_type_sec);
            if (card_thirdtype == null)
            {
                _giveBackCard(list_cards);
                return null;
            }
            list_cards.Add(card_thirdtype);

            var card_lasttype = _getNotSameTypeCard(1, card_type, card_type_sec, (CardType)card_thirdtype.Type);
            if (card_lasttype == null)
            {
                _giveBackCard(list_cards);
                return null;
            }
            list_cards.Add(card_lasttype);

            return list_cards;
        }

        //---------------------------------------------------------------------
        List<Card> _getTwoPair()
        {
            List<Card> list_cards = _getPairOnlyTwoCardsExceptType(null);
            if (list_cards == null)
            {
                return null;
            }

            Card pair_card = list_cards[0];
            var card_secondtype = _getPairOnlyTwoCardsExceptType(pair_card);
            if (card_secondtype == null)
            {
                _giveBackCard(list_cards);
                return null;
            }
            list_cards.AddRange(card_secondtype);
            Card pair_card_two = card_secondtype[0];

            var card_thirdtype = _getNotSameTypeCard(1, (CardType)pair_card.Type, (CardType)pair_card_two.Type);
            if (card_thirdtype == null)
            {
                _giveBackCard(list_cards);
                return null;
            }
            list_cards.Add(card_thirdtype);

            return list_cards;
        }

        //---------------------------------------------------------------------
        List<Card> _getThreeOfAKind(bool only_threecards)
        {
            bool have_sametypecard = _ifHaveSameTypeCard();
            if (!have_sametypecard)
            {
                return null;
            }

            var sametype_card = mapSameTypeCard.Where(x => x.Value.Count > 2);
            if (sametype_card == null || (sametype_card != null && sametype_card.Count() == 0))
            {
                return null;
            }

            var list_type_cards = sametype_card.Select(x => x.Value).ToList();
            var index = RandomEx.Next(0, list_type_cards.Count);
            var list_final_cards = list_type_cards[index];
            List<Card> list_threeofakind = list_final_cards.GetRange(0, 3);
            foreach (var i in list_threeofakind)
            {
                _removeCard(i);
            }

            if (only_threecards)
            {
                return list_threeofakind;
            }

            Card threeofakind_card = list_threeofakind[0];
            CardType card_type = (CardType)threeofakind_card.Type;
            var card_secondtype = _getNotSameTypeCard(1, card_type);
            if (card_secondtype == null)
            {
                _giveBackCard(list_threeofakind);
                return null;
            }
            list_threeofakind.Add(card_secondtype);

            var card_thirdtype = _getNotSameTypeCard(1, card_type, (CardType)card_secondtype.Type);
            if (card_thirdtype == null)
            {
                _giveBackCard(list_threeofakind);
                return null;
            }
            list_threeofakind.Add(card_thirdtype);

            return list_threeofakind;
        }

        //---------------------------------------------------------------------
        List<Card> _getStraight()
        {
            List<Card> list_card = new List<Card>();
            var card = GetNextCard();
            list_card.Add(card);
            CardType card_type = (CardType)card.Type;
            CardSuit card_suit = (CardSuit)card.Suit;
            if ((card.Type >= ((int)CardType.Jack)) && (card.Type < ((int)CardType.Ace)))
            {
                _getStraightEx(list_card, card_type, card_suit, false, false);
            }
            else if (card_type == CardType.Ace)
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

            var hand_rank = CardTypeHelperTexas.GetHandRankHTexas(list_card);
            if (hand_rank == HandRankTypeTexasH.StraightFlush || hand_rank == HandRankTypeTexasH.RoyalFlush)
            {
                _giveBackCard(list_card);
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
            list_card.Add(card);
            CardSuit card_suit = (CardSuit)card.Suit;
            var card_second = _getSameSuitCard(card_suit);
            if (card_second == null)
            {
                _giveBackCard(list_card);
                return null;
            }
            list_card.Add(card_second);

            var card_third = _getSameSuitCard(card_suit);
            if (card_third == null)
            {
                _giveBackCard(list_card);
                return null;
            }
            list_card.Add(card_third);

            var card_fourth = _getSameSuitCard(card_suit);
            if (card_fourth == null)
            {
                _giveBackCard(list_card);
                return null;
            }
            list_card.Add(card_fourth);

            var card_last = _getSameSuitCard(card_suit);
            if (card_last == null)
            {
                _giveBackCard(list_card);
                return null;
            }
            list_card.Add(card_last);

            var hand_rank = CardTypeHelperTexas.GetHandRankHTexas(list_card);
            if (hand_rank == HandRankTypeTexasH.StraightFlush || hand_rank == HandRankTypeTexasH.RoyalFlush)
            {
                _giveBackCard(list_card);
                list_card = _getFlush();
            }

            return list_card;
        }

        //---------------------------------------------------------------------
        List<Card> _getFullHouse()
        {
            var list_card = _getThreeOfAKind(true);
            if (list_card == null)
            {
                return null;
            }

            Card threeofakind_card = list_card[0];
            var list_pair = _getPairOnlyTwoCardsExceptType(threeofakind_card);
            if (list_pair == null)
            {
                _giveBackCard(list_card);
                list_card = _getFullHouse();
            }
            else
            {
                list_card.AddRange(list_pair);
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

            var sametype_card = mapSameTypeCard.Where(x => x.Value.Count == 4);
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

            var card_secondtype = _getNotSameTypeCard(1, (CardType)fourofakind_card.Type);
            if (card_secondtype == null)
            {
                _giveBackCard(list_four_cards);
                return null;
            }
            list_four_cards.Add(card_secondtype);

            return list_four_cards;
        }

        //---------------------------------------------------------------------
        List<Card> _getStraightFlush()
        {
            List<Card> list_card = new List<Card>();
            var card = GetNextCard();
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
            else if (card_type == CardType.Ten)
            {
                _getStraightEx(list_card, card_type, card_suit, true, false);
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
        List<Card> _getRoyalFlush()
        {
            List<Card> list_card = new List<Card>();
            var list_ace = _getAllSameTypeCard(CardType.Ace);
            if (list_ace == null)
            {
                return list_card;
            }

            list_ace = list_ace.Shuffle(RandomEx).ToList();

            foreach (var i in list_ace)
            {
                var ace = i;
                var k = _getCard(CardType.King, (CardSuit)ace.Suit);
                var q = _getCard(CardType.Queen, (CardSuit)ace.Suit);
                var j = _getCard(CardType.Jack, (CardSuit)ace.Suit);
                var t = _getCard(CardType.Ten, (CardSuit)ace.Suit);
                if (k == null || q == null || j == null || t == null)
                {
                    continue;
                }
                else
                {
                    list_card.Add(ace);
                    list_card.Add(k);
                    list_card.Add(q);
                    list_card.Add(j);
                    list_card.Add(t);
                    break;
                }
            }
            foreach (var i in list_card)
            {
                _removeCard(i);
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
        List<Card> _getAllSameTypeCard(CardType card_type)
        {
            List<Card> list_sametypecard = null;
            mapSameTypeCard.TryGetValue(card_type, out list_sametypecard);

            return list_sametypecard;
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
            CardType need_cardtype = CardType.Two;
            if (is_next)
            {
                int next_cardtype = ((int)card_type) + 1;
                if (next_cardtype == 15)
                {
                    need_cardtype = CardType.Two;
                }
                else
                {
                    need_cardtype = (CardType)next_cardtype;
                }
            }
            else
            {
                int last_cardtype = ((int)card_type) - 1;
                if (last_cardtype == 1)
                {
                    need_cardtype = CardType.Ace;
                }
                else
                {
                    need_cardtype = (CardType)last_cardtype;
                }
            }

            if (is_same_suit)
            {
                List<Card> list_same_suitcards = null;
                if (mapSameSuitCard.TryGetValue(card_suit, out list_same_suitcards))
                {
                    var list_suit_card = list_same_suitcards.Where(x => (CardType)x.Type == need_cardtype);
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
        Card _getCard(CardType card_type, CardSuit card_suit)
        {
            Card c = null;
            var l_c = this.listOfCards.Where(x => x.Suit == (byte)card_suit && x.Type == (byte)card_type).ToList();
            if (l_c != null && l_c.Count() > 0)
            {
                c = l_c[0];
            }

            return c;
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

            var card_fourth = _getNextOrLastCardTypeCard((CardType)card_third.Type, card_suit, get_same_suit, get_next_card);
            if (card_fourth == null)
            {
                _giveBackCard(list_card);
                return;
            }
            list_card.Add(card_fourth);

            var card_last = _getNextOrLastCardTypeCard((CardType)card_fourth.Type, card_suit, get_same_suit, get_next_card);
            if (card_last == null)
            {
                _giveBackCard(list_card);
                return;
            }
            list_card.Add(card_last);
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
    }
}