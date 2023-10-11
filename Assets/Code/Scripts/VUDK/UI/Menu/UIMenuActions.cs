namespace VUDK.UI.Menu
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Utility;

    [RequireComponent(typeof(SwitchScene))]
    public class UIMenuActions : MonoBehaviour
    {
        private SwitchScene _sceneSwitcher;

        private void Awake()
        {
            TryGetComponent(out _sceneSwitcher);
        }

        public void ChangeScene(int sceneIndex)
        {
            ClickButton();
            _sceneSwitcher.WaitChangeScene(sceneIndex);
        }

        public void ExitApplication()
        {
            ClickButton();
            Application.Quit();
        }

        public void ClickButton()
        {
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.UIEvents.OnButtonClick);
        }
    }
}