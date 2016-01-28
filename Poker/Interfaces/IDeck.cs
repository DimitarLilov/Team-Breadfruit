namespace Poker.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Implements a class that holds a collection of cards.
    /// </summary>
    public interface IDeck
    {
        IEnumerable<ICard> Cards { get; }
    } 
}
