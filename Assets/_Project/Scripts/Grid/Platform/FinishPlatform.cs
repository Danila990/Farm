namespace MyCode
{
    public class FinishPlatform : Platform
    {
        public override void Event()
        {
            EventBus.Publish(EventType.FinishPlatform);
        }
    }
}