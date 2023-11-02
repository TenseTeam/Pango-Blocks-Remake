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
        private Sprite _locked;
        [SerializeField]
        private Sprite _completed;

        private Button _button;
        private Image _image;
        private int _levelNumber;
        private LevelStatus _status;

        private void Awake()
        {
            TryGetComponent(out _image);
            TryGetComponent(out _button);

            SetStatus();
            _levelNumber = transform.GetSiblingIndex();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(LoadLevelScene);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(LoadLevelScene);
        }

        private void SetStatus()
        {
            LevelKey levelKey = LevelMapper.GetLevelKeyByLevelIndex(_levelNumber);
            _status = ProfileSelector.SelectedProfile.LevelsData[levelKey].Status;
            SetSpriteStatus(_status);
        }

        private void SetSpriteStatus(LevelStatus status)
        {
            switch (status)
            {
                case LevelStatus.Locked:
                    _image.sprite = _locked;
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

            int buildIndex = LevelMapper.GetBuildIndexByLevelIndex(_levelNumber);
            MainManager.Ins.SceneManager.WaitChangeScene(buildIndex);
        }
    }
}
