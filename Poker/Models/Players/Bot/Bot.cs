namespace Poker.Models.Players.Bot
{
    using System;
    using System.Windows.Forms;

    //check bots possibility
    public class Bot
    {
        #region fields
        private GameManager currentForm;
        #endregion

        #region constructor
        public Bot(GameManager currentForm)
        {
            this.currentForm = currentForm;
        }
        #endregion

        #region methods
        public void CheckBotsHand(int botFirstCard, int botSecondCard, ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower, double botCurrent)
        {
            if (!hasBotFolded)
            {
                if (botCurrent == -1)
                {
                    this.CheckBotsHighCard(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 0)
                {
                    this.PairTable(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 1)
                {
                    this.CheckBotsPairHand(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 2)
                {
                    this.CheckBotsTwoPair(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 3)
                {
                    this.CheckBotsThreeOfAKind(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 4)
                {
                    this.CheckBotsStraight(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    this.CheckBotsFlush(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus);
                }
                if (botCurrent == 6)
                {
                    this.CheckBotsFullHouse(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 7)
                {
                    this.CheckBotsFourOfAKind(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }

                if (botCurrent == 8 || botCurrent == 9)
                {
                    this.CheckBotsStraightFlush(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower);
                }
            }

            if (hasBotFolded)
            {
                this.currentForm.cardsImages[botFirstCard].Visible = false;
                this.currentForm.cardsImages[botSecondCard].Visible = false;
            }
        }

        private void BotsMoveFirstPossibility(ref int botChips, ref bool isBotsTurn, ref bool hasBotFold, Label statusLabel, double safePlay, int behaviour, int riskPlay)
        {
            Random randomNumberGenerator = new Random();
            int randomNumber = randomNumberGenerator.Next(1, 4);
            if (this.currentForm.callChipsValue <= 0)
            {
                this.ChangeStatusToChecking(ref isBotsTurn, statusLabel);
            }
            if (this.currentForm.callChipsValue > 0)
            {
                if (randomNumber == 1)
                {
                    if (this.currentForm.callChipsValue <= BotMaximumBidAbility(botChips, behaviour))
                    {
                        this.Call(ref botChips, ref isBotsTurn, statusLabel);
                    }
                    else
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref hasBotFold, statusLabel);
                    }
                }
                if (randomNumber == 2)
                {
                    if (this.currentForm.callChipsValue <= BotMaximumBidAbility(botChips, riskPlay))
                    {
                        this.Call(ref botChips, ref isBotsTurn, statusLabel);
                    }
                    else
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref hasBotFold, statusLabel);
                    }
                }
            }

            if (randomNumber == 3)
            {
                if (this.currentForm.Raise == 0)
                {
                    this.currentForm.Raise = this.currentForm.callChipsValue * 2;
                    this.RaiseBet(ref botChips, ref isBotsTurn, statusLabel);
                }
                else
                {
                    if (this.currentForm.Raise <= BotMaximumBidAbility(botChips, behaviour))
                    {
                        this.currentForm.Raise = this.currentForm.callChipsValue * 2;
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

        private void BotsMoveSecondPossibility(ref int botChips, ref bool isBotsTurn, ref bool botFolds, Label labelStatus, int raiseFactor, int botsPower, int callPower)
        {
            Random randomNumberGenerator = new Random();
            int randomNumber = randomNumberGenerator.Next(1, 3);

            if (this.currentForm.totalRounds < 2)
            {
                if (this.currentForm.callChipsValue <= 0)
                {
                    this.ChangeStatusToChecking(ref isBotsTurn, labelStatus);
                }

                if (this.currentForm.callChipsValue > 0)
                {
                    if (this.currentForm.callChipsValue >= BotMaximumBidAbility(botChips, botsPower))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (this.currentForm.Raise > BotMaximumBidAbility(botChips, raiseFactor))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (!botFolds)
                    {
                        if (this.currentForm.callChipsValue >= BotMaximumBidAbility(botChips, raiseFactor) && this.currentForm.callChipsValue <= BotMaximumBidAbility(botChips, botsPower))
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this.currentForm.Raise <= BotMaximumBidAbility(botChips, raiseFactor) && this.currentForm.Raise >= (BotMaximumBidAbility(botChips, raiseFactor)) / 2)
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this.currentForm.Raise <= (BotMaximumBidAbility(botChips, raiseFactor)) / 2)
                        {
                            if (this.currentForm.Raise > 0)
                            {
                                this.currentForm.Raise = BotMaximumBidAbility(botChips, raiseFactor);
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }

                            else
                            {
                                this.currentForm.Raise = this.currentForm.callChipsValue * 2;
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }
                        }

                    }
                }
            }
            if (this.currentForm.totalRounds >= 2)
            {
                if (this.currentForm.callChipsValue > 0)
                {
                    if (this.currentForm.callChipsValue >= BotMaximumBidAbility(botChips, botsPower - randomNumber))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (this.currentForm.Raise > BotMaximumBidAbility(botChips, raiseFactor - randomNumber))
                    {
                        this.ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (!botFolds)
                    {
                        if (this.currentForm.callChipsValue >= BotMaximumBidAbility(botChips, raiseFactor - randomNumber) && this.currentForm.callChipsValue <= BotMaximumBidAbility(botChips, botsPower - randomNumber))
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this.currentForm.Raise <= BotMaximumBidAbility(botChips, raiseFactor - randomNumber) && this.currentForm.Raise >= (BotMaximumBidAbility(botChips, raiseFactor - randomNumber)) / 2)
                        {
                            this.Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (this.currentForm.Raise <= (BotMaximumBidAbility(botChips, raiseFactor - randomNumber)) / 2)
                        {
                            if (this.currentForm.Raise > 0)
                            {
                                this.currentForm.Raise = BotMaximumBidAbility(botChips, raiseFactor - randomNumber);
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }

                            else
                            {
                                this.currentForm.Raise = this.currentForm.callChipsValue * 2;
                                this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }
                        }
                    }
                }

                if (this.currentForm.callChipsValue <= 0)
                {
                    this.currentForm.Raise = BotMaximumBidAbility(botChips, callPower - randomNumber);
                    this.RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                }
            }

            if (botChips <= 0)
            {
                botFolds = true;
            }
        }

        private void BotsMoveThirdPossibility(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, int behaviour)
        {
            Random randomNumberGenerator = new Random();

            if (this.currentForm.callChipsValue <= 0)
            {
                this.ChangeStatusToChecking(ref isBotTurn, botStatus);
            }
            else
            {
                if (this.currentForm.callChipsValue >= BotMaximumBidAbility(botChips, behaviour))
                {
                    if (botChips > this.currentForm.callChipsValue)
                    {
                        this.Call(ref botChips, ref isBotTurn, botStatus);
                    }
                    else if (botChips <= this.currentForm.callChipsValue)
                    {
                        this.currentForm.isRaising = false;
                        isBotTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        this.currentForm.potTextBox.Text = (int.Parse(this.currentForm.potTextBox.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (this.currentForm.Raise > 0)
                    {
                        if (botChips >= this.currentForm.Raise * 2)
                        {
                            this.currentForm.Raise *= 2;
                            this.RaiseBet(ref botChips, ref isBotTurn, botStatus);
                        }
                        else
                        {
                            this.Call(ref botChips, ref isBotTurn, botStatus);
                        }
                    }
                    else
                    {
                        this.currentForm.Raise = this.currentForm.callChipsValue * 2;
                        this.RaiseBet(ref botChips, ref isBotTurn, botStatus);
                    }
                }
            }

            if (botChips <= 0)
            {
                hasBotFolded = true;
            }
        }

        private void ChangeStatusToFold(ref bool isBotTurn, ref bool hasBotFold, Label sStatus)
        {
            this.currentForm.isRaising = false;
            sStatus.Text = "Fold";
            isBotTurn = false;
            hasBotFold = true;
        }

        private void ChangeStatusToChecking(ref bool isBotsTurn, Label statusLabel)
        {
            statusLabel.Text = "Check";
            isBotsTurn = false;
            this.currentForm.isRaising = false;
        }

        private void Call(ref int botsChips, ref bool isBotsTurn, Label statusLabel)
        {
            this.currentForm.isRaising = false;
            isBotsTurn = false;
            botsChips -= this.currentForm.callChipsValue;
            statusLabel.Text = "Call " + this.currentForm.callChipsValue;
            this.currentForm.potTextBox.Text = (int.Parse(this.currentForm.potTextBox.Text) + this.currentForm.callChipsValue).ToString();
        }

        private void RaiseBet(ref int botChips, ref bool isBotsTurn, Label statusLabel)
        {
            botChips -= Convert.ToInt32(this.currentForm.Raise);
            statusLabel.Text = "Raise " + this.currentForm.Raise;
            this.currentForm.potTextBox.Text = (int.Parse(this.currentForm.potTextBox.Text) + Convert.ToInt32(this.currentForm.Raise)).ToString();
            this.currentForm.callChipsValue = Convert.ToInt32(this.currentForm.Raise);
            this.currentForm.isRaising = true;
            isBotsTurn = false;
        }

        private static double BotMaximumBidAbility(int botChips, int behaviour)
        {
            double maximumBidChips = Math.Round((botChips / behaviour) / 100d, 0) * 100;
            return maximumBidChips;
        }

        private void CheckBotsHighCard(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            BotsMoveFirstPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower, 20, 25);
        }

        private void PairTable(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            BotsMoveFirstPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, botPower, 16, 25);
        }

        private void CheckBotsPairHand(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);

            if (botPower <= 199 && botPower >= 140)
            {
                BotsMoveSecondPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 6, rRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
                BotsMoveSecondPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 7, rRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
                BotsMoveSecondPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 9, rRaise);
            }
        }

        private void CheckBotsTwoPair(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);

            if (botPower <= 290 && botPower >= 246)
            {
                BotsMoveSecondPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 3, rRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
                BotsMoveSecondPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 4, rRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
                BotsMoveSecondPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, rCall, 4, rRaise);
            }
        }

        private void CheckBotsThreeOfAKind(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);

            if (botPower <= 390 && botPower >= 330)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall);
            }

            if (botPower <= 327 && botPower >= 321)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall);
            }

            if (botPower < 321 && botPower >= 303)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, tCall);
            }
        }

        private void CheckBotsStraight(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);

            if (botPower <= 480 && botPower >= 410)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall);
            }

            if (botPower <= 409 && botPower >= 407)//10  8
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall);
            }

            if (botPower < 407 && botPower >= 404)
            {
               BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sCall);
            }
        }

        private void CheckBotsFlush(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fCall);
        }

        private void CheckBotsFullHouse(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);

            if (botPower <= 626 && botPower >= 620)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fhCall);
            }

            if (botPower < 620 && botPower >= 602)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fhCall);
            }
        }

        private void CheckBotsFourOfAKind(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);

            if (botPower <= 752 && botPower >= 704)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, fkCall);
            }
        }

        private void CheckBotsStraightFlush(ref int botChips, ref bool isBotTurn, ref bool hasBotFolded, Label botStatus, double botPower)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                BotsMoveThirdPossibility(ref botChips, ref isBotTurn, ref hasBotFolded, botStatus, sfCall);
            }
        }
        #endregion

    }
}