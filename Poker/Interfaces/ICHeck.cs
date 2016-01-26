using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    /// <summary>
    /// Implements functions that are needed in order 
    /// for all player to be able to "Check" ingame. 
    /// </summary>
    public interface ICheck
    {
        /// <summary>
        /// Checks if the current player has continued the game turn.
        /// </summary>
        bool HasChecked { get; }

        /// <summary>
        /// Sets the player status so that he can continue playing on this turn.
        /// </summary>
        void Check();
    }
}
