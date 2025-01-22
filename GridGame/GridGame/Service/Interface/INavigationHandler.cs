namespace GridGame.Service.Interface
{
    public interface INavigationHandler
    {
        (int rowOffset, int colOffset) GetMoveOffset(ConsoleKey key);
    }
}
