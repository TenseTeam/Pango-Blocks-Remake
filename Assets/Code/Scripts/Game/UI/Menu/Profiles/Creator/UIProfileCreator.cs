namespace ProjectPBR.UI.Menu.Profiles.Creator
{
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Static.Profiles;

    public class UIProfileCreator : MonoBehaviour
    {
        [SerializeField, Header("Difficulty Buttons")]
        private UIDifficultyButton _easyButton;
        [SerializeField]
        private UIDifficultyButton _hardButton;

        [SerializeField, Header("Profile Buttons")]
        private Button _createButton;

        [SerializeField, Header("Profile Inputs")]
        private TMP_InputField _profileNameField;

        private GameDifficulty _profileDifficulty;

        private string _profileName => _profileNameField.text;

        [Header("Events")]
        public UnityEvent OnOpenCreator;
        public UnityEvent OnCreatedProfile;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<UIDifficultyButton>(GameConstants.UIEvents.OnSelectedDifficultyButton, ButtonSelectProfileDifficulty);
       }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<UIDifficultyButton>(GameConstants.UIEvents.OnSelectedDifficultyButton, ButtonSelectProfileDifficulty);
        }

        private void Start()
        {
            ValidateCreateButton();
        }

        public void OpenCreator()
        {
            ButtonSelectProfileDifficulty(_easyButton);
            OnOpenCreator?.Invoke();
        }

        public void CreateNewProfile()
        {
            if (!ProfilesManager.IsProfileNameValid(_profileName)) return;
            if (!ProfilesManager.CreateAndSelect(_profileName, _profileDifficulty)) return;
            ResetCreator();
            OnCreatedProfile?.Invoke();
        }

        public void ValidateCreateButton()
        {
            if (!ProfilesManager.IsProfileNameValid(_profileName))
            {
                _createButton.interactable = false;
                return;
            }

            _createButton.interactable = !ProfilesManager.HasProfile(_profileName);
        }

        private void ButtonSelectProfileDifficulty(UIDifficultyButton difficultyBtn)
        {
            DeselectDifficultySprites();
            difficultyBtn.ChangeToSelectedSprite();
            SetProfileDifficulty(difficultyBtn.Difficulty);
        }

        private void SetProfileDifficulty(GameDifficulty difficulty)
        {
            _profileDifficulty = difficulty;
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
    }
}