// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;

    public enum CardType : byte
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14,
    }

    public enum CardSuit : byte
    {
        Club = 0,// ♣
        Diamond = 1,// ♦
        Heart = 2,// ♥
        Spade = 3// ♠
    }

    [Serializable]
    public class CardData : IComparable<CardData>
    {
        public byte suit;
        public byte type;

        public int CompareTo(CardData other)
        {
            if (this.type > other.type)
            {
                return 1;
            }
            else if (this.type < other.type)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    // Immutable object to represent game card with suit and type.
    public class Card : IDeepCloneable<Card>
    {
        //---------------------------------------------------------------------
        public byte Suit { get; private set; }
        public byte Type { get; private set; }

        //---------------------------------------------------------------------
        public Card(byte suit, byte type)
        {
            this.Suit = suit;
            this.Type = type;
        }

        //---------------------------------------------------------------------
        public CardData GetCardData()
        {
            CardData card_data = new CardData();
            card_data.suit = Suit;
            card_data.type = Type;
            return card_data;
        }

        //---------------------------------------------------------------------
        public static Card FromHashCode(int hashCode)
        {
            var suitId = hashCode / 13;
            return new Card((byte)suitId, (byte)(hashCode - (suitId * 13) + 2));
        }

        //---------------------------------------------------------------------
        public override bool Equals(object obj)
        {
            var anotherCard = obj as Card;
            return anotherCard != null && this.Equals(anotherCard);
        }

        //---------------------------------------------------------------------
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)this.Suit * 13) + (int)this.Type - 2;
            }
        }

        //---------------------------------------------------------------------
        public Card DeepClone()
        {
            return new Card(this.Suit, this.Type);
        }

        //---------------------------------------------------------------------
        public override string ToString()
        {
            //return string.Format("{0} {1}", this.Type.ToFriendlyString(), this.Suit.ToFriendlyString());
            return string.Format("{0} {1}", this.Type, this.Suit);
        }

        //---------------------------------------------------------------------
        private bool Equals(Card other)
        {
            return this.Suit == other.Suit && this.Type == other.Type;
        }
    }
}