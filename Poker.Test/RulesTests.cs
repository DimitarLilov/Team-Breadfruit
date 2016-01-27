
namespace Poker.Test
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Models.Rules;

    [TestClass]
    public class RulesTests
    {
        private Rules rules;
        private GameManager gameManager;
        private int sampleCardOne;
        private int sampleCardTwo;
        private double sampleCurrent;
        private double samplePower;
        private bool sampleHasBankrupted;

        [TestInitialize]
        public void TestInitialize()
        {
            this.gameManager = new GameManager();
            this.rules = new Rules(this.gameManager);
            this.sampleCardOne = 0;
            this.sampleCardTwo = 0;
            this.sampleCurrent = -1;
            this.samplePower = 0;
            this.sampleHasBankrupted = false;
        }

        [TestMethod]
        public void GameRulesCreatorShould_TriggerAllVariationsOfHandsAndReturnTrue_IfSucsuccessfully_Accessed()
        {
            this.sampleCurrent = 3;
            this.gameManager.cardsImages[sampleCardOne].Tag = new int();
            this.gameManager.cardsImages[sampleCardTwo].Tag = new int();

            this.rules.GameRulesCreator(this.sampleCardOne,
                this.sampleCardTwo,
                ref this.sampleCurrent,
                ref this.samplePower,
                this.sampleHasBankrupted);

            var expected = true;
            var actual = this.rules.UniteTest;

            Assert.AreEqual(expected, actual, "The GameRulesCreator isn't checking the types of hands.");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GameRulesCreator_Should_ReturnArgumentNullExceptionInCase_TheTagsAreWrong()
        {
            this.sampleCurrent = 4;
            this.rules.GameRulesCreator(this.sampleCardOne,
                this.sampleCardTwo,
                ref this.sampleCurrent,
                ref this.samplePower,
                this.sampleHasBankrupted);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void GameRulesCreatorShould_Return_IncorrectFormatException_InCase_Tags_Are_NotIntegers()
        {
            this.sampleCurrent = 6;
            this.gameManager.cardsImages[sampleCardOne].Tag = "sampleTestOne";
            this.gameManager.cardsImages[sampleCardTwo].Tag = "sampleTestTwo";

            this.rules.GameRulesCreator(this.sampleCardOne,
                this.sampleCardTwo,
                ref this.sampleCurrent,
                ref this.samplePower,
                this.sampleHasBankrupted);
        }

        [TestMethod]
        public void GameRulesCreatorShould_ReturnTrueForAllTheMethods_UserSucsuccessfullyEnters()
        {
            this.sampleCurrent = 7;
            this.gameManager.cardsImages[sampleCardOne].Tag = new int();
            this.gameManager.cardsImages[sampleCardTwo].Tag = new int();
            var expected = true;

            this.rules.GameRulesCreator(this.sampleCardOne,
                this.sampleCardTwo,
                ref this.sampleCurrent,
                ref this.samplePower,
                this.sampleHasBankrupted);

            Assert.AreEqual(expected, this.rules.HasSucsuccessfullyExecutedRulesTwoPair, "Unable to get into the RulesTwoPair method.");
            //Assert.AreEqual(expected, this.rules.HasSucsuccessfullyExecutedRulesPairFromHand, "Unable to get into the RulesPairFromHand method.");
            Assert.AreEqual(expected, this.rules.HasSucsuccessfullyExecutedRulesStraight, "Unable to get into the RulesStraight method.");
            Assert.AreEqual(expected, this.rules.HasSucsuccessfullyExecutedRulesThreeOfAKind, "Unable to get into the RulesThreeOfAKind method.");
            Assert.AreEqual(expected, this.rules.HasSucsuccessfullyExecutedRulesFullHouse, "Unable to get into the RulesFullHouse method.");
            Assert.AreEqual(expected, this.rules.HasSucsuccessfullyExecutedRulesStraightFlush, "Unable to get into the RulesStraightFlush method.");
            Assert.AreEqual(expected, this.rules.HasSucsuccessfullyExecutedRulesFourOfAKind, "Unable to get into the RulesFourOfAKind method.");
            Assert.AreEqual(expected, this.rules.HasSucsuccessfullyExecutedRulesHighCard, "Unable to get into the RulesHighCard method.");
            Assert.AreEqual(expected, this.rules.HasSucsuccessfullyExecutedRulesFlush, "Unable to get into the RulesFlush method.");
        }
    }
}
