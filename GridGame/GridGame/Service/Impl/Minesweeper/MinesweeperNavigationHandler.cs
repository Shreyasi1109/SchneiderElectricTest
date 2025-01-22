using GridGame.Service.Interface;

namespace GridGame.Service.Impl.Minesweeper
{
    public class MinesweeperNavigationHandler : INavigationHandler
    {
        public (int rowOffset, int colOffset) GetMoveOffset(ConsoleKey key)
        {
            return key switch
            {
                ConsoleKey.UpArrow => (-1, 0),
                ConsoleKey.DownArrow => (1, 0),
                ConsoleKey.LeftArrow => (0, -1),
                ConsoleKey.RightArrow => (0, 1),
                _ => (0, 0)
            };
        }
    }
}
