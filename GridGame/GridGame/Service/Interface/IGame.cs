namespace GridGame.Service.Interface
{
    public interface IGame
    {
        void StartGame();
        void ProcessMove(ConsoleKey key);
        bool IsGameOver();
        void ShowCurrentStatus();
    }
}
