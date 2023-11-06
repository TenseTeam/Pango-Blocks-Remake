namespace ProjectPBR.UI.Stages
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Managers.Static.Profiles;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Managers.Static;

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

        public void Init()
        {
            TryGetComponent(out _image);
            TryGetComponent(out _button);
            _levelIndex = transform.GetSiblingIndex();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(LoadLevelScene);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(LoadLevelScene);
        }

        public void SetStatus()
        {
            _lockImage.enabled = false;
            LevelKey levelKey = LevelMapper.GetLevelKeyByLevelIndex(_levelIndex);
            _status = ProfileSelector.SelectedProfile.LevelsData[levelKey].Status;
            SetSpriteStatus(_status);
        }

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

        private void LoadLevelScene()
        {
            if (_status == LevelStatus.Locked) return;

            int buildIndex = LevelMapper.GetBuildIndexByLevelIndex(_levelIndex);
            MainManager.Ins.SceneManager.WaitChangeScene(buildIndex);
        }
    }
}
