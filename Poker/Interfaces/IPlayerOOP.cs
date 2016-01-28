using Poker.Enum;
using Poker.Models.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    public interface IPlayerOOP
    {
        string Name { get; }
        int Budget { get; set; }
        Hand Hand { get; set; }
        BidType Strategy { get; set; }
        PlayerState State { get; set; }
        PlayerPosition Position { get; set; }

        void AddCard(SimpleCard card);
        void ChooseStrategy(); //check fold raise
        
    }
}
