using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyCode
{
    public class GameScope : LifetimeScope
    {
        [Space(0), Header("Game Scope Fields")]
        [SerializeField] private Factory _factory;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent<IFactory>(_factory);
        }
    }
}