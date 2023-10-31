namespace VUDK.Generic.Managers.Main
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;

    [DefaultExecutionOrder(-850)]
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, Delegate> _eventListeners = new Dictionary<string, Delegate>();

        public void AddListener(string eventKey, Action listener)
        {
            RegisterEvent(eventKey, listener);
        }

        public void AddListener<T>(string eventKey, Action<T> listener)
        {
            RegisterEvent(eventKey, listener);
        }

        public void RemoveListener(string eventKey, Action listener)
        {
            UnregisterEvent(eventKey, listener);
        }

        public void RemoveListener<T>(string eventKey, Action<T> listener)
        {
            UnregisterEvent(eventKey, listener);
        }

        public void TriggerEvent(string eventKey)
        {
            if (_eventListeners.ContainsKey(eventKey))
            {
                Action del = _eventListeners[eventKey] as Action;
                del.Invoke();
            }
        }

        public void TriggerEvent<T>(string eventKey, T parameter)
        {
            if (_eventListeners.ContainsKey(eventKey))
            {
                Action<T> del = _eventListeners[eventKey] as Action<T>;
                del.Invoke(parameter);
            }
        }

        private void RegisterEvent(string eventKey, Delegate listener)
        {
            if (_eventListeners.ContainsKey(eventKey))
                _eventListeners[eventKey] = Delegate.Combine(_eventListeners[eventKey], listener);
            else
                _eventListeners.Add(eventKey, listener);
        }

        private void UnregisterEvent(string eventKey, Delegate listener)
        {
            if (_eventListeners.ContainsKey(eventKey))
                _eventListeners[eventKey] = Delegate.Remove(_eventListeners[eventKey], listener);
        }
    }
}
