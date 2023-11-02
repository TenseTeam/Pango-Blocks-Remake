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

        public void ConfirmModify()
        {
            if (!ProfilesManager.IsProfileNameValid(_profileModifiedName)) return;
            //if (ProfilesManager.HasMultipleProfiles(_profileModifiedName)) return;

            ProfileSelector.ChangeSelectedProfileValues(_profileModifiedName, _profileModifiedDifficulty);
            OnProfileModified?.Invoke();
        }

        public void DeleteOpenProfile()
        {
            ProfileSelector.DeleteAndDeselect();
            ProfileSelector.TrySelectFirstProfile();
            MainManager.Ins.EventManager.TriggerEvent(GameConstants.Events.OnDeletedProfile, ProfilesManager.Count);
            OnDeletedProfile?.Invoke();
        }

        public void ValidateModifyButton()
        {
            //if (ProfilesManager.HasMultipleProfiles(_profileModifiedName))
            //{
            //    _confirmButton.interactable = false;
            //    return;
            //}

            _confirmButton.interactable = ProfilesManager.IsProfileNameValid(_profileModifiedName);
        }

        public void OpenProfile()
        {
            if (ProfileSelector.SelectedProfile == null) return;

            OpenProfileName();
            OpenProfileDifficulty();
            ValidateModifyButton();
            OnOpenProfile?.Invoke();
        }

        private void OpenProfileName()
        {
            _profileModifiedNameField.text = ProfileSelector.SelectedProfile.ProfileName;
        }

        private void OpenProfileDifficulty()
        {
            DeselectDifficultySprites();
            GameDifficulty diff = ProfileSelector.SelectedProfile.CurrentDifficulty;

            if (diff == GameDifficulty.Easy)
                _easyButton.ChangeToSelectedSprite();
            else
                _hardButton.ChangeToSelectedSprite();
        }

        private void ButtonSelectProfileDifficulty(UIDifficultyButton btn)
        {
            DeselectDifficultySprites();
            btn.ChangeToSelectedSprite();
            SetProfileDifficulty(btn.Difficulty);
        }

        private void SetProfileDifficulty(GameDifficulty difficulty)
        {
            _profileModifiedDifficulty = difficulty;
        }

        private void DeselectDifficultySprites()
        {
            _easyButton.ChangeToDeselectedSprite();
            _hardButton.ChangeToDeselectedSprite();
        }
    }
}
