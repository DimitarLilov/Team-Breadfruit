namespace Poker.Interfaces
{
    /// <summary>
    /// Implements a method that checks if the amount of chips
    /// needed for a player to set in the pot has been increased.
    /// </summary>
    public interface ICheckIfSomeoneRaised
    {
        void CheckIfSomeoneRaised(int currentTurn);
    }
}
