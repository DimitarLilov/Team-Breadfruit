namespace Poker
{
    public class CheckRaiseDealer
    {
        private Form1 currentForm;

        public CheckRaiseDealer(Form1 currentForm)
        {
            this.currentForm = currentForm;
        }

        public void AddChipsIfLost()
        {
            if (currentForm.playerChips <= 0)
            {
                AddChipsWhenLost f2 = new AddChipsWhenLost();
                f2.ShowDialog();

                if (f2.AddChipsValue != 0)
                {
                    currentForm.playerChips = f2.AddChipsValue;
                    currentForm.botOnehips += f2.AddChipsValue;
                    currentForm.botTwoChips += f2.AddChipsValue;
                    currentForm.botThreeChips += f2.AddChipsValue;
                    currentForm.botFourChips += f2.AddChipsValue;
                    currentForm.botFiveChips += f2.AddChipsValue;
                    currentForm.hasPlayerBankrupted = false;
                    currentForm.playerTurn = true;
                    currentForm.playerRaiseButton.Enabled = true;
                    currentForm.playerFoldButton.Enabled = true;
                    currentForm.playerCheckButton.Enabled = true;
                    currentForm.playerRaiseButton.Text = "Raise";
                }
            }
        }

        public string CheckPlayerBotsStatus(string fixedLast)
        {
            if (!currentForm.playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                currentForm.CurrentRules.GameRules(0, 1, ref currentForm.playerType, ref currentForm.playerPower, currentForm.hasPlayerBankrupted);
            }

            if (!currentForm.botOneStatus.Text.Contains("Fold"))
            {
                fixedLast = "Winner 1";
                currentForm.CurrentRules.GameRules(2, 3, ref currentForm.botOneType, ref currentForm.botOnePower, currentForm.hasBotOneBankrupted);
            }

            if (!currentForm.botTwoStatus.Text.Contains("Fold"))
            {
                fixedLast = "Winner 2";
                currentForm.CurrentRules.GameRules(4, 5, ref currentForm.botTwoType, ref currentForm.botTwoPower, currentForm.hasBotTwoBankrupted);
            }

            if (!currentForm.botThreeStatus.Text.Contains("Fold"))
            {
                fixedLast = "Winner 3";
                currentForm.CurrentRules.GameRules(6, 7, ref currentForm.botThreeType, ref currentForm.botThreePower, currentForm.hasBotThreeBankrupted);
            }

            if (!currentForm.botFourStatus.Text.Contains("Fold"))
            {
                fixedLast = "Winner 4";
                currentForm.CurrentRules.GameRules(8, 9, ref currentForm.botFourType, ref currentForm.botFourPower, currentForm.hasBotFourBankrupted);
            }

            if (!currentForm.botFiveStatus.Text.Contains("Fold"))
            {
                fixedLast = "Winner 5";
                currentForm.CurrentRules.GameRules(10, 11, ref currentForm.botFiveType, ref currentForm.botFivePower, currentForm.hasBotFiveBankrupted);
            }
            return fixedLast;
        }

        public void CheckFlopTurnOrRiver()
        {
            if (currentForm.totalRounds == Form1.Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    ResetPlayerBotsValues(j);
                }
            }

            if (currentForm.totalRounds == Form1.Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    ResetPlayerBotsValues(j);
                }
            }

            if (currentForm.totalRounds == Form1.River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    ResetPlayerBotsValues(j);
                }
            }
        }

        private void ResetPlayerBotsValues(int j)
        {
            if (currentForm.cardsImages[j].Image != currentForm.Deck[j])
            {
                currentForm.cardsImages[j].Image = currentForm.Deck[j];
                currentForm.playerCall = 0;
                currentForm.playerRaise = 0;
                currentForm.botOneCall = 0;
                currentForm.botOneRaise = 0;
                currentForm.botTwoCall = 0;
                currentForm.botTwoRaise = 0;
                currentForm.botThreeCall = 0;
                currentForm.botThreeRaise = 0;
                currentForm.botFourCall = 0;
                currentForm.botFourRaise = 0;
                currentForm.botFiveCall = 0;
                currentForm.botFiveRaise = 0;
            }
        }

        public void CheckIfSomeoneRaised(int currentTurn)
        {
            if (currentForm.isRaising)
            {
                currentForm.turnCount = 0;
                currentForm.isRaising = false;
                currentForm.raisedTurn = currentTurn;
                currentForm.changed = true;
            }

            else
            {
                if (currentForm.turnCount >= currentForm.maxLeft - 1 ||
                    !currentForm.changed && currentForm.turnCount == currentForm.maxLeft)
                {
                    if (currentTurn == currentForm.raisedTurn - 1 ||
                        !currentForm.changed && currentForm.turnCount == currentForm.maxLeft || currentForm.raisedTurn == 0 && currentTurn == 5)
                    {
                        currentForm.changed = false;
                        currentForm.turnCount = 0;
                        currentForm.Raise = 0;
                        currentForm.callChipsValue = 0;
                        currentForm.raisedTurn = 123;
                        currentForm.totalRounds++;

                        CheckIfBankrupted();
                    }
                }
            }
        }

        private void CheckIfBankrupted()
        {
            if (!currentForm.hasPlayerBankrupted)
            {
                currentForm.playerStatus.Text = "";
            }

            if (!currentForm.hasBotOneBankrupted)
            {
                currentForm.botOneStatus.Text = "";
            }

            if (!currentForm.hasBotTwoBankrupted)
            {
                currentForm.botTwoStatus.Text = "";
            }

            if (!currentForm.hasBotThreeBankrupted)
            {
                currentForm.botThreeStatus.Text = "";
            }

            if (!currentForm.hasBotFourBankrupted)
            {
                currentForm.botFourStatus.Text = "";
            }

            if (!currentForm.hasBotFiveBankrupted)
            {
                currentForm.botFiveStatus.Text = "";
            }
        }
    }
}