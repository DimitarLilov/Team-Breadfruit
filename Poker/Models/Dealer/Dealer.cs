namespace Poker.Models
{
    using Poker;
    using Interfaces;

    using Constants;

    public class Dealer : IDealer
    {
        private readonly GameManager currentForm;
        public bool hasExecutedFlop;
        private bool hasExecutedTurn;
        private bool hasExecutedRiver;

        public Dealer(GameManager currentForm)
        {
            this.currentForm = currentForm;
        }

        public bool HasExecutedFlop
        {
            get { return hasExecutedFlop; }
            private set { hasExecutedFlop = value; }
        }

        public bool HasExecutedTurn
        {
            get { return hasExecutedTurn; }
            private set { hasExecutedTurn = value; }
        }

        public bool HasExecutedRiver
        {
            get { return hasExecutedRiver; }
            private set { hasExecutedRiver = value; }
        }

        public void AddChipsIfLost()
        {
            if (this.currentForm.playerChips <= 0)
            {
                AddChipsWhenLost AchipsWhenLost = new AddChipsWhenLost();
                AchipsWhenLost.ShowDialog();

                if (AchipsWhenLost.AddChipsValue != 0)
                {
                    this.currentForm.playerChips = AchipsWhenLost.AddChipsValue;
                    this.currentForm.botOneChips += AchipsWhenLost.AddChipsValue;
                    this.currentForm.botTwoChips += AchipsWhenLost.AddChipsValue;
                    this.currentForm.botThreeChips += AchipsWhenLost.AddChipsValue;
                    this.currentForm.botFourChips += AchipsWhenLost.AddChipsValue;
                    this.currentForm.botFiveChips += AchipsWhenLost.AddChipsValue;
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
            if (!this.currentForm.playerStatus.Text.Contains(Constant.Fold))
            {
                fixedLast = Constant.Player;
                this.currentForm.CurrentRules.GameRulesCreator(0, 1, ref this.currentForm.playerType, ref this.currentForm.playerPower, this.currentForm.hasPlayerBankrupted);
            }

            if (!this.currentForm.botOneStatus.Text.Contains(Constant.Fold))
            {
                fixedLast = Constant.Winner1;
                this.currentForm.CurrentRules.GameRulesCreator(2, 3, ref this.currentForm.botOneType, ref this.currentForm.botOnePower, this.currentForm.hasBotOneBankrupted);
            }

            if (!this.currentForm.botTwoStatus.Text.Contains(Constant.Fold))
            {
                fixedLast = Constant.Winner2;
                this.currentForm.CurrentRules.GameRulesCreator(4, 5, ref this.currentForm.botTwoType, ref this.currentForm.botTwoPower, this.currentForm.hasBotTwoBankrupted);
            }

            if (!this.currentForm.botThreeStatus.Text.Contains(Constant.Fold))
            {
                fixedLast = Constant.Winner3;
                this.currentForm.CurrentRules.GameRulesCreator(6, 7, ref this.currentForm.botThreeType, ref this.currentForm.botThreePower, this.currentForm.hasBotThreeBankrupted);
            }

            if (!this.currentForm.botFourStatus.Text.Contains(Constant.Fold))
            {
                fixedLast = Constant.Winner4;
                this.currentForm.CurrentRules.GameRulesCreator(8, 9, ref this.currentForm.botFourType, ref this.currentForm.botFourPower, this.currentForm.hasBotFourBankrupted);
            }

            if (!this.currentForm.botFiveStatus.Text.Contains(Constant.Fold))
            {
                fixedLast = Constant.Winner5;
                this.currentForm.CurrentRules.GameRulesCreator(10, 11, ref this.currentForm.botFiveType, ref this.currentForm.botFivePower, this.currentForm.hasBotFiveBankrupted);
            }

            return fixedLast;
        }

        public void CheckFlopTurnOrRiver()
        {
            if (this.currentForm.totalRounds == Constant.Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    this.ResetPlayerBotsValues(j);
                }

                this.HasExecutedFlop = true;
            }

            if (this.currentForm.totalRounds == Constant.Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    this.ResetPlayerBotsValues(j);
                }

                this.HasExecutedTurn = true;
            }

            if (this.currentForm.totalRounds == Constant.River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    this.ResetPlayerBotsValues(j);
                }

                this.hasExecutedRiver = true;
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
                this.currentForm.playerStatus.Text = string.Empty;
            }

            if (!this.currentForm.hasBotOneBankrupted)
            {
                this.currentForm.botOneStatus.Text = string.Empty;
            }

            if (!this.currentForm.hasBotTwoBankrupted)
            {
                this.currentForm.botTwoStatus.Text = string.Empty;
            }

            if (!this.currentForm.hasBotThreeBankrupted)
            {
                this.currentForm.botThreeStatus.Text = string.Empty;
            }

            if (!this.currentForm.hasBotFourBankrupted)
            {
                this.currentForm.botFourStatus.Text = string.Empty;
            }

            if (!this.currentForm.hasBotFiveBankrupted)
            {
                this.currentForm.botFiveStatus.Text = string.Empty;
            }
        }
    }
}