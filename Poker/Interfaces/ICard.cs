namespace Poker.Interfaces
{
    using System.Drawing;
    using System.Windows.Forms;
    using Poker.Enum;

    interface ICard
    {
        Image Front { get; }

        
        PictureBox PictureBox { get; set; }

        
        int Power { get; set; }

        
        Suit Suit { get; set; }
    }
}
