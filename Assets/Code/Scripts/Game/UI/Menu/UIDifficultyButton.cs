namespace ProjectPBR.UI.Menu
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;

    [RequireComponent(typeof(Button))]
    public class UIDifficultyButton : MonoBehaviour
    {
        [SerializeField, Header("Sprites")]
        private Sprite _selectedSprite;
        [SerializeField]
        private Sprite _deselectedSprite;

        [SerializeField, Header("Image")]
        private Image _spriteImage;

        private Button _btn;

        [field: SerializeField, Header("Difficulty")]
        public GameDifficulty Difficulty { get; private set; }

        private void Awake()
        {
            TryGetComponent(out _btn);
        }

        private void OnEnable()
        {
            _btn.onClick.AddListener(SelectedDifficultyButton);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveListener(SelectedDifficultyButton);
        }

        public void ChangeToDeselectedSprite()
        {
            _spriteImage.sprite = _deselectedSprite;
        }

        public void ChangeToSelectedSprite()
        {
            _spriteImage.sprite = _selectedSprite;
        }

        private void SelectedDifficultyButton()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnSelectedDifficultyButton, this);
        }
    }
}
