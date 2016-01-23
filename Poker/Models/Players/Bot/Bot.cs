namespace Poker.Models.Players.Bot
{
    using System;
    using System.Windows.Forms;

    //check bots possibility
    public class Bot
    {
        private GameManager gameManager;

        public Bot(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }


        private void ChangeStatusToFold(ref bool isBotTurn, ref bool sFTurn, Label sStatus)
        {
            this.gameManager.isRaising = false;
            sStatus.Text = "Fold";
            isBotTurn = false;
            sFTurn = true;
        }

        private void ChangeStatusToChecking(ref bool isBotsTurn, Label statusLabel)
        {
            statusLabel.Text = "Check";
            isBotsTurn = false;
            this.gameManager.isRaising = false;
        }

        private void Call(ref int botsChips, ref bool isBotsTurn, Label statusLabel)
        {
            this.gameManager.isRaising = false;
            isBotsTurn = false;
            botsChips -= this.gameManager.callChipsValue;
            statusLabel.Text = "Call " + this.gameManager.callChipsValue;
            this.gameManager.potTextBox.Text = (int.Parse(this.gameManager.potTextBox.Text) + this.gameManager.callChipsValue).ToString();
        }

        private void RaiseBet(ref int botChips, ref bool isBotsTurn, Label statusLabel)
        {
            botChips -= Convert.ToInt32(this.gameManager.Raise);
            statusLabel.Text = "Raise " + this.gameManager.Raise;
            this.gameManager.potTextBox.Text = (int.Parse(this.gameManager.potTextBox.Text) + Convert.ToInt32(this.gameManager.Raise)).ToString();
            this.gameManager.callChipsValue = Convert.ToInt32(this.gameManager.Raise);
            this.gameManager.isRaising = true;
            isBotsTurn = false;
        }

        private static double BotMaximumBidAbility(int botChips, int behaviour)
        {
            double maximumBidChips = Math.Round((botChips / behaviour) / 100d, 0) * 100;
            return maximumBidChips;
        }

        public void BotsMoveFirst(ref int botChips, ref bool isBotsTurn, ref bool hasBotFold, Label statusLabel, double safePlay, int n, int riskPlay)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (this.gameManager.callChipsValue <= 0)
            {
                this.ChangeStatusToChecking(ref isBotsTurn, statusLabel);
            }
            if (this.gameManager.callChipsValue > 0)
            {
                if (rnd == 1)
                {
                    if (this.gameManager.callChipsValue <= BotMaximumBidAbility(botChips, n))
                    {
                        this.Call(ref botChips, ref isBotsTurn, statusLabel);
                    }
                    else
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref hasBotFold, statusLabel);
                    }
                }
                if (rnd == 2)
                {
                    if (this.gameManager.callChipsValue <= BotMaximumBidAbility(botChips, riskPlay))
                    {
                        this.Call(ref botChips, ref isBotsTurn, statusLabel);
                    }
                    else
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref hasBotFold, statusLabel);
                    }
                }
            }

            if (rnd == 3)
            {
                if (this.gameManager.Raise == 0)
                {
                    this.gameManager.Raise = this.gameManager.callChipsValue * 2;
                    this.RaiseBet(ref botChips, ref isBotsTurn, statusLabel);
                }
                else
                {
                    if (this.gameManager.Raise <= BotMaximumBidAbility(botChips, n))
                    {
                        this.gameManager.Raise = this.gameManager.callChipsValue * 2;
                        this.RaiseBet(ref botChips, ref isBotsTurn, statusLabel);
                    }
                    else
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref hasBotFold, statusLabel);
                    }
                }
            }

            if (botChips <= 0)
            {
                hasBotFold = true;
            }
        }

        public void BotsMoveSecond(ref int botChips, ref bool isBotsTurn, ref bool botFolds, Label labelStatus, int raiseFactor, int botsPower, int callPower)
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 3);

            if (this.gameManager.totalRounds < 2)
            {
                if (this.gameManager.callChipsValue <= 0)
                {
                    this.ChangeStatusToChecking(ref isBotsTurn, labelStatus);
                }

                if (this.gameManager.callChipsValue > 0)
                {
                    if (this.gameManager.callChipsValue >= BotMaximumBidAbility(botChips, botsPower))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (this.gameManager.Raise > BotMaximumBidAbility(botChips, raiseFactor))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (!botFolds)
                    {
                        if (this.gameManager.callChipsValue >= BotMaximumBidAbility(botChips, raiseFactor) && this.gameManager.callChipsValue <= BotMaximumBidAbility(botChips, botsPower))
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this.gameManager.Raise <= BotMaximumBidAbility(botChips, raiseFactor) && this.gameManager.Raise >= (BotMaximumBidAbility(botChips, raiseFactor)) / 2)
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this.gameManager.Raise <= (BotMaximumBidAbility(botChips, raiseFactor)) / 2)
                        {
                            if (this.gameManager.Raise > 0)
                            {
                                this.gameManager.Raise = BotMaximumBidAbility(botChips, raiseFactor);
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }

                            else
                            {
                                this.gameManager.Raise = this.gameManager.callChipsValue * 2;
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }
                        }

                    }
                }
            }
            if (this.gameManager.totalRounds >= 2)
            {
                if (this.gameManager.callChipsValue > 0)
                {
                    if (this.gameManager.callChipsValue >= BotMaximumBidAbility(botChips, botsPower - randomNumber))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (this.gameManager.Raise > BotMaximumBidAbility(botChips, raiseFactor - randomNumber))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (!botFolds)
                    {
                        if (this.gameManager.callChipsValue >= BotMaximumBidAbility(botChips, raiseFactor - randomNumber) && this.gameManager.callChipsValue <= BotMaximumBidAbility(botChips, botsPower - randomNumber))
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this.gameManager.Raise <= BotMaximumBidAbility(botChips, raiseFactor - randomNumber) && this.gameManager.Raise >= (BotMaximumBidAbility(botChips, raiseFactor - randomNumber)) / 2)
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this.gameManager.Raise <= (BotMaximumBidAbility(botChips, raiseFactor - randomNumber)) / 2)
                        {
                            if (this.gameManager.Raise > 0)
                            {
                                this.gameManager.Raise = BotMaximumBidAbility(botChips, raiseFactor - randomNumber);
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }

                            else
                            {
                                this.gameManager.Raise = this.gameManager.callChipsValue * 2;
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }
                        }
                    }
                }

                if (this.gameManager.callChipsValue <= 0)
                {
                    this.gameManager.Raise = BotMaximumBidAbility(botChips, callPower - randomNumber);
                    this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                }
            }

            if (botChips <= 0)
            {
                botFolds = true;
            }
        }

        public void BotsMoveThirdPossibility(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, int behaviour)
        {
            Random rand = new Random();

            if (this.gameManager.callChipsValue <= 0)
            {
                this.ChangeStatusToChecking(ref isBotTurn, botStatus);
            }
            else
            {
                if (this.gameManager.callChipsValue >= BotMaximumBidAbility(botChips, behaviour))
                {
                    if (botChips > this.gameManager.callChipsValue)
                    {
                        this.Call(ref botChips, ref isBotTurn, botStatus);
                    }
                    else if (botChips <= this.gameManager.callChipsValue)
                    {
                        this.gameManager.isRaising = false;
                        isBotTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        this.gameManager.potTextBox.Text = (int.Parse(this.gameManager.potTextBox.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (this.gameManager.Raise > 0)
                    {
                        if (botChips >= this.gameManager.Raise * 2)
                        {
                            this.gameManager.Raise *= 2;
                            this.RaiseBet(ref botChips, ref isBotTurn, botStatus);
                        }
                        else
                        {
                            this.Call(ref botChips, ref isBotTurn, botStatus);
                        }
                    }
                    else
                    {
                        this.gameManager.Raise = this.gameManager.callChipsValue * 2;
                        this.RaiseBet(ref botChips, ref isBotTurn, botStatus);
                    }
                }
            }

            if (botChips <= 0)
            {
                hasBotFolded = true;
            }
        }
    }
}