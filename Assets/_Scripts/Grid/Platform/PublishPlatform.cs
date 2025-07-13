using UnityEngine;

namespace Code
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