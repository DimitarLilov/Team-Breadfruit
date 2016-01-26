namespace Poker.Interfaces
{
    public interface IFold
    {
        bool HasFolded { get; }

        void Fold();
    }
}
