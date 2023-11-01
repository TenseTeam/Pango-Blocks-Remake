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
        private TMP_Text _profileNameText;

        private GameDifficulty _newProfileDifficulty;
        private string _newProfileName;

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

        public void SetNewProfileName()
        {
            _newProfileName = _profileNameText.text;
            //_newProfileName = _newProfileName.ToLowerInvariant();
        }

        public void SetProfileDifficulty(GameDifficulty difficulty)
        {
            _newProfileDifficulty = difficulty;
        }

        public void CreateNewProfile()
        {
            if (string.IsNullOrEmpty(_newProfileName)) return;
            if (!ProfilesManager.CreateAndSelect(_newProfileName, _newProfileDifficulty)) return;
            _profileNameText.text = string.Empty;
            OnCreatedProfile?.Invoke();
        }

        public void ValidateCreateButton()
        {
            if (string.IsNullOrEmpty(_newProfileName))
            {
                _createButton.interactable = false;
                return;
            }

            _createButton.interactable = ProfilesManager.HasProfile(_newProfileName);
        }

        private void SelectProfileDifficulty(UIDifficultyButton difficultyBtn)
        {
            DeselectSprites();
            difficultyBtn.ChangeToSelectedSprite();
            SetProfileDifficulty(difficultyBtn.Difficulty);
        }

        private void DeselectSprites()
        {
            _easyButton.ChangeToDeselectedSprite();
            _hardButton.ChangeToDeselectedSprite();
        }

        //public void DeleteProfile()
        //{
        //    if (!ProfilesManager.DeleteProfile(ProfileSelector.SelectedProfile.ProfileName)) return;
        //    ProfileSelector.TrySelectFirstProfile();
        //    OnDeletedProfile?.Invoke();
        //}
    }
}