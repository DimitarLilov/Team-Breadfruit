namespace Poker.Interfaces
{
    using System.Collections.Generic;

    public interface IDeck
    {
        IEnumerable<ICard> Cards { get; }
    } 
}
