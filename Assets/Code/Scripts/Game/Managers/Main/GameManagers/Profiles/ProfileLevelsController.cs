namespace ProjectPBR.Managers.Main.GameManagers.Profiles
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.SaveSystem.Interfaces;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Data.SaveDatas;
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

        /// <inheritdoc/>
        public void Save()
        {
            ProfilesManager.SaveProfile(_selectedProfile);
        }

        /// <summary>
        /// Completes the current level by its scene build index and unlocks the next one.
        /// </summary>
        private void CompleteLevel()
        {
            LevelKey levelKey = LevelMapper.GetLevelKeyByBuildIndex(SceneManager.CurrentSceneIndex);
            LevelOperation.CompleteLevel(levelKey);
            Save();
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnSavedCompletedLevel);
        }
    }
}
