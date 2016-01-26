namespace Poker.Interfaces
{
    /// <summary>
    /// Implements a method that scans all the bots' and player's current statuses.
    /// </summary>
    public interface ICheckPlayerBotsStatus
    {
        string CheckPlayerBotsStatus(string fixedLast);
    }
}
