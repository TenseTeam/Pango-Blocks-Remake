namespace VUDK.UI.Menu
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Static;
    using ProjectPBR.UI.Menu;

    public class UIProfileCreator : MonoBehaviour
    {
        [SerializeField, Header("Difficulty Buttons")]
        private UIDifficultyButton _easyButton;
        [SerializeField]
        private UIDifficultyButton _hardButton;

        [SerializeField, Header("Create Button")]
        private Button _createButton;

        [SerializeField, Header("Profile Inputs")]
        private TMP_InputField _profileNameField;

        private GameDifficulty _newProfileDifficulty;

        private string _newProfileName => _profileNameField.text.ToLower();

        [Header("Events")]
        public UnityEvent OnCreatedProfile;
        public UnityEvent OnDeletedProfile;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<UIDifficultyButton>(GameConstants.UIEvents.OnSelectedDifficultyButton, SelectProfileDifficulty);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<UIDifficultyButton>(GameConstants.UIEvents.OnSelectedDifficultyButton, SelectProfileDifficulty);
        }

        private void Start()
        {
            SelectProfileDifficulty(_easyButton);
            ValidateCreateButton();
        }

        public void SetProfileDifficulty(GameDifficulty difficulty)
        {
            _newProfileDifficulty = difficulty;
        }

        public void CreateNewProfile()
        {
            if (!IsProfileNameValid()) return;
            if (!ProfilesManager.CreateAndSelect(_newProfileName, _newProfileDifficulty)) return;
            ResetCreator();
            OnCreatedProfile?.Invoke();
        }

        public void ValidateCreateButton()
        {
            if (!IsProfileNameValid())
            {
                _createButton.interactable = false;
                return;
            }

            _createButton.interactable = !ProfilesManager.HasProfile(_newProfileName);
        }

        private void SelectProfileDifficulty(UIDifficultyButton difficultyBtn)
        {
            DeselectDifficultySprites();
            difficultyBtn.ChangeToSelectedSprite();
            SetProfileDifficulty(difficultyBtn.Difficulty);
        }

        private void DeselectDifficultySprites()
        {
            _easyButton.ChangeToDeselectedSprite();
            _hardButton.ChangeToDeselectedSprite();
        }

        private void ResetCreator()
        {
            _profileNameField.text = string.Empty;
            _createButton.interactable = false;
        }

        private bool IsProfileNameValid()
        {
            return 
                !string.IsNullOrEmpty(_newProfileName)
                && !string.IsNullOrWhiteSpace(_newProfileName)
                && !_newProfileName.Contains(" ");
        }

        //public void DeleteProfile()
        //{
        //    if (!ProfilesManager.DeleteProfile(ProfileSelector.SelectedProfile.ProfileName)) return;
        //    ProfileSelector.TrySelectFirstProfile();
        //    OnDeletedProfile?.Invoke();
        //}
    }
}