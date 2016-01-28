namespace Poker.Test
{
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Poker.Models;

    [TestClass]
    public class DealerTests
    {
        private Dealer dealer;
        private GameManager gameManager;

        [TestInitialize]
        public void TestInitialize()
        {
            this.gameManager = new GameManager();
            this.dealer = new Dealer(this.gameManager);
        }

        [TestMethod]
        public void CheckFlopTurnOrRiverShould_ReturnTrue_When_Values_Are_SucsuccessfullyResetted_OnFLop()
        {
            var expectedOutput = true;
            this.gameManager.totalRounds = 1;
            this.gameManager.cardsImages[12] = new PictureBox();
            this.gameManager.cardsImages[13] = new PictureBox();
            this.gameManager.cardsImages[14] = new PictureBox();

            this.dealer.CheckFlopTurnOrRiver();

            Assert.AreEqual(expectedOutput, this.dealer.HasExecutedFlop);
        }

        [TestMethod]
        public void CheckFlopTurnOrRiverShould_ReturnTrue_When_Values_Are_SucsuccessfullyResetted_OnTurn()
        {
            var expectedOutput = true;
            this.gameManager.totalRounds = 2;
            this.gameManager.cardsImages[14] = new PictureBox();
            this.gameManager.cardsImages[15] = new PictureBox();
            this.gameManager.cardsImages[16] = new PictureBox();

            this.dealer.CheckFlopTurnOrRiver();

            Assert.AreEqual(expectedOutput, this.dealer.HasExecutedTurn);
        }

        [TestMethod]
        public void CheckFlopTurnOrRiverShould_ReturnTrue_When_Values_Are_SucsuccessfullyResetted_OnRiver()
        {
            var expectedOutput = true;
            this.gameManager.totalRounds = 3;
            this.gameManager.cardsImages[15] = new PictureBox();
            this.gameManager.cardsImages[16] = new PictureBox();
            this.gameManager.cardsImages[17] = new PictureBox();

            this.dealer.CheckFlopTurnOrRiver();

            Assert.AreEqual(expectedOutput, this.dealer.HasExecutedRiver);
        }

        [TestMethod]
        public void CheckIfSomeoneRaisedTurnCount_ShouldReturnZeroWhen_isRaising_IsTrue()
        {
            var expected = 0;
            var secondTestExpected = false;
            var currentTurn = 0;

            this.gameManager.isRaising = true;

            this.dealer.CheckIfSomeoneRaised(currentTurn);
            Assert.AreEqual(expected, this.gameManager.turnCount);
            Assert.AreEqual(secondTestExpected, this.gameManager.isRaising);
        }

        [TestMethod]
        public void CheckIfSomeoneRaisedShould_Return_123_WhenTheCurrentTurnCount_IsMoreThanTheMaxLeft()
        {
            var expected = 123;
            var currentTurn = 2;

            this.gameManager.isRaising = false;
            this.gameManager.maxLeft = 0;
            this.gameManager.turnCount = 2;
            this.gameManager.raisedTurn = 3;

            this.dealer.CheckIfSomeoneRaised(currentTurn);
            Assert.AreEqual(expected, this.gameManager.raisedTurn);
        }

    }
}
