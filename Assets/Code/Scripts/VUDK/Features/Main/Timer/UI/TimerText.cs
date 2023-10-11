namespace VUDK.Features.Main.Timer.UI
{
    using UnityEngine;
    using TMPro;
    using VUDK.Generic.Managers.Main;

    public class TimerText : MonoBehaviour
    {
        [SerializeField]
        private string _incipit;

        [SerializeField]
        private TMP_Text _text;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<int>(EventKeys.CountdownEvents.OnCountdownCount, UpdateTimerText);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<int>(EventKeys.CountdownEvents.OnCountdownCount, UpdateTimerText);
        }

        private void UpdateTimerText(int time)
        {
            _text.text = _incipit + time.ToString();
        }
    }
}