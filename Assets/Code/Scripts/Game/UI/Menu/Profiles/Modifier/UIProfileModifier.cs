namespace ProjectPBR.UI.Menu.Profiles.Modifier
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    using TMPro;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Static.Profiles;

    public class UIProfileModifier : MonoBehaviour
    {
        [SerializeField, Header("Difficulty Buttons")]
        private UIDifficultyButton _easyButton;
        [SerializeField]
        private UIDifficultyButton _hardButton;

        [SerializeField, Header("Profile Buttons")]
        private Button _confirmButton;

        [SerializeField, Header("Profile Inputs")]
        private TMP_InputField _profileModifiedNameField;

        private GameDifficulty _profileModifiedDifficulty;

        private string _profileModifiedName => _profileModifiedNameField.text;

        [Header("Events")]
        public UnityEvent OnOpenProfile;
        public UnityEvent OnProfileModified;
        public UnityEvent OnDeletedProfile;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<UIDifficultyButton>(GameConstants.UIEvents.OnSelectedDifficultyButton, ButtonSelectProfileDifficulty);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<UIDifficultyButton>(GameConstants.UIEvents.OnSelectedDifficultyButton, ButtonSelectProfileDifficulty);
        }

        /// <summary>
        /// Confirm the profile modification and modify the selected profile with the new values.
        /// </summary>
        public void ConfirmModify()
        {
            if (!ProfilesManager.IsProfileNameValid(_profileModifiedName)) return;
            if (ProfilesManager.HasProfile(_profileModifiedName, ProfileSelector.SelectedProfile)) return;

            ProfileSelector.ChangeSelectedProfileValues(_profileModifiedName, _profileModifiedDifficulty);
            OnProfileModified?.Invoke();
        }

        /// <summary>
        /// Deletes the selected profile and selects the first profile in the list.
        /// </summary>
        public void DeleteOpenProfile()
        {
            ProfileSelector.DeleteAndDeselect();
            ProfileSelector.TrySelectFirstProfile();
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnDeletedProfile, ProfilesManager.Count);
            OnDeletedProfile?.Invoke();
        }

        /// <summary>
        /// Validates the modify button based on the current profile name.
        /// </summary>
        public void ValidateModifyButton()
        {
            if (ProfilesManager.HasProfile(_profileModifiedName, ProfileSelector.SelectedProfile))
            {
                _confirmButton.interactable = false;
                return;
            }

            _confirmButton.interactable = ProfilesManager.IsProfileNameValid(_profileModifiedName);
        }

        /// <summary>
        /// Opens the selected profile.
        /// </summary>
        public void OpenProfile()
        {
            if (ProfileSelector.SelectedProfile == null) return;

            OpenProfileName();
            OpenProfileDifficulty();
            ValidateModifyButton();
            OnOpenProfile?.Invoke();
        }

        /// <summary>
        /// Sets the profile name field to the selected profile name.
        /// </summary>
        private void OpenProfileName()
        {
            _profileModifiedNameField.text = ProfileSelector.SelectedProfile.ProfileName;
        }

        /// <summary>
        /// Sets the profile difficulty to the selected profile difficulty.
        /// </summary>
        private void OpenProfileDifficulty()
        {
            DeselectDifficultySprites();
            GameDifficulty diff = ProfileSelector.SelectedProfile.CurrentDifficulty;

            if (diff == GameDifficulty.Easy)
                _easyButton.ChangeToSelectedSprite();
            else
                _hardButton.ChangeToSelectedSprite();
        }

        /// <summary>
        /// Selects the correct difficulty sprite based on the selected difficulty button.
        /// </summary>
        /// <param name="btn">Difficulty button to select correctly.</param>
        private void ButtonSelectProfileDifficulty(UIDifficultyButton btn)
        {
            DeselectDifficultySprites();
            btn.ChangeToSelectedSprite();
            SetProfileDifficulty(btn.Difficulty);
        }

        /// <summary>
        /// Sets the profile difficulty to the given difficulty.
        /// </summary>
        /// <param name="difficulty"></param>
        private void SetProfileDifficulty(GameDifficulty difficulty)
        {
            _profileModifiedDifficulty = difficulty;
        }

        /// <summary>
        /// Deselects the difficulty buttons sprites.
        /// </summary>
        private void DeselectDifficultySprites()
        {
            _easyButton.ChangeToDeselectedSprite();
            _hardButton.ChangeToDeselectedSprite();
        }
    }
}
