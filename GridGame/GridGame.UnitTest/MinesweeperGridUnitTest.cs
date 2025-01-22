using Moq;
using Xunit;
using GridGame.Service.Impl.Minesweeper;
using System;
using GridGame.Constants;
using Microsoft.Extensions.Configuration;

namespace GridGame.UnitTest
{
    public class MinesweeperGridTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly MinesweeperGrid _grid;

        public MinesweeperGridTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(config => config[MinesweeperConstants.MinesweeperConfig]).Returns("5"); // Assume 5 mines from config

            _grid = new MinesweeperGrid(_mockConfiguration.Object);
        }

        [Fact]
        public void Constructor_ShouldInitializeGridWithGivenRowsAndCols()
        {
            Assert.Equal(8, _grid.TotalRows);
            Assert.Equal(8, _grid.TotalCols);
        }

        [Fact]
        public void PlaceMines_ShouldPlaceCorrectNumberOfMines()
        {
            var mineCount = CountMines();
            Assert.Equal(5, mineCount);
        }


        [Fact]
        public void HasMine_ShouldReturnFalse_WhenNoMineAtPosition()
        {
            Assert.False(_grid.HasMine(0, 0));
        }

        [Fact]
        public void IsValidPosition_ShouldReturnTrue_ForValidPosition()
        {
            Assert.True(_grid.IsValidPosition(3, 3));
        }

        [Fact]
        public void IsValidPosition_ShouldReturnFalse_ForInvalidPosition()
        {
            Assert.False(_grid.IsValidPosition(-1, 3));

            Assert.False(_grid.IsValidPosition(8, 8));
        }


        [Fact]
        public void RevealMines_ShouldDisplayMines()
        {
            _grid.TriggerMine(1, 1);
            _grid.TriggerMine(2, 2);

            var stringWriter = new System.IO.StringWriter();
            Console.SetOut(stringWriter);

            _grid.RevealMines();

            var output = stringWriter.ToString();
            Assert.Contains("*", output);
        }

        private int CountMines()
        {
            int mineCount = 0;
            for (int row = 0; row < _grid.TotalRows; row++)
            {
                for (int col = 0; col < _grid.TotalCols; col++)
                {
                    if (_grid.HasMine(row, col))
                    {
                        mineCount++;
                    }
                }
            }
            return mineCount;
        }
    }
}
