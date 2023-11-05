namespace ProjectPBR.UI.Menu.Profiles
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Data.SaveDatas;
    using ProjectPBR.Data.SaveDatas.Enums;
    using ProjectPBR.GameConfig.Constants;
    using ProjectPBR.Managers.Static.Profiles;
    using System;
    using VUDK.Extensions.Colors;

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

        private void SetProfileTag(ProfileData profile)
        {
            if (profile == null) ResetProfileTag();

            _profileTagImage.color = ColorExtension.Deserialize(profile.Rgba);
            _profileTagNameText.text = profile.ProfileName;
            _profileTagDifficultyImage.enabled = true;
            _profileTagDifficultyImage.sprite = profile.CurrentDifficulty == GameDifficulty.Easy ? _easySprite : _hardSprite;
        }

        private void ValidateProfileTag(int count)
        {
            ValidateAddProfileButton(count);
            if (count.Equals(0)) ResetProfileTag();
        }

        private void ValidateAddProfileButton(int profilesCount)
        {
            bool profilesAreFull = profilesCount >= GameConstants.ProfileSaving.MaxProfilesCount;
            _addProfileButton.gameObject.SetActive(!profilesAreFull);
        }

        private void SwitchProfile()
        {
            ProfileSelector.SelectNextProfile();
        }

        private void ResetProfileTag()
        {
            _profileTagDifficultyImage.enabled = false;
            _profileTagImage.color = _defaultProfileTagColor;
            _profileTagNameText.text = _defaultProfileString;
        }
    }
}