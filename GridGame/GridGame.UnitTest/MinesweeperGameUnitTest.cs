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

            _stringWriter = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_stringWriter);

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
            _mockPlayer.Setup(p => p.Row).Returns(0);
            _mockPlayer.Setup(p => p.Col).Returns(0);
            _mockNavigationHandler.Setup(n => n.GetMoveOffset(It.IsAny<ConsoleKey>())).Returns((0, 0));
            _mockGrid.Setup(g => g.IsValidPosition(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            _game.ProcessMove(ConsoleKey.UpArrow);

            _mockPlayer.Verify(p => p.Move(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void IsGameOver_ShouldReturnTrue_WhenPlayerHasNoLives()
        {
            _mockPlayer.Setup(p => p.Lives).Returns(0);

            var result = _game.IsGameOver();

            Assert.True(result); 
        }

        [Fact]
        public void IsGameOver_ShouldReturnTrue_WhenPlayerReachesEnd()
        {
            _mockPlayer.Setup(p => p.Lives).Returns(3);
            _mockPlayer.Setup(p => p.Row).Returns(7);
            _mockGrid.Setup(g => g.TotalRows).Returns(8);

            var result = _game.IsGameOver();

            Assert.True(result); 
        }

    }
}
