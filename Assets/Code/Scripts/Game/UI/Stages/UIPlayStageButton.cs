namespace ProjectPBR.UI.Stages
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Managers.Static;

    [RequireComponent(typeof(Button))]
    public class UIPlayStageButton : MonoBehaviour
    {
        [SerializeField, Header("Cutscene Unlocker")]
        private UICutsceneUnlocker _cutscene;
        private Button _button;

        private void Awake()
        {
            TryGetComponent(out _button);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(TryPlayFirstAvailableLevel);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(TryPlayFirstAvailableLevel);
        }

        private void TryPlayFirstAvailableLevel()
        {
            _cutscene.TryLaodCutscene();
            int buildIndex = LevelMapper.GetFirstUnlockedLevelBuildIndex();

            if (buildIndex < 0) return;
            MainManager.Ins.SceneManager.WaitChangeScene(buildIndex);
        }
    }
}
