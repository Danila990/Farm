using ProjectCode;
using System.Threading;
using UnityEngine;

namespace ProjectCode
{
    public class Player : MonoBehaviour
    {
        private PlayerNavigation _navigation;
        private PlayerMover _mover;
        private PlayerDirectionArrow _directionArrow;

        public void Init()
        {
            _navigation = GetComponent<PlayerNavigation>();
            _mover = GetComponent<PlayerMover>();
            _directionArrow = GetComponent<PlayerDirectionArrow>();
            _mover.Init();
            _directionArrow.Init();
        }

        public void Startable()
        {
            _navigation.Setup();
        }
    }
}