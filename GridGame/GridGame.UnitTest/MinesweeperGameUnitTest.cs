using Moq;
using Xunit;
using GridGame.Service.Interface;
using GridGame.Service.Interface.Minesweeper;
using GridGame.Service.Impl.Minesweeper;
using System;
using System.IO;

namespace GridGame.UnitTest
{
    public class MinesweeperGameUnitTest
    {
        private readonly Mock<IMinesweeperGrid> _mockGrid;
        private readonly Mock<IPlayer> _mockPlayer;
        private readonly Mock<INavigationHandler> _mockNavigationHandler;
        private readonly MinesweeperGame _game;
        private readonly StringWriter _stringWriter;
        private readonly TextWriter _originalOutput;

        public MinesweeperGameUnitTest()
        {
            _mockGrid = new Mock<IMinesweeperGrid>();
            _mockPlayer = new Mock<IPlayer>();
            _mockNavigationHandler = new Mock<INavigationHandler>();

            // Redirect Console output to a StringWriter
            _stringWriter = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_stringWriter);

            // Initialize the game with mocked dependencies
            _game = new MinesweeperGame(_mockGrid.Object, _mockPlayer.Object, _mockNavigationHandler.Object);
        }

        public void Dispose()
        {
            Console.SetOut(_originalOutput);
            _stringWriter.Dispose();
        }


        [Fact]
        public void ProcessMove_ShouldNotUpdatePosition_WhenInvalidMove()
        {
            // Arrange
            _mockPlayer.Setup(p => p.Row).Returns(0);
            _mockPlayer.Setup(p => p.Col).Returns(0);
            _mockNavigationHandler.Setup(n => n.GetMoveOffset(It.IsAny<ConsoleKey>())).Returns((0, 0)); // No movement
            _mockGrid.Setup(g => g.IsValidPosition(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            // Act
            _game.ProcessMove(ConsoleKey.UpArrow);

            // Assert
            _mockPlayer.Verify(p => p.Move(It.IsAny<int>(), It.IsAny<int>()), Times.Never); // Ensure Move is not called
        }

        [Fact]
        public void IsGameOver_ShouldReturnTrue_WhenPlayerHasNoLives()
        {
            // Arrange
            _mockPlayer.Setup(p => p.Lives).Returns(0);

            // Act
            var result = _game.IsGameOver();

            // Assert
            Assert.True(result); // Game should be over when player has no lives
        }

        [Fact]
        public void IsGameOver_ShouldReturnTrue_WhenPlayerReachesEnd()
        {
            // Arrange
            _mockPlayer.Setup(p => p.Lives).Returns(3);
            _mockPlayer.Setup(p => p.Row).Returns(7);
            _mockGrid.Setup(g => g.TotalRows).Returns(8);

            // Act
            var result = _game.IsGameOver();

            // Assert
            Assert.True(result); // Game should be over when player reaches the last row
        }

    }
}
