namespace Poker.Interfaces
{
    public interface ICheck
    {
        bool HasChecked { get; }

        void Check();
    }
}
