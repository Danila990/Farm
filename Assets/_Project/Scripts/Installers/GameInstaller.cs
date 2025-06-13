using System;
using UnityEngine;
using Zenject;

namespace MyCode
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GridController _gridController;

        public override void InstallBindings()
        {
            BuildGrid();
            BuildCharacter();
        }

        private void BuildCharacter()
        {

        }

        private void BuildGrid()
        {
            Container.Bind<GridController>().FromInstance(_gridController).AsSingle();
            Container.BindInterfacesTo<GridNavigator>().AsSingle();
        }
    }
}