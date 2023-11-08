namespace VUDK.UI.Buttons
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Config;

    [RequireComponent(typeof(Button))]
    public class UIButton : MonoBehaviour
    {
        protected Button Button { get; private set; }

        [Header("Events")]
        public UnityEvent OnButtonPressedSuccess;
        public UnityEvent OnButtonPressedFail;

        protected virtual void Awake()
        {
            TryGetComponent(out Button button);
            Button = button;
        }

        protected virtual void OnEnable()
        {
            Button.onClick.AddListener(Press);
        }

        protected virtual void OnDisable()
        {
            Button.onClick.RemoveListener(Press);
        }

        protected virtual void Press()
        {
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.UIEvents.OnButtonPressed);
            OnButtonPressedSuccess?.Invoke();
        }
    }
}
