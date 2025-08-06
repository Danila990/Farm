using Project1;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace ProjectCode
{
    public class PlayerController : MonoBehaviour
    {
        private Player _player;
        private PlayerGridMover _gridMover;

        public void Initialize(LevelSettings levelSettings)
        {
            _player = Instantiate(levelSettings.PlayerInfo.PlayerPrefab);
            _gridMover = _player.GetComponent<PlayerGridMover>();
            _player.Init(levelSettings.PlayerInfo);
            ServiceLocator.Register(_player);
            ServiceLocator.Register(_gridMover);
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