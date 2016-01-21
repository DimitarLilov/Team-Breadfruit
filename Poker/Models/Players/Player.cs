namespace Poker.Models.Players
{
    using System;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    using Poker.Interfaces;
    public abstract class Player : IPlayer
    {
        public bool HasCalled { get; }

        public void Call()
        {
            throw new NotImplementedException();
        }

        public bool HasChecked { get; }

        public void Check()
        {
            throw new NotImplementedException();
        }

        public bool HasFolded { get; }

        public void Fold()
        {
            throw new NotImplementedException();
        }

        public bool HasRaised { get; }

        public void Raise()
        {
            throw new NotImplementedException();
        }

        public string Name { get; }

        public IEnumerable<ICard> Cards { get; }

        public IPosition cardsPosition { get; }

        public int Chips { get; }

        public Label playerStatus { get; }
    }
}
