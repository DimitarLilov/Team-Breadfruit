namespace Poker.Models
{
    using Poker.Interfaces;
    using System.Collections.Generic;

    public class Game
    {
        private DealerManager dealerManager;
        private IList<IPlayerOOP> players;

        public Game(IPlayerOOP firstPlayer, IPlayerOOP secondPlayer, IPlayerOOP thirdPlayer, IPlayerOOP forthPlayer,
                    IPlayerOOP fifthPlayer, IPlayerOOP sixthPlayer)
        {
            this.players = new List<IPlayerOOP>
            {
                firstPlayer, secondPlayer,
                thirdPlayer, forthPlayer,
                fifthPlayer, sixthPlayer
            };


        }
    }
}
