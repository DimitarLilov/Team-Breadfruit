using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    /// <summary>
    /// Implements all the actions needed by every player.
    /// </summary>
    public interface IActions : ICall, ICheck , IFold, IRaise
    {
    }
}
