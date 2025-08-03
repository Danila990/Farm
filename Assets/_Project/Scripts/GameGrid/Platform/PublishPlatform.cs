using UnityEngine;
using ProjectCode;

namespace ProjectCode
{
    public class PublishPlatform : Platform
    {
        [SerializeField] private ProjectCode.EventType _eventType;

        public override void Event()
        {
            EventBus.Publish(_eventType);
        }
    }
}