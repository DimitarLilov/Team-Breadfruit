namespace Poker.Interfaces
{
    /// <summary>
    /// Implements all the functions and actions needed by the dealer.
    /// </summary>
    public interface IDealer : ICheckIfSomeoneRaised, ICheckFlopTurnOrRiver, ICheckPlayerBotsStatus, IAddChipsIfLost
    {

    }
}
