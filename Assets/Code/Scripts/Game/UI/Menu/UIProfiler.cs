namespace VUDK.UI.Menu
{
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Managers.Static;
    using ProjectPBR.SaveSystem;
    using UnityEngine;

    public class UIProfiler : MonoBehaviour
    {
        private string _profileName;
        private GameDifficulty _difficulty;

        public void SetProfileName(string profileName)
        {
            _profileName = profileName;
        }

        public void SetGameDifficulty(GameDifficulty difficulty)
        {
            _difficulty = difficulty;
        }

        public void SelectProfile()
        {
            ProfileSelector.SelectProfile(_profileName);
        }

        public void CreateProfile()
        {
            ProfilesManager.CreateAndSelect(_profileName, _difficulty);
        }

        public void DeleteProfile()
        {
            ProfilesManager.DeleteProfile(ProfileSelector.SelectedProfile.ProfileName);
            ProfileSelector.TrySelectFirstProfile();
        }
    }
}