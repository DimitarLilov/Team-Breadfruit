namespace Poker.Interfaces
{
    /// <summary>
    /// Implements functions that are needed in order 
    /// for all player to be able to "Raise" ingame. 
    /// </summary>
    public interface IRaise
    {
        /// <summary>
        /// Checks if the current player has decided to raise the amount of chips
        /// that are needed to be set in the pot.
        /// </summary>
        bool HasRaised { get; }

        /// <summary>
        /// Sets a bigger amount of chips needed to be given that are already in the pot.
        /// </summary>
        void Raise();
    }
}
