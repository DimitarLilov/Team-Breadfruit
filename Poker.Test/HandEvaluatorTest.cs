namespace Poker.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Enum;
    using Models;
    using Models.Players;

    [TestClass]
    public class HandEvaluatorTest
    {
        [TestMethod]
        public void Test_CheckFourOfKindHand_ShouldPass()
        {
            var card1 = new SimpleCard(CardType.Ace, Suit.Clubs);
            var card2 = new SimpleCard(CardType.Ace, Suit.Diamonds);
            var card3 = new SimpleCard(CardType.Ace, Suit.Hearts);
            var card4 = new SimpleCard(CardType.Ace, Suit.Spades);
            var card5 = new SimpleCard(CardType.Five, Suit.Spades);
            var card6 = new SimpleCard(CardType.Four, Suit.Spades);
            var card7 = new SimpleCard(CardType.Two, Suit.Spades);
            var hand = new Hand();
            hand.Add(card1);
            hand.Add(card2);
            hand.Add(card3);
            hand.Add(card4);
            hand.Add(card5);
            hand.Add(card6);
            hand.Add(card7);
            var sortedHand = hand.Sort();
            var firstHand = new HandEvaluator(sortedHand);
            var result = firstHand.EvaluateHand();

            Assert.AreEqual(HandStrength.FourKind, result);
        }

        [TestMethod]
        public void Test_CheckStraightHand_ShouldPass()
        {
            var card1 = new SimpleCard(CardType.Two, Suit.Clubs);
            var card2 = new SimpleCard(CardType.Three, Suit.Diamonds);
            var card3 = new SimpleCard(CardType.Four, Suit.Hearts);
            var card4 = new SimpleCard(CardType.Five, Suit.Spades);
            var card5 = new SimpleCard(CardType.Six, Suit.Spades);
            var card6 = new SimpleCard(CardType.Ten, Suit.Spades);
            var card7 = new SimpleCard(CardType.Ace, Suit.Spades);
            var hand = new Hand();
            hand.Add(card1);
            hand.Add(card2);
            hand.Add(card3);
            hand.Add(card4);
            hand.Add(card5);
            hand.Add(card6);
            hand.Add(card7);
            var sortedHand = hand.Sort();
            var firstHand = new HandEvaluator(sortedHand);
            var result = firstHand.EvaluateHand();

            Assert.AreEqual(HandStrength.Straight, result);
        }

        [TestMethod]
        public void Test_CheckFullHouseHand_ShouldPass()
        {
            var card1 = new SimpleCard(CardType.Ace, Suit.Clubs);
            var card2 = new SimpleCard(CardType.Ace, Suit.Diamonds);
            var card3 = new SimpleCard(CardType.Ace, Suit.Hearts);
            var card4 = new SimpleCard(CardType.Nine, Suit.Spades);
            var card5 = new SimpleCard(CardType.Nine, Suit.Diamonds);
            var card6 = new SimpleCard(CardType.Two, Suit.Diamonds);
            var card7 = new SimpleCard(CardType.Three, Suit.Diamonds);
            var hand = new Hand();
            hand.Add(card1);
            hand.Add(card2);
            hand.Add(card3);
            hand.Add(card4);
            hand.Add(card5);
            hand.Add(card6);
            hand.Add(card7);
            var sortedHand = hand.Sort();
            var firstHand = new HandEvaluator(sortedHand);
            var result = firstHand.EvaluateHand();

            Assert.AreEqual(HandStrength.FullHouse, result);
        }

        [TestMethod]
        public void Test_CheckThreeKindHand_ShouldPass()
        {
            var card1 = new SimpleCard(CardType.Ace, Suit.Clubs);
            var card2 = new SimpleCard(CardType.Ace, Suit.Diamonds);
            var card3 = new SimpleCard(CardType.Ace, Suit.Hearts);
            var card4 = new SimpleCard(CardType.Eight, Suit.Spades);
            var card5 = new SimpleCard(CardType.Nine, Suit.Diamonds);
            var card6 = new SimpleCard(CardType.Five, Suit.Diamonds);
            var card7 = new SimpleCard(CardType.Two, Suit.Diamonds);
            var hand = new Hand();
            hand.Add(card1);
            hand.Add(card2);
            hand.Add(card3);
            hand.Add(card4);
            hand.Add(card5);
            hand.Add(card6);
            hand.Add(card7);
            var sortedHand = hand.Sort();
            var firstHand = new HandEvaluator(sortedHand);
            var result = firstHand.EvaluateHand();

            Assert.AreEqual(HandStrength.ThreeKind, result);
        }

        [TestMethod]
        public void Test_CheckTwoPairsHand_ShouldPass()
        {
            var card1 = new SimpleCard(CardType.Ace, Suit.Clubs);
            var card2 = new SimpleCard(CardType.Ace, Suit.Diamonds);
            var card3 = new SimpleCard(CardType.King, Suit.Hearts);
            var card4 = new SimpleCard(CardType.Nine, Suit.Spades);
            var card5 = new SimpleCard(CardType.Nine, Suit.Diamonds);
            var card6 = new SimpleCard(CardType.Five, Suit.Diamonds);
            var card7 = new SimpleCard(CardType.Six, Suit.Diamonds);
            var hand = new Hand();
            hand.Add(card1);
            hand.Add(card2);
            hand.Add(card3);
            hand.Add(card4);
            hand.Add(card5);
            hand.Add(card6);
            hand.Add(card7);
            var sortedHand = hand.Sort();
            var firstHand = new HandEvaluator(sortedHand);
            var result = firstHand.EvaluateHand();

            Assert.AreEqual(HandStrength.TwoPairs, result);
        }

        [TestMethod]
        public void Test_CheckOnePairHand_ShouldPass()
        {
            var card1 = new SimpleCard(CardType.Ace, Suit.Clubs);
            var card2 = new SimpleCard(CardType.Ace, Suit.Diamonds);
            var card3 = new SimpleCard(CardType.Jack, Suit.Hearts);
            var card4 = new SimpleCard(CardType.Queen, Suit.Spades);
            var card5 = new SimpleCard(CardType.Nine, Suit.Diamonds);
            var card6 = new SimpleCard(CardType.Two, Suit.Diamonds);
            var card7 = new SimpleCard(CardType.Four, Suit.Diamonds);
            var hand = new Hand();
            hand.Add(card1);
            hand.Add(card2);
            hand.Add(card3);
            hand.Add(card4);
            hand.Add(card5);
            hand.Add(card6);
            hand.Add(card7);
            var sortedHand = hand.Sort();
            var firstHand = new HandEvaluator(sortedHand);
            var result = firstHand.EvaluateHand();

            Assert.AreEqual(HandStrength.OnePair, result);
        }

        [TestMethod]
        public void Test_CheckNothingHand_ShouldPass()
        {
            var card1 = new SimpleCard(CardType.Two, Suit.Clubs);
            var card2 = new SimpleCard(CardType.Three, Suit.Diamonds);
            var card3 = new SimpleCard(CardType.Four, Suit.Hearts);
            var card4 = new SimpleCard(CardType.Five, Suit.Spades);
            var card5 = new SimpleCard(CardType.Nine, Suit.Diamonds);
            var card6 = new SimpleCard(CardType.Ten, Suit.Diamonds);
            var card7 = new SimpleCard(CardType.Ace, Suit.Diamonds);

            var hand = new Hand();
            hand.Add(card1);
            hand.Add(card2);
            hand.Add(card3);
            hand.Add(card4);
            hand.Add(card5);
            hand.Add(card6);
            hand.Add(card7);
            var sortedHand = hand.Sort();
            var firstHand = new HandEvaluator(sortedHand);
            var result = firstHand.EvaluateHand();

            Assert.AreEqual(HandStrength.Nothing, result);
        }

        [TestMethod]
        public void Test_CheckHighCardHand_ShouldPass()
        {
            var card1 = new SimpleCard(CardType.Two, Suit.Clubs);
            var card2 = new SimpleCard(CardType.Four, Suit.Diamonds);
            var card3 = new SimpleCard(CardType.Five, Suit.Hearts);
            var card4 = new SimpleCard(CardType.Six, Suit.Spades);
            var card5 = new SimpleCard(CardType.Eight, Suit.Diamonds);
            var card6 = new SimpleCard(CardType.Ace, Suit.Diamonds);
            var card7 = new SimpleCard(CardType.Ten, Suit.Diamonds);

            var hand = new Hand();

            hand.Add(card1);
            hand.Add(card2);
            hand.Add(card3);
            hand.Add(card4);
            hand.Add(card5);
            hand.Add(card6);
            hand.Add(card7);

            var sortedHand = hand.Sort();
            var firstHand = new HandEvaluator(sortedHand);
            firstHand.EvaluateHand();

            bool highCard = firstHand.HandValue.HighCard.Equals((int)card6.Type);

            Assert.IsTrue(highCard);
        }
    }
}
