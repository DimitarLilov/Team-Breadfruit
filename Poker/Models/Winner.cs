namespace Poker.Models
{
    using System;
    using System.Windows.Forms;

    public class Winner
    {
        private GameManager currentForm;

        public Winner(GameManager currentForm)
        {
            this.currentForm = currentForm;
        }

        public void WinnerRules(double current, double power, string currentText, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }

            for (int j = 0; j <= 16; j++)
            {
                if (this.currentForm.cardsImages[j].Visible)
                {
                    this.currentForm.cardsImages[j].Image = this.currentForm.Deck[j];
                }
            }

            if (current == this.currentForm.sorted.Current)
            {
                if (power == this.currentForm.sorted.Power)
                {
                    this.currentForm.winners++;
                    this.currentForm.CheckWinners.Add(currentText);

                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }

                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }

                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }

                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }

                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }

                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }

                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }

                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }

                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }

            if (currentText == lastly)
            {
                if (this.currentForm.winners > 1)
                {
                    if (this.currentForm.CheckWinners.Contains("Player"))
                    {
                        this.currentForm.playerChips += int.Parse(this.currentForm.potTextBox.Text) /this.currentForm.winners;
                        this.currentForm.playerChipsTextBox.Text = this.currentForm.playerChips.ToString();
                    }

                    if (this.currentForm.CheckWinners.Contains("Bot 1"))
                    {
                        this.currentForm.botOnehips += int.Parse(this.currentForm.potTextBox.Text) /this.currentForm.winners;
                        this.currentForm.botOneChipsTextBox.Text = this.currentForm.botOnehips.ToString();
                    }

                    if (this.currentForm.CheckWinners.Contains("Bot 2"))
                    {
                        this.currentForm.botTwoChips += int.Parse(this.currentForm.potTextBox.Text) /this.currentForm.winners;
                        this.currentForm.botTwoChipsTextBox.Text = this.currentForm.botTwoChips.ToString();
                    }

                    if (this.currentForm.CheckWinners.Contains("Bot 3"))
                    {
                        this.currentForm.botThreeChips += int.Parse(this.currentForm.potTextBox.Text) /this.currentForm.winners;
                        this.currentForm.botThreeChipsTextBox.Text = this.currentForm.botThreeChips.ToString();
                    }

                    if (this.currentForm.CheckWinners.Contains("Bot 4"))
                    {
                        this.currentForm.botFourChips += int.Parse(this.currentForm.potTextBox.Text) /this.currentForm.winners;
                        this.currentForm.botFourChipsTextBox.Text = this.currentForm.botFourChips.ToString();
                    }

                    if (this.currentForm.CheckWinners.Contains("Bot 5"))
                    {
                        this.currentForm.botFiveChips += int.Parse(this.currentForm.potTextBox.Text) /this.currentForm.winners;
                        this.currentForm.botFiveChipsTextBox.Text = this.currentForm.botFiveChips.ToString();
                    }

                }

                if (this.currentForm.winners == 1)
                {
                    if (this.currentForm.CheckWinners.Contains("Player"))
                    {
                        this.currentForm.playerChips += int.Parse(this.currentForm.potTextBox.Text);
                    }

                    if (this.currentForm.CheckWinners.Contains("Bot 1"))
                    {
                        this.currentForm.botOnehips += int.Parse(this.currentForm.potTextBox.Text);
                    }

                    if (this.currentForm.CheckWinners.Contains("Bot 2"))
                    {
                        this.currentForm.botTwoChips += int.Parse(this.currentForm.potTextBox.Text);
                    }

                    if (this.currentForm.CheckWinners.Contains("Bot 3"))
                    {
                        this.currentForm.botThreeChips += int.Parse(this.currentForm.potTextBox.Text);
                    }

                    if (this.currentForm.CheckWinners.Contains("Bot 4"))
                    {
                        this.currentForm.botFourChips += int.Parse(this.currentForm.potTextBox.Text);
                    }

                    if (this.currentForm.CheckWinners.Contains("Bot 5"))
                    {
                        this.currentForm.botFiveChips += int.Parse(this.currentForm.potTextBox.Text);
                    }
                }
            }
        }

        public void FixWinners()
        {
            this.currentForm.winningingHands.Clear();
            this.currentForm.sorted.Current = 0;
            this.currentForm.sorted.Power = 0;
            string fixedLast = String.Empty;

            if (!this.currentForm.playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                this.currentForm.CurrentRules.GameRules(0, 1, ref this.currentForm.playerType, ref this.currentForm.playerPower, this.currentForm.hasPlayerBankrupted);
            }

            if (!this.currentForm.botOneStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                this.currentForm.CurrentRules.GameRules(2, 3, ref this.currentForm.botOneType, ref this.currentForm.botOnePower, this.currentForm.hasBotOneBankrupted);
            }

            if (!this.currentForm.botTwoStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                this.currentForm.CurrentRules.GameRules(4, 5, ref this.currentForm.botTwoType, ref this.currentForm.botTwoPower, this.currentForm.hasBotTwoBankrupted);
            }

            if (!this.currentForm.botThreeStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                this.currentForm.CurrentRules.GameRules(6, 7, ref this.currentForm.botThreeType, ref this.currentForm.botThreePower, this.currentForm.hasBotThreeBankrupted);
            }

            if (!this.currentForm.botFourStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                this.currentForm.CurrentRules.GameRules(8, 9, ref this.currentForm.botFourType, ref this.currentForm.botFourPower, this.currentForm.hasBotFourBankrupted);
            }

            if (!this.currentForm.botFiveStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                this.currentForm.CurrentRules.GameRules(10, 11, ref this.currentForm.botFiveType, ref this.currentForm.botFivePower, this.currentForm.hasBotFiveBankrupted);
            }

            this.currentForm.Winner1.WinnerRules(this.currentForm.playerType, this.currentForm.playerPower, "Player", fixedLast);

            this.currentForm.Winner1.WinnerRules(this.currentForm.botOneType, this.currentForm.botOnePower, "Bot 1", fixedLast);

            this.currentForm.Winner1.WinnerRules(this.currentForm.botTwoType, this.currentForm.botTwoPower, "Bot 2", fixedLast);

            this.currentForm.Winner1.WinnerRules(this.currentForm.botThreeType, this.currentForm.botThreePower, "Bot 3", fixedLast);

            this.currentForm.Winner1.WinnerRules(this.currentForm.botFourType, this.currentForm.botFourPower, "Bot 4", fixedLast);

            this.currentForm.Winner1.WinnerRules(this.currentForm.botFiveType, this.currentForm.botFivePower, "Bot 5", fixedLast);
        }
    }
}