using Code;
using System;
using VContainer;
using VContainer.Unity;
using EventType = Code.EventType;

namespace Code
{
    public class PlayerEventHandler : IDisposable, IStartable
    {
        [Inject] private IPlayerService _service;

        public void Start()
        {
            EventBus.Subscribe(EventType.Start, _service.StartPlayer);
            EventBus.Subscribe(EventType.Stop, _service.StopPlayer);
            EventBus.Subscribe(EventType.Dead, _service.DeadPlayer);
            EventBus.Subscribe(EventType.Respawn, _service.RestartPlayer);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe(EventType.Start, _service.StartPlayer);
            EventBus.Unsubscribe(EventType.Stop, _service.StopPlayer);
            EventBus.Unsubscribe(EventType.Dead, _service.DeadPlayer);
            EventBus.Unsubscribe(EventType.Respawn, _service.RestartPlayer);
        }
    }
}