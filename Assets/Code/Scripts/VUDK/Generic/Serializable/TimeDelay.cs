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
        public bool IsReady { get; private set; }

        public bool IsRunning => _isProcessing;
        public float ClampTime => Mathf.Clamp(CurrentTime, 0, Delay);
        public float ClampNormalizedTime => Mathf.Clamp01(CurrentTime / Delay);

        private bool _isProcessing;

        public event Action OnCompleted;

        public TimeDelay()
        {
            _isProcessing = false;
        }

        public TimeDelay(float delay)
        {
            Delay = delay;
        }

        public void Start()
        {
            Reset();
            _isProcessing = true;
        }

        public void Stop() => _isProcessing = false;
        
        public void Resume() => _isProcessing = true;
        
        public void Reset()
        {
            IsReady = false;
            _isProcessing = false;
            CurrentTime = 0;
        }

        public bool Process() => Process(Time.deltaTime);

        public bool Process(float time)
        {
            if (!_isProcessing) return false;
            if (CurrentTime >= Delay) 
            {
                Complete();
                return false;
            }
            CurrentTime += time;
            return true;
        }

        public void ChangeDelay(float delay)
        {
            Delay = delay;
            Reset();
        }

        private void Complete()
        {
            IsReady = true;
            _isProcessing = false;
            OnCompleted?.Invoke();
        }
    }
}
