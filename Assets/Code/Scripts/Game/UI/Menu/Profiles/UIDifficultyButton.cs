namespace ProjectPBR.UI.Menu.Profiles
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.UI.Buttons;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;

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

        /// <summary>
        /// Triggers the press of this button and triggers its selected difficulty.
        /// </summary>
        protected override void Press()
        {
            base.Press();
            SelectedDifficultyButton();
        }

        /// <summary>
        /// Changes the sprite of this button to its deselected sprite.
        /// </summary>
        public void ChangeToDeselectedSprite()
        {
            _spriteImage.sprite = _deselectedSprite;
        }

        /// <summary>
        /// Changes the sprite of this button to its selected sprite.
        /// </summary>
        public void ChangeToSelectedSprite()
        {
            _spriteImage.sprite = _selectedSprite;
        }

        /// <summary>
        /// Triggers the selected difficulty button.
        /// </summary>
        private void SelectedDifficultyButton()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnSelectedDifficultyButton, this);
        }
    }
}
