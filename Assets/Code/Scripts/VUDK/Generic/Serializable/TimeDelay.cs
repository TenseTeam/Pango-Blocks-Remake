namespace VUDK.Generic.Serializable
{
    using UnityEngine;

    //[System.Serializable]
    public class TimeDelay
    {
        public float Delay { get; private set; }
        public float CurrentTime { get; private set; }

        public float ClampTime => Mathf.Clamp(CurrentTime, 0, Delay);
        public float ClampNormalizedTime => Mathf.Clamp01(CurrentTime / Delay);

        public TimeDelay(float delay)
        {
            Delay = delay;
        }

        public void AddDeltaTime()
        {
            CurrentTime += Time.deltaTime;
        }

        public bool IsReady()
        {
            if (CurrentTime >= Delay)
            {
                CurrentTime = 0;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            CurrentTime = 0;
        }
    }
}
