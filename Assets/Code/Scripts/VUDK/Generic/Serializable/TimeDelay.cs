namespace VUDK.Generic.Serializable
{
    using System;
    using UnityEngine;

    [System.Serializable]
    public class TimeDelay
    {
        [field: SerializeField, Min(0f)]
        public float Delay { get; private set; }
        public float CurrentTime { get; private set; }

        public float ClampTime => Mathf.Clamp(CurrentTime, 0, Delay);
        public float ClampNormalizedTime => Mathf.Clamp01(CurrentTime / Delay);
        public bool IsReady => CurrentTime >= Delay;
        public bool IsDelaying => !IsReady;

        public event Action OnCompleted;

        private bool _isStarted = false;

        public TimeDelay()
        {
            _isStarted = false;
        }

        public TimeDelay(float delay, bool startOnInitialization = false)
        {
            Delay = delay;
            _isStarted = startOnInitialization;
        }

        public void Start()
        {
            _isStarted = true;
        }

        public void Process()
        {
            if (!_isStarted) return;
            if (IsReady) 
            {
                OnCompleted?.Invoke();
                return;
            }
            CurrentTime += Time.deltaTime;
        }

        public void Reset()
        {
            _isStarted = false;
            CurrentTime = 0;
        }
    }
}
