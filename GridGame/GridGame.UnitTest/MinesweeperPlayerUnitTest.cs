using Xunit;
using GridGame.Service.Impl.Minesweeper;

namespace GridGame.UnitTest
{
    public class MinesweeperPlayerTests
    {
        private readonly MinesweeperPlayer _player;

        public MinesweeperPlayerTests()
        {
            _player = new MinesweeperPlayer();
        }

        [Fact]
        public void Constructor_ShouldSetInitialValues_WhenNoArgumentsProvided()
        {
            Assert.Equal(0, _player.Row);
            Assert.Equal(0, _player.Col);
            Assert.Equal(3, _player.Lives);
        }

        [Fact]
        public void Constructor_ShouldSetInitialValues_WhenArgumentsProvided()
        {
            var player = new MinesweeperPlayer(startRow: 5, startCol: 5, lives: 5);

            Assert.Equal(5, player.Row);
            Assert.Equal(5, player.Col);
            Assert.Equal(5, player.Lives);
        }

        [Fact]
        public void Move_ShouldUpdatePosition_WhenValidOffsetsAreProvided()
        {
            _player.Row = 0;
            _player.Col = 0;

            _player.Move(2, 3);

            Assert.Equal(2, _player.Row);
            Assert.Equal(3, _player.Col);
        }

        [Fact]
        public void LoseLife_ShouldDecreaseLivesByOne_WhenCalled()
        {
            int initialLives = _player.Lives;

            _player.LoseLife();

            Assert.Equal(initialLives - 1, _player.Lives);
        }
       
    }
}
