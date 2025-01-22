using Xunit;
using GridGame.Service.Impl.Minesweeper;
using System;

namespace GridGame.UnitTest
{
    public class MinesweeperNavigationHandlerTests
    {
        private readonly MinesweeperNavigationHandler _navigationHandler;

        public MinesweeperNavigationHandlerTests()
        {
            _navigationHandler = new MinesweeperNavigationHandler();
        }

        [Theory]
        [InlineData(ConsoleKey.UpArrow, -1, 0)]
        [InlineData(ConsoleKey.DownArrow, 1, 0)]
        [InlineData(ConsoleKey.LeftArrow, 0, -1)]
        [InlineData(ConsoleKey.RightArrow, 0, 1)]
        [InlineData(ConsoleKey.Escape, 0, 0)]
        public void GetMoveOffset_ShouldReturnCorrectOffset_WhenKeyIsPressed(ConsoleKey key, int expectedRowOffset, int expectedColOffset)
        {
            var result = _navigationHandler.GetMoveOffset(key);

            Assert.Equal(expectedRowOffset, result.rowOffset);
            Assert.Equal(expectedColOffset, result.colOffset);
        }
    }
}
