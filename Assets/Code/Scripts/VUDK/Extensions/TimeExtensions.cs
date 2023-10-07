namespace VUDK.Extensions.Time
{
    using UnityEngine;

    public static class TimeExtension
    {
        public static void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }
    }
}