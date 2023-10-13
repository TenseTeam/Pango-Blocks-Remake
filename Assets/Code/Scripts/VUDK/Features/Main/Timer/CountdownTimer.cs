namespace VUDK.Features.Main.Timer
{
    using System.Collections;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;

    public class CountdownTimer : MonoBehaviour
    {
        public void StartTimer(int time)
        {
            StartCoroutine(CountdownRoutine(time));
        }

        public void StopTimer()
        {
            StopAllCoroutines();
        }

        private IEnumerator CountdownRoutine(int time)
        {
            do
            {
                MainManager.Ins.EventManager.TriggerEvent(EventKeys.CountdownEvents.OnCountdownCount, time);
                yield return new WaitForSeconds(1);
                time--;
            } while (time > 0);

            MainManager.Ins.EventManager.TriggerEvent(EventKeys.CountdownEvents.OnCountdownCount, time);
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.CountdownEvents.OnCountdownTimesUp);
        }
    }
}
