using System.Linq;

namespace Poker
{
    public class HandRules
    {
        private Form1 currentForm;

        public HandRules(Form1 currentForm)
        {
            this.currentForm = currentForm;
        }

        public void RulesStraightFlush(ref double current, ref double Power, int[] clubsStrenghtValues, int[] diamondsStrenghtValues, int[] heartsStrenghtValues, int[] spadesStrenghtValues)
        {
            if (current >= -1)
            {
                if (clubsStrenghtValues.Length >= 5)
                {
                    if (clubsStrenghtValues[0] + 4 == clubsStrenghtValues[4])
                    {
                        current = 8;
                        sortWinningHands(current, out Power, clubsStrenghtValues);
                    }

                    if (clubsStrenghtValues[0] == 0 &&
                        clubsStrenghtValues[1] == 9 &&
                        clubsStrenghtValues[2] == 10 &&
                        clubsStrenghtValues[3] == 11 &&
                        clubsStrenghtValues[0] + 12 == clubsStrenghtValues[4])
                    {
                        current = 9;
                        sortWinningHands(current, out Power, clubsStrenghtValues);
                    }
                }

                if (diamondsStrenghtValues.Length >= 5)
                {
                    if (diamondsStrenghtValues[0] + 4 == diamondsStrenghtValues[4])
                    {
                        current = 8;
                        sortWinningHands(current, out Power, diamondsStrenghtValues);
                    }

                    if (diamondsStrenghtValues[0] == 0 &&
                        diamondsStrenghtValues[1] == 9 &&
                        diamondsStrenghtValues[2] == 10 &&
                        diamondsStrenghtValues[3] == 11 &&
                        diamondsStrenghtValues[0] + 12 == diamondsStrenghtValues[4])
                    {
                        current = 9;
                        sortWinningHands(current, out Power, diamondsStrenghtValues);
                    }
                }

                if (heartsStrenghtValues.Length >= 5)
                {
                    if (heartsStrenghtValues[0] + 4 == heartsStrenghtValues[4])
                    {
                        current = 8;
                        sortWinningHands(current, out Power, heartsStrenghtValues);
                    }

                    if (heartsStrenghtValues[0] == 0 &&
                        heartsStrenghtValues[1] == 9 &&
                        heartsStrenghtValues[2] == 10 &&
                        heartsStrenghtValues[3] == 11 &&
                        heartsStrenghtValues[0] + 12 == heartsStrenghtValues[4])
                    {
                        current = 9;
                        sortWinningHands(current, out Power, heartsStrenghtValues);
                    }
                }

                if (spadesStrenghtValues.Length >= 5)
                {
                    if (spadesStrenghtValues[0] + 4 == spadesStrenghtValues[4])
                    {
                        current = 8;
                        sortWinningHands(current, out Power, spadesStrenghtValues);
                    }

                    if (spadesStrenghtValues[0] == 0 &&
                        spadesStrenghtValues[1] == 9 &&
                        spadesStrenghtValues[2] == 10 &&
                        spadesStrenghtValues[3] == 11 &&
                        spadesStrenghtValues[0] + 12 == spadesStrenghtValues[4])
                    {
                        current = 9;
                        sortWinningHands(current, out Power, spadesStrenghtValues);
                    }
                }
            }
        }

        private void sortWinningHands(double current, out double Power, int[] strenght)
        {
            Power = (strenght.Max()) / 4 + current * 100;
            currentForm.winningingHands.Add(new Type() { Power = Power, Current = 8 });
            currentForm.sorted = currentForm.winningingHands
                .OrderByDescending(op1 => op1.Current)
                .ThenByDescending(op1 => op1.Power).First();
        }

        public void RulesFourOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4 &&
                        Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        current = 7;
                        Power = (Straight[j] / 4) * 4 + current * 100;
                        this.SortedWinningHands(current, Power);
                    }

                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0 && Straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        Power = 13 * 4 + current * 100;
                        this.SortedWinningHands(current, Power);
                    }
                }
            }
        }

        public void RulesFullHouse(ref double current, ref double Power, ref bool done, int[] Straight)
        {
            if (current >= -1)
            {
                currentForm.type = Power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                current = 6;
                                Power = 13 * 2 + current * 100;
                                this.SortedWinningHands(current, Power);
                                break;
                            }

                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                Power = fh.Max() / 4 * 2 + current * 100;
                                this.SortedWinningHands(current, Power);
                                break;
                            }

                        }

                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                Power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                Power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }

                if (current != 6)
                {
                    Power = currentForm.type;
                }
            }
        }

        public void RulesFlush(ref double current, ref double Power, ref bool hasFlush, int[] Straight1)
        {
            if (current >= -1)
            {
                var clubs = Straight1.Where(o => o % 4 == 0).ToArray();
                var diamonds = Straight1.Where(o => o % 4 == 1).ToArray();
                var hearts = Straight1.Where(o => o % 4 == 2).ToArray();
                var spades = Straight1.Where(o => o % 4 == 3).ToArray();

                if (clubs.Length == 3 || clubs.Length == 4)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 == currentForm.cardsAsNumbers[currentForm.i + 1] % 4 && currentForm.cardsAsNumbers[currentForm.i] % 4 == clubs[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }

                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }

                        else if (currentForm.cardsAsNumbers[currentForm.i] / 4 < clubs.Max() / 4 && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 < clubs.Max() / 4)
                        {
                            current = 5;
                            Power = clubs.Max() + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }
                    }
                }
                if (clubs.Length == 4)//different cards in hand
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 != currentForm.cardsAsNumbers[currentForm.i + 1] % 4 && currentForm.cardsAsNumbers[currentForm.i] % 4 == clubs[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            Power = clubs.Max() + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }
                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] % 4 != currentForm.cardsAsNumbers[currentForm.i] % 4 && currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == clubs[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > clubs.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            Power = clubs.Max() + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }
                    }
                }

                if (clubs.Length == 5)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 == clubs[0] % 4 && currentForm.cardsAsNumbers[currentForm.i] / 4 > clubs.Min() / 4)
                    {
                        current = 5;
                        Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                        this.SortedWinningHands(current, Power);
                        hasFlush = true;
                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == clubs[0] % 4 && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > clubs.Min() / 4)
                    {
                        current = 5;
                        Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, Power);
                        hasFlush = true;
                    }

                    else if (currentForm.cardsAsNumbers[currentForm.i] / 4 < clubs.Min() / 4 && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 < clubs.Min())
                    {
                        current = 5;
                        Power = clubs.Max() + current * 100;
                        this.SortedWinningHands(current, Power);
                        hasFlush = true;
                    }
                }

                if (diamonds.Length == 3 || diamonds.Length == 4)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 == currentForm.cardsAsNumbers[currentForm.i + 1] % 4 && currentForm.cardsAsNumbers[currentForm.i] % 4 == diamonds[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }

                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }

                        else if (currentForm.cardsAsNumbers[currentForm.i] / 4 < diamonds.Max() / 4 && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 < diamonds.Max() / 4)
                        {
                            current = 5;
                            Power = diamonds.Max() + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }
                    }
                }

                if (diamonds.Length == 4)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 != currentForm.cardsAsNumbers[currentForm.i + 1] % 4
                        && currentForm.cardsAsNumbers[currentForm.i] % 4 == diamonds[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            Power = diamonds.Max() + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }
                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] % 4 != currentForm.cardsAsNumbers[currentForm.i] % 4 && currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == diamonds[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > diamonds.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            Power = diamonds.Max() + current * 100;
                            this.SortedWinningHands(current, Power);
                            hasFlush = true;
                        }
                    }
                }

                if (diamonds.Length == 5)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 == diamonds[0] % 4
                        && currentForm.cardsAsNumbers[currentForm.i] / 4 > diamonds.Min() / 4)
                    {
                        current = 5;
                        Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                        this.SortedWinningHands(current, Power);
                        hasFlush = true;
                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == diamonds[0] % 4
                        && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > diamonds.Min() / 4)
                    {
                        current = 5;
                        Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, Power);
                        hasFlush = true;
                    }

                    else if (currentForm.cardsAsNumbers[currentForm.i] / 4 < diamonds.Min() / 4 && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 < diamonds.Min())
                    {
                        current = 5;
                        Power = diamonds.Max() + current * 100;
                        this.SortedWinningHands(current, Power);

                        hasFlush = true;
                    }
                }

                if (hearts.Length == 3 || hearts.Length == 4)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 == currentForm.cardsAsNumbers[currentForm.i + 1] % 4
                        && currentForm.cardsAsNumbers[currentForm.i] % 4 == hearts[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }

                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }

                        else if (currentForm.cardsAsNumbers[currentForm.i] / 4 < hearts.Max() / 4 && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 < hearts.Max() / 4)
                        {
                            current = 5;
                            Power = hearts.Max() + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }
                    }
                }

                if (hearts.Length == 4)//different cards in hand
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 != currentForm.cardsAsNumbers[currentForm.i + 1] % 4 && currentForm.cardsAsNumbers[currentForm.i] % 4 == hearts[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            Power = hearts.Max() + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }
                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] % 4 != currentForm.cardsAsNumbers[currentForm.i] % 4 && currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == hearts[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > hearts.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            Power = hearts.Max() + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }
                    }
                }

                if (hearts.Length == 5)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 == hearts[0] % 4
                        && currentForm.cardsAsNumbers[currentForm.i] / 4 > hearts.Min() / 4)
                    {
                        current = 5;
                        Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                        this.SortedWinningHands(current, Power);

                        hasFlush = true;
                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == hearts[0] % 4 && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > hearts.Min() / 4)
                    {
                        current = 5;
                        Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, Power);

                        hasFlush = true;
                    }

                    else if (currentForm.cardsAsNumbers[currentForm.i] / 4 < hearts.Min() / 4 && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 < hearts.Min())
                    {
                        current = 5;
                        Power = hearts.Max() + current * 100;
                        this.SortedWinningHands(current, Power);

                        hasFlush = true;
                    }
                }

                if (spades.Length == 3 || spades.Length == 4)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 == currentForm.cardsAsNumbers[currentForm.i + 1] % 4 && currentForm.cardsAsNumbers[currentForm.i] % 4 == spades[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }

                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }

                        else if (currentForm.cardsAsNumbers[currentForm.i] / 4 < spades.Max() / 4 && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 < spades.Max() / 4)
                        {
                            current = 5;
                            Power = spades.Max() + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }
                    }
                }

                if (spades.Length == 4)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 != currentForm.cardsAsNumbers[currentForm.i + 1] % 4
                        && currentForm.cardsAsNumbers[currentForm.i] % 4 == spades[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            Power = spades.Max() + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }
                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] % 4 != currentForm.cardsAsNumbers[currentForm.i] % 4 && currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == spades[0] % 4)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > spades.Max() / 4)
                        {
                            current = 5;
                            Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }
                        else
                        {
                            current = 5;
                            Power = spades.Max() + current * 100;
                            this.SortedWinningHands(current, Power);

                            hasFlush = true;
                        }
                    }
                }

                if (spades.Length == 5)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] % 4 == spades[0] % 4 && currentForm.cardsAsNumbers[currentForm.i] / 4 > spades.Min() / 4)
                    {
                        current = 5;
                        Power = currentForm.cardsAsNumbers[currentForm.i] + current * 100;
                        this.SortedWinningHands(current, Power);

                        hasFlush = true;
                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == spades[0] % 4
                        && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 > spades.Min() / 4)
                    {
                        current = 5;
                        Power = currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, Power);

                        hasFlush = true;
                    }

                    else if (currentForm.cardsAsNumbers[currentForm.i] / 4 < spades.Min() / 4
                             && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 < spades.Min())
                    {
                        current = 5;
                        Power = spades.Max() + current * 100;
                        this.SortedWinningHands(current, Power);

                        hasFlush = true;
                    }
                }

                if (clubs.Length > 0)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] / 4 == 0 && currentForm.cardsAsNumbers[currentForm.i] % 4 == clubs[0] % 4 &&
                        hasFlush && clubs.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.SortedWinningHands(current, Power);

                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 == 0 && currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == clubs[0] % 4 &&
                        hasFlush && clubs.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.SortedWinningHands(current, Power);

                    }
                }
                if (diamonds.Length > 0)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] / 4 == 0 && currentForm.cardsAsNumbers[currentForm.i] % 4 == diamonds[0] % 4 &&
                        hasFlush && diamonds.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.SortedWinningHands(current, Power);

                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 == 0 && currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == diamonds[0] % 4 &&
                        hasFlush && diamonds.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.SortedWinningHands(current, Power);

                    }
                }
                if (hearts.Length > 0)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] / 4 == 0 && currentForm.cardsAsNumbers[currentForm.i] % 4 == hearts[0] % 4 &&
                        hasFlush && hearts.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.SortedWinningHands(current, Power);

                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 == 0 && currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == hearts[0] % 4 && hasFlush && hearts.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.SortedWinningHands(current, Power);

                    }
                }

                if (spades.Length > 0)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i] / 4 == 0 && currentForm.cardsAsNumbers[currentForm.i] % 4 == spades[0] % 4 &&
                        hasFlush && spades.Length > 0)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.SortedWinningHands(current, Power);

                    }

                    if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 == 0 && currentForm.cardsAsNumbers[currentForm.i + 1] % 4 == spades[0] % 4 &&
                        hasFlush)
                    {
                        current = 5.5;
                        Power = 13 + current * 100;
                        this.SortedWinningHands(current, Power);

                    }
                }
            }
        }

        public void RulesStraight(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                var op = Straight.Select(o => o / 4)
                    .Distinct()
                    .ToArray();

                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            Power = op.Max() + current * 100;
                            this.SortedWinningHands(current, Power);

                        }

                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current * 100;
                            this.SortedWinningHands(current, Power);

                        }
                    }

                    if (op[j] == 0 && op[j + 1] == 9 &&
                        op[j + 2] == 10 &&
                        op[j + 3] == 11 &&
                        op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current * 100;
                        this.SortedWinningHands(current, Power);

                    }
                }
            }
        }

        public void RulesThreeOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j)
                        .ToArray();

                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            Power = 13 * 3 + current * 100;
                            this.SortedWinningHands(current, Power);

                        }
                        else
                        {
                            current = 3;
                            Power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            this.SortedWinningHands(current, Power);

                        }
                    }
                }
            }
        }

        public void RulesTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;

                for (int totalCards = 16; totalCards >= 12; totalCards--)
                {
                    int max = totalCards - 12;
                    if (currentForm.cardsAsNumbers[currentForm.i] / 4 != currentForm.cardsAsNumbers[currentForm.i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (totalCards - k < 12)
                            {
                                max--;
                            }

                            if (totalCards - k >= 12)
                            {
                                if (currentForm.cardsAsNumbers[currentForm.i] / 4 == currentForm.cardsAsNumbers[totalCards] / 4 && currentForm.cardsAsNumbers[currentForm.i + 1] / 4 == currentForm.cardsAsNumbers[totalCards - k] / 4 || currentForm.cardsAsNumbers[currentForm.i + 1] / 4 == currentForm.cardsAsNumbers[totalCards] / 4 && currentForm.cardsAsNumbers[currentForm.i] / 4 == currentForm.cardsAsNumbers[totalCards - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (currentForm.cardsAsNumbers[currentForm.i + 1] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, Power);

                                        }

                                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = 13 * 4 + (currentForm.cardsAsNumbers[currentForm.i] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, Power);

                                        }

                                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 != 0 && currentForm.cardsAsNumbers[currentForm.i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (currentForm.cardsAsNumbers[currentForm.i] / 4) * 2 + (currentForm.cardsAsNumbers[currentForm.i + 1] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, Power);

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

        public void RulesPairTwoPair(ref double current, ref double Power)
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
                            if (currentForm.cardsAsNumbers[totalCards] / 4 == currentForm.cardsAsNumbers[totalCards - k] / 4)
                            {
                                if (currentForm.cardsAsNumbers[totalCards] / 4 != currentForm.cardsAsNumbers[currentForm.i] / 4 && currentForm.cardsAsNumbers[totalCards] / 4 != currentForm.cardsAsNumbers[currentForm.i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (currentForm.cardsAsNumbers[currentForm.i] / 4) * 2 + 13 * 4 + current * 100;
                                            this.SortedWinningHands(current, Power);

                                        }

                                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 == 0)
                                        {
                                            current = 2;
                                            Power = (currentForm.cardsAsNumbers[currentForm.i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            this.SortedWinningHands(current, Power);

                                        }

                                        if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (currentForm.cardsAsNumbers[totalCards] / 4) * 2 + (currentForm.cardsAsNumbers[currentForm.i + 1] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, Power);

                                        }

                                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 != 0)
                                        {
                                            current = 2;
                                            Power = (currentForm.cardsAsNumbers[totalCards] / 4) * 2 + (currentForm.cardsAsNumbers[currentForm.i] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, Power);

                                        }
                                    }

                                    msgbox = true;
                                }

                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 > currentForm.cardsAsNumbers[currentForm.i + 1] / 4)
                                        {
                                            if (currentForm.cardsAsNumbers[totalCards] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + currentForm.cardsAsNumbers[currentForm.i] / 4 + current * 100;
                                                this.SortedWinningHands(current, Power);

                                            }

                                            else
                                            {
                                                current = 0;
                                                Power = currentForm.cardsAsNumbers[totalCards] / 4 + currentForm.cardsAsNumbers[currentForm.i] / 4 + current * 100;
                                                this.SortedWinningHands(current, Power);

                                            }
                                        }
                                        else
                                        {
                                            if (currentForm.cardsAsNumbers[totalCards] / 4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + currentForm.cardsAsNumbers[currentForm.i + 1] + current * 100;
                                                this.SortedWinningHands(current, Power);

                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = currentForm.cardsAsNumbers[totalCards] / 4 + currentForm.cardsAsNumbers[currentForm.i + 1] / 4 + current * 100;
                                                this.SortedWinningHands(current, Power);

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

        public void RulesPairFromHand(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                if (currentForm.cardsAsNumbers[currentForm.i] / 4 == currentForm.cardsAsNumbers[currentForm.i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (currentForm.cardsAsNumbers[currentForm.i] / 4 == 0)
                        {
                            current = 1;
                            Power = 13 * 4 + current * 100;
                            this.SortedWinningHands(current, Power);

                        }
                        else
                        {
                            current = 1;
                            Power = (currentForm.cardsAsNumbers[currentForm.i + 1] / 4) * 4 + current * 100;
                            this.SortedWinningHands(current, Power);

                        }
                    }

                    msgbox = true;
                }

                for (int totalCards = 16; totalCards >= 12; totalCards--)
                {
                    if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 == currentForm.cardsAsNumbers[totalCards] / 4)
                    {
                        if (!msgbox)
                        {
                            if (currentForm.cardsAsNumbers[currentForm.i + 1] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + currentForm.cardsAsNumbers[currentForm.i] / 4 + current * 100;
                                this.SortedWinningHands(current, Power);

                            }
                            else
                            {
                                current = 1;
                                Power = (currentForm.cardsAsNumbers[currentForm.i + 1] / 4) * 4 + currentForm.cardsAsNumbers[currentForm.i] / 4 + current * 100;
                                this.SortedWinningHands(current, Power);

                            }
                        }

                        msgbox = true;
                    }

                    if (currentForm.cardsAsNumbers[currentForm.i] / 4 == currentForm.cardsAsNumbers[totalCards] / 4)
                    {
                        if (!msgbox)
                        {
                            if (currentForm.cardsAsNumbers[currentForm.i] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + currentForm.cardsAsNumbers[currentForm.i + 1] / 4 + current * 100;
                                this.SortedWinningHands(current, Power);

                            }
                            else
                            {
                                current = 1;
                                Power = (currentForm.cardsAsNumbers[totalCards] / 4) * 4 + currentForm.cardsAsNumbers[currentForm.i + 1] / 4 + current * 100;
                                this.SortedWinningHands(current, Power);

                            }
                        }

                        msgbox = true;
                    }
                }
            }
        }

        public void RulesHighCard(ref double current, ref double Power)
        {
            if (current == -1)
            {
                if (currentForm.cardsAsNumbers[currentForm.i] / 4 > currentForm.cardsAsNumbers[currentForm.i + 1] / 4)
                {
                    current = -1;
                    Power = currentForm.cardsAsNumbers[currentForm.i] / 4;
                    this.SortedWinningHands(current, Power);

                }
                else
                {
                    current = -1;
                    Power = currentForm.cardsAsNumbers[currentForm.i + 1] / 4;
                    this.SortedWinningHands(current, Power);

                }

                if (currentForm.cardsAsNumbers[currentForm.i] / 4 == 0 || currentForm.cardsAsNumbers[currentForm.i + 1] / 4 == 0)
                {
                    current = -1;
                    Power = 13;
                    this.SortedWinningHands(current, Power);

                }
            }
        }

        private void SortedWinningHands(double current, double Power)
        {
            this.currentForm.winningingHands.Add(new Type() { Power = Power, Current = current });
            this.currentForm.sorted =
                this.currentForm.winningingHands.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
        }
    }
}