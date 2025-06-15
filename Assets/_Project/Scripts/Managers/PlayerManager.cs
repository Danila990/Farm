using UnityEngine;

namespace MyCode 
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [SerializeField] private PlayerScriptable _playerScriptable;

        public PlayerMover Mover {  get; private set; }
        public PlayerHealth Health { get; private set; }
        public PlayerGridNavigator GridNavigator { get; private set; }

        public PlayerScriptableInfo PlayerInfo => _playerScriptable.PlayerInfo;

        public void CreatePlayer()
        {
            Mover = Instantiate(PlayerInfo.Prefab);
            Health = Mover.GetComponent<PlayerHealth>();
            GridNavigator = Mover.GetComponent<PlayerGridNavigator>();
            Mover.Setup(PlayerInfo.MoveSpeed, PlayerInfo.RotateSpeed);
            Health.Setup(PlayerInfo.Health);
            GridNavigator.Setup();
        }

        public void ResetPlayer()
        {

        }

        public void PlayMode()
        {

        }

        public void PauseMove()
        {

        }
    }
}