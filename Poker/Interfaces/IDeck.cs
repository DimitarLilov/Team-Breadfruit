using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    /// <summary>
    /// Implements a class that holds a collection of cards.
    /// </summary>
    interface IDeck
    {
        IEnumerable<ICard> Cards { get; }
    } 
}
