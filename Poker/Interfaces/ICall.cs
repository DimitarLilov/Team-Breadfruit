namespace Poker.Interfaces
{
    public interface ICall
    {
        bool HasCalled { get; }

        void Call();
    }
}
