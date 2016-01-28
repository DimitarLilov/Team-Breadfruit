#pragma warning disable 197
namespace Poker.Models
{
    using System;
    using System.Windows.Forms;

    using Poker.Constants;
    using Poker.Interfaces;

    public class Winner :IWinner
    {
        private readonly GameManager currentForm;
        
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
                        MessageBox.Show(currentText + Constants.HighCard);
                    }

                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + Constants.Pair);
                    }

                    if (current == 2)
                    {
                        MessageBox.Show(currentText + Constants.TwoPair);
                    }

                    if (current == 3)
                    {
                        MessageBox.Show(currentText + Constants.ThreeOfAKind);
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + Constants.Straight);
                    }

                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + Constants.Flush);
                    }

                    if (current == 6)
                    {
                        MessageBox.Show(currentText + Constants.FullHouse);
                    }

                    if (current == 7)
                    {
                        MessageBox.Show(currentText + Constants.FourOfAKind);
                    }

                    if (current == 8)
                    {
                        MessageBox.Show(currentText + Constants.StraightFlush);
                    }

                    if (current == 9)
                    {
                        MessageBox.Show(currentText + Constants.RoyalFlush);
                    }
                }
            }

            if (currentText == lastly)
            {
                if (this.currentForm.winners > 1)
                {
                    if (this.currentForm.CheckWinners.Contains(Constants.Player))
                    {
                        this.currentForm.playerChips += int.Parse(this.currentForm.potTextBox.Text) / this.currentForm.winners;
                        this.currentForm.playerChipsTextBox.Text = this.currentForm.playerChips.ToString();
                    }

                    if (this.currentForm.CheckWinners.Contains(Constants.Bot1Winner))
                    {
                        this.currentForm.botOneChips += int.Parse(this.currentForm.potTextBox.Text) / this.currentForm.winners;
                        this.currentForm.botOneChipsTextBox.Text = this.currentForm.botOneChips.ToString();
                    }

                    if (this.currentForm.CheckWinners.Contains(Constants.Bot2Winner))
                    {
                        this.currentForm.botTwoChips += int.Parse(this.currentForm.potTextBox.Text) / this.currentForm.winners;
                        this.currentForm.botTwoChipsTextBox.Text = this.currentForm.botTwoChips.ToString();
                    }

                    if (this.currentForm.CheckWinners.Contains(Constants.Bot3Winner))
                    {
                        this.currentForm.botThreeChips += int.Parse(this.currentForm.potTextBox.Text) / this.currentForm.winners;
                        this.currentForm.botThreeChipsTextBox.Text = this.currentForm.botThreeChips.ToString();
                    }

                    if (this.currentForm.CheckWinners.Contains(Constants.Bot4Winner))
                    {
                        this.currentForm.botFourChips += int.Parse(this.currentForm.potTextBox.Text) / this.currentForm.winners;
                        this.currentForm.botFourChipsTextBox.Text = this.currentForm.botFourChips.ToString();
                    }

                    if (this.currentForm.CheckWinners.Contains(Constants.Bot5Winner))
                    {
                        this.currentForm.botFiveChips += int.Parse(this.currentForm.potTextBox.Text) / this.currentForm.winners;
                        this.currentForm.botFiveChipsTextBox.Text = this.currentForm.botFiveChips.ToString();
                    }
                }

                if (this.currentForm.winners == 1)
                {
                    if (this.currentForm.CheckWinners.Contains(Constants.Player))
                    {
                        this.currentForm.playerChips += int.Parse(this.currentForm.potTextBox.Text);
                    }

                    if (this.currentForm.CheckWinners.Contains(Constants.Bot1Winner))
                    {
                        this.currentForm.botOneChips += int.Parse(this.currentForm.potTextBox.Text);
                    }

                    if (this.currentForm.CheckWinners.Contains(Constants.Bot2Winner))
                    {
                        this.currentForm.botTwoChips += int.Parse(this.currentForm.potTextBox.Text);
                    }

                    if (this.currentForm.CheckWinners.Contains(Constants.Bot3Winner))
                    {
                        this.currentForm.botThreeChips += int.Parse(this.currentForm.potTextBox.Text);
                    }

                    if (this.currentForm.CheckWinners.Contains(Constants.Bot4Winner))
                    {
                        this.currentForm.botFourChips += int.Parse(this.currentForm.potTextBox.Text);
                    }

                    if (this.currentForm.CheckWinners.Contains(Constants.Bot5Winner))
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

            if (!this.currentForm.playerStatus.Text.Contains(Constants.Fold))
            {
                fixedLast = Constants.Player;
                this.currentForm.CurrentRules.GameRulesCreator(0, 1, ref this.currentForm.playerType, ref this.currentForm.playerPower, this.currentForm.hasPlayerBankrupted);
            }

            if (!this.currentForm.botOneStatus.Text.Contains(Constants.Fold))
            {
                fixedLast = Constants.Bot1Winner;
                this.currentForm.CurrentRules.GameRulesCreator(2, 3, ref this.currentForm.botOneType, ref this.currentForm.botOnePower, this.currentForm.hasBotOneBankrupted);
            }

            if (!this.currentForm.botTwoStatus.Text.Contains(Constants.Fold))
            {
                fixedLast = Constants.Bot2Winner;
                this.currentForm.CurrentRules.GameRulesCreator(4, 5, ref this.currentForm.botTwoType, ref this.currentForm.botTwoPower, this.currentForm.hasBotTwoBankrupted);
            }

            if (!this.currentForm.botThreeStatus.Text.Contains(Constants.Fold))
            {
                fixedLast = Constants.Bot3Winner;
                this.currentForm.CurrentRules.GameRulesCreator(6, 7, ref this.currentForm.botThreeType, ref this.currentForm.botThreePower, this.currentForm.hasBotThreeBankrupted);
            }

            if (!this.currentForm.botFourStatus.Text.Contains(Constants.Fold))
            {
                fixedLast = Constants.Bot4Winner;
                this.currentForm.CurrentRules.GameRulesCreator(8, 9, ref this.currentForm.botFourType, ref this.currentForm.botFourPower, this.currentForm.hasBotFourBankrupted);
            }

            if (!this.currentForm.botFiveStatus.Text.Contains(Constants.Fold))
            {
                fixedLast = Constants.Bot5Winner;
                this.currentForm.CurrentRules.GameRulesCreator(10, 11, ref this.currentForm.botFiveType, ref this.currentForm.botFivePower, this.currentForm.hasBotFiveBankrupted);
            }

            this.currentForm.Winner1.WinnerRules(this.currentForm.playerType, this.currentForm.playerPower, Constants.Player, fixedLast);

            this.currentForm.Winner1.WinnerRules(this.currentForm.botOneType, this.currentForm.botOnePower, Constants.Bot1Winner, fixedLast);

            this.currentForm.Winner1.WinnerRules(this.currentForm.botTwoType, this.currentForm.botTwoPower, Constants.Bot2Winner, fixedLast);

            this.currentForm.Winner1.WinnerRules(this.currentForm.botThreeType, this.currentForm.botThreePower, Constants.Bot3Winner, fixedLast);

            this.currentForm.Winner1.WinnerRules(this.currentForm.botFourType, this.currentForm.botFourPower, Constants.Bot4Winner, fixedLast);

            this.currentForm.Winner1.WinnerRules(this.currentForm.botFiveType, this.currentForm.botFivePower, Constants.Bot5Winner, fixedLast);
        }
    }
}