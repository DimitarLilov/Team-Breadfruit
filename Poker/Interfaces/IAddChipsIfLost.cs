namespace Poker.Interfaces
{
    /// <summary>
    /// Implements the method needed 
    /// when the player has lost all his chips and wants to continue the game by adding more.
    /// The amount of chips recieved by the player are also added to all the bots individualy
    /// which are currenty ingame.
    /// </summary>
    public interface IAddChipsIfLost
    {
        void AddChipsIfLost();
    }
}
