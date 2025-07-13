using Code;
using System;
using VContainer.Unity;

namespace Code
{
    public class CoinScore : IDisposable, IStartable
    {
        public int Score { get; private set; } = 0;

        private CointEvent _coinEvent;

        public void Start()
        {
            EventBus.Subscribe(EventType.Coin, UpdateScore);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe(EventType.Coin, UpdateScore);
        }

        private void UpdateScore()
        {
            if (_coinEvent == null)
                _coinEvent = new CointEvent();

            Score++;
            _coinEvent.Score = Score;
            EventBus.Publish(_coinEvent);
        }
    }
}