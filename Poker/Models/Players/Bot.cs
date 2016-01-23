namespace Poker.Models.Players
{
    using System;
    using System.Windows.Forms;

    //check bots possibility
    public class Bot
    {
        private Form1 _form1;

        public Bot(Form1 form1)
        {
            this._form1 = form1;
        }

        private void ChangeStatusToFold(ref bool isBotTurn, ref bool sFTurn, Label sStatus)
        {
            this._form1.isRaising = false;
            sStatus.Text = "Fold";
            isBotTurn = false;
            sFTurn = true;
        }

        private void ChangeStatusToChecking(ref bool isBotsTurn, Label statusLabel)
        {
            statusLabel.Text = "Check";
            isBotsTurn = false;
            this._form1.isRaising = false;
        }

        private void Call(ref int botsChips, ref bool isBotsTurn, Label statusLabel)
        {
            this._form1.isRaising = false;
            isBotsTurn = false;
            botsChips -= this._form1.callChipsValue;
            statusLabel.Text = "Call " + this._form1.callChipsValue;
            this._form1.potTextBox.Text = (int.Parse(this._form1.potTextBox.Text) + this._form1.callChipsValue).ToString();
        }

        private void RaiseBet(ref int botChips, ref bool isBotsTurn, Label statusLabel)
        {
            botChips -= Convert.ToInt32(this._form1.Raise);
            statusLabel.Text = "Raise " + this._form1.Raise;
            this._form1.potTextBox.Text = (int.Parse(this._form1.potTextBox.Text) + Convert.ToInt32(this._form1.Raise)).ToString();
            this._form1.callChipsValue = Convert.ToInt32(this._form1.Raise);
            this._form1.isRaising = true;
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
            if (this._form1.callChipsValue <= 0)
            {
                this.ChangeStatusToChecking(ref isBotsTurn, statusLabel);
            }
            if (this._form1.callChipsValue > 0)
            {
                if (rnd == 1)
                {
                    if (this._form1.callChipsValue <= BotMaximumBidAbility(botChips, n))
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
                    if (this._form1.callChipsValue <= BotMaximumBidAbility(botChips, riskPlay))
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
                if (this._form1.Raise == 0)
                {
                    this._form1.Raise = this._form1.callChipsValue * 2;
                    this.RaiseBet(ref botChips, ref isBotsTurn, statusLabel);
                }
                else
                {
                    if (this._form1.Raise <= BotMaximumBidAbility(botChips, n))
                    {
                        this._form1.Raise = this._form1.callChipsValue * 2;
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

            if (this._form1.totalRounds < 2)
            {
                if (this._form1.callChipsValue <= 0)
                {
                    this.ChangeStatusToChecking(ref isBotsTurn, labelStatus);
                }

                if (this._form1.callChipsValue > 0)
                {
                    if (this._form1.callChipsValue >= BotMaximumBidAbility(botChips, botsPower))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (this._form1.Raise > BotMaximumBidAbility(botChips, raiseFactor))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (!botFolds)
                    {
                        if (this._form1.callChipsValue >= BotMaximumBidAbility(botChips, raiseFactor) && this._form1.callChipsValue <= BotMaximumBidAbility(botChips, botsPower))
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this._form1.Raise <= BotMaximumBidAbility(botChips, raiseFactor) && this._form1.Raise >= (BotMaximumBidAbility(botChips, raiseFactor)) / 2)
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this._form1.Raise <= (BotMaximumBidAbility(botChips, raiseFactor)) / 2)
                        {
                            if (this._form1.Raise > 0)
                            {
                                this._form1.Raise = BotMaximumBidAbility(botChips, raiseFactor);
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }

                            else
                            {
                                this._form1.Raise = this._form1.callChipsValue * 2;
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }
                        }

                    }
                }
            }
            if (this._form1.totalRounds >= 2)
            {
                if (this._form1.callChipsValue > 0)
                {
                    if (this._form1.callChipsValue >= BotMaximumBidAbility(botChips, botsPower - randomNumber))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (this._form1.Raise > BotMaximumBidAbility(botChips, raiseFactor - randomNumber))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (!botFolds)
                    {
                        if (this._form1.callChipsValue >= BotMaximumBidAbility(botChips, raiseFactor - randomNumber) && this._form1.callChipsValue <= BotMaximumBidAbility(botChips, botsPower - randomNumber))
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this._form1.Raise <= BotMaximumBidAbility(botChips, raiseFactor - randomNumber) && this._form1.Raise >= (BotMaximumBidAbility(botChips, raiseFactor - randomNumber)) / 2)
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this._form1.Raise <= (BotMaximumBidAbility(botChips, raiseFactor - randomNumber)) / 2)
                        {
                            if (this._form1.Raise > 0)
                            {
                                this._form1.Raise = BotMaximumBidAbility(botChips, raiseFactor - randomNumber);
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }

                            else
                            {
                                this._form1.Raise = this._form1.callChipsValue * 2;
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }
                        }
                    }
                }

                if (this._form1.callChipsValue <= 0)
                {
                    this._form1.Raise = BotMaximumBidAbility(botChips, callPower - randomNumber);
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

            if (this._form1.callChipsValue <= 0)
            {
                this.ChangeStatusToChecking(ref isBotTurn, botStatus);
            }
            else
            {
                if (this._form1.callChipsValue >= BotMaximumBidAbility(botChips, behaviour))
                {
                    if (botChips > this._form1.callChipsValue)
                    {
                        this.Call(ref botChips, ref isBotTurn, botStatus);
                    }
                    else if (botChips <= this._form1.callChipsValue)
                    {
                        this._form1.isRaising = false;
                        isBotTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        this._form1.potTextBox.Text = (int.Parse(this._form1.potTextBox.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (this._form1.Raise > 0)
                    {
                        if (botChips >= this._form1.Raise * 2)
                        {
                            this._form1.Raise *= 2;
                            this.RaiseBet(ref botChips, ref isBotTurn, botStatus);
                        }
                        else
                        {
                            this.Call(ref botChips, ref isBotTurn, botStatus);
                        }
                    }
                    else
                    {
                        this._form1.Raise = this._form1.callChipsValue * 2;
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