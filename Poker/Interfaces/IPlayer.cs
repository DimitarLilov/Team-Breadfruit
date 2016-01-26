namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public interface IPlayer : IActions
    {
        string Name { get; }

        IEnumerable<ICard> Cards { get; }

        IPosition cardsPosition { get; }

        int Chips { get; }

        Label playerStatus { get; }
    }
}
