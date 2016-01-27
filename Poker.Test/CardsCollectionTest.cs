namespace Poker.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Models.Players;
    using Poker.Enum;
    using System.Collections.Generic;
    using Extentions;

    [TestClass]
    public class CardsCollectionTest
    {
        CardsCollection fullDeck;

        [TestInitialize]
        public void TestInitialize()
        {
            fullDeck = CardsCollection.FullDeckOfCards;
        }

        [TestMethod]
        public void TestDeck_ContainsAllCards_ShouldPass()
        {
            Assert.AreEqual(fullDeck.Count, 52);
        }
        
        [TestMethod]
        public void TestDeck_SortCards_ShouldPass()
        {
            var shufledCards = new Queue<SimpleCard>(fullDeck);
            shufledCards.Shuffle();

            var deck = new CardsCollection();
            foreach (var card in shufledCards)
            {
                deck.Add(card);
            }
            System.Console.WriteLine();
            var sortedDeck = deck.Sort();
            SimpleCard checkCard = null;
            foreach (var item in sortedDeck)
            {
                checkCard = item as SimpleCard;
                break;
            }
            SimpleCard firstCard = new SimpleCard(CardType.Two, Suit.Clubs);
            bool areSame = checkCard.Equals(firstCard);
            Assert.IsTrue(areSame);
        }
    }
}
