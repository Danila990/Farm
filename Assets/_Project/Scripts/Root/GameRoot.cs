using UnityEngine;

namespace MyCode
{
    public class GameRoot : Singleton<GameRoot>
    {
        [SerializeField] private PlayerRoot _playerRoot;
        [SerializeField] private GridRoot _gridRoot;

        private void Start()
        {
            Initialize();
            Startable();
        }

        private void Initialize()
        {
            _gridRoot.Initialize();
            _playerRoot.Initialize();
        }

        private void Startable()
        {
            _playerRoot.Startable();
        }
    }
}
