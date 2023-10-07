namespace VUDK.Features.Main.Timer.UI
{
    using UnityEngine;
    using TMPro;
    using VUDK.Features.Main.EventsSystem.Events;
    using VUDK.Features.Main.EventsSystem;
    using VUDK.Generic.Managers;

    public class TimerText : MonoBehaviour
    {
        [SerializeField]
        private string _incipit;

        [SerializeField]
        private TMP_Text _text;

        private void OnEnable()
        {
            GameManager.Instance.EventManager.AddListener<int>(EventKeys.CountdownEvents.OnCountdownCount, UpdateTimerText);
        }

        private void OnDisable()
        {
            GameManager.Instance.EventManager.RemoveListener<int>(EventKeys.CountdownEvents.OnCountdownCount, UpdateTimerText);
        }

        private void UpdateTimerText(int time)
        {
            _text.text = _incipit + time.ToString();
        }
    }
}