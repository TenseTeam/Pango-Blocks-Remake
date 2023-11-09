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

        /// <summary>
        /// Opens the profile creator.
        /// </summary>
        public void OpenCreator()
        {
            ButtonSelectProfileDifficulty(_easyButton);
            OnOpenCreator?.Invoke();
        }

        /// <summary>
        /// Creates a new profile.
        /// </summary>
        public void CreateNewProfile()
        {
            if (!ProfilesManager.IsProfileNameValid(_profileName)) return;
            if (!ProfilesManager.CreateAndSelect(_profileName, _profileDifficulty)) return;
            ResetCreator();
            OnCreatedProfile?.Invoke();
        }

        /// <summary>
        /// Validates if the create button should be interactable or not based if the profile values are valid.
        /// </summary>
        public void ValidateCreateButton()
        {
            if (!ProfilesManager.IsProfileNameValid(_profileName))
            {
                _createButton.interactable = false;
                return;
            }

            _createButton.interactable = !ProfilesManager.HasProfile(_profileName);
        }

        /// <summary>
        /// Selects the correct difficulty sprite based on the selected difficulty button.
        /// </summary>
        /// <param name="btn">Difficulty button to select correctly.</param>
        private void ButtonSelectProfileDifficulty(UIDifficultyButton difficultyBtn)
        {
            DeselectDifficultySprites();
            difficultyBtn.ChangeToSelectedSprite();
            SetProfileDifficulty(difficultyBtn.Difficulty);
        }

        /// <summary>
        /// Sets the profile difficulty to the given difficulty.
        /// </summary>
        /// <param name="difficulty"></param>
        private void SetProfileDifficulty(GameDifficulty difficulty)
        {
            _profileDifficulty = difficulty;
        }

        /// <summary>
        /// Deselects the difficulty buttons sprites.
        /// </summary>
        private void DeselectDifficultySprites()
        {
            _easyButton.ChangeToDeselectedSprite();
            _hardButton.ChangeToDeselectedSprite();
        }

        /// <summary>
        /// Resets the creator to its default values.
        /// </summary>
        private void ResetCreator()
        {
            _profileNameField.text = string.Empty;
            _createButton.interactable = false;
        }
    }
}