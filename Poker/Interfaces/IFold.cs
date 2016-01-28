namespace Poker.Interfaces
{
    /// <summary>
    /// Implements functions that are needed in order 
    /// for all player to be able to "Fold" ingame. 
    /// </summary>
    public interface IFold
    {
        /// <summary>
        /// Checks if the player has decided to quit the current turn.
        /// </summary>
        bool HasFolded { get; }

        /// <summary>
        /// The player folds and loses all the chips that he set in the pot.
        /// </summary>
        void Fold();
    }
}
