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
        [SerializeField] private bool _isPcInput = true;

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

            if (_isPcInput)
                builder.Register<IInputService, PcInputService>(Lifetime.Singleton);
            else
                builder.Register<IInputService, MobileInputService>(Lifetime.Singleton);
        }

        private void BuildGrid(IContainerBuilder builder)
        {
            builder.RegisterComponent(_gridController);
        }
    }
}