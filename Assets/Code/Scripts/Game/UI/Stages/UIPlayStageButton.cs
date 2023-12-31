﻿namespace ProjectPBR.UI.Stages
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectPBR.Managers.Main.SceneManager;

    [RequireComponent(typeof(Button))]
    public class UIPlayStageButton : MonoBehaviour, ICastSceneManager<GameSceneManager>
    {
        private Button _button;

        public GameSceneManager SceneManager => MainManager.Ins.SceneManager as GameSceneManager;

        private void Awake()
        {
            TryGetComponent(out _button);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(LoadNextLevel);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(LoadNextLevel);
        }

        /// <summary>
        /// Loads the next level or cutscene.
        /// </summary>
        private void LoadNextLevel()
        {
            SceneManager.LoadCutsceneOrFirstUnlocked();
        }
    }
}
