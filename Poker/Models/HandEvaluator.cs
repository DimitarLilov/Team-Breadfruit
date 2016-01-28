namespace Poker.Models
{
    //TODO: make it work for 7 cards
    //TODO: optimize if possible
    using Enum;
    using Players;

    using System.Collections.Generic;


    public class HandEvaluator
    {
        private int heartsSum;
        private int diamondSum;
        private int clubSum;
        private int spadesSum;
        private Hand cards;
        private HandValue handValue;

        public HandEvaluator(IList<SimpleCard> sortedHand)
        {
            this.heartsSum = 0;
            this.diamondSum = 0;
            this.clubSum = 0;
            this.spadesSum = 0;
            this.cards = new Hand(7);
            this.Cards = sortedHand;
            this.handValue = new HandValue();
        }

        public HandValue HandValue
        {
            get
            {
                return this.handValue;
            }

            private set
            {
                this.handValue = value;
            }
        }

        public IList<SimpleCard> Cards
        {
            get
            {
                return this.cards;
            }
            private set
            {
                this.cards.Add(value[0]);
                this.cards.Add(value[1]);
                this.cards.Add(value[2]);
                this.cards.Add(value[3]);
                this.cards.Add(value[4]);
                this.cards.Add(value[5]);
                this.cards.Add(value[6]);
            }
           }

            public HandStrength EvaluateHand()
            {
                GetNumberOfSuits();

                if (CheckFourOfKind())
                {
                    return HandStrength.FourKind;
                }
                else if (CheckFullHouse())
                {
                    return HandStrength.FullHouse;
                }
                else if (CheckFlush())
                {
                    return HandStrength.Flush;
                }
                else if (CheckStraight())
                {
                    return HandStrength.Straight;
                }
                else if (CheckTreeOfKind())
                {
                    return HandStrength.ThreeKind;
                }
                else if (CheckTwoPairs())
                {
                    return HandStrength.TwoPairs;
                }
                else if (CheckOnePair())
                {
                    return HandStrength.OnePair;
                }

                //if the hand is nothing, than the player with highest card wins
                this.handValue.HighCard = (int)cards[6].Type;
                return HandStrength.Nothing;
            }
        //get the number of each suit on hand
        private void GetNumberOfSuits()
        {
            foreach (var element in Cards)
            {
                if (element.Suit == Suit.Hearts)
                    heartsSum++;
                else if (element.Suit == Suit.Diamonds)
                    diamondSum++;
                else if (element.Suit == Suit.Clubs)
                    clubSum++;
                else if (element.Suit == Suit.Spades)
                    spadesSum++;
            }
        }
        
        private bool CheckFourOfKind()
        {
            //if the first 4 cards, add values of the four cards and last card is the highest
            if (cards[0].Type == cards[1].Type && cards[0].Type == cards[2].Type && cards[0].Type == cards[3].Type)
            {
                handValue.Total = (int)cards[1].Type * 4;
                handValue.HighCard = (int)cards[4].Type;

                return true;
            }
            else if (cards[1].Type == cards[2].Type && cards[1].Type == cards[3].Type && cards[1].Type == cards[4].Type)
            {
                handValue.Total = (int)cards[1].Type * 4;
                handValue.HighCard = (int)cards[0].Type;

                return true;
            }
            else if(cards[2].Type == cards[3].Type && cards[2].Type == cards[4].Type && cards[2].Type == cards[5].Type)
            {
                handValue.Total = (int)cards[2].Type * 4;
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[3].Type == cards[4].Type && cards[3].Type == cards[5].Type && cards[3].Type == cards[6].Type)
            {
                handValue.Total = (int)cards[2].Type * 4;
                handValue.HighCard = (int)cards[2].Type;
                return true;
            }

            return false;
        }

        private bool CheckFullHouse()
        {
            //  fullhouse combinations:
            //  012/34, 012/45, 012/56, 123/45, 123/45, 123/56, 234/56
            //  01/234, 01/345, 01/456, 12/345, 12/456, 23/456
            if ((cards[0].Type == cards[1].Type && cards[0].Type == cards[2].Type && cards[3].Type == cards[4].Type) ||
                (cards[0].Type == cards[1].Type && cards[2].Type == cards[3].Type && cards[2].Type == cards[4].Type))
            {
                handValue.Total = (int)(cards[0].Type) + (int)(cards[1].Type) + (int)(cards[2].Type) +
                    (int)(cards[3].Type) + (int)(cards[4].Type);

                return true;
            }

            return false;
        }

        private bool CheckFlush()
        {
            //if all suits are the same
            if (heartsSum >= 5 || diamondSum >= 5 || clubSum >= 5 || spadesSum >= 5)
            {
                //if flush, the player with higher cards win
                //whoever has the last card the highest, has automatically all the cards total higher
                handValue.Total = CheckHighestFlushCard();

                return true;
            }

            return false;
        }

        private int CheckHighestFlushCard()
        {
            var flushSuit = Suit.Spades;
            if(heartsSum >= 5)
            {
                flushSuit = Suit.Hearts;
            }
            else if(diamondSum >= 5)
            {
                flushSuit = Suit.Diamonds;
            }
            else if(clubSum >= 5)
            {
                flushSuit = Suit.Clubs;
            }


            int flushTotalValue = 0;
            for (int i = cards.Count-1; i >= 0; i--)
            {
                if(cards[i].Suit == flushSuit)
                {
                    flushTotalValue = (int)cards[i].Type;
                    break;
                }
            }

            return flushTotalValue;
        }

        private bool CheckStraight()
        {
            //if 5 consecutive values
            if (cards[0].Type + 1 == cards[1].Type &&
                cards[1].Type + 1 == cards[2].Type &&
                cards[2].Type + 1 == cards[3].Type &&
                cards[3].Type + 1 == cards[4].Type)
            {
                //player with the highest value of the last card wins
                handValue.Total = (int)cards[4].Type;

                return true;
            }

            return false;
        }

        private bool CheckTreeOfKind()
        {
            //if the 1,2,3 cards are the same OR
            //2,3,4 cards are the same OR
            //3,4,5 cards are the same OR
            //4,5,6 cards are same OR
            //5,6,7 cards are same
            if ((cards[0].Type == cards[1].Type && cards[0].Type == cards[2].Type))
            {
                handValue.Total = (int)cards[2].Type * 3;
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if((cards[1].Type == cards[2].Type && cards[1].Type == cards[3].Type))
            {
                handValue.Total = (int)cards[2].Type * 3;
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[2].Type == cards[3].Type && cards[2].Type == cards[4].Type)
            {
                handValue.Total = (int)cards[2].Type * 3;
                handValue.HighCard = (int)cards[1].Type;

                return true;
            }
            else if (cards[3].Type == cards[4].Type && cards[3].Type == cards[5].Type)
            {
                handValue.Total = (int)cards[3].Type * 3;
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[4].Type == cards[5].Type && cards[4].Type == cards[6].Type)
            {
                handValue.Total = (int)cards[4].Type * 3;
                handValue.HighCard = (int)cards[3].Type;
                return true;
            }


            return false;
        }

        private bool CheckTwoPairs()
        {
            //if 1,2 and 3,4
            //if 1.2 and 4,5
            //if 2.3 and 4,5
            //with two pairs, the 2nd card will always be a part of one pair 
            //and 4th card will always be a part of second pair
            if (cards[0].Type == cards[1].Type && cards[2].Type == cards[3].Type)
            {
                handValue.Total = ((int)cards[1].Type * 2) + ((int)cards[3].Type * 2);
                handValue.HighCard = (int)cards[6].Type;

                return true;
            }
            else if (cards[0].Type == cards[1].Type && cards[3].Type == cards[4].Type)
            {
                handValue.Total = ((int)cards[1].Type * 2) + ((int)cards[3].Type * 2);
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[0].Type == cards[1].Type && cards[4].Type == cards[5].Type)
            {
                handValue.Total = ((int)cards[1].Type * 2) + ((int)cards[4].Type * 2);
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[0].Type == cards[1].Type && cards[5].Type == cards[6].Type)
            {
                handValue.Total = ((int)cards[1].Type * 2) + ((int)cards[5].Type * 2);
                handValue.HighCard = (int)cards[4].Type;
                return true;
            }
            else if (cards[1].Type == cards[2].Type && cards[3].Type == cards[4].Type)
            {
                handValue.Total = ((int)cards[1].Type * 2) + ((int)cards[3].Type * 2);
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[1].Type == cards[2].Type && cards[4].Type == cards[5].Type)
            {
                handValue.Total = ((int)cards[1].Type * 2) + ((int)cards[4].Type * 2);
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[1].Type == cards[2].Type && cards[5].Type == cards[6].Type)
            {
                handValue.Total = ((int)cards[1].Type * 2) + ((int)cards[5].Type * 2);
                handValue.HighCard = (int)cards[4].Type;
                return true;
            }
            else if (cards[2].Type == cards[3].Type && cards[4].Type == cards[5].Type)
            {
                handValue.Total = ((int)cards[2].Type * 2) + ((int)cards[4].Type * 2);
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[2].Type == cards[3].Type && cards[5].Type == cards[6].Type)
            {
                handValue.Total = ((int)cards[2].Type * 2) + ((int)cards[5].Type * 2);
                handValue.HighCard = (int)cards[4].Type;
                return true;
            }
            else if (cards[3].Type == cards[4].Type && cards[5].Type == cards[6].Type)
            {
                handValue.Total = ((int)cards[3].Type * 2) + ((int)cards[5].Type * 2);
                handValue.HighCard = (int)cards[2].Type;
                return true;
            }

            return false;
        }

        private bool CheckOnePair()
        {
            //if 1,2 -> 5th card has the highest value
            //2.3
            //3,4
            //4,5 -> card #3 has the highest value
            if (cards[0].Type == cards[1].Type)
            {
                handValue.Total = (int)cards[0].Type * 2;
                handValue.HighCard = (int)cards[6].Type;

                return true;
            }
            else if (cards[1].Type == cards[2].Type)
            {
                handValue.Total = (int)cards[1].Type * 2;
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[2].Type == cards[3].Type)
            {
                handValue.Total = (int)cards[2].Type * 2;
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[3].Type == cards[4].Type)
            {
                handValue.Total = (int)cards[3].Type * 2;
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[4].Type == cards[5].Type)
            {
                handValue.Total = (int)cards[4].Type * 2;
                handValue.HighCard = (int)cards[6].Type;
                return true;
            }
            else if (cards[5].Type == cards[6].Type)
            {
                handValue.Total = (int)cards[5].Type * 2;
                handValue.HighCard = (int)cards[4].Type;

                return true;
            }

            return false;
        }
    }
}
