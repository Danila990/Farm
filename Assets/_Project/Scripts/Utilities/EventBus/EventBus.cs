using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyCode
{
    public class EventBus : Singleton<EventBus>
    {
        private Dictionary<string, List<SignalInfoMood>> _signals = new();

        public void Sub<T>(Action<T> callback, int priority = 0)
        {
            string key = typeof(T).Name;
            AddSignal(key, callback, priority);
        }

        public void Sub(string signalName, Action callback, int priority = 0)
        {
            AddSignal(signalName, callback, priority);
        }

        public void Invoke<T>(T signal)
        {
            string key = typeof(T).Name;
            foreach (var callback in GetSignals(key))
            {
                var invokeSignal = callback as Action<T>;
                invokeSignal.Invoke(signal);
            }
        }

        public void Invoke(string signalName)
        {
            foreach (var callback in GetSignals(signalName))
            {
                var invokeSignal = callback as Action;
                invokeSignal.Invoke();
            }
        }

        public void Unsub<T>(Action<T> callback)
        {
            string key = typeof(T).Name;
            RemoveSignal(key, callback);
        }

        public void Unsub(string signalName, Action callback)
        {
            RemoveSignal(signalName, callback);
        }

        public void Clear()
        {
            _signals?.Clear();
        }

        private List<object> GetSignals(string signalName) 
        {
            List<object> calbacks = new List<object>();
            if (_signals.TryGetValue(signalName, out List<SignalInfoMood> signalList))
            {
                foreach (var signalnfo in signalList)
                {
                    calbacks.Add(signalnfo.Callback);
                }
                return calbacks;
            }

            Debug.Log($"Not registed signal: Name - {signalName}");
            return calbacks;
        }

        private void AddSignal(string signalName, object callback, int priority)
        {
            if (_signals.TryGetValue(signalName, out List<SignalInfoMood> signalList))
                signalList.Add(new SignalInfoMood(priority, callback));
            else
                _signals.Add(signalName, new List<SignalInfoMood>() { new(priority, callback) });

            _signals[signalName] = _signals[signalName].OrderByDescending(x => x.Priority).ToList();
        }

        private void RemoveSignal(string signalName, object callback)
        {
            if (_signals.TryGetValue(signalName, out List<SignalInfoMood> signalList))
            {
                var callbackToDelete = signalList.FirstOrDefault(x => x.Callback.Equals(callback));
                if (callbackToDelete.Callback != null)
                    _signals[signalName].Remove(callbackToDelete);
            }
            else
                Debug.LogError($"Not Remove Signal: Name - {signalName}, Callback - {callback}");
        }
    }

    public struct SignalInfoMood
    {
        public readonly int Priority;
        public readonly object Callback;

        public SignalInfoMood(int priority, object callback)
        {
            Priority = priority;
            Callback = callback;
        }
    }
}
