namespace Poker.Test.BotTests
{
    using System;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.Players.Bot;

    [TestClass]
    public class CheckBotsHandTests
    {
        private Bot bot;
        private int sampleBotsFirstCard;
        private int sampleBotsSecondCard;
        private int sampleBotChips;
        private bool sampleIsBotTurn;
        private bool sampleHasBotFolded;
        private Label sampleBotStatus;

        double sampleBotPower = 5;
        double sampleBotCurrent = -1;

        [TestInitialize]
        public void TestInitialize()
        {
            this.bot = new Bot();
            this.sampleBotStatus = new Label();
            this.sampleBotStatus.Text = "Check";
            this.sampleBotsFirstCard = 2;
            this.sampleBotsSecondCard = 3;
            this.sampleBotChips = 10000;
            this.sampleIsBotTurn = true;
            this.sampleHasBotFolded = false;
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void CheckBotsHandShould_Call_CheckBotsHighCard_WhenTheCurrentIsMinusOne()
        {
            this.bot.CheckBotsHand(this.sampleBotsFirstCard,
                this.sampleBotsSecondCard,
                ref this.sampleBotChips,
                ref this.sampleIsBotTurn,
                ref this.sampleHasBotFolded,
                this.sampleBotStatus,
                this.sampleBotPower,
                this.sampleBotCurrent);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void CheckBotsHandShould_ReturnANull_ReferenceException_WhenBotFirstPossibility_IsAccessed()
        {
            this.sampleBotCurrent = 0;

            this.bot.CheckBotsHand(this.sampleBotsFirstCard,
                this.sampleBotsSecondCard,
                ref this.sampleBotChips,
                ref this.sampleIsBotTurn,
                ref this.sampleHasBotFolded,
                this.sampleBotStatus,
                this.sampleBotPower,
                this.sampleBotCurrent);
        }

        [TestMethod]
        public void CheckBotsHandShould_ReturnTrue_WhenCheckBotsPairHandMethod_IsAccessed()
        {
            this.sampleBotCurrent = 1;
            var expected = true;

            this.bot.CheckBotsHand(this.sampleBotsFirstCard,
                this.sampleBotsSecondCard,
                ref this.sampleBotChips,
                ref this.sampleIsBotTurn,
                ref this.sampleHasBotFolded,
                this.sampleBotStatus,
                this.sampleBotPower,
                this.sampleBotCurrent);
            

            Assert.AreEqual(expected, this.bot.UnitTest);
        }

        [TestMethod]
        public void CheckBotsHandShould_ReturnTrue_WhenCheckBotsTwoPairMethod_IsAccessed()
        {
            this.sampleBotCurrent = 2;
            var expected = true;

            this.bot.CheckBotsHand(this.sampleBotsFirstCard,
                this.sampleBotsSecondCard,
                ref this.sampleBotChips,
                ref this.sampleIsBotTurn,
                ref this.sampleHasBotFolded,
                this.sampleBotStatus,
                this.sampleBotPower,
                this.sampleBotCurrent);

            Assert.AreEqual(expected, this.bot.UnitTest);
        }

        [TestMethod]
        public void CheckBotsHandShould_ReturnTrue_WhenCheckBotsThreeOfAKindMethod_IsAccessed()
        {
            this.sampleBotCurrent = 3;
            var expected = true;

            this.bot.CheckBotsHand(this.sampleBotsFirstCard,
                this.sampleBotsSecondCard,
                ref this.sampleBotChips,
                ref this.sampleIsBotTurn,
                ref this.sampleHasBotFolded,
                this.sampleBotStatus,
                this.sampleBotPower,
                this.sampleBotCurrent);

            Assert.AreEqual(expected, this.bot.UnitTest);
        }

        [TestMethod]
        public void CheckBotsHandShould_ReturnTrue_WhenCheckBotsStraightMethod_IsAccessed()
        {
            this.sampleBotCurrent = 4;
            var expected = true;

            this.bot.CheckBotsHand(this.sampleBotsFirstCard,
                this.sampleBotsSecondCard,
                ref this.sampleBotChips,
                ref this.sampleIsBotTurn,
                ref this.sampleHasBotFolded,
                this.sampleBotStatus,
                this.sampleBotPower,
                this.sampleBotCurrent);

            Assert.AreEqual(expected, this.bot.UnitTest);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CheckBotsHandShould_ReturnNullException_WhenCheckBotsFlushMethod_IsAccessed()
        {
            this.sampleBotCurrent = 5;

            this.bot.CheckBotsHand(this.sampleBotsFirstCard,
                this.sampleBotsSecondCard,
                ref this.sampleBotChips,
                ref this.sampleIsBotTurn,
                ref this.sampleHasBotFolded,
                this.sampleBotStatus,
                this.sampleBotPower,
                this.sampleBotCurrent);
        }

        [TestMethod]
        public void CheckBotsHandShould_ReturnTrue_WhenCheckBotsFullHouseMethod_IsAccessed()
        {
            this.sampleBotCurrent = 6;

            this.bot.CheckBotsHand(this.sampleBotsFirstCard,
                this.sampleBotsSecondCard,
                ref this.sampleBotChips,
                ref this.sampleIsBotTurn,
                ref this.sampleHasBotFolded,
                this.sampleBotStatus,
                this.sampleBotPower,
                this.sampleBotCurrent);
            var expected = true;

            Assert.AreEqual(expected, this.bot.UnitTest);
        }

        [TestMethod]
        public void CheckBotsHandShould_ReturnTrue_WhenCheckBotsFourOfAKindMethod_IsAccessed()
        {
            this.sampleBotCurrent = 7;

            this.bot.CheckBotsHand(this.sampleBotsFirstCard,
                this.sampleBotsSecondCard,
                ref this.sampleBotChips,
                ref this.sampleIsBotTurn,
                ref this.sampleHasBotFolded,
                this.sampleBotStatus,
                this.sampleBotPower,
                this.sampleBotCurrent);
            var expected = true;

            Assert.AreEqual(expected, this.bot.UnitTest);
        }

        [TestMethod]
        public void CheckBotsHandShould_ReturnTrue_WhenCheckBotsStraightFlushMethod_IsAccessed()
        {
            this.sampleBotCurrent = 8;
            var expected = true;

            this.bot.CheckBotsHand(this.sampleBotsFirstCard,
                this.sampleBotsSecondCard,
                ref this.sampleBotChips,
                ref this.sampleIsBotTurn,
                ref this.sampleHasBotFolded,
                this.sampleBotStatus,
                this.sampleBotPower,
                this.sampleBotCurrent);

            Assert.AreEqual(expected, this.bot.UnitTest);
        }
    }
}
