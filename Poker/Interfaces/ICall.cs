using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    public interface ICall
    {
        bool HasCalled { get; }
        void Call();
    }
}
