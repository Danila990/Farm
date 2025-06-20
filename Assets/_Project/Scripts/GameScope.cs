using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyCode
{
    public class GameScope : LifetimeScope
    {
        [Space(5), Header("Scope")]
        [SerializeField] private GridController _gridController;
        [SerializeField] private PlayerController _playerController;

        protected override void Configure(IContainerBuilder builder)
        {
            BuildBase(builder);
            BuildGrid(builder);
            BuildPlayer(builder);
        }

        private void BuildBase(IContainerBuilder builder)
        {
            builder.Register<GameBootstrap>(Lifetime.Singleton).As<IInitializable>();
            builder.Register<InjectService>(Lifetime.Singleton);
        }

        private void BuildPlayer(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playerController);
        }

        private void BuildGrid(IContainerBuilder builder)
        {
            builder.RegisterComponent(_gridController);
            builder.Register<GridNavigation>(Lifetime.Singleton);
        }
    }
}