namespace Poker.Models
{
    using Poker.Interfaces;
    public class Dealer : IDealer
    {
        private readonly GameManager currentForm;

        public Dealer(GameManager currentForm)
        {
            this.currentForm = currentForm;
        }

        public void AddChipsIfLost()
        {
            if (this.currentForm.playerChips <= 0)
            {
                AddChipsWhenLost f2 = new AddChipsWhenLost();
                f2.ShowDialog();

                if (f2.AddChipsValue != 0)
                {
                    this.currentForm.playerChips = f2.AddChipsValue;
                    this.currentForm.botOneChips += f2.AddChipsValue;
                    this.currentForm.botTwoChips += f2.AddChipsValue;
                    this.currentForm.botThreeChips += f2.AddChipsValue;
                    this.currentForm.botFourChips += f2.AddChipsValue;
                    this.currentForm.botFiveChips += f2.AddChipsValue;
                    this.currentForm.hasPlayerBankrupted = false;
                    this.currentForm.playerTurn = true;
                    this.currentForm.playerRaiseButton.Enabled = true;
                    this.currentForm.playerFoldButton.Enabled = true;
                    this.currentForm.playerCheckButton.Enabled = true;
                    this.currentForm.playerRaiseButton.Text = "Raise";
                }
            }
        }

        public string CheckPlayerBotsStatus(string fixedLast)
        {
            if (!this.currentForm.playerStatus.Text.Contains(Winner.FoldString))
            {
                fixedLast = "Player";
                this.currentForm.CurrentRules.GameRulesCreator(0, 1, ref this.currentForm.playerType, ref this.currentForm.playerPower, this.currentForm.hasPlayerBankrupted);
            }

            if (!this.currentForm.botOneStatus.Text.Contains(Winner.FoldString))
            {
                fixedLast = "Winner 1";
                this.currentForm.CurrentRules.GameRulesCreator(2, 3, ref this.currentForm.botOneType, ref this.currentForm.botOnePower, this.currentForm.hasBotOneBankrupted);
            }

            if (!this.currentForm.botTwoStatus.Text.Contains(Winner.FoldString))
            {
                fixedLast = "Winner 2";
                this.currentForm.CurrentRules.GameRulesCreator(4, 5, ref this.currentForm.botTwoType, ref this.currentForm.botTwoPower, this.currentForm.hasBotTwoBankrupted);
            }

            if (!this.currentForm.botThreeStatus.Text.Contains(Winner.FoldString))
            {
                fixedLast = "Winner 3";
                this.currentForm.CurrentRules.GameRulesCreator(6, 7, ref this.currentForm.botThreeType, ref this.currentForm.botThreePower, this.currentForm.hasBotThreeBankrupted);
            }

            if (!this.currentForm.botFourStatus.Text.Contains(Winner.FoldString))
            {
                fixedLast = "Winner 4";
                this.currentForm.CurrentRules.GameRulesCreator(8, 9, ref this.currentForm.botFourType, ref this.currentForm.botFourPower, this.currentForm.hasBotFourBankrupted);
            }

            if (!this.currentForm.botFiveStatus.Text.Contains(Winner.FoldString))
            {
                fixedLast = "Winner 5";
                this.currentForm.CurrentRules.GameRulesCreator(10, 11, ref this.currentForm.botFiveType, ref this.currentForm.botFivePower, this.currentForm.hasBotFiveBankrupted);
            }

            return fixedLast;
        }

        public void CheckFlopTurnOrRiver()
        {
            if (this.currentForm.totalRounds == GameManager.Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    this.ResetPlayerBotsValues(j);
                }
            }

            if (this.currentForm.totalRounds == GameManager.Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    this.ResetPlayerBotsValues(j);
                }
            }

            if (this.currentForm.totalRounds == GameManager.River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    this.ResetPlayerBotsValues(j);
                }
            }
        }

        public void CheckIfSomeoneRaised(int currentTurn)
        {
            if (this.currentForm.isRaising)
            {
                this.currentForm.turnCount = 0;
                this.currentForm.isRaising = false;
                this.currentForm.raisedTurn = currentTurn;
                this.currentForm.changed = true;
            }
            else
            {
                if (this.currentForm.turnCount >= this.currentForm.maxLeft - 1 ||
                    !this.currentForm.changed && this.currentForm.turnCount == this.currentForm.maxLeft)
                {
                    if (currentTurn == this.currentForm.raisedTurn - 1 ||
                        !this.currentForm.changed && this.currentForm.turnCount == this.currentForm.maxLeft ||
                        this.currentForm.raisedTurn == 0 && currentTurn == 5)
                    {
                        this.currentForm.changed = false;
                        this.currentForm.turnCount = 0;
                        this.currentForm.Raise = 0;
                        this.currentForm.callChipsValue = 0;
                        this.currentForm.raisedTurn = 123;
                        this.currentForm.totalRounds++;

                        this.CheckIfBankrupted();
                    }
                }
            }
        }

        private void ResetPlayerBotsValues(int j)
        {
            if (this.currentForm.cardsImages[j].Image != this.currentForm.Deck[j])
            {
                this.currentForm.cardsImages[j].Image = this.currentForm.Deck[j];
                this.currentForm.playerCall = 0;
                this.currentForm.playerRaise = 0;
                this.currentForm.botOneCall = 0;
                this.currentForm.botOneRaise = 0;
                this.currentForm.botTwoCall = 0;
                this.currentForm.botTwoRaise = 0;
                this.currentForm.botThreeCall = 0;
                this.currentForm.botThreeRaise = 0;
                this.currentForm.botFourCall = 0;
                this.currentForm.botFourRaise = 0;
                this.currentForm.botFiveCall = 0;
                this.currentForm.botFiveRaise = 0;
            }
        }
        
        private void CheckIfBankrupted()
        {
            if (!this.currentForm.hasPlayerBankrupted)
            {
                this.currentForm.playerStatus.Text = "";
            }

            if (!this.currentForm.hasBotOneBankrupted)
            {
                this.currentForm.botOneStatus.Text = "";
            }

            if (!this.currentForm.hasBotTwoBankrupted)
            {
                this.currentForm.botTwoStatus.Text = "";
            }

            if (!this.currentForm.hasBotThreeBankrupted)
            {
                this.currentForm.botThreeStatus.Text = "";
            }

            if (!this.currentForm.hasBotFourBankrupted)
            {
                this.currentForm.botFourStatus.Text = "";
            }

            if (!this.currentForm.hasBotFiveBankrupted)
            {
                this.currentForm.botFiveStatus.Text = "";
            }
        }
    }
}