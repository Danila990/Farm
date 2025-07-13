using Code;
using System;
using VContainer;
using VContainer.Unity;

namespace Code
{
    public class GridEventHandler : IDisposable, IStartable
    {
        [Inject] private IGridService _service;
        
        public void Start()
        {
            EventBus.Subscribe(Code.EventType.Respawn, _service.RestartGrid);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe(Code.EventType.Respawn, _service.RestartGrid);
        }
    }
}