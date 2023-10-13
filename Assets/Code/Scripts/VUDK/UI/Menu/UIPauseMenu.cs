namespace VUDK.UI.Menu
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Extensions.Time;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.InputSystem;

    public class UIPauseMenu : MonoBehaviour
    {
        [SerializeField, Header("Pause Panel")]
        private RectTransform _pausePanel;
        [SerializeField]
        private RectTransform _pauseMenuPanel;

        private bool _isPaused;

        private void OnEnable()
        {
            DisablePause();
            InputsManager.Inputs.UI.PauseMenuToggle.canceled += PauseToggle;
        }

        private void OnDisable()
        {
            //DisablePause();
            InputsManager.Inputs.UI.PauseMenuToggle.canceled -= PauseToggle;
        }

        /// <summary>
        /// Pauses and unpauses the game by setting the time scale.
        /// </summary>
        public void PauseToggle(InputAction.CallbackContext context)
        {
            if (_isPaused)
            {
                DisablePause();
            }
            else
            {
                EnablePause();
            }
        }

        public void EnablePause()
        {
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.PauseEvents.OnPauseEnter);
            _isPaused = true;
            _pausePanel.gameObject.SetActive(true);
            TimeExtension.SetTimeScale(0f);
        }

        public void DisablePause()
        {
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.PauseEvents.OnPauseExit);
            _isPaused = false;
            _pausePanel.gameObject.SetActive(false);
            TimeExtension.SetTimeScale(1f);
            Reset();
        }

        private void Reset()
        {
            _pauseMenuPanel.GetChild(0).gameObject.SetActive(true);

            for (int i = 1; i < _pausePanel.transform.childCount; i++)
                _pauseMenuPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}