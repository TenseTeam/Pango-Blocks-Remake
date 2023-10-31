namespace ProjectPBR.SaveSystem
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.SaveSystem.Interfaces;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers.SceneManager;

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
                    ProfileSelector.CreateAndSelect();          // If no profile exists, create one
                }
            }
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameConstants.Events.OnBeginGameWonPhase, SaveLevel);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameConstants.Events.OnBeginGameWonPhase, SaveLevel);
        }

        public void Save()
        {
            ProfilesManager.SaveProfile(_selectedProfile);
        }

        private void SaveLevel()
        {
            LevelKey levelKey = SceneManager.GetLevelKeyByBuildIndex(SceneManager.CurrentSceneIndex);
            Debug.Log(levelKey.SaveIndex + " " + levelKey.Difficulty);
            _selectedProfile.LevelsData[levelKey].Status = LevelStatus.Completed;
            Save();
        }
    }
}
