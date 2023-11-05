namespace ProjectPBR.Managers.Main.GameManagers.Profiles
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.SaveSystem.Interfaces;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Managers.Main.SceneManager;
    using ProjectPBR.Managers.Static;
    using ProjectPBR.Managers.Static.Profiles;

    public class ProfileLevelsController : MonoBehaviour, ISaver, ICastSceneManager<GameSceneManager>
    {
        private ProfileData _selectedProfile => ProfileSelector.SelectedProfile;

        public GameSceneManager SceneManager => MainManager.Ins.SceneManager as GameSceneManager;

        private void Awake()
        {
            ProfileSelector.SelectOrCreateIfNoProfileSelected();
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameWonPhase, CompleteLevel);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameWonPhase, CompleteLevel);
        }

        public void Save()
        {
            ProfilesManager.SaveProfile(_selectedProfile);
        }

        private void CompleteLevel()
        {
            LevelKey levelKey = LevelMapper.GetLevelKeyByBuildIndex(SceneManager.CurrentSceneIndex);
            LevelOperation.SetLevelStatus(levelKey, LevelStatus.Completed);
            LevelOperation.SetLevelStatus(++levelKey, LevelStatus.Unlocked);
            Save();
        }
    }
}
