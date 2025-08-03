using System.Threading.Tasks;
using UnityEngine;

namespace ProjectCode
{
    public class PlayerRoot : MonoBehaviour
    {
        [SerializeField] private string _pathPlayer = "Player";

        private Player _player;

        public async Task Initializable()
        {
            _player = await AssetFactory.Create<Player>(_pathPlayer);
            ServiceLocator.Register(_player);
        }

        public async Task Startable()
        {
            SetupPlayer();
        }

        private void SetupPlayer()
        {
            Platform spawnPlatform = ServiceLocator.Get<IGridMap>().FindPlatform(PlatformType.Player);
            _player.transform.position = spawnPlatform.transform.position;
            _player.Initialize();
        }
    }
}