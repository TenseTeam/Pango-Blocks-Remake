namespace ProjectPBR.UI.Menu
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;

    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class UIDifficultyButton : MonoBehaviour
    {
        [SerializeField, Header("Sprites")]
        private Sprite _selectedSprite;
        [SerializeField]
        private Sprite _deselectedSprite;
        [SerializeField, Header("Difficulty")]
        private GameDifficulty _difficulty;

        private Button _btn;

        private void Awake()
        {
            TryGetComponent(out _btn);
        }

        private void Start()
        {
            _btn.onClick.AddListener(SelectDifficulty);
        }

        public void Deselect()
        {
            _btn.image.sprite = _deselectedSprite;
        }

        private void SelectDifficulty()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnSelectedDifficulty, this);
        }
    }
}
