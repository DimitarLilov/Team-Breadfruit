namespace Poker.Models
{
    using Extentions;
    using Players;
    using System;
    using System.Collections.Generic;

    public class DealerManager
    {
        private readonly Game game;

        private readonly Queue<SimpleCard> cardDeck;

        private readonly Hand[] playerCards;

        public DealerManager(Game game)
        {
            this.game = game;
            this.cardDeck = new Queue<SimpleCard>(CardsCollection.FullDeckOfCards);
        }

        public void PlayDeal()
        {
            //shuffle deck
            cardDeck.Shuffle();

            //deal to each player 2 cards
            DealCardsVisibleForPlayer();

            //TODO: checking/reising/folding
            ChooseStragies();

            //burn 1 card
            //deal 3 cards visible to everyone (flop)
            cardDeck.Dequeue();
            DealCardsVisibleForEveryone(3);

            //TODO: checking/reising/folding
            ChooseStragies();

            //burn 1 card
            //deal 1 card visible to everyone (turn)
            cardDeck.Dequeue();
            DealCardsVisibleForEveryone(1);

            //TODO: checking/reising/folding
            ChooseStragies();

            //burn 1 card
            //deal 1 card visible to everyone (river)
            cardDeck.Dequeue();
            DealCardsVisibleForEveryone(1);

            //TODO: showdown
            ShowdownCards();
        }

        private void DealCardsVisibleForPlayer()
        {
            int cardsCount = 2;
            throw new NotImplementedException();   
        }
        
        private void DealCardsVisibleForEveryone(int cardsCount)
        {
            throw new NotImplementedException();
        }

        private void ChooseStragies()
        {
            throw new NotImplementedException();
        }

        private void ShowdownCards()
        {
            throw new NotImplementedException();
        }
    }
}
