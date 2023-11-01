namespace ProjectPBR.SaveSystem
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

    public class ProfileSaver : MonoBehaviour, ISaver, ICastSceneManager<GameSceneManager>
    {
        private ProfileData _selectedProfile => ProfileSelector.SelectedProfile;

        public GameSceneManager SceneManager => MainManager.Ins.SceneManager as GameSceneManager;

        private void Awake()
        {
            if (ProfileSelector.SelectedProfile == null)        // If no profile is selected
            {
                if(!ProfileSelector.TrySelectFirstProfile())    // Try to select the first profile
                {
                    ProfilesManager.CreateRandomAndSelect();          // If no profile exists, create one
                }
            }
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
            ProfileLevelOperation.SetLevelStatus(levelKey, LevelStatus.Completed);
            ProfileLevelOperation.SetLevelStatus(++levelKey, LevelStatus.Unlocked);
            Save();
        }
    }
}
