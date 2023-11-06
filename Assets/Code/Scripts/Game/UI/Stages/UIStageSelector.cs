namespace ProjectPBR.UI.Stages
{
    using UnityEngine;
    using ProjectPBR.Managers.Static;

    public class UIStageSelector : MonoBehaviour
    {
        [SerializeField, Header("Level Buttons")]
        private UILevelUnlocker[] _levelButtons;

        [SerializeField, Header("Cutscene Button")]
        private UICutsceneUnlocker _cutsceneButton;

        [SerializeField, Min(0), Header("Stage Index")]
        private int _stageIndex;

        private void Awake()
        {
            InitButtons();
        }

        private void OnEnable()
        {
            LevelMapper.TrySetCurrentStageIndex(_stageIndex);
            SetButtonsStatus();
        }

        private void InitButtons()
        {
            foreach(UILevelUnlocker levelButton in _levelButtons)
                levelButton.Init();

            _cutsceneButton.Init();
        }

        private void SetButtonsStatus()
        {
            foreach (UILevelUnlocker levelButton in _levelButtons)
                levelButton.SetStatus();

            _cutsceneButton.SetStatus();
        }
    }
}
