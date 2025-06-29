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

        private static readonly Dictionary<Type, List<Subscriber>> _subscribers = new Dictionary<Type, List<Subscriber>>();

        public static void Subscribe<T>(Action<T> callback, int priority = 0)
        {
            var type = typeof(T);
            if (!_subscribers.TryGetValue(type, out var list))
            {
                list = new List<Subscriber>();
                _subscribers[type] = list;
            }
            var subscriber = new Subscriber(callback, priority);

            int index = list.FindIndex(s => s.Priority < priority);
            if (index == -1)
                list.Add(subscriber);
            else
                list.Insert(index, subscriber);
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var list))
            {
                list.RemoveAll(s => s.Callback.Equals(callback));
                if (list.Count == 0)
                    _subscribers.Remove(type);
            }
        }

        public static void Publish<T>(T eventData)
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var list))
            {
                var subscribersCopy = list.ToArray();
                foreach (var subscriber in subscribersCopy)
                {
                    ((Action<T>)subscriber.Callback)?.Invoke(eventData);
                }
            }

            Debug.Log("Publish: " + eventData);
        }

        public static void Clear()
        {
            _subscribers.Clear();
        }
    }
}
