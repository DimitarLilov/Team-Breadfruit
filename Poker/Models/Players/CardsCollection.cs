namespace Poker.Models.Players
{
    using Enum;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class CardsCollection : IList<SimpleCard>
    {
        public static readonly CardsCollection FullDeckOfCards = null;

        protected IList<SimpleCard> cards;

        static CardsCollection()
        {
            FullDeckOfCards = new CardsCollection()
                                {
                                    new SimpleCard(CardType.Ace, Suit.Clubs),
                                    new SimpleCard(CardType.Ace, Suit.Diamonds),
                                    new SimpleCard(CardType.Ace, Suit.Hearts),
                                    new SimpleCard(CardType.Ace, Suit.Spades),
                                    new SimpleCard(CardType.Two, Suit.Clubs),
                                    new SimpleCard(CardType.Two, Suit.Diamonds),
                                    new SimpleCard(CardType.Two, Suit.Hearts),
                                    new SimpleCard(CardType.Two, Suit.Spades),
                                    new SimpleCard(CardType.Three, Suit.Clubs),
                                    new SimpleCard(CardType.Three, Suit.Diamonds),
                                    new SimpleCard(CardType.Three, Suit.Hearts),
                                    new SimpleCard(CardType.Three, Suit.Spades),
                                    new SimpleCard(CardType.Four, Suit.Clubs),
                                    new SimpleCard(CardType.Four, Suit.Diamonds),
                                    new SimpleCard(CardType.Four, Suit.Hearts),
                                    new SimpleCard(CardType.Four, Suit.Spades),
                                    new SimpleCard(CardType.Five, Suit.Clubs),
                                    new SimpleCard(CardType.Five, Suit.Diamonds),
                                    new SimpleCard(CardType.Five, Suit.Hearts),
                                    new SimpleCard(CardType.Five, Suit.Spades),
                                    new SimpleCard(CardType.Six, Suit.Clubs),
                                    new SimpleCard(CardType.Six, Suit.Diamonds),
                                    new SimpleCard(CardType.Six, Suit.Hearts),
                                    new SimpleCard(CardType.Six, Suit.Spades),
                                    new SimpleCard(CardType.Seven, Suit.Clubs),
                                    new SimpleCard(CardType.Seven, Suit.Diamonds),
                                    new SimpleCard(CardType.Seven, Suit.Hearts),
                                    new SimpleCard(CardType.Seven, Suit.Spades),
                                    new SimpleCard(CardType.Eight, Suit.Clubs),
                                    new SimpleCard(CardType.Eight, Suit.Diamonds),
                                    new SimpleCard(CardType.Eight, Suit.Hearts),
                                    new SimpleCard(CardType.Eight, Suit.Spades),
                                    new SimpleCard(CardType.Nine, Suit.Clubs),
                                    new SimpleCard(CardType.Nine, Suit.Diamonds),
                                    new SimpleCard(CardType.Nine, Suit.Hearts),
                                    new SimpleCard(CardType.Nine, Suit.Spades),
                                    new SimpleCard(CardType.Ten, Suit.Clubs),
                                    new SimpleCard(CardType.Ten, Suit.Diamonds),
                                    new SimpleCard(CardType.Ten, Suit.Hearts),
                                    new SimpleCard(CardType.Ten, Suit.Spades),
                                    new SimpleCard(CardType.Jack, Suit.Clubs),
                                    new SimpleCard(CardType.Jack, Suit.Diamonds),
                                    new SimpleCard(CardType.Jack, Suit.Hearts),
                                    new SimpleCard(CardType.Jack, Suit.Spades),
                                    new SimpleCard(CardType.Queen, Suit.Clubs),
                                    new SimpleCard(CardType.Queen, Suit.Diamonds),
                                    new SimpleCard(CardType.Queen, Suit.Hearts),
                                    new SimpleCard(CardType.Queen, Suit.Spades),
                                    new SimpleCard(CardType.King, Suit.Clubs),
                                    new SimpleCard(CardType.King, Suit.Diamonds),
                                    new SimpleCard(CardType.King, Suit.Hearts),
                                    new SimpleCard(CardType.King, Suit.Spades),
                                };
        }

        public CardsCollection()
        {
            this.cards = new List<SimpleCard>();
        }

        public CardsCollection(int capacity)
        {
            this.cards = new List<SimpleCard>(capacity);
        }

        public SimpleCard this[int index]
        {
            get
            {
                return this.cards[index];
            }

            set
            {
                this.cards[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return this.cards.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public void Add(SimpleCard item)
        {
            this.cards.Add(item);
        }

        public void Clear()
        {
            this.cards.Clear();
        }

        public bool Contains(SimpleCard item)
        {
            return this.cards.Contains(item);
        }

        public void CopyTo(SimpleCard[] array, int arrayIndex)
        {
            this.cards.CopyTo(array, arrayIndex);
        }

        public IEnumerator<SimpleCard> GetEnumerator()
        {
            return this.cards.GetEnumerator();
        }

        public int IndexOf(SimpleCard item)
        {
            return this.cards.IndexOf(item);
        }

        public void Insert(int index, SimpleCard item)
        {
            this.cards.Insert(index, item);
        }

        public bool Remove(SimpleCard item)
        {
            var removed = this.cards.Remove(item);

            return removed;
        }

        public void RemoveAt(int index)
        {
            this.cards.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerable<SimpleCard> Sort()
        {
            var sortedCards = cards.OrderBy(card => card.Type);
            return sortedCards;
        }
    }
}
