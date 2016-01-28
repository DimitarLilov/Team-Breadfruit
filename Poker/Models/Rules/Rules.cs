namespace Poker.Models.Rules
{
    using System;
    using System.Linq;

    using Poker.Constants;

    using Type = Poker.Type;

    public class Rules
    {
        #region fields
        private GameManager currentForm;
        private bool uniteTest;
        private bool hasSucsuccessfullyExecutedRulesPairFromHand;
        private bool hasSucsuccessfullyExecutedRulesTwoPair;
        private bool hasSucsuccessfullyExecutedRulesThreeOfAKind;
        private bool hasSucsuccessfullyExecutedRulesStraightFlush;
        private bool hasSucsuccessfullyExecutedRulesFullHouse;
        private bool hasSucsuccessfullyExecutedRulesFourOfAKind;
        private bool hasSucsuccessfullyExecutedRulesHighCard;
        private bool hasSucsuccessfullyExecutedRulesStraight;
        private bool hasSucsuccessfullyExecutedRulesFlush;
        private bool hasSucsuccessfullyExecutedRulesPairTwoPair;
        #endregion

        public Rules(GameManager currentForm)
        {
            this.currentForm = currentForm;
        }

        #region properties
        public bool HasSucsuccessfullyExecutedRulesStraight
        {
            get { return hasSucsuccessfullyExecutedRulesStraight; }
            private set { hasSucsuccessfullyExecutedRulesStraight = value; }
        }

        public bool UniteTest
        {
            get { return uniteTest; }
            private set { uniteTest = value; }
        }

        public bool HasSucsuccessfullyExecutedRulesPairFromHand
        {
            get { return hasSucsuccessfullyExecutedRulesPairFromHand; }
            private set { hasSucsuccessfullyExecutedRulesPairFromHand = value; }
        }

        public bool HasSucsuccessfullyExecutedRulesTwoPair
        {
            get { return hasSucsuccessfullyExecutedRulesTwoPair; }
            private set { hasSucsuccessfullyExecutedRulesTwoPair = value; }
        }

        public bool HasSucsuccessfullyExecutedRulesThreeOfAKind
        {
            get { return hasSucsuccessfullyExecutedRulesThreeOfAKind; }
            private set { hasSucsuccessfullyExecutedRulesThreeOfAKind = value; }
        }

        public bool HasSucsuccessfullyExecutedRulesStraightFlush
        {
            get { return hasSucsuccessfullyExecutedRulesStraightFlush; }
            private set { hasSucsuccessfullyExecutedRulesStraightFlush = value; }
        }

        public bool HasSucsuccessfullyExecutedRulesFullHouse
        {
            get { return hasSucsuccessfullyExecutedRulesFullHouse; }
            private set { hasSucsuccessfullyExecutedRulesFullHouse = value; }
        }

        public bool HasSucsuccessfullyExecutedRulesFourOfAKind
        {
            get { return hasSucsuccessfullyExecutedRulesFourOfAKind; }
            private set { hasSucsuccessfullyExecutedRulesFourOfAKind = value; }
        }

        public bool HasSucsuccessfullyExecutedRulesHighCard
        {
            get { return hasSucsuccessfullyExecutedRulesHighCard; }
            private set { hasSucsuccessfullyExecutedRulesHighCard = value; }
        }

        public bool HasSucsuccessfullyExecutedRulesPairTwoPair
        {
            get { return hasSucsuccessfullyExecutedRulesPairTwoPair; }
            set { hasSucsuccessfullyExecutedRulesPairTwoPair = value; }
        }

        public bool HasSucsuccessfullyExecutedRulesFlush
        {
            get { return hasSucsuccessfullyExecutedRulesFlush; }
            private set { hasSucsuccessfullyExecutedRulesFlush = value; }
        }
        #endregion

        public void GameRulesCreator(int cardOne, int cardTwo, ref double current, ref double power, bool foldedTurn)
        {
            if (!foldedTurn || cardOne == 0 && cardTwo == 1 && this.currentForm.playerStatus.Text.Contains(Constants.Fold) == false)
            {
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

                var clubs = currentPlayerAndTableCards.Where(o => o % Constants.Devider == 0).ToArray();
                var diamonds = currentPlayerAndTableCards.Where(o => o % Constants.Devider == 1).ToArray();
                var hearts = currentPlayerAndTableCards.Where(o => o % Constants.Devider == 2).ToArray();
                var spades = currentPlayerAndTableCards.Where(o => o % Constants.Devider == 3).ToArray();

                var clubsStrenghtValues = clubs.Select(o => o / Constants.Devider).Distinct().ToArray();
                var diamondsStrenghtValues = diamonds.Select(o => o / Constants.Devider).Distinct().ToArray();
                var heartsStrenghtValues = hearts.Select(o => o / Constants.Devider).Distinct().ToArray();
                var spadesStrenghtValues = spades.Select(o => o / Constants.Devider).Distinct().ToArray();

                Array.Sort(currentPlayerAndTableCards);
                Array.Sort(clubsStrenghtValues);
                Array.Sort(diamondsStrenghtValues);
                Array.Sort(heartsStrenghtValues);
                Array.Sort(spadesStrenghtValues);

                for (int i = 0; i < 16; i++)
                {
                    if (this.currentForm.cardsAsNumbers[i] == int.Parse(this.currentForm.cardsImages[cardOne].Tag.ToString()) &&
                        this.currentForm.cardsAsNumbers[i + 1] == int.Parse(this.currentForm.cardsImages[cardTwo].Tag.ToString()))
                    {
                        this.UniteTest = true;
                        this.RulesPairTwoPair(ref current, ref power);

                        this.RulesTwoPair(ref current, ref power);

                        this.RulesThreeOfAKind(ref current, ref power, currentPlayerAndTableCards);

                        this.RulesStraight(ref current, ref power, currentPlayerAndTableCards);

                        this.RulesFlush(ref current, ref power, ref hasFlush, cardsOnTheTable);

                        this.RulesFullHouse(ref current, ref power, ref done, currentPlayerAndTableCards);

                        this.RulesFourOfAKind(ref current, ref power, currentPlayerAndTableCards);

                        this.RulesStraightFlush(ref current, ref power, clubsStrenghtValues, diamondsStrenghtValues, heartsStrenghtValues, spadesStrenghtValues);

                        this.RulesHighCard(ref current, ref power);
                    }
                }
            }
        }

        private void RulesStraightFlush(
            ref double current,
            ref double power,
            int[] clubsStrenghtValues,
            int[] diamondsStrenghtValues,
            int[] heartsStrenghtValues,
            int[] spadesStrenghtValues)
        {
            this.HasSucsuccessfullyExecutedRulesStraightFlush = true;
            if (current >= -1)
            {
                if (clubsStrenghtValues.Length >= 5)
                {
                    if (clubsStrenghtValues[0] + 4 == clubsStrenghtValues[4])
                    {
                        current = 8;
                        power = clubsStrenghtValues.Max() / Constants.Devider + current * 100;
                        this.SortedWinningHands(current, power);

                    }

                    if (clubsStrenghtValues[0] == 0 &&
                        clubsStrenghtValues[1] == 9 &&
                        clubsStrenghtValues[2] == 10 &&
                        clubsStrenghtValues[3] == 11 &&
                        clubsStrenghtValues[0] + 12 == clubsStrenghtValues[4])
                    {
                        current = 9;
                        power = (clubsStrenghtValues.Max()) / Constants.Devider + current * 100;
                        this.SortedWinningHands(current, power);
                    }
                }

                if (diamondsStrenghtValues.Length >= 5)
                {
                    if (diamondsStrenghtValues[0] + 4 == diamondsStrenghtValues[4])
                    {
                        current = 8;
                        power = diamondsStrenghtValues.Max() / Constants.Devider + current * 100;
                        this.SortedWinningHands(current, power);
                    }

                    if (diamondsStrenghtValues[0] == 0 &&
                        diamondsStrenghtValues[1] == 9 &&
                        diamondsStrenghtValues[2] == 10 &&
                        diamondsStrenghtValues[3] == 11 &&
                        diamondsStrenghtValues[0] + 12 == diamondsStrenghtValues[4])
                    {
                        current = 9;
                        power = diamondsStrenghtValues.Max() / Constants.Devider + current * 100;
                        this.SortedWinningHands(current, power);
                    }
                }

                if (heartsStrenghtValues.Length >= 5)
                {
                    if (heartsStrenghtValues[0] + 4 == heartsStrenghtValues[4])
                    {
                        current = 8;
                        power = heartsStrenghtValues.Max() / Constants.Devider + current * 100;
                        this.SortedWinningHands(current, power);
                    }

                    if (heartsStrenghtValues[0] == 0 &&
                        heartsStrenghtValues[1] == 9 &&
                        heartsStrenghtValues[2] == 10 &&
                        heartsStrenghtValues[3] == 11 &&
                        heartsStrenghtValues[0] + 12 == heartsStrenghtValues[4])
                    {
                        current = 9;
                        power = heartsStrenghtValues.Max() / Constants.Devider + current * 100;
                        this.SortedWinningHands(current, power);
                    }
                }

                if (spadesStrenghtValues.Length >= 5)
                {
                    if (spadesStrenghtValues[0] + 4 == spadesStrenghtValues[4])
                    {
                        current = 8;
                        power = spadesStrenghtValues.Max() / Constants.Devider + current * 100;
                        this.SortedWinningHands(current, power);
                    }

                    if (spadesStrenghtValues[0] == 0 &&
                        spadesStrenghtValues[1] == 9 &&
                        spadesStrenghtValues[2] == 10 &&
                        spadesStrenghtValues[3] == 11 &&
                        spadesStrenghtValues[0] + 12 == spadesStrenghtValues[4])
                    {
                        current = 9;
                        power = spadesStrenghtValues.Max() / Constants.Devider + current * 100;
                        this.SortedWinningHands(current, power);
                    }
                }
            }
        }

        private void RulesFourOfAKind(ref double current, ref double power, int[] straight)
        {
            this.HasSucsuccessfullyExecutedRulesFourOfAKind = true;
            if (current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (straight[j] / Constants.Devider == straight[j + 1] / Constants.Devider &&
                        straight[j] / Constants.Devider == straight[j + 2] / Constants.Devider &&
                        straight[j] / Constants.Devider == straight[j + 3] / Constants.Devider)
                    {
                        current = 7;
                        power = (straight[j] / Constants.Devider) * 4 + current * 100;
                        this.SortedWinningHands(current, power);
                    }

                    if (straight[j] / Constants.Devider == 0 &&
                        straight[j + 1] / Constants.Devider == 0 &&
                        straight[j + 2] / Constants.Devider == 0 &&
                        straight[j + 3] / Constants.Devider == 0)
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
            this.HasSucsuccessfullyExecutedRulesFullHouse = true;
            if (current >= -1)
            {
                this.currentForm.type = power;
                for (int j = 0; j <= Constants.CardTypes; j++)
                {
                    var fullHouse = straight.Where(o => o / Constants.Devider == j).ToArray();
                    if (fullHouse.Length == 3 || done)
                    {
                        if (fullHouse.Length == 2)
                        {
                            if (fullHouse.Max() / Constants.Devider == 0)
                            {
                                current = 6;
                                power = 13 * 2 + current * 100;
                                this.SortedWinningHands(current, power);
                                break;
                            }

                            if (fullHouse.Max() / Constants.Devider > 0)
                            {
                                current = 6;
                                power = fullHouse.Max() / Constants.Devider * 2 + current * 100;
                                this.SortedWinningHands(current, power);
                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fullHouse.Max() / Constants.Devider == 0)
                            {
                                power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                power = fullHouse.Max() / Constants.Devider;
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
            this.HasSucsuccessfullyExecutedRulesFlush = true;
            if (current >= -1)
            {
                var clubs = straight1.Where(o => o % Constants.Devider == 0).ToArray();
                var diamonds = straight1.Where(o => o % Constants.Devider == 1).ToArray();
                var hearts = straight1.Where(o => o % Constants.Devider == 2).ToArray();
                var spades = straight1.Where(o => o % Constants.Devider == 3).ToArray();

                if (clubs.Length == 3 || clubs.Length == 4)
                {
                    if (
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider ==
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == 
                        clubs[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > clubs.Max() / Constants.Devider)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }

                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > clubs.Max() / Constants.Devider)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                        else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider < clubs.Max() / Constants.Devider &&
                            this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider < clubs.Max() / Constants.Devider)
                        {
                            current = 5;
                            power = clubs.Max() + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                    }
                }

                if (clubs.Length == 4) //Different cardt in hand
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider !=
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider ==
                        clubs[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > clubs.Max() / Constants.Devider)
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

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider != 
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == 
                        clubs[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > clubs.Max() / Constants.Devider)
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
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == clubs[0] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > clubs.Min() / Constants.Devider)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == clubs[0] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > clubs.Min() / Constants.Devider)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }
                    else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider < clubs.Min() / Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider < clubs.Min())
                    {
                        current = 5;
                        power = clubs.Max() + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }
                }

                if (diamonds.Length == 3 || diamonds.Length == 4)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == 
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider ==
                        diamonds[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > diamonds.Max() / Constants.Devider)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }

                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > diamonds.Max() / Constants.Devider)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                        else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider < diamonds.Max() / Constants.Devider &&
                            this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider < diamonds.Max() / Constants.Devider)
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
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider !=
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider ==
                        diamonds[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > diamonds.Max() / Constants.Devider)
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

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider !=
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == 
                        diamonds[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > diamonds.Max() / Constants.Devider)
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
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == diamonds[0] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > diamonds.Min() / Constants.Devider)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == diamonds[0] % Constants.Devider
                        && this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > diamonds.Min() / Constants.Devider)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }
                    else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider < diamonds.Min() / Constants.Devider && 
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider < diamonds.Min())
                    {
                        current = 5;
                        power = diamonds.Max() + current * 100;
                        this.SortedWinningHands(current, power);

                        hasFlush = true;
                    }
                }

                if (hearts.Length == 3 || hearts.Length == 4)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == 
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == 
                        hearts[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > hearts.Max() / Constants.Devider)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }

                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > hearts.Max() / Constants.Devider)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);

                            hasFlush = true;
                        }
                        else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider < hearts.Max() / Constants.Devider &&
                            this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider < hearts.Max() / Constants.Devider)
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
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider !=
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == 
                        hearts[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > hearts.Max() / Constants.Devider)
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

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider != 
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == 
                        hearts[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > hearts.Max() / Constants.Devider)
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
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == hearts[0] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > hearts.Min() / Constants.Devider)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == hearts[0] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > hearts.Min() / Constants.Devider)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }
                    else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider < hearts.Min() / Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider < hearts.Min())
                    {
                        current = 5;
                        power = hearts.Max() + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }
                }

                if (spades.Length == 3 || spades.Length == 4)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == 
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider ==
                        spades[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > spades.Max() / Constants.Devider)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }

                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > spades.Max() / Constants.Devider)
                        {
                            current = 5;
                            power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                            this.SortedWinningHands(current, power);
                            hasFlush = true;
                        }
                        else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider < spades.Max() / Constants.Devider &&
                            this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider < spades.Max() / Constants.Devider)
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
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider != 
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider ==
                        spades[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > spades.Max() / Constants.Devider)
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

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider != 
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == 
                        spades[0] % Constants.Devider)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > spades.Max() / Constants.Devider)
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
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == spades[0] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > spades.Min() / Constants.Devider)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == spades[0] % Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider > spades.Min() / Constants.Devider)
                    {
                        current = 5;
                        power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }
                    else if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider < spades.Min() / Constants.Devider &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider < spades.Min())
                    {
                        current = 5;
                        power = spades.Max() + current * 100;
                        this.SortedWinningHands(current, power);
                        hasFlush = true;
                    }
                }

                if (clubs.Length > 0)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 0 &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == clubs[0] % Constants.Devider &&
                        hasFlush &&
                        clubs.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider == 0 &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == clubs[0] % Constants.Devider &&
                        hasFlush &&
                        clubs.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);
                    }
                }

                if (diamonds.Length > 0)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 0 &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == diamonds[0] % Constants.Devider &&
                        hasFlush &&
                        diamonds.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider == 0 &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == diamonds[0] % Constants.Devider &&
                        hasFlush &&
                        diamonds.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);
                    }
                }

                if (hearts.Length > 0)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 0 &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == hearts[0] % Constants.Devider &&
                        hasFlush &&
                        hearts.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider == 0 &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == hearts[0] % Constants.Devider &&
                        hasFlush &&
                        hearts.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);
                    }
                }

                if (spades.Length > 0)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 0 &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i] % Constants.Devider == spades[0] % Constants.Devider &&
                        hasFlush &&
                        spades.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        this.SortedWinningHands(current, power);
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider == 0 &&
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] % Constants.Devider == spades[0] % Constants.Devider &&
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
            this.HasSucsuccessfullyExecutedRulesStraight = true;
            if (current >= -1)
            {
                var op = straight.Select(o => o / Constants.Devider)
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
            this.HasSucsuccessfullyExecutedRulesThreeOfAKind = true;
            if (current >= -1)
            {
                for (int j = 0; j <= Constants.CardTypes; j++)
                {
                    var threeOfAkind = straight.Where(o => o / Constants.Devider == j)
                        .ToArray();

                    if (threeOfAkind.Length == 3)
                    {
                        if (threeOfAkind.Max() / Constants.Devider == 0)
                        {
                            current = 3;
                            power = 13 * 3 + current * 100;
                            this.SortedWinningHands(current, power);
                        }
                        else
                        {
                            current = 3;
                            power = threeOfAkind[0] / Constants.Devider + threeOfAkind[1] / Constants.Devider + threeOfAkind[2] / Constants.Devider + current * 100;
                            this.SortedWinningHands(current, power);
                        }
                    }
                }
            }
        }

        private void RulesTwoPair(ref double current, ref double power)
        {
            this.HasSucsuccessfullyExecutedRulesTwoPair = true;
            if (current >= -1)
            {
                bool msgbox = false;

                for (int totalCards = 16; totalCards >= Constants.CardTypes; totalCards--)
                {
                    int max = totalCards - 12;

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider != 
                        this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (totalCards - k < Constants.CardTypes)
                            {
                                max--;
                            }

                            if (totalCards - k >= Constants.CardTypes)
                            {
                                if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider ==
                                    this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider &&
                                    this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider == 
                                    this.currentForm.cardsAsNumbers[totalCards - k] / Constants.Devider ||
                                    this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider == 
                                    this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider &&
                                    this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 
                                    this.currentForm.cardsAsNumbers[totalCards - k] / Constants.Devider)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 0)
                                        {
                                            current = 2;
                                            power = 13 * 4 + (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider) * 2 + current * 100;
                                            this.SortedWinningHands(current, power);
                                        }

                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider == 0)
                                        {
                                            current = 2;
                                            power = 13 * 4 + (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider) * 2 + current * 100;
                                            this.SortedWinningHands(current, power);
                                        }

                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider != 0 &&
                                            this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider != 0)
                                        {
                                            current = 2;
                                            power = (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider) * 2 +
                                                (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4) * 2 + current * 100;
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
            this.HasSucsuccessfullyExecutedRulesPairTwoPair = true;
            if (current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;

                for (int totalCards = 16; totalCards >= Constants.CardTypes; totalCards--)
                {
                    int max = totalCards - Constants.CardTypes;
                    for (int k = 1; k <= max; k++)
                    {
                        if (totalCards - k < Constants.CardTypes)
                        {
                            max--;
                        }

                        if (totalCards - k >= Constants.CardTypes)
                        {
                            if (this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider ==
                                this.currentForm.cardsAsNumbers[totalCards - k] / Constants.Devider)
                            {
                                if (this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider != 
                                    this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider &&
                                    this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider != 
                                    this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider &&
                                    current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider == 0)
                                        {
                                            current = 2;
                                            power = (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider) * 2 + 13 * 4 + current * 100;
                                            this.SortedWinningHands(current, power);
                                        }

                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 0)
                                        {
                                            current = 2;
                                            power = (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider) * 2 + 13 * 4 + current * 100;
                                            this.SortedWinningHands(current, power);
                                        }

                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider != 0)
                                        {
                                            current = 2;
                                            power = (this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider) * 2 + 
                                                (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, power);
                                        }

                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider != 0)
                                        {
                                            current = 2;
                                            power = (this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider) * 2 + 
                                                (this.currentForm.cardsAsNumbers[this.currentForm.i] / 4) * 2 + current * 100;
                                            this.SortedWinningHands(current, power);
                                        }
                                    }

                                    msgbox = true;
                                }

                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > 
                                            this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider)
                                        {
                                            if (this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider == 0)
                                            {
                                                current = 0;
                                                power = 13 + this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider + current * 100;
                                                this.SortedWinningHands(current, power);
                                            }
                                            else
                                            {
                                                current = 0;
                                                power = this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider + 
                                                    this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider + current * 100;
                                                this.SortedWinningHands(current, power);
                                            }
                                        }
                                        else
                                        {
                                            if (this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider == 0)
                                            {
                                                current = 0;
                                                power = 13 + this.currentForm.cardsAsNumbers[this.currentForm.i + 1] + current * 100;
                                                this.SortedWinningHands(current, power);
                                            }
                                            else
                                            {
                                                current = 0;
                                                power = this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider + 
                                                    this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider + current * 100;
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
            this.HasSucsuccessfullyExecutedRulesPairFromHand = true;
            if (current >= -1)
            {
                bool msgbox = false;
                if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 
                    this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider)
                {
                    if (!msgbox)
                    {
                        if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 0)
                        {
                            current = 1;
                            power = 13 * 4 + current * 100;
                            this.SortedWinningHands(current, power);
                        }
                        else
                        {
                            current = 1;
                            power = (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider) * 4 + current * 100;
                            this.SortedWinningHands(current, power);
                        }
                    }

                    msgbox = true;
                }

                for (int totalCards = 16; totalCards >= Constants.CardTypes; totalCards--)
                {
                    if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider ==
                        this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider)
                    {
                        if (!msgbox)
                        {
                            if (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider == 0)
                            {
                                current = 1;
                                power = 13 * 4 + this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider + current * 100;
                                this.SortedWinningHands(current, power);
                            }
                            else
                            {
                                current = 1;
                                power = (this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider) * 4 + 
                                    this.currentForm.cardsAsNumbers[this.currentForm.i] / 4 + current * 100;
                                this.SortedWinningHands(current, power);
                            }
                        }

                        msgbox = true;
                    }

                    if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 
                        this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider)
                    {
                        if (!msgbox)
                        {
                            if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 0)
                            {
                                current = 1;
                                power = 13 * 4 + this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider + current * 100;
                                this.SortedWinningHands(current, power);
                            }
                            else
                            {
                                current = 1;
                                power = (this.currentForm.cardsAsNumbers[totalCards] / Constants.Devider) * 4 + 
                                    this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider + current * 100;
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
            this.HasSucsuccessfullyExecutedRulesHighCard = true;
            if (current == -1)
            {
                if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider > 
                    this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider)
                {
                    current = -1;
                    power = this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider;
                    this.SortedWinningHands(current, power);
                }
                else
                {
                    current = -1;
                    power = this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider;
                    this.SortedWinningHands(current, power);
                }

                if (this.currentForm.cardsAsNumbers[this.currentForm.i] / Constants.Devider == 0 ||
                    this.currentForm.cardsAsNumbers[this.currentForm.i + 1] / Constants.Devider == 0)
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