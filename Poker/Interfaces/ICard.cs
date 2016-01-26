namespace Poker.Interfaces
{
    using System.Drawing;
    using System.Windows.Forms;
    using Poker.Enum;

    /// <summary>
    /// Implements images and values for cards.
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Sets the image that will be shown on front of the card.
        /// </summary>
        Image Front { get; }

        //TODO:Understand what this is. 
        /// <summary>
        /// 
        /// </summary>
        PictureBox PictureBox { get; set; }

        /// <summary>
        /// Sets the value of the card.
        /// </summary>
        int Power { get; set; }

        /// <summary>
        /// Sets the card to be one of the four chosen suits.
        /// </summary>
        Suit Suit { get; set; }
    }
}
