using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Models.Players
{
    class CardsCollection : IList<Card>
    {
        public static readonly CardsCollection FullDeckOfCards = null;

        protected IList<Card> cards;

        static CardsCollection()
        {
            FullDeckOfCards = new CardsCollection()
                {
                    //TODO initiliazie full deck of cards 52 of them
                };
        }

        public CardsCollection()
        {
            this.cards = new List<Card>();
        }

        public Card this[int index]
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

        public void Add(Card item)
        {
            this.cards.Add(item);
        }

        public void Clear()
        {
            this.cards.Clear();
        }

        public bool Contains(Card item)
        {
            return this.cards.Contains(item);
        }

        public void CopyTo(Card[] array, int arrayIndex)
        {
            this.cards.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return this.cards.GetEnumerator();
        }

        public int IndexOf(Card item)
        {
            return this.cards.IndexOf(item);
        }

        public void Insert(int index, Card item)
        {
            this.cards.Insert(index, item);
        }

        public bool Remove(Card item)
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
    }
}
