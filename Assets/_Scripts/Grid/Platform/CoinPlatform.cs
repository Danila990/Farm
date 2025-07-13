using UnityEngine;

namespace Code
{
    public class CoinPlatform : Platform
    {
        [SerializeField] private GameObject _coin;

        private bool _isUp = false;

        private void OnEnable()
        {
            EventBus.Subscribe(EventType.Dead, RestartPlatform);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(EventType.Dead, RestartPlatform);
        }

        public override void Event()
        {
            if(!_isUp)
            {
                _coin.SetActive(false);
                _isUp = true;
                EventBus.Publish(EventType.Coin);
            }
        }

        private void RestartPlatform()
        {
            _isUp = false;
            _coin.SetActive(true);
        }
    }
}