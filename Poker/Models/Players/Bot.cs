using System;
using System.Windows.Forms;

namespace Poker
{
    //check bots possibility
    public class Bot
    {
        private Form1 _form1;

        public Bot(Form1 form1)
        {
            _form1 = form1;
        }

        private void ChangeStatusToFold(ref bool isBotTurn, ref bool sFTurn, Label sStatus)
        {
            _form1.isRaising = false;
            sStatus.Text = "Fold";
            isBotTurn = false;
            sFTurn = true;
        }

        private void ChangeStatusToChecking(ref bool isBotsTurn, Label statusLabel)
        {
            statusLabel.Text = "Check";
            isBotsTurn = false;
            _form1.isRaising = false;
        }

        private void Call(ref int botsChips, ref bool isBotsTurn, Label statusLabel)
        {
            _form1.isRaising = false;
            isBotsTurn = false;
            botsChips -= _form1.callChipsValue;
            statusLabel.Text = "Call " + _form1.callChipsValue;
            _form1.potTextBox.Text = (int.Parse(_form1.potTextBox.Text) + _form1.callChipsValue).ToString();
        }

        private void RaiseBet(ref int botChips, ref bool isBotsTurn, Label statusLabel)
        {
            botChips -= Convert.ToInt32(_form1.Raise);
            statusLabel.Text = "Raise " + _form1.Raise;
            _form1.potTextBox.Text = (int.Parse(_form1.potTextBox.Text) + Convert.ToInt32(_form1.Raise)).ToString();
            _form1.callChipsValue = Convert.ToInt32(_form1.Raise);
            _form1.isRaising = true;
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
            if (_form1.callChipsValue <= 0)
            {
                ChangeStatusToChecking(ref isBotsTurn, statusLabel);
            }
            if (_form1.callChipsValue > 0)
            {
                if (rnd == 1)
                {
                    if (_form1.callChipsValue <= BotMaximumBidAbility(botChips, n))
                    {
                        Call(ref botChips, ref isBotsTurn, statusLabel);
                    }
                    else
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref hasBotFold, statusLabel);
                    }
                }
                if (rnd == 2)
                {
                    if (_form1.callChipsValue <= BotMaximumBidAbility(botChips, riskPlay))
                    {
                        Call(ref botChips, ref isBotsTurn, statusLabel);
                    }
                    else
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref hasBotFold, statusLabel);
                    }
                }
            }

            if (rnd == 3)
            {
                if (_form1.Raise == 0)
                {
                    _form1.Raise = _form1.callChipsValue * 2;
                    RaiseBet(ref botChips, ref isBotsTurn, statusLabel);
                }
                else
                {
                    if (_form1.Raise <= BotMaximumBidAbility(botChips, n))
                    {
                        _form1.Raise = _form1.callChipsValue * 2;
                        RaiseBet(ref botChips, ref isBotsTurn, statusLabel);
                    }
                    else
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref hasBotFold, statusLabel);
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

            if (_form1.totalRounds < 2)
            {
                if (_form1.callChipsValue <= 0)
                {
                    ChangeStatusToChecking(ref isBotsTurn, labelStatus);
                }

                if (_form1.callChipsValue > 0)
                {
                    if (_form1.callChipsValue >= BotMaximumBidAbility(botChips, botsPower))
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (_form1.Raise > BotMaximumBidAbility(botChips, raiseFactor))
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (!botFolds)
                    {
                        if (_form1.callChipsValue >= BotMaximumBidAbility(botChips, raiseFactor) && _form1.callChipsValue <= BotMaximumBidAbility(botChips, botsPower))
                        {
                            Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (_form1.Raise <= BotMaximumBidAbility(botChips, raiseFactor) && _form1.Raise >= (BotMaximumBidAbility(botChips, raiseFactor)) / 2)
                        {
                            Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (_form1.Raise <= (BotMaximumBidAbility(botChips, raiseFactor)) / 2)
                        {
                            if (_form1.Raise > 0)
                            {
                                _form1.Raise = BotMaximumBidAbility(botChips, raiseFactor);
                                RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }

                            else
                            {
                                _form1.Raise = _form1.callChipsValue * 2;
                                RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }
                        }

                    }
                }
            }
            if (_form1.totalRounds >= 2)
            {
                if (_form1.callChipsValue > 0)
                {
                    if (_form1.callChipsValue >= BotMaximumBidAbility(botChips, botsPower - randomNumber))
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (_form1.Raise > BotMaximumBidAbility(botChips, raiseFactor - randomNumber))
                    {
                        ChangeStatusToFold(ref isBotsTurn, ref botFolds, labelStatus);
                    }

                    if (!botFolds)
                    {
                        if (_form1.callChipsValue >= BotMaximumBidAbility(botChips, raiseFactor - randomNumber) && _form1.callChipsValue <= BotMaximumBidAbility(botChips, botsPower - randomNumber))
                        {
                            Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (_form1.Raise <= BotMaximumBidAbility(botChips, raiseFactor - randomNumber) && _form1.Raise >= (BotMaximumBidAbility(botChips, raiseFactor - randomNumber)) / 2)
                        {
                            Call(ref botChips, ref isBotsTurn, labelStatus);
                        }

                        if (_form1.Raise <= (BotMaximumBidAbility(botChips, raiseFactor - randomNumber)) / 2)
                        {
                            if (_form1.Raise > 0)
                            {
                                _form1.Raise = BotMaximumBidAbility(botChips, raiseFactor - randomNumber);
                                RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }

                            else
                            {
                                _form1.Raise = _form1.callChipsValue * 2;
                                RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
                            }
                        }
                    }
                }

                if (_form1.callChipsValue <= 0)
                {
                    _form1.Raise = BotMaximumBidAbility(botChips, callPower - randomNumber);
                    RaiseBet(ref botChips, ref isBotsTurn, labelStatus);
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

            if (_form1.callChipsValue <= 0)
            {
                ChangeStatusToChecking(ref isBotTurn, botStatus);
            }
            else
            {
                if (_form1.callChipsValue >= BotMaximumBidAbility(botChips, behaviour))
                {
                    if (botChips > _form1.callChipsValue)
                    {
                        Call(ref botChips, ref isBotTurn, botStatus);
                    }
                    else if (botChips <= _form1.callChipsValue)
                    {
                        _form1.isRaising = false;
                        isBotTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        _form1.potTextBox.Text = (int.Parse(_form1.potTextBox.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (_form1.Raise > 0)
                    {
                        if (botChips >= _form1.Raise * 2)
                        {
                            _form1.Raise *= 2;
                            RaiseBet(ref botChips, ref isBotTurn, botStatus);
                        }
                        else
                        {
                            Call(ref botChips, ref isBotTurn, botStatus);
                        }
                    }
                    else
                    {
                        _form1.Raise = _form1.callChipsValue * 2;
                        RaiseBet(ref botChips, ref isBotTurn, botStatus);
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