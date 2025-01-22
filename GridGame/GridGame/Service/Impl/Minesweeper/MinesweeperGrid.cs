using GridGame.Constants;
using GridGame.Service.Interface.Minesweeper;
using Microsoft.Extensions.Configuration;

namespace GridGame.Service.Impl.Minesweeper
{
    public class MinesweeperGrid : IMinesweeperGrid
    {
        private readonly int _totalRows;
        private readonly int _totalCols;
        private readonly bool[,] _mines;
        private readonly bool[,] _triggeredMines;
        private readonly IConfiguration _configuration;

        public int TotalRows => _totalRows;
        public int TotalCols => _totalCols;

        public MinesweeperGrid(IConfiguration configuration,int rows = 8, int cols = 8)
        {
            _totalRows = rows;
            _totalCols = cols;
            _mines = new bool[rows, cols];
            _triggeredMines = new bool[rows, cols];
            _configuration = configuration;
            ;
            PlaceMines(Int32.Parse(_configuration[MinesweeperConstants.MinesweeperConfig]));
        }

        public void TriggerMine(int row, int col)
        {
            if (HasMine(row, col))
            {
                _triggeredMines[row, col] = true;
            }
        }

        private void PlaceMines(int numberOfMines)
        {
            var random = new Random();
            int placedMines = 0;

            while (placedMines < numberOfMines)
            {
                int row = random.Next(_totalRows);
                int col = random.Next(_totalCols);

                if (!_mines[row, col])
                {
                    _mines[row, col] = true;
                    placedMines++;
                }
            }
        }

        public bool HasMine(int row, int col) => _mines[row, col];

        public bool IsValidPosition(int row, int col) => row >= 0 && row < _totalRows && col >= 0 && col < _totalCols;

        public void RenderGrid(int playerRow, int playerCol, char playerSymbol)
        {
            for (int row = 0; row < _totalRows; row++)
            {
                for (int col = 0; col < _totalCols; col++)
                {
                    if (row == playerRow && col == playerCol)
                    {
                        Console.Write($"{playerSymbol} ");
                    }
                    else if (_triggeredMines[row, col])
                    {
                        Console.Write("* "); 
                    }
                    else
                    {
                        Console.Write(". "); 
                    }
                }
                Console.WriteLine();
            }
        }

        public void RevealMines()
        {
            for (int row = 0; row < _totalRows; row++)
            {
                for (int col = 0; col < _totalCols; col++)
                {
                    if (HasMine(row, col))
                        Console.Write("* "); 
                    else
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
        }
    }
}
