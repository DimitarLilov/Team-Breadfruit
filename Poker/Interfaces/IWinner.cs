namespace Poker.Interfaces
{
    /// <summary>
    /// Implements all needed functions for finding out the winner of the current turn
    /// and adding chips in his stash.
    /// </summary>
    public interface IWinner : IWinnerRules, IFixWinners
    {
    }
}
