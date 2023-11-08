namespace ProjectPBR.UI.Menu.Profiles
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;
    using VUDK.UI.Buttons;

    [RequireComponent(typeof(Button))]
    public class UIDifficultyButton : UIButton
    {
        [SerializeField, Header("Sprites")]
        private Sprite _selectedSprite;
        [SerializeField]
        private Sprite _deselectedSprite;

        [SerializeField, Header("Image")]
        private Image _spriteImage;

        [field: SerializeField, Header("Difficulty")]
        public GameDifficulty Difficulty { get; private set; }

        protected override void Press()
        {
            base.Press();
            SelectedDifficultyButton();
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
