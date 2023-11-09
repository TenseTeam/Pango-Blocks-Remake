namespace ProjectPBR.UI.Menu.Profiles
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Generic.Managers.Main;
    using VUDK.Extensions.Colors;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Static.Profiles;

    public class UIProfileSwitcher : MonoBehaviour
    {
        [SerializeField, Header("Switch Button")]
        private Button _switchButton;

        [SerializeField, Header("Profile Tag")]
        private TMP_Text _profileTagNameText;
        [SerializeField]
        private string _defaultProfileString;
        [SerializeField]
        private Color _defaultProfileTagColor;
        [SerializeField]
        private Image _profileTagDifficultyImage;
        [SerializeField]
        private Image _profileTagImage;

        [SerializeField, Header("Buttons")]
        private Button _addProfileButton;

        [SerializeField, Header("Difficulty Sprites")]
        private Sprite _easySprite;
        [SerializeField]
        private Sprite _hardSprite;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<ProfileData>(GameConstants.Events.OnProfileAlteration, SetProfileTag);
            MainManager.Ins.EventManager.AddListener<int>(GameConstants.Events.OnDeletedProfile, ValidateProfileTag);
            MainManager.Ins.EventManager.AddListener<int>(GameConstants.Events.OnCreatedProfile, ValidateAddProfileButton);
            _switchButton.onClick.AddListener(SwitchProfile);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<ProfileData>(GameConstants.Events.OnProfileAlteration, SetProfileTag);
            MainManager.Ins.EventManager.RemoveListener<int>(GameConstants.Events.OnDeletedProfile, ValidateProfileTag);
            MainManager.Ins.EventManager.RemoveListener<int>(GameConstants.Events.OnCreatedProfile, ValidateAddProfileButton);
            _switchButton.onClick.RemoveListener(SwitchProfile);
        }

        private void Start()
        {
            ValidateProfileTag(ProfilesManager.Count);
        }

        /// <summary>
        /// Sets the profile tag to the given profile correct values.
        /// </summary>
        /// <param name="profile"><see cref="ProfileData"/> for the profile tag.</param>
        private void SetProfileTag(ProfileData profile)
        {
            if (profile == null) ResetProfileTag();

            _profileTagImage.color = ColorExtension.Deserialize(profile.Rgba);
            _profileTagNameText.text = profile.ProfileName;
            _profileTagDifficultyImage.enabled = true;
            _profileTagDifficultyImage.sprite = profile.CurrentDifficulty == GameDifficulty.Easy ? _easySprite : _hardSprite;
        }

        /// <summary>
        /// Checks if the profile tag should be resetted or not.
        /// </summary>
        /// <param name="profilesCount">Profiles count.</param>
        private void ValidateProfileTag(int profilesCount)
        {
            ValidateAddProfileButton(profilesCount);
            if (profilesCount.Equals(0)) ResetProfileTag();
        }

        /// <summary>
        /// Checks if the add profile button should be active or not.
        /// </summary>
        /// <param name="profilesCount">Profiles count.</param>
        private void ValidateAddProfileButton(int profilesCount)
        {
            bool profilesAreFull = profilesCount >= GameConstants.ProfileSaving.MaxProfilesCount;
            _addProfileButton.gameObject.SetActive(!profilesAreFull);
        }

        /// <summary>
        /// Switches to the next profile.
        /// </summary>
        private void SwitchProfile()
        {
            ProfileSelector.SelectNextProfile();
        }

        /// <summary>
        /// Resets the profile tag to is default values.
        /// </summary>
        private void ResetProfileTag()
        {
            _profileTagDifficultyImage.enabled = false;
            _profileTagImage.color = _defaultProfileTagColor;
            _profileTagNameText.text = _defaultProfileString;
        }
    }
}