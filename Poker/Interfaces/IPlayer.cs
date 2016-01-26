using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    /// <summary>
    /// Implements all the qualities and abillities needed by every player in the game.
    /// </summary>
    public interface IPlayer : IActions
    {
        /// <summary>
        /// Sets an ID for the player.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Holds a set of the two cards that the player has inhand.
        /// </summary>
        IEnumerable<ICard> Cards { get; }

        /// <summary>
        /// Sets the position of the cards.
        /// </summary>
        IPosition cardsPosition { get; }

        /// <summary>
        /// Sets the amount of chips in the player's stash.
        /// </summary>
        int Chips { get; }

        /// <summary>
        /// Sets the player's status.
        /// </summary>
        Label playerStatus { get; }
    }
}
