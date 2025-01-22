using GridGame.Service.Interface;

namespace GridGame.Service.Impl.Minesweeper
{
    public class MinesweeperPlayer : IPlayer
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Lives { get; set; }

        public MinesweeperPlayer(int startRow = 0, int startCol = 0, int lives = 3)
        {
            Row = startRow;
            Col = startCol;
            Lives = lives;
        }

        public void Move(int rowOffset, int colOffset)
        {
            Row += rowOffset;
            Col += colOffset;
        }

        public void LoseLife()
        {
            Lives--;
        }
    }
}
