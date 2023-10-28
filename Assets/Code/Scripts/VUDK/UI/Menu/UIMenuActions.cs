namespace VUDK.UI.Menu
{
    using UnityEngine;
    using VUDK.Features.Main.SceneManagement;
    using VUDK.Generic.Managers.Main;

    [RequireComponent(typeof(SceneSwitcher))]
    public class UIMenuActions : MonoBehaviour
    {
        private SceneSwitcher _sceneSwitcher;

        private void Awake()
        {
            TryGetComponent(out _sceneSwitcher);
        }

        private void Start()
        {
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.SceneEvents.OnMainMenuLoaded);
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