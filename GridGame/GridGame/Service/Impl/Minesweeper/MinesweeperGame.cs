using GridGame.Service.Interface;
using GridGame.Service.Interface.Minesweeper;
using System.Data;

namespace GridGame.Service.Impl.Minesweeper
{
    public class MinesweeperGame : IGame
    {
        private readonly IMinesweeperGrid _grid;
        private IPlayer _player;
        private readonly INavigationHandler _navigationHandler;
        private int _moveCount;
        private const char PlayerSymbol = 'S';

        public MinesweeperGame(IMinesweeperGrid grid, IPlayer player, INavigationHandler navigationHandler)
        {
            _grid = grid;
            _player = player;
            _navigationHandler = navigationHandler;
            _moveCount = 0;
        }

        public void StartGame()
        {
            Console.Clear(); 

            _grid.RenderGrid(_player.Row, _player.Col, PlayerSymbol);
            Console.SetCursorPosition(0, _grid.TotalRows + 1); 

            while (!IsGameOver())
            {
                ShowCurrentStatus();
                Console.WriteLine("Use arrow keys to navigate up, down, left or right. Press 'Esc' to exit.");
                var keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Game exited.");
                    return;
                }

                ProcessMove(keyInfo.Key);
            }

            if (_player.Lives > 0)
            {
                Console.WriteLine("Congratulations! You've reached the end.");
            }
            else
            {
                Console.WriteLine("Game over. You ran out of lives.");
            }

            _grid.RevealMines();
        }

        public void ProcessMove(ConsoleKey key)
        {
            if (IsGameOver()) return;

            int oldRow = _player.Row;
            int oldCol = _player.Col;

            var (rowOffset, colOffset) = _navigationHandler.GetMoveOffset(key);

            if (rowOffset == 0 && colOffset == 0)
            {
                Console.WriteLine("Invalid move. Use arrow keys to navigate.");
                return;
            }

            int newRow = oldRow + rowOffset;
            int newCol = oldCol + colOffset;

            if (!_grid.IsValidPosition(newRow, newCol))
            {
                Console.WriteLine("Move is out of bounds.");
                return;
            }

            _player.Move(rowOffset, colOffset);
            _moveCount++;

            if (_grid.HasMine(_player.Row, _player.Col))
            {
                _player.LoseLife();
                Console.WriteLine($"Boom! You hit a mine. Lives left: {_player.Lives}");

                Console.SetCursorPosition(newCol * 2, newRow);
                Console.Write("* ");
            }

            Console.SetCursorPosition(oldCol * 2, oldRow); 
            Console.Write(". "); 

            Console.SetCursorPosition(newCol * 2, newRow); 
            Console.Write(PlayerSymbol);

            Console.SetCursorPosition(0, _grid.TotalRows + 1);
        }



        public bool IsGameOver()
        {
            return _player.Lives <= 0 || _player.Row == _grid.TotalRows - 1; 
        }

        public void ShowCurrentStatus()
        {
            Console.SetCursorPosition(0, _grid.TotalRows + 1); 
            
            char columnLetter = (char)('A' + _player.Col);

            int rowNumber = _player.Row + 1;

            Console.WriteLine($"Position: {columnLetter}{rowNumber} | Moves: {_moveCount} | Lives: {_player.Lives} ");
        }
    }
}
