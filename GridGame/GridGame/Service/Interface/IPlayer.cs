namespace GridGame.Service.Interface
{
    public interface IPlayer
    {
        int Row { get; set; }
        int Col { get; set; }
        int Lives { get; set; }
        void Move(int rowOffset, int colOffset);

        void LoseLife();
    }
}
