using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyCode
{
    public static class EventBus
    {
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

        private static readonly Dictionary<(EventType, Type), List<Subscriber>> _subscribersTest = new Dictionary<(EventType, Type), List<Subscriber>>();

        private static readonly Type _voidType = typeof(void);

        public static void Subscribe<T>(Action<T> callback, int priority = 0)
        {
            var key = (EventType.Void, typeof(T));
            if (!_subscribersTest.TryGetValue(key, out var list))
            {
                list = new List<Subscriber>();
                _subscribersTest[key] = list;
            }
            InsertSubscriber(list, new Subscriber(callback, priority));
        }

        public static void Subscribe(EventType eventType, Action callback, int priority = 0)
        {
            var key = (eventType, _voidType);
            if (!_subscribersTest.TryGetValue(key, out var list))
            {
                list = new List<Subscriber>();
                _subscribersTest[key] = list;
            }
            InsertSubscriber(list, new Subscriber(callback, priority));
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            var key = (EventType.Void, typeof(T));
            if (_subscribersTest.TryGetValue(key, out var list))
            {
                list.RemoveAll(s => s.Callback.Equals(callback));
                if (list.Count == 0)
                    _subscribersTest.Remove(key);
            }
        }

        public static void Unsubscribe(EventType eventType, Action callback)
        {
            var key = (eventType, _voidType);
            if (_subscribersTest.TryGetValue(key, out var list))
            {
                list.RemoveAll(s => s.Callback.Equals(callback));
                if (list.Count == 0)
                    _subscribersTest.Remove(key);
            }
        }

        public static void Publish<T>(T eventData)
        {
            var key = (EventType.Void, typeof(T));
            if (_subscribersTest.TryGetValue(key, out var list))
            {
                var subscribersCopy = list.ToArray();
                foreach (var subscriber in subscribersCopy)
                {
                    ((Action<T>)subscriber.Callback)?.Invoke(eventData);
                }
            }

            Debug.Log("Publish: " + eventData);
        }

        public static void Publish(EventType eventType)
        {
            var key = (eventType, _voidType);
            if (_subscribersTest.TryGetValue(key, out var list))
            {
                var copy = list.ToArray();
                foreach (var subscriber in copy)
                {
                    ((Action)subscriber.Callback)?.Invoke();
                }
            }
        }

        private static void InsertSubscriber(List<Subscriber> list, Subscriber subscriber)
        {
            int index = list.FindIndex(s => s.Priority < subscriber.Priority);
            if (index == -1)
                list.Add(subscriber);
            else
                list.Insert(index, subscriber);
        }
    }

    public enum EventType
    {
        Void = 0,
        Start,
    }
}
