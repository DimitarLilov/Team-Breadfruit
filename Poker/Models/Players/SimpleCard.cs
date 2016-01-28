namespace Poker.Models.Players
{
    using Poker.Enum;
    using System;

    public class SimpleCard : IComparable<SimpleCard>
    {
        public SimpleCard(CardType type, Suit suit)
        {
            this.Type = type;
            this.Suit = suit;
        }

        public CardType Type { get; set; }

        public Suit Suit { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is SimpleCard)
            {
                var card = (SimpleCard)obj;

                return this.Type == card.Type && this.Suit == card.Suit;
            }

            return false;
        }

        public int CompareTo(SimpleCard other)
        {
            if (this.Suit == other.Suit)
            {
                return this.Type.CompareTo(other.Type);
            }

            return this.Suit.CompareTo(other.Suit);
        }

        public override int GetHashCode()
        {
            return ((int)this.Type * 8) + (int)this.Suit;
        }
    }
}
