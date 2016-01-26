namespace Poker.Models.Players
{
    using System.Drawing;
    using System.Windows.Forms;

    using Poker.Enum;
    using Poker.Interfaces;

    public class Card : ICard
    {
        public Card(Image front, PictureBox pictureBox, int power, Suit suit)
        {
            this.Front = front;
            this.PictureBox = pictureBox;
            this.Power = power;
            this.Suit = suit;
        }

        public Image Front { get; set; }

        public PictureBox PictureBox { get; set; }

        public int Power { get; set; }

        public Suit Suit { get; set; }
    }
}
