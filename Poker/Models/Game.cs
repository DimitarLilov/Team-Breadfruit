namespace Poker.Models
{
    using Enum;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

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

            this.dealerManager = new DealerManager(this);
        }

        public IList<IPlayerOOP> GetActivePlayers()
        {
            var activePlayers = players.Where(p => p.State == PlayerState.Active).ToList();
            return activePlayers;
        }
    }
}
