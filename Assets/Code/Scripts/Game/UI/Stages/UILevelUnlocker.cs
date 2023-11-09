namespace ProjectPBR.UI.Stages
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Managers.Static.Profiles;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Managers.Static;
    using ProjectPBR.GameConfig.Constants;

    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class UILevelUnlocker : MonoBehaviour
    {
        [SerializeField, Header("Status Sprites")]
        private Sprite _unlocked;
        [SerializeField]
        private Sprite _completed;
        [SerializeField, Header("Lock Image")]
        private Image _lockImage;

        private Button _button;
        private Image _image;
        private int _levelIndex;
        private LevelStatus _status;

        /// <summary>
        /// Initializes the level unlocker.
        /// </summary>
        public void Init()
        {
            TryGetComponent(out _image);
            TryGetComponent(out _button);
            _levelIndex = transform.GetSiblingIndex();
            _button.onClick.AddListener(LoadLevelScene);
        }

        /// <summary>
        /// Set the status of the level unlocker.
        /// </summary>
        public void SetStatus()
        {
            _lockImage.enabled = false;
            LevelKey levelKey = LevelMapper.GetLevelKeyByLevelIndex(_levelIndex);
            _status = ProfileSelector.SelectedProfile.LevelsData[levelKey].Status;
            SetSpriteStatus(_status);
        }

        /// <summary>
        /// Sets the sprite status of the level unlocker.
        /// </summary>
        /// <param name="status"><see cref="LevelStatus"/> of the level for the level unlocker.</param>
        private void SetSpriteStatus(LevelStatus status)
        {
            switch (status)
            {
                case LevelStatus.Locked:
                    _image.sprite = _unlocked;
                    _lockImage.enabled = true;
                    break;
                case LevelStatus.Unlocked:
                    _image.sprite = _unlocked;
                    break;
                case LevelStatus.Completed:
                    _image.sprite = _completed;
                    break;
            }
        }

        /// <summary>
        /// Loads the level scene.
        /// </summary>
        private void LoadLevelScene()
        {
            if (_status == LevelStatus.Locked) return;
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.UIEvents.OnLevelButtonPressed);

            int buildIndex = LevelMapper.GetBuildIndexByLevelIndex(_levelIndex);
            MainManager.Ins.SceneManager.WaitChangeScene(buildIndex);
        }
    }
}
