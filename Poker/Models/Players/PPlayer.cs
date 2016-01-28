using Poker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.Enum;

namespace Poker.Models.Players
{
    class PPlayer : IPlayerOOP
    {

        public PPlayer()
        {
                
        }

        public int Budget { get; set; }

        public Hand Hand { get; set; }

        public string Name { get; set; }

        public PlayerPosition Position { get; set; }

        public PlayerState State { get; set; }

        public BidType Strategy { get; set; }

        public void AddCard(SimpleCard card)
        {
            throw new NotImplementedException();
        }

        public void ChooseStrategy()
        {
            throw new NotImplementedException();
        }
    }
}
