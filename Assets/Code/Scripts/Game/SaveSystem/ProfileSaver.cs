namespace ProjectPBR.SaveSystem
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.SaveSystem.Interfaces;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;

    public class ProfileSaver : MonoBehaviour, ISaver
    {
        private ProfileData _selectedProfile => ProfileSelector.SelectedProfile;

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
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            _selectedProfile.LevelsData[currentScene].Status = LevelStatus.Completed;
            Save();
        }
    }
}
