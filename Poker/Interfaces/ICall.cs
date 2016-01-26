using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    /// <summary>
    /// Implements functions that are needed in order 
    /// for all player to be able to "Call" ingame. 
    /// </summary>
    public interface ICall
    {
        /// <summary>
        /// Checks if the player has already given the amount of chips that have been raised.
        /// </summary>
        bool HasCalled { get; }

        /// <summary>
        /// Gives the current amount of chips in the pot needed to continue playing.
        /// </summary>
        void Call();
    }
}
