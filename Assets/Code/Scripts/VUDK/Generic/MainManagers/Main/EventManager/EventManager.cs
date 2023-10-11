namespace VUDK.Generic.Managers.Main
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [DefaultExecutionOrder(-850)]
    public class EventManager : MonoBehaviour
    {
        private Dictionary<string, Delegate> s_EventListeners = new Dictionary<string, Delegate>();

        public void AddListener(string eventKey, Action listener)
        {
            RegisterEvent(eventKey, listener);
        }

        public void AddListener<T>(string eventKey, Action<T> listener)
        {
            RegisterEvent(eventKey, listener);
        }

        public void AddListener<T1, T2>(string eventKey, Action<T1, T2> listener)
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

        public void RemoveListener<T1, T2>(string eventKey, Action<T1, T2> listener)
        {
            UnregisterEvent(eventKey, listener);
        }

        public void TriggerEvent(string eventKey)
        {
            if (s_EventListeners.ContainsKey(eventKey))
            {
                Action del = s_EventListeners[eventKey] as Action;
                del.Invoke();
            }
        }

        public void TriggerEvent<T>(string eventKey, T parameter)
        {
            if (s_EventListeners.ContainsKey(eventKey))
            {
                Action<T> del = s_EventListeners[eventKey] as Action<T>;
                del.Invoke(parameter);
            }
        }

        public void TriggerEvent<T1, T2>(string eventKey, T1 param1, T2 param2)
        {
            if (s_EventListeners.ContainsKey(eventKey))
            {
                Action<T1, T2> del = s_EventListeners[eventKey] as Action<T1, T2>;
                del.Invoke(param1, param2);
            }
        }

        [System.ObsoleteAttribute("This TriggerEvent is obsolete. Use TriggerEvent<T> instead.", false)]
        public void TriggerEvent(string eventKey, params object[] parameters)
        {
            if (s_EventListeners.ContainsKey(eventKey))
            {
                s_EventListeners[eventKey].DynamicInvoke(parameters);
            }
        }

        private void RegisterEvent(string eventKey, Delegate listener)
        {
            if (s_EventListeners.ContainsKey(eventKey))
                s_EventListeners[eventKey] = Delegate.Combine(s_EventListeners[eventKey], listener);
            else
                s_EventListeners.Add(eventKey, listener);
        }

        private void UnregisterEvent(string eventKey, Delegate listener)
        {
            if (s_EventListeners.ContainsKey(eventKey))
                s_EventListeners[eventKey] = Delegate.Remove(s_EventListeners[eventKey], listener);
        }
    }
}
