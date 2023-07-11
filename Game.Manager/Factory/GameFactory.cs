using System;
using Game.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Manager
{
    public class GameFactory : IGameFactory
    {
        public IGameRepository GetGameInstance(string gameType, IServiceProvider serviceProvider)
        {
            var serviceResolver = serviceProvider.GetService<serviceResolver>();
            return serviceResolver(gameType);
        }
    }
}
