namespace ProjectPBR.UI.Stages
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Managers.Static;
    using ProjectPBR.Managers.Static.Profiles;

    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class UICutsceneUnlocker : MonoBehaviour
    {
        [SerializeField, Header("Status Sprites")]
        private Sprite _unlocked;

        [SerializeField]
        private Sprite _locked;

        private Image _image;
        private Button _button;
        private bool _isUnlocked;

        public void Init()
        {
            TryGetComponent(out _image);
            TryGetComponent(out _button);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(LoadCutscene);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(LoadCutscene);
        }

        public bool TryLaodCutscene()
        {
            if (!_isUnlocked) return false;

            int buildIndex = LevelMapper.GetCutsceneBuildIndex();
            MainManager.Ins.SceneManager.ChangeScene(buildIndex);

            return true;
        }

        public void SetStatus()
        {
            _isUnlocked = LevelOperation.IsCutsceneUnlocked(LevelMapper.CurrentStageIndex);
            SetSpriteStatus(_isUnlocked);
        }

        private void SetSpriteStatus(bool isUnlocked)
        {
            _image.sprite = isUnlocked ? _unlocked : _locked;
        }

        private void LoadCutscene() => TryLaodCutscene();
    }
}