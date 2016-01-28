namespace Poker.Interfaces
{
    /// <summary>
    /// Implements all the actions needed by every player.
    /// </summary>
    public interface IActions : ICall, ICheck, IFold, IRaise
    {
    }
}
