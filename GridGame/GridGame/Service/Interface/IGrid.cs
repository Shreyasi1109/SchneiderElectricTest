namespace GridGame.Service.Interface
{
    public interface IGrid
    {
        void RenderGrid(int playerRow, int playerCol, char playerSymbol);
        bool IsValidPosition(int row, int col);
        int TotalRows { get; } 
        int TotalCols { get; }
    }
}
