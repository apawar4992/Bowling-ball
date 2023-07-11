using System;
using Game.Repository;
using Microsoft.Extensions.DependencyInjection;

public delegate Game.Repository.IGameRepository serviceResolver(string gameType);

namespace Game.Manager
{
    public class GameManager
    {
        private readonly IGameFactory _gameFactory = null;
        private readonly IServiceProvider _serviceProvider;
        public GameManager(IServiceProvider serviceProvider)
        {
            _gameFactory = serviceProvider.GetService<IGameFactory>();
            _serviceProvider = serviceProvider;
        }

        public GameResult GetResult(Game game)
        {
            try
            {
                IGameRepository gameRepository = _gameFactory.GetGameInstance(game.GameType, _serviceProvider);
                Repository.Game repoGame = game.ToGame();
                if (repoGame == null)
                    return null;

                return gameRepository.CalculateScore(repoGame)?.ToGameResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
