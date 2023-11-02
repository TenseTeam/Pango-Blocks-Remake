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

    public class UIProfileSwitcher : MonoBehaviour
    {
        [SerializeField, Header("Switch Button")]
        private Button _switchButton;

        [SerializeField, Header("Profile Tag")]
        private TMP_Text _profileTagNameText;

        [SerializeField]
        private string _defaultProfileString;

        [SerializeField]
        private Image _profileTagDifficultyImage;

        [SerializeField, Header("Difficulty Sprites")]
        private Sprite _easySprite;

        [SerializeField]
        private Sprite _hardSprite;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<ProfileData>(GameConstants.Events.OnSelectedProfile, SetProfileTag);
            _switchButton.onClick.AddListener(SwitchProfile);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<ProfileData>(GameConstants.Events.OnSelectedProfile, SetProfileTag);
            _switchButton.onClick.RemoveListener(SwitchProfile);
        }

        private void Start()
        {
            //SetProfileTag(ProfileSelector.SelectedProfile);
            ValidateProfileTagNameField();
        }

        private void SetProfileTag(ProfileData profile)
        {
            if (profile == null) ResetTagNameText();

            _profileTagNameText.text = profile.ProfileName;
            _profileTagDifficultyImage.sprite = profile.CurrentDifficulty == GameDifficulty.Easy ? _easySprite : _hardSprite;
        }

        private void ValidateProfileTagNameField()
        {
            if (ProfilesManager.Count.Equals(0))
                ResetTagNameText();
        }

        private void ResetTagNameText()
        {
            _profileTagNameText.text = _defaultProfileString;
        }

        private void SwitchProfile()
        {
            ProfileSelector.SelectNextProfile();
        }
    }
}