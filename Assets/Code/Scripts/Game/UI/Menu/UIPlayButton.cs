namespace ProjectPBR.UI.Menu
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using ProjectPBR.Managers.Static.Profiles;

    [RequireComponent(typeof(Button))]
    public class UIPlayButton : MonoBehaviour
    {
        private Button _button;

        [Header("Events")]
        public UnityEvent OnPlaySuccess;
        public UnityEvent OnPlayFail;

        private void Awake()
        {
            TryGetComponent(out _button);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(TryPlay);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(TryPlay);
        }

        private void TryPlay()
        {
            if (ProfileSelector.SelectedProfile == null)
            {
                OnPlayFail?.Invoke();
                return;
            }

            OnPlaySuccess?.Invoke();
        }
    }
}
