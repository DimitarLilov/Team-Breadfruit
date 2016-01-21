using System;
using System.Linq;

namespace Poker
{
    public class Rules
    {
        private Form1 currentForm;

        public Rules(Form1 currentForm)
        {
            this.currentForm = currentForm;
        }

        public void GameRules(int cardOne, int cardTwo, ref double current, ref double Power, bool foldedTurn)
        {
            if (!foldedTurn || cardOne == 0 && cardTwo == 1 && currentForm.playerStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false, hasFlush = false;
                int[] cardsOnTheTable = new int[5];
                int[] currentPlayerAndTableCards = new int[7];

                currentPlayerAndTableCards[0] = currentForm.cardsAsNumbers[cardOne];
                currentPlayerAndTableCards[1] = currentForm.cardsAsNumbers[cardTwo];
                currentPlayerAndTableCards[2] = currentForm.cardsAsNumbers[12];
                currentPlayerAndTableCards[3] = currentForm.cardsAsNumbers[13];
                currentPlayerAndTableCards[4] = currentForm.cardsAsNumbers[14];
                currentPlayerAndTableCards[5] = currentForm.cardsAsNumbers[15];
                currentPlayerAndTableCards[6] = currentForm.cardsAsNumbers[16];
               
                cardsOnTheTable[0] = currentForm.cardsAsNumbers[12];
                cardsOnTheTable[1] = currentForm.cardsAsNumbers[13];
                cardsOnTheTable[2] = currentForm.cardsAsNumbers[14];
                cardsOnTheTable[3] = currentForm.cardsAsNumbers[15];
                cardsOnTheTable[4] = currentForm.cardsAsNumbers[16];
               
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
                    if (currentForm.cardsAsNumbers[i] == int.Parse(currentForm.cardsImages[cardOne].Tag.ToString()) && currentForm.cardsAsNumbers[i + 1] == int.Parse(currentForm.cardsImages[cardTwo].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        currentForm.HandRules.RulesPairFromHand(ref current, ref Power);

                        #region Pair or Two Pair from Table current = 2 || 0
                        currentForm.HandRules.RulesPairTwoPair(ref current, ref Power);
                        #endregion

                        #region Two Pair current = 2
                        currentForm.HandRules.RulesTwoPair(ref current, ref Power);
                        #endregion

                        #region Three of a kind current = 3
                        currentForm.HandRules.RulesThreeOfAKind(ref current, ref Power, currentPlayerAndTableCards);
                        #endregion

                        #region CheckBotsStraight current = 4
                        currentForm.HandRules.RulesStraight(ref current, ref Power, currentPlayerAndTableCards);
                        #endregion

                        #region CheckBotsFlush current = 5 || 5.5
                        currentForm.HandRules.RulesFlush(ref current, ref Power, ref hasFlush, cardsOnTheTable);
                        #endregion

                        #region Full House current = 6
                        currentForm.HandRules.RulesFullHouse(ref current, ref Power, ref done, currentPlayerAndTableCards);
                        #endregion

                        #region Four of a Kind current = 7
                        currentForm.HandRules.RulesFourOfAKind(ref current, ref Power, currentPlayerAndTableCards);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        currentForm.HandRules.RulesStraightFlush(ref current, ref Power, clubsStrenghtValues, diamondsStrenghtValues, heartsStrenghtValues, spadesStrenghtValues);
                        #endregion

                        #region High Card current = -1
                        currentForm.HandRules.RulesHighCard(ref current, ref Power);
                        #endregion
                    }
                }
            }
        }
    }
}