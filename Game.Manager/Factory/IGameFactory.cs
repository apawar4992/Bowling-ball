using System;
using Game.Repository;

namespace Game.Manager
{
    public interface IGameFactory
    {
        IGameRepository GetGameInstance(string gameType, IServiceProvider serviceProvider);
    }
}
