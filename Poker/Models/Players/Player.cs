namespace Poker.Models.Players
{
    using System;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    using Poker.Interfaces;

    public abstract class Player : IPlayer
    {
        public bool HasCalled { get; set; }

        public bool HasChecked { get; set; }

        public bool HasFolded { get; set; }

        public bool HasRaised { get; set; }

        public void Call()
        {
            throw new NotImplementedException();
        }

        public void Check()
        {
            throw new NotImplementedException();
        }

        public void Fold()
        {
            throw new NotImplementedException();
        }

        public void Raise()
        {
            throw new NotImplementedException();
        }

        public string Name { get; set; }

        public IEnumerable<ICard> Cards { get; set; }

        public IPosition cardsPosition { get; set; }

        public int Chips { get; set; }

        public Label playerStatus { get; set; }
    }
}
