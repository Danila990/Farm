using UnityEngine;

namespace MyCode
{
    public class PublishPlatform : Platform
    {
        [SerializeField] private EventType _eventType;

        public override void Event()
        {
            EventBus.Publish(_eventType);
        }
    }
}