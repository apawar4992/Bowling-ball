namespace Game.Repository
{
    public interface IGameRepository
    {
        GameResult CalculateScore(Game game);
    }
}
