using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code
{
    public class GameScope : LifetimeScope
    {
        [Header("Installers")]
        [SerializeField] private GridController _gridController;
        [SerializeField] private PlayerController _playerController;

        protected override void Configure(IContainerBuilder builder)
        {
            BuildGrid(builder);
            BuildPlayer(builder);
            builder.Register<CoinScore>(Lifetime.Singleton).As<IDisposable, IStartable>();
            builder.RegisterComponentOnNewGameObject<GameManager>(Lifetime.Singleton).As<IInitializable>();
        }

        private void BuildGrid(IContainerBuilder builder)
        {
            builder.RegisterComponent<IGridService>(_gridController);
            builder.Register<GridEventHandler>(Lifetime.Singleton).As<IDisposable, IStartable>();
        }

        private void BuildPlayer(IContainerBuilder builder)
        {
            builder.RegisterComponent<IPlayerService>(_playerController);
            builder.Register<PlayerEventHandler>(Lifetime.Singleton).As<IDisposable, IStartable>();
        }
    }
}