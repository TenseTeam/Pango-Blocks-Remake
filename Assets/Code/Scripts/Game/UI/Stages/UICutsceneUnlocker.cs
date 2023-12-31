﻿namespace ProjectPBR.UI.Stages
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using ProjectPBR.Managers.Static;
    using ProjectPBR.Managers.Static.Profiles;

    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class UICutsceneUnlocker : MonoBehaviour
    {
        [SerializeField, Header("Status Sprites")]
        private Sprite _unlocked;

        [SerializeField]
        private Sprite _locked;

        private Image _image;
        private Button _button;
        private bool _isUnlocked;

        /// <summary>
        /// Initializes the cutscene unlocker.
        /// </summary>
        public void Init()
        {
            TryGetComponent(out _image);
            TryGetComponent(out _button);
            _button.onClick.AddListener(LoadCutscene);
        }

        /// <summary>
        /// Tries to load the cutscene.
        /// </summary>
        /// <returns>True if successful, False if not.</returns>
        public bool TryLaodCutscene()
        {
            if (!_isUnlocked) return false;

            int buildIndex = LevelMapper.GetCutsceneBuildIndex();
            MainManager.Ins.SceneManager.ChangeScene(buildIndex);

            return true;
        }

        /// <summary>
        /// Sets the status of the cutscene unlocker.
        /// </summary>
        public void SetStatus()
        {
            _isUnlocked = LevelOperation.IsCutsceneUnlocked(LevelMapper.CurrentStageIndex);
            SetSpriteStatus(_isUnlocked);
        }

        /// <summary>
        /// Sets the sprite status of the cutscene unlocker.
        /// </summary>
        /// <param name="isUnlocked">Is cutscene unlocked value.</param>
        private void SetSpriteStatus(bool isUnlocked)
        {
            _image.sprite = isUnlocked ? _unlocked : _locked;
        }

        /// <summary>
        /// Loads the cutscene.
        /// </summary>
        private void LoadCutscene() => TryLaodCutscene();
    }
}