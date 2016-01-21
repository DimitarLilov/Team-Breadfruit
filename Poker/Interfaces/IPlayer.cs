using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    public interface IPlayer : IActions
    {
        string Name { get; }
        IEnumerable<ICard> Cards { get; }
        IPosition cardsPosition { get; }
        int Chips { get; }
        Label playerStatus { get; }
    }
}
