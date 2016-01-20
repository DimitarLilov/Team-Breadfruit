namespace Poker.Models
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Enum;
    using Poker.Interfaces;
    class Card : ICard
    {
        public Image Front
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public PictureBox PictureBox
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Power
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Suit Suit
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
