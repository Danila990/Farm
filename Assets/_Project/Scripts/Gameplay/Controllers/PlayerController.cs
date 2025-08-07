using UnityEngine;

namespace ProjectCode
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player _player;

        public void Initialize()
        {
            _player = Instantiate(_player);
            _player.Init();
            ServiceLocator.Register(_player);
        }

        public void Startable()
        {
            SetupPlayer();
            _player.Startable();
        }

        private void SetupPlayer()
        {
            Platform spawnPlatform = ServiceLocator.Get<IGridMap>().FindPlatform(PlatformType.Player);
            _player.transform.position = spawnPlatform.transform.position;
        }
    }
}