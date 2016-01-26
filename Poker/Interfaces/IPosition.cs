using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    /// <summary>
    /// Defines an object that has coordinates.
    /// </summary>
    public interface IPosition
    {
        int X { get; }
        int Y { get; }
    }
}
