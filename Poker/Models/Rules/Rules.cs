namespace Poker.Models.Rules
{
    using System;
    using System.Linq;

    using Type = Poker.Type;

    public class Rules
    {
        private GameManager currentForm;

        public Rules(GameManager currentForm)
        {
            this.currentForm = currentForm;
        }

        public void GameRules(int cardOne, int cardTwo, ref double current, ref double power, bool foldedTurn)
        {
            if (!foldedTurn || cardOne == 0 && cardTwo == 1 && this.currentForm.playerStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false, hasFlush = false;
                int[] cardsOnTheTable = new int[5];
                int[] currentPlayerAndTableCards = new int[7];

                currentPlayerAndTableCards[0] = this.currentForm.cardsAsNumbers[cardOne];
                currentPlayerAndTableCards[1] = this.currentForm.cardsAsNumbers[cardTwo];
                currentPlayerAndTableCards[2] = this.currentForm.cardsAsNumbers[12];
                currentPlayerAndTableCards[3] = this.currentForm.cardsAsNumbers[13];
                currentPlayerAndTableCards[4] = this.currentForm.cardsAsNumbers[14];
                currentPlayerAndTableCards[5] = this.currentForm.cardsAsNumbers[15];
                currentPlayerAndTableCards[6] = this.currentForm.cardsAsNumbers[16];
               
                cardsOnTheTable[0] = this.currentForm.cardsAsNumbers[12];
                cardsOnTheTable[1] = this.currentForm.cardsAsNumbers[13];
                cardsOnTheTable[2] = this.currentForm.cardsAsNumbers[14];
                cardsOnTheTable[3] = this.currentForm.cardsAsNumbers[15];
                cardsOnTheTable[4] = this.currentForm.cardsAsNumbers[16];
               
                var clubs = currentPlayerAndTableCards.Where(o => o % 4 == 0).ToArray();
                var diamonds = currentPlayerAndTableCards.Where(o => o % 4 == 1).ToArray();
                var hearts = currentPlayerAndTableCards.Where(o => o % 4 == 2).ToArray();
                var spades = currentPlayerAndTableCards.Where(o => o % 4 == 3).ToArray();
               
                var clubsStrenghtValues = clubs.Select(o => o / 4).Distinct().ToArray();
                var diamondsStrenghtValues = diamonds.Select(o => o / 4).Distinct().ToArray();
                var heartsStrenghtValues = hearts.Select(o => o / 4).Distinct().ToArray();
                var spadesStrenghtValues = spades.Select(o => o / 4).Distinct().ToArray();
               
                Array.Sort(currentPlayerAndTableCards);
                Array.Sort(clubsStrenghtValues);
                Array.Sort(diamondsStrenghtValues);
                Array.Sort(heartsStrenghtValues);
                Array.Sort(spadesStrenghtValues);
                #endregion

                for (int i = 0; i < 16; i++)
                {
                    if (this.currentForm.cardsAsNumbers[i] == int.Parse(this.currentForm.cardsImages[cardOne].Tag.ToString()) && this.currentForm.cardsAsNumbers[i + 1] == int.Parse(this.currentForm.cardsImages[cardTwo].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        this.RulesPairFromHand(ref current, ref power);

                        #region Pair or Two Pair from Table current = 2 || 0
                        this.RulesPairTwoPair(ref current, ref power);
                        #endregion

                        #region Two Pair current = 2
                        this.RulesTwoPair(ref current, ref power);
                        #endregion

                        #region Three of a kind current = 3
                        this.RulesThreeOfAKind(ref current, ref power, currentPlayerAndTableCards);
                        #endregion

                        #region CheckBotsStraight current = 4
                        this.RulesStraight(ref current, ref power, currentPlayerAndTableCards);
                        #endregion

                        #region CheckBotsFlush current = 5 || 5.5
                        this.RulesFlush(ref current, ref power, ref hasFlush, cardsOnTheTable);
                        #endregion

                        #region Full House current = 6
                        this.RulesFullHouse(ref current, ref power, ref done, currentPlayerAndTableCards);
                        #endregion

                        #region Four of a Kind current = 7
                        this.RulesFourOfAKind(ref current, ref power, currentPlayerAndTableCards);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        this.RulesStraightFlush(ref current, ref power, clubsStrenghtValues, diamondsStrenghtValues, heartsStrenghtValues, spadesStrenghtValues);
                        #endregion

                        #region High Card current = -1
                        this.RulesHighCard(ref current, ref power);
                        #endregion
                    }
                }
            }
        }


        private void RulesStraightFlush(ref double current, ref double power, int[] clubsStrenghtValues, int[] diamondsStrenghtValues, int[] heartsStrenghtValues, int[] spadesStrenghtValues)
        {
            if (current >= -1)
            {
                if (clubsStrenghtValues.Length >= 5)
                {
                    if (clubsStrenghtValues[0] + 4 == clubsStrenghtValues[4])
                    {
                        current = 8;
                        power = (clubsStrenghtValues.Max()) / 4 + current * 100;
                        this.SortedWinningHands(current, power);

                    }

                    if (clubsStrenghtValues[0] == 0 &&
                        clubsStrenghtValues[1] == 9 &&
                        clubsStrenghtValues[2] == 10 &&
                        clubsStrenghtValues[3] == 11 &&
                        clubsStrenghtValues[0] + 12 == clubsStrenghtValues[4])
                    {
                        current = 9;
                        power = (clubsStrenghtValues.Max()) / 4 + current * 100;
                        this.SortedWinningHands(current, power);

                    }
                }

                if (diamondsStrenghtValues.Length >= 5)
                {
                    if (diamondsStrenghtValues[0] + 4 == diamondsStrenghtValues[4])
                    {
                        current = 8;
                        power = (diamondsStrenghtValues.Max()) / 4 + current * 100;
                        this.SortedWinningHands(current, power);

                    }

                    if (diamondsStrenghtValues[0] == 0 &&
                        diamondsStrenghtValues[1] == 9 &&
                        diamondsStrenghtValues[2] == 10 &&
                        diamondsStrenghtValues[3] == 11 &&
                        diamondsStrenghtValues[0] + 12 == diamondsStrenghtValues[4])
                    {
                        current = 9;
                        power = (diamondsStrenghtValues.Max()) / 4 + current * 100;
                        this.SortedWinningHands(current, power);
                    }
                }

                if (heartsStrenghtValues.Length >= 5)
                {
                    if (heartsStrenghtValues[0] + 4 == heartsStrenghtValues[4])
                    {
                        current = 8;
                        power = (heartsStrenghtValues.Max()) / 4 + current * 100;
                        this.SortedWinningHands(current, power);

                    }

                    if (heartsStrenghtValues[0] == 0 &&
                        heartsStrenghtValues[1] == 9 &&
                        heartsStrenghtValues[2] == 10 &&
                        heartsStrenghtValues[3] == 11 &&
                        heartsStrenghtValues[0] + 12 == heartsStrenghtValues[4])
                    {
                        current = 9;
                        power = (heartsStrenghtValues.Max()) / 4 + current * 100;
                        this.SortedWinningHands(current, power);
                    }
                }

                if (spadesStrenghtValues.Length >= 5)
                {
                    if (spadesStrenghtValues[0] + 4 == spadesStrenghtValues[4])
                    {
                        current = 8;
                        power = (spadesStrenghtValues.Max()) / 4 + current * 100;
                        this.SortedWinningHands(current, power);
                    }

                    if (spadesStrenghtValues[0] == 0 &&
                        spadesStrenghtValues[1] == 9 &&
                        spadesStrenghtValues[2] == 10 &&
                        spadesStrenghtValues[3] == 11 &&
                        spadesStrenghtValues[0] + 12 == spadesStrenghtValues[4])
                    {
                        current = 9;
                        power = (spadesStrenghtValues.Max()) / 4 + current * 100;
                        this.SortedWinningHands(current, power);
                    }
                }
            }
        }

        private void RulesFourOfAKind(ref double current, ref double power, int[] straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (straight[j] / 4 == straight[j + 1] / 4 && straight[j] / 4 == straight[j + 2] / 4 &&
                        straight[j] / 4 == straight[j + 3] / 4)
                    {
                        current = 7;
                        power = (straight[j] / 4) * 4 + current * 100;
                        this.SortedWinningHands(current, power);
                    }

                    if (straight[j] / 4 == 0 && straight[j + 1] / 4 == 0 && straight[j + 2] / 4 == 0 && straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        power = 13 * 4 + current * 100;
                        this.SortedWinningHands(current, power);
                    }
                }
            }
        }

        private void RulesFullHouse(ref double current, ref double power, ref bool done, int[] straight)
        {
            if (current >= -1)
            {
                this.currentForm.type = power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                current = 6;
                                power = 13 * 2 + current * 100;
                                this.SortedWinningHands(current, power);
                                break;
                            }

                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                power = fh.Max() / 4 * 2 + current * 100;
                                this.SortedWinningHands(current, power);
                                break;
                            }

                        }

                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }

                if (current != 6)
                {
                    power = this.currentForm.type;
                }
            }
        }

        private void RulesFlush(ref double current, ref double power, ref bool hasFlush, int[] straight1)
        {
            if (current >= -1)
            {
                var clubs = straight1.Where(o => o % 4 == 0).ToArray();
                var diamonds = straight1.Where(o => o % 4 == 1).ToArray();
                var hearts = straight1.Where(o => o % 4 == 2).ToArray();
                var spades = straight1.Where(o => o % 4 == 3).ToArray();

                if (clubs.Length == 3 || clubs.Length == 4)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == clubs[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }

                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }

                        else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 < clubs.Max() / 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 < clubs.Max() / 4)
                        {
                            current = 5;
                            power = clubs.Max() + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                    }
                }
                if (clubs.Length == 4)//different cards in hand
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 != this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == clubs[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            power = clubs.Max() + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 != this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == clubs[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            power = clubs.Max() + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                    }
                }

                if (clubs.Length == 5)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == clubs[0] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > clubs.Min() / 4)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == clubs[0] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > clubs.Min() / 4)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }

                    else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 < clubs.Min() / 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 < clubs.Min())
                    {
                        current = 5;
                        power = clubs.Max() + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }
                }

                if (diamonds.Length == 3 || diamonds.Length == 4)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == diamonds[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }

                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }

                        else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 < diamonds.Max() / 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 < diamonds.Max() / 4)
                        {
                            current = 5;
                            power = diamonds.Max() + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                    }
                }

                if (diamonds.Length == 4)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 != this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4
                        && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == diamonds[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            power = diamonds.Max() + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 != this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == diamonds[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            power = diamonds.Max() + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                    }
                }

                if (diamonds.Length == 5)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == diamonds[0] % 4
                        && this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > diamonds.Min() / 4)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == diamonds[0] % 4
                        && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > diamonds.Min() / 4)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }

                    else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 < diamonds.Min() / 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 < diamonds.Min())
                    {
                        current = 5;
                        power = diamonds.Max() + current * 100;
                        this.SortedWinningHands(current, power);

                        hasFlush = true;
                    }
                }

                if (hearts.Length == 3 || hearts.Length == 4)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4
                        && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == hearts[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }

                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }

                        else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 < hearts.Max() / 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 < hearts.Max() / 4)
                        {
                            current = 5;
                            power = hearts.Max() + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                    }
                }

                if (hearts.Length == 4)//different cards in hand
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 != this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == hearts[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            power = hearts.Max() + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 != this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == hearts[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            power = hearts.Max() + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                    }
                }

                if (hearts.Length == 5)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == hearts[0] % 4
                        && this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > hearts.Min() / 4)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                        this.SortedWinningHands(current, power);

                        hasFlush = true;
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == hearts[0] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > hearts.Min() / 4)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, power);

                        hasFlush = true;
                    }

                    else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 < hearts.Min() / 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 < hearts.Min())
                    {
                        current = 5;
                        power = hearts.Max() + current * 100;
                        this.SortedWinningHands(current, power);

                        hasFlush = true;
                    }
                }

                if (spades.Length == 3 || spades.Length == 4)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == spades[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }

                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }

                        else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 < spades.Max() / 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 < spades.Max() / 4)
                        {
                            current = 5;
                            power = spades.Max() + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                    }
                }

                if (spades.Length == 4)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 != this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4
                        && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == spades[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            power = spades.Max() + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 != this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == spades[0] % 4)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            power = spades.Max() + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                    }
                }

                if (spades.Length == 5)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == spades[0] % 4 && this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > spades.Min() / 4)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                        this.SortedWinningHands(current, power);

                        hasFlush = true;
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == spades[0] % 4
                        && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 > spades.Min() / 4)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, power);

                        hasFlush = true;
                    }

                    else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 < spades.Min() / 4
                             && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 < spades.Min())
                    {
                        current = 5;
                        power = spades.Max() + current * 100;
                        this.SortedWinningHands(current, power);

                        hasFlush = true;
                    }
                }

                if (clubs.Length > 0)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == 0 && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == clubs[0] % 4 &&
                        hasFlush && clubs.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);

                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 == 0 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == clubs[0] % 4 &&
                        hasFlush && clubs.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);

                    }
                }
                if (diamonds.Length > 0)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == 0 && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == diamonds[0] % 4 &&
                        hasFlush && diamonds.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);

                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 == 0 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == diamonds[0] % 4 &&
                        hasFlush && diamonds.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);

                    }
                }
                if (hearts.Length > 0)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == 0 && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == hearts[0] % 4 &&
                        hasFlush && hearts.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);

                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 == 0 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == hearts[0] % 4 && hasFlush && hearts.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);

                    }
                }

                if (spades.Length > 0)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == 0 && this.currentForm.cardsAsNumbers[this.currentForm.i] % 4 == spades[0] % 4 &&
                        hasFlush && spades.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);

                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 == 0 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % 4 == spades[0] % 4 &&
                        hasFlush)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);

                    }
                }
            }
        }

        private void RulesStraight(ref double current, ref double power, int[] straight)
        {
            if (current >= -1)
            {
                var op = straight.Select(o => o / 4)
                    .Distinct()
                    .ToArray();

                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            power = op.Max() + current * 100;
                            this.SortedWinningHands(current, power);

                        }

                        else
                        {
                            current = 4;
                            power = op[j + 4] + current * 100;
                            this.SortedWinningHands(current, power);

                        }
                    }

                    if (op[j] == 0 && op[j + 1] == 9 &&
                        op[j + 2] == 10 &&
                        op[j + 3] == 11 &&
                        op[j + 4] == 12)
                    {
                        current = 4;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);

                    }
                }
            }
        }

        private void RulesThreeOfAKind(ref double current, ref double power, int[] straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = straight.Where(o => o / 4 == j)
                        .ToArray();

                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            power = 13 * 3 + current * 100;
                            this.SortedWinningHands(current, power);

                        }
                        else
                        {
                            current = 3;
                            power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            this.SortedWinningHands(current, power);

                        }
                    }
                }
            }
        }

        private void RulesTwoPair(ref double current, ref double power)
        {
            if (current >= -1)
            {
                bool msgbox = false;

                for (int totalCards = 16; totalCards >= 12; totalCards--)
                {
                    int max = totalCards - 12;
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 != this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (totalCards - k < 12)
                            {
                                max--;
                            }

                            if (totalCards - k >= 12)
                            {
                                if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == this.currentForm.cardsAsNumbers[totalCards] / 4 && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 == this.currentForm.cardsAsNumbers[totalCards - k] / 4 || this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 == this.currentForm.cardsAsNumbers[totalCards] / 4 && this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == this.currentForm.cardsAsNumbers[totalCards - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == 0)
                                        {
                                            current = 2;
                                            power = 13 * 4 + (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, power);

                                        }

                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            power = 13 * 4 + (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, power);

                                        }

                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 != 0 && this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 != 0)
                                        {
                                            current = 2;
                                            power = (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4) * 2 + (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, power);

                                        }
                                    }

                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RulesPairTwoPair(ref double current, ref double power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;

                for (int totalCards = 16; totalCards >= 12; totalCards--)
                {
                    int max = totalCards - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (totalCards - k < 12)
                        {
                            max--;
                        }

                        if (totalCards - k >= 12)
                        {
                            if (this.currentForm.cardsAsNumbers[totalCards] / 4 == this.currentForm.cardsAsNumbers[totalCards - k] / 4)
                            {
                                if (this.currentForm.cardsAsNumbers[totalCards] / 4 != this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 && this.currentForm.cardsAsNumbers[totalCards] / 4 != this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            power = (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4) * 2 + 13 * 4 + current * 100;
                                            this.SortedWinningHands(current, power);

                                        }

                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == 0)
                                        {
                                            current = 2;
                                            power = (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            this.SortedWinningHands(current, power);

                                        }

                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            power = (this.currentForm.cardsAsNumbers[totalCards] / 4) * 2 + (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, power);

                                        }

                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 != 0)
                                        {
                                            current = 2;
                                            power = (this.currentForm.cardsAsNumbers[totalCards] / 4) * 2 + (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, power);

                                        }
                                    }

                                    msgbox = true;
                                }

                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4)
                                        {
                                            if (this.currentForm.cardsAsNumbers[totalCards] / 4 == 0)
                                            {
                                                current = 0;
                                                power = 13 + this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 + current * 100;
                                                this.SortedWinningHands(current, power);

                                            }

                                            else
                                            {
                                                current = 0;
                                                power = this.currentForm.cardsAsNumbers[totalCards] / 4 + this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 + current * 100;
                                                this.SortedWinningHands(current, power);

                                            }
                                        }
                                        else
                                        {
                                            if (this.currentForm.cardsAsNumbers[totalCards] / 4 == 0)
                                            {
                                                current = 0;
                                                power = 13 + this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                                                this.SortedWinningHands(current, power);

                                            }
                                            else
                                            {
                                                current = 0;
                                                power = this.currentForm.cardsAsNumbers[totalCards] / 4 + this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 + current * 100;
                                                this.SortedWinningHands(current, power);

                                            }
                                        }
                                    }

                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RulesPairFromHand(ref double current, ref double power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == 0)
                        {
                            current = 1;
                            power = 13 * 4 + current * 100;
                            this.SortedWinningHands(current, power);

                        }
                        else
                        {
                            current = 1;
                            power = (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4) * 4 + current * 100;
                            this.SortedWinningHands(current, power);

                        }
                    }

                    msgbox = true;
                }

                for (int totalCards = 16; totalCards >= 12; totalCards--)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 == this.currentForm.cardsAsNumbers[totalCards] / 4)
                    {
                        if (!msgbox)
                        {
                            if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 == 0)
                            {
                                current = 1;
                                power = 13 * 4 + this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 + current * 100;
                                this.SortedWinningHands(current, power);

                            }
                            else
                            {
                                current = 1;
                                power = (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4) * 4 + this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 + current * 100;
                                this.SortedWinningHands(current, power);

                            }
                        }

                        msgbox = true;
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == this.currentForm.cardsAsNumbers[totalCards] / 4)
                    {
                        if (!msgbox)
                        {
                            if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == 0)
                            {
                                current = 1;
                                power = 13 * 4 + this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 + current * 100;
                                this.SortedWinningHands(current, power);

                            }
                            else
                            {
                                current = 1;
                                power = (this.currentForm.cardsAsNumbers[totalCards] / 4) * 4 + this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 + current * 100;
                                this.SortedWinningHands(current, power);

                            }
                        }

                        msgbox = true;
                    }
                }
            }
        }

        private void RulesHighCard(ref double current, ref double power)
        {
            if (current == -1)
            {
                if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 > this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4)
                {
                    current = -1;
                    power = this.currentForm.cardsAsNumbers[this.currentForm.i] / 4;
                    this.SortedWinningHands(current, power);

                }
                else
                {
                    current = -1;
                    power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4;
                    this.SortedWinningHands(current, power);

                }

                if (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 == 0 || this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4 == 0)
                {
                    current = -1;
                    power = 13;
                    this.SortedWinningHands(current, power);

                }
            }
        }

        private void SortedWinningHands(double current, double power)
        {
            this.currentForm.winningingHands.Add(new Type() { Power = power, Current = current });
            this.currentForm.sorted =
                this.currentForm.winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        }
    }
}