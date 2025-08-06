using Project1;
using System.Threading;
using UnityEngine;


namespace ProjectCode
{
    public class Player : MonoBehaviour
    {
        private PlayerGridMover _gridMover;
        private PlayerDirectionArrow _directionArrow;

        public void Init(PlayerInfo playerInfo)
        {
            _gridMover = GetComponent<PlayerGridMover>();
            _directionArrow = GetComponent<PlayerDirectionArrow>();

            _gridMover.Init(playerInfo);
            _directionArrow.Init(_gridMover, playerInfo);
        }

        public void Startable()
        {
            _gridMover.Startable();
        }
    }
}