// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    //private static readonly IEnumerable<MaJiangType> AllCardTypes = new List<MaJiangType>
    //                                                                 {
    //                                                                     MaJiangType.One,
    //                                                                     MaJiangType.Two,
    //                                                                     MaJiangType.Three,
    //                                                                     MaJiangType.Four,
    //                                                                     MaJiangType.Five,
    //                                                                     MaJiangType.Six,
    //                                                                     MaJiangType.Seven,
    //                                                                     MaJiangType.Eight,
    //                                                                     MaJiangType.Nine,                                                                                                                                                     
    //                                                                 };
    //private static readonly IEnumerable<MaJiangSuit> AllCardSuits = new List<MaJiangSuit>
    //                                                                 {
    //                                                                     MaJiangSuit.Wan,
    //                                                                     MaJiangSuit.Bing,
    //                                                                     MaJiangSuit.Tiao,
    //                                                                     MaJiangSuit.DongFeng,
    //                                                                     MaJiangSuit.NanFeng,
    //                                                                     MaJiangSuit.XiFeng,
    //                                                                     MaJiangSuit.BeiFeng,
    //                                                                     MaJiangSuit.Zhong,
    //                                                                     MaJiangSuit.Fa,
    //                                                                     MaJiangSuit.Bai,
    //                                                                 };

    public class DeckZhongFB : IDeck
    {
        //---------------------------------------------------------------------
        public static readonly IList<Card> AllCards;
        private static readonly IEnumerable<MaJiangType> AllCardTypes = new List<MaJiangType>
                                                                         {
                                                                             MaJiangType.One,
                                                                             MaJiangType.Two,
                                                                             MaJiangType.Three,
                                                                             MaJiangType.Four,
                                                                             MaJiangType.Five,
                                                                             MaJiangType.Six,
                                                                             MaJiangType.Seven,
                                                                             MaJiangType.Eight,
                                                                             MaJiangType.Nine,
                                                                             MaJiangType.Zhong,
                                                                             MaJiangType.Fa,
                                                                             MaJiangType.Bai,
                                                                         };
        private static readonly IEnumerable<MaJiangSuit> AllCardSuits = new List<MaJiangSuit>
                                                                         {
                                                                             MaJiangSuit.Tong,
                                                                             MaJiangSuit.Zhong,
                                                                             MaJiangSuit.Fa,
                                                                             MaJiangSuit.Bai,
                                                                         };
        private Dictionary<MaJiangType, List<Card>> mapSameTypeCard;
        private Dictionary<MaJiangSuit, List<Card>> mapSameSuitCard;
        private IList<Card> listOfCards;
        private Random Random;
        private Random RandomEx;
        const int NormalCardsCount = 2;

        //---------------------------------------------------------------------
        static DeckZhongFB()
        {
            var cards = new List<Card>();
            for (int i = 0; i < 4; i++)
            {
                foreach (var cardSuit in AllCardSuits)
                {
                    foreach (var cardType in AllCardTypes)
                    {
                        if (cardSuit != MaJiangSuit.Tong)
                        {
                            if (cardType.ToString().Equals(cardSuit.ToString()))
                            {
                                cards.Add(new Card((byte)cardSuit, (byte)cardType));
                            }
                        }
                        else
                        {
                            if (cardType == MaJiangType.Zhong || cardType == MaJiangType.Fa || cardType == MaJiangType.Bai)
                            {
                                continue;
                            }
                            cards.Add(new Card((byte)cardSuit, (byte)cardType));
                        }
                    }
                }
            }
            AllCards = cards.AsReadOnly();
        }

        //---------------------------------------------------------------------
        public DeckZhongFB(Random rd, Random rd2)
        {
            Random = rd;
            RandomEx = rd2;

            Shuffle();
        }

        //---------------------------------------------------------------------
        public void Shuffle()
        {
            this.listOfCards = AllCards.Shuffle(Random).ToList();

            mapSameTypeCard = new Dictionary<MaJiangType, List<Card>>();
            mapSameSuitCard = new Dictionary<MaJiangSuit, List<Card>>();
            foreach (var i in listOfCards)
            {
                List<Card> list_sametypecard = null;
                mapSameTypeCard.TryGetValue((MaJiangType)i.Type, out list_sametypecard);
                if (list_sametypecard == null)
                {
                    list_sametypecard = new List<Card>();
                    mapSameTypeCard[(MaJiangType)i.Type] = list_sametypecard;
                }

                list_sametypecard.Add(i);

                List<Card> list_samesuitcard = null;
                mapSameSuitCard.TryGetValue((MaJiangSuit)i.Suit, out list_samesuitcard);
                if (list_samesuitcard == null)
                {
                    list_samesuitcard = new List<Card>();
                    mapSameSuitCard[(MaJiangSuit)i.Suit] = list_samesuitcard;
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
        public List<Card> GetCardsWithHundredHandRankType(HandRankTypeZhongFB hand_ranktype)
        {
            List<Card> list_cards = new List<Card>();
            switch (hand_ranktype)
            {
                case HandRankTypeZhongFB.Dian0:
                    list_cards = _getDian0();
                    break;
                case HandRankTypeZhongFB.Dian1:                    
                case HandRankTypeZhongFB.Dian2:                    
                case HandRankTypeZhongFB.Dian3:                   
                case HandRankTypeZhongFB.Dian4:                    
                case HandRankTypeZhongFB.Dian5:                    
                case HandRankTypeZhongFB.Dian6:                    
                case HandRankTypeZhongFB.Dian7:                    
                case HandRankTypeZhongFB.Dian8:                    
                case HandRankTypeZhongFB.Dian9:
                    list_cards = _getDiannWithOut0((byte)hand_ranktype);
                    break;
                case HandRankTypeZhongFB.BaoZi:
                    list_cards = _getPairExceptType(MaJiangType.Fa, MaJiangType.Zhong, MaJiangType.Bai);
                    break;
                case HandRankTypeZhongFB.BaoZiBai:
                    list_cards = _getPairWithType(MaJiangType.Bai);
                    break;
                case HandRankTypeZhongFB.BaoZiZhong:
                    list_cards = _getPairWithType(MaJiangType.Zhong);
                    break;
                case HandRankTypeZhongFB.BaoZiFa:
                    list_cards = _getPairWithType(MaJiangType.Fa);
                    break;
                case HandRankTypeZhongFB.TianGang:
                    list_cards = _getTianGang();
                    break;
                default:
                    break;
            }

            return list_cards;
        }

        //---------------------------------------------------------------------
        List<Card> _getDian0()
        {
            var card = GetNextCard();
            if (card == null)
            {
                return null;
            }

            List<Card> list_card = new List<Card>();
            switch ((MaJiangType)card.Type)
            {
                case MaJiangType.One:
                case MaJiangType.Three:
                case MaJiangType.Four:
                case MaJiangType.Six:
                case MaJiangType.Seven:
                case MaJiangType.Nine:
                    {
                        byte next_cardtype = (byte)(10 - card.Type);
                        var card_next = _getSameTypeCard((MaJiangType)next_cardtype);
                        if (card_next == null)
                        {
                            _giveBackCard(card);
                            list_card = _getDian0();
                        }
                        else
                        {
                            list_card.Add(card);
                            list_card.Add(card_next);
                        }
                    }
                    break;
                case MaJiangType.Five:
                case MaJiangType.Eight:
                case MaJiangType.Two:
                case MaJiangType.DongFeng:
                case MaJiangType.NanFeng:
                case MaJiangType.XiFeng:
                case MaJiangType.BeiFeng:
                    _giveBackCard(card);
                    list_card = _getDian0();
                    break;
                case MaJiangType.Bai:
                    {
                        var card_next = _getSameTypeCard(MaJiangType.Zhong, MaJiangType.Fa);
                        if (card_next == null)
                        {
                            _giveBackCard(card);
                            list_card = _getDian0();
                        }
                        else
                        {
                            list_card.Add(card);
                            list_card.Add(card_next);
                        }
                    }
                    break;
                case MaJiangType.Zhong:
                    {
                        var card_next = _getSameTypeCard(MaJiangType.Fa, MaJiangType.Bai);
                        if (card_next == null)
                        {
                            _giveBackCard(card);
                            list_card = _getDian0();
                        }
                        else
                        {
                            list_card.Add(card);
                            list_card.Add(card_next);
                        }
                    }
                    break;
                case MaJiangType.Fa:
                    {
                        var card_next = _getSameTypeCard(MaJiangType.Bai, MaJiangType.Zhong);
                        if (card_next == null)
                        {
                            _giveBackCard(card);
                            list_card = _getDian0();
                        }
                        else
                        {
                            list_card.Add(card);
                            list_card.Add(card_next);
                        }
                    }
                    break;
                default:
                    break;
            }

            return list_card;
        }

        //---------------------------------------------------------------------
        List<Card> _getDiannWithOut0(byte dian)
        {
            var card = GetNextCard();
            if (card == null)
            {
                return null;
            }

            byte card_typevalue = card.Type;
            if (card_typevalue >= 10)
            {
                card_typevalue = 10;
            }

            List<Card> list_card = new List<Card>();
            byte next_cardtype = (byte)((10 + dian - card_typevalue) % 10);
            if (next_cardtype == card.Type)
            {
                _giveBackCard(card);
                list_card = _getDiannWithOut0(dian);
            }
            else
            {
                var card_next = _getSameTypeCard((MaJiangType)next_cardtype);
                if (card_next == null)
                {
                    _giveBackCard(card);
                    list_card = _getDiannWithOut0(dian);
                }
                else
                {
                    list_card.Add(card);
                    list_card.Add(card_next);
                }
            }

            return list_card;
        }

        //---------------------------------------------------------------------
        List<Card> _getTianGang()
        {
            var card_two = _getSameTypeCard(MaJiangType.Two);
            if (card_two == null)
            {
                return null;
            }

            var card_eight = _getSameTypeCard(MaJiangType.Eight);
            if (card_eight == null)
            {
                return null;
            }

            List<Card> list_cards = new List<Card>();
            list_cards.Add(card_two);
            list_cards.Add(card_eight);
            return list_cards;
        }

        //---------------------------------------------------------------------
        List<Card> _getPairWithType(MaJiangType type)
        {
            List<Card> list_sametypecard = null;
            mapSameTypeCard.TryGetValue(type, out list_sametypecard);
            if (list_sametypecard == null || list_sametypecard.Count < 2)
            {
                return null;
            }
            List<Card> list_cards = list_sametypecard.Take(2).ToList();

            foreach (var i in list_cards)
            {
                _removeCard(i);
            }

            return list_cards;
        }

        //---------------------------------------------------------------------
        List<Card> _getPairExceptType(params MaJiangType[] type)
        {
            bool have_sametypecard = _ifHaveSameTypeCard();
            if (!have_sametypecard)
            {
                return null;
            }

            List<Card> list_cards = new List<Card>();
            Card card = null;
            if (type == null || type.Count() == 0)
            {
                card = GetNextCard();
            }
            else
            {
                card = _getNotSameTypeCard(2, type);
            }

            var same_typecard = _getSameTypeCard((MaJiangType)card.Type);
            if (same_typecard == null)
            {
                _giveBackCard(card);
                list_cards = _getPairExceptType(type);
            }
            else
            {
                list_cards.Add(card);
                list_cards.Add(same_typecard);
            }

            return list_cards;
        }

        //---------------------------------------------------------------------
        Card _getSameTypeCard(MaJiangType card_type)
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
        Card _getSameTypeCard(params MaJiangType[] card_type)
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
        Card _getNotSameTypeCard(int sametype_card_mincount, params MaJiangType[] card_type)
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
        Card _getNextOrLastCardTypeCard(MaJiangType card_type, MaJiangSuit card_suit, bool is_same_suit, bool is_next)
        {
            Card card = null;
            MaJiangType need_cardtype = MaJiangType.Two;
            if (is_next)
            {
                int next_cardtype = ((int)card_type) + 1;
                if (next_cardtype == 15)
                {
                    need_cardtype = MaJiangType.Two;
                }
                else
                {
                    need_cardtype = (MaJiangType)next_cardtype;
                }
            }
            else
            {
                int last_cardtype = ((int)card_type) - 1;
                if (last_cardtype == 1)
                {
                    need_cardtype = MaJiangType.Nine;
                }
                else
                {
                    need_cardtype = (MaJiangType)last_cardtype;
                }
            }

            if (is_same_suit)
            {
                List<Card> list_same_suitcards = null;
                if (mapSameSuitCard.TryGetValue(card_suit, out list_same_suitcards))
                {
                    byte need_cardtype_bte = (byte)need_cardtype;
                    var list_suit_card = list_same_suitcards.Where(x => x.Type == need_cardtype_bte);
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
        Card _getSameSuitCard(MaJiangSuit card_suit)
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
        Card _getNotSameSuitCard(int samesuit_card_mincount, params MaJiangSuit[] card_suit)
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
            mapSameTypeCard.TryGetValue((MaJiangType)card.Type, out list_sametypecard);
            if (list_sametypecard != null && !list_sametypecard.Contains(card))
            {
                list_sametypecard.Add(card);
            }

            List<Card> list_samesuitcard = null;
            mapSameSuitCard.TryGetValue((MaJiangSuit)card.Suit, out list_samesuitcard);
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
            mapSameTypeCard.TryGetValue((MaJiangType)card.Type, out list_sametypecard);
            if (list_sametypecard != null)
            {
                list_sametypecard.Remove(card);
            }

            List<Card> list_samesuitcard = null;
            mapSameSuitCard.TryGetValue((MaJiangSuit)card.Suit, out list_samesuitcard);
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