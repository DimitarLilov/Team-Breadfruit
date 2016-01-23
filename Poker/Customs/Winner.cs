using System;
using System.Windows.Forms;

namespace Poker
{
    public class Winner
    {
        private Form1 currentForm;

        public Winner(Form1 currentForm)
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
                if (currentForm.cardsImages[j].Visible)
                {
                    currentForm.cardsImages[j].Image = currentForm.Deck[j];
                }
            }

            if (current == currentForm.sorted.Current)
            {
                if (power == currentForm.sorted.Power)
                {
                    currentForm.winners++;
                    currentForm.CheckWinners.Add(currentText);

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
                if (currentForm.winners > 1)
                {
                    if (currentForm.CheckWinners.Contains("Player"))
                    {
                        currentForm.playerChips += int.Parse(currentForm.potTextBox.Text) /currentForm.winners;
                        currentForm.playerChipsTextBox.Text = currentForm.playerChips.ToString();
                    }

                    if (currentForm.CheckWinners.Contains("Bot 1"))
                    {
                        currentForm.botOnehips += int.Parse(currentForm.potTextBox.Text) /currentForm.winners;
                        currentForm.botOneChipsTextBox.Text = currentForm.botOnehips.ToString();
                    }

                    if (currentForm.CheckWinners.Contains("Bot 2"))
                    {
                        currentForm.botTwoChips += int.Parse(currentForm.potTextBox.Text) /currentForm.winners;
                        currentForm.botTwoChipsTextBox.Text = currentForm.botTwoChips.ToString();
                    }

                    if (currentForm.CheckWinners.Contains("Bot 3"))
                    {
                        currentForm.botThreeChips += int.Parse(currentForm.potTextBox.Text) /currentForm.winners;
                        currentForm.botThreeChipsTextBox.Text = currentForm.botThreeChips.ToString();
                    }

                    if (currentForm.CheckWinners.Contains("Bot 4"))
                    {
                        currentForm.botFourChips += int.Parse(currentForm.potTextBox.Text) /currentForm.winners;
                        currentForm.botFourChipsTextBox.Text = currentForm.botFourChips.ToString();
                    }

                    if (currentForm.CheckWinners.Contains("Bot 5"))
                    {
                        currentForm.botFiveChips += int.Parse(currentForm.potTextBox.Text) /currentForm.winners;
                        currentForm.botFiveChipsTextBox.Text = currentForm.botFiveChips.ToString();
                    }

                }

                if (currentForm.winners == 1)
                {
                    if (currentForm.CheckWinners.Contains("Player"))
                    {
                        currentForm.playerChips += int.Parse(currentForm.potTextBox.Text);
                    }

                    if (currentForm.CheckWinners.Contains("Bot 1"))
                    {
                        currentForm.botOnehips += int.Parse(currentForm.potTextBox.Text);
                    }

                    if (currentForm.CheckWinners.Contains("Bot 2"))
                    {
                        currentForm.botTwoChips += int.Parse(currentForm.potTextBox.Text);
                    }

                    if (currentForm.CheckWinners.Contains("Bot 3"))
                    {
                        currentForm.botThreeChips += int.Parse(currentForm.potTextBox.Text);
                    }

                    if (currentForm.CheckWinners.Contains("Bot 4"))
                    {
                        currentForm.botFourChips += int.Parse(currentForm.potTextBox.Text);
                    }

                    if (currentForm.CheckWinners.Contains("Bot 5"))
                    {
                        currentForm.botFiveChips += int.Parse(currentForm.potTextBox.Text);
                    }
                }
            }
        }

        public void FixWinners()
        {
            currentForm.winningingHands.Clear();
            currentForm.sorted.Current = 0;
            currentForm.sorted.Power = 0;
            string fixedLast = String.Empty;

            if (!currentForm.playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                currentForm.CurrentRules.GameRules(0, 1, ref currentForm.playerType, ref currentForm.playerPower, currentForm.hasPlayerBankrupted);
            }

            if (!currentForm.botOneStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                currentForm.CurrentRules.GameRules(2, 3, ref currentForm.botOneType, ref currentForm.botOnePower, currentForm.hasBotOneBankrupted);
            }

            if (!currentForm.botTwoStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                currentForm.CurrentRules.GameRules(4, 5, ref currentForm.botTwoType, ref currentForm.botTwoPower, currentForm.hasBotTwoBankrupted);
            }

            if (!currentForm.botThreeStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                currentForm.CurrentRules.GameRules(6, 7, ref currentForm.botThreeType, ref currentForm.botThreePower, currentForm.hasBotThreeBankrupted);
            }

            if (!currentForm.botFourStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                currentForm.CurrentRules.GameRules(8, 9, ref currentForm.botFourType, ref currentForm.botFourPower, currentForm.hasBotFourBankrupted);
            }

            if (!currentForm.botFiveStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                currentForm.CurrentRules.GameRules(10, 11, ref currentForm.botFiveType, ref currentForm.botFivePower, currentForm.hasBotFiveBankrupted);
            }

            currentForm.Winner1.WinnerRules(currentForm.playerType, currentForm.playerPower, "Player", fixedLast);

            currentForm.Winner1.WinnerRules(currentForm.botOneType, currentForm.botOnePower, "Bot 1", fixedLast);

            currentForm.Winner1.WinnerRules(currentForm.botTwoType, currentForm.botTwoPower, "Bot 2", fixedLast);

            currentForm.Winner1.WinnerRules(currentForm.botThreeType, currentForm.botThreePower, "Bot 3", fixedLast);

            currentForm.Winner1.WinnerRules(currentForm.botFourType, currentForm.botFourPower, "Bot 4", fixedLast);

            currentForm.Winner1.WinnerRules(currentForm.botFiveType, currentForm.botFivePower, "Bot 5", fixedLast);
        }
    }
}