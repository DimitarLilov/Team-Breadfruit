namespace Poker.Interfaces
{
    using Poker.Enum;
    using Poker.Models.Players;

    using System.Collections.Generic;

    public interface IPlayerOOP
    {
        string Name { get; }

        int Budget { get; set; }

        Hand Hand { get; set; }

        BidType Strategy { get; set; }

        PlayerState State { get; set; }

        PlayerPosition Position { get; set; }

        void AddCards(IEnumerable<SimpleCard> cards);

        void ChangeStrategy(); //check fold raise
    }
}
