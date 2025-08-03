using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCode
{
    public enum EventType
    {
        Void = 0,
        Start = 1,
        Stop = 2,
        Coin = 3,
        Dead = 4,
        Respawn = 5,
        Finish = 6,
    }

    public static class EventBus
    {
        private readonly struct EventKey : IEquatable<EventKey>
        {
            public readonly EventType EventType;
            public readonly Type PayloadType;

            public EventKey(EventType eventType, Type payloadType)
            {
                EventType = eventType;
                PayloadType = payloadType;
            }

            public bool Equals(EventKey other) =>
                EventType == other.EventType && PayloadType == other.PayloadType;

            public override bool Equals(object obj) =>
                obj is EventKey other && Equals(other);

            public override int GetHashCode() =>
                HashCode.Combine(EventType, PayloadType);
        }

        private class Subscriber
        {
            public readonly Delegate Callback;
            public readonly int Priority;

            public Subscriber(Delegate callback, int priority)
            {
                Callback = callback;
                Priority = priority;
            }
        }

        private static readonly Dictionary<EventKey, List<Subscriber>> _subscribers = new Dictionary<EventKey, List<Subscriber>>();

        private static readonly Type _voidType = typeof(void);

        public static void Subscribe<T>(Action<T> callback, int priority = 0)
        {
            if (callback == null) 
                throw new ArgumentNullException(nameof(callback));

            var key = new EventKey(EventType.Void, typeof(T));
            AddSubscriber(key, new Subscriber(callback, priority));
        }

        public static void Subscribe(EventType eventType, Action callback, int priority = 0)
        {
            if (callback == null) 
                throw new ArgumentNullException(nameof(callback));

            if (eventType == EventType.Void)
                throw new ArgumentException("EventType.Void is reserved for generic parameterized events", nameof(eventType));

            var key = new EventKey(eventType, _voidType);
            AddSubscriber(key, new Subscriber(callback, priority));
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            var key = new EventKey(EventType.Void, typeof(T));
            RemoveSubscriber(key, callback);
        }

        public static void Unsubscribe(EventType eventType, Action callback)
        {
            if (callback == null) 
                throw new ArgumentNullException(nameof(callback));

            if (eventType == EventType.Void)
                throw new ArgumentException("EventType.Void is reserved for generic parameterized events", nameof(eventType));

            var key = new EventKey(eventType, _voidType);
            RemoveSubscriber(key, callback);
        }


        public static void Publish<T>(T eventData)
        {
            var key = new EventKey(EventType.Void, typeof(T));
            if (_subscribers.TryGetValue(key, out var subscribers))
            {
                var subscribersCopy = subscribers.ToArray();
                foreach (var subscriber in subscribersCopy)
                {
                    if (subscriber.Callback is Action<T> action)
                        action.Invoke(eventData);
                }
            }

            Debug.Log($"Publish: {key.EventType} with payload {key.PayloadType}");
        }

        public static void Publish(EventType eventType)
        {
            if (eventType == EventType.Void)
                throw new ArgumentException("EventType.Void is reserved for generic parameterized events", nameof(eventType));

            var key = new EventKey(eventType, _voidType);
            if (_subscribers.TryGetValue(key, out var subscribers))
            {
                var subscribersCopy = subscribers.ToArray();
                foreach (var subscriber in subscribersCopy)
                {
                    if (subscriber.Callback is Action action)
                        action.Invoke();
                }
            }

            Debug.Log($"Publish: {eventType}");
        }

        public static void Clear()
        {
            Debug.Log("EventBus cleared");
            _subscribers.Clear();
        }

        private static void AddSubscriber(EventKey key, Subscriber subscriber)
        {
            if (!_subscribers.TryGetValue(key, out var list))
            {
                list = new List<Subscriber>();
                _subscribers[key] = list;
            }

            int index = list.FindIndex(s => s.Priority < subscriber.Priority);
            if (index == -1)
                list.Add(subscriber);
            else
                list.Insert(index, subscriber);
        }

        private static void RemoveSubscriber(EventKey key, Delegate callback)
        {
            if (_subscribers.TryGetValue(key, out var list))
            {
                list.RemoveAll(s => s.Callback.Equals(callback));
                if (list.Count == 0)
                    _subscribers.Remove(key);
            }
        }
    }
}