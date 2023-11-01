namespace VUDK.UI.Menu
{
    using UnityEngine;
    using TMPro;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.Managers.Static;
    using ProjectPBR.SaveSystem;

    public class UIProfiler : MonoBehaviour
    {
        [SerializeField, Header("Profile Inputs")]
        private TMP_Text _profileNameText;

        private string _profileName;
        private GameDifficulty _difficulty;

        public void SetProfileName()
        {
            _profileName = _profileNameText.text;
        }

        public void SetDifficulty(GameDifficulty difficulty)
        {
            _difficulty = difficulty;
        }

        public void SelectProfile()
        {
            ProfileSelector.SelectProfile(_profileName);
        }

        public void CreateProfile()
        {
            if(string.IsNullOrEmpty(_profileName)) return;
            ProfilesManager.CreateAndSelect(_profileName, _difficulty);
        }

        public void DeleteProfile()
        {
            ProfilesManager.DeleteProfile(ProfileSelector.SelectedProfile.ProfileName);
            ProfileSelector.TrySelectFirstProfile();
        }
    }
}