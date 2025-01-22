using System;
namespace GridGame.Service.Interface.Minesweeper
{
    public interface IMinesweeperGrid : IGrid
    {
        bool HasMine(int row, int col);
        void RevealMines();

        void TriggerMine(int row, int col);

    }
}
