namespace Poker.Interfaces
{
    public interface IRaise
    {
        bool HasRaised { get; }

        void Raise();
    }
}
